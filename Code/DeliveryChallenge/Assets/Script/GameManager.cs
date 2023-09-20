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
    public TextMeshProUGUI taskContentText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI displayMoneyText;
    public Button restartButton;

    public float timeLeft;
    public bool timerOn = false;
    public bool showText = false;
    public bool showPizza = false;
    public bool showDeliver = false;
    public bool showGameOver = false;
    public bool showMoney = false;

    public int pizza = 0;
    public bool isGameActive;
    // Start is called before the first frame update
    void Start()
    {
        restartButton.gameObject.SetActive(false);
        money = 0;
        timeLeft = 180.0f;
        moneyText.text = "$ " + money;
        interactText.text = "";
        gameOverText.text = "";
        displayMoneyText.text = "";
        updateTimer(timeLeft);
        timerOn = true;
        taskContentText.text = "Get a Pizza from a Pizza Store";
        isGameActive = true;
        showGameOver = false;
        showMoney = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn){
            if (timeLeft > 0){
                timeLeft -= Time.deltaTime;
                updateTimer(timeLeft);
            }else{ 
                handleTimeUp(); 
            }
        }

        if (showText)
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
        }


    }

    //add money to player
    public void AddMoney(double amount)
    {
        money += amount;
        moneyText.text = "$ " + money;
    }

    //add pizza
    public void AddPizza(int amount)
    {
        pizza += 1;
        taskContentText.text = "Find Auto Service and Delivery the Pizza";
    }

    //deliver pizza
    public void DeliverPizza(int amount)
    {
        pizza -= 1;
        AddMoney(20f);
        taskContentText.text = "You did it, Now Get More Pizzas to be Delivered!";
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
    }

    public void RestartGame()
    {
        Application.LoadLevel(0);
    }
}
