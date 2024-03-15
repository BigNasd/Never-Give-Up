using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfoData
{
    private static GameInfoData instance = new GameInfoData();
    public static GameInfoData Instance => instance;

    public int waveID;

    public int[] waveQuantity;

    public bool gameStart;

    public int enemyNum;

    public List<GameObject> corpse;

    public int playerNum;

    GameInfoData() 
    {
        playerNum = 0;

        enemyNum = 0;

        gameStart = false;

        waveID = 1;

        waveQuantity = new int[] { 4, 5, 6, 7, 8};

        corpse = new List<GameObject>();
    }
}
