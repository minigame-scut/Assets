using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //最终钥匙碎片被玩家触碰
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //玩家的 tag 为 Player
        if (otherCollider.tag.Equals("Player"))
        {
            //获取玩家的控制脚本
            Player player = otherCollider.gameObject.GetComponent<Player>();

            //更新最终钥匙碎片的收集状态
            //其 tag 为 finalKey_0, finalKey_1, ...
            //使用正则表达式提取字符串中的数字
            string szIndex = System.Text.RegularExpressions.Regex.Replace(gameObject.tag, @"[^0-9]+", "");
            int index = int.Parse(szIndex);
            player.finalKey[index] = true;

            //销毁最终钥匙碎片自身
            Destroy(gameObject);
        }
    }
}
