using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject LockPick;
    public GameObject Lock;


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
        Lock.SetActive(true);
        LockPick.SetActive(true);
    }

    public void CloseLock()
    {
        Lock.SetActive(false);
        LockPick.SetActive(false);
    }
}
