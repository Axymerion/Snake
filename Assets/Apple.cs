using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public float rotationsPerSecond = 0.5f;

    void Rotate()
    {
        transform.Rotate(0, 3.6f, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Rotate", 0, 0.01f / rotationsPerSecond);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
