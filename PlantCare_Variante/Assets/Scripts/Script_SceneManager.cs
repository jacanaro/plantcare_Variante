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

    public void pflanzenProfil()
    {
        SceneManager.LoadScene("Pflanzenprofil");
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

    public void Hilfedialog(){
        SceneManager.LoadScene("Hilfedialog");
    }

    public void errungenschaften()
    {
        SceneManager.LoadScene("Erfolge");
    }

    public void pflanzeHinzufugen()
    {
        SceneManager.LoadScene("PflanzeHinzufuegen");
    }

    public void pflanzenInfos()
    {
        SceneManager.LoadScene("Pflanzeninfos");
    }

    public void einstellungen()
    {
        SceneManager.LoadScene("Einstellungen");
    }

    public void infosGiessen()
    {
        SceneManager.LoadScene("InfosGiessen");
    }

    public void infosUmtopfen()
    {
        SceneManager.LoadScene("InfosUmtopfen");
    }

    public void infosDuengen()
    {
        SceneManager.LoadScene("InfosDuengen");
    }
}