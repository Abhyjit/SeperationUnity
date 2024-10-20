using UnityEngine;

namespace Neur
{
    public class DragAndDrop : MonoBehaviour
    {
        private Vector3 offset;
        private float zCoord;

        public bool isDragging = false;
       

        
        void OnMouseDown()
        {
            zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            offset = gameObject.transform.position - GetMouseWorldPos();
            isDragging = true;  
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
            isDragging = false;  
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
                StepManager.instance.sandDropper.SetActive(true);
                StepManager.instance.SandBeaker.SetActive(true);
                StepManager.instance.SOundaudioSource.clip = StepManager.instance.WaterPouring;
                StepManager.instance.SOundaudioSource.Play();
                this.gameObject.SetActive(false);
                other.gameObject.SetActive(false);
                StepManager.instance.Invoke("NextStepManually", 5f);
            }
            else if (other.CompareTag("Sand"))
            {
                this.gameObject.SetActive(false);
                other.gameObject.SetActive(false) ;
               
                StepManager.instance.kettleDropper.SetActive(true);
                StepManager.instance.SOundaudioSource.clip = StepManager.instance.WaterPouring;
                StepManager.instance.SOundaudioSource.Play();
                StepManager.instance.Invoke("NextStepManually", 5f);
            }
            else if (other.CompareTag("Plate"))
            {
                this.gameObject.SetActive(false);
                other.gameObject.SetActive(false);
                StepManager.instance.NextStepManually();


            }
            else if (other.CompareTag("Beaker2"))
            {
                this.gameObject.SetActive(false);
                other.gameObject.SetActive(false);
                StepManager.instance.beaker2Water.SetActive(true);
                StepManager.instance.waterdroplets.SetActive(true);
                StepManager.instance.SOundaudioSource.clip = StepManager.instance.waterDrop;
                StepManager.instance.SOundaudioSource.Play();
                StepManager.instance.Invoke("NextStepManually", 5f);

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

