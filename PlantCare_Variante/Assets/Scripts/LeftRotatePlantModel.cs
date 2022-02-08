using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftRotatePlantModel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Camera camera;
    public GameObject target;
    private float rotateSpeed = 10f;
    bool rotateRight = false;
    bool rotateLeft = false;

    // Update is called once per frame
    void Update()
    {
        if (rotateRight)
        {
            camera.transform.RotateAround(target.transform.position, -Vector3.down, 100 * Time.deltaTime);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rotateRight = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rotateRight = false;
    }
}