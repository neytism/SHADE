using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerSavedValues : MonoBehaviour
{
    public static PlayerSavedValues Instance;
    
    public float Score;
    public float Time;
    public float Coin;
    
    public TextMeshProUGUI PlayerName;
    public TextMeshProUGUI PlayerScore;
    public TextMeshProUGUI PlayerTime;
    public TextMeshProUGUI PlayerCoin;
    public TextMeshProUGUI PlayerAmmo;

    public TMP_InputField PlayerInput;

    public PlayerData _PlayerData;
    
    private void Awake()
    {
        Instance = this;
        
        _PlayerData = GameObject.FindObjectOfType<PlayerData>();
        
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            if (PlayerInput != null)
            {
                PlayerInput.text = PlayerPrefs.GetString("PlayerName");
            }
            PlayerName.text = PlayerPrefs.GetString("PlayerName");
        }
        if (PlayerPrefs.HasKey("PlayerScore"))
        {
            PlayerScore.text = PlayerPrefs.GetInt("PlayerScore").ToString("000000");
            Score = PlayerPrefs.GetInt("PlayerScore");
        }
    }

    public void OnValueChangeInputFieldTMP()
    {
        PlayerName.text = PlayerInput.text;
        PlayerPrefs.SetString("PlayerName",PlayerInput.text);
    }

    public void AddScore(float value)
    {
        Score += value;
        PlayerScore.text = Score.ToString("000000");
        PlayerPrefs.SetFloat("PlayerScore", Score);
        _PlayerData.AddScore(value);
       
    }
    
    public void AddTime(float value)
    {
        Time = value;
        PlayerTime.text = Time.ToString();
        PlayerPrefs.SetFloat("PlayerTime", Time);
        _PlayerData.AddTime(value);
    }
    
    public void AddCoin(float value)
    {
        Coin += value;
        PlayerCoin.text = Coin.ToString();
        PlayerPrefs.SetFloat("PlayerCoin", Coin);
        _PlayerData.AddCoin(value);
    }

    public void MultiplyScore()
    {
        _PlayerData.AddScore(-Score);
        Score = (Score + (Coin * 10)) * (100 - ((Time+1)*0.5f)*0.5f);
        PlayerScore.text = Score.ToString("000000");
        PlayerPrefs.SetFloat("PlayerScore", Score);
        _PlayerData.AddScore(Score);
        _PlayerData.updatePlayerScore();
        
    }
    
    public void CalculateScoreNotFinished()
    {
        _PlayerData.AddScore(-Score);
        Score = Score + (Coin * 10);
        PlayerScore.text = Score.ToString("000000");
        PlayerPrefs.SetFloat("PlayerScore", Score);
        _PlayerData.AddScore(Score);
        _PlayerData.updatePlayerScore();
        
    }
    
}