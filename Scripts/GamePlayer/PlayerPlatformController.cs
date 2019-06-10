using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPlatformController : PhysicalObject
{
    public float maxSpeed = 7;

    //冲刺速度
    public float rushSpeed = 20;

    public float jumpTakeOffSpeed = 7;

    //冲刺计时器
    public float rushTimer = 0;

    //最大冲刺时间
    public float rushMaxTime = 0.15f;

    //人物的方向 -1左 1右
    public int dir = 1;

    private Animator anim;

    Vector2 move;
    // Start is called before the first frame update
    void Start()
    {
        move = Vector2.zero;
        anim = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        //  Vector2 move = Vector2.zero;
        //spriteRenderer.flipX = (dir == 1) ? false : true;
        if (isRush)
        {
            //是冲刺状态
            if(rushTimer <= rushMaxTime)
            {
                rushTimer += Time.fixedDeltaTime;
                targetVelocity = move * rushSpeed;
            }
            else  //结束了冲刺状态
            {
                rushTimer = 0;
                targetVelocity = move * maxSpeed;
                isRush = false;
            }
            return;
        }
        else
        {
            move = Vector2.zero;
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir = -1;
            move.x = dir ;
            isWalk = true;

        }else if (Input.GetKey(KeyCode.D))
        {
            dir = 1;
            move.x = dir;
            isWalk = true;
        }
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            isWalk = false;
        }
        //冲刺状态
        if (Input.GetKeyDown(KeyCode.K)&& canRush)
        {
            isJump = false;
            isWalk = false;
            move.x = dir;
            isRush = true;
            targetVelocity = move * rushSpeed;
            //空中只能冲刺一次
            canRush = false;
            //计算冲刺的起始点 计算差值
            return;
        }

        if(Input.GetButtonDown("Jump") && canJump)
        {
            isWalk = false;
            isJump = true;
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            //大跳小跳
            isWalk = false;
            isJump = true;
            if (velocity.y > 0)
                velocity.y = velocity.y * 0.5f;
        }
        targetVelocity = move * maxSpeed;
    }
    protected override void playAnimation()
    {
        if(isWalk && dir == 1)
        {
            anim.SetBool("iswalk", true);
            anim.SetFloat("direction", dir);
        }
        else if(isWalk && dir == -1)
        {
            anim.SetBool("iswalk", true);
            anim.SetFloat("direction", dir);
        }
        else
        {
            anim.SetBool("iswalk", false);
        }
        if(isJump)
        {
            anim.SetBool("isJump", true);
        }
        else if(!isJump)
        {
            anim.SetBool("isJump", false);
        }
        if(isRush)
        {
            anim.SetBool("isDash", true);
        }
        else if(!isRush)
        {
            anim.SetBool("isDash", false);
        }
    }
}
