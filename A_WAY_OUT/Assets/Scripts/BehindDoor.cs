using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindDoor : MonoBehaviour
{
    public GameObject enemy;
    public bool isDisppear = true;

    private bool enterBox = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enterBox)
        {
            enemy.SetActive(true);
        }
        else if(!enterBox && isDisppear)
        {
            enemy.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Player enter");
        enterBox = true;
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("Player out");
        enterBox = false;
    }
}
