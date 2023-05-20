using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Save这个类保存所有游戏对象

[System.Serializable]
public class Save 
{
    //不需要集成MonoBehaviour类，直接删除即可
    //存放分数
    public int saveScore;
    //存放当前场景中的游戏对象的名字，若存放游戏对象则在做序列化的时候，序列化不了这个游戏对象的位置信息
    public List<string> saveLiveType = new List<string>();
    //存放游戏对象的x轴坐标
    public List<float> xPst = new List<float>();
    //存放游戏对象的y轴坐标
    public List<float> yPst = new List<float>();
}
