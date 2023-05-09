namespace AudioUnit.Audio.Struct
{
    /// <summary>
    /// 播放状态
    /// 郑伟 2023-03-30
    /// </summary>
    public enum PlayStatus
    {
        /// <summary>
        /// 未播放
        /// </summary>
        NotStarted,

        /// <summary>
        /// 等待播放
        /// </summary>
        WaitingPlay,

        /// <summary>
        /// 播放中
        /// </summary>
        Playing,

        /// <summary>
        /// 已完成
        /// </summary>
        Completed,
    }
}