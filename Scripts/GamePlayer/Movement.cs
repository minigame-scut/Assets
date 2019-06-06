using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private int CountJ;
    public bool useInput;
    public GameObject shadowr;
    public GameObject shadowl;
    //人物组件
    private GamePlayer para;
    private Animator anim;
    private Rigidbody2D rig;

    void Start()
    {
        para = GamePlayer.getInstance();
        anim = gameObject.GetComponent<Animator>();
        rig = gameObject.GetComponent<Rigidbody2D>();
        CountJ = 0;
        useInput = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (para.isDead)
        {
            Dead();
            return;
        }
        if (Input.GetKeyDown(KeyCode.K) || para.DashTime != 0.0f)
        {
            if (para.buffList.Contains(Buff.SUPER))
                superRush();
            else
                Dash();
        }
        if (useInput)
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
            useInput = false;
            rig.velocity = new Vector2(para.Direction * para.MoveSpeed * 2.5f, 0);
            para.DashTime += Time.deltaTime;
            Debug.Log("1");
            if(para.Direction==1)
                Instantiate<GameObject>(shadowr, transform.position, transform.rotation);
            else if(para.Direction==-1)
                Instantiate<GameObject>(shadowl, transform.position, transform.rotation);     
        }
        else if(para.DashTime>=0.15f)
        {
            anim.SetBool("isdash", false);
            rig.isKinematic = false;
            useInput = true;
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
        if (j >= 2)
            para.canJump = false;           
    }
    void Dead()
    {
        //anim.SetBool("dead", true);
        rig.velocity = new Vector2(0, -10.0f);
    }
    //超级跳
    void superJump()
    {

    }
    //超级冲刺
    void superRush()
    {

    }
    //反重力
    void gravityContrary()
    {

    }
    //弹起
    void elastic()
    {

    }
}
