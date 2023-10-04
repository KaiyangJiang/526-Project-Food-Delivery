using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController3Opposite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 20);
        CheckBoundary();
    }

    void CheckBoundary()
    {
        if (transform.position.z > 120)
        {
            //transform.position = startingPosition;
            transform.position = new Vector3(125, 0, 0);
        }

    }
}
