using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrowdirection : MonoBehaviour
{
    public Transform destination;  // Drag the destination object here in the inspector
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction from the arrow to the destination
        Vector3 directionToDestination = (destination.position - transform.position).normalized;

        // Adjust the y component to ensure the arrow stays flat or at a fixed angle
        directionToDestination.y = 0;

        // Make the arrow's forward direction point towards the destination
        transform.forward = directionToDestination;
    }
}
