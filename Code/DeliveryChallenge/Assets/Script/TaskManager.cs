using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public GameTask gameTaskPrefab;  // 这是一个预制体，用于生成GameTask实例
    public float spawnInterval = 30.0f;
    public Transform interactorsTransform;

    private void Start()
    {
        // 创建初始的GameTask
        CreateGameTask();

        // 开始周期性生成GameTask
        // StartCoroutine(GenerateTasks());
    }

    private void CreateGameTask()
    {
        GameTask newTask = Instantiate(gameTaskPrefab, interactorsTransform);
        Debug.Log("Task: " + " Generated");
        newTask.Initialize();
    }

    private IEnumerator GenerateTasks()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            CreateGameTask();
        }
    }



}