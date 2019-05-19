using UnityEngine;
using System.Collections;

public class LevelGenerationUI : MonoBehaviour
{
    public int mapSize = 4;
    public GameObject[] rooms;
    // index 0 --> not on the solution path, index 1 --> LR, index 2 --> LRT, index 3 --> LRB, index 4 --> LRBT, 
    // index 5 --> closed, index 6 --> start, index 7 --> end
    public int moveAmount;

    int[,] map;

    // Use this for initialization
    void Start()
    {
        LevelGeneration levelGenerator = new LevelGeneration(mapSize);
        map = levelGenerator.MakeMap();

        DrawLevel();
    }

   void DrawLevel() 
   {
        Vector3 currentPosition = this.transform.position;

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                if(map[i, j] != 0)
                Instantiate(rooms[map[i, j]], currentPosition, Quaternion.identity);

                currentPosition.x += moveAmount;
            }

            currentPosition.x = this.transform.position.x;
            currentPosition.y -= moveAmount;
        }

    }

}
