using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    
    //public Text ScoreTextUI;
    private GameObject main;
    public GameObject Player;       //���ù�������
    Plane playerScript;
    public GameObject life2;            //�������ֵ2��3
    public GameObject life3;        
    // Start is called before the first frame update
    void Start()
    {
        //ȡ��Audiosource���
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(audio.clip);
        life3 = GameObject.Find("/Canvas/Lifes/life1");
        life2 = GameObject.Find("/Canvas/Lifes/life2");
        main = GameObject.Find("��Ϸ����");
        playerScript = Player.GetComponent<Plane>();        //��ȡ�����µĽű�

    }

    // Update is called once per frame
    void Update()
    {
        float Bulletspeed = 5.5f * Time.deltaTime;
        //�����ӵ��ٶ�

        transform.Translate(0, Bulletspeed, 0, Space.Self);
        //�����ƶ�����
        Vector3 Bulletps = Camera.main.WorldToScreenPoint(transform.position);
        //ת��Ϊ��Ļ����  Bullet position
        
        if (Bulletps.y > Screen.height)
        {   //Y�����������Ļ�߶�
            if (this.gameObject)
            {
                //Destroy(this.gameObject);
                //�����߽������ӵ�����
                ReturnPool();

            }
        }

    }

    //��ײ�ص�����
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag.Equals("Enemy1"))
        //�����ײ�Է��ǵ���1
        {
            //Enemy.Instance.OneDie();  �����õ�������������һ������λ���ϲ�����ը
            collision.gameObject.GetComponent<Enemy>().Die();
            collision.gameObject.GetComponent<Enemy>().Destroy();
            main.SendMessage("AddScore");
            //GameControl._instance.AddScore();   //����
            //���ٶԷ�����Ӧ����Ϸ����
            Destroy(collision.gameObject);
            //Destroy(this.gameObject);
            //�����ӵ�
            ReturnPool();
            //ȥ����
        }
        
        else if(collision.tag.Equals("Enemy2"))
   //�����ײ�Է��ǵ���2
        {
            collision.gameObject.GetComponent<Enemy>().Upblood();
            //print("������Ч");
            //Destroy(this.gameObject);
            //�����ӵ�
            ReturnPool();
            //ȥ����
        }
        else if (collision.tag.Equals("Enemy3"))

        //�����ײ�Է��ǵ���3
         {
            collision.gameObject.GetComponent<Enemy>().Die();
            collision.gameObject.GetComponent<Enemy>().Destroy();
            main.SendMessage("AddScore");
            //���ٶԷ�����Ӧ����Ϸ����
            Destroy(collision.gameObject);
            //Destroy(this.gameObject);
            //�����ӵ�
            ReturnPool();
            //ȥ����

        }
        else if (collision.tag.Equals("Buff"))
        {
            if (GameControl._instance.lifes < 3)
            {
                GameControl._instance.lifes++;
            }
            if (GameControl._instance.lifes == 3)
            {       //�����ǰ����ֵС��3
               // GameControl._instance.ChangeLife(3);    //����valueֵ������ֵ����
                life3.SetActive(true);
            }else if (GameControl._instance.lifes == 2)
            {
                //GameControl._instance.ChangeLife(2);    //����valueֵ������ֵ����
                life2.SetActive(true);
            }
            main.SendMessage("DestroyArray");   //�����ŵ���2������

            //��������main��list����ķ���
            //����buff
            Destroy(collision.gameObject);
            //Destroy(this.gameObject);
            //�����ӵ�
            ReturnPool();
            //ȥ����
        }
    }
    
    public void ReturnPool()
    {
        if (Plane.instance.list1.Count > 0)
        {
            //ȥ����
            Plane.instance.list1[0].SetActive(false);
            //��list[0]�е����鷵�ض����
            BulletPool._instance.SaveObject(Plane.instance.list1[0]);
            //�ӵ�ǰ��������ɾ��
            Plane.instance.list1.RemoveAt(0);

        }
    }
}
