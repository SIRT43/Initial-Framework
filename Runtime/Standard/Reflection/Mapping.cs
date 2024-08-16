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

        /// <summary>
        /// �������� <see cref="MapContainerAttribute.TargetType"/> �� UniqueName ���ࡣ
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
        /// ��֤����Ŀ�������Ƿ������͡�
        /// </summary>
        public static bool VerifyMapTarget(Type from, Type to)
        {
            if (GetMapTargetType(from, true) is Type type)
                return type == to || to.IsSubclassOf(from);

            return false;
        }

        /// <summary>
        /// ��֤�����������Ƿ���Խ���ӳ�䡣
        /// </summary>
        public static bool VerifyMapping(Type from, Type to)
        {
            if (!VerifyMapTarget(from, to)) return false;
            if (!IsMappable(to)) return false;

            return true;
        }

        /// <summary>  
        /// �������ı���ӳ�䵽Ŀ�����  
        /// </summary>
        public static object MapVariables(object container, object instance)
        {
            VariableMapper mapper = new();
            mapper.Map(container, instance);

            return mapper.Result;
        }

        /// <summary>  
        /// ��Ŀ�����ӳ�䵽�����ı�����  
        /// </summary>
        public static object ReverseMapVariables(object container, object instance)
        {
            ReverseVariableMapper mapper = new();
            mapper.Map(container, instance);

            return mapper.Result;
        }
    }
}
