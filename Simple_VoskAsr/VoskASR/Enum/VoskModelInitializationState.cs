using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoskASR
{
    /// <summary>
    /// Vosk初始化状态
    /// 郑伟 2023-05-07
    /// </summary>
    public enum VoskModelInitializationState : byte
    {
        /// <summary>
        /// 未开始
        /// </summary>
        None = 0,
        /// <summary>
        /// 未找到模型文件
        /// </summary>
        ModelFileMissing = 1,
        /// <summary>
        /// 开始初始化
        /// </summary>
        StartingInitialization = 2,
        /// <summary>
        /// 开始使用GPU初始化
        /// </summary>
        StartingGPUInitialization = 3,
        /// <summary>
        /// 模型初始化异常
        /// </summary>
        InitializationException = 4,
        /// <summary>
        /// 非64位应用无法初始化库
        /// </summary>
        Non64bitProgramException = 5,
        /// <summary>
        /// 初始化完成
        /// </summary>
        Success = 9,
    }
}
