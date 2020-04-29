using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Sword : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private GameObject playerNatsuki, playerSatomi, playerHaruno;
    [SerializeField] private Transform followNatsuki, followSatomi, followHaruno;


    private Vector3 velocity;
    private float tempSlopeLimit;
    private float tempStepOffset;
    private Vector2 impulse;

    private Animator anim;
    private Stamina stamina;

    public int sword = 0;
    float[,] delay = new float[3, 4];

    public bool isGrounded = false;
    public bool instantIsGrounded = false;

    public bool isAttacking = false;
    public bool canMove = true;
    public int attackLayer = 0;
    public int attackIndex = 0;


    void Start()
    {
        stamina = GetComponent<Stamina>();
        tempSlopeLimit = controller.slopeLimit;
        tempStepOffset = controller.stepOffset;

        /*
            get sword from PlayerInfo
        */

        switch (sword)
        {
            case 0: // Natsuki
                delay = AttackDetails.NatsukiTimings; // get delay values for attack timing
                playerNatsuki.SetActive(true); // set proper model active
                anim = playerNatsuki.GetComponent<Animator>(); // get proper animator
                Camera.main.GetComponent<MouseLook>().camFollow = followNatsuki; // set proper object for camera to follow
                break;

            case 1: // Satmoi
                delay = AttackDetails.SatomiTimings; // get delay values for attack timing
                playerSatomi.SetActive(true); // set proper model active
                anim = playerSatomi.GetComponent<Animator>(); // get proper animator
                Camera.main.GetComponent<MouseLook>().camFollow = followSatomi; // set proper object for camera to follow
                break;

            case 2: // Haruno
                delay = AttackDetails.HarunoTimings; // get delay values for attack timing
                playerHaruno.SetActive(true); // set proper model active
                anim = playerHaruno.GetComponent<Animator>(); // get proper animator
                Camera.main.GetComponent<MouseLook>().camFollow = followHaruno; // set proper object for camera to follow
                break;
        }
    }

    void Update()
    {
        if (canMove)
        {
            handleMove();
            handleJump();
        }

        handleAttack();

        handleGravity();

        smoothGroundCheck();
        groundCheck();

        //If you land on the ground, don't accumulate negative velocity
        //OR
        //If you bump your head, lose upward velocity
        if ((groundCheck() && velocity.y < 0) || ((controller.collisionFlags & CollisionFlags.Above) != 0))
        {

            //reset our parameters
            controller.slopeLimit = tempSlopeLimit;
            controller.stepOffset = tempStepOffset;


            velocity.y = 0f;
        }
    }

    private float airTimer;
    [SerializeField] private float airTimeThreshold = .5f;
    bool smoothGroundCheck()
    {
        if (!groundCheck())
        {
            airTimer += Time.deltaTime;
        }
        else
        {
            airTimer = 0f;
        }
        if (airTimer > airTimeThreshold)
        {
            isGrounded = false;
            return false;
        }
        isGrounded = true;
        return true;
    }

    bool groundCheck()
    {
        if ((controller.collisionFlags & CollisionFlags.Below) != 0)
        {
            instantIsGrounded = true;
            return true;
        }
        instantIsGrounded = false;
        return false;
    }

    void handleJump()
    {
        if (Input.GetButtonDown("Jump") && smoothGroundCheck())
        {

            //this block helps prevent the player from getting stuck on walls
            tempSlopeLimit = controller.slopeLimit;
            controller.slopeLimit = 90f;
            tempStepOffset = controller.stepOffset;
            controller.stepOffset = .1f;


            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            // smoothedIsGrounded = false;
            airTimer = airTimeThreshold * 2;
        }
    }

    void handleMove() {

        float sprintBonus = 1f;
        anim.speed = 1f;

        if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && stamina.value > Time.deltaTime * 20){
            print(stamina.value);
            sprintBonus = 1.5f;
            stamina.subtract(Time.deltaTime * 20);
            anim.speed = 1.5f;
        }

        impulse.x = Input.GetAxisRaw("Horizontal");
        impulse.y = Input.GetAxisRaw("Vertical");
        impulse.Normalize();

        Vector3 move = transform.right * impulse.x + transform.forward * impulse.y;

        controller.Move(Vector3.ClampMagnitude(move, 1f) * speed * sprintBonus * Time.deltaTime);

        if(impulse.y > 0) anim.SetInteger("condition", 1);
        else if(impulse.y < 0) anim.SetInteger("condition", 2);
        else if(impulse.x > 0) anim.SetInteger("condition", 3);
        else if(impulse.x < 0) anim.SetInteger("condition", 5);
        else anim.SetInteger("condition", 0);
    }

    void handleGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void handleAttack()
    {
        // if grounded
        if (isGrounded && !isAttacking)
        {
            // if attack triggered, reserve attack
            anim.speed = 1;
            if (Input.GetKeyDown(KeyCode.Mouse0)) StartCoroutine(Attack(0));

            else if (Input.GetKeyDown(KeyCode.Mouse1)) StartCoroutine(Attack(1));
        }
    }

    IEnumerator Attack(int type)
    {
        canMove = false;
        isAttacking = true;

        // update layer and index
        attackLayer++;
        attackIndex += type;

        // trigger animation
        anim.SetInteger("condition", 4);
        anim.SetInteger("attackLayer", attackLayer);
        anim.SetInteger("attackIndex", attackIndex);

        // wait for animation
        yield return new WaitForSeconds(delay[attackLayer - 1, attackIndex]);

        // release attack
        isAttacking = false;

        if (attackLayer != 3)
            yield return new WaitForSeconds(.2f);

        // reset if combo over
        if (!isAttacking)
        {
            isAttacking = true;

            attackLayer = 0;
            attackIndex = 0;

            anim.SetInteger("condition", -1);
            anim.SetInteger("attackLayer", attackLayer);
            anim.SetInteger("attackIndex", attackIndex);

            yield return new WaitForSeconds(.3f);
            canMove = true;
            isAttacking = false;
        }
    }
}

