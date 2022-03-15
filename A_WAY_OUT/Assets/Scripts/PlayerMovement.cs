using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    //walk speed
    public float walkSpeed = 5f;
    //run speed
    public float runSpeed = 6f;
    //gravity
    public float gravity = -9.8f;
    Vector3 velocity;
    public Transform groundCheck;
    //check ground and player
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 2f;
    bool isGrounded;
    public Slider powerSlider;

    private Pack pack;
    private Transform tr;

    private AudioSource audioSource;

    void Start()
    {
        tr = GetComponent<Transform>();
        pack = GetComponent<Pack>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;//on ground
        }

        //jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * walkSpeed * Time.deltaTime);

        // y=1/2*g*t*t
        // gravity change
        velocity.y += gravity * Time.deltaTime;
        
        controller.Move(velocity * Time.deltaTime);
        if (z != 0)
        {
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }
        }
        else if (audioSource.isPlaying && z == 0)
        {
            audioSource.Stop();
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && powerSlider.value > 0.01)//if there is != 0, its not gonna be work,because the value is floatting;
        {
            controller.Move(move * runSpeed * Time.deltaTime);
            powerSlider.value -= runSpeed * Time.deltaTime;
        }
        else
        {
            powerSlider.value += Time.deltaTime;

        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            pack.showPack();
        }

        Debug.DrawRay(tr.position, tr.forward * 1.5f, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(tr.position, tr.forward, out hit, 1.5f))
        {
            Debug.Log ("RayHit:" + hit.collider.gameObject.name + "\n tag:" + hit.collider.tag);
            GameObject gameObj = hit.collider.gameObject;
            ObjectItem obj = (ObjectItem)gameObj.GetComponent<ObjectItem>();
            if (obj != null)
            {
                obj.isChecked = true;
                //Debug.Log(obj.objName);
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (obj.name == "Key1")
                    {
                        TwoKeyDoor.key1 = true;
                    }
                    else if (obj.name == "Key2")
                    {
                        TwoKeyDoor.key2 = true;

                    }
                    obj = pack.getItem(obj);
                    if (obj.count == 0)
                    {
                        //gameObj.SetActive(false);
                        Destroy(gameObj);
                    }
                }
            }
        }

    }

}
