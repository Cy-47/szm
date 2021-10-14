using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    private Timer _timer;
    private GameObject _gameSystem;
    private float _time;
    private InGame _inGame;
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
    }

    public float GetTime()
    {
        return _time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector2(transform.localPosition.x,
            (_time - _timer.GetTime() + _inGame.offset / 1000f) * _inGame.flowRate);
        if(_time-_timer.GetTime()+_inGame.offset/1000f < -0.6) Destroy(gameObject);
    }
}
