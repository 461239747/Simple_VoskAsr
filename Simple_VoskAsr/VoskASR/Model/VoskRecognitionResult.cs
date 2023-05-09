using System;

namespace VoskASR
{
    /// <summary>
    /// 识别结果
    /// 郑伟 2023-05-05
    /// </summary>
    public struct VoskRecognitionResult
    {
        /// <summary>
        /// 错误码 
        /// </summary>
        public VoskError_Code err_no;
        /// <summary>
        /// 异常信息
        /// </summary>
        public string err_msg;
        /// <summary>
        /// 多条语音的识别结果
        /// </summary>
        public Alternative[] alternatives;
        /// <summary>
        /// 产生结果的时间
        /// </summary>
        public DateTime ResultTime;
    }
}
