    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicAudio;
using UnityEngine.Events;
public class EnviromentInteractionEvent : MonoBehaviour
{
    public static void PlayParticleAndSound(ObjectInfo objectInfo, EnviromentEventProperty propertyHolder)
    {
        PlayPaticle(objectInfo, propertyHolder);
        PlaySound(objectInfo, propertyHolder);
    }
    public static void PlayEvent(ObjectInfo objectInfo, EnviromentEventProperty propertyHolder)
    {
        int value = ReturnObjectNumber(objectInfo);
        MaterialType type = (MaterialType)value;
        UnityEvent events = ReturnObjectEvent(propertyHolder, value);
        events?.Invoke();
    }
    public static void PlayPaticleAtPosition(ObjectInfo objectInfo, EnviromentEventProperty propertyHolder,Vector3 transformPar)
    {
        int value = ReturnObjectNumber(objectInfo);
        MaterialType type = (MaterialType)value;
        if (type == MaterialType.None)
        {

            int ranNum = Random.Range(0, objectInfo.particle.Length);
            GameObject particleObject = objectInfo.particle[ranNum].ParticleObject;
            if (!particleObject.GetComponent<ParticleSystem>()) return;
            Debug.LogWarning("PlaySound");
            // Reference ParticleSystem component (already retrieved)

            // Store MainModule in a variable
            var mainModule = particleObject.GetComponent<ParticleSystem>().main;

            // Modify startDelay and stopAction using the variable
            mainModule.startDelay = objectInfo.particle[ranNum].startDelay;
            mainModule.stopAction = objectInfo.particle[ranNum].action;

            // Instantiate at desired position
            Instantiate(particleObject, transformPar,Quaternion.identity);
            return;
        }

        ObjectParticle[] objectParticles = ReturnObjectParticle(propertyHolder, value);
        int ran = Random.Range(0, objectParticles.Length);
        GameObject particleObj = objectParticles[ran].ParticleObject;
        Debug.Log(particleObj.GetComponent<ParticleSystem>());
        if (!particleObj.GetComponent<ParticleSystem>()) return;

        // Reference ParticleSystem component (already retrieved)

        // Store MainModule in a variable
        var main = particleObj.GetComponent<ParticleSystem>().main;

        // Modify startDelay and stopAction using the variable
        main.startDelay = objectParticles[ran].startDelay;
        main.stopAction = objectParticles[ran].action;

        // Instantiate at desired position
        Instantiate(particleObj, transformPar, Quaternion.identity);
    }
    public static void PlayPaticle(ObjectInfo objectInfo, EnviromentEventProperty propertyHolder)
    {
        int value = ReturnObjectNumber(objectInfo);
        MaterialType type = (MaterialType)value;
        if (type == MaterialType.None)
        {
            
            int ranNum = Random.Range(0, objectInfo.particle.Length);
            GameObject particleObject = objectInfo.particle[ranNum].ParticleObject;
            if (!particleObject.GetComponent<ParticleSystem>()) return;
            Debug.LogWarning("PlaySound");
            // Reference ParticleSystem component (already retrieved)

            // Store MainModule in a variable
            var mainModule = particleObject.GetComponent<ParticleSystem>().main;

            // Modify startDelay and stopAction using the variable
            mainModule.startDelay = objectInfo.particle[ranNum].startDelay;
            mainModule.stopAction = objectInfo.particle[ranNum].action;

            // Instantiate at desired position
            Instantiate(particleObject, objectInfo.particle[ranNum].particleSpawnTransform);
            return;
        }

        ObjectParticle[] objectParticles = ReturnObjectParticle(propertyHolder, value);
        int ran = Random.Range(0, objectParticles.Length);
        GameObject particleObj = objectParticles[ran].ParticleObject;
        if (!particleObj.GetComponent<ParticleSystem>()) return;
   
        // Reference ParticleSystem component (already retrieved)

        // Store MainModule in a variable
        var main = particleObj.GetComponent<ParticleSystem>().main;

        // Modify startDelay and stopAction using the variable
        main.startDelay = objectParticles[ran].startDelay;
        main.stopAction = objectParticles[ran].action;

        // Instantiate at desired position
        Instantiate(particleObj, objectParticles[ran].particleSpawnTransform);
    }

    public static void PlaySound(ObjectInfo objectInfo, EnviromentEventProperty soundHolder)
    {
        //Get Enum Value Number
        int value = ReturnObjectNumber(objectInfo);
        MaterialType type = (MaterialType)value;
        if(type == MaterialType.None)
        {
            PlaySound(objectInfo);
            return;

        }
        //Get Sound 
        Sound[] sounds = ReturnSounds(soundHolder,value);

        int num = Random.Range(0, sounds.Length);
        objectInfo.source.clip = sounds[num].audioClip;
        objectInfo.source.volume = sounds[num].volume;
        objectInfo.source.priority = (int)sounds[num].piority;
        objectInfo.source.PlayOneShot(objectInfo.source.clip);
    }
    public static void PlaySound(ObjectInfo objectInfo)
    {
        int num = Random.Range(0, objectInfo.ownedSound.Length);
        objectInfo.source.clip = objectInfo.ownedSound[num].audioClip;
        objectInfo.source.volume = objectInfo.ownedSound[num].volume;
        objectInfo.source.priority = (int)objectInfo.ownedSound[num].piority;
        objectInfo.source.PlayOneShot(objectInfo.source.clip);
    }
    
    public static void PlaySound(int layer,GameObject ob, EnviromentEventProperty soundHolder)
    {
        string mask = LayerMask.LayerToName(layer);
        MaterialType materialType;
        if(System.Enum.TryParse(mask,out materialType))
        {
            materialType = (MaterialType)System.Enum.Parse(typeof(MaterialType), mask);

            int value = (int)System.Enum.Parse(typeof(MaterialType), materialType.ToString());
           
            Sound[] sounds = ReturnSounds(soundHolder,value);
            AudioSource source = ob.GetComponent<AudioSource>();
            int num = Random.Range(0, sounds.Length);

            source.clip = sounds[num].audioClip;
            source.volume = sounds[num].volume;
            source.priority = (int)sounds[num].piority;
            source.PlayOneShot(source.clip);
        }
        
    }
    public static void PlaySound(MaterialType materialType, EnviromentEventProperty soundHolder,GameObject ob)
    {

        int i = (int)materialType;
        Sound[] sounds = ReturnSounds(soundHolder, i);
        AudioSource source = ob.GetComponent<AudioSource>();

        int num = Random.Range(0, sounds.Length);

        source.clip = sounds[num].audioClip;
        source.volume = sounds[num].volume;
        source.priority = (int)sounds[num].piority;
        source.PlayOneShot(source.clip);

    }
    static Sound[] ReturnSounds(EnviromentEventProperty holder, int value)
    {
        return holder.objectAudios[value].objectSounds;
    }
    static ObjectParticle[] ReturnObjectParticle(EnviromentEventProperty holder, int value)
    {
        return holder.objectAudios[value].particleSystems;
    }
    static UnityEvent ReturnObjectEvent(EnviromentEventProperty holder, int value)
    {
        return holder.objectAudios[value].objectEvent;
    }
    static int ReturnObjectNumber(ObjectInfo objectInfo)
    {
        return (int)System.Enum.Parse(typeof(MaterialType), objectInfo.materialType.ToString()); // return the number of element in Enum
    }    
}
