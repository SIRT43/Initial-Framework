using System.Reflection;

namespace InitialFramework.Reflection
{
    /// <summary>  
    /// �̳��� <see cref="VariableMapperBase"/>��ʵ��������ӳ���߼�������������������Ի��ֶ�ֵӳ�䵽ʵ������Ķ�Ӧ���Ի��ֶΡ�  
    /// </summary>
    public class VariableMapper : VariableMapperBase
    {
        public VariableMapper(BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : base(bindingAttr) { }

        protected override void TryMap(object container, object instance, VariableInfo containerVariable, VariableInfo instanceVariable) =>
            instanceVariable.SetValue(instance, containerVariable.GetValue(container));



        /// <summary>  
        /// �������ı���ӳ�䵽Ŀ�����  
        /// </summary>
        public static void MapVariables(object container, object instance, out object result)
        {
            VariableMapper mapper = new();
            mapper.Map(container, instance, out object resu);
            result = resu;
        }
    }

    /// <summary>  
    /// �̳��� <see cref="VariableMapperBase"/>��ʵ���˷���ӳ���߼�������ʵ����������Ի��ֶ�ֵӳ�����������Ķ�Ӧ���Ի��ֶΡ�  
    /// </summary>
    public class ReverseVariableMapper : VariableMapperBase
    {
        public ReverseVariableMapper(BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : base(bindingAttr) { }

        protected override void TryMap(object container, object instance, VariableInfo containerVariable, VariableInfo instanceVariable) =>
            containerVariable.SetValue(container, instanceVariable.GetValue(instance));



        /// <summary>  
        /// ��Ŀ�����ӳ�䵽�����ı�����  
        /// </summary>
        public static void ReverseMapVariables(object container, object instance, out object result)
        {
            ReverseVariableMapper mapper = new();

            mapper.Map(container, instance, out object resu);
            result = resu;
        }

    }
}
