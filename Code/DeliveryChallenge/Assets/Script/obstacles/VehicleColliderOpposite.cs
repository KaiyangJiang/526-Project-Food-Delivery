using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleColliderOpposite : MonoBehaviour
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
        if (transform.position.x > 80)
        {
            //transform.position = startingPosition;
            transform.position = new Vector3(0, 0, -3);
        }

    }
}
