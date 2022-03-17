using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectItem : MonoBehaviour
{

    public string objId;
    public string objName;
    public int count;
    public string note;
    public int level;
    public bool isCanAdd;
    public int maxAdd;

    public bool isChecked;

    bool inInventory = false;
    public UIManager ui_Manager;

    // Use this for initialization
    void Start()
    {
        isChecked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isChecked)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
            //
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
            //
        }
        isChecked = false;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!inInventory)
        {
            ui_Manager.showInteractablePickup(true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (!inInventory)
        {
            ui_Manager.showInteractablePickup(false);
        }
    }
    private void OnDestroy()
    {
        ui_Manager.showInteractablePickup(false);
    }
}
