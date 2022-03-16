using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoxFOrLeave : MonoBehaviour
{
    //public GameObject key1;
    //public GameObject key;
    public GameObject player;
    public GameManager gameManager;
    public GameObject AI1;
    public GameObject AI2;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("touch box");
        Debug.Log(other.tag);

        if (gameManager.isKey)
        {
            Debug.Log("TriggerBoxCall");
        }

        if (gameManager.isKey == true && player.tag =="Player")
        {
            AI1.SetActive(true);
            AI2.SetActive(true);
        }
    }
}
