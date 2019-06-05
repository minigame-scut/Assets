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

        mapData = SceneMapData.getInstance().getMapData();
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
        int toPlaceIndex = toPlace[toPlace.IndexOf('-')];
        Debug.Log(toPlace);
        Debug.Log(toPlaceIndex);
    }
}
