using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EvalText : MonoBehaviour
{
    private TextMeshProUGUI evalText;
    // Start is called before the first frame update
    void Start()
    {
        evalText = GetComponent<TextMeshProUGUI>();
        evalText.alpha = 0;
    }

    public void SetEval(string eval)
    {
        evalText.SetText(eval);
        if(eval.Equals("Perfect")) evalText.color = Color.yellow;
        else if(eval.Equals("Good")) evalText.color = Color.green;
        else evalText.color = Color.gray;
        evalText.alpha = 1;
    }
    
    // Update is called once per frame
    void Update()
    {
        evalText.alpha -= 0.01f;
    }
}
