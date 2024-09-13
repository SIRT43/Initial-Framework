using System;
using UnityEngine;

namespace InitialFramework
{
    [Serializable]
    public class IntRandom : IntRange, IRandomable<int>
    {
        public IntRandom(int min, int max) : base(min, max) { }

        public int Range() => UnityEngine.Random.Range(min, max);
    }

    [Serializable]
    public class FloatRandom : FloatRange, IRandomable<float>
    {
        public FloatRandom(float min, float max) : base(min, max) { }

        public float Range() => UnityEngine.Random.Range(min, max);
    }

    [Serializable]
    public class Vector2Random : Vector2Range, IRandomable<Vector2>
    {
        public Vector2Random(Vector2 min, Vector2 max) : base(min, max) { }

        public Vector2 Range() => IFRandom.Range(min, max);
    }

    [Serializable]
    public class Vector3Random : Vector3Range
    {
        public Vector3Random(Vector3 min, Vector3 max) : base(min, max) { }

        public Vector3 Range() => IFRandom.Range(min, max);
    }
}
