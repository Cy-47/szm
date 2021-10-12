using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    private Timer _timer;
    //public GameObject game;
    private float _time;
    // Start is called before the first frame update

    public void SetTime(float time)
    {
        this._time = time;
    }
    
    void Start()
    {
        _timer = GameObject.Find("GameSystem").GetComponent<Timer>();
    }

    public float GetTime()
    {
        return _time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, ( _time-_timer.GetTime()+PlayerPrefs.GetInt("offset")/1000f )*200);
        if(_time-_timer.GetTime() < -0.6) Destroy(gameObject);
    }
}
