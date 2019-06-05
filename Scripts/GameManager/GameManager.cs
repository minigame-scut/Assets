using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


//游戏管理类

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    Dictionary<string, string> mapData;

    //玩家的object
    static public GameObject player;

 

    public string sceneName;

    static string toPlace; //通往关卡的标号
    static string toTrans;

    int toPlaceIndex;  //小关卡的下标
    int toBigPlaceIndex;  //大关卡的下标


   

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

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
        if (toPlace != null)
        {
            Transform birthPlacePosition = GameObject.Find(toPlace).transform;
            Debug.Log("birthPlacePosition" + birthPlacePosition.position);
            //生成玩家
            GameObject.Instantiate(player, birthPlacePosition.position, Quaternion.identity);
        }
    


    }

    // Update is called once per frame
    void Update()
    {
        
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
        //SceneManager.LoadScene(3);

       // Debug.Log("birthPlacePosition" + birthPlacePosition.position);
      //  GameObject.Instantiate(player, birthPlacePosition.position,Quaternion.identity);
    }

    void toTransDoor(string nowTransDoor)
    {


    }
   
}
