using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObj : MonoBehaviour
{
    private float Time;
    public float temp;
    private void OnEnable()
    {
        Time = temp;
        Invoke("PushMyself", Time);
    }

    void PushMyself()
    {
        PoolManager.GetInstance().PushObj(gameObject.name, gameObject);
    }
}
