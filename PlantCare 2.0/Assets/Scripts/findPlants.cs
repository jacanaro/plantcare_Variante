using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System;
using TMPro;

public class findPlants : MonoBehaviour {

    private string dbName = "URI=file:Plants.db";
    public Transform locationDropdown;
    public Transform sunDropdown;
    public Transform difficultyDropdown;
    
    public RawImage plantImage;
    public RawImage plantImage2;
    public Texture[] imageTextures = new Texture[3];
    public TextMeshProUGUI  name;
    public TextMeshProUGUI  sonnenbedarf;
    public TextMeshProUGUI  schwierigkeit;
     public TextMeshProUGUI  name2;
    public TextMeshProUGUI  sonnenbedarf2;
    public TextMeshProUGUI  schwierigkeit2;
    public GameObject loading;
    public GameObject match;
    public GameObject match2;
    public GameObject noMatch;
    public GameObject results;
    public GameObject btn1;
    public GameObject btn2;

    private int checkMatch;

    public void findPlant() {
        
        checkMatch = 0;
        
        results.SetActive(false);
        //loading.SetActive(true);
        match.SetActive(false);
        noMatch.SetActive(false);

        Debug.Log("Suche gestartet");

        //checken was ausgewaehlt ist
        int indexLocation = locationDropdown.GetComponent<Dropdown>().value; //menuIndex
        int indexSun = sunDropdown.GetComponent<Dropdown>().value;
        int indexDifficulty = difficultyDropdown.GetComponent<Dropdown>().value;

        List<Dropdown.OptionData> optionsLocation = locationDropdown.GetComponent<Dropdown>().options;
        List<Dropdown.OptionData> optionsSun = sunDropdown.GetComponent<Dropdown>().options; 
        List<Dropdown.OptionData> optionsDifficulty = difficultyDropdown.GetComponent<Dropdown>().options;
 
        //value ist die jeweils ausgewaehlte option
        string locationValue = optionsLocation[indexLocation].text;
        string sunValue = optionsSun[indexSun].text;
        string difficultyValue = optionsDifficulty[indexDifficulty].text;

        using (var connection = new SqliteConnection(dbName)) {
            connection.Open();

            using (var command = connection.CreateCommand()) {     

                if (difficultyValue == "ein Vollprofi") {
                    if (sunValue == "viel") {
                        command.CommandText = "SELECT name, amountOfSunNeeded, difficultyLevel FROM publicPlants WHERE plantsOptimalLocation = '" + locationValue + "' AND (amountOfSunNeeded = '" + "viel" + "' OR amountOfSunNeeded = '" + "mittel" + "');";
                    } else if (sunValue == "wenig") {
                        command.CommandText = "SELECT name, amountOfSunNeeded, difficultyLevel FROM publicPlants WHERE plantsOptimalLocation = '" + locationValue + "' AND (amountOfSunNeeded = '" + "wenig" + "' OR amountOfSunNeeded = '" + "mittel" + "');";
                    } else {
                        command.CommandText = "SELECT name, amountOfSunNeeded, difficultyLevel FROM publicPlants WHERE plantsOptimalLocation = '" + locationValue + "';";
                    } 
                } else if (difficultyValue == "eine Niete") {
                     if (sunValue == "viel") {
                        command.CommandText = "SELECT name, amountOfSunNeeded, difficultyLevel FROM publicPlants WHERE plantsOptimalLocation = '" + locationValue + "' AND (amountOfSunNeeded = '" + "viel" + "' OR amountOfSunNeeded = '" + "mittel" + "') AND difficultyLevel = '" + "einfach" + "';";
                    } else if (sunValue == "wenig") {
                        command.CommandText = "SELECT name, amountOfSunNeeded, difficultyLevel FROM publicPlants WHERE plantsOptimalLocation = '" + locationValue + "' AND (amountOfSunNeeded = '" + "wenig" + "' OR amountOfSunNeeded = '" + "mittel" + "') AND difficultyLevel = '" + "einfach" + "';";
                    } else {
                        command.CommandText = "SELECT name, amountOfSunNeeded, difficultyLevel FROM publicPlants WHERE plantsOptimalLocation = '" + locationValue + "' AND difficultyLevel = '" + "einfach" + "';";
                    } 
                } else {
                    if (sunValue == "viel") {
                        command.CommandText = "SELECT name, amountOfSunNeeded, difficultyLevel FROM publicPlants WHERE plantsOptimalLocation = '" + locationValue + "' AND (amountOfSunNeeded = '" + "viel" + "' OR amountOfSunNeeded = '" + "mittel" + "') AND (difficultyLevel = '" + "einfach"  + "' OR difficultyLevel = '" + "mittel" + "');";
                    } else if (sunValue == "wenig") {
                        command.CommandText = "SELECT name, amountOfSunNeeded, difficultyLevel FROM publicPlants WHERE plantsOptimalLocation = '" + locationValue + "' AND (amountOfSunNeeded = '" + "wenig" + "' OR amountOfSunNeeded = '" + "mittel" + "') AND (difficultyLevel = '" + "einfach"  + "' OR difficultyLevel = '" + "mittel" + "');";
                    } else {
                        command.CommandText = "SELECT name, amountOfSunNeeded, difficultyLevel FROM publicPlants WHERE plantsOptimalLocation = '" + locationValue + "'AND (difficultyLevel = '" + "einfach"  + "' OR difficultyLevel = '" + "mittel" + "');";
                    } 
                }

                using (IDataReader reader = command.ExecuteReader()) {
                    int i=0;
                    while (reader.Read()) {
                        if(i==0){
                            Debug.Log("\nName: " + reader["name"]);
                            Debug.Log("\nBraucht " + reader["amountOfSunNeeded"] + " Sonne");
                            Debug.Log("\nSchwierigkeit: " + reader["difficultyLevel"]);
                            checkMatch++;
                            //plantImage.texture =
                            name.text = "" + reader["name"]; 
                            sonnenbedarf.text = "Braucht " + reader["amountOfSunNeeded"] + " Sonne";
                            schwierigkeit.text = "Schwierigkeit: " + reader["difficultyLevel"];
                            
                            foreach (Texture x in imageTextures)
                            {
                                if (x.name.Equals(reader["name"]))
                                {
                                    plantImage.texture = x;
                                }
                            }
                            i++;
                        }else{
                            btn1.SetActive(true);
                            btn2.SetActive(true);
                            Debug.Log("\nName: " + reader["name"]);
                            Debug.Log("\nBraucht " + reader["amountOfSunNeeded"] + " Sonne");
                            Debug.Log("\nSchwierigkeit: " + reader["difficultyLevel"]);
                            checkMatch++;
                            //plantImage.texture =
                            name2.text = "" + reader["name"]; 
                            sonnenbedarf2.text = "Braucht " + reader["amountOfSunNeeded"] + " Sonne";
                            schwierigkeit2.text = "Schwierigkeit: " + reader["difficultyLevel"];
                            
                            foreach (Texture x in imageTextures)
                            {
                                if (x.name.Equals(reader["name"]))
                                {
                                    plantImage2.texture = x;
                                }
                            }
                        }
                    }
                    reader.Close();

                    if (checkMatch != 0) { 
                        results.SetActive(true);
                        loading.SetActive(false);
                        match.SetActive(true);
                        noMatch.SetActive(false);
                    } else {
                        results.SetActive(true);
                        loading.SetActive(false);
                        match.SetActive(false);
                        noMatch.SetActive(true);
                    }
                }
            }
            connection.Close();
        }
    }

    public void switchToSecondResult(){
        match.SetActive(false);
        match2.SetActive(true);
    }
    
    public void switchToFirstResult(){
        match.SetActive(true);
        match2.SetActive(false);
    }
}