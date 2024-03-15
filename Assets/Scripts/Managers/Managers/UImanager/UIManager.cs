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
        //�õ������ϴ����õ�Canvas����
        canvasTrans = GameObject.Find("Canvas").transform;
        GameObject.DontDestroyOnLoad(canvasTrans.gameObject);
    }
    public T ShowPanel<T>() where T : PanelBase
    {

        string panelName = typeof(T).Name;
        //�Ƿ��Ѿ�����ʾ�������
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
        //�õ����Ԥ����Ľű�
        T panel = panelObj.GetComponent<T>();
        //�ѵõ��Ľű��洢����
        panelDic.Add(panelName, panel);
        //������ʾ�Լ��ķ���
        panel.ShowMe();
        return panel;
    }
    //isFade�Ǿ���ϣ��������壬false��ֱ��ɾ�����
    public void HidePanel<T>( bool isFade = true ) where T : PanelBase
    {
        string panelName = typeof(T).Name;
        //�жϵ�ǰ�ֵ�����û�и����ֵ����
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
