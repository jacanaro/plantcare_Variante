using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Plant;
    private float rotateSpeed = 100f;
    bool rotate = false;

    private void FixedUpdate()
    {
        if (rotate == false)
            return;

        Plant.transform.Rotate(-Vector3.down * rotateSpeed * Time.deltaTime);
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