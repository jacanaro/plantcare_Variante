using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class loadCorrectHilfedialog : MonoBehaviour
{
public GameObject HilfedialogTomate;
    public GameObject HilfedialogPaprika;
    public GameObject HilfedialogAloeVera;
    private string dbName = "URI=file:Plants.db";

    void Start()
    {
        int plantID=PlayerPrefs.GetInt("plantID");
        string pflanzenName="unbekannt";

        //create the db connection
        using (var connection = new SqliteConnection(dbName)) {
            connection.Open();

            // set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand()) {
                
                //get generalInfo
                command.CommandText = "SELECT name FROM publicPlants,userPlants WHERE publicPlants.latName=userPlants.latName AND plantID='" + plantID + "';";
                
                using (IDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        pflanzenName = ""+reader["name"];
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }

        switch (pflanzenName){
        case "Tomate":
            HilfedialogTomate.SetActive(true);
            break;
        case "Paprika":
            HilfedialogPaprika.SetActive(true);
            break;
        case "Aloe Vera":
            HilfedialogAloeVera.SetActive(true);
            break;
        default:
            print ("No plant in Profile!");
            break;
        }  
    }

}
