using System;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Vosk;

using static System.Net.Mime.MediaTypeNames;

namespace VoskASR
{
    /// <summary>
    /// VOSK语音识别接口
    /// 郑伟 2023-05-07
    /// </summary>
    public static class VoskTTSInstance
    {
        /// <summary>
        /// 音频识别对象
        /// </summary>
        static VoskRecognizer voskRecognizer;

        /// <summary>
        /// 加载的Vosk模型
        /// </summary>
        static Model VoskModel;

        /// <summary>
        /// 初始化音频识别模型
        /// </summary>
        /// <param name="voskAPISetting"></param>
        /// <param name="initializationed_Callback">初始化完成回调函数 True初始化成功 Flase初始化失败</param>
        public static void InitializationVoskModel(VoskAPISetting voskAPISetting, Action<VoskModelInitializationState> initializationed_Callback)
        {
            initializationed_Callback?.Invoke(VoskModelInitializationState.None);
            bool is64Bit = IntPtr.Size == 8;
            if (is64Bit)
            {
                string modelDirectoryName = APILanguageModelTypePath(voskAPISetting.model);
                string modelPath = Environment.CurrentDirectory + "\\" + modelDirectoryName;
#if UNITY_64
                bool hasGPU = GPUSearch();
#endif
                if (Directory.Exists(modelPath))
                {
                    Task.Run(() =>
                    {
                        if (VoskModel != null)
                        {
                            VoskModel.Dispose();
                        }
                        Vosk.Vosk.SetLogLevel((int)voskAPISetting.log_level);
                        //包含GPU时 且显存大于2G使用GPU进行计算
#if !UNITY_64
                        if (GPUSearch())
                        {
                            Vosk.Vosk.GpuInit();
                            initializationed_Callback.Invoke(VoskModelInitializationState.StartingGPUInitialization);
                        }
#endif
#if UNITY_64
                        if (hasGPU)
                        {
                            Vosk.Vosk.GpuInit();
                            initializationed_Callback.Invoke(VoskModelInitializationState.StartingGPUInitialization);
                        }
#endif
                        else
                            initializationed_Callback.Invoke(VoskModelInitializationState.StartingInitialization);

                        #region 初始化模型
                        try
                        {
                            VoskModel = new Model(modelDirectoryName);
                            voskRecognizer = new VoskRecognizer(VoskModel, (int)voskAPISetting.sample_rate);

                            voskRecognizer.SetMaxAlternatives(voskAPISetting.max_alternatives);
                            voskRecognizer.SetWords(voskAPISetting.words);

                            initializationed_Callback?.Invoke(VoskModelInitializationState.Success);
                        }
                        catch (Exception ex)
                        {
                            initializationed_Callback?.Invoke(VoskModelInitializationState.InitializationException);
                        }
                        #endregion
                    });
                }
                else
                {
                    initializationed_Callback?.Invoke(VoskModelInitializationState.ModelFileMissing);
                }
            }
            else
            {
                initializationed_Callback?.Invoke(VoskModelInitializationState.Non64bitProgramException);
            }
        }

