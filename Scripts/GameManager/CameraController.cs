using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;  //主角
    public float speed;  //相机跟随速度

    //设定一个角色能看到的最远值
    public float Ahead = 1;

    //设置一个摄像机要移动到的点
    public Vector3 targetPos;

    //设置一个缓动速度插值
    public float smooth = 1;

    public 


    void Start()
    {

    }
    void Update()
    {
        if (player == null)
            player = GameObject.Find("player(Clone)");

        if (player != null)
            FixCameraPos();
    }

    void FixCameraPos()
    {
        targetPos = new Vector3(player.transform.position.x, player.transform.position.y, gameObject.transform.position.z);

        transform.position = targetPos;

        //if (player.transform.position.y > 0f)
        //{
        //    targetPos = new Vector3(gameObject.transform.position.x, player.transform.position.y + Ahead, gameObject.transform.position.z);
        //}
        //else
        //{
        //    targetPos = new Vector3(gameObject.transform.position.x - Ahead, player.transform.position.y - Ahead, gameObject.transform.position.z);
        //}

        //  transform.position = Vector3.Lerp(transform.position, targetPos, smooth * Time.deltaTime);

    }

}
