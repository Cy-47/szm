using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettleScoreDisplayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject scoreTextObject,
        perfectCountTextObject,
        goodCountTextObject,
        badCountTextObject,
        missCountTextObject,
        gradeTextObject;

    private string _grade;
    private Color color;
    private ScoreMeter _scoreMeter;
    
    void Start()
    {
        _scoreMeter = GameObject.Find("ScoreMeter").GetComponent<ScoreMeter>();
        scoreTextObject.GetComponent<TextMeshProUGUI>().text = _scoreMeter.GetScoreString();
        perfectCountTextObject.GetComponent<TextMeshProUGUI>().text = _scoreMeter.GetCount("Perfect").ToString() + "\nPerfect";
        goodCountTextObject.GetComponent<TextMeshProUGUI>().text = _scoreMeter.GetCount("Good").ToString() + "\nGood";
        badCountTextObject.GetComponent<TextMeshProUGUI>().text = _scoreMeter.GetCount("Bad").ToString() + "\nBad";
        missCountTextObject.GetComponent<TextMeshProUGUI>().text = (_scoreMeter.GetCount("Total") - _scoreMeter.GetCount("Perfect") - _scoreMeter.GetCount("Good") - _scoreMeter.GetCount("Bad")).ToString() + "\nMiss";
        if (_scoreMeter.GetScore() == 1000000)
        {
            _grade = "善哉";
            color = new Color32(255, 118, 117,255);
        }
        else if (_scoreMeter.GetScore() > 960000)
        {
            _grade = "V";
            color = new Color32(255, 234, 167, 255);
        }
        else if (_scoreMeter.GetScore() > 900000)
        {
            _grade = "S";
            color = new Color32(85, 239, 196,255);
        }
        else if (_scoreMeter.GetScore() > 800000)
        {
            _grade = "A";
            color = new Color32(129, 236, 236, 255);
        }
        else if (_scoreMeter.GetScore() > 700000)
        {
            _grade = "B";
            color = new Color32(116, 185, 255, 255);
        }
        else if (_scoreMeter.GetScore() > 600000)
        {
            _grade = "C";
            color = new Color32(108, 92, 231, 255);
        }
        else
        {
            _grade = "寄";
            color = new Color32(178, 190, 195, 255);
        }
        gradeTextObject.GetComponent<TextMeshProUGUI>().SetText(_grade);
        gradeTextObject.GetComponent<TextMeshProUGUI>().color = color;
        Destroy(_scoreMeter.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
