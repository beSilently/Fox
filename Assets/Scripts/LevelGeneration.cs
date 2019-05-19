using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration
{
    class Position
    {
        public int X, Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

    }

    readonly int mapSize = 4;

    int[,] map;
    // index 0 --> not on the solution path, index 1 --> LR, index 2 --> LRT, index 3 --> LRB, index 4 --> LRBT, 
    // index 5 --> closed, index 6 --> start, index 7 --> end

    int direction;
    int downCounter;
    Position currentPosition;
    bool stopGeneration = false;

    public LevelGeneration(int mapSize)
    {
        this.mapSize = mapSize;
    }

    public int[,] MakeMap()
    {
        map = new int[mapSize, mapSize];
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                map[i, j] = 0;
            }
        }
        int startPositionX = Random.Range(0, mapSize);
        currentPosition = new Position(startPositionX, 0);
        map[currentPosition.Y, currentPosition.X] = 6;

        direction = Random.Range(1, 6);

        while (!stopGeneration)
        {
            Move();
        }

        return map;
    }

    private void Move()
    {
        if (direction == 1 || direction == 2) // move RIGHT
        {
            if (currentPosition.X < mapSize - 1)
            {
                currentPosition.X++;
                map[currentPosition.Y, currentPosition.X] = Random.Range(1, 5);

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
            if (currentPosition.X > 0)
            {
                currentPosition.X--;
                map[currentPosition.Y, currentPosition.X] = Random.Range(1, 5);

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
            if (currentPosition.Y < mapSize - 1)
            {
                downCounter++;

                if (!(map[currentPosition.Y, currentPosition.X] == 3 || map[currentPosition.Y, currentPosition.X] == 4))
                {
                    if (downCounter >= 2)
                    {
                        map[currentPosition.Y, currentPosition.X] = 4;
                    }
                    else
                    {
                        map[currentPosition.Y, currentPosition.X] = Random.Range(3, 5);
                    }
                }

                currentPosition.Y++;

                int randTopOpeningRoom = Random.Range(2, 4);
                if (randTopOpeningRoom == 3)
                {
                    randTopOpeningRoom = 4;
                }
                map[currentPosition.Y, currentPosition.X] = randTopOpeningRoom;

                direction = Random.Range(1, 6);
            }
            else
            {
                stopGeneration = true;
                map[currentPosition.Y, currentPosition.X] = 7;

                FillEmptySpaces();
            }
        }
    }

    private void FillEmptySpaces()
    {
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                if (map[i, j] == 0)
                {
                    map[i, j] = Random.Range(1, 6);
                }
            }

        }
    }
}
