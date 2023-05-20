using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Die : MonoBehaviour
{
    // Start is called before the first frame update
 
    void Start()
    {
        Destroy(this.gameObject, 0.3f);
    }
}
