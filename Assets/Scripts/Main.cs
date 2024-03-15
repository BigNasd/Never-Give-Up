using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private void Awake()
    {
        //for(int i = 0; i < 20; i++)
        //{
        //    GameObject obj = Instantiate(Resources.Load<GameObject>("Effect/Buttle"));
        //    obj.name = "Effect/Buttle";
        //    PoolManager.GetInstance().PushObj(obj.name, obj);
        //}

        //EventCenter.GetInstance().EventTrigger<int>("GameStart",4);

        //UIManager.Instance.GetPanel<BeginPanel>();
    }

    private void Start()
    {

        //BeginPanel beginPanel = UIManager.Instance.ShowPanel<BeginPanel>();
        //beginPanel.transform.GetChild(0).GetComponent<Animator>().speed = 5.0f;
        //Time.timeScale = 0.2f;
        UIManager.Instance.ShowPanel<MenuPanel>();
    }
}
