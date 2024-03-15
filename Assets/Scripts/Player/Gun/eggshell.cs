using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eggshell : MonoBehaviour
{
    public float rotateSpeed;

    public float upForce;

    public Vector2 upDir;

    private Rigidbody2D rb;

    public bool isFly;

    public GameObject player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isFly = true;
        rb.AddForce(new Vector2(upDir.x * player.transform.localScale.x,upDir.y) * upForce, ForceMode2D.Impulse);

    }

    private void Update()
    {
        if (isFly)
            Rotetion();
    }

    private void Rotetion()
    {
        transform.localEulerAngles += new Vector3(0, 0, rotateSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isFly = false;
    }
}
