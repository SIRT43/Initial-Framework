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
        /// 您必须指定一个 <see cref="Type"/> 使容器为其或其派生类工作。
        /// </summary>
        public MapContainerAttribute(Type targetType) => this.targetType = targetType;
    }
}
