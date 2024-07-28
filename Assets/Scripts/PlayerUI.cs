using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private const string TIMER_FORMAT = "{0:00}:{1:00}";

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI coinsText;

    private int uiLives;
    private int uiCoins;
    

    private float uiTimer;

    private void Start()
    {
        
        
        SetLives();
        SetCoins();
    }

    private void Update()
    {
        uiTimer = GameManager.Instance.timer;
        int minutes = Mathf.FloorToInt(uiTimer / 60);
        int seconds = Mathf.FloorToInt(uiTimer % 60);
        timeText.text = ("Time: " + string.Format(TIMER_FORMAT, minutes, seconds));
    }

    public void SetLives()
    {
        uiLives = GameManager.Instance.lives;
        livesText.text = ("Lives: " + uiLives);
        
    }

    public void SetCoins()
    {
        uiCoins = GameManager.Instance.coins;
        coinsText.text = ("Coins: " + uiCoins);
        
    }
}
