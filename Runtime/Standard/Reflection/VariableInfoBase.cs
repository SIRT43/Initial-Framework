using System;
using System.Collections.Generic;
using System.Reflection;

namespace FTGAMEStudio.InitialFramework.Reflection
{
    public interface IVariableInfo
    {
        public VariableType VariableType { get; }
        public MemberInfo Original { get; }

        public Type ValueType { get; }

        public object GetValue(object obj);
        public void SetValue(object obj, object value);
    }

    public abstract class VariableInfoBase : MemberInfo, IVariableInfo
    {
        protected abstract MemberInfo Target { get; }

        private FieldInfo fieldInfo;
        private FieldInfo FieldInfo
        {
            get
            {
                fieldInfo ??= Target as FieldInfo;
                return fieldInfo;
            }
        }

        private PropertyInfo propertyInfo;
        private PropertyInfo PropertyInfo
        {
            get
            {
                propertyInfo ??= Target as PropertyInfo;
                return propertyInfo;
            }
        }


        public abstract VariableType VariableType { get; protected set; }
        public virtual MemberInfo Original => Target;

        public virtual Type ValueType =>
            VariableType == VariableType.Field ? FieldInfo.FieldType : PropertyInfo.PropertyType;


        public virtual object GetValue(object obj) =>
            VariableType == VariableType.Field ? FieldInfo.GetValue(obj) : PropertyInfo.GetValue(obj);

        public virtual void SetValue(object obj, object value)
        {
            if (VariableType == VariableType.Field) FieldInfo.SetValue(obj, value);
            else PropertyInfo.SetValue(obj, value);
        }



        public override Type DeclaringType =>
            VariableType == VariableType.Field ? FieldInfo.DeclaringType : PropertyInfo.DeclaringType;

        public override MemberTypes MemberType =>
            VariableType == VariableType.Field ? FieldInfo.MemberType : PropertyInfo.MemberType;

        public override string Name =>
            VariableType == VariableType.Field ? FieldInfo.Name : PropertyInfo.Name;

        public override Type ReflectedType =>
            VariableType == VariableType.Field ? FieldInfo.ReflectedType : PropertyInfo.ReflectedType;

        public override int MetadataToken =>
            VariableType == VariableType.Field ? FieldInfo.MetadataToken : PropertyInfo.MetadataToken;

        public override Module Module =>
            VariableType == VariableType.Field ? FieldInfo.Module : PropertyInfo.Module;

        public override IEnumerable<CustomAttributeData> CustomAttributes =>
            VariableType == VariableType.Field ? FieldInfo.CustomAttributes : PropertyInfo.CustomAttributes;

        public override object[] GetCustomAttributes(bool inherit) =>
            VariableType == VariableType.Field ? FieldInfo.GetCustomAttributes(inherit) : PropertyInfo.GetCustomAttributes(inherit);

        public override object[] GetCustomAttributes(Type attributeType, bool inherit) =>
            VariableType == VariableType.Field ? FieldInfo.GetCustomAttributes(attributeType, inherit) : PropertyInfo.GetCustomAttributes(attributeType, inherit);

        public override bool IsDefined(Type attributeType, bool inherit) =>
            VariableType == VariableType.Field ? FieldInfo.IsDefined(attributeType, inherit) : PropertyInfo.IsDefined(attributeType, inherit);

        public override IList<CustomAttributeData> GetCustomAttributesData() =>
            VariableType == VariableType.Field ? FieldInfo.GetCustomAttributesData() : PropertyInfo.GetCustomAttributesData();

        public override bool HasSameMetadataDefinitionAs(MemberInfo other) =>
            VariableType == VariableType.Field ? FieldInfo.HasSameMetadataDefinitionAs(other) : PropertyInfo.HasSameMetadataDefinitionAs(other);
    }
}
