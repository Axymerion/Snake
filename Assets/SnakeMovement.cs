using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public GameObject tailPrefab;
    public GameObject snakeParent;
    public float speed = 1;
    [Range(1, 20)]public float movementSmoothing = 20;
    public static int food = 0;
    Quaternion currDirection;
    List<SnakeElement> tailList = new List<SnakeElement>();

    struct SnakeElement
    {
        public GameObject segment;
        public  Vector3 lastPosition;

        public SnakeElement(GameObject segment, Vector3 lastPosition)
        {
            this.segment = segment;
            this.lastPosition = lastPosition;
        }

    }

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

    void UpdateSnake()
    {
        transform.rotation = currDirection;
        for (int i = 0; i < tailList.Count; i++)
        {
            SnakeElement temp = tailList[i];
            temp.lastPosition = temp.segment.transform.position;
            tailList[i] = temp;
        }

        if(food > 0)
        {
            tailList.Add(new SnakeElement(Instantiate(tailPrefab, tailList[tailList.Count - 1].segment.transform.position, Quaternion.identity, snakeParent.transform), tailList[tailList.Count - 1].segment.transform.position));
            food--;
        }
    }

    void SmoothMovement()
    {
        transform.Translate(0, 0, 2/movementSmoothing);
        for(int i = 1; i < tailList.Count; i++)
        {
            SnakeElement temp = tailList[i];
            temp.segment.transform.Translate((tailList[i - 1].lastPosition - temp.lastPosition) / movementSmoothing);
        }
    }

    public void SnakeCollided(Collider other)
    {
        if (other.name.StartsWith("Apple"))
        {
            Destroy(other.gameObject);
            food++;
            Food.FoodEaten();
        }
        else if (other.gameObject != tailList[1].segment.gameObject && other.name.StartsWith("Tail"))
        {
            EndGame();
        }
    }

    void EndGame()
    {
        CancelInvoke("UpdateSnake");
        CancelInvoke("SmoothMovement");
    }

    // Start is called before the first frame update
    void Start()
    {
        tailList.Add(new SnakeElement(gameObject, transform.position));
        currDirection = transform.rotation;
        InvokeRepeating("UpdateSnake", 0, 1/speed);
        InvokeRepeating("SmoothMovement", 0, 1 / (speed*movementSmoothing));
    }

    // Update is called once per frame
    void Update()
    {
        SelectRotation();
    }
}