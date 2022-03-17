using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakUpAI : MonoBehaviour
{
    //public GameObject ai;
    public AIMove aimove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Enemy Weakup");
            aimove.ChangeAiStatePatrol();
        }
    }
}
