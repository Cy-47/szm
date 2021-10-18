using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettleManager : MonoBehaviour
{
    public Button returnButton, retryButton;
    
    public TextMeshProUGUI scoreText,
        perfectCountText,
        goodCountText,
        badCountText,
        missCountText,
        gradeText;

    private string _grade;
    private Color _color;
    private ScoreMeter _scoreMeter;
    // Start is called before the first frame update
    void Start()
    {
        returnButton.onClick.AddListener(Return);
        retryButton.onClick.AddListener(Retry);
        
        _scoreMeter = DataHolder.ScoreMeter;
        scoreText.SetText(_scoreMeter.GetScoreString());
        perfectCountText.SetText(_scoreMeter.GetCount("Perfect").ToString() + "\nPerfect");
        goodCountText.SetText(_scoreMeter.GetCount("Good").ToString() + "\nGood");
        badCountText.SetText(_scoreMeter.GetCount("Bad").ToString() + "\nBad");
        missCountText.SetText(_scoreMeter.GetCount("Total") - _scoreMeter.GetCount("Perfect") - _scoreMeter.GetCount("Good") - _scoreMeter.GetCount("Bad") + "\nMiss");
        if (_scoreMeter.GetScore() == 1000000)
        {
            _grade = "善哉";
            _color = new Color32(255, 118, 117,255);
        }
        else if (_scoreMeter.GetScore() > 960000)
        {
            _grade = "V";
            _color = new Color32(255, 234, 167, 255);
        }
        else if (_scoreMeter.GetScore() > 900000)
        {
            _grade = "S";
            _color = new Color32(85, 239, 196,255);
        }
        else if (_scoreMeter.GetScore() > 800000)
        {
            _grade = "A";
            _color = new Color32(129, 236, 236, 255);
        }
        else if (_scoreMeter.GetScore() > 700000)
        {
            _grade = "B";
            _color = new Color32(116, 185, 255, 255);
        }
        else if (_scoreMeter.GetScore() > 600000)
        {
            _grade = "C";
            _color = new Color32(108, 92, 231, 255);
        }
        else
        {
            _grade = "寄";
            _color = new Color32(178, 190, 195, 255);
        }
        gradeText.SetText(_grade);
        gradeText.color = _color;
    }

    // Update is called once per frame
    private void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Retry()
    {
        SceneManager.LoadScene("MainGame");
    }
}
