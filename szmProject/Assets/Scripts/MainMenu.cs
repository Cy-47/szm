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
        settingButton.onClick.AddListener(GoToSetting);
        if(!PlayerPrefs.HasKey("offset") || !PlayerPrefs.HasKey("flowRate")) GoToSetting();
    }

    // Update is called once per frame
    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    void GoToSetting()
    {
        SceneManager.LoadScene("Setting");
    }
}
