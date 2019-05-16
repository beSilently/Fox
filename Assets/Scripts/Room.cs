using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int type;

    public void DestroyRoom() 
    {
        Destroy(gameObject);
    }

}
