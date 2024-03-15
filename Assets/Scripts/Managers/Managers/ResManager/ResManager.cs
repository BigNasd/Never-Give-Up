using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 资源加载模块
/// </summary>
public class ResManager : BaseManager<ResManager>
{
    /// <summary>
    /// 同步加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T Load<T>(string name) where T : Object
    {
        T res = Resources.Load<T>(name);
        if (res is GameObject)
            return GameObject.Instantiate(res);
        else
            return res;
    }

    public void LoadAsync<T>(string name, UnityAction<T> callBack = null) where T : Object
    {
        MonoManager.GetInstance().StartCoroutine(ReallyLoadAsync(name, callBack));
    }

    public IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callBack)where T : Object
    {
        ResourceRequest r = Resources.LoadAsync<T>(name);
        yield return r;
        if (r.asset is GameObject)
            callBack(GameObject.Instantiate(r.asset) as T);
        else
            callBack(r.asset as T);
    }
}
