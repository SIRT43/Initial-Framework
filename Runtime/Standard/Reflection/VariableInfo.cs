using FTGAMEStudio.InitialFramework.ExtensionMethods;
using System;
using System.Reflection;

namespace FTGAMEStudio.InitialFramework.Reflection
{
    public interface IVariableInfo<TMemberType> : IVariableInfo where TMemberType : MemberInfo
    {
        public new TMemberType Original { get; }
    }

    public class VariableInfo : VariableInfoBase
    {
        public static implicit operator VariableInfo(FieldInfo fieldInfo) => new(fieldInfo);
        public static implicit operator VariableInfo(PropertyInfo propertyInfo) => new(propertyInfo);



        protected MemberInfo target;
        protected override MemberInfo Target => target;

        private VariableType variableType;
        public override VariableType VariableType { get => variableType; protected set => variableType = value; }



        /// <summary>
        /// �ڴ�֮ǰ���������ṩ�ֶλ����ԡ�
        /// </summary>
        public VariableInfo(FieldInfo fieldInfo)
        {
            target = fieldInfo;
            variableType = VariableType.Field;
        }
        /// <summary>
        /// �ڴ�֮ǰ���������ṩ�ֶλ����ԡ�
        /// </summary>
        public VariableInfo(PropertyInfo propertyInfo)
        {
            target = propertyInfo;
            variableType = VariableType.Property;
        }

        protected VariableInfo() { }
    }

    public class VariableInfo<TMemberType> : VariableInfo, IVariableInfo<TMemberType> where TMemberType : MemberInfo
    {
        public static implicit operator VariableInfo<TMemberType>(TMemberType memberType) => new(memberType);



        private readonly TMemberType original;
        public new TMemberType Original => original;

        /// <summary>
        /// �ڴ�֮ǰ���������ṩ��Ϣ��
        /// </summary>
        public VariableInfo(TMemberType memberType)
        {
            target = memberType;

            if (memberType is FieldInfo) VariableType = VariableType.Field;
            else if (memberType is PropertyInfo) VariableType = VariableType.Property;
            else throw new ArgumentException($"{memberType.GetType().GetUniqueName()} is not the expected type.", nameof(memberType));
        }
    }
}
