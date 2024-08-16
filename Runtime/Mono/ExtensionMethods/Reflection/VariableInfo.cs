using FTGAMEStudio.InitialFramework.Reflection;

namespace FTGAMEStudio.InitialFramework.ExtensionMethods
{
    public static class VariableInfoMethods
    {
        public static bool IsSameType(this VariableInfo variableInfo, VariableInfo other) => variableInfo.ValueType == other.ValueType;

        public static void SetValue(this VariableInfo variableInfo, object obj, object other, VariableInfo otherVariable) =>
            variableInfo.SetValue(obj, otherVariable.GetValue(other));
    }
}
