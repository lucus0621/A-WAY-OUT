using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISight : MonoBehaviour
{
    public AIMove aiMove;

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
        //if AI see the player or player's light, it will change the state from sleep/patrol to chase
        if (other.tag == "Player" || other.tag == "Light")
        {
            aiMove.CheckVisibleHostile(other.gameObject);
        }
    }
}
