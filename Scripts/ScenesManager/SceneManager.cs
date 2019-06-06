using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//sceneManager负责接收门触碰发出的信号，然后使用该信号来操作玩家做出相应的改变。

public class SManager : MonoBehaviour
{
    protected SManager(){ }
    private static SManager instance = null;

    public static SManager Instance
    {
        get
        {
            if (SManager.instance == null)
            {
                DontDestroyOnLoad(SManager.instance);
                SManager.instance = new SManager();
            }
                
            return SManager.instance;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //添加监听
        EventCenter.AddListener(EventType.BROKESPEEDDOOR, responseForSignalBROKESPEEDDOOR);
        EventCenter.AddListener(EventType.DEATHDOOR, responseForSignalDEATHDOOR);
        EventCenter.AddListener(EventType.GDOOR, responseForSignalGDOOR);
        EventCenter.AddListener(EventType.MAGICALDOOR, responseForMAGICALDOOR);
        EventCenter.AddListener<Vector3>(EventType.TRANSDOOR, responseForTRANSDOOR);
        EventCenter.AddListener(EventType.UPSPEEDDOOR, responseForUPSPEEDDOOR);
        EventCenter.AddListener(EventType.DEATH, responseForDEATH);
        EventCenter.AddListener(EventType.BIRTH, responseForBIRTH);
        EventCenter.AddListener(EventType.NEXTPLACE, responseForNEXTPLACE);
        EventCenter.AddListener(EventType.JUMP, responseForJUMP);
        EventCenter.AddListener(EventType.RUSH, responseForRUSH);
        EventCenter.AddListener<GameObject>(EventType.DESTROY, responseForDESTROY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //委托的方法
    //玩家通过门重置状态，玩家死亡，玩家重置位置，玩家经过门之后的效果
    private void responseForSignalBROKESPEEDDOOR()
    {
        GamePlayer.GetInstance().buffList.Add(Buff.ELASTIC);
    }
    private void responseForSignalDEATHDOOR()
    {
        //Player.Instance.setBuff();
    }
    private void responseForSignalGDOOR()
    {
        //Player.Instance.setBuff();
    }
    private void responseForMAGICALDOOR()
    {
        //Player.Instance.setBuff();
    }
    private void responseForTRANSDOOR(Vector3 newPosition)
    {
        //Player.Instance.transform.position = newPosition;
    }
    private void responseForUPSPEEDDOOR()
    {
        //Player.Instance.setBuff();
    }
    private void responseForDEATH()
    {
        //Player.Instance.setState();
    }
    private void responseForBIRTH()
    {
        //Player.Instance.setState();
    }
    private void responseForNEXTPLACE()
    {
        //Player.Instance.setState();
    }
    private void responseForJUMP()
    {
        //Player.Instance.setState();
    }
    private void responseForRUSH()
    {
        //Player.Instance.setState();
    }
    private void responseForDESTROY(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
