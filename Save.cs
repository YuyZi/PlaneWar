using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Save����ౣ��������Ϸ����

[System.Serializable]
public class Save 
{
    //����Ҫ����MonoBehaviour�ֱ࣬��ɾ������
    //��ŷ���
    public int saveScore;
    //��ŵ�ǰ�����е���Ϸ��������֣��������Ϸ�������������л���ʱ�����л����������Ϸ�����λ����Ϣ
    public List<string> saveLiveType = new List<string>();
    //�����Ϸ�����x������
    public List<float> xPst = new List<float>();
    //�����Ϸ�����y������
    public List<float> yPst = new List<float>();
}
