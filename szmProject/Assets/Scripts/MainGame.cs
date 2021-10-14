using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    private InGame _inGame;
    private InGame.Score _score;
    public TextMeshProUGUI scoreText;
    public ScoreMeter scoreMeter;
    private ScoreFileReader _scoreFileReader;
    private AudioClip _music;
    public Button pauseButton, returnButton, resumeButton, retryButton;
    private bool _paused;
    public GameObject pauseMenu;
    
    // Start is called before the first frame update

    private void Awake()
    {
        _scoreFileReader = GetComponent<ScoreFileReader>();
        _inGame = GetComponent<InGame>();
    }

    void Start()
    {
        pauseMenu.SetActive(false);
        TextAsset txt = Resources.Load("两只老虎") as TextAsset;
        _score = _scoreFileReader.ReadFromTaxtAsset(txt);
        _music = Resources.Load<AudioClip>("宝宝巴士 - 两只老虎");
        _inGame.SetScore(_score);
        _inGame.SetMusic(_music);
        scoreMeter.Clear();
        scoreMeter.SetCount("Total", _score.Length());
        _inGame.onHitNote.AddListener(HitNote);
        pauseButton.onClick.AddListener(PauseOrResume);
        returnButton.onClick.AddListener(Return);
        resumeButton.onClick.AddListener(PauseOrResume);
        retryButton.onClick.AddListener(Retry);
        _inGame.StartNewGame();
        _inGame.GetTimer().setTime(-1);
        _paused = false;
    }

    void PauseOrResume()
    {
        if (_paused)
        {
            _inGame.Resume();
            _paused = false;
            pauseMenu.SetActive(false);
        }
        else
        {
            _inGame.Pause();
            _paused = true;
            pauseMenu.SetActive(true);
        }
    }
    
    void HitNote()
    {
        scoreMeter.AddCount(_inGame.GetEval(), 1);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = scoreMeter.GetScoreString();
        if(Input.GetKeyDown(KeyCode.Escape)) PauseOrResume();
    }

    void Return()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(scoreMeter.gameObject);
    }

    void Retry()
    {
        foreach (Transform child in GameObject.Find("NoteCanvas").transform)
        {
            if(child.name != "ZeroLine") Destroy(child.gameObject);
        }
        scoreMeter.Clear();
        _inGame.StartNewGame();
        _paused = false;
        pauseMenu.SetActive(false);
    }
}
