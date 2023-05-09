using NAudio.Wave;

using System;
using System.IO;

namespace AudioUnit.SoundRecord
{
    /// <summary>
    /// 音频录音器
    /// 郑伟 2023-05-05
    /// </summary>
    public static class ByteRecorder
    {
        /// <summary>
        /// 录音设备对象
        /// </summary>
        private static WaveIn mWavIn;

        /// <summary>
        /// 录制的音频数据
        /// </summary>
        private static MemoryStream mRecordedAudioStream;

        /// <summary>
        /// 开始录音
        /// </summary>
        public static void StartRecord()
        {
            //是否有录音设备
            if (WaveIn.DeviceCount > 0)
            {
                try
                {
                    mRecordedAudioStream = new MemoryStream();
                    //默认为PCM编码
                    mWavIn = new WaveIn { WaveFormat = new WaveFormat(48000, 1) };//设置码率
                    mWavIn.DataAvailable += MWavIn_DataAvailable;
                    mWavIn.RecordingStopped += MWavIn_RecordingStopped;
                    mWavIn.StartRecording();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("录音失败：" + ex.Message);
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
        public static byte[] StopRecord()
        {
            try
            {
                mWavIn?.StopRecording();
                mWavIn?.Dispose();
                return mRecordedAudioStream.ToArray();
            }
            finally
            {
                mWavIn = null;
                mRecordedAudioStream = null;
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
            mRecordedAudioStream.Write(e.Buffer, 0, e.BytesRecorded);
        }
    }
}
