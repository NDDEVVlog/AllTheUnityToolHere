using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

    [System.Serializable]
    public class AnimationEventCallbackData
    {
    public string CallbackID;
        [Multiline] public string description;
        public UnityEvent callback = new();
    }


    public class AnimationEventCallback : MonoBehaviour
    {
        [SerializeField] private List<AnimationEventCallbackData> animationCallbacks = new();
    [HideInInspector] public List<string> callbackIDstrings;




        
        public void DoAnimationCallback(int indexCallback)
        {
            animationCallbacks[indexCallback].callback?.Invoke();
        }
        public void DoAnimationCallbackByName(string actionName)
        {
        animationCallbacks[FindIndexByCallbackID(actionName)].callback?.Invoke();
        }


        public void DoSth(int active)
    {

    }
    private int FindIndexByCallbackID(string callbackID)
    {
        for (int i = 0; i < animationCallbacks.Count; i++)
        {
            if (animationCallbacks[i].CallbackID == callbackID)
            {
                return i;
            }
        }
        return -1; // Not found
    }
    // Function to get a list of CallbackIDs
    public List<string> GetCallbackIDs()
    {
        List<string> callbackIDs = new List<string>();
        foreach (var callbackData in animationCallbacks)
        {
            callbackIDs.Add(callbackData.CallbackID);
        }
        return callbackIDs;
    }

    private void OnValidate()
    {
        callbackIDstrings = GetCallbackIDs();
    }
}
