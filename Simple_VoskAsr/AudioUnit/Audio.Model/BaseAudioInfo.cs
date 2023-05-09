using AudioUnit.Audio.Struct;
using NAudio.Wave;
using System;
using System.Threading.Tasks;

namespace AudioUnit
{

    /// <summary>
    /// 播放的音频信息
    /// 郑伟 2023-03-30
    /// </summary>
    public class BaseAudioInfo
    {
        /// <summary>
        /// 播放停止
        /// </summary>
        public Action E_PlayFinished;

        /// <summary>
        /// 音频文件类型
        /// </summary>
        public AudioType AudioPlayType
        {
            get
            {
                return _AudioPlayType;
            }
            protected set
            {
                _AudioPlayType = value;
            }
        }
        private AudioType _AudioPlayType;

        /// <summary>
        /// 播放状态
        /// </summary>
        public PlayStatus PlayStatus
        {
            get
            {
                return _PlayStatus;
            }
            set
            {
                _PlayStatus = value;
            }
        }
        private PlayStatus _PlayStatus = PlayStatus.NotStarted;

        /// <summary>
        /// 播放次数
        /// -1时为一直播放
        /// </summary>
        public int PlaybackTimes
        {
            get
            {
                return _PlaybackTimes;
            }
            set
            {
                _PlaybackTimes = value;
            }
        }
        private int _PlaybackTimes = 1;

        /// <summary>
        /// 已播放次数
        /// </summary>
        private int PlayedTimes
        {
            get
            {
                return _PlayedTimes;
            }
            set
            {
                _PlayedTimes = value;
                if (PlaybackTimes > 0 && _PlayedTimes > PlaybackTimes)
                {
                    E_PlayFinished?.Invoke();
                }
            }
        }
        private int _PlayedTimes;

        /// <summary>
        /// 延迟播放时间 用于控制时间间隔 单位毫秒
        /// </summary>
        public int DelayTime
        {
            get
            {
                return _DelayTime;
            }
            set
            {
                _DelayTime = value;
            }
        }
        private int _DelayTime;

        /// <summary>
        /// 音频文件
        /// </summary>
        public WaveFileReader AudioReader
        {
            get
            {
                return _AudioReader;
            }
        }
        protected WaveFileReader _AudioReader;

        /// <summary>
        /// 初始化留
        /// </summary>
        public virtual void InitLoopStream()
        {

        }

        /// <summary>
        /// 开始播放
        /// </summary>
        public void Play()
        {
            if (_PlaybackTimes != 0)
            {
                PlayStatus = PlayStatus.WaitingPlay;
                if (_AudioReader == null)
                    InitLoopStream();
                if (_AudioReader != null)
                {
                    // 将 AudioFileReader 对象作为 WaveOut 的输入
                    WaveOut.Init(AudioReader);
                    WaveOut.PlaybackStopped += WaveOut_PlaybackStopped;

                    Task _PlayTask = Task.Delay(TimeSpan.FromMilliseconds(DelayTime)).ContinueWith(task =>
                    {
                        // 播放音频
                        WaveOut.Play();
                        PlayStatus = PlayStatus.Playing;
                    });
                }
            }
        }

        /// <summary>
        /// 音频输出对象
        /// </summary>
        private WaveOut WaveOut
        {
            get
            {
                if (_WaveOut == null)
                    _WaveOut = new WaveOut();
                return _WaveOut;
            }
        }
        private WaveOut _WaveOut;

        /// <summary>
        /// 停止播放
        /// </summary>
        public void Stop()
        {
            PlayStatus = PlayStatus.Completed;
            PlayedTimes = 0;
            PlaybackTimes = 0;
            if (WaveOut != null)
            {
                WaveOut.PlaybackStopped -= WaveOut_PlaybackStopped;
                WaveOut?.Stop();
                WaveOut?.Dispose();
            }
            AudioReader?.Dispose();
            E_PlayFinished?.Invoke();
        }

        /// <summary>
        /// 停止播放但不释放大部分资源
        /// </summary>
        public void StopWithOutDispose()
        {
            PlayStatus = PlayStatus.Completed;
            PlayedTimes = 0;
            if (WaveOut != null)
            {
                WaveOut.PlaybackStopped -= WaveOut_PlaybackStopped;
                WaveOut?.Stop();
                AudioReader.Position = 0;
            }
            E_PlayFinished?.Invoke();
        }

        /// <summary>
        /// 播放完成后重新启动播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WaveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            // 播放完成且未发生错误
            if (e.Exception == null)
            {
                // 增加计数器
                PlayedTimes++;
                // 检查播放次数是否达到最大值
                if (PlayedTimes < PlaybackTimes || PlaybackTimes < 0)
                {
                    // 重新启动播放
                    WaveOut.Stop();
                    AudioReader.Position = 0;
                    WaveOut.Play();
                }
                else
                {
                    // 播放次数达到最大值，注销事件处理程序并释放资源
                    StopWithOutDispose();
                }
            }
            else//发生异常 不再继续播放
            {
                // 播放次数达到最大值，注销事件处理程序并释放资源
                WaveOut.PlaybackStopped -= WaveOut_PlaybackStopped;
                AudioReader.Dispose();
                WaveOut.Dispose();
                PlayStatus = PlayStatus.Completed;
            }
        }
    }
}
