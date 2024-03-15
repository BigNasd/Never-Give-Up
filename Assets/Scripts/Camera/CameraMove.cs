using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject player;

    private Animator anim;

    private bool isShake;//是否震动

    private float currentShakeValue;//当前震动系数
    public float shakeValue = 2;//震动系数

    public float offset;

    private void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Point");
        anim = GetComponent<Animator>();
        EventCenter.GetInstance().AddEventListener<float>("Shack",Shack);
        EventCenter.GetInstance().AddEventListener("FindPlayer", FindPlayer);
        EventCenter.GetInstance().AddEventListener("FindPoint", FindPoint);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + offset * player.transform.localScale.x, player.transform.position.y, transform.position.z),5 * Time.deltaTime);
        if (transform.position.y <= -2.4f)
            transform.position = new Vector3(transform.position.x, -2.4f, transform.position.z);
        if (transform.position.x <= -22.5f)
            transform.position = new Vector3(-22.5f, transform.position.y, transform.position.z);
        if (transform.position.x >= 22.5f)
            transform.position = new Vector3(22.5f, transform.position.y, transform.position.z);

        if (isShake)
        {
            //先左右震动再上下震动
            transform.position = new Vector3(transform.position.x + (Random.Range(-currentShakeValue, currentShakeValue)), transform.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x, transform.position.y + (Random.Range(-currentShakeValue/2, currentShakeValue/2)), transform.position.z);

            currentShakeValue /= 1.1f;//震动数值减少
            if (currentShakeValue <= 0.05f)
            {
                isShake = false;
                currentShakeValue = shakeValue;
            }
        }
        else
        {
            currentShakeValue = shakeValue;
        }
    }

    private void Shack(float shakeValue)
    {
        this.shakeValue = shakeValue;
        isShake = true;
    }

    private void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FindPoint()
    {
        player = GameObject.FindGameObjectWithTag("Point");
    }

}
