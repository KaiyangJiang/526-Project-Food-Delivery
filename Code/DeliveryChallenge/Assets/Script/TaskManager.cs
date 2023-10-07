using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public GameTask gameTaskPrefab;  // 这是一个预制体，用于生成GameTask实例
    private float spawnInterval = 8.0f;
    public Transform interactorsTransform;
    private TaskData taskData = new TaskData();
   
    
    private int MAX_TASK_NUM = 3;
    private int currentTaskNum = 0;
    private Dictionary<string, GameTask> activeTasks = new Dictionary<string, GameTask>();
    private HashSet<string> activeDestinations = new HashSet<string>();
    private List<GameTask> activeTasksList = new List<GameTask>();

    private void Start()
    {
        // 创建初始的GameTask
        CreateGameTask();

        // 开始周期性生成GameTask
        StartCoroutine(GenerateTasks());
    }

    private void CreateGameTask()
    {
        if(currentTaskNum>=MAX_TASK_NUM || currentTaskNum>=taskData.TASK_NUM) return;
        int randomTaskIndex = Random.Range(0, taskData.TASK_NUM);
        while (activeTasks.ContainsKey(taskData.taskTitles[randomTaskIndex]))
        {
            randomTaskIndex = Random.Range(0, taskData.TASK_NUM);
        }
        int randomDestinationIndex = Random.Range(0, taskData.TASK_NUM);
        while (activeDestinations.Contains(taskData.taskDestination[randomDestinationIndex]))
        {
            randomDestinationIndex = Random.Range(0, taskData.TASK_NUM);
        }
        
        
        GameTask newTask = Instantiate(gameTaskPrefab, interactorsTransform);
        string taskTitle = taskData.taskTitles[randomTaskIndex];
        string taskDestination = taskData.taskDestination[randomDestinationIndex];
        
        TaskInfo taskInfo = taskData.getTaskInfo(taskTitle, taskDestination);
        //newTask.Initialize(taskData.taskMoney[randomTaskIndex], taskData.taskTitles[randomTaskIndex], taskData.taskDescriptionsGet[randomTaskIndex], taskData.taskColors[randomTaskIndex], taskData.taskGetPositions[randomTaskIndex], taskData.taskDeliverPositions[randomTaskIndex]);
        newTask.Initialize(taskInfo,currentTaskNum);
        currentTaskNum++;
        activeTasks.Add(taskTitle, newTask);
        activeDestinations.Add(taskDestination);
        activeTasksList.Add(newTask);
        
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
        //task.setContent("Deliver a " + title + " to the ");
        task.updateTask();
    }
    
    public float completeTask(string title)
    {
        GameTask task = activeTasks[title];
        task.setContent("Completed!");
        float money = task.getEarnedMoney();
        int index = task.getIndex();
        string destination = task.getDestination();
        
        Destroy(task.gameObject);
        for(int i = index+1;i<currentTaskNum;i++)
        {
            activeTasksList[i].updateTaskInfoDisplay();
        }
        activeTasks.Remove(title);
        activeDestinations.Remove(destination);
        activeTasksList.RemoveAt(index);
        currentTaskNum--;
        
        return money;
    }



}