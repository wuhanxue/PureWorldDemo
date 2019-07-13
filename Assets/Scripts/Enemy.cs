using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;  // 动画
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    // 获取动画组件
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
