using UnityEngine;

namespace Neur
{
   

    public class HighlightOnClick : MonoBehaviour
    {
        public Material originalColor;  
        public Material highlightColor;  

        private Renderer objectRenderer;
        public bool IsHighlighted = false;
        public bool IsHighlighted2 = false;
        //public GameObject once;

        void Start()
        {
           
            objectRenderer = GetComponent<Renderer>();

            
        }

        
        void OnMouseDown()
        {

            ResetHighlight();
          
        }

      
        void OnMouseUp()
        {
            //ResetHighlight();  
        }

       
        public void HighlightObject()
        {
            print("Called");
            if (objectRenderer != null)
            {
                objectRenderer.material = highlightColor;
                

                
            }
            

        }

        
        private void ResetHighlight()
        {
            if (objectRenderer != null)
            {
                objectRenderer.material = originalColor;
                if (IsHighlighted)
                {
                    IsHighlighted = false;
                    StepManager.instance.NextStepManually();
                }
                else if (IsHighlighted2)
                {
                    IsHighlighted2 = false;
                    StepManager.instance.BeakerDrop2.SetActive(true);
                }
                
            }
        }
    }


}
