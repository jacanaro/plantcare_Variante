using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class lookUpPlantInfoFromDB : MonoBehaviour
{
    // name the db -> Plants
    // and set location of db
    private string dbName = "URI=file:Plants.db";
        //createDB and tables
        //CreateDB();

        //adds the plant with parameters everytime the skript is started 
        /*AddPlant(
            "Tomate",
            "Solanum lycopersicum",
            "Nach dem Guinness-Buch der Rekorde wurde die groesste Tomate 1986 in Oklahoma mit einem Gewicht von 3,5 kg (7,7lbs) registriert. Aufgrund der großen Blattmasse haben Tomaten einen hohen Wasserbedarf, deshalb regelmäßig und ausreichend gießen. Eine gleichmäßige Wasserversorgung vermindert außerdem die Gefahr, dass die Früchte – von diesbezüglich empfindlichen Sorten – platzen. Tomatenpflanzen nie von oben beregnen, sondern das Gießwasser am Fuß der Pflanze ganz gezielt direkt auf den Boden ausbringen. Dazu eine Kanne ohne Gießbrause verwenden. Die Blätter müssen möglichst trocken bleiben, um eine Infektion mit Braunfäule (eine pilzliche Erkrankung, die zu großen Schäden bei Tomaten führen kann) zu verhindern bzw. zu erschweren.",
            "viel",
            "mittel",
            1,
            2,
            "drinnen"
        );*/

    public InputField iField;
    public Text latNameText;
    public RawImage plantImage;
    public Texture[] imageTextures = new Texture[3];

    public void lookUpPlantByName() {        
        //get use input plantname
        string userInput = iField.text;
        
        //look for plant image texture by plantname        
        foreach (Texture x in imageTextures)
        {
            if (x.name.Equals(userInput))
            {
                plantImage.texture = x;
            }
        }
            
        //create the db connection
        using (var connection = new SqliteConnection(dbName)) {
            connection.Open();
            // set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand()) {
                //get generalInfo
                command.CommandText = "SELECT latName,generalInfo FROM publicPlants WHERE name='" + userInput + "';";
                using (IDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        latNameText.text = "" + reader["latName"];
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }
    }

    public void CreateDB() {
        //create the db connection
        using (var connection = new SqliteConnection(dbName)) {
            connection.Open();
            // set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand()) {
                //create a table called publicPlants, if it doesnt exist already
                //it has  fields: name (up to 20 chars), latName, generalInfo, amountOfSunNeeded, difficultyLevel, pourFrequencyInDays, fertilizeFrequencyInWeeks, plantsOptimalLocation
                command.CommandText = "CREATE TABLE IF NOT EXISTS publicPlants (name VARCHAR(20), latName VARCHAR(20), generalInfo TEXT, amountOfSunNeeded VARCHAR(20), difficultyLevel VARCHAR(20), pourFrequencyInDays INT, fertilizeFrequencyInWeeks INT,  plantsOptimalLocation VARCHAR(20));";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void DeleteTableIFExists() {
        //create the db connection
        using (var connection = new SqliteConnection(dbName)) {
            connection.Open();
            // set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand()) {
                //create a table called publicPlants, if it doesnt exist already
                //it has  fields: name (up to 20 chars), latName, generalInfo, amountOfSunNeeded, difficultyLevel, pourFrequencyInDays, fertilizeFrequencyInWeeks, plantsOptimalLocation
                command.CommandText = "DROP TABLE IF EXISTS publicPlants;";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void AddPlant(string name, string latName, string generalInfo, string amountOfSunNeeded, string difficultyLevel, int pourFrequencyInDays, int fertilizeFrequencyInWeeks, string plantsOptimalLocation) {
        using (var connection = new SqliteConnection(dbName)) {
            connection.Open();
            // set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand()) {
                //sql command for insertion
                command.CommandText = "INSERT INTO publicPlants (name, latName, generalInfo, amountOfSunNeeded, difficultyLevel, pourFrequencyInDays, fertilizeFrequencyInWeeks, plantsOptimalLocation) VALUES ('" + name + "', '" + latName+ "', '" +generalInfo+ "', '" + amountOfSunNeeded+ "', '" + difficultyLevel+ "', '" + pourFrequencyInDays+ "', '" + fertilizeFrequencyInWeeks+ "', '" + plantsOptimalLocation + "');";
                command.ExecuteNonQuery(); //runs sql command
            }
            connection.Close();
        }
    }

    public void consoleLogPlants() {
        using (var connection = new  SqliteConnection(dbName)) {
            connection.Open();
            //set up command obj
            using (var command = connection.CreateCommand()) {
                //get everything from publicPlants table
                command.CommandText = "SELECT * FROM publicPlants;";
                //iterate through publicPlants
                using (IDataReader reader = command.ExecuteReader()) {
                    while (reader.Read())
                    Debug.Log("Name: " + reader["name"] + "\tlatName: " + reader["latName"] + "\tgeneralInfo: " + reader["generalInfo"] + "\tamountOfSunNeeded: " + reader["amountOfSunNeeded"] + "\tdifficultyLevel: " + reader["difficultyLevel"] + "\tpourFrequencyInDays: " + reader["pourFrequencyInDays"] + "\tfertilizeFrequencyInWeeks: " + reader["fertilizeFrequencyInWeeks"] + "\tplantsOptimalLocation: " + reader["plantsOptimalLocation"]);
                    reader.Close();
                }
            }
            connection.Close();
        }
    }    
}