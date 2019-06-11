using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPlatformController : PhysicalObject
{
    //动画组件
    private Animator anim;
    //当前跳跃速度
    public float curJumpSpeed;
    //当前冲刺速度
    public float curRushSpeed;
    //阴影对象
    public GameObject shadowr;
    public GameObject shadowl;
    //输入计时器，用于防止出生动画未播完玩家就开始进行操作
    public float inputTimer = 0;
    //是否暂停
    public bool isPause = false;

    //受到弹力方向
    public Transform elasticTrans;

    Vector2 move;
    
    // Start is called before the first frame update
    void Start()
    {
        move = Vector2.zero;
        anim = GetComponent<Animator>();
        curJumpSpeed = playerData.normalJumpSpeed;
        curRushSpeed = playerData.normalRushSpeed;
        //设置玩家出生动画
        if (playerData.isBirth)
        {
            anim.SetBool("isBirth", true);
            playerData.isBirth = false;
            StartCoroutine(setBirthAnimAsFalse());
        }
    }

    protected override void ComputeVelocity()
    {
        if (isPause)
            return;
        //输入计时器计时
        if (inputTimer <= 1.2f)
        {
            inputTimer += Time.fixedDeltaTime;
            return;
        }
        //玩家是否死亡
        if (playerData.isDead)
        {
            Dead();
            return;
        }
        //是否重力翻转
        spriteRenderer.flipY = (playerData.gravityTrans == 1) ? false : true;
        if(playerData.buff.contains(Buff.GRAVITY))
        {
            if(playerData.flagGravity == 0)
            {
                gravityContrary();
                playerData.flagGravity = 1;
            }
        }
        if (isRush)
        {
            //生成阴影
            createShdow();
            //是冲刺状态
            if (playerData.rushTimer <= playerData.rushMaxTime)
            {   

                playerData.rushTimer += Time.fixedDeltaTime;
                targetVelocity = move * curRushSpeed;
            }
            else  //结束了冲刺状态
            {
                playerData.rushTimer = 0;
                targetVelocity = move * playerData.maxSpeed;
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
            playerData.dir = -1;
            move.x = playerData.dir ;
            isWalk = true;

        }else if (Input.GetKey(KeyCode.D))
        {
            playerData.dir = 1;
            move.x = playerData.dir;
            isWalk = true;
        }
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            isWalk = false;
        }
        //冲刺状态
        if (Input.GetKeyDown(KeyCode.K)&& playerData.canRush)
        {
            isJump = false;
            isWalk = false;
            move.x = playerData.dir;
            isRush = true;
            if (playerData.buff.contains(Buff.SUPER))
            {
                superRush();
            }
            else
            {
                targetVelocity = move * curRushSpeed;
            }
            //空中只能冲刺一次
            playerData.canRush = false;
            //生成阴影
            createShdow();
            //广播移除重置buff的信号
            EventCenter.Broadcast(EventType.INITDELETE);
            //计算冲刺的起始点 计算差值
            return;
        }
        //跳跃
        if (Input.GetButtonDown("Jump") && playerData.canJump)
        {
            curJumpSpeed = playerData.normalJumpSpeed;
            if(playerData.buff.contains(Buff.SUPER))
            {
                isWalk = false;
                isJump = true;
                superJump();
            }
            else
            {
                isWalk = false;
                isJump = true;
                velocity.y = curJumpSpeed;
            }
            EventCenter.Broadcast(EventType.INITDELETE);   
        }
        else if (Input.GetButtonUp("Jump"))
        {
            //大跳小跳
            isWalk = false;
            isJump = true;
            if (velocity.y > 0)
                velocity.y = velocity.y * 0.5f;
        }
        //判断玩家当前是否持有弹力buff
        if (playerData.buff.contains(Buff.ELASTIC))
        {
            //计算弹力buff持有的时间，当时间>=0.2s时广播信号移除该buff
            playerData.elasticTimer += Time.fixedDeltaTime;

            if (playerData.elasticTimer < 0.3f)
            {
                elasticUp();
            }
            else
            {
                EventCenter.Broadcast(EventType.ELASTICDELETE);
            }
        }
        targetVelocity = move * playerData.maxSpeed;
    }
    protected override void playAnimation()
    {
        if(playerData.dir == 1)
        {
            anim.SetFloat("direction", 1);
        }
        else if(playerData.dir == -1)
        {
            anim.SetFloat("direction", -1);
        }
        if(isWalk && playerData.dir == 1)
        {
            anim.SetBool("isWalk", true);
            anim.SetFloat("direction", playerData.dir);
        }
        else if(isWalk && playerData.dir == -1)
        {
            anim.SetBool("isWalk", true);
            anim.SetFloat("direction", playerData.dir);
        }
        else
        {
            anim.SetBool("isWalk", false);
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
    //得到玩家的数据
    public PlayerData getPlayerData()
    {
        return this.playerData;
    }
    //设置玩家的数据
    public void setPlayerData(PlayerData playerData)
    {
        this.playerData = playerData;
    }
    //设置玩家的位置数据
    public void setPlayerDataPosition(Vector3 pos)
    {
        this.playerData.setPlayerVector3DPositionData(pos.x, pos.y, pos.z);
    }

    //设置玩家所处地图的数据
    public void setPlayerDataMapIndex(int index)
    {
        this.playerData.mapIndex = index;
    }

    //超级跳
    void superJump()
    {
        curJumpSpeed = playerData.superJumpSpeed;
        velocity.y = curJumpSpeed;
        EventCenter.Broadcast(EventType.JUMP);
    }
    //超级冲刺
    void superRush()
    {
        this.curRushSpeed = playerData.superRushSpeed;
        targetVelocity = move * curRushSpeed;
        EventCenter.Broadcast(EventType.RUSH);
    }
    //生成阴影
    void createShdow()
    {
        if (playerData.dir == 1)
            Instantiate<GameObject>(shadowr, transform.position, transform.rotation);
        else if (playerData.dir == -1)
            Instantiate<GameObject>(shadowl, transform.position, transform.rotation);
    }
    //反重力
    void gravityContrary()
    {
        playerData.gravityTrans *= -1;
        velocity.y = 0;
    }
    //弹起
    void elasticUp()
    {
        velocity.y = elasticTrans.right.y * 4;
        velocity.x = elasticTrans.right.x * 4;
    }
    //玩家死亡
    void Dead()
    {
        velocity = Vector2.zero;
        anim.SetBool("isDeath", true);
    }

    IEnumerator setBirthAnimAsFalse()
    {
        yield return new WaitForSeconds(0.958f);
        anim.SetBool("isBirth", false);
    }
}
