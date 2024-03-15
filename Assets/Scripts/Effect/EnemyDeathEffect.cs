using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathEffect : MonoBehaviour
{


    private void OnEnable()
    {
        EventCenter.GetInstance().EventTrigger<float>("Shack", 2f);
    }
}
