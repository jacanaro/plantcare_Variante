using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class managePlantModel : MonoBehaviour
{
    public GameObject modelWraper;
    public GameObject[] modelTextures = new GameObject[3];
    public Camera cam;

    void Start()
    {
        foreach (GameObject x in modelTextures)
        {
            if(x.name.Equals("Paprikamittel")){
                GameObject model = Instantiate(x);
                model.transform.parent=modelWraper.transform;
                model.transform.localPosition = new Vector3(-0.004f, -0.111f,-0.058f);
                model.transform.rotation= Quaternion.Euler(0f, 1.849f, 0f);
                model.transform.localScale= new Vector3(0.0387501f, 0.0387501f, 0.0387501f);
                Debug.Log(model.transform.position);
                Debug.Log(model.transform.rotation);
                Debug.Log(model.transform.localScale);
                Thread.Sleep(1000);
                cam.cullingMask=0;
                Thread.Sleep(1000);
                cam.cullingMask=-1;
            }
        }
    }
}
