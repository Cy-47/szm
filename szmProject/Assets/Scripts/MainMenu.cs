using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton, settingButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        settingButton.onClick.AddListener(Setting);
    }

    // Update is called once per frame
    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    void Setting()
    {
        SceneManager.LoadScene("Setting");
    }
}
