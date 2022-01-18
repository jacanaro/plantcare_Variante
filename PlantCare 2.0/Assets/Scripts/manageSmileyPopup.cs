using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manageSmileyPopup : MonoBehaviour
{
    public GameObject Popup;
    public GameObject smileyHappy;
    public GameObject smileySad;

    void Update(){
        if(PlayerPrefs.GetString("gesundheit").Equals("gut")){
            smileyHappy.SetActive(true);
            smileySad.SetActive(false);
        }
        else if(PlayerPrefs.GetString("gesundheit").Equals("schlecht"))
        {
            smileyHappy.SetActive(false);
            smileySad.SetActive(true);
        }
    }

    public void openHilfedialog(){
        SceneManager.LoadScene("Hilfedialog");
        Popup.SetActive(false);
        PlayerPrefs.SetString("gesundheit", "schlecht");
    }

    public void closeSmileyPopup(){
        PlayerPrefs.SetString("gesundheit", "gut");
        Popup.SetActive(false);
    }

    public void openSmileyPopup(){
        Popup.SetActive(true);
    }
}
