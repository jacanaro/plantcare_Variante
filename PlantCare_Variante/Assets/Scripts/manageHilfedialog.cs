using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class manageHilfedialog : MonoBehaviour
{
    public GameObject Fenster1;
    public GameObject Fenster21;
    public GameObject Fenster22;
    public GameObject Fenster23;
    public GameObject Fenster24;
    
    public void option1() {
        Fenster1.SetActive(false);
        Fenster21.SetActive(true);
    }

    public void option2() {
        Fenster1.SetActive(false);
        Fenster22.SetActive(true);
    }

    public void option3() {
        Fenster1.SetActive(false);
        Fenster23.SetActive(true);
    }

    public void option4() {
        Fenster1.SetActive(false);
        Fenster24.SetActive(true);
    }

    public void manageBackButton() {
        if (Fenster1.active) SceneManager.LoadScene("Pflanzenprofil");
        if (Fenster21.active || Fenster22.active || Fenster23.active || Fenster24.active) SceneManager.LoadScene("Hilfedialog");
    }

    public void setHappySmileyAndReturnToProfile() {
        Script_Erfolge.erf4Count++;
        PlayerPrefs.SetString("gesundheit", "gut");
        SceneManager.LoadScene("Pflanzenprofil");
    }
}