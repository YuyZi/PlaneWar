using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMove : MonoBehaviour
{
    public float Buffspeed = 0.6f;
    //����BUFF�ٶ�
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Buff���
        float Bfspeed = Buffspeed * Time.deltaTime;
        //BUFF�ƶ��ٶ�
        transform.Translate(0, -Bfspeed, 0);
        //����λ�ã����ƶ�
        Vector3 Buffps = Camera.main.WorldToScreenPoint(transform.position);
        //ps  position
        if (Buffps.y < 0)
        {
            Destroy(this.gameObject);
            //�����±߽�����BUFF
        }
//�����Է�����Ϸ���أ����� ���ؾͱ�����
          
    }

}
