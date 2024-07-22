using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderManager : MonoBehaviour
{
    public static LadderManager instance;
    public Transform ladderStart;
    public Transform ladderEnd;
    public Vector2 ladderSize;
    [SerializeField] private Transform ladder;

    private void Awake()
    {
        instance = this;
        
        
    }

    private void Start()
    {
        ladderSize = ladderStart.position - ladderEnd.position;
        Instantiate(ladder, ladderStart.position, Quaternion.identity);
    }
}
