using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏管理类

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    Dictionary<string, string> mapData;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        //初始化映射关系
        SceneMapData.getInstance().init();
        mapData = SceneMapData.getInstance().getMapData();

        //监听玩家关卡转换
        EventCenter.AddListener<string>(EventType.NEXTPLACE, toNextPlace);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //转化关卡
    void toNextPlace(string nowPlace)
    {
        //获取通往的关卡的标号
        string toPlace = mapData[nowPlace];
        int toPlaceIndex = 0;
        try
        {
         toPlaceIndex = int.Parse(toPlace.Substring(toPlace.IndexOf('-') + 1, 1));
        }
        catch (UnityException e)
        {
            Debug.Log("error_placeIndex");
        }
     
        Debug.Log(toPlace);
        Debug.Log(toPlaceIndex);
    }
}
