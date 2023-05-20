using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    
    //public Text ScoreTextUI;
    private GameObject main;
    public GameObject Player;       //设置公共变量
    Plane playerScript;
    public GameObject life2;            //获得生命值2和3
    public GameObject life3;        
    // Start is called before the first frame update
    void Start()
    {
        //取得Audiosource组件
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(audio.clip);
        life3 = GameObject.Find("/Canvas/Lifes/life1");
        life2 = GameObject.Find("/Canvas/Lifes/life2");
        main = GameObject.Find("游戏主控");
        playerScript = Player.GetComponent<Plane>();        //获取对象下的脚本

    }

    // Update is called once per frame
    void Update()
    {
        float Bulletspeed = 5.5f * Time.deltaTime;
        //设置子弹速度

        transform.Translate(0, Bulletspeed, 0, Space.Self);
        //设置移动方向
        Vector3 Bulletps = Camera.main.WorldToScreenPoint(transform.position);
        //转换为屏幕坐标  Bullet position
        
        if (Bulletps.y > Screen.height)
        {   //Y轴坐标大于屏幕高度
            if (this.gameObject)
            {
                //Destroy(this.gameObject);
                //超出边界销毁子弹对象
                ReturnPool();

            }
        }

    }

    //碰撞回调方法
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag.Equals("Enemy1"))
        //如果碰撞对方是敌人1
        {
            //Enemy.Instance.OneDie();  不能用单例，会在另外一个对象位置上产生爆炸
            collision.gameObject.GetComponent<Enemy>().Die();
            collision.gameObject.GetComponent<Enemy>().Destroy();
            main.SendMessage("AddScore");
            //GameControl._instance.AddScore();   //单例
            //销毁对方所对应的游戏对象
            Destroy(collision.gameObject);
            //Destroy(this.gameObject);
            //销毁子弹
            ReturnPool();
            //去激活
        }
        
        else if(collision.tag.Equals("Enemy2"))
   //如果碰撞对方是敌人2
        {
            collision.gameObject.GetComponent<Enemy>().Upblood();
            //print("攻击无效");
            //Destroy(this.gameObject);
            //销毁子弹
            ReturnPool();
            //去激活
        }
        else if (collision.tag.Equals("Enemy3"))

        //如果碰撞对方是敌人3
         {
            collision.gameObject.GetComponent<Enemy>().Die();
            collision.gameObject.GetComponent<Enemy>().Destroy();
            main.SendMessage("AddScore");
            //销毁对方所对应的游戏对象
            Destroy(collision.gameObject);
            //Destroy(this.gameObject);
            //销毁子弹
            ReturnPool();
            //去激活

        }
        else if (collision.tag.Equals("Buff"))
        {
            if (GameControl._instance.lifes < 3)
            {
                GameControl._instance.lifes++;
            }
            if (GameControl._instance.lifes == 3)
            {       //如果当前生命值小于3
               // GameControl._instance.ChangeLife(3);    //更改value值，生命值增加
                life3.SetActive(true);
            }else if (GameControl._instance.lifes == 2)
            {
                //GameControl._instance.ChangeLife(2);    //更改value值，生命值增加
                life2.SetActive(true);
            }
            main.SendMessage("DestroyArray");   //消灭存放敌人2的数组

            //调用销毁main中list组件的方法
            //销毁buff
            Destroy(collision.gameObject);
            //Destroy(this.gameObject);
            //销毁子弹
            ReturnPool();
            //去激活
        }
    }
    
    public void ReturnPool()
    {
        if (Plane.instance.list1.Count > 0)
        {
            //去激活
            Plane.instance.list1[0].SetActive(false);
            //将list[0]中的数组返回对象池
            BulletPool._instance.SaveObject(Plane.instance.list1[0]);
            //从当前的数组中删除
            Plane.instance.list1.RemoveAt(0);

        }
    }
}
