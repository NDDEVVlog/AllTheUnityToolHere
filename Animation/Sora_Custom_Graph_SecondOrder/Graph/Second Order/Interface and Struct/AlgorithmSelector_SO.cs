using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sora_Ults
{
    public enum SelectedAlgorithm {
        ZeroPole,
        Linear,
        Euler,
        StableForcedIterations,
        EulerClampedK2,
        EulerNoJitter,
        None
    }
    public class AlgorithmSelector_SO
    {
        public SelectedAlgorithm selectedAlgorithm;
    }
}