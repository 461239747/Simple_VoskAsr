using System;
using System.Diagnostics;
using System.IO;

using NAudio.Wave;

namespace AudioUnit.SoundRecord
{
    /// <summary>
    /// 音频录音器
    /// 郑伟 2023-05-05
    /// </summary>
    public static class Recorder
    {
        /// <summary>
        /// 录音设备对象
        /// </summary>
        private static WaveIn mWavIn;

        /// <summary>
        /// 录音文件写入对象
        /// </summary>
        private static WaveFileWriter mWavWriter;

        /// <summary>
        /// 开始录音
        /// </summary>
        /// <param name="filePath"></param>
        public static void StartRecord(string filePath)
        {
            //是否有录音设备
            if (WaveIn.DeviceCount > 0)
            {
                try
                {
                    string directoryPath = Path.GetDirectoryName(filePath);
                    //文件目录不存在则创建
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    // 如果文件存在则删除
                    if (File.Exists(filePath))
                    {
                        try
                        {
                            // 删除文件
                            File.Delete(filePath);
                        }
                        catch (IOException ex)
                        {
                            // 判断是否是文件被占用所致
                            if (IsFileLocked(ex))
                            {
                                // 获取占用文件的进程
                                Process[] processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(filePath));
                                foreach (Process process in processes)
                                {
                                    process.Kill();
                                }
                                // 删除文件
                                File.Delete(filePath);
                            }
                            else
                            {
                                // 处理其他异常
                                Console.WriteLine("Error: " + ex.Message);
                            }
                        }
                    }

                    if (!File.Exists(filePath))
                    {
                        //默认为PCM编码
                        mWavIn = new WaveIn { WaveFormat = new WaveFormat(48000, 1) };//设置码率
                        mWavIn.DataAvailable += MWavIn_DataAvailable;
                        mWavIn.RecordingStopped += MWavIn_RecordingStopped;
                        mWavWriter = new WaveFileWriter(filePath, mWavIn.WaveFormat);
                        mWavIn.StartRecording();
                    }
                }
                catch (Exception ex)
                {
                    //MsgBox.ShowInfoMsgBox("录音失败");
                    Console.WriteLine("录音失败");
                }
            }
            else
            {
                Console.WriteLine("没有录音设备");
            }
        }

        /// <summary>
        /// 停止录音
        /// </summary>
        public static string StopRecord()
        {
            try
            {
                mWavIn?.StopRecording();
                mWavIn?.Dispose();
                mWavWriter?.Close();
                if (mWavWriter != null && mWavWriter.TotalTime.TotalSeconds > 0.5D)
                    return mWavWriter.Filename;
                else
                    return string.Empty;
            }
            finally
            {
                mWavIn = null;
                mWavWriter = null;
            }
        }

        /// <summary>
        /// 录音停止后释放录制对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MWavIn_RecordingStopped(object sender, StoppedEventArgs e)
        {
            mWavIn?.Dispose();
            mWavIn = null;
        }

        /// <summary>
        /// 写入录制的音频文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MWavIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            mWavWriter.Write(e.Buffer, 0, e.BytesRecorded);
        }

        /// <summary>
        /// 判断是否是文件被占用所致
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static bool IsFileLocked(IOException ex)
        {
            int errorCode = ex.HResult & ((1 << 16) - 1);
            return errorCode == 32 || errorCode == 33;
        }
    }
}
