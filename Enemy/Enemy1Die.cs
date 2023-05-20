using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Die : MonoBehaviour
{

    void Start()
    {
        Destroy(this.gameObject, 0.4f);
    }
}
