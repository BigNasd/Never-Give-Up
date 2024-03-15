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
    /// 把对象压入缓存池
    /// </summary>
    /// <param name="obj"></param>
    public void PushObj(GameObject obj)
    {
        poolList.Enqueue(obj);
        obj.SetActive(false);
        obj.transform.parent = fatherObj.transform;
    }
    /// <summary>
    /// 从缓存池中取出对象
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
    /// 把缓存池里的对象拿出来 
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
            ////把对象名改为和池子一样
            //obj.name = name;
        }
        return obj; 
    }
    /// <summary>
    /// 把缓存池里的对象拿出来 
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
            ////把对象名改为和池子一样
            //obj.name = name;
        }
        return obj;
    }
    /// <summary>
    /// 把对象放进缓存池
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
    /// 清空缓存池
    /// </summary>
    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
