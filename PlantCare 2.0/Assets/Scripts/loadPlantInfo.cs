using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class loadPlantInfo : MonoBehaviour
{
    private string dbName = "URI=file:Plants.db";
    private string pflanzenName="unbekannt";
    private string generalInfo="unbekannt";

    public void loadInfo(){
        int plantID=PlayerPrefs.GetInt("plantID");

        //create the db connection
        using (var connection = new SqliteConnection(dbName)) {
            connection.Open();

            // set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand()) {
                
                //get generalInfo
                command.CommandText = "SELECT name, generalInfo FROM publicPlants,userPlants WHERE publicPlants.latName=userPlants.latName AND plantID='" + plantID + "';";
                
                using (IDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        pflanzenName= ""+reader["name"];
                        generalInfo= ""+reader["generalInfo"];
                        PlayerPrefs.SetString("name", pflanzenName);
                        PlayerPrefs.SetString("generalInfo", generalInfo);
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }

        SceneManager.LoadScene("InfoPflanzenprofil");

    }
}
