using System;

namespace FTGAMEStudio.InitialFramework.Reflection
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
    public class NonMappableAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class MapContainerAttribute : Attribute
    {
        protected Type targetType;
        public Type TargetType => targetType;

        /// <summary>
        /// ������ָ��һ�� <see cref="Type"/> ������Ϊ�乤����
        /// </summary>
        public MapContainerAttribute(Type targetType) => this.targetType = targetType;
    }
}
