using System;
using System.Collections.Generic;
using System.Reflection;
using InitialFramework.Classifying;

namespace InitialFramework.Reflection
{
    /// <summary>
    /// 基于反射实现的变量成员映射。
    /// 
    /// <para>另请参阅 System.Reflection。</para>
    /// </summary>
    public static class Mapping
    {
        public static bool IsMappable(MemberInfo memberInfo) => !memberInfo.IsDefined(typeof(NonMappableAttribute), true);
        public static bool IsMappable(VariableInfo variableInfo) => IsMappable(variableInfo as MemberInfo) && IsMappable(variableInfo.ValueType);



        public static Type GetMapTargetType(Type container, bool inherit = false)
        {
            if (container.GetCustomAttribute<MapContainerAttribute>(inherit) is MapContainerAttribute attribute)
                return attribute.TargetType;

            return null;
        }

        public static Type GetMapTargetType(object container, bool inherit = false) => GetMapTargetType(container.GetType(), inherit);



        /// <summary>
        /// 验证容器的目标类型是否是指定类型。
        /// </summary>
        public static bool VerifyMapTarget(Type container, Type instance)
        {
            if (GetMapTargetType(container, true) is Type mapTarget)
                return mapTarget == instance || instance.IsSubclassOf(container);

            return false;
        }

        /// <summary>
        /// 验证容器的目标类型是否是指定类型。
        /// </summary>
        public static bool VerifyMapTarget(object container, object instance) => VerifyMapTarget(container.GetType(), instance.GetType());



        /// <summary>  
        /// 验证容器是否可以与指定类型进行映射。
        /// </summary>
        public static bool VerifyMapping(Type container, Type instance)
        {
            if (!IsMappable(instance)) return false;
            if (!VerifyMapTarget(container, instance)) return false;

            return true;
        }

        /// <summary>  
        /// 验证容器是否可以与指定类型进行映射。
        /// </summary>
        public static bool VerifyMapping(object container, object instance) => VerifyMapping(container.GetType(), instance.GetType());



        /// <summary>
        /// 将容器类型按 <see cref="MapContainerAttribute.TargetType"/> 的 UniqueName 分类。
        /// </summary>
        public static Dictionary<string, Type> ClassifyMapTargetType(bool inherit = false, params Type[] containerTypes)
        {
            FilterableMonoClassifier<string, Type> classifier = new((value, context) => GetMapTargetType(value, inherit) is Type targetType ?
                     targetType.GetUniqueName()
                     :
                     throw new ArgumentException("Misclassified! Cannot classify a type that isn't a container or a container that has an empty mapping target.", nameof(value)));

            return classifier.Classify(containerTypes);
        }
    }
}
