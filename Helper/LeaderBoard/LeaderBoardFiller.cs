using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardFiller : MonoBehaviour
{
    public LeaderboardParser LeaderBoardParser;
    public GameObject LeaderBoardListToFill;
    public GameObject LeaderBoardRankPrefab;
    public GameObject ErrorLeaderBoard;

    public Color m_MyNameColor;
    public void DisplayLeader()
    {
        ErrorLeaderBoard.SetActive(false);
        for (int i = 0; i < LeaderBoardListToFill.transform.childCount; i++)
        {
            Destroy(LeaderBoardListToFill.transform.GetChild(i).gameObject);
        }
        Dreamlo444 result = LeaderBoardParser.Result;

        int entrycount = result.Leaderboard.Entry.Count;
        float cumulateHeightToFocusEntry = 0;
        float cumulateHeight = 0f;
        float containerHeight = this.GetComponent<RectTransform>().rect.height;
        for (int i = 0; i < entrycount; i++)
        {
            GameObject rank = Instantiate(LeaderBoardRankPrefab, LeaderBoardListToFill.transform);
            cumulateHeight += rank.GetComponent<RectTransform>().rect.height;
            rank.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = result.Leaderboard.Entry[i].Score;
            rank.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = result.Leaderboard.Entry[i].Name;
            if (PlayerPrefs.GetString("Name") == result.Leaderboard.Entry[i].Name)
            {
                rank.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().color = m_MyNameColor;
                rank.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().color = m_MyNameColor;
                cumulateHeightToFocusEntry = cumulateHeight;
                if(cumulateHeightToFocusEntry > containerHeight*2f/3f)
                {
                    cumulateHeightToFocusEntry -= containerHeight / 2; //center
                }
                else
                {
                    cumulateHeightToFocusEntry = 0;
                }
            }
        }

        RectTransform rect = LeaderBoardListToFill.GetComponent<RectTransform>();
        LeaderBoardListToFill.GetComponent<RectTransform>().sizeDelta = new Vector2(rect.sizeDelta.x, cumulateHeight);
        LeaderBoardListToFill.GetComponent<RectTransform>().anchoredPosition = new Vector2(LeaderBoardListToFill.GetComponent<RectTransform>().anchoredPosition.x, cumulateHeightToFocusEntry);
    }
}
