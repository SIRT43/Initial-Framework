using FTGAMEStudio.InitialFramework.Classifying;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace FTGAMEStudio.InitialFramework.Reflection
{
    public static class TypeUtilis
    {
        /// <summary>
        /// 获取字段，但该字段的值类型应该是指定的类型。
        /// </summary>
        public static FieldInfo GetField(Type type, string name, Type fieldType, BindingFlags bindingAttr = BindingFlags.Default)
        {
            FieldInfo fieldInfo = type.GetField(name, bindingAttr);

            if (fieldInfo == null) return null;
            if (fieldInfo.FieldType != fieldType) return null;

            return fieldInfo;
        }

        /// <summary>
        /// 获取属性，但该属性的值类型应该是指定的类型。
        /// </summary>
        public static PropertyInfo GetProperty(Type type, string name, Type propertyType, BindingFlags bindingAttr = BindingFlags.Default)
        {
            PropertyInfo propertyInfo = type.GetProperty(name, bindingAttr);

            if (propertyInfo == null) return null;
            if (propertyInfo.PropertyType != propertyType) return null;

            return propertyInfo;
        }

        /// <summary>
        /// 获取变量。
        /// <br>变量是对字段与属性的封装，它支持表达字段，也支持表达属性。</br>
        /// </summary>
        public static VariableInfo GetVariable(Type type, string name, BindingFlags bindingAttr = BindingFlags.Default)
        {
            if (type.GetField(name, bindingAttr) is FieldInfo fieldInfo) return fieldInfo;
            else if (type.GetProperty(name, bindingAttr) is PropertyInfo propertyInfo) return propertyInfo;

            return null;
        }

        /// <summary>
        /// 获取变量，但该变量的值类型应该是指定的类型。
        /// </summary>
        public static VariableInfo GetVariable(Type type, string name, Type valueType, BindingFlags bindingAttr = BindingFlags.Default)
        {
            VariableInfo variableInfo = GetVariable(type, name, bindingAttr);

            if (variableInfo == null) return null;
            if (variableInfo.ValueType != valueType) return null;

            return variableInfo;
        }

        /// <summary>
        /// 获取所有变量。
        /// <br>变量是对字段与属性的封装，它支持表达字段，也支持表达属性。</br>
        /// 
        /// <para>此方法将返回 <see cref="Type.GetFields"/> 与 <see cref="Type.GetProperties"/> 的合并数组。</para>
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
        /// 获取唯一名称，本方法将返回 "Assembly: {type.AssemblyQualifiedName} FullName: {type.FullName}" 格式的字符串。
        /// </summary>
        public static string GetUniqueName(Type type) => $"Assembly: {type.AssemblyQualifiedName} FullName: {type.FullName}";

        /// <summary>
        /// 将派生类按 <see cref="GetUniqueName(Type)"/> 分类。
        /// </summary>
        public static Dictionary<string, List<T>> ClassifyWithUniqueName<T>(T[] values)
        {
            FilterableClassifier<string, T> classifier = new((T value) => true, (T value) => GetUniqueName(value.GetType()));

            return classifier.Classify(values);
        }
    }
}
