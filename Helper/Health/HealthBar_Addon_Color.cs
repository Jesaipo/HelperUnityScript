using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthBar))]
public class HealthBar_Addon_Color : MonoBehaviour
{
    bool IsUnder = false;

    private HealthBar m_HealthBar;

    public Color m_MainColor;
    Color m_OldMainColor;
    public Color m_EvolColor;
    Color m_OldEvolColor;

    public float m_TurnColorsThresholdPercent;


    // Start is called before the first frame update
    void Start()
    {
        m_HealthBar = GetComponent<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsUnder && m_HealthBar.GetCurrentHealth() < (m_TurnColorsThresholdPercent * m_HealthBar.GetMaxHealth())/100f)
        {
            m_OldMainColor = m_HealthBar.Image.color;
            m_HealthBar.Image.color = m_MainColor;

            m_OldEvolColor = m_HealthBar.Image2.color;
            m_HealthBar.Image2.color = m_EvolColor;

            IsUnder = true;
        }

        if(IsUnder && m_HealthBar.GetCurrentHealth() >= (m_TurnColorsThresholdPercent * m_HealthBar.GetMaxHealth()) / 100f)
        {
            m_HealthBar.Image.color = m_OldMainColor;

            m_HealthBar.Image2.color = m_OldEvolColor;

            IsUnder = false;
        }
    }
}
