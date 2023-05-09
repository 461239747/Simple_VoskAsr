namespace VoskASR
{
    /// <summary>
    /// 语音识别错误码
    /// </summary>
    public enum VoskError_Code : byte
    {
        /// <summary>
        /// 识别成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 模型未初始化完成
        /// </summary>
        ModelNotInitializedCompleted = 5,
        /// <summary>
        /// 识别失败
        /// 文件不存在
        /// </summary>
        FileNotExist = 7,
        /// <summary>
        /// 识别语音数据时发生异常
        /// </summary>
        RecognizeAudioException = 9,
    }
}
