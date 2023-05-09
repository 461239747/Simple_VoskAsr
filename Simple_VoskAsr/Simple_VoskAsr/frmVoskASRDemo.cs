using System;
using System.IO;
using System.Windows.Forms;

using AudioUnit.SoundRecord;

using VoskASR;

namespace Simple_VoskAsr
{
    /// <summary>
    /// Vosk调用语音识别的Demo
    /// </summary>
    public partial class frmVoskASRDemo : Form
    {
        public frmVoskASRDemo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化模型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmVoskASRDemo_Load(object sender, EventArgs e)
        {
            VoskAPISetting voskAPISetting = VoskTTSInstance.GetDefaultVoskAPISetting();
            //更多模型请下载包后再使用
            voskAPISetting.model = APILanguageModelType.CN_SmallModel_022;
            VoskTTSInstance.InitializationVoskModel(voskAPISetting, VoskModelInitializationed_Callback);
        }

        /// <summary>
        /// 鼠标按下 开始录音
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            string sPath = $"{Application.StartupPath}/audio/{DateTime.Now.ToString("yyyyMMdd")}/";
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
            string fileName = $"{DateTime.Now.ToString("yyyyMMdd-HHmmss")}.wav";

            Recorder.StartRecord(sPath + fileName);
        }

        /// <summary>
        /// 鼠标抬起 结束录音
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            string filePath = Recorder.StopRecord();
            txtfilePath.Text = filePath;

            VoskRecognitionResult recognize = VoskTTSInstance.Recognize(filePath);
            //VoskRecognitionResult recognize = VoskTTSInstance.Recognize(byte[]);

            txtResult.Text = "语音识别结果:" + recognize.alternatives[0].text;
        }

        /// <summary>
        /// 是否初始化成功
        /// </summary>
        /// <param name="isSuccess"></param>
        private void VoskModelInitializationed_Callback(VoskModelInitializationState initializationState)
        {
            this.Invoke(new Action(() =>
            {
                switch (initializationState)
                {
                    case VoskModelInitializationState.StartingInitialization:
                    case VoskModelInitializationState.StartingGPUInitialization:
                        btnRecord.Text = "正在初始化语言模型";
                        break;
                    case VoskModelInitializationState.Success:
                        btnRecord.Text = "录音";
                        btnRecord.Enabled = true;
                        break;
                    case VoskModelInitializationState.ModelFileMissing:
                        MessageBox.Show("未找到语言模型");
                        break;
                    case VoskModelInitializationState.Non64bitProgramException:
                        MessageBox.Show("语言模型仅支持在64位程序下运行");
                        break;
                    case VoskModelInitializationState.InitializationException:
                        MessageBox.Show("语言模型初始化异常");
                        break;
                }
            }));
        }
    }
}
