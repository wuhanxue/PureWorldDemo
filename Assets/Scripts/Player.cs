using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.5f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float horizontal;
    private float vertical;
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (vertical > 0) animator.SetInteger("Walk", -1);
        else if (vertical < 0) animator.SetInteger("Walk", 1);
        else if (horizontal > 0)
        {
            animator.SetInteger("Walk", -2);
            spriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            animator.SetInteger("Walk", 2);
            spriteRenderer.flipX = true;
        }
        else animator.SetInteger("Walk", 0);

    }
    private void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector2.up * vertical * moveSpeed * Time.deltaTime);
        transform.Translate(Vector2.right * horizontal * moveSpeed * Time.deltaTime);
    }
}
