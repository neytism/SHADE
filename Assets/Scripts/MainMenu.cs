using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class MainMenu : MonoBehaviour
{
    private Button PlayGameButton;
    private Button QuitGameButton;
    private Button SettingsGameButton;
    private Button CloseSettingsGameButton;
    private TMP_Dropdown ResolutionDropdown;

    private GameObject SettingsPanel;
    
    public AudioSource bgSound;
    public AudioSource UIClickSound;
    private void Awake()
    {
        SettingsPanel = GameObject.Find("SettingsPanel").gameObject;
        ResolutionDropdown = GameObject.Find("ResolutionDropdown").gameObject.GetComponent<TMP_Dropdown>();
        
        PlayGameButton = GameObject.Find("Play").gameObject.GetComponent<Button>();
        QuitGameButton = GameObject.Find("Quit").gameObject.GetComponent<Button>();
        SettingsGameButton = GameObject.Find("Settings").gameObject.GetComponent<Button>();
        CloseSettingsGameButton = GameObject.Find("CloseSettings").gameObject.GetComponent<Button>();
        
        PlayGameButton.onClick.AddListener(PlayGame);
        QuitGameButton.onClick.AddListener(QuitGame);
        SettingsGameButton.onClick.AddListener(OpenSettingsGame);
        CloseSettingsGameButton.onClick.AddListener(CloseSettingsGame);
        ResolutionDropdown.onValueChanged.AddListener(ResolutionDropdownValueChanged);
        
        SettingsPanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        UIClickSound.Play();
        SceneManager.LoadScene("Level1");
        bgSound.Stop();
    }

    public void OpenSettingsGame()
    {
        UIClickSound.Play();
        SettingsPanel.SetActive(true);

    }

    public void CloseSettingsGame()
    {
        UIClickSound.Play();
        SettingsPanel.SetActive(false);
    }
    public void QuitGame()
    {
        UIClickSound.Play();
        Application.Quit();
    }

    public void ResolutionDropdownValueChanged(int value)
    {
        switch (value)
        {
            case 0:
                Screen.SetResolution(1920,1080,true);
                break;
            case 1:
                Screen.SetResolution(900,600,true);
                break;
            case 2:
                Screen.SetResolution(1024,1024,true);
                break;
        }
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
    }

    
}
