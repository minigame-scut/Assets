using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    //PlayerData实例化对象
    public PlayerData playerData;

    //阴影对象
    public GameObject shadowr;
    public GameObject shadowl;

    //人物组件
    public Animator anim;
    public Rigidbody2D rig;
    //受到的弹力方向
    public Transform elasticTrans;


    protected static GamePlayer playerInstance;
    public static GamePlayer Instance
    {
        get
        {
            if (playerInstance != null)
                return playerInstance;
            playerInstance = FindObjectOfType<GamePlayer>();
            return playerInstance;
        }
    }
    void Awake()
    {
        playerData = new PlayerData();
    }

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rig = gameObject.GetComponent<Rigidbody2D>();
        playerData.CountJ = 0;
        playerData.useInput = true;
        if (playerData.isBirth)
        {
            anim.SetBool("isBirth", true);
            playerData.isBirth = false;
            StartCoroutine(setBirthAnimAsFalse());
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerData.inputTimer <= 1.0f)
        {
            playerData.inputTimer += Time.fixedDeltaTime;
            return;
        }
        if (playerData.isDead)
        {
            Dead();
            return;
        }
        //判断玩家当前是否持有重力buff
        if (playerData.buff.contains(Buff.GRAVITY))
        {
            if (playerData.flagGravity == 0)
            {
                gravityContrary();
                playerData.flagGravity = 1;
            }
        }
        //判断玩家当前是否持有弹力buff
        if(playerData.buff.contains(Buff.ELASTIC))
        {
            //计算弹力buff持有的时间，当时间>=0.2s时广播信号移除该buff
            playerData.elasticTimer += Time.fixedDeltaTime;
            
            if (playerData.elasticTimer < 0.2f)
            {
                elasticUp();
            }
            else
            {
                EventCenter.Broadcast(EventType.ELASTICDELETE);
            }
        }
        if (Input.GetKeyDown(KeyCode.K) || playerData.DashTime != 0.0f)
        {
            if (playerData.buff.contains(Buff.SUPER))
            {
                superRush();
            }
                
            else
                Dash();
        }
        if (playerData.useInput)
        {
            Walk();
            if (playerData.buff.contains(Buff.SUPER))
            {
                superJump();
            }
            else
            {
                Jump();
            }
            
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

    void Walk()
    {
        if (Input.GetKey(KeyCode.D))          //  播放向右走动画
        {
            playerData.Direction = 1;
            rig.velocity = new Vector2(playerData.Direction * playerData.MoveSpeed, rig.velocity.y);
            anim.SetBool("iswalk", true);
            anim.SetFloat("direction", playerData.Direction);
        }
        else if (Input.GetKey(KeyCode.A))         // 播放向左走动画
        {
            playerData.Direction = -1;
            rig.velocity = new Vector2(playerData.Direction * playerData.MoveSpeed, rig.velocity.y);
            anim.SetBool("iswalk", true);
            anim.SetFloat("direction", playerData.Direction);
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
            playerData.CountJ++;
            playerData.JumpTime = 0.0f;
        }
        getJump(playerData.CountJ);
        if (Input.GetKey(KeyCode.J) && playerData.canJump && playerData.JumpTime < 0.25f && playerData.CountJ != 0)
        {
            rig.isKinematic = true;
            rig.velocity = new Vector2(rig.velocity.x, playerData.JumpSpeed * playerData.flagGravityTrans);
            playerData.JumpTime += Time.deltaTime;
            anim.SetBool("isjump", true);
        }
        else if (Input.GetKeyUp(KeyCode.J) || playerData.JumpTime >= 0.25f || !playerData.canJump)
        {
            rig.isKinematic = false;
        }
    }
    void Dash()
    {
        if (playerData.DashTime < 0.15f)
        {
            rig.isKinematic = true;
            anim.SetBool("isdash", true);
            playerData.useInput = false;
            rig.velocity = new Vector2(playerData.Direction * playerData.MoveSpeed * 3f, 0);
            playerData.DashTime += Time.deltaTime;
            if (playerData.Direction == 1)
                Instantiate<GameObject>(shadowr, transform.position, transform.rotation);
            else if (playerData.Direction == -1)
                Instantiate<GameObject>(shadowl, transform.position, transform.rotation);
        }
        else if (playerData.DashTime >= 0.15f)
        {
            anim.SetBool("isdash", false);
            rig.isKinematic = false;
            playerData.useInput = true;
            rig.velocity = new Vector2(0, 0);
            playerData.DashTime = 0.0f;
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            anim.SetBool("isjump", false);
            playerData.canJump = true;
            playerData.CountJ = 0;
        }
    }
    void getJump(int j)
    {
        if (j >= 2)
        {
            playerData.canJump = false;
        }
            
    }
    void Dead()
    {
        rig.velocity = new Vector2(0, 0);
        anim.SetBool("isDeath", true);
    }
    //超级跳
    void superJump()
    {
        if (Input.GetKeyDown(KeyCode.J))
        { 
            rig.isKinematic = true;
            playerData.CountJ++;
            playerData.JumpTime = 0.0f;
        }
        getJump(playerData.CountJ);
        if (Input.GetKey(KeyCode.J) && playerData.JumpTime < 0.1f && playerData.CountJ != 0)
        {
            rig.isKinematic = true;
            rig.velocity = new Vector2(rig.velocity.x, playerData.JumpSpeed * playerData.flagGravityTrans * 10f);
            playerData.JumpTime += Time.deltaTime;
            anim.SetBool("isjump", true);
            EventCenter.Broadcast(EventType.JUMP);
        }
        else if (Input.GetKeyUp(KeyCode.J) || playerData.JumpTime >= 0.1f || !playerData.canJump)
        {
            rig.isKinematic = false;
        }
    }
    //超级冲刺
    void superRush()
    {
        if (playerData.DashTime < 0.15f)
        {
            rig.isKinematic = true;
            anim.SetBool("isdash", true);
            playerData.useInput = false;
            rig.velocity = new Vector2(playerData.Direction * playerData.MoveSpeed * 5f, 0);
            playerData.DashTime += Time.deltaTime;
            if (playerData.Direction == 1)
                Instantiate<GameObject>(shadowr, transform.position, transform.rotation);
            else if (playerData.Direction == -1)
                Instantiate<GameObject>(shadowl, transform.position, transform.rotation);

            EventCenter.Broadcast(EventType.JUMP);
        }
        else if (playerData.DashTime >= 0.15f)
        {
            anim.SetBool("isdash", false);
            rig.isKinematic = false;
            playerData.useInput = true;
            rig.velocity = new Vector2(0, 0);
            playerData.DashTime = 0.0f;
        }
    }
    //反重力
    void gravityContrary()
    {
        Vector3 axisX = new Vector3(1, 0, 0);
        gameObject.transform.Rotate(axisX, 180.0f);
        rig.gravityScale *= -1;
        playerData.flagGravityTrans *= -1;
    }
    //弹起
    void elasticUp()
    {
        rig.AddForce(elasticTrans.right * playerData.testForce);
    }

    IEnumerator setBirthAnimAsFalse()
    {
        yield return new WaitForSeconds(0.958f);
        anim.SetBool("isBirth", false);
    }
}
