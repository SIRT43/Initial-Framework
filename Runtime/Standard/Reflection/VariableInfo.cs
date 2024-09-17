using System;
using System.Reflection;

namespace InitialFramework.Reflection
{
    public interface IVariableInfo<TMemberType> : IVariableInfo where TMemberType : MemberInfo
    {
        new TMemberType Original { get; }
    }

    public class VariableInfo : VariableInfoBase
    {
        public static implicit operator VariableInfo(FieldInfo fieldInfo) => new(fieldInfo);
        public static implicit operator VariableInfo(PropertyInfo propertyInfo) => new(propertyInfo);



        public VariableInfo(FieldInfo fieldInfo) : base(fieldInfo) { }
        public VariableInfo(PropertyInfo propertyInfo) : base(propertyInfo) { }

        protected VariableInfo() : base() { }
    }

    public class VariableInfo<TMemberType> : VariableInfo, IVariableInfo<TMemberType> where TMemberType : MemberInfo
    {
        public static implicit operator VariableInfo<TMemberType>(TMemberType memberType) => new(memberType);



        public new TMemberType Original { get; protected set; }


        public VariableInfo(TMemberType memberType)
        {
            Original = memberType;

            if (memberType is not FieldInfo or PropertyInfo)
                throw new ArgumentException($"{memberType.GetType().GetUniqueName()} is not the expected type.", nameof(memberType));
        }
    }
}
