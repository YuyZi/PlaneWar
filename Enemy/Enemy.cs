using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class Enemy : MonoBehaviour
{
    public AudioSource Music;
    public static Enemy Instance;    //����
    public static float Enemyspeed = 0.6f ;
    //���ù����ٶ�
    public GameObject EnemyPrefab;
    // Ԥ������Դ '�ӵ�2'

    public bool Fire=true;      //���״̬
    //public float ShootTime = 0.6f;    // ��ʱ
    // ÿ��0.6�뷢��һ�� ���Զ���
    public GameObject PerfabOne;
    public GameObject PerfabTwo;
    public GameObject PerfabThree;
    private GameObject Gamectrl;
    int blood = 0;      //ÿ�����ﶼ���¶���һ��blood ��������
    public LayerMask FindPlyaer;        //����һ��ͼ��;
    // Update is called once per frame
    private void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        Gamectrl = GameObject.Find("��Ϸ����");
        // ��ʱ
        //InvokeRepeating("Fire2", 0.5f, ShootTime);

    }
    public void Die()
    {       //���������ϼ�
        if (this.tag.Equals("Enemy1"))
        {
            Instantiate(PerfabOne, transform.position, Quaternion.identity);    //1�ŵ�����������

        }else if (this.tag.Equals("Enemy2"))
        {
            //2�ŵ�����������
            Destroy(this.gameObject);
            print("Destroy Enemy2");    //����
            Instantiate(PerfabTwo, transform.position, Quaternion.identity);
        }else if (this.tag.Equals("Enemy3"))
        {
            //3�ŵ�����������
            Instantiate(PerfabThree, transform.position, Quaternion.identity);
        }
    }


    public void Upblood()
    {
        blood++;    //public ������Bullet �ű�����
        print(blood);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))

        //�����ײ�Է������

        {
            GameControl._instance.lifes--;
            collision.SendMessage("Hit");  //��Ѫ
            //plane.PlayerDie();
            //if (GameControl._instance.lifes == 0)
            //{
            //    collision.SendMessage("PlayerDie"); //����
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
        {   //���Ϊ�ɹ���״̬
            Vector2 pos = transform.position;   //��ȡ��ɫλ��
            RaycastHit2D attack = Physics2D.Raycast(pos, Vector2.down, 20, FindPlyaer);
            //���߼�ⷵ�ز���  ��������Ϊ ����λ�ã����򣬳��ȣ��Լ�ͼ�㡣
            if (attack)
            {
                //ʹ��rigidbody2dҪʹ��physics2d
                //����Ͷ�䣬pos Ϊ��ʼλ�� Vector2.downΪ���� ���£� 20����ʾ���߳���
                //���������ײ�����Ϊtrue��ִ�����к���
                Fire2();
                Fire = false;     //״̬���ģ�֮��0.5s�����ٴ�ִ��
                                  //Э��
                StartCoroutine(waitTime(2f));
            }
            
        }
    }
    IEnumerator waitTime(float time)
    {
        //���ı���
        yield return new WaitForSeconds(time);//��ʱ���ã�֮������ظ�ִ���������
        Fire = true;//��Ϊ��
    }
    // Update is called once per frame
    void Update()
    {
            Attack();
        float dy = Enemyspeed * Time.deltaTime;
        //�����ƶ��ٶ�
        transform.Translate(0, -dy, 0);
        //����λ�ã����ƶ�
        Vector3 Enemyps = Camera.main.WorldToScreenPoint(transform.position);
        if (Enemyps.y < 0)
        {
            Destroy(this.gameObject);
            //�����±߽����ٹ���
        }
        Enemy2();       //ÿһ֡���ã��ж�ʲôʱ�����
    }
    


    private void Fire2()
    {
        GameObject bullet2 = Instantiate(EnemyPrefab);
        bullet2.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        //����ɼ��ӵ�
       /*if (Launcher.JoinRoom)
        {
            PhotonNetwork.Instantiate("bullet2", transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity, 0);
        }
        else
        {
            
        }*/
        
        
    }
    public void Destroy()
    {   //������Ч
        Music.Play();
    }






}



