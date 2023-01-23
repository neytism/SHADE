using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGoal : MonoBehaviour
{
    public GameObject gameOverScreen;
    public TextMeshProUGUI text;

    public AudioSource CongratsSound;
    public AudioSource BgSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            BgSound.Stop();
            CongratsSound.Play();
            text.text = "CONGRATS";
            PlayerSavedValues.Instance.MultiplyScore();
            StartCoroutine(ShowGameOverScreen()); 
        }
    }
    
    IEnumerator ShowGameOverScreen() {
        yield return new WaitForSeconds(1.5f);
        gameOverScreen.SetActive(true);
        
        Time.timeScale = 0f;
    }
}