using UnityEngine;

namespace Neur
{
    public class DragAndDrop : MonoBehaviour
    {
        private Vector3 offset;
        private float zCoord;

        public bool isDragging = false;
       // public GameObject WaterDropper;

        // Called when the mouse button is pressed down on the object
        void OnMouseDown()
        {
            zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            offset = gameObject.transform.position - GetMouseWorldPos();
            isDragging = true;  // Start dragging
        }

        // Called every frame while the object is being dragged
        void OnMouseDrag()
        {
            if (isDragging)
            {
                transform.position = GetMouseWorldPos() + offset;
            }
        }

        // Called when the mouse button is released
        void OnMouseUp()
        {
            isDragging = false;  // Stop dragging
        }

        // Get the mouse position in world coordinates
        private Vector3 GetMouseWorldPos()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = zCoord;
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }

        // Detects when the draggable object enters a trigger zone
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("DropZone"))
            {
                Debug.Log("Entered Drop Zone: " + other.name);
                StepManager.instance.waterDropper.SetActive(true);
                StepManager.instance.media.GetComponent<ConstantForce>().enabled = true;
                StepManager.instance.media2.GetComponent<ConstantForce>().enabled = true;

                //StepManager.instance.Coroutiine(2f);
                this.gameObject.SetActive(false);
                other.gameObject.SetActive(false);
                
                // Add custom logic here when the object enters the trigger zone
                // For example, you can snap the object to the drop zone or trigger other actions
            }
            else if (other.CompareTag("BowlDrop"))
            {
                StepManager.instance.EmptyBeaker.SetActive(false);
                StepManager.instance.SandBeaker.SetActive(true);
                this.gameObject.SetActive(false);
                other.gameObject.SetActive(false);
            }
        }

        // Optional: Detect when the object exits the trigger zone
        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("DropZone"))
            {
                Debug.Log("Exited Drop Zone: " + other.name);
                // Add custom logic here when the object leaves the trigger zone
            }
        }
    }
}

