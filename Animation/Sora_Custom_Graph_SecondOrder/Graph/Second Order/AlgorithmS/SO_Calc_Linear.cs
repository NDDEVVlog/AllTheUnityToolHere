using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sora_Ults;

namespace Sora_Ults
{
    public class SO_Calc_Linear: ISecondOrderSystem
    {
        public override void RecalculateConstants(ref SecondOrderState state, float initialValue) { }

        public override void OnNewValue(ref SecondOrderState state, float deltaTime, float targetValue, float targetVelocity)
            => state.CurrentValue = targetValue;
    }
}
