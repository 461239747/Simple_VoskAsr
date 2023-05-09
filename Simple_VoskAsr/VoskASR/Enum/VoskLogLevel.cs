using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoskASR
{
    /// <summary>
    /// VOSK日志级别
    /// 郑伟 2023-05-07
    /// </summary>
    public enum VoskLogLevel : byte
    {
        /// <summary>
        /// (默认值) 不进行记录日志
        /// </summary>
        None = 0x00,
        /// <summary>
        /// 错误级别日志
        /// </summary>
        Error = 0x01,
        /// <summary>
        /// 警告级别日志
        /// </summary>
        Warn = 0x02,
        /// <summary>
        /// 信息级别日志
        /// </summary>
        Info = 0x03,
        /// <summary>
        /// 调试级别日志
        /// </summary>
        Debug = 0x04,
        /// <summary>
        /// 开发者模式日志
        /// </summary>
        Developer = 0x05
    }
}
