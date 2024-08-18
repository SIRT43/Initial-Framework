using FTGAMEStudio.InitialFramework.ExtensionMethods;
using System;
using System.Reflection;

namespace FTGAMEStudio.InitialFramework.Reflection
{
    /// <summary>  
    /// ���ӿڶ�����ӳ�����Ļ����ṹ�͹��ܣ�����ӳ�����Ի��ֶΡ�  
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// Ӧ����������������ѯ��Ա��
        /// </summary>
        public BindingFlags BindingAttr { get; set; }

        public bool IsMappable(MemberInfo memberInfo);
        public bool IsMappable(VariableInfo variableInfo);

        /// <summary>
        /// ��֤������Ŀ�������Ƿ���ָ�����͡�
        /// </summary>
        public bool VerifyMapTarget(Type container, Type instance);
        /// <summary>  
        /// ��֤�����Ƿ������ָ�����ͽ���ӳ�䡣
        /// </summary>
        public bool VerifyMapping(Type container, Type instance);

        public bool Map<T>(object container, ref T instance);
    }

    /// <summary>  
    /// MapperBase ������ӳ������Ļ��࣬�ṩ��ӳ������Ļ�����ܺ����ԡ�  
    /// </summary>
    public abstract class MapperBase : IMapper
    {
        public virtual BindingFlags BindingAttr { get; set; }

        public virtual bool IsMappable(MemberInfo memberInfo) => Mapping.IsMappable(memberInfo);
        public virtual bool IsMappable(VariableInfo variableInfo) => Mapping.IsMappable(variableInfo);

        public virtual bool VerifyMapTarget(Type container, Type instance) => Mapping.VerifyMapTarget(container, instance);
        public virtual bool VerifyMapping(Type container, Type instance) => Mapping.VerifyMapping(container, instance);


        public virtual bool Map<T>(object container, ref T instance)
        {
            Type containerType = container.GetType();
            Type instanceType = instance.GetType();

            if (!VerifyMapping(containerType, instanceType)) return false;

            foreach (VariableInfo containerVariable in containerType.GetVariables(BindingAttr))
            {
                if (!IsMappable(containerVariable)) continue;

                VariableInfo instanceVariable =
                    instanceType.GetVariable(containerVariable.Name, containerVariable.ValueType, BindingAttr)
                    ??
                    throw new MissingFieldException($"The Type {containerType} member '{containerVariable.Name}' could not be found in the Type {instanceType}.");

                if (!IsMappable(instanceType)) continue;

                TryMap(container, ref instance, containerVariable, instanceVariable);
            }

            return true;
        }

        protected abstract void TryMap<T>(object container, ref T instance, VariableInfo containerVariable, VariableInfo instanceVariable);



        protected MapperBase(BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) => BindingAttr = bindingAttr;
    }
}
