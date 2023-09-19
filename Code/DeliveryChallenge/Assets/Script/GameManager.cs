using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private double money;
    public TextMeshProUGUI moneyText;

    public float timeLeft;
    public bool timerOn = false;
    public TextMeshProUGUI timerText;
    // Start is called before the first frame update
    void Start()
    {
        money = 0;
        timeLeft = 30.0f;
        moneyText.text = "$ " + money;
        updateTimer(timeLeft);
        timerOn = true;
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
    }

    public void AddMoney(double amount)
    {
        money += amount;
        moneyText.text = "$ " + money;
        Debug.Log("Money:"+money);
    }


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
        Debug.Log("Time is UP!");
        timeLeft = 0;
        timerOn = false;
    }
}
