using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticuleSystem_Addon_ExternalForce : MonoBehaviour
{
    public void ActivateExternalForce()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var ex = ps.externalForces;
        ex.enabled = true;
    }

    public void DeactivateExternalForce()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var ex = ps.externalForces;
        ex.enabled = false;
    }
}