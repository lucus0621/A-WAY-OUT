using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMove : MonoBehaviour
{
    private bool enterCollide = false;

    public GameObject Table;
    public bool Table_false = false;
    Quaternion targetAngels01;

    public AudioSource Moving;
    public bool IsMoving = false;

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
        if (Table_false == true)
        {

            Table.transform.rotation = Quaternion.Slerp(Table.transform.rotation, targetAngels01, 1 * Time.deltaTime);

            if (Quaternion.Angle(targetAngels01, Table.transform.rotation) < 1)
            {
                Table.transform.rotation = targetAngels01;
                Table_false = false;
            }

        }

        if (enterCollide)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Table_false = true;

                if (IsMoving == false)
                {
                    Moving.Play();
                    targetAngels01 = Quaternion.Euler(-60, -15, -90);
                    //transform.position = Vector3.MoveTowards(transform.position, targetPosition, maxDistanceDelta);
                    IsMoving = true;
                }
                else
                {
                    targetAngels01 = Quaternion.Euler(0, 0, 0);
                    IsMoving = false;
                }
            }
        }

    }
}
