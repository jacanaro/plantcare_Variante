using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class managePlantModel : MonoBehaviour
{
    [SerializeField] private GameObject modelWraper;
    [SerializeField] private GameObject[] modelTextures = new GameObject[12];
    private string dbName = "URI=file:Plants.db";

    void Start()
    {
        string plantname=PlayerPrefs.GetString("name");
        int plantID= PlayerPrefs.GetInt("plantID");
        Debug.Log(plantID);
                //create the db connection
        using (var connection = new SqliteConnection(dbName)) {
            connection.Open();

            // set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand()) {
                
                //get generalInfo
                command.CommandText = "SELECT plantStage FROM userPlants, publicPlants where userPlants.latName=publicPlants.latName AND plantID='"+plantID+"';";
                
                using (IDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Debug.Log(reader["plantStage"]);
                    }
                    reader.Close();
                }
            }

            connection.Close();
        }

        foreach (GameObject x in modelTextures)
        {
            if(x.name.Equals("Paprikamittel")){
                GameObject model = Instantiate(x);
                model.transform.position = new Vector3(0f,0.6f,-9f);
                model.transform.localScale= new Vector3(0.1f, 0.1f, 0.1f);
            }
        }
    }
}
