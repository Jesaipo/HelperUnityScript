using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyBox;
[RequireComponent(typeof(LayoutElement))]
public class LayoutElementAnimator : MonoBehaviour
{
    public enum Mode
    {
        GotoInitialMinWidth,
        GotoZero
    }

    public Mode m_Mode = Mode.GotoInitialMinWidth;
    private LayoutElement m_Layout;
    //[ConditionalField()] TODO
    public float m_AdditionalStartMinWidth = 3000f;
    private float m_InitialMinWidth;
    public AnimationCurve m_AnimationCurve;

    private float m_Time = 0.0f;

    public bool m_IgnoreLock = false;

    private static bool s_Lock = false;
    private bool m_PersonalLock = false;

    private bool m_IsOver = false;

    static public void ReleaseLock()
    {
        s_Lock = false;
    }

    private void Start()
    {
        m_Layout = GetComponent<LayoutElement>();
        m_InitialMinWidth = GetComponent<RectTransform>().rect.width;
        if (m_Mode == Mode.GotoInitialMinWidth)
        {
            m_Layout.minWidth = m_InitialMinWidth + m_AdditionalStartMinWidth;
        }
        else
        {
            m_Layout.minWidth = m_InitialMinWidth;
        }
    }

    public void Update()
    {

        if(m_IsOver)
        {
            return;
        }

        if(!m_PersonalLock && !s_Lock && !m_IgnoreLock)
        {
            s_Lock = true;
            m_PersonalLock = true;
        }

        if (m_PersonalLock || m_IgnoreLock)
        {
            if (m_Mode == Mode.GotoInitialMinWidth)
            {
                m_Layout.minWidth = m_InitialMinWidth + m_AdditionalStartMinWidth * m_AnimationCurve.Evaluate(m_Time);
            }
            else
            {
                m_Layout.minWidth = m_InitialMinWidth * m_AnimationCurve.Evaluate(m_Time);
            }
            m_Time += Time.deltaTime;
            if(m_Time > m_AnimationCurve[m_AnimationCurve.length - 1].time)
            {
                //AnimatinOver
                if (!m_IgnoreLock)
                {
                    m_PersonalLock = false;
                    s_Lock = false;
                }
                m_IsOver = true;

                if (m_Mode == Mode.GotoInitialMinWidth)
                {
                    m_Layout.minWidth = m_InitialMinWidth;
                }
                else
                {
                    m_Layout.minWidth = 0;
                }

            }
        }
    }
}
