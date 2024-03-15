using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : PanelBase
{
    public Button startBtn;

    public Button ExitBtn;

    protected override void Start()
    {
        base.Start();
        startBtn.onClick.AddListener(() =>
        {
            GameObject player = Instantiate<GameObject>(Resources.Load<GameObject>("Enemy&Player/Player"));
            EventCenter.GetInstance().EventTrigger("FindPlayer");
            GameInfoData.Instance.gameStart = true;
            BeginPanel beginPanel = UIManager.Instance.ShowPanel<BeginPanel>();
            beginPanel.transform.GetChild(0).GetComponent<Animator>().speed = 4.0f;
            beginPanel.waveText.text = "The" + GameInfoData.Instance.waveID + "Wave";
            Time.timeScale = 0.2f;
            UIManager.Instance.HidePanel<MenuPanel>(false);
        });
        ExitBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
