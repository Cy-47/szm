using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    private InGame _inGame;
    private AudioClip _music;
    private InGame.Score _score;
    private ScoreFileReader _scoreFileReader;
    public Button returnButton, addOffsetButton, minusOffsetButton, addFlowRateButton, minusFlowRateButton;
    public TextMeshProUGUI offsetText, flowRateText;
    private int _offset;
    private int _flowRate;
    // Start is called before the first frame update
    void Start()
    {
        _inGame = GetComponent<InGame>();
        _scoreFileReader = GetComponent<ScoreFileReader>();
        _music = Resources.Load<AudioClip>("AdjustOffset");
        _score = _scoreFileReader.ReadFromTaxtAsset(Resources.Load("AdjustOffsetScore") as TextAsset);
        if(!PlayerPrefs.HasKey("offset")) PlayerPrefs.SetInt("offset", 0);
        if(!PlayerPrefs.HasKey("flowRate")) PlayerPrefs.SetInt("flowRate", 200);
        _offset = PlayerPrefs.GetInt("offset");
        _flowRate = PlayerPrefs.GetInt("flowRate");
        returnButton.onClick.AddListener(Return);
        addOffsetButton.onClick.AddListener(IncreaseOffset);
        minusOffsetButton.onClick.AddListener(DecreaseOffset);
        addFlowRateButton.onClick.AddListener(IncreaseFlowRate);
        minusFlowRateButton.onClick.AddListener(DecreaseFlowRate);
        _inGame.SetMusic(_music);
        _inGame.SetScore(_score);
        _inGame.StartNewGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Timer>().GetTime() > 2)
        {
            foreach (Transform child in GameObject.Find("NoteCanvas").transform)
            {
                if(child.name != "ZeroLine") Destroy(child.gameObject);
            }
            _inGame.StartNewGame();
        }
        if(Input.GetKeyDown(KeyCode.A)) DecreaseOffset();
        if(Input.GetKeyDown(KeyCode.D)) IncreaseOffset();
        if(Input.GetKeyDown(KeyCode.LeftArrow)) DecreaseOffset();
        if(Input.GetKeyDown(KeyCode.RightArrow)) IncreaseOffset();
        offsetText.SetText(_offset + "ms");
        flowRateText.SetText(_flowRate.ToString() + "%");
    }

    void IncreaseOffset()
    {
        _offset += 5;
        _inGame.offset = _offset;
        PlayerPrefs.SetInt("offset", _offset);
    }
    
    void DecreaseOffset()
    {
        _offset -= 5;
        _inGame.offset = _offset;
        PlayerPrefs.SetInt("offset", _offset);
    }
    
    void IncreaseFlowRate()
    {
        _flowRate += 10;
        _inGame.flowRate = _flowRate;
        PlayerPrefs.SetInt("flowRate", _flowRate);
    }
    
    void DecreaseFlowRate()
    {
        _flowRate -= 10;
        _inGame.flowRate = _flowRate;
        PlayerPrefs.SetInt("flowRate", _flowRate);
    }

    void Return()
    {
        PlayerPrefs.SetInt("offset", _offset);
        PlayerPrefs.SetInt("flowRate", _flowRate);
        SceneManager.LoadScene("MainMenu");
    }
}
