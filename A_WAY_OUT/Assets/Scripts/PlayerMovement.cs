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

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && powerSlider.value > 0.01)//if there is != 0, its not gonna be work,because the value is floatting;
        {
            controller.Move(move * runSpeed * Time.deltaTime);
            powerSlider.value -= runSpeed * Time.deltaTime;
            Debug.Log("RUN");
        }
        else
        {
            powerSlider.value += Time.deltaTime;

        }

    }

}
