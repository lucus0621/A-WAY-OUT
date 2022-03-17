using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    public string mainscene;

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
        if (other.tag == "Enemy")
        {
            Spwan();
        }
    }

    private void Spwan()
    {
        SceneManager.LoadScene("mainscene");
    }
}
