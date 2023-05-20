using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;       //引用
using UnityEngine.SceneManagement;
public class Launcher : MonoBehaviourPunCallbacks
{
    public static bool JoinRoom;
    // Start is called before the first frame update
    void Start()
    {
        
        PhotonNetwork.ConnectUsingSettings();       //使用设置好的ServiseSettings
    }

    //是否连接到服务器
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Welcome To My Game");

        //建立火加入房间           可以在photon的手册中观看方法       房间名，玩家数，其他设置默认
        //https://doc.photonengine.com/zh-cn/pun/current/getting-started/pun-intro
        PhotonNetwork.JoinOrCreateRoom("PlaneWar",new Photon.Realtime.RoomOptions() { MaxPlayers = 2},default);
        //给角色添加photonView   服务器是否观察角色 


    }
    //加入房间后
    public override void OnJoinedRoom()
    {
        int index = SceneManager.GetActiveScene().buildIndex;       //获取当前场景编号
        base.OnJoinedRoom();
        if (index!=0) {
            if (PhotonNetwork.LocalPlayer.IsMasterClient)//判断是否是房主
            {
                PhotonNetwork.Instantiate("airplane", new Vector3(-0.01f, -4f, 0), Quaternion.identity, 0);
                JoinRoom = true;
            }
            else
            {
                GameObject Player = PhotonNetwork.Instantiate("airplane", new Vector3(-0.01f, -4f, 0), Quaternion.identity, 0);
                Player.GetComponent<SpriteRenderer>().color = new Color(129 / 255f, 69 / 255f, 69 / 255f, 255 / 255f);
                //更改颜色
                JoinRoom = true;
            }

        }
    }
}
