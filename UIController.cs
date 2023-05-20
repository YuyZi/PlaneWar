using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//���������ռ� ---loadscene

public class UIController : MonoBehaviour
{
    public GameObject gameOverPrefab;
    public GameObject startUI;
    public GameObject startButton;

    private void Update()
    {
        if (Plane.playerDie)
        {
            gameOverPrefab.SetActive(true);
           
            //Э�� - �ȴ�0.4������showUI()
            StartCoroutine(showUI());
            //������IEnumerator����ʹ��

            //gameOverPrefab.SetActive(false);
        }
    }
    IEnumerator showUI()
    {   
        yield return new WaitForSeconds(2f);  //Э�̱ر����ȴ�ʱ������һ�п�ʼ
        gameOverPrefab.SetActive(false);
        
        Plane.playerDie = false;    //��playerDie�Ļ�false�������һֱ��true & false�����

        //GameOver�������¼��ص�ǰҳ��
        SceneManager.LoadScene(0);
    }

}
