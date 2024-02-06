using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TextMeshProHelper : MonoBehaviour
{
    public Color m_Color;

    public void SetColor(Color color)
    {
        GetComponent<TMP_Text>().color = color;
    }

    public void SetPresetColor()
    {
        GetComponent<TMP_Text>().color = m_Color;
    }

}
