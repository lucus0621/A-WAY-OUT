using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject LockPick;
    //public GameObject Lock;
    //public GameObject LockPanel;
    public GameObject key;
    public GameObject locks;
    public Camera mainCamera;
    public Camera keyCamera;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   public void ShowLock()
   {
        //LockPanel.SetActive(true);
        mainCamera.enabled = false;
        keyCamera.enabled = true;
        key.SetActive(true);
        locks.SetActive(true);
   }
   
   public void CloseLock()
   {
        //LockPanel.SetActive(false);
        mainCamera.enabled = true;
        keyCamera.enabled = false;
        key.SetActive(false);
        locks.SetActive(false);
    }
}
