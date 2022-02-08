using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageInfoPflanzenprofil : MonoBehaviour
{
    public GameObject title;
    public GameObject body;

    void Start(){
        TMPro.TextMeshProUGUI titleObject = title.GetComponentInChildren<TMPro.TextMeshProUGUI>();    
        TMPro.TextMeshProUGUI bodyObject = body.GetComponentInChildren<TMPro.TextMeshProUGUI>();

        titleObject.text=PlayerPrefs.GetString("name");
        bodyObject.text=PlayerPrefs.GetString("generalInfo");
    }
}