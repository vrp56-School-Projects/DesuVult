﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float moveAcceleration = 2f;

    public bool isGrounded;

    private Vector3 velocity;
    private float tempSlopeLimit;
    private float tempStepOffset;

    void Start() {
        tempSlopeLimit = controller.slopeLimit;
        tempStepOffset = controller.stepOffset;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(Vector3.ClampMagnitude(move, 1f) * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {

            //this block helps prevent the player from getting stuck on walls
            tempSlopeLimit = controller.slopeLimit;
            controller.slopeLimit = 90f;
            tempStepOffset = controller.stepOffset;
            controller.stepOffset = .1f;


            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        isGrounded = ((controller.collisionFlags & CollisionFlags.Below) != 0);

        //If you land on the ground, don't accumulate negative velocity
        //OR
        //If you bump your head, lose upward velocity
        if((isGrounded && velocity.y < 0)||((controller.collisionFlags & CollisionFlags.Above)!= 0))
        {

            //reset our parameters
            controller.slopeLimit = tempSlopeLimit;
            controller.stepOffset = tempStepOffset;


            velocity.y = 0f;
        }

    }

}
