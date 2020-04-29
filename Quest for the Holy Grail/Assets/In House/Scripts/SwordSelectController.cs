using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwordSelectController : MonoBehaviour
{
    [SerializeField] private AudioClip natsukiClip, satomiClip, harunoClip;
    [SerializeField] AudioSource swordSound;

    public Text selectionText;
    public FadeObject FG;

    Ray ray;
    RaycastHit hit;

    string highlightedSword = "";
    string selectedSword = "";
    bool confirm = false;

    void Start()
    {
        // hide sword outlines
        OutlineObject[] Swords = GameObject.FindObjectsOfType<OutlineObject>();

        foreach (OutlineObject Sword in Swords)
        {
            Sword.HideOutline();
        }
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<OutlineObject>() != null && hit.collider.name != highlightedSword)
            {
                // hide old outline
                if (highlightedSword != "") GameObject.Find(highlightedSword).GetComponent<OutlineObject>().HideOutline();

                // show outline
                hit.collider.gameObject.GetComponent<OutlineObject>().ShowOutline();

                // show selected
                if (selectedSword != "") GameObject.Find(selectedSword).GetComponent<OutlineObject>().ShowOutline();


                highlightedSword = hit.collider.name;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && highlightedSword != "")
        {
            // show selected
            if (selectedSword != "") GameObject.Find(selectedSword).GetComponent<OutlineObject>().HideOutline();

            selectedSword = highlightedSword;

            if(selectedSword == "Natsuki") swordSound.clip = natsukiClip;
            else if(selectedSword == "Satomi") swordSound.clip = satomiClip;
            else if(selectedSword == "Haruno") swordSound.clip = harunoClip;
            swordSound.Play();

            selectionText.text = selectedSword + " selected. Press 'space' to confirm your weapon.";

            Vector3 pos = GameObject.Find(selectedSword).transform.position;

            Camera.main.transform.LookAt(new Vector3(pos.x, 6.5f, pos.z));
            GetComponent<Sound>().DoSound();
            confirm = true;

        }

        if (Input.GetKeyDown(KeyCode.Space) && confirm)
        {
            if (selectedSword == "Natsuki") PlayerInfo.SetSwordIndex(0);
            else if (selectedSword == "Satomi") PlayerInfo.SetSwordIndex(1);
            else if (selectedSword == "Haruno") PlayerInfo.SetSwordIndex(2);

            print(PlayerInfo.GetSwordIndex());
            FG.StartFade(0);
            StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Level1Dungeon");
    }
}
