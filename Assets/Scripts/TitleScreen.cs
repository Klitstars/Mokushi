using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] public GameObject creditsPanel;
    [SerializeField] public GameObject howToPanel;


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void CretitsPanel()
    {
        creditsPanel.SetActive(true);
    }

    public void HowToPanel()
    {
        howToPanel.SetActive(true);
    }
}
