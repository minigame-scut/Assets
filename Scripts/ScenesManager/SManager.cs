using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//sceneManager负责接收门触碰发出的信号，然后使用该信号来操作玩家做出相应的改变。

public class SManager : MonoBehaviour
{
    public  Vector3 birthPosition;//当前场景的出生点
    private static SManager instance = null;

    public static SManager Instance
    {
        get
        {
            if (SManager.instance == null)
            {
                SManager.instance = new SManager();
            }
            return SManager.instance;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        listener();
    }

    // Update is called once per frame
    void Update()
    {
        listener();
    }
    //获取当前场景出生点位置的函数
    public void setBirthPosition(Vector3 newPosition)
    {
        this.birthPosition = newPosition;
    }

    //委托的方法
    //玩家通过门重置状态，玩家死亡，玩家重置位置，玩家经过门之后的效果
    private void responseForSignalBROKESPEEDDOOR()
    {
        GamePlayer.getInstance().buffList.Add(Buff.ELASTIC);
    }
    private void responseForSignalDEATHDOOR()
    {
        
    }
    private void responseForSignalGDOOR()
    {
        GamePlayer.getInstance().buffList.Add(Buff.GRAVITY);
    }
    private void responseForMAGICALDOOR()
    {
        //Magic
    }
    private void responseForTRANSDOOR(Vector3 newPosition)
    {
        //GamePlayer.getInstance().transform.position = newPosition;
    }
    private void responseForUPSPEEDDOOR()
    {
        GamePlayer.getInstance().buffList.Add(Buff.SUPER);
    }
    private void responseForDEATH()
    {
        //为人物设置死亡状态
        GamePlayer.getInstance().ifDead = true;
        //销毁人物，3s延迟后销毁
        Destroy(GameObject.FindWithTag("Player"), 3.0f);
        //创建对应prefeb
        Quaternion newQuaternion = new Quaternion(0, 0, 0, 0);//实例化预制体的rotation
        GameObject.Instantiate(prefeb, birthPosition, newQuaternion);
        //GameObject.Instantiate(/*prefeb*/);
    }
    private void responseForJUMP()
    {
        //播放JUMP音效
        AudioManager.getInstance().PlaySound("Jump");
        //遍历Player中的buffList列表，查看当前玩家的buff状态
        //如果在接受到JUMP信号时玩家buff状态为SUPER，则移除该buff状态
        foreach (Buff curBuff in GamePlayer.getInstance().buffList)
        {
            if (curBuff == Buff.SUPER)
                GamePlayer.getInstance().buffList.Remove(curBuff);
        }
    }
    private void responseForRUSH()
    {
        //播放RUSH音效
        AudioManager.getInstance().PlaySound("Rush");
        //遍历Player中的buffList列表，如果在接收到RUSH信号时玩家buff状态为SUPER，则移除该buff状态
        foreach (Buff curBuff in GamePlayer.getInstance().buffList)
        {
            if (curBuff == Buff.SUPER)
                GamePlayer.getInstance().buffList.Remove(curBuff);
        }
    }
    private void responseForDESTROY(GameObject gameObject)
    {
        //销毁当前场景玩家的prefeb
        Destroy(gameObject);
    }
    //添加监听器
    private void listener()
    {
        EventCenter.AddListener(EventType.BROKESPEEDDOOR, responseForSignalBROKESPEEDDOOR);
        EventCenter.AddListener(EventType.DEATHDOOR, responseForSignalDEATHDOOR);
        EventCenter.AddListener(EventType.GDOOR, responseForSignalGDOOR);
        EventCenter.AddListener(EventType.MAGICALDOOR, responseForMAGICALDOOR);
        EventCenter.AddListener<Vector3>(EventType.TRANSDOOR, responseForTRANSDOOR);
        EventCenter.AddListener(EventType.UPSPEEDDOOR, responseForUPSPEEDDOOR);
        EventCenter.AddListener(EventType.DEATH, responseForDEATH);
        EventCenter.AddListener(EventType.JUMP, responseForJUMP);
        EventCenter.AddListener(EventType.RUSH, responseForRUSH);
        EventCenter.AddListener<GameObject>(EventType.DESTROY, responseForDESTROY);
    }

}
