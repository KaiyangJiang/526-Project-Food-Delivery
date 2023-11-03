using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportPlayer : MonoBehaviour
{
    public Transform player;  // Drag your player GameObject's Transform here in the Inspector
    public Transform[] teleportLocations; // Array of locations to teleport to
    public GameObject Overviewmap;
    private GameManager manager;
    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // Ensure you have buttons under your OverviewMap and they're in the same order as the teleportLocations
        Button[] buttons = GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Important for capturing the correct value in the lambda function
            buttons[i].onClick.AddListener(() => TeleportToLocation(index));
        }
        
    }

    public void TeleportToLocation(int index)
    {
        if (index < teleportLocations.Length)
        {
            player.position = teleportLocations[index].position;
            CloseMap();
            manager.updateTutorial(7);
        }
    }

    public void CloseMap()
    {
        if (Overviewmap.activeSelf)
        {
            Overviewmap.SetActive(false);
        }
    }
}