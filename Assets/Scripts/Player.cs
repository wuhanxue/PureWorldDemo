using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.5f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Debug.Log("horizontal" + horizontal);
        //Debug.Log("vertical" + vertical);
        Debug.Log("Direction: " +  animator.GetInteger("Direction") );
        if (horizontal > 0)
        {
            animator.SetInteger("Direction", 2);
        }
        else if (horizontal < 0)
        {
            animator.SetInteger("Direction", 3);
        }
        if (vertical > 0)
        {
            animator.SetInteger("Direction", 1);
        }
        else if (vertical < 0)
        {
            animator.SetInteger("Direction", 0);
        }
        transform.Translate(Vector2.up * vertical * moveSpeed * Time.deltaTime);
        transform.Translate(Vector2.right * horizontal * moveSpeed * Time.deltaTime);
    }
}
