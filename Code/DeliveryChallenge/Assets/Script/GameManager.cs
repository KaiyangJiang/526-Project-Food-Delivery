using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
public class GameManager : MonoBehaviour
{
    private double money;
    private WeaponManager weaponManager;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI interactText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI displayMoneyText;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI leftBoxText;
    public TextMeshProUGUI rightBoxText;
    public Button restartButton;
    public Button startButton;
    public GameObject guidePanel;
    public GameObject GameOverPanel;
    public GameObject TreasureBoxPanel;
    public GameObject miniMap;
    public GameDataCollector gameDataCollector;
    public GameObject bagPanel;
    public GameObject bagGrid;
    public GameObject taskPanel;
    public GameObject skillRoll;
    public Timer timer;
    public Image fruits;
    public Image chicken;
    public Image sushi;
    public Image hamburger;
    public Image fries;
    public Image popcorn;
    public Image pizzas;
    Dictionary<string, Image> foodsImages = new Dictionary<string, Image>();
    Dictionary<string, int> foodsImageIndex = new Dictionary<string, int>();
    public float timeLeft;
    public float statusTime;
    public double GoalMoney;
    public bool gameStarted = false;
    public bool timerOn = false;
    public bool showText = false;
    public bool showPizza = false;
    public bool showDeliver = false;
    public bool showGameOver = false;
    public bool showMoney = false;
    public string statusTextInput = "";
    public bool isInVisible = false;
    public bool pause = false;
    public int pizza = 0;
    public bool isGameActive;
    public bool inTresureBox = false;
    public bool inTutorial = false;
    public HashSet<string> itemsInHand = new HashSet<string>();
    public List<string> treasureBoxItems = new List<string>() { "+10$\n-15s", "+15s \n-10$", "+Mechine Gun\n-10$", "+Hand Gun\n-10$", "+UZI\n-10$"};
    public List<int> treasureBoxIndexes = new List<int>() {0,1};
    // Start is called before the first frame update
    // tutorial variables
    public GameObject tutorialPanel;
    public GameObject tutorialTextPanel;
    public GameObject skipbutton;
    public GameObject finishTutorialPanel;
    public GameObject deliveryMan;
    public TextMeshProUGUI tutorialMissionText;
    public float transparency = 0.5f;
    public List<string> tutorialItems;
    public List<int> tutorialItemInd;
    void Start()
    {
        print("xxx start");
        if (tutorialTextPanel)
        {
            tutorialTextPanel.SetActive(false);
        }
        if (skipbutton)
        {
            skipbutton.SetActive(false);
        }
        if (finishTutorialPanel)
        {
            finishTutorialPanel.SetActive(false);
        }
        tutorialItemInd = new List<int>(){0,0,0,0,0,0,0,1,0};
        tutorialItems = new List<string>() {
        "Tutorial: Follow arrow and Pick up food",
        "Tutorial: Now try to run into the box",
        "Tutorial: Pick one effect from box",
        "Tutorial: Follow arrow and deliver food",
        "Tutorial: Try to defeat the enemy (Hint: Box may have weapon)",
        "Tutorial: Find teleport door",
        "Tutorial: Click on map to teleport",
        "Congrats you have finished tutorial",
        "Tutorial: You can try to click the task to get direction"};
        //guidePanel.SetActive(false);
        //inTutorial = true;
        weaponManager = GameObject.Find("WeaponManager").GetComponent<WeaponManager>();
        timer = GameObject.Find("DialSeconds").GetComponent<Timer>();
        statusText.text = "";
        startButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
        GameOverPanel.SetActive(false);
        bagPanel.SetActive(false);
        taskPanel.SetActive(false);
        skillRoll.SetActive(false);
        miniMap.SetActive(false);
        TreasureBoxPanel.SetActive(false);
        initializeBagDic();
        money = 0;
        statusTime = 181.0f;
        timeLeft = 180.0f;
        GoalMoney = 100;
        moneyText.text = "Goal: $" + money + "/ $"+GoalMoney;
        interactText.text = "";
        gameOverText.text = "";
        displayMoneyText.text = "";
        updateTimer(timeLeft);
        timerOn = false;
        isGameActive = true;
        showGameOver = false;
        showMoney = false;
        assignTreasureBox();
    }

