﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "player")
        {
            Debug.Log("demo");
            Debug.Log(transform.position);
            EventCenter.Broadcast(EventType.HELLODEMO, transform.position);
        }
    }
}
