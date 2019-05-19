using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OldLevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public Transform[] positions;
    public GameObject player;
    public float playerOffset = 5;
    public GameObject[] rooms; // index 0 --> closed, index 1 --> LR, index 2 --> LRT, index 3 --> LRB, index 4 --> LRBT
    public int moveAmount;
    public float minX, minY, maxX;
    public float startTimeBtwRoom = 0.25f;
    public LayerMask room;
    public CinemachineVirtualCamera cvc;

    private int direction;
    private float timeBtwRoom;
    private bool stopGeneration = false;
    private int downCounter = 0;
    private bool playerIsInstantiated;
    private Vector3 playerPos;
    //private Vector2 startPosition;
    //private Vector2 endPosition;

    private void Start()
    {
        int randStartPosition = Random.Range(1, startingPositions.Length);
        transform.position = startingPositions[randStartPosition].position;
        Instantiate(rooms[1], transform.position, Quaternion.identity);

        playerPos = transform.position;
        playerPos.x -= playerOffset;
        playerPos.y += playerOffset;

        direction = Random.Range(1, 6);
    }
    private void Update()
    {
        if(!stopGeneration && timeBtwRoom <= 0)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }

        if(stopGeneration && !playerIsInstantiated)
        {
            playerIsInstantiated = true;
            GameObject playerObject = Instantiate(player, playerPos, Quaternion.identity);
            cvc.Follow = playerObject.transform;
        }
    }

    private void Move()
    {
        if (direction == 1 || direction == 2) // move RIGHT
        {
            if (transform.position.x < maxX)
            {
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int randRoom = Random.Range(1, rooms.Length);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                direction = Random.Range(1, 4);
                downCounter = 0;
            }
            else
            {
                direction = 3;
            }
        }
        else if (direction == 4 || direction == 5) // move LEFT
        {
            if (transform.position.x > minX)
            {
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int randRoom = Random.Range(1, rooms.Length);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
                downCounter = 0;
            }
            else
            {
                direction = 3;
            }
        }
        else // move DOWN
        {
            if (transform.position.y > minY)
            {
                downCounter++;

                Collider2D roomDetector = Physics2D.OverlapCircle(transform.position, 1, room);
                if (roomDetector.GetComponent<Room>().type != 3 && roomDetector.GetComponent<Room>().type != 4)
                {
                    roomDetector.GetComponent<Room>().DestroyRoom();

                    if(downCounter >= 2)
                    {
                        Instantiate(rooms[4], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        int randBottomOpeningRoom = Random.Range(3, 5);
                        Instantiate(rooms[randBottomOpeningRoom], transform.position, Quaternion.identity);
                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int randTopOpeningRoom = Random.Range(2, 4);
                if (randTopOpeningRoom == 3)
                {
                    randTopOpeningRoom = 4;
                }
                Instantiate(rooms[randTopOpeningRoom], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else
            {
                stopGeneration = true;
                FillEmptySpaces();
            }
        }
    }

    private void FillEmptySpaces()
    {
        Collider2D roomDetector;

        foreach (var pos in positions)
        {
            roomDetector = Physics2D.OverlapCircle(pos.transform.position, 1, room);
            if (roomDetector == null)
            {
                int randRoom = Random.Range(0, rooms.Length);
                Instantiate(rooms[randRoom], pos.transform.position, Quaternion.identity);
            }
        }
    }
}
