public enum EventType{
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
    //玩家信号
    DEATH,           //玩家死亡
    BIRTH,           //玩家诞生
    NEXTPLACE,       //玩家进入下一关
    JUMP,            //玩家跳
    RUSH,            //玩家冲
    //道具信号
    DESTROY          //道具销毁
}
