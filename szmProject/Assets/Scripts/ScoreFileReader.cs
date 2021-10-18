using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFileReader : MonoBehaviour
{

    public InGame.Score ReadFromTaxtAsset(TextAsset ta)
    {
        Dictionary<string, KeyCode> dict = new Dictionary<string, KeyCode>();
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if(!dict.ContainsKey(key.ToString())) dict.Add(key.ToString(), key);
        }
        var score = new InGame.Score();
        float bpm = 60, startTime=0;
        foreach (var line in ta.text.Split(new string[] {"\n", "\r\n", "\r"}, StringSplitOptions.RemoveEmptyEntries))
        {
            if (line.Length > 2)
            {
                if(line[0]=='/' && line[1]=='/') continue;
            }
            var ln = line.Split(' ');
            if (ln[0] == "#")
            {
                switch (ln[1])
                {
                    case "BPM":
                        bpm = float.Parse(ln[2]);
                        break;
                    case "START":
                        startTime = float.Parse(ln[2]);
                        break;
                    case "MUSIC":
                        score.MusicName = "";
                        for (int i = 2; i < ln.Length; i++)
                        {
                            score.MusicName += ln[i];
                            if (i < ln.Length - 1) score.MusicName += " ";
                        }
                        break;
                    case "TIME_LENGTH":
                        score.SetTimeLength(float.Parse(ln[2]));
                        break;
                }
                continue;
            }
            if (ln.Length == 2) score.Add(new InGame.Note(float.Parse(ln[0])*60/bpm+startTime, dict[ln[1]], ln[1]));
            else if(ln.Length == 3) score.Add(new InGame.Note(float.Parse(ln[0])*60/bpm+startTime, dict[ln[1]], ln[2]));
        }

        return score;
    }
}
