using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private int CountJ;
    //人物组件
    private GamePlayer para;
    private Animator anim;
    private Rigidbody2D rig;

    void Start()
    {
        para = GamePlayer.GetInstance();
        anim = gameObject.GetComponent<Animator>();
        rig = gameObject.GetComponent<Rigidbody2D>();
        CountJ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) || para.DashTime != 0.0f)
        {
            Dash();
        }
        if (!para.isdash)
        {
            Walk();
            Jump();
        }
    }
    void Walk()
    {
        if (Input.GetKey(KeyCode.D))          //  播放向右走动画
        {
            para.Direction = 1;
            rig.velocity = new Vector2(para.Direction * para.MoveSpeed, rig.velocity.y);
            anim.SetBool("iswalk", true);
            anim.SetFloat("direction", para.Direction);
        }
        else if (Input.GetKey(KeyCode.A))         // 播放向左走动画
        {
            para.Direction = -1;
            rig.velocity = new Vector2(para.Direction * para.MoveSpeed, rig.velocity.y);
            anim.SetBool("iswalk", true);
            anim.SetFloat("direction", para.Direction);
        }
        else                                 //静止 Idle 动画
        {
            rig.velocity = new Vector2(0, rig.velocity.y);
            anim.SetBool("iswalk", false);
        }
    }
    void Jump()
    {
        Vector3 up = new Vector3(0, para.size,0)+transform.position;
        if (Input.GetKeyDown(KeyCode.J))
        {
            rig.isKinematic = true;
            CountJ++;
            para.JumpTime = 0.0f;
        }
        getJump(CountJ);
        if (Input.GetKey(KeyCode.J) && para.canJump && para.JumpTime < 0.25f&&CountJ!=0)
        {
            rig.isKinematic = true;
            rig.velocity = new Vector2(rig.velocity.x, para.JumpSpeed);
            para.JumpTime += Time.deltaTime;
            anim.SetBool("isjump", true);
        }
        else if (Input.GetKeyUp(KeyCode.J) || para.JumpTime >= 0.25f||!para.canJump)
        {
            rig.isKinematic = false;
        }
    }
    void Dash()
    {
        if (para.DashTime < 0.15f)
        {
            rig.isKinematic = true;
            anim.SetBool("isdash", true);
            para.isdash = true;
            rig.velocity = new Vector2(para.Direction * para.MoveSpeed * 2.5f, 0);
            para.DashTime += Time.deltaTime;
        }
        else if(para.DashTime>=0.15f)
        {
            anim.SetBool("isdash", false);
            rig.isKinematic = false;
            para.isdash = false;
            rig.velocity = new Vector2(0, 0);
            para.DashTime = 0.0f;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ground")
        {

            anim.SetBool("isjump", false);
            para.canJump = true;
            CountJ = 0;
        }
    }
    void getJump(int j)
    {
        if (para.DoubleJump)
        {
            if (j >= 3)
                para.canJump = false;
        }
        else
        {
            if (j >= 2)
                para.canJump = false;
        }             
    }
}
