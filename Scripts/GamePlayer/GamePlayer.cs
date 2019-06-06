using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer
{
    public float MoveSpeed;
    public int Direction;
    //跳跃参数
    public float JumpSpeed;
    public bool canJump;
    public float JumpTime;
    public bool DoubleJump;
    //冲刺参数
    public bool isdash;
    public int TimesofDash;
    public float DashTime;
    //规格参数
    public float size;//128


    private static GamePlayer myGamePlayer = null;
    public static GamePlayer GetInstance()
    {
        if (myGamePlayer == null)
        {
            myGamePlayer = new GamePlayer();
        }
        return myGamePlayer;
    }
    private GamePlayer()
    {
        MoveSpeed = 5.0f;
        Direction = 1;

        JumpSpeed = 5.0f;
        canJump = true;
        JumpTime = 0.0f;
        DoubleJump = false;

        isdash = false;
        TimesofDash = 1;
        DashTime = 0.0f;

        size = 5.0f;
    }
}
