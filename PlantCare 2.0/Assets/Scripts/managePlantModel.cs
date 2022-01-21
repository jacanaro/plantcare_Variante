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
        string plantname = "unbekannt";
        int plantID = PlayerPrefs.GetInt("plantID");
        string plantStage ="unbekannt";
        string gesundheit = PlayerPrefs.GetString("gesundheit");

        //create the db connection
        using (var connection = new SqliteConnection(dbName)) {
            connection.Open();

            // set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand()) {
                
                //get plantstage
                command.CommandText = "SELECT plantStage, name FROM userPlants, publicPlants where userPlants.latName=publicPlants.latName AND plantID='"+plantID+"';";
                
                using (IDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        plantStage=""+reader["plantStage"];
                        plantname=""+reader["name"];
                    }
                    reader.Close();
                }
            }

            connection.Close();
        }

        Debug.Log("plantname: "+ plantname+ " plantID: "+ plantID + " plantStage: " + plantStage + " gesundheit: "+ gesundheit);

        if(gesundheit.Equals("gut")){
            foreach (GameObject x in modelTextures)
            {
                string[] words = x.name.Split(' ');

                //catch aloe vera
                if(words.Length==3){
                    string aloeVeraName=words[0]+" "+words[1];
                    
                    if(aloeVeraName.Equals(plantname) && words[2].Equals(plantStage)){
                        instantiateModel(x);
                        break; 
                    }               
                }else{
                    if(words[0].Equals(plantname) && words[1].Equals(plantStage)){
                        instantiateModel(x);
                        break;
                    }
                }
            }
        }else{
            foreach (GameObject x in modelTextures)
            {
                string[] words = x.name.Split(' ');

                //catch aloe vera
                if(words.Length==3){
                    string aloeVeraName=words[0]+" "+words[1];
                    
                    if(aloeVeraName.Equals(plantname) && words[2].Equals("Welk")){
                        instantiateModel(x);
                        break;
                    }
                }else{
                    if(words[0].Equals(plantname) && words[1].Equals("Welk")){
                        instantiateModel(x);
                        break;
                    }
                }
            }
        }
    }

    private void instantiateModel(GameObject x){
        GameObject model = Instantiate(x);
        model.transform.position = new Vector3(0f,0.52f,-9f);
        model.transform.localScale= new Vector3(0.09f, 0.09f, 0.09f);
    }
}
