using FTGAMEStudio.InitialFramework.ExtensionMethods;
using System;
using System.Reflection;

namespace FTGAMEStudio.InitialFramework.Reflection
{
    /// <summary>  
    /// 本接口定义了映射器的基本结构和功能，用于映射属性或字段。  
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// 应该以怎样的特征查询成员。
        /// </summary>
        BindingFlags BindingAttr { get; set; }

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

        bool Map<T>(object container, ref T instance);
    }

    /// <summary>  
    /// <see cref="MapperBase"/> 是所有映射器类的基类，提供了映射操作的基本框架和属性。  
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
