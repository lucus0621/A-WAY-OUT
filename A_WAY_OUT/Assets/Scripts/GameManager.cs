using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isKey = false;
    public GameObject lockpinScene;
    public enum PViews
    {
        NormalGame,
        Lockpin
    }

    public string SceneName1;
    public string SceneName2;

    private bool isKeyScene = false;
    public Camera NormalGame;
    public Camera LockPickCamera;
    public Camera curCamera;
    internal PViews curView;

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
        isKeyScene = true;
        if (isKeyScene)
        {
            SceneManager.LoadScene(SceneName2);
        }
    }

    public void CallMainScene()
    {
        SceneManager.LoadScene(SceneName1);
    }

    public void GetKey()
    {
        isKey = true;
    }

    public void SetCamera(PViews n_View)
    {
        curView = n_View;
        switch (curView)
        {
            case PViews.NormalGame:
               // NormalGame.transform.rotation = NormalGame.transform.rotation;

                curCamera = NormalGame;

                LockPickCamera.gameObject.SetActive(false);
                NormalGame.gameObject.SetActive(true);
                lockpinScene.SetActive(false);
                break;
            case PViews.Lockpin:
               // LockPickCamera.transform.rotation = NormalGame.transform.rotation;
                curCamera = LockPickCamera;

                LockPickCamera.gameObject.SetActive(true);
                NormalGame.gameObject.SetActive(false);
                lockpinScene.SetActive(true);
                break;
        }
    }
}
