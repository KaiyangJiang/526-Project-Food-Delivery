using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private double money;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI interactText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI displayMoneyText;
    public TextMeshProUGUI statusText;
    public Button restartButton;
    public Button startButton;
    public GameObject guidePanel;
    public GameObject GameOverPanel;
    public GameObject TreasureBoxPanel;
    public GameDataCollector gameDataCollector;
    public TextMeshProUGUI skillHint;
    public GameObject bagPanel;
    public GameObject skillRoll;
    public Image fruits;
    public Image chicken;
    public Image sushi;
    public Image hamburger;
    public Image fries;
    public Image popcorn;
    public Image pizzas;
    Dictionary<string, Image> foods = new Dictionary<string, Image>();
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

    public int pizza = 0;
    public bool isGameActive;
    public HashSet<string> itemsInHand = new HashSet<string>();
    // Start is called before the first frame update
    void Start()
    {
        statusText.text = "";
        startButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
        skillHint.gameObject.SetActive(false);
        GameOverPanel.SetActive(false);
        bagPanel.SetActive(false);
        skillRoll.SetActive(false);
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
        timerOn = true;
        isGameActive = true;
        showGameOver = false;
        showMoney = false;
    }

    // Update is called once per frame
    void Update()
    {
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
        foods.Add("Pizza",pizzas);
        foods.Add("Fried Chickens", chicken);
        foods.Add("Fruits", fruits);
        foods.Add("Hamburger", hamburger);
        foods.Add("Popcorn", popcorn);
        foods.Add("French Fries", fries);
        foods.Add("Sushi", sushi);
    }
    //handle when time is up and game
    void handleGameOver()
    {
        timeLeft = 0;
        timerOn = false;
        isGameActive = false;
        GameOverPanel.SetActive(true);
        showGameOver = true;
        showMoney = true;
        gameOverText.text = "Congraduations!";
        displayMoneyText.text = "You Earned: " + money.ToString() + " $";
        restartButton.gameObject.SetActive(true);
        gameDataCollector.goldCollected = (int)money;
        gameDataCollector.SendDataToGoogleForm();
    }

    public void treasureBoxButtonHandler(string button)
    {
        //handle left button
        if(button == "left")
        {
            AddMoney(10);
            timeLeft -= 15.0f;
            updateStatus("Money +10$, time -15s", 1, Color.yellow);
        }
        if(button == "right")
        {
            timeLeft += 15.0f;
            DecreaseMoney(10);
            updateStatus("Time +15s and Money +10$", 1, Color.green);
        }
        print("xxxxx: "+button);
        TreasureBoxPanel.SetActive(false);
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
        skillRoll.SetActive(true);
        gameStarted = true;
        guidePanel.SetActive(false);

        timeLeft = 180f;
    }
}
