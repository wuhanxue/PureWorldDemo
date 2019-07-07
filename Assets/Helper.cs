using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    private Animator animator;  // 动画
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Damage()
    {
        animator.SetTrigger("Damage");
    }
}
