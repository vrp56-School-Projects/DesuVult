using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _message;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartMessage());
    }

    IEnumerator StartMessage()
    {
        _message.text = "Kill samurai until you die. Press 'Escape' to quit.";
        yield return new WaitForSeconds(5f);

        _message.text = "";
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
}
