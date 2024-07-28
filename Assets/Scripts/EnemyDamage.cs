using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private int playerCoins;
    // Start is called before the first frame update
    void Start()
    {
        playerCoins = GameManager.Instance.coins;
    }

   public void TakeDamage()
    {
        if (playerCoins < 5)
        {
            playerCoins = 0;
        }
        else
        {
            playerCoins -= 5;
            GameManager.Instance.coins = playerCoins;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            TakeDamage();
        }
    }
}
