using AudioUnit.Audio.Struct;
using NAudio.Wave;
using System.IO;

namespace AudioUnit
{
    /// <summary>
    /// 音频文件播放信息
    /// </summary>
    public class AudioFilesInfo : BaseAudioInfo
    {
        public AudioFilesInfo()
        {
            AudioPlayType = AudioType.AudioFiles;
        }

        /// <summary>
        /// 音频文件路径
        /// </summary>
        public string FilePath
        {
            get
            {
                return _FilePath;
            }
            set
            {
                _FilePath = value;
            }
        }

        private string _FilePath;

        #region 读取音频流

        /// <summary>
        /// 初始化流
        /// </summary>
        public override void InitLoopStream()
        {
            if (File.Exists(FilePath))
                _AudioReader = new WaveFileReader(FilePath);
        }

        #endregion
    }
}