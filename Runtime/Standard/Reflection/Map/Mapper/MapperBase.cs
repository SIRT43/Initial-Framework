using InitialFramework.Traverse;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace InitialFramework.Reflection
{
    /// <summary>  
    /// 本接口定义了映射器的基本结构和功能，用于映射属性或字段。  
    /// </summary>
    public interface IMapper
    {
        bool IsMappable(MemberInfo memberInfo);
        bool IsMappable(VariableInfo variableInfo);

        /// <summary>
        /// 验证容器的目标类型是否是指定类型。
        /// </summary>
        bool VerifyMapTarget(Type container, Type instance);
        /// <summary>  
        /// 验证容器是否可以与指定类型进行映射。
        /// </summary>
        bool VerifyMapping(Type container, Type instance);

        bool Map(object container, object instance, out object result);
    }

    public struct MappingPair
    {
        public object container;
        public object instance;

        public MappingPair(object container, object instance)
        {
            this.container = container;
            this.instance = instance;
        }
    }

    /// <summary>  
    /// <see cref="MapperBase"/> 是所有映射器类的基类，提供了映射操作的基本框架和属性。  
    /// </summary>
    public abstract class MapperBase : Traverser<KeyValuePair<VariableInfo, VariableInfo>, MappingPair>, IMapper
    {
        public virtual BindingFlags BindingAttr { get; }

        protected MapperBase(BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : base(FlowControl.Continue)
            => BindingAttr = bindingAttr;


        public virtual bool IsMappable(MemberInfo memberInfo) => Mapping.IsMappable(memberInfo);
        public virtual bool IsMappable(VariableInfo variableInfo) => Mapping.IsMappable(variableInfo);

        public virtual bool VerifyMapTarget(Type container, Type instance) => Mapping.VerifyMapTarget(container, instance);
        public virtual bool VerifyMapping(Type container, Type instance) => Mapping.VerifyMapping(container, instance);


        public virtual bool Map(object container, object instance, out object result)
        {
            result = null;

            Type containerType = container.GetType();
            Type instanceType = instance.GetType();

            if (!VerifyMapping(containerType, instanceType)) return false;

            Dictionary<VariableInfo, VariableInfo> mapPairs = new();

            foreach (VariableInfo containerVariable in containerType.GetVariables(BindingAttr))
                mapPairs.Add(containerVariable, instanceType.GetVariable(containerVariable.Name, containerVariable.ValueType, BindingAttr));

            Traverse(mapPairs, new(container, instance));

            result = instance;
            return true;
        }

        public override bool IsCanonical(KeyValuePair<VariableInfo, VariableInfo> value, MappingPair context) =>
            value.Value != null ? IsMappable(value.Key) && IsMappable(value.Value) :
            throw new MissingFieldException($"The member '{value.Key.Name}' could not be found in the instance.");

        protected override void OnTraverse(KeyValuePair<VariableInfo, VariableInfo> value, MappingPair context) =>
            TryMap(context.container, context.instance, value.Value, value.Key);

        protected abstract void TryMap(object container, object instance, VariableInfo containerVariable, VariableInfo instanceVariable);
    }
}
