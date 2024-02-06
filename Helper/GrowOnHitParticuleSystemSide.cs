using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowOnHitParticuleSystemSide : MonoBehaviour
{
    ParticleSystem part;
    List<ParticleCollisionEvent> collisionEvents;

    public bool m_Negate = false;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        GrowOnHit hit;
        if (other.TryGetComponent<GrowOnHit>(out hit))
        {
            if(m_Negate)
            {
                numCollisionEvents = -numCollisionEvents;
            }
            hit.OnParticuleHit(numCollisionEvents);
        }
        else
        {
            Debug.LogError("Particule from GrowOnHitParticuleSystemSide need to collide with an GrowOnHit object");
        }
    }

    }
