using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//sceneManager负责接收门触碰发出的信号，然后使用该信号来操作玩家做出相应的改变。

public class SManager : MonoBehaviour
{
    public  Vector3 birthPosition;//当前场景的出生点
    
    //当前玩家的实例
    public GameObject gamePlayer;
    //玩家prefab
    GameObject player;

    private static SManager instance = null;

    public static SManager Instance
    {
        get
        {
            //if (SManager.instance == null)
            //{
            //    SManager.instance = new SManager();
            //}
            return SManager.instance;
        }

    }
    void Awake()
    {
        player = Resources.Load<GameObject>("GameManagerRes/player 1");
    }

    // Start is called before the first frame update
    void Start()
    {
        birthPlayer();
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

    //返回一个玩家的实例
    public GameObject getGamePlayer()
    {
        return this.gamePlayer;
    }


    //生成玩家
    public void birthPlayer()
    {
        //如果找到玩家物体，则销毁当前玩家物体
        if (GameObject.Find("player(clone)"))
        {
            Destroy(GameObject.Find("player(Clone)"));
            //return;
        }
        //加载player预制体，在出生位置创建玩家
        //GameObject player = Resources.Load<GameObject>("GameManagerRes/player");
        gamePlayer = GameObject.Instantiate(player,birthPosition, Quaternion.identity);



        // //在进入场景的时候应该读取修改保存一个玩家的数据
        try
        {
            PlayerData playerData = (PlayerData)SavePlayerData.GetData("Save/PlayerData.sav", typeof(PlayerData));
            //为玩家的生成位置赋值
            playerData.setPlayerVector3DPositionData(this.birthPosition.x, this.birthPosition.y, this.birthPosition.z);
            playerData.mapIndex = int.Parse(SceneMapData.getInstance().getMapData()[GameManager.instance.sceneName]);
            //修改
            gamePlayer.GetComponent<PlayerPlatformController>().setPlayerData(playerData);

            //保存
            SavePlayerData.SetData("Save/PlayerData.sav", gamePlayer.GetComponent<PlayerPlatformController>().getPlayerData());
            //读取 修改位置地图信息 再保存
            Debug.Log("save: " + playerData.gravityTrans);
            Debug.Log("save: " + playerData.buff.contains(Buff.GRAVITY));

        }
        catch (UnityException e)
        {
            Debug.Log(e.Message);
            gamePlayer.GetComponent<PlayerPlatformController>().getPlayerData().setPlayerVector3DPositionData(this.birthPosition.x, this.birthPosition.y, this.birthPosition.z);
            gamePlayer.GetComponent<PlayerPlatformController>().getPlayerData().mapIndex = int.Parse(SceneMapData.getInstance().getMapData()[GameManager.instance.sceneName]);
            //保存
            SavePlayerData.SetData("Save/PlayerData.sav", gamePlayer.GetComponent<PlayerPlatformController>().getPlayerData());
        }



    }


    //委托的方法
    //玩家通过门重置状态，玩家死亡，玩家重置位置，玩家经过门之后的效果
    private void responseForSignalBROKESPEEDDOOR(Transform transform)
    {
        if (gamePlayer != null)
        {
            gamePlayer.GetComponent<PlayerPlatformController>().getPlayerData().buff.add(Buff.ELASTIC);
            gamePlayer.GetComponent<PlayerPlatformController>().elasticTrans = transform;
            //根据弹力门的方向设置玩家刚体该方向速度为0
            //gamePlayer.GetComponent<PlayerPlatformController>().rig.velocity *= (transform.right.x == 0) ? new Vector2(0, 1) : new Vector2(1, 0);
        }
    }
    private void responseForSignalDEATHDOOR()
    {
        
    }
    private void responseForSignalGDOOR()
    {
        if (gamePlayer != null)
        {
            gamePlayer.GetComponent<PlayerPlatformController>().getPlayerData().flagGravity = 0;
            gamePlayer.GetComponent<PlayerPlatformController>().getPlayerData().buff.add(Buff.GRAVITY);
        }
    }
    private void responseForMAGICALDOOR()
    {
        //Magic
    }
    private void responseForTRANSDOOR(Vector3 newPosition, string curTag)
    {
        if (gamePlayer != null)
        {
            newPosition += (curTag == "transDoor_r") ? new Vector3(-1, 0, 0) : new Vector3(1, 0, 0);
            gamePlayer.GetComponent<PlayerPlatformController>().transform.position = newPosition;
        }
    }
    private void responseForUPSPEEDDOOR()
    {
        if(gamePlayer != null)
        {
            gamePlayer.GetComponent<PlayerPlatformController>().getPlayerData().buff.add(Buff.SUPER);
        }
    }
    private void responseForINITDOOR()
    {
        if (gamePlayer != null)
        {
            gamePlayer.GetComponent<PlayerPlatformController>().getPlayerData().buff.add(Buff.INITSTATE);
        }
    }
    private void responseForDEATH()
    {
        //为人物设置死亡状态

        //销毁人物，3s延迟后销毁
        if (gamePlayer != null)
        {
            gamePlayer.GetComponent<PlayerPlatformController>().getPlayerData().isDead = true;
            Destroy(gamePlayer, 2.0f);
            StartCoroutine(createNewPlayerInBirthPlaceAfterDeath());
        }
            
        
        //创建对应prefeb
        //Quaternion newQuaternion = new Quaternion(0, 0, 0, 0);//实例化预制体的rotation
        // GameObject.Instantiate(prefeb, birthPosition, newQuaternion);
        //GameObject.Instantiate(/*prefeb*/);
    }
    private void responseForREBIRTH()
    {
        if(gamePlayer != null)
            gamePlayer.GetComponent<PlayerPlatformController>().getPlayerData().isBirth = true;
    }
    private void responseForJUMP()
    {
        //播放JUMP音效
        //AudioManager.getInstance().PlaySound("Jump");
        //遍历Player中的buffList列表，查看当前玩家的buff状态
        //如果在接受到JUMP信号时玩家buff状态为SUPER，则移除该buff状态
        if(gamePlayer != null)
        {
            gamePlayer.GetComponent<PlayerPlatformController>().getPlayerData().buff.remove(Buff.SUPER);
        }
        
    }
    private void responseForRUSH()
    {
        //播放RUSH音效
        //AudioManager.getInstance().PlaySound("Rush");
        //遍历Player中的buffList列表，如果在接收到RUSH信号时玩家buff状态为SUPER，则移除该buff状态
        if(gamePlayer != null)
        {
            gamePlayer.GetComponent<PlayerPlatformController>().getPlayerData().buff.remove(Buff.SUPER);
        }
        
    }
    private void responseForELASTICDELETE()
    {
        //接受到ELASTICDELETE信号后设置弹力计时器时间为0，并移除弹力buff
        if(gamePlayer != null)
        {
            gamePlayer.GetComponent<PlayerPlatformController>().getPlayerData().elasticTimer = 0.0f;
            gamePlayer.GetComponent<PlayerPlatformController>().getPlayerData().buff.remove(Buff.ELASTIC);
        }
    }

    private void responseForINITDELETE()
    {
        //接受到INITDELETE信号后移除该buff
        if(gamePlayer != null)
        {
            gamePlayer.GetComponent<PlayerPlatformController>().getPlayerData().buff.remove(Buff.INITSTATE);
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
        //监听门信号
        EventCenter.AddListener<Transform>(EventType.BROKESPEEDDOOR, responseForSignalBROKESPEEDDOOR);
        EventCenter.AddListener(EventType.DEATHDOOR, responseForSignalDEATHDOOR);
        EventCenter.AddListener(EventType.GDOOR, responseForSignalGDOOR);
        EventCenter.AddListener(EventType.MAGICALDOOR, responseForMAGICALDOOR);
        EventCenter.AddListener<Vector3, string>(EventType.TRANSDOOR, responseForTRANSDOOR);
        EventCenter.AddListener(EventType.UPSPEEDDOOR, responseForUPSPEEDDOOR);
        EventCenter.AddListener(EventType.INITDOOR, responseForINITDOOR);
        //监听玩家信号
        EventCenter.AddListener(EventType.DEATH, responseForDEATH);
        EventCenter.AddListener(EventType.JUMP, responseForJUMP);
        EventCenter.AddListener(EventType.RUSH, responseForRUSH);
        EventCenter.AddListener(EventType.ELASTICDELETE, responseForELASTICDELETE);
        EventCenter.AddListener(EventType.REBIRTH, responseForREBIRTH);
        EventCenter.AddListener(EventType.INITDELETE, responseForINITDELETE);
        //
        EventCenter.AddListener<GameObject>(EventType.DESTROY, responseForDESTROY);
    }

    IEnumerator createNewPlayerInBirthPlaceAfterDeath()
    {
        yield return new WaitForSeconds(3.0f);
        if(gamePlayer == null)
        {
            birthPlayer();
            EventCenter.Broadcast(EventType.REBIRTH);
        }
            
    }
}
