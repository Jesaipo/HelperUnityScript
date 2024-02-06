using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OrderInLayerHelper : MonoBehaviour
{
    public float m_MaxY = 100;
    public float m_MinY = -100;
    public int m_MaxOrderInLayer = 1000; //max is above
    public int m_MinOrderInLayer = 1;
    public int m_Offset = 0;

    public bool m_NeedRefresh = false;

    List<KeyValuePair<SpriteRenderer, int>> m_SpritesAndOffset = new List<KeyValuePair<SpriteRenderer, int>>();
    List<KeyValuePair<ParticleSystemRenderer, int>> m_ParticlesAndOffset = new List<KeyValuePair<ParticleSystemRenderer, int>>();

    private void FillSpritesAndOffset()
    {
        foreach(SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            int offset = 0;
            OrderInLayerOffsetHelper offsetCompo = sr.gameObject.GetComponent<OrderInLayerOffsetHelper>();
            if (offsetCompo != null)
            {
                offset = offsetCompo.m_Offset;
            }
            m_SpritesAndOffset.Add(new KeyValuePair<SpriteRenderer, int>(sr, offset));
        }

        foreach (ParticleSystemRenderer sr in GetComponentsInChildren<ParticleSystemRenderer>())
        {
            int offset = 0;
            OrderInLayerOffsetHelper offsetCompo = sr.gameObject.GetComponent<OrderInLayerOffsetHelper>();
            if (offsetCompo != null)
            {
                offset = offsetCompo.m_Offset;
            }
            m_ParticlesAndOffset.Add(new KeyValuePair<ParticleSystemRenderer, int>(sr, offset));
        }


    }

    private void Start()
    {
        FillSpritesAndOffset();
        SetOrderInLayer();
    }

    private void Update()
    {
        #if !UNITY_EDITOR
            if(m_NeedRefresh)
        #endif
        {
            SetOrderInLayer();
        }
    }

    private void SetOrderInLayer()
    {
#if UNITY_EDITOR
        //m_SpritesAndOffset.Clear();
        //FillSpritesAndOffset();
#endif

        for (int i = 0; i < m_SpritesAndOffset.Count; i++)
        {
            float y = m_SpritesAndOffset[i].Key.transform.position.y;
            float factor = (y - m_MinY) / (m_MaxY - m_MinY);
            int value = (int)((1 - factor) * m_MaxOrderInLayer) + m_MinOrderInLayer + m_Offset;

            m_SpritesAndOffset[i].Key.sortingOrder = value + m_SpritesAndOffset[i].Value;
        }

        for (int i = 0; i < m_ParticlesAndOffset.Count; i++)
        {
            float y = m_ParticlesAndOffset[i].Key.transform.position.y;
            float factor = (y - m_MinY) / (m_MaxY - m_MinY);
            int value = (int)((1 - factor) * m_MaxOrderInLayer) + m_MinOrderInLayer + m_Offset;

            m_ParticlesAndOffset[i].Key.sortingOrder = value + m_ParticlesAndOffset[i].Value;
        }
    }
}
