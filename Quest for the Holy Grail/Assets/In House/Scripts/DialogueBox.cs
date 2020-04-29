using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueBox : MonoBehaviour
{

    enum Scene
    {
        beforeBossBattle,
        afterBossBattle
    };

    [SerializeField] private Text dialogueText;
    [SerializeField] private BlinkFade spaceToContinue;
    [SerializeField] private Scene currentScene;
    [SerializeField] private float textSpeed;
    [SerializeField] private float delay;
    [SerializeField] private CamController cam;
    [SerializeField] private FadeObject BG;

    int i = 0;

    private string[,] scene_swords = new string[3, 4]{
        {
            "Natsuki: You jerk! Give us the cup!",
            "Natsuki: Give us the chalice, baka!",
            "Natsuki: Nani?",
            "Natsuki: What..."
        },{
            "Satomi: I, too, can attest to the adequacy of the priests. Give us the chalice.",
            "Satomi: Your skills were simply insufficient.",
            "Satomi: Nani?",
            "Satomi: Disgusting..."
        },{
            "Haruno: Do what he says or I'll cut your throat :).",
            "Haruno: Let's finish him!",
            "Haruno: Nani?",
            "Haruno: EW KILL HIM!"
        }
    };

    private int[] cam_positions1 = {
    // before boss
        0,
        4,
        3,
        3,
        0,
        5,
        6,
        6,
        0,
        0,
        4
    };

    private int[] cam_positions2 = {
    // after boss
        4,
        3,
        0,
        6,
        0,
        4,
        5,
        0,
        7,
        7,
        3,
        0,
        0,
        6,
        6,
        6,
        6,
        5,
        0,
        2,
        2,
        2,
        0,
        1

    };

    void Start()
    {
        string[] scene = { };
        int[] positions = { };

        if ((int)currentScene == 0)
        {
            scene = new string[]
            {
                "Knight: Villain! Return to me the holy chalice which you have stolen.",
                "Ninja King: Verily, I know not of what you speak...",
                "Knight: Underestimate not the power of the templar priests. The church knows your vile misdeeds.",
                "You shall return the grail or you shall meet your end.",
                scene_swords[PlayerInfo.GetSwordIndex(), 0],
                "Ninja King: Is that... one of the thre legendary 'talking blade waifus'???",
                "My appologies, but I simply can't let you leave with such an item.",
                "Begone, and leave your blade.",
                "Knight: That won't be possible. It seems that I will have to slay you as I did with your armies.",
                "Prepare yourself!",
                "Ninja King: Make peace with your god, templar."
            };
            positions = cam_positions1;
        }
        else
        {
            scene = new string[]
            {
                "Ninja King: NOOOOOO!!!",
                "Knight: Misguided fiend... This was the inevitable planning of god.",
                scene_swords[PlayerInfo.GetSwordIndex(), 1],
                "Ninja King: Please... allow me to keep the chalice... I need it...",
                "Knight: Pray tell, why did you steal it in the first place?",
                "Ninja King: I need it...",
                "... for a costume...",
                scene_swords[PlayerInfo.GetSwordIndex(), 2],
                "Ninja King: (Removes mask...)",
                "This.. is my true form.",
                "Knight: ...",
                scene_swords[PlayerInfo.GetSwordIndex(), 3],
                "Knight: What manner of witchcraft is this?",
                "Ninja King: It's true... I am of european descent much like yourself...",
                "But I find Japanese culture so... INTRIGUING!",
                "All of my ninjas... none of them were Japanese at all...",
                "We were simply playing in costumes...",
                "I called it 'cosplaying'.",
                "Knight: (Takes Holy Grail)",
                "You sir... You who have disowned your own culture...",
                "I shall call you 'weaboo'.",
                "The entire western world will know of your antics.",
                "And they will cringe.",
                "Now, perish from this world, weeb."
            };
            positions = cam_positions2;
        }


        StartCoroutine(Cutscene(scene, positions));
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) NextScene();
    }

    IEnumerator Cutscene(string[] cutscene, int[] cam_positions)
    {

        yield return new WaitForSeconds(delay);

        foreach (string s in cutscene)
        {
            // clear box
            dialogueText.text = "";
            spaceToContinue.StopBlinking();

            // move camera
            if (i > 0)
            {
                if (cam_positions[i] != cam_positions[i - 1])
                    cam.Trigger(cam_positions[i]);
            }
            else
            {
                cam.Trigger(cam_positions[i]);
            }
            i++;

            foreach (char c in s)
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(1 / textSpeed);
            }

            spaceToContinue.StartBlinking();

            while (!Input.GetKey(KeyCode.Space)) yield return new WaitForEndOfFrame();
        }

        BG.StartFade(0);

        yield return new WaitForSeconds(3);

        NextScene();
    }

    void NextScene()
    {
        if (currentScene == Scene.beforeBossBattle)
        {
            // SceneManager.LoadScene();
            Debug.Log("NextScene");
        }
        else
        {
            // SceneManager.LoadScene();
            Debug.Log("Credits");
        }
    }
}
