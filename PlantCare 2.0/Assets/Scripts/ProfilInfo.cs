using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System.Globalization;
using System;

public class ProfilInfo : MonoBehaviour
{
    public TMPro.TextMeshProUGUI title;
    public TMPro.TextMeshProUGUI info;
    private int plantID;
    private string dbName = "URI=file:Plants.db";

    void Start() {
        plantID = PlayerPrefs.GetInt("plantID");
        DateTime dateOfPlantsCreation;
        DateTime currentDate=DateTime.Now;

        //create the db connection
        using (var connection = new SqliteConnection(dbName)) {
            connection.Open();

            // set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand()) {
                //get generalInfo
                command.CommandText = "SELECT publicPlants.latName, difficultyLevel, yearOfCreation, monthOfCreation, dayOfCreation, fertilizeFrequencyInWeeks, pourFrequencyInDays, name, nickname FROM userPlants INNER JOIN publicPlants on publicPlants.latName = userPlants.latName WHERE plantID='" + plantID + "';";
                
                using (IDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        string termine = "";
                        dateOfPlantsCreation = new DateTime(reader.GetInt32(reader.GetOrdinal("yearOfCreation")),
                        reader.GetInt32(reader.GetOrdinal("monthOfCreation")), reader.GetInt32(reader.GetOrdinal("dayOfCreation")));
                                
                        try {
                            // Calculate number of days passed between the two dates.
                            TimeSpan interval = currentDate- dateOfPlantsCreation;

                            //get value in days
                            int fertilizeFrequencyInDays = 7*reader.GetInt32(reader.GetOrdinal("fertilizeFrequencyInWeeks"));

                            //check if fertilizing is requiered at current date
                            if(interval.Days % fertilizeFrequencyInDays == 0) {
                                termine += "düngen";
                            }
                            //check if pouring is requiered at current date
                            if(interval.Days % reader.GetInt32(reader.GetOrdinal("pourFrequencyInDays")) == 0) {
                                termine += " gießen";
                            }
                        } catch(Exception e) {
                            Debug.Log(e);
                        }
                        title.text = "" + reader["nickname"];
                        if(reader["latName"].Equals(reader["name"])){
                            info.text ="Name: " + "Wüstenblume" + "\n" + "Lat. Name: " +reader["latName"] + "\n" + "Schwierigkeitsgrad: " +reader["difficultyLevel"] + "\n" + "Termine heute: " + termine;
                        }
                        else{
                            info.text = "Name: " + reader["name"] + "\n" + "Lat. Name: " +reader["latName"] + "\n" + "Schwierigkeitsgrad: " +reader["difficultyLevel"] + "\n" + "Termine heute: " + termine;
                        }
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }
    }
}