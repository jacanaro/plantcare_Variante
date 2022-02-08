using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;
using System.Data;
using Mono.Data.Sqlite;

public class Script_Reminder : MonoBehaviour
{
    public GameObject panel;
    public GameObject reminderPanel;
    public Text monatUndJahr;
    public Text tagText;
    private string dbName = "URI=file:Plants.db";

    //private bool isActive1 = false;

    //show reminder for the day clicked by user in kalender
    public void markReminder()
    {
        //(re-)create default-state ofchild of reminderpanel
        Text textChildOfReminderPanel = reminderPanel.GetComponentInChildren<Text>();
        textChildOfReminderPanel.text="";

        // Define dates
        DateTime dateClickedByUser;
        DateTime dateOfPlantsCreation;

        //get date of creation of plant created by user
        //create the db connection
        using (var connection = new SqliteConnection(dbName)) {
            connection.Open();

            // set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand()) {
                
                //get yearOfCreation, monthOfCreation, dayOfCreation and fertilizeFrequencyInWeeks, pourFrequencyInDays, name, nickname
                command.CommandText = "SELECT yearOfCreation, monthOfCreation, dayOfCreation, fertilizeFrequencyInWeeks, pourFrequencyInDays, name, nickname FROM userPlants INNER JOIN publicPlants on publicPlants.latName = userPlants.latName;";
                
                using (IDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        dateOfPlantsCreation = new DateTime(reader.GetInt32(reader.GetOrdinal("yearOfCreation")),
                        reader.GetInt32(reader.GetOrdinal("monthOfCreation")), reader.GetInt32(reader.GetOrdinal("dayOfCreation")));
                                
                        //get Datevalue from Day clicked by user
                        try {
                            dateClickedByUser=getDateTimeClickedByUser();
                            // Calculate number of days passed between the two dates.
                            TimeSpan interval = dateClickedByUser - dateOfPlantsCreation;
                            //get value in days
                            int fertilizeFrequencyInDays = 7*reader.GetInt32(reader.GetOrdinal("fertilizeFrequencyInWeeks"));

                            //check reminderPanel
                            if (reminderPanel != null) {
                                Animator animator = reminderPanel.GetComponent<Animator>();
                                if (animator == null) {
                                    Debug.Log("reminder animator equals null!");
                                    break;
                                } else {
                                    //check if fertilizing is requiered at clicked date
                                    if (interval.Days % fertilizeFrequencyInDays == 0) {
                                        int testFertilize=interval.Days % fertilizeFrequencyInDays;
                                        if (animator.GetBool("open") == false) animator.SetBool("open", true);
                                        textChildOfReminderPanel.text+= "Düngen von "+ reader["nickname"] + " der " + reader["name"] + " erforderlich!\n";
                                    }
                                    //check if pouring is requiered at clicked date
                                    if (interval.Days % reader.GetInt32(reader.GetOrdinal("pourFrequencyInDays")) == 0) {
                                        
                                        int lal=interval.Days % reader.GetInt32(reader.GetOrdinal("pourFrequencyInDays"));
                                        if (animator.GetBool("open") == false) animator.SetBool("open", true);
                                        textChildOfReminderPanel.text+= "Gießen von " + reader["nickname"] + " der " + reader["name"] + " erforderlich!\n";
                                    }
                                    /*
                                    else{
                                        if(animator.GetBool("open")==true)animator.SetBool("open", false);
                                    }*/
                                }
                            }
                            else {
                                Debug.Log("Reminder Panel equals null!");
                            }

                        } catch(Exception e) {
                            Debug.Log(e);
                        }
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }
        //Marks event on day (should only mark if there is text in reminderPanel)
        /*if (isActive1)
        {
            panel.active = false;
            isActive1 = !isActive1;
        }
        else
        {
            panel.active = true;
            isActive1 = !isActive1;
        }*/
    }

    //returns the Date of the Day clicked by the user in the calender scene
    public DateTime getDateTimeClickedByUser() {
        
        //get month and year from Gameobject Datum ->monatUndJahr
        string[] monatUndJahrArray= monatUndJahr.text.Split(' ');

        //get Monthnumber from monatUndJahrArray
        int monthNum=getMonthNumFromMonthName(monatUndJahrArray[0]);
        int day;
        int year;

        //convert day and year to integers
            day=Int32.Parse(tagText.text);
            year = Int32.Parse(monatUndJahrArray[1]);

        //create DatetimeObject with retrieved values    
        DateTime theDateClickedByUser = new DateTime(year, monthNum, day);
        return theDateClickedByUser; 
    }

        //translate monthname into number of month (there is already a function that does it, but only for the eng. names (January etc.))
        public int getMonthNumFromMonthName(string monthName) {
        switch (monthName) {
            case "Januar":
            return 1;
            case "Februar":
            return 2;
            case "März":
            return 3;
            case "April":
            return 4;
            case "Mai":
            return 5;
            case "Juni":
            return 6;
            case "Juli":
            return 7;
            case "August":
            return 8;
            case "September":
            return 9;
            case "Oktober":
            return 10;
            case "November":
            return 11;
            case "Dezember":
            return 12;
            default:
            Debug.Log("Monat Falsch/nicht erkannt!");
            return 0;
        }
    }
}