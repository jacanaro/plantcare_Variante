using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_BtnAnim: MonoBehaviour
{
    public GameObject Panel;

    public void OpenPanel()
    {
        if(Panel != null)
        {
            Animator animator = Panel.GetComponent<Animator>();
            if(animator != null)
            {
                bool isOpen = animator.GetBool("open");

                animator.SetBool("open", !isOpen);
            }
        }

        /*Debug.Log("Before: " + Script_Erfolge.erf1Count);
        Script_Erfolge.erf1Count += 1;
        Debug.Log("After: " + Script_Erfolge.erf1Count);*/
    }
}
