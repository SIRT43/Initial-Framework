using FTGAMEStudio.InitialFramework.Classifying;
using FTGAMEStudio.InitialFramework.ExtensionMethods;
using System.Collections.Generic;

namespace FTGAMEStudio.InitialFramework.Reflection
{
    public static class ReflectionMacro
    {
        /// <summary>
        /// �������ఴ UniqueName ���ࡣ
        /// </summary>
        public static Dictionary<string, List<T>> ClassifyWithUniqueName<T>(T[] values)
        {
            FilterableClassifier<string, T> classifier = new((T value, out string key) =>
            {
                key = value.GetType().GetUniqueName();
                return true;
            });

            return classifier.Classify(values);
        }
    }
}
