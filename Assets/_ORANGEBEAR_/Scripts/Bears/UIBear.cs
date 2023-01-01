#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.GlobalVariables;
using _ORANGEBEAR_.EventSystem;
using _ORANGEBEAR_.Scripts.Enums;
using _ORANGEBEAR_.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _ORANGEBEAR_.Scripts.Bears
{
    public class UIBear : Bear
    {
        #region SerializeFields

        #region Panels

        [Header("Panels")] [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameObject gameFailPanel;
        [SerializeField] private GameObject gameCompletePanel;
        [SerializeField] private GameObject gamePausePanel;

        #endregion

        #region Buttons

        [Header("Buttons")] [SerializeField] private Button startButton;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button homeButton;
        [SerializeField] private Button claimButton;

        #endregion

        #region Texts

        [Header("Texts")] [SerializeField] private TMP_Text scoreText;

        #endregion
        
        

        #endregion

        #region MonoBehaviour Methods

        protected virtual void Awake()
        {
            claimButton.gameObject.SetActive(true);
            startButton.onClick.AddListener(StartGame);
            retryButton.onClick.AddListener(Restart);
            nextButton.onClick.AddListener(NextLevel);
            pauseButton.onClick.AddListener(PauseGame);
            restartButton.onClick.AddListener(Restart);
            resumeButton.onClick.AddListener(ResumeGame);
            homeButton.onClick.AddListener(Home);
            claimButton.onClick.AddListener(Claim);
            
            

            Activate(mainMenuPanel);
        }

        private void ResumeGame()
        {
            Roar(GameEvents.OnGamePaused, false);
        }

        private void PauseGame()
        {
            Roar(GameEvents.OnGamePaused, true);
        }

        private void Home()
        {
            NextLevel();
            Activate(mainMenuPanel);
        }

        private void Restart()
        {
            GameManager.Instance.IsGameRestarted = true;
            NextLevel();
        }

        private void Claim()
        {
            Advertisements.Instance.ShowRewardedVideo(Claimed);
        }

        protected virtual void Claimed(bool arg0)
        {
            claimButton.gameObject.SetActive(false);
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.ActivatePanel, ActivatePanel);
                Register(GameEvents.GetLevelNumber, GetLevelNumber);
                Register(GameEvents.InitLevel, InitLevel);
                Register(GameEvents.OnGamePaused, OnGamePaused);
            }

            else
            {
                UnRegister(GameEvents.ActivatePanel, ActivatePanel);
                UnRegister(GameEvents.GetLevelNumber, GetLevelNumber);
                UnRegister(GameEvents.InitLevel, InitLevel);
                UnRegister(GameEvents.OnGamePaused, OnGamePaused);
            }
        }

        private void OnGamePaused(object[] args)
        {
            bool status = (bool) args[0];

            if (status)
            {
                Activate(gamePausePanel);
            }

            else
            {
                Deactivate(gamePausePanel);
                Activate(gamePanel);
            }
        }

        private void InitLevel(object[] args)
        {
            if (!GameManager.Instance.IsGameRestarted)
            {
                Activate(mainMenuPanel);
            }

            else
            {
                GameManager.Instance.IsGameRestarted = false;
                StartGame();
            }
        }

        private void ActivatePanel(object[] obj)
        {
            PanelsEnums panel = (PanelsEnums)obj[0];

            switch (panel)
            {
                case PanelsEnums.MainMenu:
                    Activate(mainMenuPanel);
                    break;
                case PanelsEnums.Game:
                    Activate(gamePanel);
                    break;
                case PanelsEnums.GameOver:
                    Activate(gameFailPanel);
                    break;
                case PanelsEnums.GameWin:
                    Activate(gameCompletePanel);
                    break;
                default:
                    Debug.Log("Panel not found");
                    break;
            }
        }

        protected virtual void GetLevelNumber(object[] obj)
        {
            int levelNumber = (int)obj[0];
            scoreText.text = "LEVEL " + levelNumber;
        }

        #endregion

        #region Private Methods

        private void NextLevel()
        {
            scoreText.transform.parent.gameObject.SetActive(true);
            Roar(CustomEvents.DestroyAllBricks);
            Roar(GameEvents.NextLevel);
        }

        protected void StartGame()
        {
            Activate(gamePanel);
            scoreText.transform.parent.gameObject.SetActive(false);
            Roar(GameEvents.OnGameStart);
        }

        private void Activate(GameObject panel)
        {
            mainMenuPanel.SetActive(false);
            gamePanel.SetActive(false);
            gameFailPanel.SetActive(false);
            gameCompletePanel.SetActive(false);
            gamePausePanel.SetActive(false);

            panel.SetActive(true);
        }

        private void Deactivate(GameObject panel)
        {
            panel.SetActive(false);
        }

        #endregion
    }
}