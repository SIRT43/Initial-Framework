using System;

namespace FTGAMEStudio.InitialFramework.Reflection
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
    public class NonMappableAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class MapContainerAttribute : Attribute
    {
        public Type TargetType { get; protected set; }

        /// <summary>
        /// ������ָ��һ�� <see cref="Type"/> ʹ����Ϊ����������๤����
        /// </summary>
        public MapContainerAttribute(Type targetType) => TargetType = targetType;
    }
}
