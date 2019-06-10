using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class fullscreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //灯笼到按钮，并且获取按钮的Button组件
        Button btn = GameObject.Find("fullScreen").GetComponent<Button>();
        //注册按钮的点击事件
        btn.onClick.AddListener(delegate () {
            this.Btn_Test();
        });
    }

    void Btn_Test()
    {
        //获取设置当前屏幕分辩率
        Resolution[] resolutions = Screen.resolutions;
        //设置当前分辨率
        Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, true);
        Screen.fullScreen = true;  //设置成全屏,
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
