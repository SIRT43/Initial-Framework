using InitialFramework.ExtensionMethods;
using InitialFramework.Traverse;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace InitialFramework.Reflection
{
    /// <summary>  
    /// ���ӿڶ�����ӳ�����Ļ����ṹ�͹��ܣ�����ӳ�����Ի��ֶΡ�  
    /// </summary>
    public interface IMapper
    {
        bool IsMappable(MemberInfo memberInfo);
        bool IsMappable(VariableInfo variableInfo);

        /// <summary>
        /// ��֤������Ŀ�������Ƿ���ָ�����͡�
        /// </summary>
        bool VerifyMapTarget(Type container, Type instance);
        /// <summary>  
        /// ��֤�����Ƿ������ָ�����ͽ���ӳ�䡣
        /// </summary>
        bool VerifyMapping(Type container, Type instance);

        bool Map(object container, object instance);
    }

    /// <summary>  
    /// <see cref="MapperBase"/> ������ӳ������Ļ��࣬�ṩ��ӳ������Ļ�����ܺ����ԡ�  
    /// </summary>
    public abstract class MapperBase : Traverser<KeyValuePair<VariableInfo, VariableInfo>>, IMapper
    {
        public virtual BindingFlags BindingAttr { get; }

        private object container;
        private object instance;



        protected MapperBase(BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : base(FlowControl.Continue)
            => BindingAttr = bindingAttr;



        public virtual bool IsMappable(MemberInfo memberInfo) => Mapping.IsMappable(memberInfo);
        public virtual bool IsMappable(VariableInfo variableInfo) => Mapping.IsMappable(variableInfo);

        public virtual bool VerifyMapTarget(Type container, Type instance) => Mapping.VerifyMapTarget(container, instance);
        public virtual bool VerifyMapping(Type container, Type instance) => Mapping.VerifyMapping(container, instance);


        public virtual bool Map(object container, object instance)
        {
            this.container = container;
            this.instance = instance;

            Type containerType = container.GetType();
            Type instanceType = instance.GetType();

            if (!VerifyMapping(containerType, instanceType)) return false;

            Dictionary<VariableInfo, VariableInfo> mapPairs = new();

            foreach (VariableInfo containerVariable in containerType.GetVariables(BindingAttr))
                mapPairs.Add(containerVariable, instanceType.GetVariable(containerVariable.Name, containerVariable.ValueType, BindingAttr));

            Traverse(mapPairs);

            this.container = null;
            this.instance = null;

            return true;
        }

        public override bool IsCanonical(KeyValuePair<VariableInfo, VariableInfo> value) =>
            value.Value != null ? IsMappable(value.Key) && IsMappable(value.Value) :
            throw new MissingFieldException($"The member '{value.Key.Name}' could not be found in the instance.");

        protected override void OnTraverse(KeyValuePair<VariableInfo, VariableInfo> value) => TryMap(container, instance, value.Value, value.Key);

        protected abstract void TryMap(object container, object instance, VariableInfo containerVariable, VariableInfo instanceVariable);
    }
}
