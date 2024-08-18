using FTGAMEStudio.InitialFramework.Reflection;
using System;
using System.Reflection;

namespace FTGAMEStudio.InitialFramework.ExtensionMethods
{
    public static class TypeMethods
    {
        /// <summary>
        /// ��ȡΨһ���ƣ������������� "Assembly: {type.AssemblyQualifiedName} FullName: {type.FullName}" ��ʽ���ַ�����
        /// </summary>
        public static string GetUniqueName(this Type type) => $"Assembly: {type.AssemblyQualifiedName} FullName: {type.FullName}";

        /// <summary>
        /// ��ȡ�ֶΣ������ֶε�ֵ����Ӧ���� fieldType ָ���ġ�
        /// </summary>
        public static FieldInfo GetField(this Type type, string name, Type fieldType, BindingFlags bindingAttr = BindingFlags.Default)
        {
            FieldInfo fieldInfo = type.GetField(name, bindingAttr);

            if (fieldInfo == null) return null;
            if (fieldInfo.FieldType != fieldType) return null;

            return fieldInfo;
        }

        /// <summary>
        /// ��ȡ���ԣ��������Ե�ֵ����Ӧ���� fieldType ָ���ġ�
        /// </summary>
        public static PropertyInfo GetProperty(this Type type, string name, Type propertyType, BindingFlags bindingAttr = BindingFlags.Default)
        {
            PropertyInfo propertyInfo = type.GetProperty(name, bindingAttr);

            if (propertyInfo == null) return null;
            if (propertyInfo.PropertyType != propertyType) return null;

            return propertyInfo;
        }



        public static VariableInfo GetVariable(this Type type, string name, BindingFlags bindingAttr = BindingFlags.Default)
        {
            if (type.GetField(name, bindingAttr) is FieldInfo fieldInfo) return fieldInfo;
            else if (type.GetProperty(name, bindingAttr) is PropertyInfo propertyInfo) return propertyInfo;

            return null;
        }

        /// <summary>
        /// ��ȡ���������ñ�����ֵ����Ӧ���� fieldType ָ���ġ�
        /// </summary>
        public static VariableInfo GetVariable(this Type type, string name, Type valueType, BindingFlags bindingAttr = BindingFlags.Default)
        {
            VariableInfo variableInfo = type.GetVariable(name, bindingAttr);

            if (variableInfo == null) return null;
            if (variableInfo.ValueType != valueType) return null;

            return variableInfo;
        }

        public static VariableInfo[] GetVariables(this Type type, BindingFlags bindingAttr = BindingFlags.Default)
        {
            FieldInfo[] fieldInfos = type.GetFields(bindingAttr);
            PropertyInfo[] propertyInfos = type.GetProperties(bindingAttr);

            if (fieldInfos.Length + propertyInfos.Length == 0) return new VariableInfo[0];

            VariableInfo[] variableInfos = new VariableInfo[fieldInfos.Length + propertyInfos.Length];

            for (int index = 0; index < fieldInfos.Length; index++) variableInfos[index] = fieldInfos[index];
            for (int index = 0; index < propertyInfos.Length; index++) variableInfos[index + fieldInfos.Length] = fieldInfos[index];

            return variableInfos;
        }
    }
}
