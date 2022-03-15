using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoKeyDoor : MonoBehaviour
{
    public static bool key1 = false;
    public static bool key2 = false;

    private void Start()
    {
        key1 = false;
        key2 = false;
    }

    private void Update()
    {
        if (key1 && key2)
        {
            GetComponent<DoorTrigger>().enabled = true;
        }
    }
}