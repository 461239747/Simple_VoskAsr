using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoskASR
{
    /// <summary>
    /// 多条语音结果
    /// 郑伟 2023-05-07
    /// </summary>
    public class Alternative
    {
        /// <summary>
        /// 可信度
        /// 数值越高,程序认为的优先级越高
        /// </summary>
        public float confidence;

        /// <summary>
        /// 对应的语音结果
        /// </summary>
        public string text;
    }
}
