using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeaderboardConfig", menuName = "ScriptableObjects/LeaderboardConfig", order = 1)]
public class LeaderboardConfig : ScriptableObject
{
    public string m_LeaderBoardRequest;// = "GetURL";

    public string m_LeaderBoardPrivateCode;// = "PrivateCode";

    public string m_LeaderBoardPublicCode;// = "PublicCode";
}