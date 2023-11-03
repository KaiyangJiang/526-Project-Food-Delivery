using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public GameDataCollector gameDataCollector;
    private GameManager manager;
    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
                manager.updateTutorial(2);
                playerController.openMagicBox();
                if(gameDataCollector != null)
                {
                    gameDataCollector.treasureBoxesCollected++;
                }
                Destroy(this.gameObject);
                
            }
        }
    }
}
