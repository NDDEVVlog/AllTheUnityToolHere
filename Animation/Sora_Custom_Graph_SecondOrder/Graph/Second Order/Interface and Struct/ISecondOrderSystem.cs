    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    namespace Sora_Ults
    {
        public abstract class ISecondOrderSystem
        {
            public virtual void RecalculateConstants(ref SecondOrderState state, float initialValue)
            {
                // Default implementation (None)
            }

            public virtual void OnNewValue(ref SecondOrderState state, float deltaTime, float targetValue, float targetVelocity)
            {
                // Default implementation (None)
            }
        

            /// <summary>
            /// This function is declare  the initial value for K1,k2,k3 base on frequency, zeta , responsive
            /// </summary> NDDevgame command this shit
            /// <param name="state"></param>
            /// <param name="initialValue"></param>
            internal static void DefaultConstantCalculation(ref SecondOrderState state, float initialValue)
            {
                state.K1 = state.Z / (Mathf.PI * state.F);

                float tauFrequency = (2 * Mathf.PI * state.F); // this is 2.pi.f. In physic we call this Omega .WTF taufrequency??????

                state.K2 = 1 / (tauFrequency * tauFrequency);
                state.K3 = state.R / tauFrequency;

                state.MaximumTimeStep = 0.8f * (Mathf.Sqrt((4 * state.K2) + (state.K1 * state.K1)) - state.K1);

                state.PreviousTargetValue = initialValue;
                state.CurrentValue = initialValue;
                state.CurrentVelocity = default;
            }
        
            protected static void Integrate(ref SecondOrderState state,
                float deltaTime,
                float targetValue,
                float targetVelocity,
                float? k1Override = null,
                float? k2Override = null
            )
            {
                float k1 = k1Override ?? state.K1;
                float k2 = k2Override ?? state.K2;

                // integrate position by velocity
                state.CurrentValue += (deltaTime * state.CurrentVelocity); // y[n+1] = y[n] + T*(dy/dt)

                // integrate velocity by acceleration
                // (dy/dt)[n+1] = (dy/dt)[n] + T * ( x[n+1] + k3 * (dx/dt)[n+1] - y[n+1] - k1 *  (dy/dt)[n]/k2
                state.CurrentVelocity += (deltaTime * (targetValue + (state.K3 * targetVelocity) - state.CurrentValue - ((k1 * state.CurrentVelocity)) / k2)); 
            }

            private static float UpdateVelocity(ref SecondOrderState state,
                float deltaTime,
                float targetValue,
                float? targetVelocityOrNull
            )
            {
                float estimatedVelocity = (targetValue - state.PreviousTargetValue) / deltaTime; // dx/dt[n+1] = (x[n+1] -x[n])/T
                float xd = targetVelocityOrNull.GetValueOrDefault(estimatedVelocity); // if target velocity is null so xd will be estimateVelocity. Else it will be targetVelocityorNull
                state.PreviousTargetValue = targetValue;
                return xd;
            }
        
            public float UpdateStrategy(ref SecondOrderState state, float deltaTime, float targetValue,
                float? targetVelocity)
            {
                float targetVelocityActual = UpdateVelocity(ref state, deltaTime, targetValue, targetVelocity);
                OnNewValue(ref state, deltaTime, targetValue, targetVelocityActual);
                return state.CurrentValue;
            }
        }
    }
