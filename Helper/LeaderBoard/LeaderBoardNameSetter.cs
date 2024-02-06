using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardNameSetter : MonoBehaviour
{

    public GameObject m_Content;

    private void Start()
    {
        //PlayerPrefs.SetString("Name", ""); //for test

        m_Content.SetActive(false);
    }

    private void Update()
    {
        if (PlayerPrefs.GetString("Name") != "")
        {
            this.gameObject.SetActive(false);
            m_Content.SetActive(true);
        }
    }

    public void SetPlayerName(string name)
    {
        if (name != "")
        {
            PlayerPrefs.SetString("Name", name);
        }
        else
        {
            int rdm = UnityEngine.Random.Range(0, 5000);
            name = "Anonymous" + rdm.ToString();
            PlayerPrefs.SetString("Name", name);
        }

        m_Content.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
