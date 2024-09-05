using System;

namespace FTGAMEStudio.InitialFramework
{
    /// <summary>
    /// �����С���࣬���ڶ���һ����С��
    /// </summary>
    [Serializable]
    public class MinMax<T> where T : struct
    {
        public T min;
        public T max;

        protected MinMax() : this(default, default) { }
        protected MinMax(T min, T max)
        {
            this.min = min;
            this.max = max;
        }
    }
}
