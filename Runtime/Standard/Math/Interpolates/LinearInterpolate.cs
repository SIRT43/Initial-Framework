using System;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.Math.Interpolates
{
    public interface ILinearIInterpolation<T> where T : struct
    {
        /// <summary>
        /// 根据 t 获取值。
        /// <br>t 的范围应该在 0 到 1 之间。</br>
        /// </summary>
        public T GetValue(float t);
    }

    [Serializable]
    public abstract class LinearInterpolate<T> : Interpolate<T>, ILinearIInterpolation<T> where T : struct
    {
        protected LinearInterpolate() : base() { }
        protected LinearInterpolate(T min, T max) : base(min, max) { }
        protected LinearInterpolate(T min, T max, bool inverted) : base(min, max, inverted) { }

        public abstract T GetValue(float t);
    }

    [Serializable]
    public class LerpFloat : LinearInterpolate<float>
    {
        public LerpFloat() : base() { }
        public LerpFloat(float min, float max) : base(min, max) { }
        public LerpFloat(float min, float max, bool inverted) : base(min, max, inverted) { }

        public override float GetValue(float t) => Mathf.Lerp(Initial, Final, t);
    }

    [Serializable]
    public class LerpVector2 : LinearInterpolate<Vector2>
    {
        public LerpVector2() : base() { }
        public LerpVector2(Vector2 min, Vector2 max) : base(min, max) { }
        public LerpVector2(Vector2 min, Vector2 max, bool inverted) : base(min, max, inverted) { }

        public override Vector2 GetValue(float t) => Vector2.Lerp(Initial, Final, t);
    }

    [Serializable]
    public class LerpVector3 : LinearInterpolate<Vector3>
    {
        public LerpVector3() : base() { }
        public LerpVector3(Vector3 min, Vector3 max) : base(min, max) { }
        public LerpVector3(Vector3 min, Vector3 max, bool inverted) : base(min, max, inverted) { }

        public override Vector3 GetValue(float t) => Vector3.Lerp(Initial, Final, t);
    }

    [Serializable]
    public class LerpQuaternion : LinearInterpolate<Quaternion>
    {
        public LerpQuaternion() : base() { }
        public LerpQuaternion(Quaternion min, Quaternion max) : base(min, max) { }
        public LerpQuaternion(Quaternion min, Quaternion max, bool inverted) : base(min, max, inverted) { }

        public override Quaternion GetValue(float t) => Quaternion.Lerp(Initial, Final, t);
    }

    [Serializable]
    public class LerpColor : LinearInterpolate<Color>
    {
        public LerpColor() : base() { }
        public LerpColor(Color min, Color max) : base(min, max) { }
        public LerpColor(Color min, Color max, bool inverted) : base(min, max, inverted) { }

        public override Color GetValue(float t) => Color.Lerp(Initial, Final, t);
    }

    [Serializable]
    public class LerpRect : LinearInterpolate<Rect>
    {
        public LerpRect() : base() { }
        public LerpRect(Rect min, Rect max) : base(min, max) { }
        public LerpRect(Rect min, Rect max, bool inverted) : base(min, max, inverted) { }

        public override Rect GetValue(float t)
        {
            Vector2 center = Vector2.Lerp(Initial.center, Final.center, t);
            Vector2 size = Vector2.Lerp(Initial.size, Final.size, t);
            return new Rect(center - size * 0.5f, size);
        }
    }

    [Serializable]
    public class LerpBounds : LinearInterpolate<Bounds>
    {
        public LerpBounds() : base() { }
        public LerpBounds(Bounds min, Bounds max) : base(min, max) { }
        public LerpBounds(Bounds min, Bounds max, bool inverted) : base(min, max, inverted) { }

        public override Bounds GetValue(float t)
        {
            Vector3 center = Vector3.Lerp(Initial.center, Final.center, t);
            Vector3 size = Vector3.Lerp(Initial.size, Final.size, t);
            return new Bounds(center, size);
        }
    }
}
