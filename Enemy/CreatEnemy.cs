using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CreatEnemy : MonoBehaviour
{
    public AudioSource music;   //爆炸音频
    public static CreatEnemy _instance;             //单例
    public GameObject[] EnemyPerfab;        //怪物数组
    //随机数
    public  static float ct = 2.5f;
    //定义一个list数组存放prefab为2的敌人,后续销毁使用
    public List<GameObject> EnemyTwoList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // 反射机制

            InvokeRepeating("CreateEnemy", 0.3f, ct);
            // 方法 时间 调用间隔


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CreateEnemy()
    {
        //判断是否是联机状态 与 是否是房主，是就生成怪物
            if (Launcher.JoinRoom&& PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            float x = Random.Range(-2.25f, 2.25f);
            //横坐标位置随机
            float y = 5;

            int index = Random.Range(0, EnemyPerfab.Length);


            //GameObject Enemy = Instantiate(EnemyPerfab[index]);
            if (index == 0)
            {
                PhotonNetwork.Instantiate("1", new Vector3(x, y, 0), Quaternion.identity, 0);
            }
            if (index == 1)
            {
                GameObject Enemy = PhotonNetwork.Instantiate("2", new Vector3(x, y, 0), Quaternion.identity, 0);
                EnemyTwoList.Add(Enemy);
            }
            if (index == 2)
            {
                PhotonNetwork.Instantiate("3", new Vector3(x, y, 0), Quaternion.identity, 0);
            }
                
            //如果抽到怪物2，则用放到list数组中
            //Enemy.transform.position = new Vector3(x, y, 0);
            //指定位置生成

            //随机选择敌人对象生成
        }
    }     
    public void DestroyArray()
    {
        music.Play();
        foreach (GameObject item in EnemyTwoList)//找到list数组
            Destroy(item);
        //销毁list组件对象的方法

    }


        }
