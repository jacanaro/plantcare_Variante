using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotatePlantModel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Camera camera;
    private float rotateSpeed = 10f;
    bool rotate = false;

    private void Update()
    {
        if (rotate)
        {
            camera.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        rotate = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        rotate = false;
    }
}