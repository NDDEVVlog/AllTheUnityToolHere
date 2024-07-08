using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sora_Ults;

namespace Sora_Ults
{
    public class SO_Calc_EulerNoJitter: ISecondOrderSystem
    {
        public override void RecalculateConstants(ref SecondOrderState state, float initialValue)
            => DefaultConstantCalculation(ref state, initialValue);

        /// <inheritdoc />
        public override void OnNewValue(ref SecondOrderState state, float deltaTime, float targetValue, float targetVelocity)
        {
            // clamp k2 above the catastrophic error value.
            // not physically correct, but goal is to prevent failure, not produce accurate physics sim

            // clamp k2 to guarantee stability without jitter
            float k2Stable = Mathf.Max(state.K2,
                ((deltaTime * deltaTime) / 2) + (deltaTime * state.K1 / 2),
                deltaTime * state.K1);

            Integrate(ref state, deltaTime, targetValue, targetVelocity, state.K1, k2Stable);
        }
    }
}
