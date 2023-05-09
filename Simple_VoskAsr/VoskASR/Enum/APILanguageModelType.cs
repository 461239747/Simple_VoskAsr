namespace VoskASR
{
    /// <summary>
    /// 语言设置
    /// 语言包下载地址:https://alphacephei.com/vosk/models 根据需要下载即可
    /// 郑伟 2023-05-07
    /// </summary>
    public enum APILanguageModelType : byte
    {
        /// <summary>
        /// 完整的中文模型
        /// vosk-model-cn-0.22
        /// </summary>
        CN_Complete_022 = 0,
        /// <summary>
        /// 较小的语言模型
        /// vosk-model-small-cn-0.22
        /// </summary>
        CN_SmallModel_022 = 1,
        /// <summary>
        /// 较小的英文识别模型
        /// Small English Model
        /// vosk-model-small-en-us-0.15
        /// </summary>
        EN_US_SmallModel_015 = 2,
        /// <summary>
        /// 完整的英文模型
        /// Complete English Model
        /// vosk-model-en-us-0.22
        /// </summary>
        EN_US_Complete_022 = 3,
    }
}
