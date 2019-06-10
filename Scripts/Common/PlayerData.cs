public class PlayerData
{
    //玩家当前所处的地图编号
    public int mapIndex;

    //玩家的出生位置
    public float x;
    public float y;
    public float z;

    


    //移动参数
    public bool canWalk = true;
    public float MoveSpeed = 5.0f;
    public int Direction = 1;
    //跳跃参数
    public float JumpSpeed = 8.0f;
    public bool canJump = true;
    public float JumpTime = 0.0f;
    //冲刺参数  
    public int TimesofDash = 1;
    public float DashTime = 0.0f;
    //规格参数
    public float size;//128
    //死亡信号
    public bool isDead = false;
    //重生信号
    public bool isBirth = false;
    //持有buff列表
    public BuffStructure buff = BuffStructure.Instance;

    public int CountJ;
    public bool useInput;

    
    
    //标志位
    public int flagGravity = 0;
    public int flagGravityTrans = 1;
    //计时器
    public float inputTimer = 0.0f;    //输入锁
    public float elasticTimer = 0.0f;  //弹力计时器

    public float testForce = 5.0f;

    public void setPlayerVector3DPositionData(double x,double y, double z)
    {
        this.x = (float)x;
        this.y = (float)y;
        this.z = (float)z;
    }
}
