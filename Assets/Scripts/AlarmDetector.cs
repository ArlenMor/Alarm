using System;
using UnityEngine;

public class AlarmDetector : MonoBehaviour
{
    public event Action OnTriggerEntered;
    public event Action OnTriggerExited;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEntered?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerExited?.Invoke();   
    }
}
