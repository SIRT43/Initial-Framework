using FTGAMEStudio.InitialFramework.Reflection;
using System;
using System.Reflection;

namespace FTGAMEStudio.InitialFramework.ExtensionMethods
{
    public static class TypeMethods
    {
        /// <summary>
        /// 获取唯一名称，本方法将返回 "Assembly: {type.AssemblyQualifiedName} FullName: {type.FullName}" 格式的字符串。
        /// </summary>
        public static string GetUniqueName(this Type type) => $"Assembly: {type.AssemblyQualifiedName} FullName: {type.FullName}";

        /// <summary>
        /// 获取字段，但该字段的值类型应该是 fieldType 指定的。
        /// </summary>
        public static FieldInfo GetField(this Type type, string name, Type fieldType, BindingFlags bindingAttr = BindingFlags.Default)
        {
            FieldInfo fieldInfo = type.GetField(name, bindingAttr);

            if (fieldInfo == null) return null;
            if (fieldInfo.FieldType != fieldType) return null;

            return fieldInfo;
        }

        /// <summary>
        /// 获取属性，但该属性的值类型应该是 fieldType 指定的。
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
        /// 获取变量，但该变量的值类型应该是 fieldType 指定的。
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
