using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    [SerializeField] private Transform player;
    [SerializeField] private PlayerUI playerUI;
    public int lives = 5;

    public int coins = 0;
    
    public Vector3 playerPos;
    private Vector3 startingPos;

    public float timer;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        startingPos = player.position;
        playerPos = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    public void Checkpoint()
    {
        playerPos = player.position;
        
    }

    public void OnDeath()
    {
        if (lives > 0)
        {
            player.position = playerPos;
            lives--;
            playerUI.SetLives();
        }
        else
        {
            player.position = startingPos;
            lives = 5;
            playerUI.SetLives();
            timer = 0f;

        }
    }

    public void AddCoins()
    {
        coins++;
        playerUI.SetCoins();
    }

    
}
