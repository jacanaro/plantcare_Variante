using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class UpdatePlant : MonoBehaviour
{
    private string dbName = "URI=file:Plants.db";
    public InputField nicknameInputfield;
    public Transform plantStageDropdown;
    public TMPro.TextMeshProUGUI responseText;
    [SerializeField] private InputField iField;

    public void updatePlantvalues(){
        //get the selected index
        int menuIndex = plantStageDropdown.GetComponent<Dropdown> ().value;
 
        //get all options available within this dropdown menu
        List<Dropdown.OptionData> menuOptions = plantStageDropdown.GetComponent<Dropdown> ().options;
 
        //get the string value of the selected index
        string dropdownValue = menuOptions [menuIndex].text;

        using (var connection = new SqliteConnection(dbName)){
            connection.Open();

            // set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand()){
                if(iField.text!=""){
                command.CommandText = "UPDATE userPlants SET nickname='" + nicknameInputfield.text + "', plantStage='" + dropdownValue + "' WHERE plantID='" + PlayerPrefs.GetInt("plantID") +"';";            
                }else{
                command.CommandText = "UPDATE userPlants SET plantStage='" + dropdownValue + "' WHERE plantID='" + PlayerPrefs.GetInt("plantID") +"';";
                }
                command.ExecuteNonQuery(); //runs sql command
            }
            connection.Close();
        }
        responseText.text="Erfolgreich!";
    }
}