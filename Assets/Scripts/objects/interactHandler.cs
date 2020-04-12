using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class interactHandler : ScriptableObject 
{
    // List of our listeners to send interaction
    public List<interactListener> listeners = new List<interactListener>();

    // Function to loop through our listeners and call methods attached to listeners
    // Calls on interactionListener in interactListener.cs 
    public void turnOnInteraction()
    {

        for (int i=listeners.Count -1; i >=0; i--)
        {
            listeners[i].interactionListener();
        }

    }

    // Adds listener to our list of interactions
    public void RegisterListener(interactListener listener)
    {
        listeners.Add(listener);
    }

    // Removes listener from our list of interactions
    public void DeRegisterListener(interactListener listener)
    {
        listeners.Remove(listener);
    }
}
    
