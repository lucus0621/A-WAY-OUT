using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWayToGate : MonoBehaviour
{
    public static bool key1 = false;
    public AudioSource HideWayBroken;
    public Transform MarkPos;
    private GameObject gameObjectsl;
    public UIManager ui_Manager;

    void Start()
    {
        key1 = false;
        GetComponent<TwoKeyDoor>();
    }

    void OnTriggerStay(Collider collider)
    {
        if (TwoKeyDoor.key1 == true)
        {
            ui_Manager.g_MoveToRoom2.SetActive(true);
            if (Input.GetKey(KeyCode.F))
            {
                HideWayBroken.Play();
                Debug.Log("HideWay");
                gameObjectsl = collider.gameObject;
                collider.gameObject.SetActive(false);
                collider.transform.position = MarkPos.position;
                Invoke("ShowObj", 0.5f);
                ui_Manager.g_MoveToRoom2.SetActive(false);
            }
           
        }
    }

    public void moveToRoom() { 
    
    }
    void ShowObj()
    {
       CancelInvoke();
       gameObjectsl.SetActive(true);
    }
}
