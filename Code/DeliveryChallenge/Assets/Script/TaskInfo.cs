using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskInfo
{
    public string title;
    public string description;
    public float money;
    public Color color;
    public Vector3 getPosition;
    public Vector3 deliverPosition;
    public Quaternion getRotation;
    public Quaternion deliverRotation;
    //public string destination;

    public TaskInfo()
    {

    }
}

public class GetInfo
{
    public string title;
    public float money;
    public Color color;
    public Vector3 getPosition;
    public Quaternion getRotation;
    public GetInfo(string title, float money, Color color, Vector3 getPosition, Quaternion getRotation)
    {
        this.title = title;
        this.money = money;
        this.color = color;
        this.getPosition = getPosition;
        this.getRotation = getRotation;
    }

}

public class DeliverInfo
{
    public Vector3 deliverPosition;
    public Quaternion deliverRotation;
    public DeliverInfo()
    {
        
    }
}

public class TaskData
{
    public string[] taskTitles =
    {
        "Pizza", "Fried Chicken"
    };
    
    public string[] taskGetName =
    {
        "Pizza Store",
        "Fried Chicken Store"
    };
    
    public string[] taskDestination =
    {
        "Auto Service",
        "Music Store"
    };
    Dictionary<string, int> taskTitlesIndex = new Dictionary<string, int>();
    Dictionary<string, int> taskDestinationIndex = new Dictionary<string, int>();
    
    
    
    public float[] taskMoney = {10, 20, 30, 40, 50, 60, 70, 80, 90, 100};
    public Color[] taskColors = {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow,
        Color.cyan,
        Color.magenta,
        Color.white,
        Color.grey,
        Color.black,
        new Color(0.5f, 0.25f, 0.75f)  
    };

    public Vector3[] taskGetPositions =
    {
        new Vector3(0.5199966f, -3.69f, 7.28f),
        new Vector3(-15.43f, -3.69f, -36.27f),
        new Vector3(0.5199966f, -3.69f, 7.28f),
        new Vector3(0.5199966f, -3.69f, 7.28f),
        new Vector3(0.5199966f, -3.69f, 7.28f),
        new Vector3(0.5199966f, -3.69f, 7.28f),
        new Vector3(0.5199966f, -3.69f, 7.28f),
        new Vector3(0.5199966f, -3.69f, 7.28f),
        new Vector3(0.5199966f, -3.69f, 7.28f),
        new Vector3(0.5199966f, -3.69f, 7.28f),
    };

    public Quaternion[] taskGetRotations =
    {
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
    };

    public Quaternion[] taskDeliverRotations =
    {
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
    };
    
    
    
    public Vector3[] taskDeliverPositions =
    {
        new Vector3(-43.13f, -3.88f, -92.776f), //Auto Service
        new Vector3(-11.97f, -3.79f, -95.98f), //Music Store
        new Vector3(-43.13f, -3.88f, -92.776f),
        new Vector3(-43.13f, -3.88f, -92.776f),
        new Vector3(-43.13f, -3.88f, -92.776f),
        new Vector3(-43.13f, -3.88f, -92.776f),
        new Vector3(-43.13f, -3.88f, -92.776f),
        new Vector3(-43.13f, -3.88f, -92.776f),
        new Vector3(-43.13f, -3.88f, -92.776f),
        new Vector3(-43.13f, -3.88f, -92.776f),
    };
    public int TASK_NUM;

    public TaskData()
    {
        TASK_NUM = taskTitles.Length;
        for(int i = 0; i < TASK_NUM; i++)
        {
            taskTitlesIndex.Add(taskTitles[i], i);
            taskDestinationIndex.Add(taskDestination[i], i);
        }
    }
    
    public TaskInfo getTaskInfo(string title)
    {
        int index = taskTitlesIndex[title];
        //int destinationIndex = taskDestinationIndex[destination];
        TaskInfo taskInfo = new TaskInfo();
        taskInfo.title = taskTitles[index];
        taskInfo.description = "Pick a " + title + " at the " + taskGetName[index];
        taskInfo.money = taskMoney[index];
        taskInfo.color = taskColors[index];
        taskInfo.getPosition = taskGetPositions[index];
        taskInfo.deliverPosition = taskDeliverPositions[index];
        taskInfo.getRotation = taskGetRotations[index];
        taskInfo.deliverRotation = taskDeliverRotations[index];
        //taskInfo.destination = destination;
        
        return taskInfo;
    }
        
}
