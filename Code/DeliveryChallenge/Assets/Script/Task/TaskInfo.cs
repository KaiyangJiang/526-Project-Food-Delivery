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
    public string destination;

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
        "Pizza", "Fried Chicken","Fruits","French Fries","Sushi","Hamburger","Popcorn"
    };
    
    public string[] taskGetName =
    {
        "Pizza Store",
        "Fried Chicken Store",
        "Fruits Store",
        "New Fast Food",
        "Restaurant",
        "Bar",
        "Supermarket"
    };
    
    public string[] taskDestination =
    {
        "Auto Service",
        "Music Store",
        "Clothing Store",
        "Green Apartment",
        "Green House",
        "Red House",
        "Yellow Apartment",
        
    };
    Dictionary<string, int> taskTitlesIndex = new Dictionary<string, int>();
    Dictionary<string, int> taskDestinationIndex = new Dictionary<string, int>();
    
    
    
    public float[] taskMoney = {20, 20, 20, 20, 20, 20, 20, 20, 20, 20};
    public Color[] taskColors = {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow,
        Color.cyan,
        Color.magenta,
        new Color(0.5f, 0.25f, 0.75f),
        Color.grey,
        Color.black,
        new Color(0.5f, 0.25f, 0.75f)  
    };

    public Vector3[] taskGetPositions =
    {
        new Vector3(0.5199966f, -3.69f, 7.28f), //Pizza Store
        new Vector3(-15.43f, -3.69f, -36.27f), //Fried Chicken Store
        new Vector3(-79.90f, -3.79f, -94.60f), //Fruits Store
        new Vector3(-2.09f, -3.75f, -36.23f), //New Fast Food
        new Vector3(-6.2f, -3.75f, -52.8f), //Restaurant
        new Vector3(-41.48f, -3.75f, -52.8f), //Bar
        new Vector3(-60.8f, -3.75f, -5.1f), // Supermarket
        new Vector3(0.5199966f, -3.69f, 7.28f),
        new Vector3(0.5199966f, -3.69f, 7.28f),
        new Vector3(0.5199966f, -3.69f, 7.28f),
    };

    public Quaternion[] taskGetRotations =
    {
        Quaternion.Euler(0,0,0),//Pizza Store
        Quaternion.Euler(0,0,0),//Fried Chicken Store
        Quaternion.Euler(0,45,0),//Fruits Store
        Quaternion.Euler(0,0,0),//New Fast Food
        Quaternion.Euler(0,0,0),//Restaurant
        Quaternion.Euler(0,0,0),//Bar
        Quaternion.Euler(0,0,0),// Supermarket
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
    };

    public Quaternion[] taskDeliverRotations =
    {
        Quaternion.Euler(0,0,0), //Auto
        Quaternion.Euler(0,0,0), //Music
        Quaternion.Euler(0,45,0), //Clothing
        Quaternion.Euler(0,90,0), //Green Apartment
        Quaternion.Euler(0,90,0), //Green House
        Quaternion.Euler(0,0,0), //Red House
        Quaternion.Euler(0,0,0), //Yellow Apartment
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
        Quaternion.Euler(0,0,0),
    };
    
    
    
    public Vector3[] taskDeliverPositions =
    {
        new Vector3(-43.13f, -3.88f, -92.776f), //Auto Service
        new Vector3(-11.97f, -3.79f, -95.98f), //Music Store
        new Vector3( 20.0f, -3.79f, 5.98f), //clothing store
        new Vector3(-82.24f, -3.75f, -62.81f),//Green Apartment
        new Vector3(-38.4f, -3.75f, -71.7f), //Green House
        new Vector3(-72.9f, -3.75f, -24.41f), //Red House
        new Vector3(-60.8f, -3.75f, -52.2f), // Yellow Apartment
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
    
    public TaskInfo getTaskInfo(string title,string destination)
    {
        int index = taskTitlesIndex[title];
        int destinationIndex = taskDestinationIndex[destination];
        TaskInfo taskInfo = new TaskInfo();
        taskInfo.title = taskTitles[index];
        taskInfo.description = "Pick a " + title + " at the " + taskGetName[index];
        taskInfo.money = taskMoney[index];
        taskInfo.color = taskColors[index];
        taskInfo.getPosition = taskGetPositions[index];
        taskInfo.deliverPosition = taskDeliverPositions[destinationIndex];
        taskInfo.getRotation = taskGetRotations[index];
        taskInfo.deliverRotation = taskDeliverRotations[destinationIndex];
        taskInfo.destination = destination;
        
        return taskInfo;
    }

    public Vector3 GetPosition(string title, bool isGet) {
        if(isGet) {
            return taskGetPositions[taskTitlesIndex[title]];
        }
        return taskDeliverPositions[taskDestinationIndex[title]];
    }
    
    public Color GetColor(string title) {
        return taskColors[taskTitlesIndex[title]];
    }
}
