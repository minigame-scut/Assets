using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoLogic : MonoBehaviour
{
    public GameObject gamePlayer;
    // Start is called before the first frame update
    void Start()
    {
        listener();
    }

    // Update is called once per frame
    void Update()
    {
        //gamePlayer.transform.position = new Vector3(12.8f, 0, 0);
    }
    void responseForHELLODEMO(Vector3 pos)
    {
        if (pos.y <= 8.6f && pos.y >= 8.4f)
            gamePlayer.transform.position = new Vector3(gamePlayer.transform.position.x, -7f, 0);
        else if(pos.y >= -8.6f && pos.y <= -8.4f)
            gamePlayer.transform.position = new Vector3(gamePlayer.transform.position.x, 7f, 0);
        else if(pos.x >= -14.5f && pos.x <= -14.3f)
            gamePlayer.transform.position = new Vector3(12.8f, gamePlayer.transform.position.y, 0);
        else if(pos.x <= 13.9f && pos.x >= 13.7f)
            gamePlayer.transform.position = new Vector3(-13.4f, gamePlayer.transform.position.y, 0);
    }
    void listener()
    {
        EventCenter.AddListener<Vector3>(EventType.HELLODEMO, responseForHELLODEMO);
    }
}
