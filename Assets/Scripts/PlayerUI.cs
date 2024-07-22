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
    

    private float timer;

    private void Start()
    {
        uiLives = GameManager.Instance.lives;
        uiCoins = GameManager.Instance.coins;
        SetLives();
        SetCoins();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timeText.text = ("Time: " + string.Format(TIMER_FORMAT, minutes, seconds));
    }

    public void SetLives()
    {
        livesText.text = ("Lives: " + uiLives);
    }

    public void SetCoins()
    {
        coinsText.text = ("Coins: " + uiCoins);
    }
}
