using System.Reflection;

namespace InitialFramework.Reflection
{
    /// <summary>  
    /// 继承自 <see cref="VariableMapperBase"/>，实现了正向映射逻辑，即将容器对象的属性或字段值映射到实例对象的对应属性或字段。  
    /// </summary>
    public class VariableMapper : VariableMapperBase
    {
        public VariableMapper(BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : base(bindingAttr) { }

        protected override void TryMap(object container, object instance, VariableInfo containerVariable, VariableInfo instanceVariable) =>
            instanceVariable.SetValue(instance, containerVariable.GetValue(container));



        /// <summary>  
        /// 将容器的变量映射到目标对象。  
        /// </summary>
        public static void MapVariables(object container, object instance, out object result)
        {
            VariableMapper mapper = new();
            mapper.Map(container, instance, out object resu);
            result = resu;
        }
    }

    /// <summary>  
    /// 继承自 <see cref="VariableMapperBase"/>，实现了反向映射逻辑，即将实例对象的属性或字段值映射回容器对象的对应属性或字段。  
    /// </summary>
    public class ReverseVariableMapper : VariableMapperBase
    {
        public ReverseVariableMapper(BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : base(bindingAttr) { }

        protected override void TryMap(object container, object instance, VariableInfo containerVariable, VariableInfo instanceVariable) =>
            containerVariable.SetValue(container, instanceVariable.GetValue(instance));



        /// <summary>  
        /// 将目标对象映射到容器的变量。  
        /// </summary>
        public static void ReverseMapVariables(object container, object instance, out object result)
        {
            ReverseVariableMapper mapper = new();

            mapper.Map(container, instance, out object resu);
            result = resu;
        }

    }
}
