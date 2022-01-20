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

public class deletePlant : MonoBehaviour
{
    private string dbName = "URI=file:Plants.db";

    public void deletePlantFromDB(){
        using (var connection = new SqliteConnection(dbName)) {
            connection.Open();

            // set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand()) {
                command.CommandText = "DELETE FROM userPlants WHERE plantID='"+PlayerPrefs.GetInt("plantID") + "';";
                command.ExecuteNonQuery(); //runs sql command
            }
            connection.Close();
        }
        SceneManager.LoadScene("MeinePflanzen");
    }
}