using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BulletPool : MonoBehaviour
{
    public static BulletPool _instance;  //����
    public GameObject BulletPrefab;       //�ӵ�����
    public GameObject Player;           //�ɻ�����
    public int MaxCount = 5;             //��������
    public List<GameObject> list = new List<GameObject>();     //�½�list����
    private void Awake()
    {
        _instance = this;    //����ʵ����

    }
    //������洢�ڶ������
    public void SaveObject(GameObject obj)
    {
        //������鳤��С����󳤶�,�����صĶ�����ӵ��������
        if (list.Count < MaxCount)
        {
            //��������ӵ�������
            list.Add(obj);
            obj.transform.SetParent(transform);
            //�趨����
            //ע�� ��ӵ���obj  ����Ԥ����
        }
        else
        {
            //������������ٶ��󣬲��Ž�����
            Destroy(obj);
        }
    }

    //��ȡ����صĶ���
    public GameObject GetObject()
    {
        //��Ҫ����һ��GameObject�Ķ���
        if (list.Count > 0 && list.Count < MaxCount)
        {
            //��������д��ڶ���
            //����һ����ʱ����洢�����е�[0]
            GameObject obj = list[0];
            //ɾ��list�е�����0   ��ʱӦ��������û�ж���

            list.RemoveAt(0);
            //���ظö���

            obj.SetActive(false);
            return obj;
        }

        //����һ��ʵ�����Ķ���        ʵ�����ڷɻ�ǰ��
        if (Launcher.JoinRoom)
        {
            return PhotonNetwork.Instantiate("bullet", Player.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, 0);
            
        }
        else
        {
            return Instantiate(BulletPrefab, Player.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
        //

    }
}
