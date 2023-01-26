using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private GameMenu _gameMenu;
    [SerializeField] private AudioClip _loseSound;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _coinSound;
    [SerializeField] private Timer _timer;
    public int Score { get => _score; set => _score = value; }

    

    private void Start()
    {
        
        Time.timeScale = 1f;
        _timer.onTimerUpdate += _gameMenu.UpdateTime;
        
    }

    public void UpdateScore(int value)
    {
        _score += value;
        if (_gameMenu)
        {
            _gameMenu.UpdateScoreValue(_score);
            _gameMenu.AudioSource.PlayOneShot(_coinSound);
        }
          
    }

    public void Finish()
    {
       
        _gameMenu.OpenFinishPanel();
        _gameMenu.AudioSource.PlayOneShot(_winSound);
        _timer.StopTimer(true);
    }
    
    public void Lose()
    {
        _gameMenu.AudioSource.PlayOneShot(_loseSound);
        _gameMenu.OpenDeathPanel();
    }
    
    public IEnumerator WaitToReload(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneLoader.instance.ReloadScene();
    }

}
