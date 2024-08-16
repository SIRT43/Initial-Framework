using System;
using System.Reflection;

namespace FTGAMEStudio.InitialFramework.ExtensionMethods
{
    public static class PropertyInfoMethods
    {
        public static bool IsSameType(this PropertyInfo propertyInfo, PropertyInfo other) => propertyInfo.PropertyType == other.PropertyType;
        public static bool IsSameType(this PropertyInfo propertyInfo, FieldInfo other) => propertyInfo.PropertyType == other.FieldType;

        public static void SetValue(this PropertyInfo propertyInfo, object obj, object other, PropertyInfo otherProperty)
        {
            if (!propertyInfo.IsSameType(otherProperty)) throw new InvalidOperationException("Invalid assignment operation with inconsistent types.");

            propertyInfo.SetValue(obj, otherProperty.GetValue(other));
        }

        public static void SetValue(this PropertyInfo propertyInfo, object obj, object other, FieldInfo otherField)
        {
            if (!propertyInfo.IsSameType(otherField)) throw new InvalidOperationException("Invalid assignment operation with inconsistent types.");

            propertyInfo.SetValue(obj, otherField.GetValue(other));
        }
    }
}
