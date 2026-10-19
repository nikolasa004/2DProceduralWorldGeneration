using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    [HideInInspector]
    public bool onGround;
    [HideInInspector]
    public Vector2 spawnPos;
    [HideInInspector]
    public Vector2Int mousePos;
    [HideInInspector]
    public bool hit;

    public MapGeneration mapGeneration;

    private Rigidbody2D rb;
    private Animator animator;
    private float horizontal;
    public void Spawn()
    {
        GetComponent<Transform>().position = spawnPos;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        float jump = Input.GetAxisRaw("Jump");
        Vector2 movement = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
        hit = Input.GetMouseButton(0);
        if(hit)
        {
            mapGeneration.RemoveTile(mousePos.x, mousePos.y);
        }
        if(horizontal>0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(horizontal<0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if(jump>0.1f)
        {
            if(onGround)
            movement.y = jumpForce;
        }
        rb.linearVelocity = movement;
    }
    private void Update()
    {
        mousePos.x =Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x-0.5f);
        mousePos.y = Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y-0.5f);
        animator.SetFloat("horizontal",horizontal);
        animator.SetBool("hit", hit);
    }
}
