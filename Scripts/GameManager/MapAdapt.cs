using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAdapt : MonoBehaviour
{
    public float width = 1024.0f;
    public float height = 597.0f;
    public GameObject mapObject;
    public Camera camera;
    public float sizeOfCamera;
    public float yOfRate;
    public float xOfRate;
    // Start is called before the first frame update
    
    void Start()
    {
        sizeOfCamera = camera.GetComponent<Camera>().orthographicSize;
        yOfRate = sizeOfCamera * 2 * 100 / (float)height;
        xOfRate = sizeOfCamera * 2 * 100 * ((float)Screen.width/Screen.height)/(float)width;
        mapObject.transform.localScale = new Vector3(xOfRate, yOfRate, 0);
        //mapObject.transform.localScale.Set(xOfRate, yOfRate, 0);
    }

    // Update is called once per frame
    void Update()
    {
        sizeOfCamera = camera.GetComponent<Camera>().orthographicSize;
        yOfRate = sizeOfCamera * 2 * 100 / (float)height;
        xOfRate = sizeOfCamera * 2 * 100 * ((float)Screen.width / Screen.height) / (float)width;
        mapObject.transform.localScale = new Vector3(xOfRate, yOfRate, 0);
    }
}
