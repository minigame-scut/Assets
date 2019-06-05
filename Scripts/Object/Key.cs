using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //关卡钥匙被玩家触碰
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //玩家的 tag 为 Player
        if(otherCollider.tag.Equals("Player"))
        {
            //获取玩家的控制脚本
            Player player = otherCollider.gameObject.GetComponent<Player>();

            //更新当前关卡的钥匙的状态
            player.hasKey = true;

            //广播销毁关卡钥匙道具的信号并传递当前gameObject对象
            GameObject gameObject = this.gameObject;
            EventCenter.BroadCast(EventType.DESTROY, gameObject); 
        }
    }

}
