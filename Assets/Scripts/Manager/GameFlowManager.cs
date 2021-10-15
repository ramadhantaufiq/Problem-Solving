using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class GameFlowManager : MonoBehaviour
    {
        #region Singleton

        private static GameFlowManager _instance;

        public static GameFlowManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameFlowManager>();

                    if (_instance == null)
                    {
                        Debug.LogError("GameFlowManager Not Found!");
                    }
                }
                return _instance;
            }
        }

        [Header("UI")]
        public UIGameOver gameOverUI;
        public UIPauseMenu pauseMenuUI;

        #endregion

        private bool _isGameOver;
        private bool _gamePaused;
        private GameObject _player;
    
        public bool IsGameOver => _isGameOver;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _isGameOver = false;
            _isGameOver = false;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.P) || Input.GetButtonUp("Fire2"))
            {
                if (!_gamePaused)
                {
                    if (!_player.GetComponent<ZoomMovement>().zoomActive)
                    {
                        PauseGame();
                    }
                }
                else
                {
                    ResumeGame();
                }
            }
        }

        public void PauseGame()
        {
            _gamePaused = true;
            Time.timeScale = 0f;
            
            _player.SetActive(false);
            pauseMenuUI.Show();
            
        }

        public void ResumeGame()
        {
            StartCoroutine(ResumeCountdown());
        }
    
        private IEnumerator ResumeCountdown()
        {
            pauseMenuUI.Hide();
            _player.SetActive(true);
            
            float pauseTimer = 3.0f;
            pauseMenuUI.UpdatePauseTimer(pauseTimer);
            pauseMenuUI.TogglePauseTimer(true);
            
            while (pauseTimer > 0.0f)
            {
                pauseTimer -= Time.unscaledDeltaTime;
                pauseMenuUI.UpdatePauseTimer(pauseTimer);
                yield return null;
            }
            
            pauseMenuUI.TogglePauseTimer(false);
            Time.timeScale = 1f;
            _gamePaused = false;
        }

        public void RestartGame()
        {
            _isGameOver = true;
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    
        public void BackToMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }

        public void GameOver()
        {
            _isGameOver = true;
            FindObjectOfType<EnemyManager>().enabled = false;
            FindObjectOfType<BoxManager>().enabled = false;
            if (ScoreManager.Instance.CurrentScore > ScoreManager.Instance.HighScore)
            {
                ScoreManager.Instance.SetHighScore();
                gameOverUI.newHighScore.text = $"You've Set a New High Score!\n{ScoreManager.Instance.HighScore}";
                gameOverUI.newHighScore.gameObject.SetActive(true);
            }
        
            gameOverUI.Show();
        }
    }
}
