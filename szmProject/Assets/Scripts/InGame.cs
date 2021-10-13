using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{
    private Score _score;
    private Timer _timer;
    private KeyCode _key;
    private float _delay;
    public GameObject notePrefab;
    private bool _gameStarted = false;
    private MusicPlayer _musicPlayer;
    public GameObject evalTextObject;
    private EvalText _evalText;
    private string _eval;
    public GameObject scoreMeterObject;
    private ScoreMeter _scoreMeter;
    public GameObject scoreTextObject;
    private TextMeshProUGUI _scoreText;
    private ScoreFileReader _scoreFileReader;
    private AudioClip _music;
    private bool _isInRealGame;
    public GameObject noteCanvas;
    
    public void StartNewGame()
    {
        _gameStarted = false;
        if (_isInRealGame)
        {
            _scoreMeter.SetCount("Total", _score.Length());
            _scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
        }
        CreateNoteObject(_score);
        _musicPlayer.SetMusic(_music);
        //Delay start time
        _timer.setTime(0);
        _timer.StartTimer();
    }

    private void Awake()
    {
        _scoreFileReader = GetComponent<ScoreFileReader>();
        _isInRealGame = SceneManager.GetActiveScene().name == "Game";
        _evalText = evalTextObject.GetComponent<EvalText>();
        print(_evalText);
        _musicPlayer = GetComponent<MusicPlayer>();
        _timer = GetComponent<Timer>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (_isInRealGame)
        {
            _scoreMeter = scoreMeterObject.GetComponent<ScoreMeter>();
            TextAsset txt = Resources.Load("两只老虎") as TextAsset;
            _score = _scoreFileReader.ReadFromTaxtAsset(txt);
            _music = Resources.Load<AudioClip>("宝宝巴士 - 两只老虎");
            StartNewGame();
        }
    }

    public void SetMusic(AudioClip music)
    {
        _music = music;
    }
    
    public void SetScore(Score score)
    {
        _score = score;
    }
    
    // Update is called once per frame
    
    void Update()
    {
        if (_timer.GetTime() > 26) SceneManager.LoadScene("Settle");
        if (_timer.GetTime() > 0 && !_gameStarted)
        {
            _gameStarted = true;
            _musicPlayer.PlayMusicFrom(_timer.GetTime());
        }
        if (Input.anyKeyDown)
        {
            foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                    _key = kcode;
            }
            Transform tno = noteCanvas.transform.Find(_key.ToString());
            if (tno != null)
            {
                NoteObject no = tno.gameObject.GetComponent<NoteObject>();
                _delay = _timer.GetTime() - no.GetTime() - PlayerPrefs.GetInt("offset")/1000f;
                Debug.Log(_delay);
                if (Math.Abs(_delay) < 0.08) _eval = "Perfect";
                else if (Math.Abs(_delay) < 0.2) _eval = "Good";
                else if (Math.Abs(_delay) < 0.6) _eval = "Bad";
                else _eval = null;
                if (_eval != null)
                {
                    _musicPlayer.PlayHitEffect();
                    _evalText.SetEval(_eval);
                    Destroy(tno.gameObject);
                    if(_isInRealGame) _scoreMeter.AddCount(_eval, 1);
                }
            }
        }
        if(_isInRealGame) _scoreText.text = _scoreMeter.GetScoreString();
    }

    void CreateNoteObject(Score score)
    {
        print(score);
        foreach (var note in score.GetNotes())
        {
            GameObject go;
            go = Instantiate(notePrefab, noteCanvas.transform, false);
            go.transform.localPosition = new Vector3((note.key - KeyCode.A) * 7 - 80, 0, 0);
            go.GetComponent<NoteObject>().SetTime(note.time);
            go.name = note.key.ToString();
            go.transform.Find("Canvas").Find("Text").GetComponent<Text>().text = note.name;
        }
    }

    public class Note
    {
        public float time;
        public KeyCode key;
        public string name;
        public bool played = false;

        public Note(float time, KeyCode key, string name)
        {
            this.time = time;
            this.key = key;
            this.name = name;
        }
    }
    public class Score
    {
        private List<Note> notes = new List<Note>();

        public void Add(Note note)
        {
            //可优化为二分查找
            int i=0;
            while (i < notes.Count && notes[i++].time < note.time) ; 
            notes.Insert(i, note);
        }

        public List<Note> GetNotes()
        {
            return notes;
        }

        public int Length()
        {
            return notes.Count;
        }
        
        public int FindNext(KeyCode key)
        {
            for (int i = 0; i < notes.Count; i++) //可优化
            {
                if (notes[i].key == key && !notes[i].played) return i;
            }

            return -1;
        }

        public void SetPlayed(int i)
        {
            notes[i].played = true;
        }

        public Note GetNote(int i)
        {
            return notes[i];
        }
    }
}
    


