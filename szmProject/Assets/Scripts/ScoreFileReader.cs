using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFileReader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public InGame.Score ReadFromTaxtAsset(TextAsset ta)
    {
        Dictionary<string, KeyCode> dict = new Dictionary<string, KeyCode>();
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if(!dict.ContainsKey(key.ToString())) dict.Add(key.ToString(), key);
        }
        var score = new InGame.Score();
        string[] ln;
        string type;
        float bpm = 60, startTime=0;
        foreach (var line in ta.text.Split('\n'))
        {
            if (line.Length == 0) continue;
            if (line.Length > 2)
            {
                if(line[0]=='/' && line[1]=='/') continue;
            }
            ln = line.Split(' ');
            if (ln[0] == "#")
            {
                if (ln[1] == "BPM") bpm = float.Parse(ln[2]);
                if (ln[1] == "TYPE") type = ln[2];
                if (ln[1] == "START") startTime = float.Parse(ln[2]);
                continue;
            }
            if (ln.Length == 2) score.Add(new InGame.Note(float.Parse(ln[0])*60/bpm+startTime, dict[ln[1]], ln[1]));
            else if(ln.Length == 3) score.Add(new InGame.Note(float.Parse(ln[0])*60/bpm+startTime, dict[ln[1]], ln[2]));
        }

        return score;
    }
}
