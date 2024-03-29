﻿public enum MyEventType{
    //各类门的信号
    BROKESPEEDDOOR,  //弹力门
    DEATHDOOR,       //死亡门
    GDOOR,           //重力门
    INWORLDDOOR,     //进入里世界的门
    MAGICALDOOR,     //魔法门
    OUTWORLDDOOR,    //出去里世界的门
    TRANSDOOR,       //传送门  传送本地图内
    TRANSDOORTOWORLD,  //传送门  传送到不同的地图
    UPSPEEDDOOR,     //加速门
    INITDOOR,        //重置门
    COLORTRANSDOOR,  //门颜色转换
    //玩家信号
    WALK,            //玩家移动
    DEATH,           //玩家死亡
    BIRTH,           //玩家诞生
    REBIRTH,         //玩家重生
    NEXTPLACE,       //玩家进入下一关
    JUMP,            //玩家跳
    RUSH,            //玩家冲
    ELASTICDELETE,   //弹力buff消除
    INITDELETE,      //重置buff消除
    NEXTMAP,        //玩家进入下一大关卡
    //道具信号
    DESTROY,          //道具销毁
    UITOGAME,        //进入游戏
    GAMETOUI,         //返回UI    
    CONTINUEGAME,     //继续游戏
    PLAYERPAUSE,       //玩家暂停
    HELLODEMO,       //进入demo
    //游戏
    ANIMPAUSE,       //顿帧
    SHAKESCREEN,     //震动
    WAVE,            //波纹
    //NPC
    DIALOG,         //对话信号
}
