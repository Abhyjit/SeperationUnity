using Neur;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

namespace Neur
{
    public class StepManager : MonoBehaviour
    {
        public List<StepData> steps;
        public int currentStep = 0;
        public AudioSource audioSource;
        public AudioSource SOundaudioSource;
        public TMPro.TextMeshProUGUI stepTextUI;

        public bool stepCompleted = false;

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

        public GameObject Kettle;
        public GameObject SandZone;

        public GameObject NextButton;

        public AudioClip WaterPouring;
        public AudioClip ClockTicking;

        public Material Highlight;
        public GameObject Fire;

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
            StartCoroutine(TrackStepsCoroutine());
        }

        private IEnumerator TrackStepsCoroutine()
        {
            while (currentStep < steps.Count)
            {
                LoadStep(currentStep);

                while (!stepCompleted)
                {
                    yield return null;
                }

                stepCompleted = false;

                if (currentStep < steps.Count - 1)
                {
                    currentStep++;
                }
                else
                {
                    Debug.Log("All steps completed!");
                    yield break;
                }
            }
        }

        public void CompleteStep()
        {
            stepCompleted = true;
        }

        private void LoadStep(int stepIndex)
        {
            StepData step = steps[stepIndex];
            stepTextUI.text = step.stepText;
            audioSource.clip = step.stepAudio;
            audioSource.Play();

            TriggerGameObjectEvents(step);
            HandleStepInteraction(stepIndex);
        }

        private void TriggerGameObjectEvents(StepData step)
        {
            foreach (GameObject obj in step.objectsToEnable)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }

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
                    Debug.Log("Interaction for Step 0");
                    Bowl.GetComponent<HighlightOnClick>().enabled = false;
                    break;
                case 1:
                    Debug.Log("Interaction for Step 1");
                    NextButton.SetActive(false);
                    Bowl.GetComponent<HighlightOnClick>().enabled = true;
                    Bowl.GetComponent<MeshRenderer>().material = Highlight;
                    break;
                case 2:
                    Bowl.GetComponent<HighlightOnClick>().enabled = false;
                    Beaker.GetComponent<DragAndDrop>().enabled = true;
                    DropZone.gameObject.SetActive(true);
                    Debug.Log("Interaction for Step 2");
                    break;
                case 3:
                    SOundaudioSource.clip = ClockTicking;
                    SOundaudioSource.Play();
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
                    Debug.Log("Interaction for Step 6");
                    break;
                case 7:
                    Kettle.SetActive(true);
                    Kettle.GetComponent<HighlightOnClick>().enabled = true;
                    Kettle.GetComponent<MeshRenderer>().material = Highlight;
                    Debug.Log("Interaction for Step 7");
                    break;
                case 8:
                    SandBeaker.GetComponent<DragAndDrop>().enabled = true;
                    SandZone.SetActive(true);
                    Debug.Log("Interaction for Step 8");
                    break;
                case 9:
                    Fire.SetActive(true);
                    break;
                default:
                    Debug.Log("No specific interaction for this step.");
                    break;
            }
        }

        public void NextStepManually()
        {
            if (currentStep < steps.Count - 1)
            {
                currentStep++;
                LoadStep(currentStep);
            }
        }

        
        public void ReloadScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;  
            Debug.Log("Game Paused");
        }

        
        public void ResumeGame()
        {
            Time.timeScale = 1f;  
            Debug.Log("Game Resumed");
        }

       
        public void QuitGame()
        {
            Debug.Log("Quitting Game...");
            Application.Quit();
        }
    }
}
