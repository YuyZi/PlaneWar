using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//引用命名空间 ---loadscene

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
           
            //协程 - 等待0.4秒后调用showUI()
            StartCoroutine(showUI());
            //与下面IEnumerator捆绑使用

            //gameOverPrefab.SetActive(false);
        }
    }
    IEnumerator showUI()
    {   
        yield return new WaitForSeconds(2f);  //协程必备，等待时间后从下一行开始
        gameOverPrefab.SetActive(false);
        
        Plane.playerDie = false;    //把playerDie改回false，否则会一直在true & false间横跳

        //GameOver收起，重新加载当前页面
        SceneManager.LoadScene(0);
    }

}
