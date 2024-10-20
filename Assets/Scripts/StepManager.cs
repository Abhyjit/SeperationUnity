using Neur;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Neur
{
    public class StepManager : MonoBehaviour
    {
        public List<StepData> steps;  // List of ScriptableObjects for each step
        public int currentStep = 0;  // Tracks the current step
        public AudioSource audioSource;  // Plays the step audio
        public TMPro.TextMeshProUGUI stepTextUI;  // Display the step text in UI

        public bool stepCompleted = false;  // Tracks if the current step is completed

        public static StepManager instance;

        public GameObject Bowl;
        public GameObject MovableBowl;
        public GameObject Beaker;
        public GameObject EmptyBeaker;
        public GameObject SandBeaker;

        public GameObject DropZone;
        public GameObject BowlDropZone;
        public GameObject waterDropper;
        public GameObject media;
        public GameObject media2;

        public GameObject NextButton;

        private void Awake()
        {
            if (instance == null) 
            { 
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
            StartCoroutine(TrackStepsCoroutine());  // Start the coroutine to track steps
        }

        // Coroutine to track and progress steps
        private IEnumerator TrackStepsCoroutine()
        {
            while (currentStep < steps.Count)
            {
                LoadStep(currentStep);

                // Wait for the step to be completed
                while (!stepCompleted)
                {
                    yield return null;  // Wait until stepCompleted becomes true
                }

                // Reset the stepCompleted flag for the next step
                stepCompleted = false;

                // Move to the next step
                if (currentStep < steps.Count - 1)
                {
                    currentStep++;
                }
                else
                {
                    Debug.Log("All steps completed!");
                    yield break;  // Exit the coroutine when all steps are completed
                }
            }
        }

        // Function to mark the current step as completed
        public void CompleteStep()
        {
            stepCompleted = true;
        }

        private void LoadStep(int stepIndex)
        {
            StepData step = steps[stepIndex];
            stepTextUI.text = step.stepText;  // Display the step text
            audioSource.clip = step.stepAudio;  // Set the audio clip
            audioSource.Play();  // Play the audio

            TriggerGameObjectEvents(step);
            HandleStepInteraction(stepIndex);
        }
        private void TriggerGameObjectEvents(StepData step)
        {
            // Enable specified GameObjects
            foreach (GameObject obj in step.objectsToEnable)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }

            // Disable specified GameObjects
            foreach (GameObject obj in step.objectsToDisable)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }
        private void HandleStepInteraction(int stepIndex)
        {
            switch (stepIndex)
            {
                case 0:
                    // Example interaction for Step 0
                    Debug.Log("Interaction for Step 0");
                    Bowl.GetComponent<HighlightOnClick>().enabled = false;
                  //  StartCoroutine(WaitBeforeProceeding(10f));
                    // Custom logic for step 0 (e.g., start an animation, activate an effect, etc.)
                    break;

                case 1:
                    // Example interaction for Step 1
                    Debug.Log("Interaction for Step 1");
                    NextButton.SetActive(false);
                    Bowl.GetComponent<HighlightOnClick>().enabled= true;
                    Bowl.GetComponent<HighlightOnClick>().HighlightObject();

                    // Custom logic for step 1 (e.g., trigger a puzzle, change lighting, etc.)
                    break;

                case 2:
                    // Example interaction for Step 2

                    Bowl.GetComponent<HighlightOnClick>().enabled = false;
                    Beaker.GetComponent<DragAndDrop>().enabled = true;
                    DropZone.gameObject.SetActive(true);
                    Debug.Log("Interaction for Step 2");
                    // Custom logic for step 2 (e.g., open a door, play a cutscene, etc.)
                    break;
                case 3:
                    //StartCoroutine(WaitBeforeProceeding(5f));
                    Debug.Log("Interaction for Step 3");
                    break;
                case 4:
                    waterDropper.gameObject.SetActive(false);
                    media.SetActive(false);
                    media2.GetComponent<Rigidbody>().freezeRotation = true;
                    media2.GetComponent<ConstantForce>().enabled = false;
                    Debug.Log("Interaction for Step 4");
                    break;
                case 5:
                    Bowl.SetActive(false);
                    EmptyBeaker.SetActive(true);
                    
                    MovableBowl.SetActive(true);
                    BowlDropZone.SetActive(true);
                    Debug.Log("Interaction for Step 5");
                    break;
                case 6:
                    break;


                // Add more cases for additional steps
                default:
                    Debug.Log("No specific interaction for this step.");
                    

                    break;
            }
        }

        // Optional function to allow manual progression (if needed)
        public void NextStepManually()
        {
            if (currentStep < steps.Count - 1)
            {
                currentStep++;
                LoadStep(currentStep);
            }
        }
        //public IEnumerator WaitBeforeProceeding(float waitTime)
        //{
        //    yield return new WaitForSeconds(waitTime);
        //    print(waitTime);
        //    CompleteStep(); // Proceed to the next step after the delay
        //}
        //public void Coroutiine(float hii)
        //{
        //    StartCoroutine(WaitBeforeProceeding(hii));
        //}
    }

}
