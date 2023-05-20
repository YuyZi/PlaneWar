using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class Enemy : MonoBehaviour
{
    public AudioSource Music;
    public static Enemy Instance;    //单例
    public static float Enemyspeed = 0.6f ;
    //设置怪物速度
    public GameObject EnemyPrefab;
    // 预制体资源 '子弹2'

    public bool Fire=true;      //射击状态
    //public float ShootTime = 0.6f;    // 定时
    // 每隔0.6秒发射一颗 可自定义
    public GameObject PerfabOne;
    public GameObject PerfabTwo;
    public GameObject PerfabThree;
    private GameObject Gamectrl;
    int blood = 0;      //每个怪物都重新定义一次blood 不用清零
    public LayerMask FindPlyaer;        //设置一个图层;
    // Update is called once per frame
    private void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        Gamectrl = GameObject.Find("游戏主控");
        // 计时
        //InvokeRepeating("Fire2", 0.5f, ShootTime);

    }
    public void Die()
    {       //死亡方法合集
        if (this.tag.Equals("Enemy1"))
        {
            Instantiate(PerfabOne, transform.position, Quaternion.identity);    //1号敌人死亡动画

        }else if (this.tag.Equals("Enemy2"))
        {
            //2号敌人死亡动画
            Destroy(this.gameObject);
            print("Destroy Enemy2");    //销毁
            Instantiate(PerfabTwo, transform.position, Quaternion.identity);
        }else if (this.tag.Equals("Enemy3"))
        {
            //3号敌人死亡动画
            Instantiate(PerfabThree, transform.position, Quaternion.identity);
        }
    }


    public void Upblood()
    {
        blood++;    //public 可以让Bullet 脚本访问
        print(blood);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))

        //如果碰撞对方是玩家

        {
            GameControl._instance.lifes--;
            collision.SendMessage("Hit");  //扣血
            //plane.PlayerDie();
            //if (GameControl._instance.lifes == 0)
            //{
            //    collision.SendMessage("PlayerDie"); //死亡
            //}

            Destroy(this.gameObject);

        }
    }
    public void Enemy2()
    {
        if (blood >= 5)
        {
            Gamectrl.SendMessage("AddScore");
            Destroy();
            Die();
        }
    }

    // Start is called before the first frame update

    public void Attack()
    {
        if (Fire == true)
        {   //如果为可攻击状态
            Vector2 pos = transform.position;   //获取角色位置
            RaycastHit2D attack = Physics2D.Raycast(pos, Vector2.down, 20, FindPlyaer);
            //射线检测返回参数  参数依次为 发射位置，方向，长度，以及图层。
            if (attack)
            {
                //使用rigidbody2d要使用physics2d
                //光线投射，pos 为起始位置 Vector2.down为方向 向下， 20，表示射线长度
                //如果发生碰撞，这个为true，执行下列函数
                Fire2();
                Fire = false;     //状态更改，之后0.5s不会再次执行
                                  //协程
                StartCoroutine(waitTime(2f));
            }
            
        }
    }
    IEnumerator waitTime(float time)
    {
        //更改变量
        yield return new WaitForSeconds(time);//延时调用，之后可以重复执行上面代码
        Fire = true;//改为真
    }
    // Update is called once per frame
    void Update()
    {
            Attack();
        float dy = Enemyspeed * Time.deltaTime;
        //怪物移动速度
        transform.Translate(0, -dy, 0);
        //更改位置，即移动
        Vector3 Enemyps = Camera.main.WorldToScreenPoint(transform.position);
        if (Enemyps.y < 0)
        {
            Destroy(this.gameObject);
            //超出下边界销毁怪物
        }
        Enemy2();       //每一帧调用，判断什么时候加满
    }
    


    private void Fire2()
    {
        GameObject bullet2 = Instantiate(EnemyPrefab);
        bullet2.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        //网络可见子弹
       /*if (Launcher.JoinRoom)
        {
            PhotonNetwork.Instantiate("bullet2", transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity, 0);
        }
        else
        {
            
        }*/
        
        
    }
    public void Destroy()
    {   //播放音效
        Music.Play();
    }






}



