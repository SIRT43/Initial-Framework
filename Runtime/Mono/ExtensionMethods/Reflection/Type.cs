using InitialFramework.Reflection;
using System;
using System.Reflection;

namespace InitialFramework.ExtensionMethods
{
    public static class TypeMethods
    {
        /// <summary>
        /// 获取唯一名称，本方法将返回 "Assembly: {type.AssemblyQualifiedName} FullName: {type.FullName}" 格式的字符串。
        /// </summary>
        public static string GetUniqueName(this Type type) =>
            TypeUtilis.GetUniqueName(type);

        /// <summary>
        /// 获取字段，但该字段的值类型应该是指定的类型。
        /// </summary>
        public static FieldInfo GetField(this Type type, string name, Type fieldType, BindingFlags bindingAttr = BindingFlags.Default) =>
            TypeUtilis.GetField(type, name, fieldType, bindingAttr);

        /// <summary>
        /// 获取属性，但该属性的值类型应该是指定的类型。
        /// </summary>
        public static PropertyInfo GetProperty(this Type type, string name, Type propertyType, BindingFlags bindingAttr = BindingFlags.Default) =>
            TypeUtilis.GetProperty(type, name, propertyType, bindingAttr);

        /// <summary>
        /// 获取变量，变量支持表达字段或属性。
        /// </summary>
        public static VariableInfo GetVariable(this Type type, string name, BindingFlags bindingAttr = BindingFlags.Default) =>
            TypeUtilis.GetVariable(type, name, bindingAttr);

        /// <summary>
        /// 获取变量，但该变量的值类型应该是指定的类型。
        /// </summary>
        public static VariableInfo GetVariable(this Type type, string name, Type valueType, BindingFlags bindingAttr = BindingFlags.Default) =>
            TypeUtilis.GetVariable(type, name, valueType, bindingAttr);

        /// <summary>
        /// 获取所有变量。
        /// <br>变量是对字段与属性的封装，它支持表达字段，也支持表达属性。</br>
        /// 
        /// <para>此方法将返回 <see cref="Type.GetFields"/> 与 <see cref="Type.GetProperties"/> 的合并数组。</para>
        /// </summary>
        public static VariableInfo[] GetVariables(this Type type, BindingFlags bindingAttr = BindingFlags.Default) =>
            TypeUtilis.GetVariables(type, bindingAttr);
    }
}
