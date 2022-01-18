using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class meinePflanzen : MonoBehaviour
{
    public GameObject meinePflanzenButtonPrefab;
    public GameObject mainPanel;
    public Texture[] imageTextures = new Texture[3];
    private string dbName = "URI=file:Plants.db";

    void Start()
    {
        //create the db connection
        using (var connection = new SqliteConnection(dbName)) {
            connection.Open();

            // set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand()) {
                
                //get generalInfo
                command.CommandText = "SELECT plantID, name, nickname FROM userPlants, publicPlants where userPlants.latName=publicPlants.latName;";
                
                //initial button position y
                float myY=195;

                using (IDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        //create buttons for user plants with custom position
                        GameObject meinePflanzeButton = Instantiate(meinePflanzenButtonPrefab);
                        meinePflanzeButton.transform.parent = mainPanel.transform;
                        RectTransform rectTransform = meinePflanzeButton.GetComponent<RectTransform>();
                        Vector2 position = rectTransform.anchoredPosition;
                        
                        rectTransform.anchoredPosition = new Vector2(0, myY);
                        myY=myY-(rectTransform.rect.height+15);
                        int plantID=reader.GetInt32(reader.GetOrdinal("plantID"));

                        //Buttons text field
                        TMPro.TextMeshProUGUI buttonText = meinePflanzeButton.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                        buttonText.text=reader["nickname"]+"\n"+reader["name"];

                        //look for plant image texture by plantname        
                        foreach (Texture x in imageTextures)
                        {
                            if (x.name.Equals(reader["name"]))
                            {
                                meinePflanzeButton.GetComponentInChildren<RawImage>().texture=x;
                            }
                        }

                        //set onlick
                        meinePflanzeButton.GetComponent<Button>().onClick.AddListener(()=> MeinePflanzeButtonOnClick(plantID));
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }
    }

    void MeinePflanzeButtonOnClick(int plantID)
    {
        PlayerPrefs.SetInt("plantID", plantID);
        SceneManager.LoadScene("Pflanzenprofil");
    }
}
