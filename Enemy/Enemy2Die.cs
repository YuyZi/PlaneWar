using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Die : MonoBehaviour
{
    public float sp = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, sp);
    }

}
