namespace FTGAMEStudio.InitialFramework
{
    public static class LogicGate
    {
        public static bool AND(params bool[] bools)
        {
            foreach (bool item in bools) if (!item) return false;
            return true;
        }

        public static bool OR(params bool[] bools)
        {
            foreach (bool item in bools) if (item) return true;
            return false;
        }

        public static bool[] NOT(params bool[] bools)
        {
            bool[] result = new bool[bools.Length];

            for (int index = 0; index < bools.Length; index++) result[index] = !bools[index];

            return result;
        }


        public static bool NAND(params bool[] bools) => !AND(bools);

        public static bool NOR(params bool[] bools) => !OR(bools);
    }
}
