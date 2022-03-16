using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowKey : MonoBehaviour
{
    public Text KeyTXT;
    public int KeyInt = 0;
    public static bool key1 = false;
    public static bool key2 = false;
    // Start is called before the first frame update
    void Start()
    {
        key1 = false;
        key2 = false;
        GetComponent<TwoKeyDoor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TwoKeyDoor.key1 == true && TwoKeyDoor.key2 == true)
        {
            KeyInt = 2;
            KeyTXT.text = KeyInt.ToString();
        }
        else if(TwoKeyDoor.key1 == true)
        {
            KeyInt = 1;
            KeyTXT.text = KeyInt.ToString();
        }
    }
}
