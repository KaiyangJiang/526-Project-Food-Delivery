using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController2Opposite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 5);
        CheckBoundary();
    }

    void CheckBoundary()
    {
        if (transform.position.x > 120)
        {
            //transform.position = startingPosition;
            transform.position = new Vector3(0, 0, 117);
        }

    }
}
