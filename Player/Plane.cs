using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;       //UI
using Photon.Pun;

public class Plane : MonoBehaviourPun
{
    public static Plane instance;  //����
    //����һ������洢��ȡ���Ķ���
    public List<GameObject> list1 = new List<GameObject>();

    // Ԥ������Դ '�ӵ�'
    public GameObject myPrefab;

    //Ԥ���屬ը����
    public GameObject explorePrefab;

    //����״̬
    public static bool playerDie = false;

    //����ֵͼƬui
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    // ��ʱ
    private float interval = 0.5f;
    // ÿ��0.4�뷢��һ�� ������
    private float count = 0;
    //��ȡ����ֵUI
    private void Awake()
    {
        instance = this;    //����
    }
    private void Start()
    {
        life1 = GameObject.Find("/Canvas/Lifes/life3");
        life2 = GameObject.Find("/Canvas/Lifes/life2");
        life3 = GameObject.Find("/Canvas/Lifes/life1");
    }
    //����UI�仯
    public void Hit(){
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(audio.clip);      //�ܻ���Ч

        if (GameControl._instance.lifes == 3)
        {
            life3.SetActive(true);      //�ָ�����ֵ
        }
        else if (GameControl._instance.lifes == 2)
        {
            life3.SetActive(false);     //�ر�UI��ʾ
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
    //�������
    public   void PlayerDie() {
        PhotonNetwork.Instantiate("PlayerDie",transform.position,Quaternion.identity);
        life1.SetActive(true);
        life2.SetActive(true);
        life3.SetActive(true);
        //����ͼ��
        playerDie = true;       //����״̬
        PhotonNetwork.Destroy(this.gameObject);

        //Destroy(this.gameObject);
       // print("�����������Ϸ����");
        //Ȼ������airplanelane��Ϸ����


    }
    void Update()
    {
        //����۲�Ľ�ɫ�ǵ�ǰ�Ľ�ɫ  �����ӵ�������
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        return; 
        Fire();       
        //  �ӵ�����
        // ��ʱ
        count += Time.deltaTime;
        if (count >= interval)
        {
            
            count = 0;
            //���ü�ʱ,ѭ��֮һ
        }
    }
    private void FixedUpdate()
    {
        //����۲�Ľ�ɫ�ǵ�ǰ�Ľ�ɫ  �����ӵ�������
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
            return;
        Moving();

    }
    private void Moving()
    {
        //������Ӧ 
        Vector3 Worldps = transform.position;
        //��ȡ��������

        Vector3 Screenps = Camera.main.WorldToScreenPoint(Worldps);
        //�������������WorldToScreenPoint����  ��������������ת������Ļ����



        float Planespeed = 2.5f * Time.deltaTime;
        //�ƶ��ٶ�
        if (Input.GetKey(KeyCode.A))
        {   //A���ƶ�
            transform.Translate(-Planespeed, 0, 0);

        }
        if (Input.GetKey(KeyCode.D))
        {   //D���ƶ�
            transform.Translate(Planespeed, 0, 0);
            if (Screenps.x > Screen.width)
            {
                Planespeed = 0;
            }

        }
        if (Input.GetKey(KeyCode.W))
        {   //W���ƶ�
            transform.Translate(0, Planespeed, 0);
            if (Screenps.y > Screen.height)
            {
                Planespeed = 0;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {   //W���ƶ�
            transform.Translate(0, -Planespeed, 0);

        }
    }
    //�ӵ�����
    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //GameObject bullet = Instantiate(myPrefab);
            //bullet.transform.position = transform.position + new Vector3(0, 0.5f, 0);
            //�ɻ��Ϸ�0.5fʵ���������ӵ�

            GameObject get = BulletPool._instance.GetObject();
            //���ö���������ӵ�
            get.transform.position = transform.position + new Vector3(0, 0.5f, 0);
            list1.Add(get);
            //���ǰ����
            get.SetActive(true);
        }
    }

    }
