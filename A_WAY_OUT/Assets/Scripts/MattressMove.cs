using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MattressMove : MonoBehaviour
{
    private bool enterCollide = false;

    public GameObject Mattress;
    public bool Mattress_false = false;
    Quaternion targetAngels01;

    public AudioSource Moving;
    public bool IsOpenClose = false;
    Vector3 targetPosition = new Vector3(0, 20, 0);
    float maxDistanceDelta = 1f;

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
        if (Mattress_false == true)
        {

            Mattress.transform.rotation = Quaternion.Slerp(Mattress.transform.rotation, targetAngels01, 1 * Time.deltaTime);

            if (Quaternion.Angle(targetAngels01, Mattress.transform.rotation) < 1)
            {
                Mattress.transform.rotation = targetAngels01;
                Mattress_false = false;
            }

        }

        if (enterCollide)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Mattress_false = true;

                if (IsOpenClose == false)
                {
                    Moving.Play();
                    targetAngels01 = Quaternion.Euler(0, 0, 100);
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, maxDistanceDelta);
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
