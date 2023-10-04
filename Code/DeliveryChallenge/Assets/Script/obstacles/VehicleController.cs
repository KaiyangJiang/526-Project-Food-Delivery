using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    private Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 20);
        CheckBoundary();
    }

    void CheckBoundary()
    {
        if (transform.position.x < 0)
        {
            //transform.position = startingPosition;
            transform.position = new Vector3(120, 0, 5);
        }

    }
}
