using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace ColorSwitch
{
    public class GameManager : MonoBehaviour
    {
        private int _score;
        public event System.Action OnLevelCompleted;        
        public bool isGameStarted;
        [SerializeField] private AudioClip _click;
        [SerializeField] private AudioClip _colorChange;
        [SerializeField] private AudioClip _wrong;
        [SerializeField] private AudioClip _scoreUp;
        private AudioSource _audioSource;
        private void Awake()
        {
            _audioSource=GetComponent<AudioSource>();
        }
        public void IncreaseScore(int increment) { _score += increment; PlayScoreUp(); }
        public void PlayClick()
        {
            _audioSource.PlayOneShot(_click);
        }
        public void PlayWrong()
        {
            _audioSource.PlayOneShot(_wrong);
        }
        public void PlayScoreUp()
        {
            _audioSource.PlayOneShot(_scoreUp);
        }
        public void PlayColorChange()
        {
            _audioSource.PlayOneShot(_colorChange);
        }
        public int GetScore() { return _score; }        
        public void StopGame() { Time.timeScale = 0; }
        public void ContinueGame() { Time.timeScale = 1; }
        public void GameOver() { OnLevelCompleted?.Invoke(); }
        public void ExitGame() { Application.Quit(); }

    }
}