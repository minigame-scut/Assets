using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//此类用于管理 map 1-6 的传送门
//上方四扇传送门的 name 为 transDoor (161), transDoor (162), ...
//下方四扇传送门的 name 为 transDoor (165), transDoor (166), ...
public class ColorTransDoorManager : MonoBehaviour
{
    private Dictionary<string, string> mapTransDoor;    //触发哪扇门，会引起哪些门的变化
    private Dictionary<string, Color> mapColors;    //颜色列表

    public GameObject transDoor_1;
    public GameObject transDoor_2;
    public GameObject transDoor_3;
    public GameObject transDoor_4;
    public bool dIsFinished = false;    //用于调试的控制

    public GameObject transDoor_0;  //本关卡最终的传送门

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //添加传送门之间关于颜色变化的映射
        mapTransDoor = new Dictionary<string, string>();
        mapTransDoor.Add("165", "161 163");
        mapTransDoor.Add("166", "162 164");
        mapTransDoor.Add("167", "162 163");
        mapTransDoor.Add("168", "161 164");

        //初始化颜色列表
        mapColors = new Dictionary<string, Color>();
        mapColors.Add("BLUE", new Color(1.0f, 1.0f, 1.0f));
        mapColors.Add("YELLOW", new Color(1.0f, 1.0f, 0.0f));
        mapColors.Add("RED", new Color(1.0f, 0.0f, 0.0f));
        mapColors.Add("GREEN", new Color(0.0f, 1.0f, 0.0f));

        //添加 颜色传送门 的监听
        EventCenter.AddListener<GameObject>(EventType.COLORTRANSDOOR, responseForCOLORTRANSDOOR);

     
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("SceneMananger").GetComponent<SManager>().getGamePlayer();
    }

    //对信号 颜色传送门 的处理函数
    void responseForCOLORTRANSDOOR(GameObject colorTransDoor)
    {
        //提取 颜色传送门的name 的数字
        //格式 transDoor (1), transDoor (2), ...
        string szIndex = System.Text.RegularExpressions.Regex.Replace(colorTransDoor.name, @"[^0-9]+", "");

        //不是底下的门， do noting
        if (!mapTransDoor.ContainsKey(szIndex)) return;
        Debug.Log(colorTransDoor.name + "发出信号");


        //寻找对应的传送门
        string mapIndex = mapTransDoor[szIndex];    //映射序列
        string[] indexes = mapIndex.Split(' '); //分割映射序列

        //对每一扇映射的门做颜色转换
        foreach (string index in indexes)
        {
            GameObject _transDoor = GameObject.Find("transDoor (" + index + ")");   //若传送门的name的格式改变，此语句也需修改
            Color _color = _transDoor.GetComponent<SpriteRenderer>().color;

            if (_color.Equals(mapColors["BLUE"]))
            {
                _transDoor.GetComponent<SpriteRenderer>().color = mapColors["YELLOW"];
            }
            else if (_color.Equals(mapColors["YELLOW"]))
            {
                _transDoor.GetComponent<SpriteRenderer>().color = mapColors["RED"];
            }
            else if (_color.Equals(mapColors["RED"]))
            {
                _transDoor.GetComponent<SpriteRenderer>().color = mapColors["GREEN"];
            }
            else if (_color.Equals(mapColors["GREEN"]))
            {
                _transDoor.GetComponent<SpriteRenderer>().color = mapColors["BLUE"];
            }
            else
            {
                Debug.Log("此种颜色的传送门未做转换设置");
            }
        }

        //上方四扇门全部为红色, 传送至本关卡的最终的传送门
        if (transDoor_1.GetComponent<SpriteRenderer>().color.Equals(mapColors["RED"]) &&
            transDoor_2.GetComponent<SpriteRenderer>().color.Equals(mapColors["RED"]) &&
            transDoor_3.GetComponent<SpriteRenderer>().color.Equals(mapColors["RED"]) &&
            transDoor_4.GetComponent<SpriteRenderer>().color.Equals(mapColors["RED"]) ||
            dIsFinished
            )
        {
            player.transform.position = transDoor_0.transform.position + new Vector3(0.5f, 0.0f);
            return;
        }


        //进行传送
        GameObject toTransDoor = genRandomTransDoor();
       player.transform.position = toTransDoor.transform.position + new Vector3(0.0f, 1.0f);
    }

    //生成一个随机的传送门(上方四扇门)
    GameObject genRandomTransDoor()
    {
        int randKey = new System.Random().Next(1, 5);
        switch (randKey)
        {
            case 1: return transDoor_1;
            case 2: return transDoor_2;
            case 3: return transDoor_3;
            case 4: return transDoor_4;
        }
        return new GameObject();
    }
}
