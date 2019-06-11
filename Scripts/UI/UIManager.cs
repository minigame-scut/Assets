﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public void startGameScene()
    {
        SceneManager.LoadScene("map1-0");
        EventCenter.Broadcast<string>(EventType.UITOGAME,"birthPlace1-0-1");
    }
    public void returnGameScene()
    {
        SceneManager.LoadScene("Interface");
        EventCenter.Broadcast(EventType.GAMETOUI);
    }

    public void loadSavedScene()
    {
        try
        {
            PlayerData playerData = (PlayerData)SavePlayerData.GetData("Save/PlayerData.sav", typeof(PlayerData));
            if (playerData == null)
            {
                startGameScene();
                return;
            }
            SceneManager.LoadScene(playerData.mapIndex);
            Vector3 playerPosition = new Vector3(playerData.x, playerData.y, playerData.z);
            EventCenter.Broadcast<Vector3>(EventType.CONTINUEGAME, playerPosition);

        }
        catch (UnityException ue)
        {
            Debug.Log(ue.Message);
        }
    }
    public void changeMV()
    {
        float tempMV = GameObject.FindWithTag("MVControl").GetComponent<Slider>().value;
        GameManager.changeMusicVolum(tempMV);
    }
    public void changeSV()
    {
        float tempSV = GameObject.FindWithTag("SVControl").GetComponent<Slider>().value;
        GameManager.changeSoundVolum(tempSV);
    }
}
