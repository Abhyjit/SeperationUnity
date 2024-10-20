using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Neur
{
    [CreateAssetMenu(menuName = "Seperation/ Step")]

    public class StepData : ScriptableObject
    {
        public string stepText;  // Text describing the step
        public AudioClip stepAudio;

        public List<GameObject> objectsToEnable;
        public List<GameObject> objectsToDisable;// Audio clip for the step
    }
}

