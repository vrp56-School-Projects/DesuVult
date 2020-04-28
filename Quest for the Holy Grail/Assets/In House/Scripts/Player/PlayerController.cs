using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Camera camera;
    [Header ("Kinematics")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f;
    private float airTimer;
    [SerializeField] private float airTimeThreshold = .5f;
    private Vector3 velocity;
    private float tempSlopeLimit;
    private float tempStepOffset;
    private Vector2 impulse;
    public bool isGrounded = false;
    public bool instantIsGrounded = false;

    [Header("Stats")]
    [SerializeField] private Health health;
    [SerializeField] private Stamina stamina;

    [Header("Attacking")]
    [SerializeField] private float swingSpeed = .2f;
    [SerializeField] private float swingCost = 10f;
    [SerializeField] private float raycastDistance = 3f;
    private bool swingingSword = false;
    




    void Start() {
        tempSlopeLimit = controller.slopeLimit;
        tempStepOffset = controller.stepOffset;
        EventManager.PlayerDamaged += onDamaged;
    }

    void Update()
    {

        handleMove();
        handleJump();
        handleGravity();


        smoothGroundCheck();
        groundCheck();

        //If you land on the ground, don't accumulate negative velocity
        //OR
        //If you bump your head, lose upward velocity
        if ((groundCheck() && velocity.y < 0)||((controller.collisionFlags & CollisionFlags.Above)!= 0))
        {

            //reset our parameters
            controller.slopeLimit = tempSlopeLimit;
            controller.stepOffset = tempStepOffset;


            velocity.y = 0f;
        }

        //Look at and swing at
        LookingAt();
        if(Input.GetButtonDown("Fire1") && !swingingSword && stamina.value >= swingCost){
            print("swinging");
            swingingSword = true;
            StartCoroutine("swingSword");
            stamina.subtract(swingCost);
        }


    }


    bool smoothGroundCheck() {
        if (!groundCheck()) {
            airTimer += Time.deltaTime;
        }
        else {
            airTimer = 0f;
        }
        if (airTimer > airTimeThreshold) {
            isGrounded = false;
            return false;
        }
        isGrounded = true;
        return true;
    }

    bool groundCheck() {
        if ((controller.collisionFlags & CollisionFlags.Below) != 0) {
            instantIsGrounded = true;
            return true;
        }
        instantIsGrounded = false;
        return false;
    }

    void handleJump() {
        if (Input.GetButtonDown("Jump") && smoothGroundCheck())
        {

            //this block helps prevent the player from getting stuck on walls
            tempSlopeLimit = controller.slopeLimit;
            controller.slopeLimit = 90f;
            tempStepOffset = controller.stepOffset;
            controller.stepOffset = .1f;


            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            // smoothedIsGrounded = false;
            airTimer = airTimeThreshold*2;
        }
    }

    void handleMove() {
        impulse.x = Input.GetAxisRaw("Horizontal");
        impulse.y = Input.GetAxisRaw("Vertical");
        impulse.Normalize();


        Vector3 move = transform.right * impulse.x + transform.forward * impulse.y;

        controller.Move(Vector3.ClampMagnitude(move, 1f) * speed * Time.deltaTime);
    }

    void handleGravity() {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void onDamaged(float damage){
        health.damage(damage);
    }

 //Raises PlayerLooked event and returns whatever object has been looked at
    RaycastHit LookingAt()
    {
      Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
          EventManager.CallPlayerLooked(hit);
          return hit;
        }
        return new RaycastHit();
    }

    //Coroutine for timing sword swing after click
    IEnumerator swingSword() {
      RaycastHit hit;
      yield return new WaitForSeconds(swingSpeed);
      print("swung");
      swingingSword = false;
      hit = LookingAt();
      //Make sure we have an actual RaycastHit object and not a dummy
      if (hit.transform? true : false){
        EventManager.CallEnemyDamaged(10f, hit.transform.gameObject);
      }

    }
}
