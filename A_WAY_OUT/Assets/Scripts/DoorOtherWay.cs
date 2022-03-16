using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOtherWay : MonoBehaviour
{
    private bool enterCollide = false;

    public GameObject Door;
    public GameManager gameManager;
    //public GameManager gameManager;
    public bool Door_false = false;
    Quaternion targetAngels01;

    public AudioSource DoorOpen;
    public bool IsOpenClose = false;

    public bool haveLock = true;

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("enter");
        enterCollide = true;
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("exit");
        enterCollide = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Door_false == true)
        {
            //gameManager.ShowLock();
            Door.transform.rotation = Quaternion.Slerp(Door.transform.rotation, targetAngels01, 1 * Time.deltaTime);

            if (Quaternion.Angle(targetAngels01, Door.transform.rotation) < 1)
            {
                Door.transform.rotation = targetAngels01;
                Door_false = false;
            }

        }

        if (enterCollide)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (haveLock)
                {
                    //gameManager.ShowLock();
                }

                DoorOpen.Play();
                Door_false = true;                

                if (IsOpenClose == false)
                {
                    targetAngels01 = Quaternion.Euler(0, -90, 0);
                    IsOpenClose = true;
                }
                else
                {
                    targetAngels01 = Quaternion.Euler(0, 0, 0);
                    IsOpenClose = false;
                }
            }
        }

    }
}
