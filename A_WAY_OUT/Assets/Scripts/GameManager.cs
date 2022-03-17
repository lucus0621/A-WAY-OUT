using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isKey = false;

    public string SceneName1;
    public string SceneName2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallKeyScene()
    {
        SceneManager.LoadScene(SceneName2);
    }

    public void CallMainScene()
    {
        SceneManager.LoadScene(SceneName1);
    }

    public void GetKey()
    {
        isKey = true;
    }
}
