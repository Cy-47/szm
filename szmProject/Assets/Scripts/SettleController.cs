using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettleController : MonoBehaviour
{
    public GameObject returnButtonObject, retryButtonObject;
    // Start is called before the first frame update
    void Start()
    {
        returnButtonObject.GetComponent<Button>().onClick.AddListener(Return);
        retryButtonObject.GetComponent<Button>().onClick.AddListener(Retry);
    }

    // Update is called once per frame
    private void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Retry()
    {
        SceneManager.LoadScene("Game");
    }
}
