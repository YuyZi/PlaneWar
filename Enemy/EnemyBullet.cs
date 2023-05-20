using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public static float B2S = 2f;

    public GameObject GameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float Bullet2speed = B2S * Time.deltaTime;
        //设置子弹速度

        transform.Translate(0, -Bullet2speed, 0, Space.Self);
        //设置移动方向

        Vector3 Bullet2ps = Camera.main.WorldToScreenPoint(transform.position);
        //转换为屏幕坐标  Bullet position
        
        if (Bullet2ps.y < 0)
        {   //出界销毁
            if (this.gameObject)
            {
                Destroy(this.gameObject);
                //超出边界销毁子弹对象
            }

        }
    }
    //敌军攻击触发事件
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.tag.Equals("Player"))

        //如果碰撞对方是玩家

        {
            GameControl._instance.lifes--;
            collision.SendMessage("Hit");  //扣血
            //if (GameControl._instance.lifes==0)
            //{
                
            //    collision.SendMessage("PlayerDie"); //死亡
            //}


            Destroy(this.gameObject);
     
        }
    }




}
