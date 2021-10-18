using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores Perfect, Good, Bad, and Miss counts
/// and calculates the score
/// </summary>
public class ScoreMeter

{
    private int _perfectCount = 0, _goodCount = 0, _badCount = 0, _totalCount;
    private const int MaxScore = 1000000;
    private const float GoodScoreRate = 0.6f;
    
    public int GetCount(string eval)
    {
        switch (eval)
        {
            case "Perfect":
                return _perfectCount;
            case "Good":
                return _goodCount;
            case "Bad":
                return _badCount;
            case "Total":
                return _totalCount;
        }
        return -1;
    }
    
    public void AddCount(string eval, int val)
    {
        switch (eval)
        {
            case "Perfect":
                _perfectCount += val;
                break;
            case "Good":
                _goodCount += val;
                break;
            case "Bad":
                _badCount += val;
                break;
            case "Total":
                _totalCount += val;
                break;
        }
    }
    
    public void SetCount(string eval, int val)
    {
        switch (eval)
        {
            case "Perfect":
                _perfectCount = val;
                break;
            case "Good":
                _goodCount = val;
                break;
            case "Bad":
                _badCount = val;
                break;
            case "Total":
                _totalCount = val;
                break;
        }
    }

    public void Clear()
    {
        _totalCount = _perfectCount = _goodCount = _badCount = 0;
    }
    
    public int GetScore()
    {
        if (_perfectCount == _totalCount) return MaxScore;
        if (_totalCount == 0) return 0;
        return (int) (MaxScore * (_perfectCount + _goodCount * GoodScoreRate) / _totalCount);
    }

    public string GetScoreString()
    {
        return GetScore().ToString().PadLeft(7, '0');
    }
}
