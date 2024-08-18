using FTGAMEStudio.InitialFramework.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace FTGAMEStudio.InitialFramework.Reflection
{
    /// <summary>
    /// 基于反射实现的变量成员映射。
    /// 
    /// <para>另请参阅 <seealso cref="System.Reflection"/>。</para>
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
        /// 验证容器是否可以与指定类型进行映射。
        /// </summary>
        public static bool VerifyMapping(Type container, Type instance)
        {
            if (!VerifyMapTarget(container, instance)) return false;
            if (!IsMappable(instance)) return false;

            return true;
        }



        /// <summary>  
        /// 将容器的变量映射到目标对象。  
        /// </summary>
        public static void MapVariables<T>(object container, ref T instance)
        {
            VariableMapper mapper = new();
            mapper.Map(container, ref instance);
        }

        /// <summary>  
        /// 将目标对象映射到容器的变量。  
        /// </summary>
        public static void ReverseMapVariables<T>(object container, ref T instance)
        {
            ReverseVariableMapper mapper = new();
            mapper.Map(container, ref instance);
        }



        /// <summary>
        /// 将容器类型按 <see cref="MapContainerAttribute.TargetType"/> 的 UniqueName 分类。
        /// </summary>
        public static Dictionary<string, Type> ClassifyMapTargetType(bool inherit = false, params Type[] containerTypes)
        {
            Dictionary<string, Type> classify = new();

            foreach (Type container in containerTypes)
            {
                string uniqueName = GetMapTargetType(container, inherit) is Type targetType ?
                     targetType.GetUniqueName()
                     :
                     throw new ArgumentException("Misclassified! Cannot classify a type that isn't a container or a container that has an empty mapping target.", nameof(container));

                classify.Add(uniqueName, container);
            }

            return classify;
        }
    }
}
