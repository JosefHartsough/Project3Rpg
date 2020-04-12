using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class interactListener : MonoBehaviour
{

    public interactHandler signal;
    public UnityEvent signalEvent;

    // Invokes a signal event in Unity
   public void interactionListener()
    {
        signalEvent.Invoke();
    }

    // When listener has been turned on
    private void OnEnable()
    {
        signal.RegisterListener(this);
    }

    // When listener has been turned off
    private void OnDisable()
    {
        signal.DeRegisterListener(this);
    }
}
