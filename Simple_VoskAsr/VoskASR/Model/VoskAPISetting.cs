using System.Runtime.InteropServices;

namespace VoskASR
{
    /// <summary>
    /// Vosk初始化设置
    /// 64位系统 对齐方式为4的倍数 Pack设置为1自定义对齐
    /// 郑伟 2023-05-05
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 11, Pack = 1)]
    public struct VoskAPISetting
    {
        /// <summary>
        /// 使用的语言模型
        /// 更多语言包请查看APILanguageModelType枚举注释
        /// </summary>
        [FieldOffset(0)]
        public APILanguageModelType model;

        /// <summary>
        /// 输入音频的采样率
        /// </summary>
        [FieldOffset(1)]
        public AudioRate sample_rate;

        /// <summary>
        /// 日志记录的级别
        /// </summary>
        [FieldOffset(5)]
        public VoskLogLevel log_level;

        /// <summary>
        /// 返回的最大备选结果数量
        /// </summary>
        [FieldOffset(6)]
        public int max_alternatives;

        /// <summary>
        /// 是否返回单词级别的结果
        /// True 识别每个词都带有时间戳 False只识别语句一般用False即可
        /// </summary>
        [FieldOffset(10)]
        public bool words;

        #region 这些属性在官方文档上存在但是在.Net平台未发现可以设置的地方 暂时注释
        ///// <summary>
        ///// 是否包含标点符号
        ///// </summary>
        //[FieldOffset(8)]
        //public bool punctuation;

        ///// <summary>
        ///// 增强语音识别结果的权重
        ///// 值范围在0-1-N 默认为1(标识不进行任何增益)
        ///// 音频增益,太大会导致音频失真
        ///// </summary>
        //[FieldOffset(9)]
        //public float boost;

        ///// <summary>
        ///// 是否使用语音活动检测
        ///// (推荐True) 对输入的音频进行分段处理，仅对包含语音信号的段进行语音识别
        ///// False 对整个音频进行识别，无法过滤掉非语音信号
        ///// </summary>
        //[FieldOffset(13)]
        //public bool vad;

        ///// <summary>
        ///// 语音活动检测的敏感度
        ///// 范围0-3 (默认:3) 
        ///// 值越高 越容易将非语音判断为语音信号
        ///// 降低可以降低误检率，但也可能会将一些较短或较弱的语音信号漏检
        ///// 场景较安静时推荐0-1 嘈杂时推荐2-3
        ///// </summary>
        //[FieldOffset(14)]
        //public int vad_aggressiveness;

        ///// <summary>
        ///// 识别结果的置信度阈值
        ///// 范围0-1(默认0)
        ///// 如果需要过滤一些置信度较低的单词可以设置高一些 提高识别精度 但是可能会忽略某些词
        ///// </summary>
        //[FieldOffset(18)]
        //public float words_threshold;

        ///// <summary>
        ///// 静默时间的超时阈值(单位:毫秒)
        ///// 默认值:500
        ///// 通常用于实时语音识别
        ///// </summary>
        //[FieldOffset(22)]
        //public int silence_timeout;

        ///// <summary>
        ///// 识别的最大持续时间(单位:毫秒)
        ///// 默认:-1 一直等待识别
        ///// </summary>
        //[FieldOffset(26)]
        //public int timeout;

        ///// <summary>
        ///// 语音活动检测的窗口长度(单位:毫秒)
        ///// 默认值:30
        ///// 降低值可以提高 VAD 的精度，但会增加计算量和延迟
        ///// 提高值可能降低 VAD 的精度, 但会降低计算量和延迟
        ///// </summary>
        //[FieldOffset(30)]
        //public int vad_window_length;

        ///// <summary>
        ///// 是否进行说话人分离
        ///// True 使用说话人模型 分离出说话人说的语句 适用于嘈杂场景
        ///// False 默认识别
        ///// </summary>
        //public bool diarization;

        ///// <summary>
        ///// 说话人模型所在的目录
        ///// </summary>
        //public string speaker_models_dir;
        #endregion
    }
}
