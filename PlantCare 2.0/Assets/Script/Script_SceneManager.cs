using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_SceneManager : MonoBehaviour
{
    public void meinePflanzen()
    {
        SceneManager.LoadScene("MeinePflanzen");
    }

    public void pflanzeFinden()
    {
        SceneManager.LoadScene("PflanzeFinden");
    }

    public void kalender()
    {
        SceneManager.LoadScene("Kalender");
    }

    public void allgemeineTipps()
    {
        SceneManager.LoadScene("AllgemeineTipps");
    }

    public void errungenschaften()
    {
        SceneManager.LoadScene("Errungenschaften");
    }

    public void pflanzeHinzufügen()
    {
        SceneManager.LoadScene("PflanzeHinzufügen");
    }
}