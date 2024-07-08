using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
namespace DynamicAudio
{
    [System.Serializable]

    public class Sound
    {   [Color(1f,0.1f,0.6f)]
        public AudioClip audioClip;
     
        [Range(0, 256)]
        [ColorBackground(1f, 0.1f, 0.6f, 0.2f)]
        public float piority;
        [Range(0, 1)]

        
        public float volume;
    }
    [System.Serializable]
    public struct EnviromentProperty // idk what should i name this lol
    {
        [ReadOnly]
        public MaterialType materialType;
        [ColorBackground(1, 0, 0, 0.05f)]
        public Sound[] objectSounds;
        [ColorBackground(0, 0.3f, 0.2f, 0.2f)]
        public ObjectParticle[] particleSystems;
        public UnityEvent objectEvent;
    }
    [System.Serializable]
    public struct SoundProperty 
    {
        [ReadOnly]
        public MaterialType materialType;     
        public Sound[] objectSounds;
    }
    [System.Serializable]
    public struct ParticleProperty 
    {
        [ReadOnly]
        public MaterialType materialType;
        public ObjectParticle[] particleSystems;
    }
    [System.Serializable]
    public struct EventProperty 
    {
        [ReadOnly]
        public MaterialType materialType;
        public UnityEvent objectEvent;
    }

    [System.Serializable]
    public struct ObjectParticle
    {
        [ColorBackground(1, 1, 0, 0.05f)]
        public GameObject ParticleObject;
        [ColorBackground(1, 1, 0, 0.05f)]
        public Transform particleSpawnTransform;
        [ColorBackground(1, 1, 0, 0.05f)]
        public float startDelay;
        [ColorBackground(1, 1, 0, 0.05f)]
        public ParticleSystemStopAction action;
    }
    public enum MaterialType
    {
        Gravel,
        Water,
        Stone,
        Wood,
        Concrete,
        Dirt,
        Grass,
        Glass,
        Metal,
        None

    }
    [System.Serializable]
    public class EnviromentEventProperty
    {
        public EnviromentProperty[] objectAudios = new EnviromentProperty[System.Enum.GetValues(typeof(MaterialType)).Length];
        public EnviromentEventProperty()
        {
            for(int i  = 0; i < System.Enum.GetValues(typeof(MaterialType)).Length;i++)
            {
                objectAudios[i].materialType = (MaterialType)i;
            }
        }
    }

    [System.Serializable]
    public class EventEventProperty
    {
        public EventProperty[] eventProperties = new EventProperty[System.Enum.GetValues(typeof(MaterialType)).Length];

        public EventEventProperty()
        {
            for (int i = 0; i < System.Enum.GetValues(typeof(MaterialType)).Length; i++)
            {
                eventProperties[i].materialType = (MaterialType)i; 
            }
        }
    }

    [System.Serializable]
    public class ParticleEventProperty
    {
        public ParticleProperty[] particleProperties = new ParticleProperty[System.Enum.GetValues(typeof(MaterialType)).Length];

        public ParticleEventProperty()
        {
            for (int i = 0; i < System.Enum.GetValues(typeof(MaterialType)).Length; i++)
            {
                particleProperties[i].materialType = (MaterialType)i;
            }
        }
    }

    [System.Serializable]
    public class SoundEventProperty
    {
        public SoundProperty[] soundProperties = new SoundProperty[System.Enum.GetValues(typeof(MaterialType)).Length];

        public SoundEventProperty()
        {
            for (int i = 0; i < System.Enum.GetValues(typeof(MaterialType)).Length; i++)
            {
                soundProperties[i].materialType = (MaterialType)i;
            }
        }
    }

}
