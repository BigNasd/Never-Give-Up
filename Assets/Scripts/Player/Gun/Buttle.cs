using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttle : MonoBehaviour
{
    public float faceDir;

    public Vector2 dir;

    public float speed;

    private Control player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Control>();
    }

    private void OnEnable()
    {
        faceDir = player.transform.localScale.x;
        transform.localScale = new Vector3(faceDir * 1.25f, 1.25f, 1.25f);
        dir = new Vector2(8 * faceDir, Random.Range(1f, -1f));
        float angle = Vector3.Angle(new Vector2(faceDir, 0), dir);
        if(faceDir > 0)
            if (dir.y < 0)
                transform.eulerAngles = new Vector3(0, 0, -angle);
            else
                transform.eulerAngles = new Vector3(0, 0, angle);
        else
            if (dir.y < 0)
                transform.eulerAngles = new Vector3(0, 0, angle);
            else
                transform.eulerAngles = new Vector3(0, 0, -angle);
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        transform.position += (Vector3)dir.normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Ground") || collision.CompareTag("Wall"))
        //{
        //    PoolManager.GetInstance().GetObj("Effect/Baozha", transform.position, Quaternion.identity, (o) =>
        //    {
        //        //PoolManager.GetInstance().PushObj(gameObject.name, gameObject);
        //        player.transform.GetChild(0).GetComponent<BulletPool>().ReturnBullet(gameObject);
        //    });
        //}
        float temp = Random.Range(0, 100);
        if(temp >= 30)
            PoolManager.GetInstance().GetObj("Effect/Baozha", transform.position, Quaternion.identity, (o) =>
            {
                //PoolManager.GetInstance().PushObj(gameObject.name, gameObject);
                player.transform.GetChild(0).GetComponent<BulletPool>().ReturnBullet(gameObject);
            });
        else
        {
            PoolManager.GetInstance().GetObj("Effect/BaozhaTwo", transform.position, Quaternion.identity, (o) =>
            {
                //PoolManager.GetInstance().PushObj(gameObject.name, gameObject);
                MusicManager.GetInstance().PlaySound("Explosion 37", false, 0.4f);
                player.transform.GetChild(0).GetComponent<BulletPool>().ReturnBullet(gameObject);
            });
        }
    }
}
