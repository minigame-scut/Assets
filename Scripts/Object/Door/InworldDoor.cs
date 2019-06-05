using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//检测里世界门的触发
public class InworldDoor : MonoBehaviour
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //检测到玩家触碰
        if (collision.transform.tag == "player")
        {
            if (deltaTime > 1)  //触发时间间隔大于一秒
            {

                Debug.Log("InWorldDoorDoor");//测试
                EventCenter.Broadcast(EventType.INWORLDDOOR);   //广播里世界门触碰信号
                deltaTime = 0;  //重置间隔定时器
            }
        }
    }
}
