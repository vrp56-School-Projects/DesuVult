using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    enum type{
        intro,
        outro
    };
    [SerializeField]
    type thisType;
    bool triggerNext = false;

    [System.Serializable]
    public class action
    {
        public float[] startLoc = new float[3];
        public float[] endLoc = new float[3];
        public float movDelay, movDuration;

        public float[] startRot = new float[3];
        public float[] endRot = new float[3];
        public float rotDelay, rotDuration;
        
    }

    public action[] actions;

    public int[] ints;

    IntroManager manager;
    int ind = 0;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<IntroManager>();
        StartCoroutine(Transform(actions));
        StartCoroutine(Rotate(actions));
    }

    // transform and repeat control
    IEnumerator Transform(action[] a)
    {
        manager.PlayClip(0);

        // initialize
        float newX = a[0].startLoc[0];
        float newY = a[0].startLoc[1];
        float newZ = a[0].startLoc[2];

        transform.position = new Vector3(newX, newY, newZ);

        // delay
        yield return new WaitForSeconds(a[0].movDelay);

        // do action
        float start = Time.time;
        manager.PlayClip(1);

        while(true)
        {
            float percent = (Time.time - start)/a[0].movDuration;

            // if 100%, end transform
            if(percent >= 1)
            {
                newX = a[0].endLoc[0];
                newY = a[0].endLoc[1];
                newZ = a[0].endLoc[2];

                transform.position = new Vector3(newX, newY, newZ);

                break;
            }

            // else, move
            else
            {
                newX = Mathf.Lerp(a[0].startLoc[0], a[0].endLoc[0], percent);
                newY = Mathf.Lerp(a[0].startLoc[1], a[0].endLoc[1], percent);
                newZ = Mathf.Lerp(a[0].startLoc[2], a[0].endLoc[2], percent);

                transform.position = new Vector3(newX, newY, newZ);
            }
            yield return new WaitForEndOfFrame();
        }

        // wait for rotation to finish
        yield return new WaitForSeconds((a[0].rotDelay + a[0].rotDuration) - (a[0].movDelay + a[0].movDuration));

        // execute again if list isn't empty
        if(a.Length-1 > 0)
        {
            // remove action
            var a_list = new List<action>(a);
            a_list.RemoveAt(0);

            while(thisType == type.outro && !triggerNext) yield return new WaitForEndOfFrame();
            triggerNext = false;

            // restart
            StartCoroutine(Transform(a_list.ToArray()));
            StartCoroutine(Rotate(a_list.ToArray()));
        }
    }

    // rotation control
    IEnumerator Rotate(action[] a)
    {
        // initialize
        float newX = a[0].startRot[0];
        float newY = a[0].startRot[1];
        float newZ = a[0].startRot[2];

        transform.rotation = Quaternion.Euler(newX, newY, newZ);

        // delay
        yield return new WaitForSeconds(a[0].rotDelay);

        // do action
        float start = Time.time;

        while(true)
        {
            float percent = (Time.time - start)/a[0].rotDuration;

            // if 100%, end transform
            if(percent >= 1)
            {
                newX = a[0].endRot[0];
                newY = a[0].endRot[1];
                newZ = a[0].endRot[2];

                transform.rotation = Quaternion.Euler(newX, newY, newZ);

                break;
            }

            // else, move
            else
            {
                newX = Mathf.Lerp(a[0].startRot[0], a[0].endRot[0], percent);
                newY = Mathf.Lerp(a[0].startRot[1], a[0].endRot[1], percent);
                newZ = Mathf.Lerp(a[0].startRot[2], a[0].endRot[2], percent);

                transform.rotation = Quaternion.Euler(newX, newY, newZ);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void Trigger()
    {
        triggerNext = true;
    }
}
