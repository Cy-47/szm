using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public float time = 0;

    public bool paused = true;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    public void PauseTimer()
    {
        paused = true;
    }

    public void StartTimer()
    {
        paused = false;
    }

    public void setTime(float time)
    {
        this.time = time;
    }

    public float GetTime()
    {
        //print(time);
        return time;
    }

    public void ResetTimer()
    {
        time = 0;
    }

}
