using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public float rotationsPerSecond = 0.5f;
    private bool triggerActive = true;

    void Rotate()
    {
        transform.Rotate(0, 3.6f, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Rotate", 0, 0.01f / rotationsPerSecond);
        Invoke("DisableTrigger", 0.4f);
    }

    void DisableTrigger()
    {
        triggerActive = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(triggerActive && (other.CompareTag("Tail") || other.CompareTag("Apple")))
        {
            Destroy(gameObject);
        }
    }
}
