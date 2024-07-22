using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLadderSize : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(spriteRenderer.size.x, LevelGenerator.Instance.ladderSize.y);
        Debug.Log(spriteRenderer.size.y);
    }
}
