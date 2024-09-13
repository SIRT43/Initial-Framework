using InitialFramework.Reflection;
using System;
using System.Reflection;

namespace InitialFramework.ExtensionMethods
{
    public static class TypeMethods
    {
        /// <summary>
        /// ��ȡΨһ���ƣ������������� "Assembly: {type.AssemblyQualifiedName} FullName: {type.FullName}" ��ʽ���ַ�����
        /// </summary>
        public static string GetUniqueName(this Type type) =>
            TypeUtilis.GetUniqueName(type);

        /// <summary>
        /// ��ȡ�ֶΣ������ֶε�ֵ����Ӧ����ָ�������͡�
        /// </summary>
        public static FieldInfo GetField(this Type type, string name, Type fieldType, BindingFlags bindingAttr = BindingFlags.Default) =>
            TypeUtilis.GetField(type, name, fieldType, bindingAttr);

        /// <summary>
        /// ��ȡ���ԣ��������Ե�ֵ����Ӧ����ָ�������͡�
        /// </summary>
        public static PropertyInfo GetProperty(this Type type, string name, Type propertyType, BindingFlags bindingAttr = BindingFlags.Default) =>
            TypeUtilis.GetProperty(type, name, propertyType, bindingAttr);

        /// <summary>
        /// ��ȡ����������֧�ֱ���ֶλ����ԡ�
        /// </summary>
        public static VariableInfo GetVariable(this Type type, string name, BindingFlags bindingAttr = BindingFlags.Default) =>
            TypeUtilis.GetVariable(type, name, bindingAttr);

        /// <summary>
        /// ��ȡ���������ñ�����ֵ����Ӧ����ָ�������͡�
        /// </summary>
        public static VariableInfo GetVariable(this Type type, string name, Type valueType, BindingFlags bindingAttr = BindingFlags.Default) =>
            TypeUtilis.GetVariable(type, name, valueType, bindingAttr);

        /// <summary>
        /// ��ȡ���б�����
        /// <br>�����Ƕ��ֶ������Եķ�װ����֧�ֱ���ֶΣ�Ҳ֧�ֱ�����ԡ�</br>
        /// 
        /// <para>�˷��������� <see cref="Type.GetFields"/> �� <see cref="Type.GetProperties"/> �ĺϲ����顣</para>
        /// </summary>
        public static VariableInfo[] GetVariables(this Type type, BindingFlags bindingAttr = BindingFlags.Default) =>
            TypeUtilis.GetVariables(type, bindingAttr);
    }
}
