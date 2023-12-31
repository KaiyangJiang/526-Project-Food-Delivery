using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBooster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController)
            {
                playerController.TimerIncrease();
                Destroy(this.gameObject);
            }
        }
    }
}
