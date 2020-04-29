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
    
    IntroManager manager;
    int ind = 0;

    int pos = 0;

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
        if(thisType == type.intro) manager.PlayClip(0);

        // initialize
        float newX = a[pos].startLoc[0];
        float newY = a[pos].startLoc[1];
        float newZ = a[pos].startLoc[2];

        transform.position = new Vector3(newX, newY, newZ);

        // delay
        yield return new WaitForSeconds(a[pos].movDelay);

        // do action
        float start = Time.time;
        if(thisType == type.intro) manager.PlayClip(1);

        while(!triggerNext && a[pos].movDuration != 0)
        {
            float percent = (Time.time - start)/a[pos].movDuration;

            // if 100%, end transform
            if(percent >= 1)
            {
                newX = a[pos].endLoc[0];
                newY = a[pos].endLoc[1];
                newZ = a[pos].endLoc[2];

                transform.position = new Vector3(newX, newY, newZ);

                break;
            }

            // else, move
            else
            {
                newX = Mathf.Lerp(a[pos].startLoc[0], a[pos].endLoc[0], percent);
                newY = Mathf.Lerp(a[pos].startLoc[1], a[pos].endLoc[1], percent);
                newZ = Mathf.Lerp(a[pos].startLoc[2], a[pos].endLoc[2], percent);

                transform.position = new Vector3(newX, newY, newZ);
            }
            yield return new WaitForEndOfFrame();
        }

        // wait for rotation to finish
        yield return new WaitForSeconds((a[pos].rotDelay + a[pos].rotDuration) - (a[pos].movDelay + a[pos].movDuration));

        // execute again if list isn't empty and intro type
        if(a.Length-1 > 0 && thisType == type.intro)
        {
            // remove action
            var a_list = new List<action>(a);
            a_list.RemoveAt(0);

            // restart
            StartCoroutine(Transform(a_list.ToArray()));
            StartCoroutine(Rotate(a_list.ToArray()));
        }

        // execute on next position
        if(thisType == type.outro)
        {
            while(!triggerNext) yield return new WaitForEndOfFrame();
            triggerNext = false;
            StartCoroutine(Transform(actions));
            StartCoroutine(Rotate(actions));
        }
    }

    // rotation control
    IEnumerator Rotate(action[] a)
    {
        // initialize
        float newX = a[pos].startRot[0];
        float newY = a[pos].startRot[1];
        float newZ = a[pos].startRot[2];

        transform.rotation = Quaternion.Euler(newX, newY, newZ);

        // delay
        yield return new WaitForSeconds(a[pos].rotDelay);

        // do action
        float start = Time.time;

        while(!triggerNext && a[pos].rotDuration != 0)
        {
            float percent = (Time.time - start)/a[pos].rotDuration;

            // if 100%, end transform
            if(percent >= 1)
            {
                newX = a[pos].endRot[0];
                newY = a[pos].endRot[1];
                newZ = a[pos].endRot[2];

                transform.rotation = Quaternion.Euler(newX, newY, newZ);

                break;
            }

            // else, move
            else
            {
                newX = Mathf.Lerp(a[pos].startRot[0], a[pos].endRot[0], percent);
                newY = Mathf.Lerp(a[pos].startRot[1], a[pos].endRot[1], percent);
                newZ = Mathf.Lerp(a[pos].startRot[2], a[pos].endRot[2], percent);

                transform.rotation = Quaternion.Euler(newX, newY, newZ);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void Trigger(int position)
    {
        pos = position;
        triggerNext = true;
    }
}
