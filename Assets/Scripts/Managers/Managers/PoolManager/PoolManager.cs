using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

public class PoolData
{
    public GameObject fatherObj;

    public Queue<GameObject> poolList;

    public PoolData(GameObject obj, GameObject poolObj)
    {
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;
        poolList = new Queue<GameObject>() { };
        PushObj(obj);
    }
    /// <summary>
    /// �Ѷ���ѹ�뻺���
    /// </summary>
    /// <param name="obj"></param>
    public void PushObj(GameObject obj)
    {
        poolList.Enqueue(obj);
        obj.SetActive(false);
        obj.transform.parent = fatherObj.transform;
    }
    /// <summary>
    /// �ӻ������ȡ������
    /// </summary>
    /// <returns></returns>
    public GameObject GetObj()
    {
        GameObject obj = poolList.Dequeue(); ;
        obj.SetActive(true);
        obj.transform.parent = null;
        return obj;
    }
} 

public class PoolManager : BaseManager<PoolManager>
{
    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();

    private GameObject poolObj;
    /// <summary>
    /// �ѻ������Ķ����ó��� 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetObj(string name, Transform transform, UnityAction<GameObject> callBack = null)
    {
        GameObject obj = null;
        if(poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
        {
            obj = poolDic[name].GetObj();
            obj.transform.position = transform.position;
            callBack?.Invoke(obj);
        }
        else
        {
            ResManager.GetInstance().LoadAsync<GameObject>(name, (o) =>
            {
                o.name = name;
                o.transform.position = transform.position;
                callBack?.Invoke(o);
            });
            //obj = GameObject.Instantiate(Resources.Load<GameObject>("EpicToonFX/Prefabs/" + name), transform);
            ////�Ѷ�������Ϊ�ͳ���һ��
            //obj.name = name;
        }
        return obj; 
    }
    /// <summary>
    /// �ѻ������Ķ����ó��� 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="vector3"></param>
    /// <param name="quaternion"></param>
    /// <returns></returns>
    public GameObject GetObj(string name, Vector3 vector3, Quaternion quaternion, UnityAction<GameObject> callBack = null)
    {
        GameObject obj = null;
        if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
        {
            obj = poolDic[name].GetObj();
            obj.transform.position = vector3;
            callBack?.Invoke(obj);
        }
        else
        {
            ResManager.GetInstance().LoadAsync<GameObject>(name, (o) =>
            {
                o.name = name;
                o.transform.position = vector3;
                o.transform.rotation = quaternion;
                callBack?.Invoke(o);
            });
            //obj = GameObject.Instantiate(Resources.Load<GameObject>("EpicToonFX/Prefabs/" + name), vector3, quaternion);
            ////�Ѷ�������Ϊ�ͳ���һ��
            //obj.name = name;
        }
        return obj;
    }
    /// <summary>
    /// �Ѷ���Ž������
    /// </summary>
    /// <param name="name"></param>
    /// <param name="obj"></param>
    public void PushObj(string name, GameObject obj)
    {
        if (poolObj == null)
            poolObj = new GameObject("Pool");
        if (poolDic.ContainsKey(name))
        {
            poolDic[name].PushObj(obj);
        }
        else
        {
            poolDic.Add(name, new PoolData(obj, poolObj));
        }
    }
    /// <summary>
    /// ��ջ����
    /// </summary>
    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
