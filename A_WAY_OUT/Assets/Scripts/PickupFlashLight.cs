using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFlashLight : MonoBehaviour
{
    public static bool flashLight = false;
    // Start is called before the first frame update
    private void Start()
    {
        flashLight = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (flashLight == true)
        {
            GetComponent<FlashLight>().enabled = true;
        }
    }
}
