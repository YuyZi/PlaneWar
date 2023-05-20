using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;       //����
using UnityEngine.SceneManagement;
public class Launcher : MonoBehaviourPunCallbacks
{
    public static bool JoinRoom;
    // Start is called before the first frame update
    void Start()
    {
        
        PhotonNetwork.ConnectUsingSettings();       //ʹ�����úõ�ServiseSettings
    }

    //�Ƿ����ӵ�������
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Welcome To My Game");

        //��������뷿��           ������photon���ֲ��йۿ�����       �����������������������Ĭ��
        //https://doc.photonengine.com/zh-cn/pun/current/getting-started/pun-intro
        PhotonNetwork.JoinOrCreateRoom("PlaneWar",new Photon.Realtime.RoomOptions() { MaxPlayers = 2},default);
        //����ɫ���photonView   �������Ƿ�۲��ɫ 


    }
    //���뷿���
    public override void OnJoinedRoom()
    {
        int index = SceneManager.GetActiveScene().buildIndex;       //��ȡ��ǰ�������
        base.OnJoinedRoom();
        if (index!=0) {
            if (PhotonNetwork.LocalPlayer.IsMasterClient)//�ж��Ƿ��Ƿ���
            {
                PhotonNetwork.Instantiate("airplane", new Vector3(-0.01f, -4f, 0), Quaternion.identity, 0);
                JoinRoom = true;
            }
            else
            {
                GameObject Player = PhotonNetwork.Instantiate("airplane", new Vector3(-0.01f, -4f, 0), Quaternion.identity, 0);
                Player.GetComponent<SpriteRenderer>().color = new Color(129 / 255f, 69 / 255f, 69 / 255f, 255 / 255f);
                //������ɫ
                JoinRoom = true;
            }

        }
    }
}
