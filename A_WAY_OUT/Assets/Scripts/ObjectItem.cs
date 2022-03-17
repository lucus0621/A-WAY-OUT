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
            ui_Manager.showInteractablePickup(true);
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
            ui_Manager.showInteractablePickup(false);
        }
        isChecked = false;
    }
}
