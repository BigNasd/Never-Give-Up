using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{

    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;

    public Dictionary<string, PanelBase> panelDic = new Dictionary<string, PanelBase>();

    private Transform canvasTrans;


    private UIManager()
    {
        //得到场景上创建好的Canvas对象
        canvasTrans = GameObject.Find("Canvas").transform;
        GameObject.DontDestroyOnLoad(canvasTrans.gameObject);
    }
    public T ShowPanel<T>() where T : PanelBase
    {

        string panelName = typeof(T).Name;
        //是否已经有显示的面板了
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        //GameObject panelObj = null;
        //ResManager.GetInstance().LoadAsync<GameObject>("UI/NormalUI/" + panelName, (obj) =>
        //{
        //   panelObj = obj;
        //});
        panelObj.transform.SetParent(canvasTrans, false);
        //得到面板预设体的脚本
        T panel = panelObj.GetComponent<T>();
        //把得到的脚本存储起来
        panelDic.Add(panelName, panel);
        //调用显示自己的方法
        panel.ShowMe();
        return panel;
    }
    //isFade是就是希望淡出面板，false是直接删除面板
    public void HidePanel<T>( bool isFade = true ) where T : PanelBase
    {
        string panelName = typeof(T).Name;
        //判断当前字典里有没有该名字的面板
        if (panelDic.ContainsKey(panelName))
        {
            if (isFade)
            {
                panelDic[panelName].HideMe(() =>
                {
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }
    }
    public T GetPanel<T>() where T : PanelBase
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        return null;
    }
}
