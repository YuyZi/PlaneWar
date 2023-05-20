using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;      ////引用命名空间 ---loadscene加载场景

public class ReturnGame : MonoBehaviour
{

    // Start is called before the first frame update

    public void Returngame()
    {
        SceneManager.LoadScene(0); //切换到场景1     开始游戏

    }





}
