namespace InitialFramework.Traverse
{
    /// <summary>  
    /// 流程控制。  
    /// <br>它规定应该如何在指定状态下使用什么控制符。</br>  
    /// </summary>  
    public enum FlowControl
    {
        /// <summary>  
        /// 跳出方法。  
        /// </summary>  
        Return,
        /// <summary>  
        /// 跳出循环。  
        /// </summary>  
        Break,
        /// <summary>  
        /// 跳过。  
        /// </summary>  
        Continue,
    }
}