using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;



public enum KindofTrans
{
    NEXTPLACE,  // 通过nextplace进行的传送
    TRANSDOOR,  //通过transDoor进行的传送
    DEFAULT,    //默认

}

//游戏管理类

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    Dictionary<string, string> mapData;

    //玩家的object
    static public GameObject player;

 

    public string sceneName;

    static  KindofTrans kindofTrans = KindofTrans.DEFAULT;

    static string toPlace; //通往关卡的标号
    static string toTrans;

    int toPlaceIndex;  //小关卡的下标
    int toBigPlaceIndex;  //大关卡的下标



 
        //不销毁GameManager
        // DontDestroyOnLoad(this); 
        void Awake()
        {
            if (instance == null)
            {
                // 判定 null 是保证场景跳转时不会出现重复的 GlobalScript 实例 (主要是跳转回上一个场景)
                // 在没有 GlobalScript 实例时才创建 GlobalScript 实例
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                // 保证场景中只有唯一的 GlobalScript 实例，如果有多余的则销毁
                Destroy(gameObject);
            }
        }
       
    


    // Start is called before the first frame update
    void Start()
    {
      
        //instance = this;

        //读取玩家资源
        //建议使用资源管理类
        player = Resources.Load<GameObject>("Image/Roles/Player/playerTestPrefab");


        //初始化映射关系
        SceneMapData.getInstance().init();
        mapData = SceneMapData.getInstance().getMapData();

        //获取当前场景的name
        sceneName = SceneManager.GetActiveScene().name;

        //监听玩家关卡转换
        EventCenter.AddListener<string>(EventType.NEXTPLACE, toNextPlace);

        //监听传送门的转换
        EventCenter.AddListener<string>(EventType.TRANSDOORTOWORLD, toTransDoor);


        //只有初始化toplace后才能生成玩家位置
        if (toPlace != null && kindofTrans == KindofTrans.NEXTPLACE)
        {
            //Transform birthPlacePosition = GameObject.Find(toPlace).transform;
            //Debug.Log("birthPlacePosition" + birthPlacePosition.position);
            ////生成玩家 
            //GameObject.Instantiate(player, birthPlacePosition.position, Quaternion.identity);
            //kindofTrans = KindofTrans.DEFAULT;
        }
        else
        {

        }
        


    }

    // Update is called once per frame
    void Update()
    {
        sceneName = SceneManager.GetActiveScene().name;
        Debug.Log(sceneName);
    }

    //转化关卡
    void toNextPlace(string nowPlace)
    {
        //获取通往的关卡的标号
        toPlace = mapData[nowPlace];
        toPlaceIndex = 0;  //小关卡的下标
        toBigPlaceIndex = 0;  //大关卡的下标

        try
        {
         toBigPlaceIndex = int.Parse(toPlace.Substring(toPlace.IndexOf('-') - 1, 1));
         toPlaceIndex = int.Parse(toPlace.Substring(toPlace.IndexOf('-') + 1, 1));
        }
        catch (UnityException e)
        {
            Debug.Log("error_placeIndex");
        }
     
        Debug.Log(toPlace);
        Debug.Log(toPlaceIndex);
        //转移到新的场景
         SceneManager.LoadScene("map" + toBigPlaceIndex + '-' +toPlaceIndex);

        sceneName = SceneManager.GetActiveScene().name;

        kindofTrans = KindofTrans.NEXTPLACE; //指明是通过nextplace进行传送的

 
        //切换场景 生成玩家
         StartCoroutine(waitForFindForNextPlace());

    }

    void toTransDoor(string toTransDoor)
    {
        toTrans = toTransDoor;  //目标传送门的标值
        toPlaceIndex = 0;  //小关卡的下标
        toBigPlaceIndex = 0;  //大关卡的下标


        Debug.Log("toTransDoor  " + toTransDoor);
        try
        {
            toBigPlaceIndex = int.Parse(toTrans.Substring(toTrans.IndexOf('-') - 1, 1));
            toPlaceIndex = int.Parse(toTrans.Substring(toTrans.IndexOf('-') + 1, 1));
         
        }
        catch (UnityException e)
        {
            Debug.Log("error_placeIndex");
        }

        Debug.Log(toPlace);
        Debug.Log(toPlaceIndex);
        //转移到新的场景
        SceneManager.LoadScene("map" + toBigPlaceIndex + '-' + toPlaceIndex);
        kindofTrans = KindofTrans.TRANSDOOR; //指明是通过tansdoor进行传送的

        StartCoroutine(waitForFindForTransDoor());
    }

    IEnumerator waitForFindForNextPlace()
    {
      
        yield return new WaitForSeconds(1);
        Transform birthPlacePosition = GameObject.Find(toPlace).transform;
        Debug.Log("birthPlacePosition" + birthPlacePosition.position);
        //生成玩家 
        GameObject.Instantiate(player, birthPlacePosition.position, Quaternion.identity);
        kindofTrans = KindofTrans.DEFAULT;

    }

    IEnumerator waitForFindForTransDoor()
    {

        yield return new WaitForSeconds(1);
        Transform toTransPosition = GameObject.Find(toTrans).transform;
        Debug.Log("toTransPosition" + toTransPosition.position);
        //生成玩家 
        GameObject.Instantiate(player, toTransPosition.position, Quaternion.identity);
        kindofTrans = KindofTrans.DEFAULT;

    }

}
