using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏管理类

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
        //初始化映射关系
        SceneMapData.instance.intit();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
