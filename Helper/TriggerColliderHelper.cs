using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerColliderHelper : MonoBehaviour
{
    public string TagToTrigger = "Player";
    public UnityEvent OnTriggerEnterEvent;
    public UnityEvent<Collider> OnTriggerEnterWithColliderEvent;
    public UnityEvent OnTriggerExitEvent;
    public UnityEvent<Collider> OnTriggerExitWithColliderEvent;
    public UnityEvent OnColliderEnterEvent;


    public void SetTagToTrigger(string tag)
    {
        TagToTrigger = tag;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == TagToTrigger)
        {
            OnColliderEnterEvent.Invoke();

            ReactToColliderEnter react = collision.gameObject.transform.GetComponentInChildren<ReactToColliderEnter>();
            react?.ReactToColliderEnterCallback();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagToTrigger)
        {
            OnTriggerEnterEvent.Invoke();
            OnTriggerEnterWithColliderEvent.Invoke(other);

            ReactToColliderEnter react = other.transform.GetComponentInChildren<ReactToColliderEnter>();
            react?.ReactToColliderEnterCallback();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == TagToTrigger)
        {
            OnTriggerExitEvent.Invoke();
            OnTriggerExitWithColliderEvent.Invoke(other);
        }
    }
}
