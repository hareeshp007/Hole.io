using Holeio.essentials;
using System;
using System.Collections;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Holeio.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Ingame")]
        public GameObject InGame;
        public TextMeshProUGUI ScoreText;
        public TextMeshProUGUI timerText;

        [Header("Game Completed")]
        public GameObject GameStart;
        [Header("Game Completed")]
        public GameObject GameFinished;
        public TextMeshProUGUI LastScore;

        [Header("Levels")]
        public string[] GameLevels;
        [Header("Private")]
        [SerializeField]
        private int[] WinScoreByLevel;
        [SerializeField]
        private int Currentscore;
        [SerializeField]
        private Scene currentLevel;
        [SerializeField]
        private float totalTime = 60f;
        [SerializeField]
        private float currentTime;
        [SerializeField]
        private float StartTime;


        private void Start()
        {
            Pause();
            initialized();
            currentTime = totalTime;
            currentLevel = SceneManager.GetActiveScene();
            
            UIService.Instance.SetUIManager(this);
        }
        private void OnEnable()
        {
            
        }
        private IEnumerator OtherLevels()
        {
            GameFinished.SetActive(false);
            InGame.SetActive(true);
            GameStart.SetActive(false);
            yield return new WaitForSeconds(StartTime);
        }

        private void initialized()
        {
            GameFinished.SetActive(false);
            InGame.SetActive(false);
            GameStart.SetActive(true);
            
        }
        private void Update()
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                GameFinishedState();
                currentTime = 0;
                UpdateTimerDisplay();
            }
        }

        private void GameFinishedState()
        {
            Time.timeScale = 0;
            LastScore.text = "SCORE : " + Currentscore;
            GameFinished.SetActive(true);
            Debug.Log("Game Completed");
        }

        private void UpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = "Timer :" + string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        public void UpdateScore(int score)
        {
            Currentscore = score;
            ScoreText.text = "Score : " + Currentscore;
            checkWin();
        }

        private void checkWin()
        {
            int Level = currentLevel.buildIndex;
            Debug.Log("Current Level : "+Level + " WinScoreByLevel :" + WinScoreByLevel[Level]);
            if (Currentscore >= WinScoreByLevel[Level])//Temp measure
            {
                GameFinishedState();
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(currentLevel.buildIndex);
        }
        public void MainMenu()
        {
            SceneManager.LoadScene(GameLevels[0]);
        }
        public void LoadNextScene()
        {
            for (int i = 0; i < GameLevels.Length; i++)
            {
                if (GameLevels[i] == currentLevel.name)
                {
                    if (i + 1 < GameLevels.Length )
                    {
                        SceneManager.LoadScene(GameLevels[i + 1]);
                        
                    }
                    else
                    {
                        SceneManager.LoadScene(GameLevels[0]);
                    }
                    break;
                }
            }
        }

        public void Pause()
        {
            Time.timeScale = 0;
        }
        public void Play()
        {
            Time.timeScale = 1;
            GameStart.SetActive(false);
            InGame.SetActive(true);
        }
        public void Exit()
        {
            Application.Quit();
        }
    }
}