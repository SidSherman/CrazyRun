using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] protected SceneLoader _sceneManager;
    
    // Buttons appears
    [SerializeField] protected Image _soundBttnImage;
    [SerializeField] protected Sprite _soundBttnActive;
    [SerializeField] protected Sprite _soundBttnInactive;
    
    // Sounds
    [SerializeField] protected AudioClip _clickSound;
    [SerializeField] protected AudioSource _audioSource;
    [SerializeField] protected AudioListener _listener;

    //Panels
    [SerializeField] protected GameObject _menuPanel;
    [SerializeField] protected GameObject _levelSelectorPanel;

    public AudioSource AudioSource { get => _audioSource; }

    public void SoundOnOff()
    {
        if(_listener.enabled == true)
        {
            _listener.enabled = false;
            _soundBttnImage.sprite = _soundBttnInactive;
        }
        else
        {
            _soundBttnImage.sprite = _soundBttnActive;
            _listener.enabled = true;
        }
    }
    
    public void PlayClickSound()
    {
        _audioSource.PlayOneShot(_clickSound);
    }

    public void OpenLevelSelector()
    {
        _levelSelectorPanel.SetActive(true);
        _menuPanel.SetActive(false);
    }
    
    public void CloseLevelSelector()
    {
        _levelSelectorPanel.SetActive(false);
        _menuPanel.SetActive(true);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
    
    
    
}
