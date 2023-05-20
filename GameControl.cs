using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       //UI
using UnityEngine.Audio;        //音频
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using Photon.Pun;


//Cannot implicitly convert type 'string' to 'UnityEngine.UI.Text'

public class GameControl : MonoBehaviour
{
    public static GameControl _instance;    //单例
    public GameObject StartUI;              //UI部分
    public GameObject[] EnemyPerfab;        //读档用
    public GameObject myPrefab;             //读档用
    public GameObject EnemyPrefab;          //读档用    
    public GameObject[] DiePerfab;          //读档用

    public bool isPause = false;         //初始为非暂停状态
    public GameObject pauseMenu;                //暂停后出现的Panel
    public GameObject AboutMenu;                //关于界面

    public GameObject PlanePerfab;       //飞机生成
    public GameObject BuffPerfab;         //Buff相关
    public  int lifes = 3;       //定义生命值
    public int Score;       // 记录当前得分
    public Text scoreText;      // 指向 '得分'节点下的 Text 组件

    public bool GetPlayer = false;           //判断是否生成玩家飞机，如果没有生成则生成
    public AudioMixer audioMixer;       //Slider音效调整
    public Slider slider;               //Slider组件

    private void Awake()
    {
        _instance = this;       //单例
    }
    public void ChangeLife(int _value)
    {   //传一个参数进去，然后那个参数来修改你的life
        lifes += _value;
    }
    //返回游戏
    public void Returngame()
    {
        SceneManager.LoadScene(0); //切换到场景0  开始界面

    }
    //开始游戏
    public void Startgame()
    {
        SceneManager.LoadScene(1); //切换到场景1     开始游戏

    }

//生成buff
    void Start()
    {
        //print("游戏开始，请使用WASD键移动，按ESC可以进行暂停，Space继续游戏,退出游戏请按两下ESC键");
        Application.targetFrameRate = 60;
        int index = SceneManager.GetActiveScene().buildIndex;       //获取当前场景编号
        //如果是场景1/2且加入房间后
        if (index == 1&&Launcher.JoinRoom)
        {
            //Creat();
            InvokeRepeating("CreateBuff", 10f, 10f);    //懒得封装改参数了，直接复制两把
            // 生成BUFF方法 时间 调用间隔

        }
        else if (index == 2 && Launcher.JoinRoom)
        {
            //Creat();                    //重新生成飞机
            Score = 10;                 //如果到第二关，即20分的时候,初始分数为10
            scoreText.text = Score.ToString();  
            Enemy.Enemyspeed = 1f;      //加快敌人飞行速度
            CreatEnemy.ct = 1.5f;         //加快生成速度
            EnemyBullet.B2S = 2.5f;     //加快敌人子弹速度
            InvokeRepeating("CreateBuff", 8f, 8f);      //更改BUFF生成时间和间隔
            // 生成BUFF方法 时间 调用间隔
        }
    }
    ////生成飞机
    //private void Creat()
    //{       GameObject Player = Instantiate(PlanePerfab);
    //        //游戏开始实例化飞机
    //        Player.transform.position = new Vector3(-0.01f, -4f, 0);
    //        //生成位置


    //}
    //buff生成方法
    private void CreateBuff()
    {
        float x = Random.Range(-2.25f, 2.25f);
        //横坐标位置随机
        float y = 5;

        GameObject Buff = PhotonNetwork.Instantiate("BUFF", new Vector3(x, y, 0), Quaternion.identity, 0);
        //GameObject Buff = Instantiate(BuffPerfab);

        //Buff.transform.position = new Vector3(x, y, 0);

        //指定位置生成
    }


