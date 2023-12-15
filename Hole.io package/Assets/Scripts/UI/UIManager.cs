using Holeio.essentials;
using System;
using System.Collections;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Holeio.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Ingame")]
        public GameObject InGame;
        public TextMeshProUGUI ScoreText;
        public TextMeshProUGUI timerText;
        public FixedJoystick PlayerJoystick;

        [Header("Game Start")]
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

        [Header("Lagged API")]
        public TextMeshProUGUI gameControlText;
        public Button callRewardAd;

        private void Awake()
        {
            SetAds();
        }

        private void SetAds()
        {
            LaggedAPIUnity.OnResumeGame += OnResumeGame;
            LaggedAPIUnity.onRewardAdSuccess += onRewardAdSuccess;
            LaggedAPIUnity.onRewardAdFailure += onRewardAdFailure;
            callRewardAd.interactable = false;
        }

        public void OnResumeGame()
        {
            gameControlText.text = "Ad completed - RESUME GAME";
            CheckRewardAd();
        }
        public void onRewardAdSuccess()
        {
            gameControlText.text = "Reward ad succesful, give user reward";
            callRewardAd.interactable = false;
            ScoreMultiplier();
        }

        private void ScoreMultiplier()
        {
            Currentscore *= 2;
            LastScore.text = "SCORE : " + Currentscore;
        }

        public void onRewardAdFailure()
        {
            gameControlText.text = "Reward ad failure";
            callRewardAd.interactable = false;
        }

        public void ShowAd()
        {
            LaggedAPIUnity.Instance.ShowAd();
            
        }

        public void CheckRewardAd()
        {
            gameControlText.text = "Checking reward ad...";
            LaggedAPIUnity.Instance.CheckRewardAd();
        }
        public void PlayRewardAd()
        {
            gameControlText.text = "Playing reward ad if available...";
            LaggedAPIUnity.Instance.PlayRewardAd();

        }

        private void Start()
        {
            Pause();
            initialized();
            currentTime = totalTime;
            currentLevel = SceneManager.GetActiveScene();
            
            UIService.Instance.SetUIManager(this);
            PlayerService.Instance.SetPlayerJoystick(PlayerJoystick);
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
            InGame.SetActive(false);
            Debug.Log("Game Completed");
            ShowAd();
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