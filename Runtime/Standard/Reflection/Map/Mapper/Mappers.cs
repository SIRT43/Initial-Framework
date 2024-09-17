using System.Reflection;

namespace InitialFramework.Reflection
{
    /// <summary>  
    /// 继承自 <see cref="MapperBase"/>，实现了正向映射逻辑，即将容器对象的属性或字段值映射到实例对象的对应属性或字段。  
    /// </summary>
    public class VariableMapper : MapperBase
    {
        public VariableMapper(BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : base(bindingAttr) { }

        protected override void TryMap(object container, object instance, VariableInfo containerVariable, VariableInfo instanceVariable) =>
            instanceVariable.SetValue(instance, containerVariable.GetValue(container));
    }

    /// <summary>  
    /// 继承自 <see cref="MapperBase"/>，实现了反向映射逻辑，即将实例对象的属性或字段值映射回容器对象的对应属性或字段。  
    /// </summary>
    public class ReverseVariableMapper : MapperBase
    {
        public ReverseVariableMapper(BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : base(bindingAttr) { }

        protected override void TryMap(object container, object instance, VariableInfo containerVariable, VariableInfo instanceVariable) =>
            containerVariable.SetValue(container, instanceVariable.GetValue(instance));
    }
}
