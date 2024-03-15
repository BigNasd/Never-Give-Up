using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatEnemy : MonoBehaviour
{

    private Vector3 One;
    private Vector3 Two;
    private Vector3 Three;
    private Vector3 Four;

    public bool isUpdate;

    private void Awake()
    {
        One = transform.GetChild(0).position;
        Two = transform.GetChild(1).position;
        Three = transform.GetChild(2).position;
        Four = transform.GetChild(3).position;

        EventCenter.GetInstance().AddEventListener<int>("GameStart", CreatEnemisePakeage);
    }

    private void Update()
    {
        if(GameInfoData.Instance.enemyNum <= 0 && GameInfoData.Instance.waveID > 1 && !isUpdate)
        {
            Invoke("NextWave", 2f);
            isUpdate = true;
        }
        print(GameInfoData.Instance.enemyNum);
    }


    private void CreatEnemisePakeage(int count)
    {
        StartCoroutine(CreatEnemise(count));
    }

    private IEnumerator CreatEnemise(int count)
    {
            while(count > 0)
            {
                yield return new WaitForSeconds(0.5f);
                if (GameInfoData.Instance.gameStart)
                {
                    PoolManager.GetInstance().GetObj("Enemy&Player/Enemy", One, Quaternion.identity);
                    PoolManager.GetInstance().GetObj("Enemy&Player/Enemy", Two, Quaternion.identity);
                    PoolManager.GetInstance().GetObj("Enemy&Player/Enemy", Three, Quaternion.identity);
                    PoolManager.GetInstance().GetObj("Enemy&Player/Enemy", Four, Quaternion.identity);
                    GameInfoData.Instance.enemyNum += 4;
                }
                count--;
            }
            GameInfoData.Instance.waveID++;
            if (GameInfoData.Instance.waveID >= 6)
                GameInfoData.Instance.waveID = 6;
            isUpdate = false;
    }

    private void NextWave()
    {
        if(GameInfoData.Instance.waveID >= 6)
        {
            //Ê¤Àû
            UIManager.Instance.ShowPanel<WinPanel>();
            GameInfoData.Instance.waveID = 1;
            GameInfoData.Instance.gameStart = false;
            GameInfoData.Instance.playerNum++;
            return;
        }
        BeginPanel beginPanel = UIManager.Instance.ShowPanel<BeginPanel>();
        beginPanel.transform.GetChild(0).GetComponent<Animator>().speed = 4.0f;
        beginPanel.waveText.text = "The" + GameInfoData.Instance.waveID + "Wave";
        Time.timeScale = 0.2f;
 
    } 
}
