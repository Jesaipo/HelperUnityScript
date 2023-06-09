﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Dreamlo : MonoBehaviour
{
    int points = 85;//add this value to AddNewHighscore function
    string user = "FIVESIGN";//& this you can replace it with string from input field
    public Text anzeige;
    const string privateCode = "8emSDmwcGkS8-Jtzi27sCQQLT30aNynE-PUTK1w2B-0g";
    const string publicCode = "572658d06e51b60644e2625c";
    const string webURL = "http://dreamlo.com/lb/";
    public Highscore[] highscoresList;
    void Awake()
    {
        //call this function to upload & put your variables inside braces 
        AddNewHighscore(user, points);
        DownloadHighscores();// & this to download
    }
    public void AddNewHighscore(string username, int score)
    {
        StartCoroutine(UploadNewHighscore(username, score));
    }
    IEnumerator UploadNewHighscore(string username, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;
        if (string.IsNullOrEmpty(www.error))
            print("Upload Successful");
        else
        {
            print("Error uploading: " + www.error);
        }
    }

    public void DownloadHighscores()
    {
        StartCoroutine("DownloadHighscoresFromDatabase");
    }
    IEnumerator DownloadHighscoresFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscores(www.text);
        }
        else
        {
            print("Error Downloading: " + www.error);
        }
    }
    void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, score);
            print(highscoresList[i].username + ": " + highscoresList[i].score);
            //this line will change the ui text 
            anzeige.text = (highscoresList[i].username + ": " + highscoresList[i].score);
        }
    }
}
public struct Highscore
{
    public string username;
    public int score;
    public Highscore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }
}