using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //滑稽脸被玩家触碰
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //玩家的 tag 为 Player
        if (otherCollider.tag.Equals("Player"))
        {
            //获取玩家的控制脚本
            Player player = otherCollider.gameObject.GetComponent<Player>();

            //更新收集滑稽的个数
            player.faceCount += 1;

            //销毁关卡滑稽脸自身
            Destroy(gameObject);
        }
    }
}