    //设置面板
    private void Update()
    {
        SettingMenu();
        //封装使用暂停和开始方法
    }
    static int pressNum = 0;            //暂停计数器
    public void SettingMenu()
    {   
        //int pressNum = 0;计数器用来判断是否按了两下ESC
        //判断按下ESC和空格
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnPause();
        }

    }
    //暂停
    public void Pause()
    {
        isPause = true;
        pauseMenu.SetActive(true);
        //调用界面
        Time.timeScale = 0f;
        pressNum++;
        print("游戏暂停,按Space键继续，再次按Esc退出游戏" + pressNum);
        if (pressNum == 2)
        {   //按两下ESC退出
            //再次点击ESC则退出游戏
            Quit();
            print("退出游戏" + pressNum);
            pressNum = 0;
        }
    }
    //取消暂停
    public void UnPause()
    {
        pressNum--; //计数器
        isPause = false;
        pauseMenu.SetActive(false);
        //界面消失
        Time.timeScale = 1f;
    }

   //退出游戏
    public void ExitGame()
    {
        Quit();
        print("退出游戏" );
    }
    //关于界面UI按钮事件UI
    public void AboutUs()
    {
        AboutMenu.SetActive(true);
        //展示关于的内容
        StartUI.SetActive(false);
        //关闭Start部分
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AboutMenu.SetActive(false);
            //关闭界面
            AboutMenu.SetActive(false);
            //展示关于的内容
            StartUI.SetActive(true);
        }
    }
    //返回
    public void Return()
    {
        AboutMenu.SetActive(false);
        //展示关于的内容
        StartUI.SetActive(true);
    }
    //退出游戏的方法
    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        //开发者模式
        #else
            Application.Quit();
            //打包后
        #endif
    }
    //加分方法
    public void AddScore()
    {
        Score += 1;
        scoreText.text = Score.ToString();
        if (Score == 10)
        {
            SceneManager.LoadScene(2);      //切换到场景3
        }
        if (Score == 20)
        {
            SceneManager.LoadScene(3);      //切换到场景3
        }
    }
   
    public void SetVolume()
    {
        //(float value) 代表浮点型数值，名字为value,他是一个内部的临时变量（具体内容可以在C#入门中学习）
        audioMixer.SetFloat("MainVolume", slider.value);
        //MainVolume为刚才上面的MyExposedParam更改后的名字
    }
    public void SaveGame()
    {
        SaveByBin();		//设置按键事件使用方法
        print("保存成功");
    }
    public void LoadGame()
    {
            //string dirPath = Application.dataPath + "/streamFile" + "/SaveByBin.txt";
            //DirectoryInfo mydir = new DirectoryInfo(dirPath);
            //print(dirPath);
            //if (!mydir.Exists)
        if (!File.Exists(Application.dataPath + "/StreamFile" + "/SaveByBin.txt"))
        {

                GetPlayer = true;
                print("没有存档，开始新游戏");
            }
            else
            {

                GetPlayer = true;
                LoadByBin();
                print("读档成功，继续游戏");
            }
    }
    public void PauseLoadGame()
    {
        LoadByBin();
        print("读档成功");
    }

    public Save CreateSaveObj()
    {
        //调用save类，首先实例化save类，通过new这个关键词实例化
        Save save = new Save();

        //遍历当前场景中所有游戏对象，并放到liveObj数组中。typeof 返回的type类型。
        GameObject[] liveObj = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

        foreach (GameObject item in liveObj)
        {
            if (item.name.Contains("Clone"))
            {   //Clone游戏对象保存在save对象中
                save.saveLiveType.Add(item.tag);
                save.xPst.Add(item.transform.position.x);
                save.yPst.Add(item.transform.position.y);
            }
        }

        save.saveScore = Score;	//保存分数
        return save;
    }

    //将CreateSaveObj方法中保存的游戏对象信息转换成字节流
    public void SaveByBin()
    {
        //将CreateSaveObj方法中保存的数据转换成字节流
        Save save = CreateSaveObj();

        //创建一个二进制格式器
        BinaryFormatter bf = new BinaryFormatter();

        //创建一个文件流保存CreateSaveObj方法所保存的数据
        //1. 在Unity编辑器中Application.dataPath: Unit的Assets文件夹
        //2. 游戏程序打包后Application.dataPath:  应用的appname_Data文件夹
        FileStream fileStream = File.Create(Application.dataPath + "/StreamFile" + "/SaveByBin.txt");
        //未打包时需要在Assets层级目录中新建一个streamFile文件夹来存放txt文档

        //把CreateSaveObj方法所保存的数据转换成字节流，然后保存在filestream中的SaveByBin.txt文件中
        bf.Serialize(fileStream, save);

        //关闭下流
        fileStream.Close();
    }
    //激活保存的save对象，生成保存的游戏对象
    private void SetSaveObj(Save save)
    {
        int i = 0;
        //销毁当前场景所有name里面包含“Clone”的游戏对象
        GameObject[] liveObj = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        foreach (GameObject item in liveObj)
        {
            if (item.name.Contains("Clone"))
            {
                Destroy(item);
            }
        }

        //通过反序列化得到的save对象，从save对象中获取到saveliveGameObj，激活数组中的游戏对象
        foreach (string item in save.saveLiveType)
        {
            switch (item)
            {	//Red 指Tag Block【1】指代码数组中的序号对象名
                case "Enemy1":
                    Instantiate(EnemyPerfab[0], new Vector3(save.xPst[i], save.yPst[i], 0), Quaternion.identity);
                    break;
                case "Enemy2":
                    Instantiate(EnemyPerfab[1], new Vector3(save.xPst[i], save.yPst[i], 0), Quaternion.identity);
                    break;
                case "Enemy3":
                    Instantiate(EnemyPerfab[2], new Vector3(save.xPst[i], save.yPst[i], 0), Quaternion.identity);
                    break;
                case "Player":
                    Instantiate(PlanePerfab, new Vector3(save.xPst[i], save.yPst[i], 0), Quaternion.identity);
                    break;
                case "Bullet":
                    Instantiate(myPrefab, new Vector3(save.xPst[i], save.yPst[i], 0), Quaternion.identity);
                    break;
                case "Bullet2":
                    Instantiate(EnemyPrefab, new Vector3(save.xPst[i], save.yPst[i], 0), Quaternion.identity);
                    break;
                case "BUFF":
                    Instantiate(BuffPerfab, new Vector3(save.xPst[i], save.yPst[i], 0), Quaternion.identity);
                    break;
                case "1Die":
                    Instantiate(DiePerfab[0], new Vector3(save.xPst[i], save.yPst[i], 0), Quaternion.identity);
                    break;
                case "2Die":
                    Instantiate(DiePerfab[1], new Vector3(save.xPst[i], save.yPst[i], 0), Quaternion.identity);
                    break;
                case "3Die":
                    Instantiate(DiePerfab[2], new Vector3(save.xPst[i], save.yPst[i], 0), Quaternion.identity);
                    break;
                case "PlayerDie":
                    Instantiate(DiePerfab[3], new Vector3(save.xPst[i], save.yPst[i], 0), Quaternion.identity);
                    break;
            }
            i++;
        }
        scoreText.text = save.saveScore.ToString();
        Score = save.saveScore;
        i = 0;
        UnPause();		//取消暂停，结合之前判断暂停的方法
    }

    //加载游戏
    public void LoadByBin()
    {
        if (File.Exists(Application.dataPath + "/StreamFile" + "/SaveByBin.txt"))
        {
            //反序列化过程，加载的时候new  原来的要么悬挂要么释放，释放后指向新的地址
            BinaryFormatter bf = new BinaryFormatter();
            //打开一个文件流
            FileStream filestream = File.Open(Application.dataPath + "/StreamFile" + "/SaveByBin.txt", FileMode.Open);
            //转定义 查看定义 进行更改 添加“，FileMode.Open” 调用格式化程序的反序列化方法，将文件流转换成一个save对象
            Save save = (Save)bf.Deserialize(filestream);
            //关闭文件流
            filestream.Close();
            SetSaveObj(save);
        }
    }

}


