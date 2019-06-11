﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//检测魔法门的触发

public class MegicalDoor : MonoBehaviour
{
    public float BiggestTriggerTime = 1.0f;   //一个门在最大triggerTime时间内能够触发的次数

    private float deltaTime = 0;       //定时器
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //检测到玩家触碰
        if (collision.transform.tag == "player")
        {
            if (deltaTime > BiggestTriggerTime)  //触发时间间隔大于一秒
            {
                EventCenter.Broadcast(EventType.WAVE, this.transform.position);
                Debug.Log("megicalDoor");//测试
                EventCenter.Broadcast(EventType.MAGICALDOOR);   //广播魔法门触碰信号
                deltaTime = 0;  //重置间隔定时器
            }
        }
    }
}
