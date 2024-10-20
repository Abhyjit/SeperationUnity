using Neur;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightKettle : MonoBehaviour
{
    public Material originalColor;
    public Material highlightColor;

    private Renderer objectRenderer;
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
            StepManager.instance.NextButton.SetActive(true);
        }
    }
}
