using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public GameTask gameTaskPrefab;  // 这是一个预制体，用于生成GameTask实例
    public GameObject arrowPrefab; // prefab to create arrows
    private float spawnInterval = 10.0f;
    public Transform interactorsTransform;
    public Transform playerTransform;
    private TaskData taskData = new TaskData();
    public GameObject tasksGrid;
    public GameObject taskListItemPrefab;
    public Sprite fruits;
    public Sprite chicken;
    public Sprite sushi;
    public Sprite hamburger;
    public Sprite fries;
    public Sprite popcorn;
    public Sprite pizzas;
    public bool inTutorial;
    Dictionary<string, Sprite> foodsImagesSprites = new Dictionary<string, Sprite>();
    

    public GameDataCollector gameDataCollector;


    private int MAX_TASK_NUM = 3;
    private int currentTaskNum = 0;
    private Dictionary<string, GameTask> activeTasks = new Dictionary<string, GameTask>();
    private Dictionary<string, GameObject> arrows = new Dictionary<string, GameObject>();
    private HashSet<string> activeDestinations = new HashSet<string>();
    private List<GameTask> activeTasksList = new List<GameTask>();
    private string clickedTask = "";

    

    

    private void Start()
    {
        foodsImagesSprites.Add("Pizza",pizzas);
        foodsImagesSprites.Add("Fried Chicken", chicken);
        foodsImagesSprites.Add("Fruits", fruits);
        foodsImagesSprites.Add("Hamburger", hamburger);
        foodsImagesSprites.Add("Popcorn", popcorn);
        foodsImagesSprites.Add("French Fries", fries);
        foodsImagesSprites.Add("Sushi", sushi);
    }

    

    public void StartTasks()
    {
        if (inTutorial)
        {
            CreateGameTaskInTuturial(0);
            CreateGameTaskInTuturial(1);
            return;
        }
        CreateGameTask();
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
        addToTaskPanel(taskTitle, taskInfo.color);
        
        
        if(gameDataCollector != null)
        {
            gameDataCollector.RecordTaskType(taskTitle);
        }

        // create an arrow pointing to the Get position
        Vector3 bias = new Vector3(-2.50f, 2.50f, 0);
        Vector3 arrowPosition = playerTransform.position + bias;
        GameObject arrow = Instantiate(arrowPrefab, arrowPosition, Quaternion.identity);
        arrow.transform.parent = playerTransform;
        arrow.GetComponent<Renderer>().material.color = taskData.GetColor(taskTitle);
        arrow.GetComponent<Renderer>().enabled = false;
        Arrowdirection arrowdirection = arrow.GetComponent<Arrowdirection>();
        arrowdirection.UpdateDestination(taskData.GetPosition(taskTitle, true) + interactorsTransform.position);
        arrows.Add(taskTitle, arrow);

        if(clickedTask == "")
        {
            SetClickedTask(taskTitle);
        }
        
    }
    
    private void CreateGameTaskInTuturial(int index)
    {
        int randomTaskIndex = index;
        int randomDestinationIndex = index;
        
        GameTask newTask = Instantiate(gameTaskPrefab, interactorsTransform);
        string taskTitle = taskData.taskTitles[randomTaskIndex];
        string taskDestination = taskData.taskDestination[randomDestinationIndex];

        TaskInfo taskInfo = taskData.getTaskInfo(taskTitle, taskDestination);
        if (index == 0)
        {
            taskInfo.deliverPosition = new Vector3(-22.31f, -3.76f, 0.0f);
            taskInfo.deliverRotation = Quaternion.Euler(0, 90, 0);
        }

        if (index == 1)
        {
            taskInfo.getPosition = new Vector3(-21.81f, -3.76f, -18.2f);
            taskInfo.getRotation = Quaternion.Euler(0, 90, 0);
            taskInfo.deliverPosition = new Vector3(-7.07f, -3.99f, -21.46f);
            taskInfo.deliverRotation = Quaternion.Euler(0, 45, 0);
        }
        //newTask.Initialize(taskData.taskMoney[randomTaskIndex], taskData.taskTitles[randomTaskIndex], taskData.taskDescriptionsGet[randomTaskIndex], taskData.taskColors[randomTaskIndex], taskData.taskGetPositions[randomTaskIndex], taskData.taskDeliverPositions[randomTaskIndex]);
        newTask.Initialize(taskInfo,currentTaskNum);
        currentTaskNum++;
        activeTasks.Add(taskTitle, newTask);
        activeDestinations.Add(taskDestination);
        activeTasksList.Add(newTask);
        addToTaskPanel(taskTitle, taskInfo.color);
        
        
        if(gameDataCollector != null)
        {
            gameDataCollector.RecordTaskType(taskTitle);
        }

        // create an arrow pointing to the Get position
        Vector3 bias = new Vector3(-2.50f, 2.50f, 0);
        Vector3 arrowPosition = playerTransform.position + bias;
        GameObject arrow = Instantiate(arrowPrefab, arrowPosition, Quaternion.identity);
        arrow.transform.parent = playerTransform;
        arrow.GetComponent<Renderer>().material.color = taskData.GetColor(taskTitle);
        arrow.GetComponent<Renderer>().enabled = false;
        Arrowdirection arrowdirection = arrow.GetComponent<Arrowdirection>();
        if (index == 0)
        {
            arrowdirection.UpdateDestination(taskData.GetPosition(taskTitle, true) + interactorsTransform.position);
        }
        else
        {
            arrowdirection.UpdateDestination(new Vector3(-21.81f, -3.76f, -18.2f) + interactorsTransform.position);
        }
        arrows.Add(taskTitle, arrow);

        if(clickedTask == "")
        {
            SetClickedTask(taskTitle);
        }
        
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
        //updateTaskPanel(title);

        GameObject arrow = arrows[title];
        Arrowdirection arrowdirection = arrow.GetComponent<Arrowdirection>();
        string destinationString = task.getDestination();
        if (!inTutorial)
        {
            arrowdirection.UpdateDestination(taskData.GetPosition(destinationString, false) + interactorsTransform.position);
        }
        else
        {
            if(title.Equals("Pizza"))
            {
                arrowdirection.UpdateDestination(new Vector3(-22.31f, -3.76f, 0.0f) + interactorsTransform.position);
            }
            else
            {
                arrowdirection.UpdateDestination(new Vector3(-7.07f, -3.99f, -21.46f) + interactorsTransform.position);
            }
        }
        
        updateTaskPanel(title);
    }
    
    public float completeTask(string title)
    {
        GameTask task = activeTasks[title];
        task.setContent("Completed!");
        float money = task.getEarnedMoney();
        int index = task.getIndex();
        string destination = task.getDestination();
        task.completeTask();
        

        Destroy(task.gameObject);
        for(int i = index+1;i<currentTaskNum;i++)
        {
            activeTasksList[i].updateTaskInfoDisplay();
        }
        activeTasks.Remove(title);
        activeDestinations.Remove(destination);
        activeTasksList.RemoveAt(index);
        currentTaskNum--;
        
         if(title == clickedTask)
        {
            clickedTask = "";
            if(currentTaskNum != 0)
            {
                SetClickedTask(activeTasksList[0].getTitle());
            }
        }
        GameObject arrow = arrows[title];
        arrows.Remove(title);
        Destroy(arrow);
        removeFromTaskPanel(title);

        if(gameDataCollector != null)
        {
            gameDataCollector.tasksCompleted++;
        }
        


        return money;
    }

    private void addToTaskPanel(string title, Color color)
    {
        GameObject taskLIstItemObject = Instantiate(taskListItemPrefab, tasksGrid.transform);
        taskLIstItemObject.name = title;
        Image taskColor = taskLIstItemObject.transform.Find("TaskColor").GetComponent<Image>();
        taskColor.color = color;
        Image taskIcon = taskLIstItemObject.transform.Find("TaskIcon").GetComponent<Image>();
        taskIcon.sprite = foodsImagesSprites[title];
    }
    

    private void removeFromTaskPanel(string title)
    {
        foreach (Transform child in tasksGrid.transform)
        {
            if (child.name == title)
            {
                Destroy(child.gameObject);
                break; 
            }
        }
    }

    private void updateTaskPanel(string title)
    {
        foreach (Transform child in tasksGrid.transform)
        {
            if (child.name == title)
            {
                
                TextMeshProUGUI taskStatus = child.Find("TaskStatus").GetComponent<TextMeshProUGUI>();
                taskStatus.text = "Picked Up";
                break; 
            }
        }
    }

    public void handleTaskClicked()
    {
        Debug.Log("888888");
    }

    public void SetClickedTask(string taskTitle)
    {
        if(clickedTask != "")
        {
            arrows[clickedTask].GetComponent<Renderer>().enabled = false;
        }
        clickedTask = taskTitle;
        arrows[taskTitle].GetComponent<Renderer>().enabled = true;
    }



    

    public Arrowdirection GetActiveArrowDirection()
    {
        if (clickedTask != "" && arrows.ContainsKey(clickedTask))
        {
            return arrows[clickedTask].GetComponent<Arrowdirection>();
        }
        return null;
    }

}