using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    private InGame _inGame;
    private InGame.Score _score;
    public TextMeshProUGUI scoreText;
    private ScoreMeter _scoreMeter;
    private ScoreFileReader _scoreFileReader;
    public Button pauseButton, returnButton, resumeButton, retryButton;
    private bool _paused;
    public GameObject pauseMenu;
    private const float InitTimerTime = -2; //Delay the game start

    // Start is called before the first frame update

    private void Awake()
    {
        _scoreFileReader = GetComponent<ScoreFileReader>();
        _inGame = GetComponent<InGame>();
    }

    void Start()
    {
        pauseMenu.SetActive(false);
        TextAsset txt = Resources.Load(DataHolder.ScoreName) as TextAsset;
        _score = _scoreFileReader.ReadFromTaxtAsset(txt);
        _inGame.SetScore(_score);
        _scoreMeter = DataHolder.ScoreMeter;
        _scoreMeter.Clear();
        _scoreMeter.SetCount("Total", _score.NoteCount());
        _inGame.onHitNote.AddListener(HitNote);
        pauseButton.onClick.AddListener(PauseOrResume);
        returnButton.onClick.AddListener(Return);
        resumeButton.onClick.AddListener(PauseOrResume);
        retryButton.onClick.AddListener(Retry);
        _inGame.StartNewGame();
        _inGame.Show4KLines();
        _inGame.GetTimer().setTime(InitTimerTime);
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
        _scoreMeter.AddCount(_inGame.GetEval(), 1);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = _scoreMeter.GetScoreString();
        if(Input.GetKeyDown(KeyCode.Escape)) PauseOrResume();
    }

    void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void Retry()
    {
        _inGame.noteCanvas.ClearNoteObjects();
        _scoreMeter.Clear();
        _scoreMeter.SetCount("Total", _score.NoteCount());
        _inGame.StartNewGame();
        _inGame.GetTimer().setTime(InitTimerTime);
        _paused = false;
        pauseMenu.SetActive(false);
    }
}
