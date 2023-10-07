using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public GameTask gameTaskPrefab;  // 这是一个预制体，用于生成GameTask实例
    public float spawnInterval = 30.0f;
    public Transform interactorsTransform;
    private TaskData taskData = new TaskData();
   
    
    private int MAX_TASK_NUM = 3;
    private int currentTaskNum = 0;
    private Dictionary<string, GameTask> activeTasks = new Dictionary<string, GameTask>();

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
        int randomTaskIndex = Random.Range(0, taskData.TASK_NUM);
        while (activeTasks.ContainsKey(taskData.taskTitles[randomTaskIndex]))
        {
            randomTaskIndex = Random.Range(0, taskData.TASK_NUM);
        }
        
        GameTask newTask = Instantiate(gameTaskPrefab, interactorsTransform);
        string taskTitle = taskData.taskTitles[randomTaskIndex];
        
        TaskInfo taskInfo = taskData.getTaskInfo(taskTitle);
        //newTask.Initialize(taskData.taskMoney[randomTaskIndex], taskData.taskTitles[randomTaskIndex], taskData.taskDescriptionsGet[randomTaskIndex], taskData.taskColors[randomTaskIndex], taskData.taskGetPositions[randomTaskIndex], taskData.taskDeliverPositions[randomTaskIndex]);
        newTask.Initialize(taskInfo);
        currentTaskNum++;
        activeTasks.Add(taskData.taskTitles[randomTaskIndex], newTask);
    }

    private IEnumerator GenerateTasks()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            CreateGameTask();
        }
    }

    public void updateTask(string title)
    {
        GameTask task = activeTasks[title];
        task.setContent("Deliver a " + title + " to the ");
    }
    
    public float completeTask(string title)
    {
        GameTask task = activeTasks[title];
        task.setContent("Completed!");
        float money = task.getEarnedMoney();
        Destroy(task.gameObject);
        return money;
    }



}