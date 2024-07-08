using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Sora_Ults
{
    public class SO_Quaternion_Handler
    {
        private ISecondOrderSystem selectedAlgorithm;
        private SecondOrderState stateX;
        private SecondOrderState stateY;
        private SecondOrderState stateZ;
        private SecondOrderState stateW;
        private Quaternion initialRotation;
        
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
        
        public void Initialize(float frequency, float damping, float responsiveness, float deltaTimeScale, Quaternion initialRotation, SelectedAlgorithm ASelector, Quaternion targetRotation)
        {
            SelectAlgorithm(ASelector);

            stateX = new SecondOrderState
            {
                F = frequency,
                Z = damping,
                R = responsiveness,
                InitialValue = initialRotation.x,
                DeltaTime = deltaTimeScale,
                TargetValue = initialRotation.x,
            };
            stateY = new SecondOrderState
            {
                F = frequency,
                Z = damping,
                R = responsiveness,
                InitialValue = initialRotation.y,
                DeltaTime = deltaTimeScale,
                TargetValue = initialRotation.y,
            };
            stateZ = new SecondOrderState
            {
                F = frequency,
                Z = damping,
                R = responsiveness,
                InitialValue = initialRotation.z, //0
                DeltaTime = deltaTimeScale,
                TargetValue = initialRotation.z, //1
            };
            stateW = new SecondOrderState
            {
                F = frequency,
                Z = damping,
                R = responsiveness,
                InitialValue = initialRotation.w, //0
                DeltaTime = deltaTimeScale,
                TargetValue = initialRotation.w, //1
            };

            selectedAlgorithm.RecalculateConstants(ref stateX, stateX.InitialValue);
            selectedAlgorithm.RecalculateConstants(ref stateY, stateY.InitialValue);
            selectedAlgorithm.RecalculateConstants(ref stateZ, stateZ.InitialValue);
            selectedAlgorithm.RecalculateConstants(ref stateW, stateW.InitialValue);
            
            //Debug.Log(initialPosition);
            this.initialRotation = initialRotation;
        }
        public Quaternion UpdateRotation(Quaternion targetRotation)
        {
            float deltaTime = Time.deltaTime * stateX.DeltaTime;
            Quaternion targetValue = targetRotation;
            
            Quaternion updatedRotation = new Quaternion(
                    selectedAlgorithm.UpdateStrategy(ref stateX, deltaTime, targetValue.x, null),
                    selectedAlgorithm.UpdateStrategy(ref stateY, deltaTime, targetValue.y, null),
                    selectedAlgorithm.UpdateStrategy(ref stateZ, deltaTime, targetValue.z, null),
                    selectedAlgorithm.UpdateStrategy(ref stateW, deltaTime, targetValue.w, null));
            
            return updatedRotation;
        }
    }
}
