using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sora_Ults;

namespace Sora_Ults
{
    public class SO_Calc_StableForcedIterations: ISecondOrderSystem
    {
        public override void RecalculateConstants(ref SecondOrderState state, float initialValue)
            => DefaultConstantCalculation(ref state, initialValue);

        public override void OnNewValue(ref SecondOrderState state, float deltaTime, float targetValue, float targetVelocity)
        { 
            int iterations = (int)Mathf.Ceil(deltaTime / state.MaximumTimeStep); // take extra iterations if deltaTime > critical time

            float deltaTimePart = deltaTime / iterations; // each iteration now has a smaller step

            for (int scan = 0; scan < iterations; scan++)
            {
                Integrate(ref state, deltaTimePart, targetValue, targetVelocity);
            }
        }
        
    }
}
