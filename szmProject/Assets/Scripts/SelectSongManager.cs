using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectSongManager : MonoBehaviour
{
    public Button twoTigersButton, fragranceButton, returnButton, settingButton;
    // Start is called before the first frame update
    void Start()
    {
        twoTigersButton.onClick.AddListener(PlayTwoTigers);
        fragranceButton.onClick.AddListener(PlayFragrance);
        returnButton.onClick.AddListener(Return);
        settingButton.onClick.AddListener(GoToSetting);
    }

    void PlayTwoTigers()
    {
        DataHolder.ScoreName = "两只老虎";
        SceneManager.LoadScene("MainGame");
    }
    
    void PlayFragrance()
    {
        DataHolder.ScoreName = "fragrance_draft";
        SceneManager.LoadScene("MainGame");
    }

    void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    void GoToSetting()
    {
        SceneManager.LoadScene("Setting");
    }
}
