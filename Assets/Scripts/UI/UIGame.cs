using ColorSwitch;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace ColorSwitch
{
    public class UIGame : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private GameObject _openingPanel;
        [SerializeField] private GameObject _gameOverPanel;

        [SerializeField] private TMP_Text _scoreGameOverText;
        [SerializeField] private TMP_Text _highScore;
        [SerializeField] private GameObject _newHighScore;
        [SerializeField] private Button _relay;
        [SerializeField] private Button _exit;
        private GameManager _gameManager;
        private LevelManager _levelManager;

        private void OnDisable()
        {
            _gameManager.OnLevelCompleted -= OpenGameOverPanel;
        }
        private void Start()
        {
            if (Waypoint.TryGetWaypoint(out GameManager gameManager)) { _gameManager = gameManager; }
            if (Waypoint.TryGetWaypoint(out LevelManager levelManager)) { _levelManager = levelManager; }
            _gameManager.OnLevelCompleted += OpenGameOverPanel;
            _relay.onClick.AddListener(_levelManager.RestartLevel);
            _exit.onClick.AddListener(_gameManager.ExitGame);
            _gameManager.StopGame();
        }
        private void Update()
        {
            if (Input.anyKeyDown && !_gameManager.isGameStarted && _openingPanel.activeSelf)
            {
                _gameManager.ContinueGame();
                _openingPanel.SetActive(false);
                _gameManager.isGameStarted = true;
            }
            WriteScore();
        }
        private void OpenGameOverPanel()
        {
            _gameOverPanel.SetActive(true);
            _gameManager.StopGame();
            WriteHighScore();
        }
        private void WriteScore()
        {
            _scoreText.text = _gameManager.GetScore().ToString();
            _scoreGameOverText.text = _gameManager.GetScore().ToString();
        }
        private void WriteHighScore()
        {
            
            if ( _gameManager.GetScore() > PlayerPrefs.GetInt("HighScore"))
            {
                _newHighScore.SetActive(true);
                PlayerPrefs.SetInt("HighScore", _gameManager.GetScore());
                _highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
            }
            else
            {
                _highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
                _newHighScore.SetActive(false);
            }
        }
    }
}
