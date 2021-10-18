using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EvalText : MonoBehaviour
{
    private TextMeshProUGUI _evalText;
    // Start is called before the first frame update
    void Start()
    {
        _evalText = GetComponent<TextMeshProUGUI>();
        _evalText.alpha = 0;
    }

    public void SetEval(string eval)
    {
        _evalText.SetText(eval);
        if(eval.Equals("Perfect")) _evalText.color = Color.yellow;
        else if(eval.Equals("Good")) _evalText.color = Color.green;
        else _evalText.color = Color.gray;
        _evalText.alpha = 1;
    }
    
    // Update is called once per frame
    void Update()
    {
        _evalText.alpha -= 0.01f;
    }
}
