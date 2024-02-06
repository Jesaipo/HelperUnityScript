using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerColliderHelper2D : MonoBehaviour
{
    public string TagToTrigger = "Player";
    public UnityEvent OnTriggerEnterEvent;
    public UnityEvent<Collider2D> OnTriggerEnterWithColliderEvent;
    public UnityEvent OnTriggerStayEvent;
    public UnityEvent<Collider2D> OnTriggerStayWithColliderEvent;
    public UnityEvent OnTriggerExitEvent;
    public UnityEvent<Collider2D> OnTriggerExitWithColliderEvent;
    public UnityEvent OnColliderEnterEvent;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TagToTrigger)
        {
            OnColliderEnterEvent.Invoke();

            ReactToColliderEnter react = collision.gameObject.transform.GetComponentInChildren<ReactToColliderEnter>();
            react?.ReactToColliderEnterCallback();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == TagToTrigger)
        {
            OnTriggerEnterEvent.Invoke();
            OnTriggerEnterWithColliderEvent.Invoke(other);

            ReactToColliderEnter react = other.transform.GetComponentInChildren<ReactToColliderEnter>();
            react?.ReactToColliderEnterCallback();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == TagToTrigger)
        {
            OnTriggerExitEvent.Invoke();
            OnTriggerExitWithColliderEvent.Invoke(other);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == TagToTrigger)
        {
            OnTriggerStayEvent.Invoke();
            OnTriggerStayWithColliderEvent.Invoke(other);
        }
    }
}
