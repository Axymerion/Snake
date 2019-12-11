using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadColliderScript : MonoBehaviour
{
    public GameObject HeadObject;
    private void OnTriggerEnter(Collider other)
    {
        HeadObject.gameObject.GetComponent<SnakeMovement>().SnakeCollided(other);
    }
}
