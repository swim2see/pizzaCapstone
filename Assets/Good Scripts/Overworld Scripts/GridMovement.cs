using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public TileType[] tileTypes;

    public int[,] tiles;

    public int mapSizeX = 10;
    public int mapSizeY = 10;

    void Start()
    {
        tiles = new int [mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                tiles[x, y] = 0; 
            }
        }

//        tiles[4, 4] = 1;
        tiles[5, 4] = 1;
    }

    public void GenerateMapVisual()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tt = tileTypes[tiles[x, y]];

                Instantiate(tt.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}
