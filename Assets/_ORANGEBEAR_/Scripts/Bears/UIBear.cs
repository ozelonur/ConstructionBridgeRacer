﻿#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.GlobalVariables;
using _ORANGEBEAR_.EventSystem;
using _ORANGEBEAR_.Scripts.Enums;
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

        #endregion

        #region Texts

        [Header("Texts")] [SerializeField] private TMP_Text scoreText;

        #endregion

        #endregion

        #region MonoBehaviour Methods

        protected virtual void Awake()
        {
            startButton.onClick.AddListener(StartGame);
            retryButton.onClick.AddListener(NextLevel);
            nextButton.onClick.AddListener(NextLevel);
            pauseButton.onClick.AddListener(PauseGame);
            restartButton.onClick.AddListener(NextLevel);
            resumeButton.onClick.AddListener(ResumeGame);
            homeButton.onClick.AddListener(Home);
            
            

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
            }
        }

        private void InitLevel(object[] args)
        {
            Activate(mainMenuPanel);
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
            Roar(GameEvents.NextLevel);
            Roar(CustomEvents.DestroyAllBricks);
        }

        protected void StartGame()
        {
            Activate(gamePanel);
            Roar(GameEvents.OnGameStart);
        }

        protected void Activate(GameObject panel)
        {
            mainMenuPanel.SetActive(false);
            gamePanel.SetActive(false);
            gameFailPanel.SetActive(false);
            gameCompletePanel.SetActive(false);
            gamePausePanel.SetActive(false);

            panel.SetActive(true);
        }
        
        protected void Deactivate(GameObject panel)
        {
            panel.SetActive(false);
        }

        #endregion
    }
}