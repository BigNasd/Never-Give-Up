using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab; // 子弹预制体
    public int poolSize = 20; // 对象池的大小

    private List<GameObject> bulletPool = new List<GameObject>();

    void Start()
    {
        // 初始化对象池
        if(GameInfoData.Instance.playerNum == 0)
            for (int i = 0; i < poolSize; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.SetActive(false);
                bulletPool.Add(bullet);
            }
    }

    // 从对象池中获取子弹
    public GameObject GetBullet()
    {
        foreach (GameObject bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }

        // 如果对象池中没有可用的子弹，则创建一个新的
        GameObject newBullet = Instantiate(bulletPrefab);
        bulletPool.Add(newBullet);
        return newBullet;
    }

    // 将子弹归还到对象池中
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }
}
