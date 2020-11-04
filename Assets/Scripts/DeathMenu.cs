using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathMenu : Menu
{
    public static readonly string[] firstNames =
    {
        "Anna", "Andrew", "Andy", "Annabelle", "Alexander", "Alexa",
        "Bob", "Catherine", "Christian", "Christina", "Caesar", "Daniel", "Damian", "Daina",
        "David", "Eugene", "Earl", "Edward", "James", "Mary", "John", "Patricia", "Robert", "Jennifer",
        "Michael", "Linda", "William", "Elizabeth", "Barbara", "Rick", "Susan", "Joseph", "Jessica",
        "Thomas", "Sarah", "Charles", "Karen", "Christopher", "Nancy", "Margaret", "Matthew", "Lisa", "George"
    };
    public static readonly string[] surnames =
    {
        "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Rodriguez", "Wilson", "Martinez", "Anderson",
        "Taylor", "Thomas", "Hernandes", "Moore", "Martin", "Jackson", "Thompson", "White", "Lopez", "Lee", "Gonzalez", "Harrison",
        "Clark", "Lewis", "Robinson", "Walker", "Hall", "Young", "Allen", "Sanchez"
    };
    public static readonly string[] causesOfDeath =
    {
        "Fatal injury",
        "Drowning",
        "Failure to achieve the goal",
        "Touching Lasers"
    };

    public TextMeshProUGUI subjectName;
    public TextMeshProUGUI subjectID;
    public TextMeshProUGUI causeOfDeath;
    public TextMeshProUGUI timeOfDeath;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(int deathId)
    {
        FindObjectOfType<PauseMenu>().Stop();
        subjectName.text += firstNames[UnityEngine.Random.Range(0, firstNames.Length)] + " " + surnames[UnityEngine.Random.Range(0, surnames.Length)];
        subjectID.text += SaveSystem.data.deaths;
        causeOfDeath.text += causesOfDeath[deathId];
        DateTime thisDay = DateTime.Now;
        timeOfDeath.text += thisDay.ToString("hh:mm:ss tt");
        gameObject.SetActive(true);
    }
}
