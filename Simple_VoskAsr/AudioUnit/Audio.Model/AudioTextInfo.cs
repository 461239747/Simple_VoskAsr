using AudioUnit.Audio.Struct;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace AudioUnit
{
    /// <summary>
    /// 文本播放信息
    /// </summary>
    public class AudioTextInfo : BaseAudioInfo
    {
        public AudioTextInfo()
        {
            AudioPlayType = AudioType.Text;
        }

        /// <summary>
        /// 要播放的音频文本
        /// </summary>
        public string PlayText
        {
            get
            {
                return _PlayText;
            }
            set
            {
                _PlayText = value;
            }
        }
        private string _PlayText;

        #region 合成音频流
        /// <summary>
        /// 初始化流
        /// </summary>
        public override void InitLoopStream()
        {
            _AudioReader = new WaveFileReader(TextStream);
        }

        /// <summary>
        /// 文本音频流
        /// </summary>
        private MemoryStream TextStream
        {
            get
            {
                // 将文本转换为语音流
                MemoryStream stream = new MemoryStream();
                // 创建语音合成器
                using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
                {
                    synthesizer.SetOutputToWaveStream(stream); // 设置输出流
                                                               // 设置语音合成器的属性
                    synthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Senior);
                    synthesizer.Rate = 0; // 设置语速（-10到10）
                    synthesizer.Speak(PlayText); // 开始合成语音
                    stream.Position = 0;
                }
                return stream;
            }
        }
        #endregion
    }
}
