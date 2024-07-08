using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sora_Ults
{
    public class SO_Scale_Handler
    {
        private ISecondOrderSystem selectedAlgorithm;
        private SecondOrderState stateX;
        private SecondOrderState stateY;
        private SecondOrderState stateZ;
        private Vector3 initialScale;
        
        void SelectAlgorithm(SelectedAlgorithm Input)
        {
            switch (Input)
            { 
                case SelectedAlgorithm.ZeroPole:
                    selectedAlgorithm = new SO_Calc_ZeroPole();
                    break;
                case SelectedAlgorithm.StableForcedIterations:
                    selectedAlgorithm = new SO_Calc_StableForcedIterations();
                    break;
                case SelectedAlgorithm.Euler:
                    selectedAlgorithm = new SO_Calc_Euler();
                    break;
                case SelectedAlgorithm.Linear:
                    selectedAlgorithm = new SO_Calc_Linear();
                    break;
                case SelectedAlgorithm.EulerClampedK2:
                    selectedAlgorithm = new SO_Calc_EulerClampedK2();
                    break;
                case SelectedAlgorithm.EulerNoJitter:
                    selectedAlgorithm = new SO_Calc_EulerNoJitter();
                    break;
                case SelectedAlgorithm.None:
                    selectedAlgorithm = new SO_Calc_None();
                    break;
                default:
                    Debug.Log("pick plz");
                    selectedAlgorithm = new SO_Calc_None();
                    break;
            }
        }

        public void Initialize(float frequency, float damping, float responsiveness, float deltaTimeScale,
            Vector3 initialScale, SelectedAlgorithm ASelector , Vector3 targetScale)
        {
            SelectAlgorithm(ASelector);

            stateX = new SecondOrderState
            {
                F = frequency,
                Z = damping,
                R = responsiveness,
                InitialValue = initialScale.x,
                DeltaTime = deltaTimeScale,
                TargetValue = targetScale.x,
            };
            stateY = new SecondOrderState
            {
                F = frequency,
                Z = damping,
                R = responsiveness,
                InitialValue = initialScale.y,
                DeltaTime = deltaTimeScale,
                TargetValue = targetScale.y,
            };
            stateZ = new SecondOrderState
            {
                F = frequency,
                Z = damping,
                R = responsiveness,
                InitialValue = initialScale.z, //0
                DeltaTime = deltaTimeScale,
                TargetValue = targetScale.z, //1
            };

            selectedAlgorithm.RecalculateConstants(ref stateX, stateX.InitialValue);
            selectedAlgorithm.RecalculateConstants(ref stateY, stateY.InitialValue);
            selectedAlgorithm.RecalculateConstants(ref stateZ, stateZ.InitialValue);
            
            //Debug.Log(initialScale);
            this.initialScale = initialScale;
        }

        
        public Vector3 UpdateScale(Vector3 targetScale)
        {
            float deltaTime = Time.deltaTime * stateX.DeltaTime;

            Vector3 targetValue = targetScale;

            Vector3 updatedValue = new Vector3(
                selectedAlgorithm.UpdateStrategy(ref stateX, deltaTime, targetValue.x, null),
                selectedAlgorithm.UpdateStrategy (ref stateY, deltaTime, targetValue.y, null),
                selectedAlgorithm.UpdateStrategy(ref stateZ, deltaTime, targetValue.z, null));

            //transformToUpdate.Scale =  updatedValue;
            return updatedValue;
        }
    }
}