    // Update is called once per frame
    void Update()
    {
        string combinedString = string.Join(", ", tutorialItemInd);
        print("xxx here");
        print("xxx status text list: " + combinedString);
        
        if(tutorialItemInd.All(n => n == 1)){
            finishTutorialPanel.SetActive(true);
        }
        if (gameStarted)
        {
            startButton.gameObject.SetActive(false);
        }
        if (timerOn){
            if (timeLeft > 0){
                timeLeft -= Time.deltaTime;
                updateTimer(timeLeft);
            }
            else{
                handleGameOver(); 
            }
        }
        if (money >= GoalMoney)
        {
            handleGameOver();
        }

        if (timeLeft>statusTime){
            statusText.text = statusTextInput;
        }
        else
        {
            statusText.text = "";
        }


    }

    //add money to player
    public void AddMoney(double amount)
    {
        money += amount;
        moneyText.text = "Goal: $" + money + "/ $" + GoalMoney;
        
    }

    public void SetMoney(double amount)
    {
        money = amount;
        moneyText.text = "Goal: $" + money + "/ $" + GoalMoney;

    }

    public void DecreaseMoney(double amount)
    {
        money -= amount;
        moneyText.text = "Goal: $" + money + "/ $" + GoalMoney;
    }
    public double GetMoney()
    {
        return money;
    }
    public void showHint(string method, string title)
    {
        interactText.text = "Press E To " + method + " " + title + "!";
    }
    
    public void unshowHint()
    {
        interactText.text = "";
    }

