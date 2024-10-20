using UnityEngine;

namespace Neur
{
   

    public class HighlightOnClick : MonoBehaviour
    {
        private Color originalColor;  // Stores the original color of the object
        public Material highlightColor;  // The color to highlight the object

        private Renderer objectRenderer;
        //public GameObject once;

        void Start()
        {
            // Get the Renderer component to change the object's color
            objectRenderer = GetComponent<Renderer>();

            
        }

        // Called when the mouse button is pressed down on the object
        void OnMouseDown()
        {
            ResetHighlight();
          
        }

        // Called when the mouse button is released
        void OnMouseUp()
        {
            //ResetHighlight();  // Reset the object color when released
        }

        // Highlights the GameObject by changing its color
        public void HighlightObject()
        {
            print("Called");
            if (objectRenderer != null)
            {
                objectRenderer.material = highlightColor;
                

                
            }
            

        }

        // Resets the object's color back to the original color
        private void ResetHighlight()
        {
            if (objectRenderer != null)
            {
                objectRenderer.material.color = originalColor;
                StepManager.instance.NextButton.SetActive(true);
            }
        }
    }


}
