using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipHelper : MonoBehaviour
{
    public bool m_FlipGameObject = false;
    Vector3 m_PreviousPosition = Vector3.zero;
    SpriteRenderer m_Sprite;

    float m_Threshold = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        m_PreviousPosition = this.transform.position;
        m_Sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 speed = this.transform.position - m_PreviousPosition;
        if (speed != Vector3.zero)
        {
            if (Mathf.Abs(speed.x) > m_Threshold)
            {
                Flip(speed.x < 0);
            }
        }

        m_PreviousPosition = this.transform.position;
    }

    void Flip(bool flip)
    {
        if(m_FlipGameObject)
        {
            if (flip)
            {
                this.transform.localEulerAngles = new Vector3(this.transform.eulerAngles.x, 180f, this.transform.eulerAngles.z);
            }
            else
            {
                this.transform.localEulerAngles = new Vector3(this.transform.eulerAngles.x, 0, this.transform.eulerAngles.z);
            }
        }
        else
        {
            if (flip)
            {
                m_Sprite.flipX = true;
            }
            else
            {
                m_Sprite.flipX = false;
            }
        }
    }
}
