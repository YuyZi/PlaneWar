using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;       //UI
using Photon.Pun;

public class Plane : MonoBehaviourPun
{
    public static Plane instance;  //单例
    //声明一个数组存储读取到的对象
    public List<GameObject> list1 = new List<GameObject>();

    // 预制体资源 '子弹'
    public GameObject myPrefab;

    //预制体爆炸动画
    public GameObject explorePrefab;

    //死亡状态
    public static bool playerDie = false;

    //生命值图片ui
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    // 定时
    private float interval = 0.5f;
    // 每隔0.4秒发射一颗 发射间隔
    private float count = 0;
    //获取生命值UI
    private void Awake()
    {
        instance = this;    //单例
    }
    private void Start()
    {
        life1 = GameObject.Find("/Canvas/Lifes/life3");
        life2 = GameObject.Find("/Canvas/Lifes/life2");
        life3 = GameObject.Find("/Canvas/Lifes/life1");
    }
    //受伤UI变化
    public void Hit(){
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(audio.clip);      //受击音效

        if (GameControl._instance.lifes == 3)
        {
            life3.SetActive(true);      //恢复生命值
        }
        else if (GameControl._instance.lifes == 2)
        {
            life3.SetActive(false);     //关闭UI显示
            life2.SetActive(true);
        }
        else if(GameControl._instance.lifes == 1)
        {
            life2.SetActive(false);
        }
        else if (GameControl._instance.lifes == 0)
        {
            life1.SetActive(false);
            PlayerDie();
        }


    }
    //玩家死亡
    public   void PlayerDie() {
        PhotonNetwork.Instantiate("PlayerDie",transform.position,Quaternion.identity);
        life1.SetActive(true);
        life2.SetActive(true);
        life3.SetActive(true);
        //重置图标
        playerDie = true;       //更改状态
        PhotonNetwork.Destroy(this.gameObject);

        //Destroy(this.gameObject);
       // print("玩家死亡，游戏结束");
        //然后销毁airplanelane游戏对象


    }
    void Update()
    {
        //如果观察的角色是当前的角色  且链接到服务器
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        return; 
        Fire();       
        //  子弹发射
        // 计时
        count += Time.deltaTime;
        if (count >= interval)
        {
            
            count = 0;
            //重置计时,循环之一
        }
    }
    private void FixedUpdate()
    {
        //如果观察的角色是当前的角色  且链接到服务器
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
            return;
        Moving();

    }
    private void Moving()
    {
        //按键响应 
        Vector3 Worldps = transform.position;
        //获取世界坐标

        Vector3 Screenps = Camera.main.WorldToScreenPoint(Worldps);
        //主摄像机，调用WorldToScreenPoint方法  并传入世界坐标转换成屏幕坐标



        float Planespeed = 2.5f * Time.deltaTime;
        //移动速度
        if (Input.GetKey(KeyCode.A))
        {   //A键移动
            transform.Translate(-Planespeed, 0, 0);

        }
        if (Input.GetKey(KeyCode.D))
        {   //D键移动
            transform.Translate(Planespeed, 0, 0);
            if (Screenps.x > Screen.width)
            {
                Planespeed = 0;
            }

        }
        if (Input.GetKey(KeyCode.W))
        {   //W键移动
            transform.Translate(0, Planespeed, 0);
            if (Screenps.y > Screen.height)
            {
                Planespeed = 0;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {   //W键移动
            transform.Translate(0, -Planespeed, 0);

        }
    }
    //子弹发射
    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //GameObject bullet = Instantiate(myPrefab);
            //bullet.transform.position = transform.position + new Vector3(0, 0.5f, 0);
            //飞机上方0.5f实例化产生子弹

            GameObject get = BulletPool._instance.GetObject();
            //利用对象池生成子弹
            get.transform.position = transform.position + new Vector3(0, 0.5f, 0);
            list1.Add(get);
            //激活当前对象
            get.SetActive(true);
        }
    }

    }
