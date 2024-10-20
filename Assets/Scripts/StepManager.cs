using Neur;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

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

        public Dictionary<int, UnityAction> stepActions;  

        
        public GameObject Bowl, MovableBowl, Beaker, EmptyBeaker, SandBeaker;
        public GameObject DropZone, BowlDropZone, waterDropper, media, media2;
        public GameObject Kettle, SandZone,sandDropper, kettleDropper,NextButton, Fire,FireBall,vapour;
        public GameObject MetalPlate,MetalPlateDrop,MetalPlateDropper,waterdroplets,Beaker2,BeakerDrop2,beaker2Water,waterInBeaker,Sediment;
        public AudioClip WaterPouring, ClockTicking,waterDrop;
        public Material Highlight;

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

            InitializeStepActions();

            // Start tracking steps
            StartCoroutine(TrackStepsCoroutine());
        }


        private void InitializeStepActions()
        {
            stepActions = new Dictionary<int, UnityAction>
            {
                { 0, Step0Interaction },
                { 1, Step1Interaction },
                { 2, Step2Interaction },
                { 3, Step3Interaction },
                { 4, Step4Interaction },
                { 5, Step5Interaction },
                { 6, Step6Interaction },
                { 7, Step7Interaction },
                { 8, Step8Interaction },
                { 9, Step9Interaction },
                {10,Step10Interaction },
                {11,Step11Interaction },
                {12,Step12Interaction },
                {13,Step13Interaction },
                {14,Step14Interaction },
                {15,Step15Interaction },
            };
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

            
            if (stepActions.ContainsKey(stepIndex))
            {
                stepActions[stepIndex].Invoke();
            }
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

        
        private void Step0Interaction()
        {
            Debug.Log("Interaction for Step 0");
            Bowl.GetComponent<HighlightOnClick>().enabled = false;
            Bowl.GetComponent<HighlightOnClick>().IsHighlighted = true;
        }

        private void Step1Interaction()
        {
            Debug.Log("Interaction for Step 1");
            NextButton.SetActive(false);
            Bowl.GetComponent<HighlightOnClick>().enabled = true;
            Bowl.GetComponent<MeshRenderer>().material = Highlight;
        }

        private void Step2Interaction()
        {
            Bowl.GetComponent<HighlightOnClick>().enabled = false;
            Beaker.GetComponent<DragAndDrop>().enabled = true;
            DropZone.gameObject.SetActive(true);
            Debug.Log("Interaction for Step 2");
        }

        private void Step3Interaction()
        {
            SOundaudioSource.clip = ClockTicking;
            SOundaudioSource.Play();
            NextButton.SetActive(true);
            Debug.Log("Interaction for Step 3");
        }

        private void Step4Interaction()
        {
            
            waterDropper.gameObject.SetActive(false);
            media.SetActive(false);
            media2.GetComponent<Rigidbody>().freezeRotation = true;
            media2.GetComponent<ConstantForce>().enabled = false;
            Debug.Log("Interaction for Step 4");
        }

        private void Step5Interaction()
        {
            NextButton.SetActive(false);
            Bowl.SetActive(false);
            EmptyBeaker.SetActive(true);
            MovableBowl.SetActive(true);
            BowlDropZone.SetActive(true);
            Debug.Log("Interaction for Step 5");
        }

        private void Step6Interaction() 
        {
            NextButton.SetActive(true);
            sandDropper.gameObject.SetActive(false);
            Debug.Log("Interaction for Step 6");
        }
        private void Step7Interaction()
        { 
            Kettle.SetActive(true); 
            Kettle.GetComponent<HighlightOnClick>().enabled = true;
            Kettle.GetComponent<HighlightOnClick>().IsHighlighted = true;
            Kettle.GetComponent<MeshRenderer>().material = Highlight;
            NextButton.SetActive(false);
            Debug.Log("Interaction for Step 7");
        }
        private void Step8Interaction()
        { 
            SandBeaker.GetComponent<DragAndDrop>().enabled = true; 
            SandZone.SetActive(true); 
            Debug.Log("Interaction for Step 8");
        }
        private void Step9Interaction() 
        { 
            kettleDropper.gameObject.SetActive(false);
            Fire.SetActive(true); 
            Debug.Log("Interaction for Step 9"); 
        }
        private void Step15Interaction()
        {
            Debug.Log("Interaction for Step 15");
        }

        private void Step14Interaction()
        {
            Debug.Log("Interaction for Step 14");
            vapour.SetActive(false);
            waterInBeaker.SetActive(false);
            Sediment.SetActive(true);
            Fire.SetActive(false);
            FireBall.SetActive(false);
            beaker2Water.SetActive(false);
            waterdroplets.SetActive(false);
            NextButton.SetActive(true);
        }

        private void Step13Interaction()
        {
            Debug.Log("Interaction for Step 13");
            NextButton.SetActive(true);
                   
           
        }

        private void Step12Interaction()
        {
            Debug.Log("Interaction for Step 12");
            MetalPlateDropper.SetActive(true);
            Beaker2.SetActive(true);
            Beaker2.GetComponent<MeshRenderer>().material = Highlight;
            Beaker2.GetComponent<HighlightOnClick>().enabled = true;
            Beaker2.GetComponent<HighlightOnClick>().IsHighlighted2 = true;
            
            //Beaker2.GetComponent<HighlightOnClick>().IsHighlighted = true;



        }

        private void Step11Interaction()
        {
            Debug.Log("Interaction for Step 11");
            MetalPlateDrop.SetActive(true);
        }

        private void Step10Interaction()
        {
            MetalPlate.gameObject.SetActive(true);
            MetalPlate.GetComponent<HighlightOnClick>().enabled = true;
            MetalPlate.GetComponent<HighlightOnClick>().IsHighlighted = true;
            MetalPlate.GetComponent<MeshRenderer>().material = Highlight;
            Debug.Log("Interaction for Step 10");
        }
        
        public void watervapour()
        {
            Invoke("WaterVapourNext", 5f);
        }
        private void WaterVapourNext()
        {
            vapour.SetActive(true);
            Invoke("NextStepManually", 2f);
        }
        
        public void ReloadScene()
        { 
            Scene currentScene = SceneManager.GetActiveScene(); SceneManager.LoadScene(currentScene.name); 
        }
        public void PauseGame() 
        { 
            Time.timeScale = 0f; Debug.Log("Game Paused"); 
        }
        public void ResumeGame() 
        { 
            Time.timeScale = 1f; Debug.Log("Game Resumed");
        }
        public void QuitGame() 
        {
            Debug.Log("Quitting Game..."); Application.Quit();
        }

        public void NextStepManually()
        {
            if (currentStep < steps.Count - 1)
            {
                currentStep++;
                LoadStep(currentStep);
            }
        }
        
    }
}
