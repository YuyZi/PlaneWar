using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CreatEnemy : MonoBehaviour
{
    public AudioSource music;   //��ը��Ƶ
    public static CreatEnemy _instance;             //����
    public GameObject[] EnemyPerfab;        //��������
    //�����
    public  static float ct = 2.5f;
    //����һ��list������prefabΪ2�ĵ���,��������ʹ��
    public List<GameObject> EnemyTwoList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // �������

            InvokeRepeating("CreateEnemy", 0.3f, ct);
            // ���� ʱ�� ���ü��


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CreateEnemy()
    {
        //�ж��Ƿ�������״̬ �� �Ƿ��Ƿ������Ǿ����ɹ���
            if (Launcher.JoinRoom&& PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            float x = Random.Range(-2.25f, 2.25f);
            //������λ�����
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
                
            //����鵽����2�����÷ŵ�list������
            //Enemy.transform.position = new Vector3(x, y, 0);
            //ָ��λ������

            //���ѡ����˶�������
        }
    }     
    public void DestroyArray()
    {
        music.Play();
        foreach (GameObject item in EnemyTwoList)//�ҵ�list����
            Destroy(item);
        //����list�������ķ���

    }


        }
