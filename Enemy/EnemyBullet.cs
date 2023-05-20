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
        //�����ӵ��ٶ�

        transform.Translate(0, -Bullet2speed, 0, Space.Self);
        //�����ƶ�����

        Vector3 Bullet2ps = Camera.main.WorldToScreenPoint(transform.position);
        //ת��Ϊ��Ļ����  Bullet position
        
        if (Bullet2ps.y < 0)
        {   //��������
            if (this.gameObject)
            {
                Destroy(this.gameObject);
                //�����߽������ӵ�����
            }

        }
    }
    //�о����������¼�
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.tag.Equals("Player"))

        //�����ײ�Է������

        {
            GameControl._instance.lifes--;
            collision.SendMessage("Hit");  //��Ѫ
            //if (GameControl._instance.lifes==0)
            //{
                
            //    collision.SendMessage("PlayerDie"); //����
            //}


            Destroy(this.gameObject);
     
        }
    }




}
