using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DynamicAudio
{
    [RequireComponent(typeof(AudioSource))]
    public class ObjectInfo : MonoBehaviour
    {
        public Sound[] ownedSound;
        public ObjectParticle[] particle;
        public AudioSource source;
        public MaterialType materialType;
        public void Awake()
        {
            source = GetComponent<AudioSource>();
            source.spatialBlend = 1f;
        }
        
    }
    
}