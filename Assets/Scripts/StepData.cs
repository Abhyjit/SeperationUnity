using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Neur
{
    [CreateAssetMenu(menuName = "Seperation/ Step")]

    public class StepData : ScriptableObject
    {
        public string stepText;  // Text describing the step
        public AudioClip stepAudio;  // Audio clip for the step
    }
}

