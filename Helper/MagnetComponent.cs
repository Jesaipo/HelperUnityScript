using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class MagnetComponent : MonoBehaviour
{
    private CircleCollider2D m_Collider;

    public float m_MagnetStrength = 10.0f;

    private void Start()
    {
        m_Collider = this.GetComponent<CircleCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector3 forceDirection = this.transform.position - collision.transform.position;

        float radius = m_Collider.radius;

        float multiplier =  forceDirection.magnitude / radius;

        collision.attachedRigidbody.AddRelativeForce(forceDirection.normalized * multiplier * m_MagnetStrength);
    }
}
