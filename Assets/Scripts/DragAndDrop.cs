using UnityEngine;

namespace Neur
{
    public class DragAndDrop : MonoBehaviour
    {
        private Vector3 offset;
        private float zCoord;

        public bool isDragging = false;
       // public GameObject WaterDropper;

        
        void OnMouseDown()
        {
            zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            offset = gameObject.transform.position - GetMouseWorldPos();
            isDragging = true;  // Start dragging
        }

        
        void OnMouseDrag()
        {
            if (isDragging)
            {
                transform.position = GetMouseWorldPos() + offset;
            }
        }

      
        void OnMouseUp()
        {
            isDragging = false;  // Stop dragging
        }

        private Vector3 GetMouseWorldPos()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = zCoord;
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }

       
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("DropZone"))
            {
                Debug.Log("Entered Drop Zone: " + other.name);
                StepManager.instance.waterDropper.SetActive(true);
                StepManager.instance.media.GetComponent<ConstantForce>().enabled = true;
                StepManager.instance.media2.GetComponent<ConstantForce>().enabled = true;
                StepManager.instance.SOundaudioSource.clip = StepManager.instance.WaterPouring;
                StepManager.instance.SOundaudioSource.Play();
                //StepManager.instance.Coroutiine(2f);
                this.gameObject.SetActive(false);
                other.gameObject.SetActive(false);
                StepManager.instance.NextStepManually();

            }
            else if (other.CompareTag("BowlDrop"))
            {
                StepManager.instance.EmptyBeaker.SetActive(false);
                StepManager.instance.SandBeaker.SetActive(true);
                this.gameObject.SetActive(false);
                other.gameObject.SetActive(false);
                StepManager.instance.NextStepManually();
            }
            else if (other.CompareTag("Sand"))
            {
                this.gameObject.SetActive(false);
                other.gameObject.SetActive(false) ;
                StepManager.instance.NextStepManually();
            }
        }

        
        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("DropZone"))
            {
                Debug.Log("Exited Drop Zone: " + other.name);
              
            }
        }
    }
}

