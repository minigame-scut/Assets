﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//地图之间的映射关系保存

public class SceneMapData
{
    public static SceneMapData instance;
    //存储映射关系的键值对容器
    private static  Dictionary<string, string> mapData;

    private SceneMapData()
    {

    }

    public static SceneMapData getInstance()
    {
        if(instance == null)
        {
            instance = new SceneMapData();
            return instance;
        }
        else
        {
            return instance;
        }
    }

    public  void init()
    {
      //if(instance != null)
      //  {
            mapData = new Dictionary<string, string>();

            mapData.Add("nextPlace1-1-2", "birthPlace1-2-1");
        //}
        //else
        //{
        //    Debug.Log("error_null_install");
        //}


    }
    //获得这个映射数据
    public Dictionary<string, string> getMapData()
    {
        return mapData;
    }
   //修改映射关系
    public void addMapData(string key, string value)
    {
        //已有这个key
        if (mapData.ContainsKey(key))
        {
            mapData[key] = value;
        }
        else//没有这个key，新建一个key
        {
            mapData.Add(key, value);
        }
    }

    //删除映射关系
    public void removeMapData(string key)
    {
        //已有这个key
        if (mapData.ContainsKey(key))
        {
            mapData.Remove(key);
        }
        else
        {
            Debug.Log("error_noSuchKeyAndValue");
        }
    }


    

}
