using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{

    private Animator anim;

    public Rigidbody2D rb;

    private PhysicsCheck physicsCheck;


    [Header("移动相关")]
    public Vector2 faceDir;

    public float moveSpeed;

    private float moveDir;

    public bool isFire;

    [Header("跳跃相关")]
    public float jumpForce;

    public float longJumpMultiplier = 2.5f;

    public float ShortJumpMultiplier = 2f;



    [Header("相关力")]
    public float Recoilforce;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
    }

    private void Start()
    {
        //UIManager.Instance.ShowPanel<BeginPanel>();
    }

    private void Update()
    {
        if (GameInfoData.Instance.gameStart)
        {
            Move();
            Jump();
        }
        anim.SetInteger("walk", (int)moveDir);
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isGround", physicsCheck.isGround);
    }

    private void LateUpdate()
    {

    }

    private void Move()
    {
        moveDir = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);

        faceDir = new Vector2(transform.localScale.x, 0);
        if (!isFire)
        {
            if (moveDir > 0)
                faceDir.x = 1;
            if (moveDir < 0)
                faceDir.x = -1;
        }

        

        transform.localScale = new Vector3(faceDir.x, 1, 1);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && physicsCheck.isGround)
        {
            rb.velocity = Vector2.up * jumpForce;
            MusicManager.GetInstance().PlaySound("Bounce_Jump 14", false, 0.1f);
        }

        if (rb.velocity.y < 0 && !physicsCheck.isGround)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (longJumpMultiplier - 1) * Time.deltaTime;
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space) && !physicsCheck.isGround)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (ShortJumpMultiplier - 1) * Time.deltaTime;
    }

    private void FootStep()
    {
        MusicManager.GetInstance().PlaySound("Footsteps (Player) 37", false, 0.2f);
    }
}