        /// <summary>
        /// 音频识别
        /// </summary>
        /// <param name="audioFilePath"></param>
        /// <returns></returns>
        public static VoskRecognitionResult Recognize(string audioFilePath)
        {
            VoskRecognitionResult recognitionResult = new VoskRecognitionResult();

            if (voskRecognizer == null)
            {
                recognitionResult.err_no = VoskError_Code.ModelNotInitializedCompleted;
                recognitionResult.err_msg = "语音识别初始化未完成";
                recognitionResult.ResultTime = DateTime.Now;
            }
            else if (!File.Exists(audioFilePath))
            {
                recognitionResult.err_no = VoskError_Code.FileNotExist;
                recognitionResult.err_msg = "文件不存在";
                recognitionResult.ResultTime = DateTime.Now;
            }
            else
            {
                try
                {
                    // 打开音频文件
                    using (FileStream stream = new FileStream(audioFilePath, FileMode.Open, FileAccess.Read))
                    {
                        byte[] data = new byte[stream.Length];
                        stream.Read(data, 0, data.Length);

                        voskRecognizer.AcceptWaveform(data, data.Length);
                        string finalResult = voskRecognizer.FinalResult();

                        recognitionResult.err_no = VoskError_Code.Success;
                        recognitionResult = JsonConvert.DeserializeObject<VoskRecognitionResult>(finalResult);
                        recognitionResult.ResultTime = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                    recognitionResult.err_no = VoskError_Code.RecognizeAudioException;
                    recognitionResult.err_msg = "语音识别过程中发生异常";
                    recognitionResult.ResultTime = DateTime.Now;
                }
            }
            return recognitionResult;
        }

        /// <summary>
        /// 音频识别
        /// </summary>
        /// <param name="audioData"></param>
        /// <returns></returns>
        public static VoskRecognitionResult Recognize(byte[] audioData)
        {
            VoskRecognitionResult recognitionResult = new VoskRecognitionResult();

            if (voskRecognizer == null)
            {
                recognitionResult.err_no = VoskError_Code.ModelNotInitializedCompleted;
                recognitionResult.err_msg = "语音识别初始化未完成";
                recognitionResult.ResultTime = DateTime.Now;
            }
            else
            {
                try
                {
                    voskRecognizer.AcceptWaveform(audioData, audioData.Length);
                    string finalResult = voskRecognizer.FinalResult();

                    recognitionResult.err_no = VoskError_Code.Success;
                    recognitionResult = JsonConvert.DeserializeObject<VoskRecognitionResult>(finalResult);
                    recognitionResult.ResultTime = DateTime.Now;
                }
                catch (Exception ex)
                {
                    recognitionResult.err_no = VoskError_Code.RecognizeAudioException;
                    recognitionResult.err_msg = "语音识别过程中发生异常";
                    recognitionResult.ResultTime = DateTime.Now;
                }
            }
            return recognitionResult;
        }

        /// <summary>
        /// 获得语言模型的路径
        /// </summary>
        /// <param name="aPILanguageModelType"></param>
        /// <returns></returns>
        private static string APILanguageModelTypePath(APILanguageModelType aPILanguageModelType)
        {
            switch (aPILanguageModelType)
            {
                case APILanguageModelType.CN_Complete_022:
#if !UNITY_64
                    return "vosk-model-cn-0.22";
#endif
#if UNITY_64
                        return Application.streamingAssetsPath + @"/vosk-model-cn-0.22";
#endif
                case APILanguageModelType.CN_SmallModel_022:
#if !UNITY_64
                    return "vosk-model-small-cn-0.22";
#endif
#if UNITY_64
                        return Application.streamingAssetsPath + @"/vosk-model-small-cn-0.22";
#endif
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// 获得默认的语音识别设置参数
        /// </summary>
        /// <returns></returns>
        public static VoskAPISetting GetDefaultVoskAPISetting()
        {
            VoskAPISetting voskAPISetting = new VoskAPISetting();
            voskAPISetting.model = APILanguageModelType.CN_Complete_022;
            voskAPISetting.sample_rate = AudioRate.Rate_48000;
            voskAPISetting.log_level = VoskLogLevel.None;
            voskAPISetting.max_alternatives = 1;
            voskAPISetting.words = false;
            #region 这些属性在官方文档上存在但是在.Net平台未发现可以设置的地方 暂时注释
            //voskAPISetting.punctuation = true;
            //voskAPISetting.boost = 1;
            //voskAPISetting.vad = true;
            //voskAPISetting.vad_aggressiveness = 1;
            //voskAPISetting.words_threshold = 0.2F;
            //voskAPISetting.silence_timeout = 500;
            //voskAPISetting.timeout = System.Threading.Timeout.Infinite;
            //voskAPISetting.vad_window_length = 20;
            #endregion
            return voskAPISetting;
        }

        /// <summary>
        /// 判断电脑是否包含GPU
        /// </summary>
        /// <returns></returns>
        public static bool GPUSearch()
        {
#if UNITY_64
            // 检查是否存在GPU
            if (SystemInfo.graphicsDeviceType != GraphicsDeviceType.Null)
            {
                // 检查显存大小是否大于2G
                if (SystemInfo.graphicsMemorySize > 2048)
                {
                    // 显存大小大于2G
                    return true;
                }
            }
            return false;
#endif
#if !UNITY_64
            ManagementObjectSearcher gpuSearcher = new ManagementObjectSearcher("SELECT AdapterRAM FROM Win32_VideoController");
            foreach (ManagementObject gpu in gpuSearcher.Get())
            {
                if (long.Parse(gpu["AdapterRAM"].ToString()) >= (2L * 1024 * 1024 * 1024))
                {
                    return true;
                }
            }
            return false;
#endif
        }
    }
}
