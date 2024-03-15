using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMove : MonoBehaviour
{
    private Control player;

    public float speedUp;

    public float speedDown;

    private Animator animator;

    private bool canFire;

    private bool isShake;//是否震动

    private float currentShakeValue;//当前震动系数
    public float shakeValue = 2;//震动系数

    private void Awake()
    {
        canFire = true;

        player = transform.parent.GetComponent<Control>();

        
        //animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameInfoData.Instance.gameStart)
        {
            Gun();
            Fire();
        }
        if (isShake)
        {
            //先左右震动再上下震动
            transform.localPosition = new Vector3(transform.localPosition.x + (Random.Range(-currentShakeValue, currentShakeValue)), transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + (Random.Range(-currentShakeValue / 2, currentShakeValue / 2)), transform.localPosition.z);

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
    private void Gun()
    {
        if (player.rb.velocity.y > 0)
            transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(0,-0.8f), Mathf.Abs(player.rb.velocity.y)/5f * Time.deltaTime);
        else if (player.rb.velocity.y < 0)
            transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(0,0.8f), Mathf.Abs(player.rb.velocity.y)/3 * Time.deltaTime); 
        else if (player.rb.velocity.y == 0)
            transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(0, 0), speedUp * Time.deltaTime);
    }
    private void Fire()
    {
        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            PoolManager.GetInstance().GetObj("Effect/Fire", new Vector2(transform.position.x + 2.08f * player.faceDir.x, transform.position.y - 0.76f), Quaternion.identity, (o) =>
            {
                MusicManager.GetInstance().PlaySound("Shooting Gun  19", false, 0.3f);
                EventCenter.GetInstance().EventTrigger<float>("Shack",0.4f);
                isShake = true;
                o.transform.parent = transform;
                o.transform.localScale = new Vector3(1, 1, 1);
                //PoolManager.GetInstance().GetObj("Effect/Buttle", o.transform.position, Quaternion.identity);
                GetComponent<BulletPool>().GetBullet().transform.position = o.transform.position;
                PoolManager.GetInstance().GetObj("Effect/Danke", o.transform.position + new Vector3(-2.33f * player.faceDir.x, 0.33f,0), Quaternion.identity);
                player.rb.AddForce(-player.faceDir * player.Recoilforce);
            });
            //animator.SetTrigger("Fire");
            StartCoroutine(FireDelateTime());
            player.isFire = true;

        }
        if(!Input.GetMouseButton(0))
            player.isFire = false;
    }

    IEnumerator FireDelateTime()
    {
        yield return new WaitForSeconds(0.1f);
        canFire = true;
    }
}
