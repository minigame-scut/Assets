using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour

{
    //移动参数
    public bool canWalk;
    public float MoveSpeed;
    public int Direction;
    //跳跃参数
    public float JumpSpeed;
    public bool canJump;
    public float JumpTime;
    public bool DoubleJump;
    //冲刺参数  
    public int TimesofDash;
    public float DashTime;
    //规格参数
    public float size;//128

    //死亡信号
    public bool isDead;


    //持有buff列表
    public List<Buff> buffList;
    //是否死亡
    public bool ifDead;

    private static GamePlayer myGamePlayer = null;
    public static GamePlayer getInstance()
    {
        if (myGamePlayer == null)
        {
            myGamePlayer = new GamePlayer();
        }
        return myGamePlayer;
    }
    private GamePlayer()
    {
        canWalk = true;
        MoveSpeed = 5.0f;
        Direction = 1;

        JumpSpeed = 5.0f;
        canJump = true;
        JumpTime = 0.0f;
        DoubleJump = false;

        TimesofDash = 1;
        DashTime = 0.0f;

        size = 5.0f;

        isDead = false;
    }

    void Start()
    {
        Debug.Log("123");
    }
}
