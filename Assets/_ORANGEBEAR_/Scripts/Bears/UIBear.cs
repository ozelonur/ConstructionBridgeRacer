#region Header

// Developed by Onur ÖZEL

#endregion

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

        #endregion

        #region Buttons

        [Header("Buttons")] [SerializeField] private Button startButton;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button nextButton;

        #endregion

        #region Texts

        [Header("Texts")] [SerializeField] private TMP_Text scoreText;

        #endregion

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            startButton.onClick.AddListener(StartGame);
            retryButton.onClick.AddListener(NextLevel);
            nextButton.onClick.AddListener(NextLevel);

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
            }

            else
            {
                UnRegister(GameEvents.ActivatePanel, ActivatePanel);
                UnRegister(GameEvents.GetLevelNumber, GetLevelNumber);
                UnRegister(GameEvents.InitLevel, InitLevel);
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

        private void GetLevelNumber(object[] obj)
        {
            int levelNumber = (int)obj[0];
            scoreText.text = "Level " + levelNumber;
        }

        #endregion

        #region Private Methods

        private void NextLevel()
        {
            Roar(GameEvents.NextLevel);
        }

        private void StartGame()
        {
            Activate(gamePanel);
        }

        private void Activate(GameObject panel)
        {
            mainMenuPanel.SetActive(false);
            gamePanel.SetActive(false);
            gameFailPanel.SetActive(false);
            gameCompletePanel.SetActive(false);

            panel.SetActive(true);
        }

        #endregion
    }
}