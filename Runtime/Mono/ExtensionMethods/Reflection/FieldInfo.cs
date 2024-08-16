using System;
using System.Reflection;

namespace FTGAMEStudio.InitialFramework.ExtensionMethods
{
    public static class FieldInfoMethods
    {
        public static bool IsSameType(this FieldInfo fieldInfo, FieldInfo other) => fieldInfo.FieldType == other.FieldType;
        public static bool IsSameType(this FieldInfo fieldInfo, PropertyInfo other) => fieldInfo.FieldType == other.PropertyType;

        public static void SetValue(this FieldInfo fieldInfo, object obj, object other, FieldInfo otherField)
        {
            if (!fieldInfo.IsSameType(otherField)) throw new InvalidOperationException("Invalid assignment operation with inconsistent types.");

            fieldInfo.SetValue(obj, otherField.GetValue(other));
        }

        public static void SetValue(this FieldInfo fieldInfo, object obj, object other, PropertyInfo otherProperty)
        {
            if (!fieldInfo.IsSameType(otherProperty)) throw new InvalidOperationException("Invalid assignment operation with inconsistent types.");

            fieldInfo.SetValue(obj, otherProperty.GetValue(other));
        }
    }
}
