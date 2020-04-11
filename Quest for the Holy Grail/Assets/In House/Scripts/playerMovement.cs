using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 13f;
    Vector3 velocity;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    public bool isGrounded;
    private float tempSlopeLimit;
    private float tempStepOffset;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

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
