using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public GameObject Camera;
    public float speed = 1;
    public float movementSmoothing = 10;
    public int food = 0;
    Quaternion currDirection;

    void SelectRotation()
    {
        if (Input.GetKey(KeyCode.UpArrow) && transform.rotation != Quaternion.Euler(0, 180, 0))
        {
            currDirection = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && transform.rotation != Quaternion.Euler(0, 0, 0))
        {
            currDirection = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && transform.rotation != Quaternion.Euler(0, 270, 0))
        {
            currDirection = Quaternion.Euler(0, 90, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && transform.rotation != Quaternion.Euler(0, 90, 0))
        {
            currDirection = Quaternion.Euler(0, 270, 0);
        }
    }

    void SetRotation()
    {
        transform.rotation = currDirection;
    }

    void SmoothMovement()
    {
        transform.Translate(0, 0, 2/movementSmoothing);
        Camera.transform.SetPositionAndRotation(new Vector3(transform.position.x, Camera.transform.position.y, transform.position.z), Camera.transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        currDirection = transform.rotation;
        InvokeRepeating("SetRotation", 0, 1/speed);
        InvokeRepeating("SmoothMovement", 0, 1 / (speed*movementSmoothing));
    }

    // Update is called once per frame
    void Update()
    {
        SelectRotation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.StartsWith("Apple"))
        {
            Destroy(other.gameObject);
            food++;
        }
    }
}