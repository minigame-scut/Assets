using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//此类用于管理 map 1-6 的传送门
//上方四扇传送门的 name 为 transDoor (161), transDoor (162), ...
//下方四扇传送门的 name 为 transDoor (165), transDoor (166), ...
public class Map1_6TransManager : MonoBehaviour
{
    private Dictionary<string, string> mapTransDoor;    //传送门的颜色变化的映射
    private Dictionary<string, Color> mapColors;    //变化的颜色

    public static Map1_6TransManager instance = null; //单实例

    public bool isFinished = false; //上方四个传送门是否全部为红色
    public GameObject transDoor_1;
    public GameObject transDoor_2;
    public GameObject transDoor_3;
    public GameObject transDoor_4;

    public GameObject transDoor_0;

    // Start is called before the first frame update
    void Start()
    {
        //创建单实例
        if(instance != null)
        {
            Debug.Log("Map1_6TransManager 实例已存在");
        }
        else
        {
            instance = this;
        }

        //添加传送门的监听
        EventCenter.AddListener<GameObject>(EventType.CHANGETRANDOORCOLOR, responseForCHANGETRANDOORCOLOR);

        //添加传送门的映射
        mapTransDoor = new Dictionary<string, string>();
        mapTransDoor.Add("165", "161 163");
        mapTransDoor.Add("166", "162 164");
        mapTransDoor.Add("167", "162 163");
        mapTransDoor.Add("168", "161 164");

        //添加门的4种变化颜色
        mapColors = new Dictionary<string, Color>();
        mapColors.Add("BLUE", new Color(1.0f, 1.0f, 1.0f));
        mapColors.Add("YELLOW", new Color(1.0f, 1.0f, 0.0f));
        mapColors.Add("RED", new Color(1.0f, 0.0f, 0.0f));
        mapColors.Add("GREEN", new Color(0.0f, 1.0f, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
    }



    //转换门的颜色
    void responseForCHANGETRANDOORCOLOR(GameObject transDoor)
    {
        //提取 传送门的name 的数字
        //格式 transDoor (1), transDoor (2), ...
        string szIndex = System.Text.RegularExpressions.Regex.Replace(transDoor.name, @"[^0-9]+", "");

        //即不是底下的门， do noting
        if (!mapTransDoor.ContainsKey(szIndex)) return;

        Debug.Log(transDoor.name + "发出转换颜色信号");

        //寻找对应的传送门 并 改变其颜色
        string mapIndex = mapTransDoor[szIndex];    //映射序列
        string[] indexes = mapIndex.Split(' '); //分割映射序列

        //对每一扇映射的门做颜色转换
        foreach (string index in indexes)
        {
            GameObject _transDoor = GameObject.Find("transDoor (" + index + ")");
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

        //检查上方四扇门是否全部为红色
        if(transDoor_1.GetComponent<SpriteRenderer>().color.Equals(mapColors["RED"]) &&
            transDoor_2.GetComponent<SpriteRenderer>().color.Equals(mapColors["RED"]) &&
            transDoor_3.GetComponent<SpriteRenderer>().color.Equals(mapColors["RED"]) &&
            transDoor_4.GetComponent<SpriteRenderer>().color.Equals(mapColors["RED"])
            )
        {
            isFinished = true;
        }

    }

    //返回一个随机的传送门
    public GameObject genRandomTransDoor()
    {
        if(!isFinished)
        {
            int randKey = new System.Random().Next(1, 5);
            switch(randKey)
            {
                case 1: return transDoor_1;
                case 2: return transDoor_2;
                case 3: return transDoor_3;
                case 4: return transDoor_4;
            }
        }
        
        return transDoor_0;
    }
}
