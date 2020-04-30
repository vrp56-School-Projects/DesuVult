using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouselook : MonoBehaviour
{
    public float yawSpeed = 300f;
    public float pitchSpeed = 600f;
    public Transform playerBody;

    public Transform camFollow;

    public float smoothSpeed = 0.125f;

    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerBody = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Camera>().fieldOfView = Options.GetFOV();
        float mouseX = Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * pitchSpeed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        // follow player 
        Vector3 desiredPosition = camFollow.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        playerBody.Rotate(Vector3.up * mouseX * Options.GetSensitivity()/10);
        transform.localRotation = Quaternion.Euler(xRotation * Options.GetSensitivity()/10, 0f, 0f);

    }
}
