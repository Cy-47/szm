using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{
    private Score _score;
    private Timer _timer;
    private float _delay;
    public GameObject notePrefab;
    private bool _gameStarted = false;
    private MusicPlayer _musicPlayer;
    public EvalText evalText;
    private string _eval;
    public GameObject noteCanvas;
    public UnityEvent onHitNote;
    public int offset;
    public int flowRate;
    private bool _paused;
    private int _atNoteNum;
    private AudioClip _music;
    private Dictionary<KeyCode, float> _xAxisOf = new Dictionary<KeyCode, float>();
    private float _timeLength;

    public void StartNewGame()
    {
        _gameStarted = false;
        _atNoteNum = 0;
        _musicPlayer.Pause();
        _music = Resources.Load<AudioClip>(_score.MusicName);
        _musicPlayer.SetMusic(_music);
        offset = PlayerPrefs.GetInt("offset");
        flowRate = PlayerPrefs.GetInt("flowRate");
        if (_score.HasSetTimeLength()) _timeLength = _score.GetTimeLength();
        else _timeLength = _music.length;
        _timer.setTime(0);
        _timer.StartTimer();
        Resume();
    }

    private void Awake()
    {
        
        SetUpXAxisDict();
        _musicPlayer = GetComponent<MusicPlayer>();
        _timer = GetComponent<Timer>();
        onHitNote = new UnityEvent();
    }
    
    // Start is called before the first frame update

    public void SetScore(Score score)
    {
        _score = score;
    }
    
    // Update is called once per frame
    
    void Update()
    {
        if (_timer.GetTime() > _timeLength) SceneManager.LoadScene("Settle");
        if (_paused) return;
        CreateNoteObject(_score);
        if (_timer.GetTime() > 0 && !_gameStarted)
        {
            _gameStarted = true;
            _musicPlayer.PlayMusicFrom(_timer.GetTime());
        }
        if (Input.anyKeyDown)
        {
            foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    print(kcode);
                    Transform tno = noteCanvas.transform.Find(kcode.ToString());
                    if (tno != null)
                    {
                        NoteObject no = tno.gameObject.GetComponent<NoteObject>();
                        _delay = _timer.GetTime() - no.GetTime() - offset / 1000f;
                        Debug.Log(_delay);
                        if (Math.Abs(_delay) < 0.08) _eval = "Perfect";
                        else if (Math.Abs(_delay) < 0.2) _eval = "Good";
                        else if (Math.Abs(_delay) < 0.6) _eval = "Bad";
                        else _eval = null;
                        if (_eval != null)
                        {
                            onHitNote.Invoke();
                            _musicPlayer.PlayHitEffect();
                            evalText.SetEval(_eval);
                            Destroy(tno.gameObject);
                        }
                    }
                }
            }

        }
    }

    void CreateNoteObject(Score score)
    {
        while(_atNoteNum < score.NoteCount() && score.GetNote(_atNoteNum).time - _timer.GetTime() + offset / 1000f < 5)
            //Generate note object 5 seconds before its time
        {
            var note = score.GetNote(_atNoteNum++);
            GameObject go;
            go = Instantiate(notePrefab, noteCanvas.transform, false);
            go.transform.localPosition = new Vector3(_xAxisOf[note.keyCode]*noteCanvas.GetComponent<NoteCanvas>().GetZeroLineWidth()/2, 1000, 0);
            go.GetComponent<NoteObject>().SetTime(note.time);
            go.name = note.keyCode.ToString();
            go.transform.Find("Canvas").Find("Text").GetComponent<Text>().text = note.name;
        }
    }

    public string GetEval()
    {
        return _eval;
    }

    public void SetGameTimeScale(float timeScale)
    {
        _musicPlayer.SetPitch(timeScale);
        Time.timeScale = timeScale;
    }
    public void Pause()
    {
        SetGameTimeScale(0);
        _paused = true;
    }

    public void Resume()
    {
        SetGameTimeScale(1);
        _paused = false;
    }

    public Timer GetTimer()
    {
        return _timer;
    }

    public void Show4KLines()
    {
        
        Vector3 position, localScale;
        Quaternion rotation;
        localScale = new Vector3(1, 400, 1);
        rotation = Quaternion.Euler(0, 0, 0);
        
        position = new Vector3(_xAxisOf[KeyCode.D] * noteCanvas.GetComponent<NoteCanvas>().GetZeroLineWidth() / 2, 0, 0);
        noteCanvas.GetComponent<NoteCanvas>().GenLine(position, rotation, localScale);
        
        position = new Vector3(_xAxisOf[KeyCode.F] * noteCanvas.GetComponent<NoteCanvas>().GetZeroLineWidth() / 2, 0, 0);
        noteCanvas.GetComponent<NoteCanvas>().GenLine(position, rotation, localScale);
        
        position = new Vector3(_xAxisOf[KeyCode.J] * noteCanvas.GetComponent<NoteCanvas>().GetZeroLineWidth() / 2, 0, 0);
        noteCanvas.GetComponent<NoteCanvas>().GenLine(position, rotation, localScale);
        
        position = new Vector3(_xAxisOf[KeyCode.K] * noteCanvas.GetComponent<NoteCanvas>().GetZeroLineWidth() / 2, 0, 0);
        noteCanvas.GetComponent<NoteCanvas>().GenLine(position, rotation, localScale);
    }
    public class Note
    {
        public float time;
        public KeyCode keyCode;
        public string name;

        public Note(float time, KeyCode keyCode, string name)
        {
            this.time = time;
            this.keyCode = keyCode;
            this.name = name;
        }
    }

    void SetUpXAxisDict()
    {
        _xAxisOf.Add(KeyCode.A, -0.98f);
        _xAxisOf.Add(KeyCode.B, -0.15f);
        _xAxisOf.Add(KeyCode.C, -0.55f);
        _xAxisOf.Add(KeyCode.D, -0.78f);
        _xAxisOf.Add(KeyCode.E, -0.6f);
        _xAxisOf.Add(KeyCode.F, -0.38f);
        _xAxisOf.Add(KeyCode.G, -0.18f);
        _xAxisOf.Add(KeyCode.H, 0.02f);
        _xAxisOf.Add(KeyCode.I, 0.4f);
        _xAxisOf.Add(KeyCode.J, 0.22f);
        _xAxisOf.Add(KeyCode.K, 0.62f);
        _xAxisOf.Add(KeyCode.L, 0.72f);
        _xAxisOf.Add(KeyCode.M, 0.25f);
        _xAxisOf.Add(KeyCode.N, 0.05f);
        _xAxisOf.Add(KeyCode.O, 0.6f);
        _xAxisOf.Add(KeyCode.P, 0.8f);
        _xAxisOf.Add(KeyCode.Q, -1.0f);
        _xAxisOf.Add(KeyCode.R, -0.4f);
        _xAxisOf.Add(KeyCode.S, -0.88f);
        _xAxisOf.Add(KeyCode.T, -0.2f);
        _xAxisOf.Add(KeyCode.U, 0.2f);
        _xAxisOf.Add(KeyCode.V, -0.35f);
        _xAxisOf.Add(KeyCode.W, -0.8f);
        _xAxisOf.Add(KeyCode.X, -0.75f);
        _xAxisOf.Add(KeyCode.Y, 0.0f);
        _xAxisOf.Add(KeyCode.Z, -0.95f);
    }
    
    /// <summary>
    /// Stores the notes and other information of a song
    /// </summary>
    public class Score
    {
        private List<Note> notes = new List<Note>();
        private float _timeLength;
        private bool _hasSetTimeLength = false;
        public string MusicName;

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

        public int NoteCount()
        {
            return notes.Count;
        }

        public void SetTimeLength(float timeLength)
        {
            _timeLength = timeLength;
            _hasSetTimeLength = true;
        }

        public float GetTimeLength()
        {
            return _timeLength;
        }

        public bool HasSetTimeLength()
        {
            return _hasSetTimeLength;
        }

        public Note GetNote(int i)
        {
            return notes[i];
        }
    }
}
    


