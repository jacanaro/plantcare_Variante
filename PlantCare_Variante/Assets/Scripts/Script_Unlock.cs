using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Unlock : MonoBehaviour
{
    public GameObject LockScreen01;
    public GameObject LockScreen02;
    public GameObject LockScreen03;
    public GameObject LockScreen04;
    public GameObject LockScreen05;

    // Update is called once per frame
    void Update()
    {
        if(Script_Erfolge.erf1Code != 0)
        {
            LockScreen01.SetActive(false);
        }

        Debug.Log(Script_Erfolge.erf2Code);
        if (Script_Erfolge.erf2Code != 0)
        {
            LockScreen02.SetActive(false);
        }

        if (Script_Erfolge.erf3Code != 0)
        {
            LockScreen03.SetActive(false);
        }

        if (Script_Erfolge.erf4Code != 0)
        {
            LockScreen04.SetActive(false);
        }

        if (Script_Erfolge.erf5Code != 0)
        {
            LockScreen05.SetActive(false);
        }
    }
}
