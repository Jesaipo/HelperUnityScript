using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.Events;

[XmlRoot(ElementName = "entry")]
    public class Entry
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "score")]
        public string Score { get; set; }
        [XmlElement(ElementName = "seconds")]
        public string Seconds { get; set; }
        [XmlElement(ElementName = "text")]
        public string Text { get; set; }
        [XmlElement(ElementName = "date")]
        public string Date { get; set; }
    }

[XmlRoot(ElementName = "leaderboard")]
public class Leaderboard
{
    [XmlElement(ElementName = "entry")]
    public List<Entry> Entry { get; set; }
}

[XmlRoot(ElementName = "dreamlo")]
    public class Dreamlo444
    {
        [XmlElement(ElementName = "leaderboard")]
        public Leaderboard Leaderboard { get; set; }
    }


public class MyLeaderboradEvent : UnityEvent<Dreamlo444>
{
}

public class LeaderboardParser : MonoBehaviour
{
    public bool m_RequestOnStart = true;
    public LeaderboardConfig m_Config;
    public UnityEvent OnError;
    public UnityEvent OnResultReady;


    public Dreamlo444 Result;

    private void Start()
    {
        if(m_RequestOnStart)
        {
            RequestNewDashboard();
        }
    }

    public void RequestNewDashboard()
    {
        StartCoroutine(GetRequest(m_Config.m_LeaderBoardRequest));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                //Debug.LogError (": Error: " + webRequest.error);
                OnError.Invoke();
            }
            else
            {
                //Debug.LogError( ":\nReceived: " + webRequest.downloadHandler.text);
                Result = Parse(webRequest.downloadHandler.text);

                if (Result != null)
                {
                    OnResultReady.Invoke();
                }
            }
        }
    }

    public Dreamlo444 Parse(string xml)
    {
        try
        {
            var serializer = new XmlSerializer(typeof(Dreamlo444));
            Dreamlo444 result;

            using (TextReader reader = new StringReader(xml))
            {
                result = (Dreamlo444)serializer.Deserialize(reader);
            }
            return result;
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("Exception loading config file: " + e);
            OnError.Invoke();

            return null;
        }
    }

}
