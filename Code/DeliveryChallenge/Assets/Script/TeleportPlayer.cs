using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportPlayer : MonoBehaviour
{
    public Transform player; // Player's Transform
    private TaskManager taskManager; // Reference to TaskManager

    private void Start()
    {
        taskManager = FindObjectOfType<TaskManager>();
        if (taskManager == null)
        {
            Debug.LogError("TaskManager not found in the scene.");
        }
    }

  
}