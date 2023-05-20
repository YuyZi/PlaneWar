using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bcakground : MonoBehaviour
{

    Transform bg1;

    Transform bg2;

    public float bgspeed = 1.0f;


    // Start is called before the first frame update
    void Start()
    {

        bg1 = GameObject.Find("/Bcakground/游戏背景1").transform;

        bg2 = GameObject.Find("/Bcakground/游戏背景2").transform;

        bg1.position = new Vector3(0, -3.6f, 0);

        bg2.position = new Vector3(0, 6.6f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float Bspeed = bgspeed * Time.deltaTime;

        bg1.Translate(0, -Bspeed, 0);

        bg2.Translate(0, -Bspeed, 0);

        // 交替时，显示的设置位置，以清除float累计误差
        if (bg1.position.y <= -10)
        {
            //bg1.Translate(0, 20, 0);    
            bg1.position = new Vector3(0, 10, 0);
            bg2.position = new Vector3(0, 0, 0);
        }
        if (bg2.position.y <= -10)
        {
            //bg2.Translate(0, 20, 0);
            bg1.position = new Vector3(0, 0, 0);
            bg2.position = new Vector3(0, 10, 0);
        }
    }
}