    //count down for timer
    void updateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (currentTime < 10f)
        {
            timerText.color = Color.red;
        }
    }
    void initializeBagDic() {
        foodsImages.Add("Pizza",pizzas);
        foodsImages.Add("Fried Chicken", chicken);
        foodsImages.Add("Fruits", fruits);
        foodsImages.Add("Hamburger", hamburger);
        foodsImages.Add("Popcorn", popcorn);
        foodsImages.Add("French Fries", fries);
        foodsImages.Add("Sushi", sushi);
        //Instantiate(sushi, bagGrid);
    }

    public void addToBag(string title)
    {
        Image newImageInstance = Instantiate(foodsImages[title], bagGrid.transform);
        newImageInstance.gameObject.name = title;
    }

    public void removeFromBag(string title)
    {
        foreach (Transform child in bagGrid.transform)
        {
            if (child.name == title)
            {
                Destroy(child.gameObject);
                break; 
            }
        }
    }
    
    //handle when time is up and game
    void handleGameOver()
    {
        if (isGameActive)
        {
            gameDataCollector.goldCollected = (int)money;
            gameDataCollector.SendDataToGoogleForm();
        }
        timeLeft = 0;
        timerOn = false;
        isGameActive = false;
        GameOverPanel.SetActive(true);
        showGameOver = true;
        showMoney = true;
        gameOverText.text = "Congraduations!";
        displayMoneyText.text = "You Earned: " + money.ToString() + " $";
        restartButton.gameObject.SetActive(true);
        
    }

    public void treasureBoxButtonHandler(string button)
    {
        //handle left button
        //this.Add("M1911");
        //this.Add("AK74");
        //this.Add("Uzi");

        if (button == "left")
        {
            print("xxx" + leftBoxText.text);
            assignTreasureEffects(treasureBoxIndexes[0]);
        }
        if(button == "right")
        {
            print("xxx" + rightBoxText.text);
            assignTreasureEffects(treasureBoxIndexes[1]);
        }
        print("xxxxx: "+button);
        assignTreasureBox();
        timerOn = true;
        inTresureBox = false;
        TreasureBoxPanel.SetActive(false);
    }

    public void assignTreasureBox()
    {
        int leftRandom = Random.Range(0, 4);
        leftBoxText.text = treasureBoxItems[leftRandom];
        int rightRandom = Random.Range(0, 4);
        while (rightRandom == leftRandom)
        {
            rightRandom = Random.Range(0, 4);
        }
        rightBoxText.text = treasureBoxItems[rightRandom];
        treasureBoxIndexes[0] = leftRandom;
        treasureBoxIndexes[1] = rightRandom;
        print("xxx " + treasureBoxIndexes);
    }
    public void setInvisible()
    {
        Renderer renderer = deliveryMan.GetComponent<Renderer>();
        timer.setTimerRunning(true);
        if (renderer != null)
        {
            Color color = renderer.material.color;
            color.a = transparency; // Set the alpha value
            renderer.material.color = color; // Apply the new color with updated alpha
            isInVisible = true;
        }
    }
    public void setVisible()
    {
        Renderer renderer = deliveryMan.GetComponent<Renderer>();
        if (renderer != null)
        {
            Color color = renderer.material.color;
            color.a = 1.0f; // Set the alpha value
            renderer.material.color = color; // Apply the new color with updated alpha
            isInVisible = false;
        }
    }
    public void assignTreasureEffects(int num)
    {
        if (num == 0)
        {
            AddMoney(10);
            timeLeft -= 15.0f;
            updateStatus("Money +10$, time -15s", 1, Color.yellow);
        }
        else if(num == 1)
        {
            timeLeft += 15.0f;
            DecreaseMoney(10);
            updateStatus("Time +15s and Money +10$", 1, Color.green);
        }
        else if(num == 2)
        {
            weaponManager.Add("AK74");
            DecreaseMoney(10);
            updateStatus("+Mechine Gun and Money -10$", 1, Color.green);
        }else if (num == 3)
        {
            weaponManager.Add("M1911");
            DecreaseMoney(10);
            updateStatus("+Hand Gun and Money -10$", 1, Color.green);
        }
        else if (num == 4)
        {
            weaponManager.Add("Uzi");
            DecreaseMoney(10);
            updateStatus("+UZI and Money -10$", 1, Color.green);
        }
        updateTutorial(8);
    }
    public void openMap()
    {
        if (miniMap.activeSelf)
        {
            miniMap.SetActive(false);
        }
        else
        {
            miniMap.SetActive(true);
        }
    }

    public void updateStatus(string Text,float TimeDuration,Color color)
    {
        statusTextInput = Text;
        statusText.color = color;
        statusTime = timeLeft - TimeDuration;
        
    }
    public void RestartGame()
    {
        Application.LoadLevel(0);
    }

    public void StartGame()
    {
        startButton.gameObject.SetActive(false);
        bagPanel.SetActive(true);
        taskPanel.SetActive(true);
        skillRoll.SetActive(true);
        gameStarted = true;
        guidePanel.SetActive(false);
        timerOn = true;
        timeLeft = 180f;
    }

    public void SkipTutorial()
    {
        SceneManager.LoadScene(1);
        tutorialPanel.SetActive(false);
        StartGame();
        
    }

    public void StartTutorial()
    {
        inTutorial = true;
        tutorialPanel.SetActive(false);
        skipbutton.SetActive(true);
        StartGame();
        tutorialTextPanel.SetActive(true);
        tutorialMissionText.text = "Tutorial: Follow arrow and Pick up food";
        timerOn = false;
    }

    public void updateTutorial(int cur)
    {
        
        if (tutorialMissionText)
        {
            print("xxx status text: " + tutorialItems[cur]);
            print("xxx status text count: "+tutorialItems.Count);
            print("xxx status text next: " + tutorialMissionText.text);
            for (int i = 0; i < tutorialItems.Count; i++)
            {
                if (tutorialMissionText)
                {
                    if (tutorialItems[i] == tutorialMissionText.text)
                    {
                        tutorialItemInd[i] = 1;
                    }
                }

            }
            tutorialMissionText.text = tutorialItems[cur];
        }
        
    }
}
