using FTGAMEStudio.InitialFramework.ExtensionMethods;
using System.Reflection;

namespace FTGAMEStudio.InitialFramework.Reflection
{
    /// <summary>  
    /// VariableMapper 类继承自 MapperBase，实现了正向映射逻辑，即将容器对象的属性或字段值映射到实例对象的对应属性或字段。  
    /// </summary>
    public class VariableMapper : MapperBase
    {
        public VariableMapper(BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : base(bindingAttr) { }

        protected override void Map(VariableInfo containerVariable, VariableInfo instanceVariable) =>
            instanceVariable.SetValue(Instance, Container, containerVariable);
    }

    /// <summary>  
    /// ReverseVariableMapper 类继承自 MapperBase，实现了反向映射逻辑，即将实例对象的属性或字段值映射回容器对象的对应属性或字段。  
    /// </summary>
    public class ReverseVariableMapper : MapperBase
    {
        public ReverseVariableMapper(BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : base(bindingAttr) { }

        protected override void Map(VariableInfo containerVariable, VariableInfo instanceVariable) =>
            containerVariable.SetValue(Container, Instance, instanceVariable);
    }
}
