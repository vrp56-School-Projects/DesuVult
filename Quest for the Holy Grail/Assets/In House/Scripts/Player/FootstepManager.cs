using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FootstepManager : MonoBehaviour
{
    public AudioSource audioSource;
    public PlayerController playerMovement;
    bool canPlaySound = false;
    public AudioClip[] audioClipsLeft;
    public AudioClip[] audioClipsRight;
    bool foot = true;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tickTock();
        //Debug.Log(Mathf.Floor(Time.time));
        if (playerMovement.isGrounded
        && (Mathf.Abs(Input.GetAxis("Vertical")) > .5f || Mathf.Abs(Input.GetAxis("Horizontal")) > .5f)
        && canPlaySound ){
            AudioClip[] clips;
            // get our left foot or right foot sound arrays
            // and pan stereo some
            if (foot)
            {
                clips = audioClipsLeft;
                audioSource.panStereo = -.2f;
            } else {
                clips = audioClipsRight;
                audioSource.panStereo = .2f;
            }

            //get a random index into our sound array
            int soundIndex = Mathf.FloorToInt(Random.value * clips.Length);

            //randomize our volume and pitch
            audioSource.volume =Random.Range(.5f,1f);
            audioSource.pitch = Random.Range(.9f,1.2f);


            //play the desired sound
            audioSource.PlayOneShot(clips[soundIndex]);
            canPlaySound = false; //wait for timer
            foot = !foot; //switch feet
        }
    }

    private float lastTime = 0f;
    public float timeBetweenPlay = .4f;

    //This helps time playback so that sounds do not play every frames
    void tickTock()
    {
        //just renaming Time.time here
        float currentTime = Time.time;

        //If enough time has passed, allow another sound to play
        if (currentTime - lastTime > timeBetweenPlay) {
            lastTime = currentTime;
            canPlaySound = true;

        }
    }
}
