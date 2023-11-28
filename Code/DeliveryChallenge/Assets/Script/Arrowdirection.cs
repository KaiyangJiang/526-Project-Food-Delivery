using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrowdirection : MonoBehaviour
{
    private Vector3 destination;  // Drag the destination object here in the inspector
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction from the arrow to the destination
        Vector3 directionToDestination = (destination - transform.position).normalized;
        if(Input.GetKeyUp(KeyCode.G)) {
            Debug.Log("directionToDestination: " + directionToDestination);
            Debug.Log("destination.position: " + destination);
            Debug.Log("position: " + transform.position);
        }

        // Adjust the y component to ensure the arrow stays flat or at a fixed angle
        directionToDestination.y = 0;

        // Make the arrow's forward direction point towards the destination
        transform.right = directionToDestination;
        //transform.Rotate(90, 0, 0);
    }

    public void UpdateDestination(Vector3 destination) {
        this.destination = destination;
    }

    public Vector3 GetDestination()
    {
        return destination;
    }
}
