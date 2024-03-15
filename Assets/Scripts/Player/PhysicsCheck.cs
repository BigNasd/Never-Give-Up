using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{

    public bool isGround;

    public LayerMask groundLayer;

    public float checkRaduis;

    public Vector2 Offset;

    public PhysicsMaterial2D wallMaterial;
    public PhysicsMaterial2D groundMaterial;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Check();
    }

    public void Check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + Offset, checkRaduis, groundLayer);
        if(isGround)
            rb.sharedMaterial = groundMaterial;
        else
            rb.sharedMaterial = wallMaterial;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + Offset, checkRaduis);
    }
}
