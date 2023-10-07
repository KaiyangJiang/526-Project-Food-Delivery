using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public GameTask gameTaskPrefab;  // 这是一个预制体，用于生成GameTask实例
    public float spawnInterval = 30.0f;
    public Transform interactorsTransform;
    
    private string[] taskTitles = {"Pizza", "Burger", "Hotdog", "Fried Rice", "Dumping", "Noodle", "Sushi", "Steak", "Salad", "Sandwich"};
    private string[] taskDescriptions = {"Get a pizza from the pizza shop", "Get a burger from the burger shop", "Get a hotdog from the hotdog shop", "Get a fried rice from the fried rice shop", "Get a dumping from the dumping shop", "Get a noodle from the noodle shop", "Get a sushi from the sushi shop", "Get a steak from the steak shop", "Get a salad from the salad shop", "Get a sandwich from the sandwich shop"};
    private float[] taskMoney = {10, 20, 30, 40, 50, 60, 70, 80, 90, 100};
    private int TASK_NUM = 10;
   
    
    private int MAX_TASK_NUM = 3;
    private int currentTaskNum = 0;
    private List<GameTask> activeTasks = new List<GameTask>();
    private HashSet<string> activeTaskTitles = new HashSet<string>();

    private void Start()
    {
        // 创建初始的GameTask
        CreateGameTask();

        // 开始周期性生成GameTask
        // StartCoroutine(GenerateTasks());
    }

    private void CreateGameTask()
    {
        if(currentTaskNum>=MAX_TASK_NUM) return;
        int randomTaskIndex = Random.Range(0, TASK_NUM);
        while (activeTaskTitles.Contains(taskTitles[randomTaskIndex]))
        {
            randomTaskIndex = Random.Range(0, TASK_NUM);
        }
        
        GameTask newTask = Instantiate(gameTaskPrefab, interactorsTransform);
        newTask.Initialize(taskMoney[randomTaskIndex], taskTitles[randomTaskIndex], taskDescriptions[randomTaskIndex]);
        
        currentTaskNum++;
        activeTasks.Add(newTask);
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