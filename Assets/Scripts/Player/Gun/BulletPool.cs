using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab; // �ӵ�Ԥ����
    public int poolSize = 20; // ����صĴ�С

    private List<GameObject> bulletPool = new List<GameObject>();

    void Start()
    {
        // ��ʼ�������
        if(GameInfoData.Instance.playerNum == 0)
            for (int i = 0; i < poolSize; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.SetActive(false);
                bulletPool.Add(bullet);
            }
    }

    // �Ӷ�����л�ȡ�ӵ�
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

        // ����������û�п��õ��ӵ����򴴽�һ���µ�
        GameObject newBullet = Instantiate(bulletPrefab);
        bulletPool.Add(newBullet);
        return newBullet;
    }

    // ���ӵ��黹���������
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }
}
