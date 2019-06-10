using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class windowed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //灯笼到按钮，并且获取按钮的Button组件
        Button btn = GameObject.Find("windowed").GetComponent<Button>();
        //注册按钮的点击事件
        btn.onClick.AddListener(delegate () {
            this.Btn_Test();
        });
    }

    void Btn_Test()
    {
        Screen.SetResolution(480, 270, false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
