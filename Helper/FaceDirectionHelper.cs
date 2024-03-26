using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FaceDirectionHelper : MonoBehaviour
{
    Vector3 m_PreviousPosition;

    public UnityEvent<bool> OnFlipX;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 currentPosition = this.transform.position;
        currentPosition.z = 0f;
        m_PreviousPosition = currentPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = this.transform.position;
        currentPosition.z = 0f;

        Vector3 direction = this.transform.position - m_PreviousPosition;

        float ZAngle = Mathf.Atan2(direction.y, direction.x);
        if(ZAngle > -90 && ZAngle < 90)
        {
            OnFlipX.Invoke(false);
        }
        else
        {
            OnFlipX.Invoke(true);
            ZAngle += 180;
        }


        this.transform.Rotate(Vector3.forward, ZAngle);

        m_PreviousPosition = this.transform.position;
    }
}
