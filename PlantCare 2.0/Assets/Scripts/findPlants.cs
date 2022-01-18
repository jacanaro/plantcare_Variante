using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System;

/*NICHT FERTIG: Logikproblem bei der verschachtelten Anfrage, welche Angaben wichtiger sind als andere, z.B. viel Sonne+Profi, und dann alle schwierigkeiten für mittel und viel sonne.... */

public class findPlants : MonoBehaviour {

    private string dbName = "URI=file:Plants.db";
    public Transform locationDropdown;
    public Transform sunDropdown;
    public Transform difficultyDropdown;
 
    public GameObject loading;
    public GameObject match;
    public GameObject noMatch;
    public GameObject results;
    private int checkMatch;

    public void findPlant() {
        
        checkMatch = 0;
        
        results.SetActive(false);
        //loading.SetActive(true);
        match.SetActive(false);
        noMatch.SetActive(false);

        Debug.Log("Suche gestartet");

        //checken was ausgewählt ist
        int indexLocation = locationDropdown.GetComponent<Dropdown>().value; //menuIndex
        int indexSun = sunDropdown.GetComponent<Dropdown>().value;
        int indexDifficulty = difficultyDropdown.GetComponent<Dropdown>().value;

        List<Dropdown.OptionData> optionsLocation = locationDropdown.GetComponent<Dropdown>().options;
        List<Dropdown.OptionData> optionsSun = sunDropdown.GetComponent<Dropdown>().options; 
        List<Dropdown.OptionData> optionsDifficulty = difficultyDropdown.GetComponent<Dropdown>().options;
 
        //value ist die jeweils ausgewählte option
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
                        command.CommandText = "SELECT name, amountOfSunNeeded, difficultyLevel FROM publicPlants WHERE plantsOptimalLocation = '" + locationValue + "' AND (amountOfSunNeeded = '" + "viel" + "' OR amountOfSunNeeded = '" + "mittel" + "') AND difficultyLevel = '" + "leicht" + "';";
                    } else if (sunValue == "wenig") {
                        command.CommandText = "SELECT name, amountOfSunNeeded, difficultyLevel FROM publicPlants WHERE plantsOptimalLocation = '" + locationValue + "' AND (amountOfSunNeeded = '" + "wenig" + "' OR amountOfSunNeeded = '" + "mittel" + "') AND difficultyLevel = '" + "leicht" + "';";
                    } else {
                        command.CommandText = "SELECT name, amountOfSunNeeded, difficultyLevel FROM publicPlants WHERE plantsOptimalLocation = '" + locationValue + "' AND difficultyLevel = '" + "leicht" + "';";
                    } 
                } else {
                    if (sunValue == "viel") {
                        command.CommandText = "SELECT name, amountOfSunNeeded, difficultyLevel FROM publicPlants WHERE plantsOptimalLocation = '" + locationValue + "' AND (amountOfSunNeeded = '" + "viel" + "' OR amountOfSunNeeded = '" + "mittel" + "') AND (difficultyLevel = '" + "leicht"  + "' OR difficultyLevel = '" + "mittel" + "');";
                    } else if (sunValue == "wenig") {
                        command.CommandText = "SELECT name, amountOfSunNeeded, difficultyLevel FROM publicPlants WHERE plantsOptimalLocation = '" + locationValue + "' AND (amountOfSunNeeded = '" + "wenig" + "' OR amountOfSunNeeded = '" + "mittel" + "') AND (difficultyLevel = '" + "leicht"  + "' OR difficultyLevel = '" + "mittel" + "');";
                    } else {
                        command.CommandText = "SELECT name, amountOfSunNeeded, difficultyLevel FROM publicPlants WHERE plantsOptimalLocation = '" + locationValue + "'AND (difficultyLevel = '" + "leicht"  + "' OR difficultyLevel = '" + "mittel" + "');";
                    } 
                }

                /*
                if (sunValue == "viel") {     
                    command.CommandText = "SELECT name, amountOfSunNeeded, difficultyLevel FROM publicPlants WHERE plantsOptimalLocation = '" + locationValue + "'AND (amountOfSunNeeded = '" + "viel" + "' OR amountOfSunNeeded = '" + "mittel" + "') AND difficultyLevel = '" + difficultyValue + "';";
                } else if (sunValue == "wenig") {
                    command.CommandText = "SELECT name, amountOfSunNeeded, difficultyLevel FROM publicPlants WHERE plantsOptimalLocation = '" + locationValue + "'AND (amountOfSunNeeded = '" + "wenig" + "' OR amountOfSunNeeded = '" + "mittel" + "') AND difficultyLevel = '" + difficultyValue + "';";
                } 
                else {
                    command.CommandText = "SELECT name, amountOfSunNeeded, difficultyLevel FROM publicPlants WHERE plantsOptimalLocation = '" + locationValue + "'AND amountOfSunNeeded = '" + sunValue + "' AND difficultyLevel = '" + difficultyValue + "';";
                }
                */

                using (IDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Debug.Log("\nName: " + reader["name"]);
                        Debug.Log("\nBraucht " + reader["amountOfSunNeeded"] + " Sonne");
                        Debug.Log("\nSchwierigkeit: " + reader["difficultyLevel"]);
                        checkMatch++;
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


}