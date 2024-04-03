using System;
using UnityEngine;

public class AlarmDetector : MonoBehaviour
{
    public event Action OnTriggerEntered;
    public event Action OnTriggerExited;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Thief>() != null)
            OnTriggerEntered?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Thief>() != null) 
            OnTriggerExited?.Invoke();   
    }
}
