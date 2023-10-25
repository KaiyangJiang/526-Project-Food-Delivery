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
    public GameDataCollector gameDataCollector;
    public TextMeshProUGUI skillHint;

    public float timeLeft;
    public float statusTime;
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
        money = 0;
        statusTime = 181.0f;
        timeLeft = 180.0f;
        moneyText.text = "$ " + money;
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
                handleTimeUp(); 
            }
        }
        if (timeLeft>statusTime){
            statusText.text = statusTextInput;
        }
        else
        {
            statusText.text = "";
        }
        /*if (showText)
        {
            if (showPizza)
            {
                interactText.text = "Press E To Get Pizza!";
            }
            else if (showDeliver)
            {
                interactText.text = "Press E To Deliver Pizza!";
            }
            else
            {
                interactText.text = "";
            }
            
        }
        else
        {
            interactText.text = "";
        }*/


    }

    //add money to player
    public void AddMoney(double amount)
    {
        money += amount;
        moneyText.text = "$ " + money;
        
    }

    public void SetMoney(double amount)
    {
        money = amount;
        moneyText.text = "$ " + money;

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

    //handle when time is up
    void handleTimeUp()
    {
        timeLeft = 0;
        timerOn = false;
        isGameActive = false;
        showGameOver = true;
        showMoney = true;
        gameOverText.text = "Game Over";
        displayMoneyText.text = "You Earned: " + money.ToString() + " $";
        restartButton.gameObject.SetActive(true);
        gameDataCollector.goldCollected = (int)money;
        gameDataCollector.SendDataToGoogleForm();
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
        gameStarted = true;
        guidePanel.SetActive(false);
        timeLeft = 180f;
    }
}
