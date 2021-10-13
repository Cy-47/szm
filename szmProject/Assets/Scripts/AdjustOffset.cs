using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdjustOffset : MonoBehaviour
{
    private InGame _inGame;
    private AudioClip _music;
    private InGame.Score _score;
    private ScoreFileReader _scoreFileReader;
    public GameObject addButtonObject, minusButtonObject, offsetTextObject, returnButtonObject;
    private int _offset = 0;
    // Start is called before the first frame update
    void Start()
    {
        _inGame = GetComponent<InGame>();
        _scoreFileReader = GetComponent<ScoreFileReader>();
        _music = Resources.Load<AudioClip>("AdjustOffset");
        _score = _scoreFileReader.ReadFromTaxtAsset(Resources.Load("AdjustOffsetScore") as TextAsset);
        _offset = PlayerPrefs.GetInt("offset");
        addButtonObject.GetComponent<Button>().onClick.AddListener(IncreaseOffset);
        minusButtonObject.GetComponent<Button>().onClick.AddListener(DecreaseOffset);
        returnButtonObject.GetComponent<Button>().onClick.AddListener(Return);
        _inGame.SetMusic(_music);
        _inGame.SetScore(_score);
        _inGame.StartNewGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Timer>().GetTime() > 2)
        {
            foreach (Transform child in GameObject.Find("NoteCanvas").transform)
            {
                if(child.name != "ZeroLine") Destroy(child.gameObject);
            }
            _inGame.StartNewGame();
        }
        offsetTextObject.GetComponent<TextMeshProUGUI>().SetText(_offset + "ms");
    }

    void IncreaseOffset()
    {
        _offset += 5;
        PlayerPrefs.SetInt("offset", _offset);
    }
    
    void DecreaseOffset()
    {
        _offset -= 5;
        PlayerPrefs.SetInt("offset", _offset);
    }

    void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
