using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailPanel : PanelBase
{
    public Button back;

    public GameObject player;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player");
        GameInfoData.Instance.enemyNum = 0;
        GameInfoData.Instance.waveID = 1;
        GameInfoData.Instance.gameStart = false;
        GameInfoData.Instance.playerNum++;
    }

    protected override void Start()
    {
        base.Start();
        back.onClick.AddListener(() =>
        {
            Destroy(player);
            EventCenter.GetInstance().EventTrigger("FindPoint");

            for (int i = 0; i < GameInfoData.Instance.corpse.Count; i++)
            {
                PoolManager.GetInstance().PushObj(GameInfoData.Instance.corpse[i].name, GameInfoData.Instance.corpse[i]);
            }
            GameInfoData.Instance.corpse.Clear();
            GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemy.Length; i++)
            {
                PoolManager.GetInstance().PushObj(enemy[i].name, enemy[i]);
            }
            UIManager.Instance.ShowPanel<MenuPanel>();
            UIManager.Instance.HidePanel<FailPanel>();
        });
    }
}
