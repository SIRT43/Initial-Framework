using System;
using System.Collections.Generic;
using System.Reflection;

namespace FTGAMEStudio.InitialFramework.Reflection
{
    public enum VariableType
    {
        Field,
        Property
    }

    public interface IVariableInfo
    {
        public VariableType VariableType { get; }

        public MemberInfo GetOriginal();
    }

    /// <summary>
    /// 变量信息。
    /// <br>它可以表达字段，也可以表达属性。</br>
    /// </summary>
    public class VariableInfo : MemberInfo, IVariableInfo
    {
        public static implicit operator VariableInfo(FieldInfo fieldInfo) => new(fieldInfo);
        public static implicit operator VariableInfo(PropertyInfo propertyInfo) => new(propertyInfo);

        protected readonly FieldInfo field;
        protected readonly PropertyInfo property;

        protected readonly VariableType variableType;
        public VariableType VariableType => variableType;

        public override Type DeclaringType =>
            variableType == VariableType.Field ? field.DeclaringType : property.DeclaringType;

        public override MemberTypes MemberType =>
            variableType == VariableType.Field ? field.MemberType : property.MemberType;

        public override string Name =>
            variableType == VariableType.Field ? field.Name : property.Name;

        public override Type ReflectedType =>
            variableType == VariableType.Field ? field.ReflectedType : property.ReflectedType;

        public override int MetadataToken =>
            variableType == VariableType.Field ? field.MetadataToken : property.MetadataToken;

        public override Module Module =>
            variableType == VariableType.Field ? field.Module : property.Module;

        public Type ValueType =>
            variableType == VariableType.Field ? field.FieldType : property.PropertyType;

        public override IEnumerable<CustomAttributeData> CustomAttributes =>
            variableType == VariableType.Field ? field.CustomAttributes : property.CustomAttributes;

        public VariableInfo(FieldInfo fieldInfo)
        {
            field = fieldInfo;
            variableType = VariableType.Field;
        }

        public VariableInfo(PropertyInfo propertyInfo)
        {
            property = propertyInfo;
            variableType = VariableType.Property;
        }

        public override object[] GetCustomAttributes(bool inherit) =>
            variableType == VariableType.Field ? field.GetCustomAttributes(inherit) : property.GetCustomAttributes(inherit);

        public override object[] GetCustomAttributes(Type attributeType, bool inherit) =>
            variableType == VariableType.Field ? field.GetCustomAttributes(attributeType, inherit) : property.GetCustomAttributes(attributeType, inherit);

        public override bool IsDefined(Type attributeType, bool inherit) =>
            variableType == VariableType.Field ? field.IsDefined(attributeType, inherit) : property.IsDefined(attributeType, inherit);

        public override IList<CustomAttributeData> GetCustomAttributesData() =>
            variableType == VariableType.Field ? field.GetCustomAttributesData() : property.GetCustomAttributesData();

        public override bool HasSameMetadataDefinitionAs(MemberInfo other) =>
            variableType == VariableType.Field ? field.HasSameMetadataDefinitionAs(other) : property.HasSameMetadataDefinitionAs(other);

        public object GetValue(object obj) =>
            variableType == VariableType.Field ? field.GetValue(obj) : property.GetValue(obj);

        public void SetValue(object obj, object value)
        {
            if (variableType == VariableType.Field) field.SetValue(obj, value);
            else property.SetValue(obj, value);
        }

        public MemberInfo GetOriginal() =>
            variableType == VariableType.Field ? field : property;
    }
}
