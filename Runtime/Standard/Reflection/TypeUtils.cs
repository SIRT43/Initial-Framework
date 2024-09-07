using FTGAMEStudio.InitialFramework.Classifying;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace FTGAMEStudio.InitialFramework.Reflection
{
    public static class TypeUtilis
    {
        /// <summary>
        /// ��ȡ�ֶΣ������ֶε�ֵ����Ӧ����ָ�������͡�
        /// </summary>
        public static FieldInfo GetField(Type type, string name, Type fieldType, BindingFlags bindingAttr = BindingFlags.Default)
        {
            FieldInfo fieldInfo = type.GetField(name, bindingAttr);

            if (fieldInfo == null) return null;
            if (fieldInfo.FieldType != fieldType) return null;

            return fieldInfo;
        }

        /// <summary>
        /// ��ȡ���ԣ��������Ե�ֵ����Ӧ����ָ�������͡�
        /// </summary>
        public static PropertyInfo GetProperty(Type type, string name, Type propertyType, BindingFlags bindingAttr = BindingFlags.Default)
        {
            PropertyInfo propertyInfo = type.GetProperty(name, bindingAttr);

            if (propertyInfo == null) return null;
            if (propertyInfo.PropertyType != propertyType) return null;

            return propertyInfo;
        }

        /// <summary>
        /// ��ȡ������
        /// <br>�����Ƕ��ֶ������Եķ�װ����֧�ֱ���ֶΣ�Ҳ֧�ֱ�����ԡ�</br>
        /// </summary>
        public static VariableInfo GetVariable(Type type, string name, BindingFlags bindingAttr = BindingFlags.Default)
        {
            if (type.GetField(name, bindingAttr) is FieldInfo fieldInfo) return fieldInfo;
            else if (type.GetProperty(name, bindingAttr) is PropertyInfo propertyInfo) return propertyInfo;

            return null;
        }

        /// <summary>
        /// ��ȡ���������ñ�����ֵ����Ӧ����ָ�������͡�
        /// </summary>
        public static VariableInfo GetVariable(Type type, string name, Type valueType, BindingFlags bindingAttr = BindingFlags.Default)
        {
            VariableInfo variableInfo = GetVariable(type, name, bindingAttr);

            if (variableInfo == null) return null;
            if (variableInfo.ValueType != valueType) return null;

            return variableInfo;
        }

        /// <summary>
        /// ��ȡ���б�����
        /// <br>�����Ƕ��ֶ������Եķ�װ����֧�ֱ���ֶΣ�Ҳ֧�ֱ�����ԡ�</br>
        /// 
        /// <para>�˷��������� <see cref="Type.GetFields"/> �� <see cref="Type.GetProperties"/> �ĺϲ����顣</para>
        /// </summary>
        public static VariableInfo[] GetVariables(Type type, BindingFlags bindingAttr = BindingFlags.Default)
        {
            FieldInfo[] fieldInfos = type.GetFields(bindingAttr);
            PropertyInfo[] propertyInfos = type.GetProperties(bindingAttr);

            List<VariableInfo> variableInfos = new();

            foreach (FieldInfo fieldInfo in fieldInfos) variableInfos.Add(fieldInfo);
            foreach (PropertyInfo propertyInfo in propertyInfos) variableInfos.Add(propertyInfo);

            return variableInfos.ToArray();
        }



        /// <summary>
        /// ��ȡΨһ���ƣ������������� "Assembly: {type.AssemblyQualifiedName} FullName: {type.FullName}" ��ʽ���ַ�����
        /// </summary>
        public static string GetUniqueName(Type type) => $"Assembly: {type.AssemblyQualifiedName} FullName: {type.FullName}";

        /// <summary>
        /// �������ఴ <see cref="GetUniqueName(Type)"/> ���ࡣ
        /// </summary>
        public static Dictionary<string, List<T>> ClassifyWithUniqueName<T>(T[] values)
        {
            FilterableClassifier<string, T> classifier = new((T value) => true, (T value) => GetUniqueName(value.GetType()));

            return classifier.Classify(values);
        }
    }
}
