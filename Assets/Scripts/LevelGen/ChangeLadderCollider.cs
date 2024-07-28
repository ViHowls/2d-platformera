using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLadderCollider : MonoBehaviour
{
    private BoxCollider2D box;
    private SpriteRenderer ladderMidSpriteRenderer;
    [SerializeField] private Transform ladderMid;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        if (ladderMid.GetComponent<SpriteRenderer>())
        {
            ladderMidSpriteRenderer = ladderMid.GetComponent<SpriteRenderer>();
        }

        box.size = new Vector2(box.size.x, ladderMidSpriteRenderer.size.y);
        

    }
}
