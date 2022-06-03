using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    Vector2Int currentTilePosition = new Vector2Int();
    [SerializeField] Vector2Int playerTilePosition;

    [SerializeField] float zeroZeroTileSpawnX = -3.52f;
    [SerializeField] float tileSpawnXCornerOffset = -1.76f; // X offset from the bottom left corner of the tile to the X spawn point of the tile
    [SerializeField] float zeroZeroTileSpawnY = -1.92f;
    [SerializeField] float tileSpawnYCornerOffset = -0.96f; // Y offset from the bottom left corner of the tile to the Y spawn point of the tile

    [SerializeField] float tileWidth = 3.52f;
    [SerializeField] float tileHeight = 1.92f;
    
    [SerializeField] float zeroZeroTileStartX;
    [SerializeField] float zeroZeroTileStartY;

    GameObject[,] terrainTiles;

    [SerializeField] int terrainTileHorizontalCount;
    [SerializeField] int terrainTileVerticalCount;

    [SerializeField] int fieldOfVisionHeight = 3;
    [SerializeField] int fieldOfVisionWidth = 3;

    private int logMod = 240;
    private int logCount = 0;

    private void Awake()
    {
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }

    public void Add(GameObject gameObject, Vector2Int tilePosition)
    {
        terrainTiles[tilePosition.x, tilePosition.y] = gameObject;
        //Debug.Log("Added tile position " + tilePosition.x + ", " + tilePosition.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTilePosition.x = 1;
        currentTilePosition.y = 1;

        zeroZeroTileStartX = zeroZeroTileSpawnX + tileSpawnXCornerOffset; // The bottom left corner of tile 0,0
        zeroZeroTileStartY = zeroZeroTileSpawnY + tileSpawnYCornerOffset; // The bottom left corner of tile 0,0
    }

    // Update is called once per frame
    void Update()
    {
        logCount++;
        float playerXDistanceFromZeroZero = playerTransform.position.x - zeroZeroTileStartX;
        float playerYDistanceFromZeroZero = playerTransform.position.y - zeroZeroTileStartY;
        playerTilePosition.x = (int)(playerXDistanceFromZeroZero / tileWidth);
        playerTilePosition.y = (int)(playerYDistanceFromZeroZero / tileHeight);

        if (logCount % logMod == 0)
        {
            //Debug.Log(playerXDistanceFromZeroZero);
            //Debug.Log(playerYDistanceFromZeroZero);
            //Debug.Log("playerTilePosition x: " + playerTilePosition.x + ", y: " + playerTilePosition.y);
        }

        playerTilePosition.x -= playerTransform.position.x < zeroZeroTileStartX ? 1 : 0;
        playerTilePosition.y -= playerTransform.position.y < zeroZeroTileStartY ? 1 : 0;

        if (logCount % logMod == 0)
        {
            //Debug.Log("playerTilePosition x: " + playerTilePosition.x + ", y: " + playerTilePosition.y);
        }

        if (currentTilePosition != playerTilePosition)
        {
            //Debug.Log("here1");
            currentTilePosition = playerTilePosition;
            UpdateTilesOnScreen();
        }
    }

    private void UpdateTilesOnScreen()
    {
        for (int pov_x = -fieldOfVisionWidth; pov_x <= fieldOfVisionWidth; pov_x++)
        {
            for (int pov_y = -fieldOfVisionHeight; pov_y <= fieldOfVisionHeight; pov_y++)
            {
                //Debug.Log("pov_x: " + pov_x + ", " + "pov_y: " + pov_y);
                int tileToUpdate_x = CalculatePositionOnAxis(playerTilePosition.x + pov_x, true);
                int tileToUpdate_y = CalculatePositionOnAxis(playerTilePosition.y + pov_y, false);

                //Debug.Log("tileToUpdate x: " + tileToUpdate_x + ", y:" + tileToUpdate_y);

                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];
                //Debug.Log("playerTilePosition x: " + playerTilePosition.x + ", playerTilePosition.y: " + playerTilePosition.y);
                //Debug.Log("x: " + playerTilePosition.x + pov_x + ", y: " + playerTilePosition.y + pov_y); 
                tile.transform.position = CalculateTilePosition(playerTilePosition.x + pov_x, playerTilePosition.y + pov_y);
            }
        }
    }

    private Vector3 CalculateTilePosition(int x, int y)
    {
        //Debug.Log("CalculateTilePosition x: " + x + ", y:" + y);
        Vector3 newPos = new Vector3((x * tileWidth) + zeroZeroTileSpawnX, (y * tileHeight) + zeroZeroTileSpawnY, 0f);
        //Debug.Log("x: " + newPos.x + ", y: " + newPos.y);
        return newPos;
    }

    private int CalculatePositionOnAxis(float currentValue, bool horizontal)
    {

        if (horizontal)
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileHorizontalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainTileHorizontalCount -1 + currentValue % terrainTileHorizontalCount;
            }
        }
        else
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileVerticalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainTileVerticalCount - 1 + currentValue % terrainTileVerticalCount;
            }
        }

        return (int)currentValue;
    }
}
