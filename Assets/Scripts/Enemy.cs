using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator anim;

    private Rigidbody2D rb;

    private GameObject player;
    [Header("基本参数")]
    public Vector2 faceDir;

    public float hurtForce;

    public float deathForce;

    [Header("基本属性")]

    public float maxHealth;

    public float currentHealth;

    public float moveSpeed;

    [Header("状态")]

    public bool isHurt;

    public bool isDeath;

    public bool isChangeDir;

    [Header("物理检测")]

    public LayerMask WallLayer;

    public float checkRaduis;

    public Vector2 OffsetRight;

    public Vector2 OffsetLeft;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");

        currentHealth = maxHealth;

    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        currentHealth = maxHealth;

        isHurt = false;

        isDeath = false;
    }

    private void Update()
    {
        faceDir = new Vector2(transform.localScale.x, 0);
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        
        if (!collision.CompareTag("Player") && !collision.CompareTag("Wall"))
        {
            anim.SetTrigger("hurt");
            MusicManager.GetInstance().PlaySound("Sword hit shield 6", false, 0.1f);
            rb.AddForce(hurtForce * (transform.position - player.transform.position).normalized, ForceMode2D.Impulse);
            if(!isHurt)
                StartCoroutine(Hurt(1f));
        }
        if (collision.CompareTag("Player"))
        {
            if (!GameInfoData.Instance.gameStart)
                return;
            PoolManager.GetInstance().GetObj("Effect/Hit", player.transform.position, Quaternion.identity);
            MusicManager.GetInstance().PlaySound("超级玛丽失败音效", false, 0.5f);
            StartCoroutine(TimeScale());

            player.GetComponent<Animator>().SetBool("isDeath", true);
            player.transform.GetChild(0).gameObject.SetActive(false);
            player.GetComponent<Rigidbody2D>().AddForce(30 * new Vector2(player.transform.position.x - transform.position.x, 6).normalized, ForceMode2D.Impulse);
        }

    }
    private IEnumerator Hurt(float dmg)
    {
        //rb.velocity = Vector2.zero;
        isHurt = true;
        CHangeDir();
        currentHealth -= dmg;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            //GetComponent<CapsuleCollider2D>().enabled = false;
            PoolManager.GetInstance().GetObj("Effect/EnemyDeathEffect", transform.position, Quaternion.identity, (o) =>
            {
                PoolManager.GetInstance().GetObj("Effect/EnemyDeath", transform.position, Quaternion.identity, (o) =>
                {
                    if(Random.Range(0f,100f) < 20f)
                        MusicManager.GetInstance().PlaySound("Zombie Death_09", false, 0.5f);
                    o.transform.localScale = transform.localScale * 1.05f;
                    o.GetComponent<Rigidbody2D>().AddForce(Random.Range(deathForce-3,deathForce+1) * new Vector2((transform.position - player.transform.position).normalized.x, 3f), ForceMode2D.Impulse);

                    GameInfoData.Instance.corpse.Add(o);
                    PoolManager.GetInstance().PushObj(gameObject.name, gameObject);
                });
            });
            GameInfoData.Instance.enemyNum--;
            isDeath = true;

        }
        yield return new WaitForSeconds(0.3f);
        isHurt = false;
    }

    private void Move()
    {
        if (!isHurt)
        {
            rb.velocity = new Vector2(faceDir.x * moveSpeed, rb.velocity.y);
        }
        if(Physics2D.OverlapCircle((Vector2)transform.position + OffsetRight, checkRaduis, WallLayer) && faceDir.x > 0 ||
           Physics2D.OverlapCircle((Vector2)transform.position + OffsetLeft, checkRaduis, WallLayer) && faceDir.x < 0)
        {
            transform.localScale = new Vector3(-faceDir.x, 1, 1);
        }
    }

    private void CHangeDir()
    {
        if (transform.position.x - player.transform.position.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        if (transform.position.x - player.transform.position.x < 0)
            transform.localScale = new Vector3(1, 1, 1);

    }

    public void Check()
    {
        isChangeDir = Physics2D.OverlapCircle((Vector2)transform.position + OffsetRight, checkRaduis, WallLayer) ||
                      Physics2D.OverlapCircle((Vector2)transform.position + OffsetLeft, checkRaduis, WallLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + OffsetRight, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + OffsetLeft, checkRaduis);
    }

    private IEnumerator TimeScale()
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(0.4f);
        Time.timeScale = 1f;
        UIManager.Instance.ShowPanel<FailPanel>();
    }
}
