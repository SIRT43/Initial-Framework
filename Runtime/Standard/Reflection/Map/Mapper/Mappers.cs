using System.Reflection;

namespace InitialFramework.Reflection
{
    /// <summary>  
    /// �̳��� <see cref="MapperBase"/>��ʵ��������ӳ���߼�������������������Ի��ֶ�ֵӳ�䵽ʵ������Ķ�Ӧ���Ի��ֶΡ�  
    /// </summary>
    public class VariableMapper : MapperBase
    {
        public VariableMapper(BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : base(bindingAttr) { }

        protected override void TryMap(object container, object instance, VariableInfo containerVariable, VariableInfo instanceVariable) =>
            instanceVariable.SetValue(instance, containerVariable.GetValue(container));
    }

    /// <summary>  
    /// �̳��� <see cref="MapperBase"/>��ʵ���˷���ӳ���߼�������ʵ����������Ի��ֶ�ֵӳ�����������Ķ�Ӧ���Ի��ֶΡ�  
    /// </summary>
    public class ReverseVariableMapper : MapperBase
    {
        public ReverseVariableMapper(BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : base(bindingAttr) { }

        protected override void TryMap(object container, object instance, VariableInfo containerVariable, VariableInfo instanceVariable) =>
            containerVariable.SetValue(container, instanceVariable.GetValue(instance));
    }
}
