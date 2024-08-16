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
        public object Container { get; }
        public object Instance { get; }

        /// <summary>
        /// Ӧ����������������ѯ��Ա��
        /// </summary>
        public BindingFlags BindingAttr { get; set; }

        /// <summary>
        /// ��ȡ�����(��� instance ��ֵ����)
        /// </summary>
        public object Result { get; }

        /// <summary>
        /// ��������֤������Ա��
        /// </summary>
        public void Map(object a, object b);
    }

    /// <summary>  
    /// MapperBase ������ӳ������Ļ��࣬�ṩ��ӳ������Ļ�����ܺ����ԡ�  
    /// </summary>
    public abstract class MapperBase : IMapper
    {
        public virtual object Container { get; protected set; }
        public virtual object Instance { get; protected set; }

        public virtual BindingFlags BindingAttr { get; set; }
        public virtual object Result => Instance;

        protected MapperBase(BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) => BindingAttr = bindingAttr;

        public virtual void Map(object container, object instance)
        {
            Container = container;
            Instance = instance;

            Type containerType = container.GetType();
            Type instanceType = instance.GetType();

            if (!Mapping.VerifyMapping(containerType, instanceType)) return;

            foreach (VariableInfo containerVariable in containerType.GetVariables(BindingAttr))
            {
                if (!Mapping.IsMappable(containerVariable)) continue;

                VariableInfo instanceVariable =
                    instanceType.GetVariable(containerVariable.Name, containerVariable.ValueType, BindingAttr)
                    ??
                    throw new MissingFieldException($"The Type {containerType} member '{containerVariable.Name}' could not be found in the Type {instanceType}.");

                if (!Mapping.IsMappable(instanceType)) continue;

                Map(containerVariable, instanceVariable);
            }
        }

        /// <summary>
        /// ��ӳ�������ڱ���������Աʱ���á�
        /// 
        /// <para>�����������Զ���ӳ����Ϊ��</para>
        /// </summary>
        protected abstract void Map(VariableInfo containerVariable, VariableInfo instanceVariable);
    }
}
