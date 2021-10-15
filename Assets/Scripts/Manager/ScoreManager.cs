using System;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class ScoreManager : MonoBehaviour
    {
        #region Singleton
        private static ScoreManager _instance = null;
        public static ScoreManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ScoreManager>();

                    if (_instance == null)
                    {
                        Debug.LogError("ScoreManager not found!");
                    }
                }

                return _instance;
            }
        }
        #endregion

        public Text scoreText;
        public EnemyManager enemyManager;

        public bool enemyEnabled = false;

        public int boxProgressCheckpoint = 5;
        public int boxProgressIncrement = 1;
        public int enemyProgressCheckpoint = 15;
        public int enemyProgressIncrement = 1;

        private int _currentScore;
        public int CurrentScore => _currentScore;
        
        private int _highScore;
        public int HighScore => _highScore;

        private void Start()
        {
            _currentScore = 0;

            if (enemyEnabled)
            {
                enemyManager = EnemyManager.Instance;
            }
        }

        public void AddScore(int amount)
        {
            _currentScore += amount;
            scoreText.text = _currentScore.ToString();

            if (!BoxManager.Instance.progressEnabled)
            {
                return;
            }

            if (_currentScore % boxProgressCheckpoint == 0)
            {
                BoxManager.Instance.IncreaseMax(boxProgressIncrement);
            }

            if (!enemyEnabled)
            {
                return;
            }

            if (_currentScore % enemyProgressCheckpoint == 0)
            {
                EnemyManager.Instance.IncreaseMax(enemyProgressIncrement);
            }
        }

        public void SetHighScore()
        {
            _highScore = _currentScore;
        }
    }
}