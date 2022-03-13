using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWayToGate : MonoBehaviour
{
    public AudioSource HideWayBroken;
    public Transform MarkPos;
    private GameObject gameObjectsl;
    void OnTriggerEnter(Collider collider)
    {
        HideWayBroken.Play();
        Debug.Log("HideWay");
        gameObjectsl = collider.gameObject;
        collider.gameObject.SetActive(false);
        collider.transform.position = MarkPos.position;
        Invoke("ShowObj", 0.5f);
    }


    void ShowObj()
    {
        CancelInvoke();
        gameObjectsl.SetActive(true);
    }
}
