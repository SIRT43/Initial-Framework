using FTGAMEStudio.InitialFramework.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace FTGAMEStudio.InitialFramework.Reflection
{
    /// <summary>
    /// ���ڷ���ʵ�ֵı�����Աӳ�䡣
    /// 
    /// <para>������� <seealso cref="System.Reflection"/>��</para>
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
        /// ��֤������Ŀ�������Ƿ���ָ�����͡�
        /// </summary>
        public static bool VerifyMapTarget(Type container, Type instance)
        {
            if (GetMapTargetType(container, true) is Type mapTarget)
                return mapTarget == instance || instance.IsSubclassOf(container);

            return false;
        }

        /// <summary>  
        /// ��֤�����Ƿ������ָ�����ͽ���ӳ�䡣
        /// </summary>
        public static bool VerifyMapping(Type container, Type instance)
        {
            if (!VerifyMapTarget(container, instance)) return false;
            if (!IsMappable(instance)) return false;

            return true;
        }



        /// <summary>  
        /// �������ı���ӳ�䵽Ŀ�����  
        /// </summary>
        public static void MapVariables<T>(object container, ref T instance)
        {
            VariableMapper mapper = new();
            mapper.Map(container, ref instance);
        }

        /// <summary>  
        /// ��Ŀ�����ӳ�䵽�����ı�����  
        /// </summary>
        public static void ReverseMapVariables<T>(object container, ref T instance)
        {
            ReverseVariableMapper mapper = new();
            mapper.Map(container, ref instance);
        }



        /// <summary>
        /// ���������Ͱ� <see cref="MapContainerAttribute.TargetType"/> �� UniqueName ���ࡣ
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
