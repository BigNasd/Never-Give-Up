using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : PanelBase
{
    public Button back;

    public GameObject player;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player");
        MusicManager.GetInstance().PlaySound("Ê¤ÀûÒôÀÖ", false, 0.5f);
    }

    protected override void Start()
    {
        base.Start();
        back.onClick.AddListener(() =>
        {
            Destroy(player);
            EventCenter.GetInstance().EventTrigger("FindPoint");

            for(int i = 0; i < GameInfoData.Instance.corpse.Count; i++)
            {
                PoolManager.GetInstance().PushObj(GameInfoData.Instance.corpse[i].name, GameInfoData.Instance.corpse[i]);
            }
            GameInfoData.Instance.corpse.Clear();
            UIManager.Instance.ShowPanel<MenuPanel>();
            UIManager.Instance.HidePanel<WinPanel>();
        });
    }
}
