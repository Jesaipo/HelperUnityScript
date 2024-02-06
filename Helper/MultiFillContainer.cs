using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MultiFillContainer : MonoBehaviour
{
    //public Color m_EmptyColor;
    //public Color m_OnFillColor;
    //public Color m_FilledColor;

    public List<Image> m_ToFillImage = new List<Image>();
    public List<UnityEvent> m_CallOnEmpty = new List<UnityEvent>();
    public List<UnityEvent> m_CallOnFill = new List<UnityEvent>();
    public List<UnityEvent> m_CallFilled = new List<UnityEvent>();

    public float m_DebugFill = -1f;

    public void SetFill(float fill)
    {
        float f = 1f / m_ToFillImage.Count;

        for(int i = 0; i< m_ToFillImage.Count; i++)
        {
            float fillValue = 0;
            Color color;
            if (fill > 0f)
            {
                if (fill < f)
                {
                    //calculate Fill value
                    fillValue = fill * m_ToFillImage.Count;
                    //color = m_OnFillColor;
                    if (i < m_CallOnFill.Count)
                        m_CallOnFill[i].Invoke();
                }
                else
                {
                    //fullFilled
                    fillValue = 1;
                    //color = m_FilledColor;
                    if (i < m_CallFilled.Count)
                        m_CallFilled[i].Invoke();
                }
            }
            else
            {
                //color = m_EmptyColor;
                if (i < m_CallOnEmpty.Count)
                    m_CallOnEmpty[i].Invoke();
            }
            fill -= f;
            m_ToFillImage[i].fillAmount = fillValue;
            //m_ToFillImage[i].color = color;
        }
    }

    private void Update()
    {
        if(m_DebugFill > 0f)
        {
            SetFill(m_DebugFill);
        }
    }
}
