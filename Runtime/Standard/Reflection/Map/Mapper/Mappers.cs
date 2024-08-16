using FTGAMEStudio.InitialFramework.ExtensionMethods;
using System.Reflection;

namespace FTGAMEStudio.InitialFramework.Reflection
{
    /// <summary>  
    /// VariableMapper ��̳��� MapperBase��ʵ��������ӳ���߼�������������������Ի��ֶ�ֵӳ�䵽ʵ������Ķ�Ӧ���Ի��ֶΡ�  
    /// </summary>
    public class VariableMapper : MapperBase
    {
        public VariableMapper(BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : base(bindingAttr) { }

        protected override void Map(VariableInfo containerVariable, VariableInfo instanceVariable) =>
            instanceVariable.SetValue(Instance, Container, containerVariable);
    }

    /// <summary>  
    /// ReverseVariableMapper ��̳��� MapperBase��ʵ���˷���ӳ���߼�������ʵ����������Ի��ֶ�ֵӳ�����������Ķ�Ӧ���Ի��ֶΡ�  
    /// </summary>
    public class ReverseVariableMapper : MapperBase
    {
        public ReverseVariableMapper(BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : base(bindingAttr) { }

        protected override void Map(VariableInfo containerVariable, VariableInfo instanceVariable) =>
            containerVariable.SetValue(Container, Instance, instanceVariable);
    }
}
