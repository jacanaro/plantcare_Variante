using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class createUserPlant : MonoBehaviour
{
    private string dbName = "URI=file:Plants.db";
    //General Variables
    public GameObject erfNotif;
    public bool erfActive = false;
    public GameObject erfBild;
    public GameObject erfTitel;
    public GameObject erfBeschr;
    private bool erf1drin = false;

    //Erster Erfolg Spezifisch
    public static int erf1Count = 0;
    public int erf1Trigger = 2;
    public static int erf1Code = 0;

    public InputField nicknameInputfield;
    public Transform plantStageDropdown;
    public Text latNameText;

    public void addUsersPlant() {
        //get the selected index
        int menuIndex = plantStageDropdown.GetComponent<Dropdown> ().value;
 
        //get all options available within this dropdown menu
        List<Dropdown.OptionData> menuOptions = plantStageDropdown.GetComponent<Dropdown> ().options;
 
        //get the string value of the selected index
        string dropdownValue = menuOptions [menuIndex].text;

        using (var connection = new SqliteConnection(dbName)) {
            connection.Open();

            // set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand()) {
                //Get current date
                DateTime thisDay = DateTime.Today;
                int year = thisDay.Year;
                int month = thisDay.Month;
                int day = thisDay.Day;

                //check if plant with given name exists
                command.CommandText = "SELECT EXISTS(SELECT * FROM publicPlants WHERE latName='" + latNameText.text + "');";
                
                bool plantExists = false;

                using (IDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        if (reader.GetInt32(0) == 1) {
                            plantExists = true;                
                        } else {
                            Debug.Log("Pflanzenart nicht bekannt!");
                        }
                    }
                    reader.Close();
                }
                
                if (plantExists == true) {
                    //sql command for insertion
                    command.CommandText = "INSERT INTO userPlants (nickname, plantStage, latName, yearOfCreation, monthOfCreation, dayOfCreation) VALUES ('" + nicknameInputfield.text + "', '" + dropdownValue + "', '" + latNameText.text + "', '" + year + "', '" + month + "', '" + day + "' );";
                    command.ExecuteNonQuery(); //runs sql command
                    Debug.Log("Pflanze hinzugefügt: " + nicknameInputfield.text +  " " + dropdownValue + " " + latNameText.text+ " " + year + " " + month + " " + day);
                    erf1drin = true;
                    Script_Erfolge.erf1Count++;
                    SceneManager.LoadScene("MeinePflanzen");
                }
            }
            connection.Close();
        }
    }
}
