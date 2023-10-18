using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDoor : MonoBehaviour
{
    public GameDataCollector gameDataCollector;
    // Start is called before the first frame update
    void Start()
    {
        gameDataCollector = FindObjectOfType<GameDataCollector>();

        if (gameDataCollector == null)
        {
            Debug.LogError("GameDataCollector not found in the scene!");
        }
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
                playerController.ResetToRandomPosition();
                gameDataCollector.magicDoorsUsed++;

            }
        }
    }
}
