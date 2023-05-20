using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMove : MonoBehaviour
{
    public float Buffspeed = 0.6f;
    //设置BUFF速度
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Buff相关
        float Bfspeed = Buffspeed * Time.deltaTime;
        //BUFF移动速度
        transform.Translate(0, -Bfspeed, 0);
        //更改位置，即移动
        Vector3 Buffps = Camera.main.WorldToScreenPoint(transform.position);
        //ps  position
        if (Buffps.y < 0)
        {
            Destroy(this.gameObject);
            //超出下边界销毁BUFF
        }
//不可以放在游戏主控，否则 主控就被销毁
          
    }

}
