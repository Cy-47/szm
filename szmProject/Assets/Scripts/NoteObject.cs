using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script for a Note GameObject
/// </summary>
public class NoteObject : MonoBehaviour
{
    private Timer _timer;
    private GameObject _gameSystem;
    private float _time;
    private InGame _inGame;
    private Color _color;
    private Text _text;
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update

    public void SetTime(float time)
    {
        _time = time;
    }
    
    void Start()
    {
        _gameSystem = GameObject.Find("GameSystem");
        _timer = _gameSystem.GetComponent<Timer>();
        _inGame = _gameSystem.GetComponent<InGame>();
        _text = GetComponentInChildren<Text>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public float GetTime()
    {
        return _time;
    }

    // Update is called once per frame
    void Update()
    {
        if (_time - _timer.GetTime() + _inGame.offset / 1000f > 0)
        {
            transform.localPosition = new Vector2(transform.localPosition.x,
                (_time - _timer.GetTime() + _inGame.offset / 1000f) * _inGame.flowRate);
        }
        else
        {
            transform.localPosition = new Vector2(transform.localPosition.x,
                (_time - _timer.GetTime() + _inGame.offset / 1000f) * _inGame.flowRate / 4);
            _color = _spriteRenderer.color;
            _color.a = 1 + (_time - _timer.GetTime() + _inGame.offset / 1000f)/0.6f;
            _spriteRenderer.color = _color;
            _color = _text.color;
            _color.a = 1 + (_time - _timer.GetTime() + _inGame.offset / 1000f)/0.6f;
            _text.color = _color;
        }
        if(_time-_timer.GetTime()+_inGame.offset/1000f < -0.6) Destroy(gameObject);
    }
}
