using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance {  get; private set; }

    private const string LADDER_START_HEX_VALUE = "683100";
    private const string LADDER_END_HEX_VALUE = "C46702";
    private const string PLAYER_START_HEX_VALUE = "670ECB";

    private Color ladderStartColor, ladderEndColor, playerStartColour;

    public Texture2D map;


    public ColorToPrefab[] colorMappings;

    public Transform player;

    [SerializeField] private Transform ladderMid;
    
    private Vector2 ladderStartPosition;
    private Vector2 ladderEndPosition;
    public Vector2 ladderSize;
    private SpriteRenderer ladderSpriteRenderer;
    private RectTransform ladderRectTransform;
    private void Awake()
    {
        Instance = this;
        ladderStartColor = GetColorFromString(LADDER_START_HEX_VALUE);
        ladderEndColor = GetColorFromString(LADDER_END_HEX_VALUE);
        GenerateLevel();
       
    }
    private void Start()
    {

        
        LadderSize();



    }

    private int HexToDec(string hex)
    {
        int dec = System.Convert.ToInt32(hex, 16);
        return dec;
    }

    private string DecToHex(int value)
    {
        return value.ToString("X2");
    }

    private string FloatNormalizedToHex(float value)
    {
        return DecToHex(Mathf.RoundToInt(value * 255f));
    }

    private float HexToFloatNormalized(string hex)
    {
        return HexToDec(hex) / 255f;
    }

    private Color GetColorFromString(string hexString)
    {
        float red = HexToFloatNormalized(hexString.Substring(0, 2));
        float green = HexToFloatNormalized(hexString.Substring (2, 2));
        float blue = HexToFloatNormalized(hexString.Substring(4, 2));
        return new Color(red, green, blue);
    }

    private void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }

    }

    private void GenerateTile(int x, int y)
    {
       Color pixelColour = map.GetPixel(x, y);

        if (pixelColour.a == 0)
        {
            // if pixel is transparent ignore
            return;
        }
        if (pixelColour == playerStartColour)
        {
            player.position = new Vector2(x, y);
        }

        if (pixelColour == ladderStartColor)
        {
            ladderStartPosition = new Vector2(x, y);
            
            Instantiate(ladderMid, ladderStartPosition, Quaternion.identity, transform);
        }
        if (pixelColour == ladderEndColor)
        {
            ladderEndPosition = new Vector2(x, y);
        }
        
        

        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color == pixelColour)
            {
                Vector2 position = new Vector2(x, y);
                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
               

            }
        }
    }

    public void LadderSize()
    {
        if (ladderMid.GetComponent<SpriteRenderer>() != null)
        {
            ladderSpriteRenderer = ladderMid.GetComponent<SpriteRenderer>();
            
            
        }
        
        ladderSize = ladderEndPosition - ladderStartPosition;
        ladderSpriteRenderer.size = new Vector2(ladderSpriteRenderer.size.x, ladderSize.y);
        
        ladderMid.position = new Vector3 (ladderMid.position.x, ladderSize.y / 2);

       
    }
}
