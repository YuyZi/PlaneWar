using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BulletPool : MonoBehaviour
{
    public static BulletPool _instance;  //单例
    public GameObject BulletPrefab;       //子弹对象
    public GameObject Player;           //飞机对象
    public int MaxCount = 5;             //最大对象数
    public List<GameObject> list = new List<GameObject>();     //新建list数组
    private void Awake()
    {
        _instance = this;    //单例实例化

    }
    //将对象存储在对象池中
    public void SaveObject(GameObject obj)
    {
        //如果数组长度小于最大长度,将返回的对象添加到对象池中
        if (list.Count < MaxCount)
        {
            //将对象添加到数组中
            list.Add(obj);
            obj.transform.SetParent(transform);
            //设定父级
            //注意 添加的是obj  不是预制体
        }
        else
        {
            //如果超出则销毁对象，不放进数组
            Destroy(obj);
        }
    }

    //获取对象池的对象
    public GameObject GetObject()
    {
        //需要返回一个GameObject的对象
        if (list.Count > 0 && list.Count < MaxCount)
        {
            //如果数组中存在对象
            //声明一个临时对象存储数组中的[0]
            GameObject obj = list[0];
            //删除list中的数组0   此时应该数组中没有对象？

            list.RemoveAt(0);
            //返回该对象

            obj.SetActive(false);
            return obj;
        }

        //返回一个实例化的对象        实例化在飞机前方
        if (Launcher.JoinRoom)
        {
            return PhotonNetwork.Instantiate("bullet", Player.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, 0);
            
        }
        else
        {
            return Instantiate(BulletPrefab, Player.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
        //

    }
}
