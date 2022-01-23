using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Script_Erfolge : MonoBehaviour
{
    //General Variables
    public GameObject erfNotif;
    public bool erfActive = false;
    public GameObject erfBild;
    public GameObject erfTitel;
    public GameObject erfBeschr;

    //Erster Erfolg Spezifisch (Eine Pflanze)
    public static int erf1Count = 1;
    public int erf1Trigger = 1;
    public static int erf1Code = 0;

    //Zweiter Erfolg Spezifisch (5 Pflanze)
    public int erf2Trigger = 5;
    public static int erf2Code = 0;

    //Dritter Erfolg Spezifisch (10 Pflanze)
    public int erf3Trigger = 10;
    public static int erf3Code = 0;

    //Vierter Erfolg Spezifisch (Pflanze gerettet)
    public static int erf4Count = 2;
    public int erf4Trigger = 1;
    public static int erf4Code = 0;

    //Fuenfter Erfolg Spezifisch (Pflanze umgetopft)
    public static int erf5Count = 0;
    public int erf5Trigger = 1;
    public static int erf5Code = 0;

    // Update is called once per frame
    void Update()
    {
        erf1Code = PlayerPrefs.GetInt("Erf1");
        if(erf1Count >= erf1Trigger && erf1Code != 12345)
        {
            StartCoroutine(Trigger01Erf());
        }

        erf2Code = PlayerPrefs.GetInt("Erf2");
        if(erf1Count >= erf2Trigger && erf2Code != 12345)
        {
            StartCoroutine(Trigger02Erf());
        }

        erf3Code = PlayerPrefs.GetInt("Erf3");
        if (erf1Count >= erf3Trigger && erf3Code != 12345)
        {
            StartCoroutine(Trigger03Erf());
        }

        erf4Code = PlayerPrefs.GetInt("Erf4");
        if (erf4Count == erf4Trigger && erf4Code != 12345)
        {
            StartCoroutine(Trigger04Erf());
        }

        erf5Code = PlayerPrefs.GetInt("Erf5");
        if (erf5Count == erf5Trigger && erf5Code != 12345)
        {
            StartCoroutine(Trigger05Erf());
        }
    }

    //ERFOLG 01
    IEnumerator Trigger01Erf()
    {
        yield return new WaitForSeconds(1);
        erfActive = true;
        erfNotif.SetActive(true);
        erf1Code = 12345;
        PlayerPrefs.SetInt("Erf1", erf1Code);
        erfBild.SetActive(true);
        erfTitel.GetComponent<TextMeshProUGUI>().text = "Erste Pflanze!";
        erfBeschr.GetComponent<TextMeshProUGUI>().text = "Erfolgreich erste Pflanze hinzugefügt";
        yield return new WaitForSeconds(5);

        //Resetting UI
        erfNotif.SetActive(false);
        erfBild.SetActive(false);
        erfTitel.GetComponent<TextMeshProUGUI>().text = "";
        erfBeschr.GetComponent<TextMeshProUGUI>().text = "";
        erfActive = false;
    }

    //ERFOLG 02
    IEnumerator Trigger02Erf()
    {
        yield return new WaitForSeconds(1);
        erfActive = true;
        erfNotif.SetActive(true);
        erf2Code = 12345;
        PlayerPrefs.SetInt("Erf2", erf2Code);
        erfBild.SetActive(true);
        erfTitel.GetComponent<TextMeshProUGUI>().text = "Fünf Pflanzen!";
        erfBeschr.GetComponent<TextMeshProUGUI>().text = "Fünf Pflanzen hinzugefügt";
        yield return new WaitForSeconds(5);

        //Resetting UI
        erfNotif.SetActive(false);
        erfBild.SetActive(false);
        erfTitel.GetComponent<TextMeshProUGUI>().text = "";
        erfBeschr.GetComponent<TextMeshProUGUI>().text = "";
        erfActive = false;
    }

    //ERFOLG 03
    IEnumerator Trigger03Erf()
    {
        yield return new WaitForSeconds(1);
        erfActive = true;
        erfNotif.SetActive(true);
        erf3Code = 12345;
        PlayerPrefs.SetInt("Erf3", erf3Code);
        erfBild.SetActive(true);
        erfTitel.GetComponent<TextMeshProUGUI>().text = "Zehn Pflanzen";
        erfBeschr.GetComponent<TextMeshProUGUI>().text = "Zehn Pflanzen hinzugefügt";
        yield return new WaitForSeconds(5);

        //Resetting UI
        erfNotif.SetActive(false);
        erfBild.SetActive(false);
        erfTitel.GetComponent<TextMeshProUGUI>().text = "";
        erfBeschr.GetComponent<TextMeshProUGUI>().text = "";
        erfActive = false;
    }

    //ERFOLG 04
    IEnumerator Trigger04Erf()
    {
        yield return new WaitForSeconds(2);
        erfActive = true;
        erfNotif.SetActive(true);
        erf4Code = 12345;
        PlayerPrefs.SetInt("Erf4", erf4Code);
        erfBild.SetActive(true);
        erfTitel.GetComponent<TextMeshProUGUI>().text = "Pflanze gerettet";
        erfBeschr.GetComponent<TextMeshProUGUI>().text = "Eine Pflanze gerettet";
        yield return new WaitForSeconds(5);

        //Resetting UI
        erfNotif.SetActive(false);
        erfBild.SetActive(false);
        erfTitel.GetComponent<TextMeshProUGUI>().text = "";
        erfBeschr.GetComponent<TextMeshProUGUI>().text = "";
        erfActive = false;
    }

    //ERFOLG 05
    IEnumerator Trigger05Erf()
    {
        yield return new WaitForSeconds(2);
        erfActive = true;
        erfNotif.SetActive(true);
        erf5Code = 12345;
        PlayerPrefs.SetInt("Erf5", erf5Code);
        erfBild.SetActive(true);
        erfTitel.GetComponent<TextMeshProUGUI>().text = "Pflanze umgetopft";
        erfBeschr.GetComponent<TextMeshProUGUI>().text = "Eine Pflanze umgetopft";
        yield return new WaitForSeconds(5);

        //Resetting UI
        erfNotif.SetActive(false);
        erfBild.SetActive(false);
        erfTitel.GetComponent<TextMeshProUGUI>().text = "";
        erfBeschr.GetComponent<TextMeshProUGUI>().text = "";
        erfActive = false;
    }
}