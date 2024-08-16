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
        public object Container { get; }
        public object Instance { get; }

        /// <summary>
        /// 应该以怎样的特征查询成员。
        /// </summary>
        public BindingFlags BindingAttr { get; set; }

        /// <summary>
        /// 获取结果。(如果 instance 是值类型)
        /// </summary>
        public object Result { get; }

        /// <summary>
        /// 遍历并验证容器成员。
        /// </summary>
        public void Map(object a, object b);
    }

    /// <summary>  
    /// MapperBase 是所有映射器类的基类，提供了映射操作的基本框架和属性。  
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
        /// 当映射器正在遍历容器成员时调用。
        /// 
        /// <para>本方法用于自定义映射行为。</para>
        /// </summary>
        protected abstract void Map(VariableInfo containerVariable, VariableInfo instanceVariable);
    }
}
