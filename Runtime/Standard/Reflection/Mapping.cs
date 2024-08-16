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

        /// <summary>
        /// 将容器按 <see cref="MapContainerAttribute.TargetType"/> 的 UniqueName 分类。
        /// </summary>
        public static Dictionary<string, Type> ClassifyMapTargetType(bool inherit = false, params Type[] containers)
        {
            Dictionary<string, Type> classify = new();

            foreach (Type container in containers)
            {
                string uniqueName = GetMapTargetType(container, inherit) is Type targetType ?
                     targetType.GetUniqueName()
                     :
                     throw new ArgumentException("Misclassified! Cannot classify a type that isn't a container or a container that has an empty mapping target.", nameof(container));

                classify.Add(uniqueName, container);
            }

            return classify;
        }

        /// <summary>
        /// 验证容器目标类型是否是类型。
        /// </summary>
        public static bool VerifyMapTarget(Type from, Type to)
        {
            if (GetMapTargetType(from, true) is Type type)
                return type == to || to.IsSubclassOf(from);

            return false;
        }

        /// <summary>
        /// 验证容器和类型是否可以进行映射。
        /// </summary>
        public static bool VerifyMapping(Type from, Type to)
        {
            if (!VerifyMapTarget(from, to)) return false;
            if (!IsMappable(to)) return false;

            return true;
        }

        /// <summary>  
        /// 将容器的变量映射到目标对象。  
        /// </summary>
        public static object MapVariables(object container, object instance)
        {
            VariableMapper mapper = new();
            mapper.Map(container, instance);

            return mapper.Result;
        }

        /// <summary>  
        /// 将目标对象映射到容器的变量。  
        /// </summary>
        public static object ReverseMapVariables(object container, object instance)
        {
            ReverseVariableMapper mapper = new();
            mapper.Map(container, instance);

            return mapper.Result;
        }
    }
}
