using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappenWhenCheast : MonoBehaviour
{
    public GameObject showCase;
    public AIMove aimove;

    private bool enterBox = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enterBox && aimove.aiState == AIState.Chase)
        {
            showCase.SetActive(true);
        }
        else
        {
            showCase.SetActive(false);
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
