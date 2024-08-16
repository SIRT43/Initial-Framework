using System;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.Math.Interpolates
{
    public interface IDeltaInterpolate<T> where T : struct
    {
        public T Update(float deltaTime);
        public void TryReset();
    }

    [Serializable]
    public class DeltaInterpolate<TLerp, TValue> : IDeltaInterpolate<TValue>
        where TLerp : LinearInterpolate<TValue>, new()
        where TValue : struct
    {
        [Min(0)] public float deltaTimeScale;
        public bool transition = false;
        public float current;

        public TLerp lerp;

        public virtual TValue Update(float deltaTime)
        {
            current += deltaTime * deltaTimeScale;
            TValue result = lerp.GetValue(current);
            TryReset();

            return result;
        }

        public virtual void TryReset()
        {
            if (current <= 1) return;

            current = 0;
            if (transition) lerp.inverted = !lerp.inverted;
        }

        public DeltaInterpolate() : this(new()) { }
        public DeltaInterpolate(TLerp lerp) : this(lerp, 1, false, 0) { }
        public DeltaInterpolate(TLerp lerp, float deltaTimeScale, bool transition, float current)
        {
            this.deltaTimeScale = deltaTimeScale;
            this.transition = transition;
            this.current = current;
            this.lerp = lerp;
        }
    }
}
