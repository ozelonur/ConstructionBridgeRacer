#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.GlobalVariables;
using _ORANGEBEAR_.EventSystem;
using _ORANGEBEAR_.Scripts.Enums;
using UnityEngine;

namespace _ORANGEBEAR_.Scripts.Managers
{
    public class GameManager : Bear
    {
        [SerializeField] private ParticleSystem confetti;

        #region Public Variables

        public static GameManager Instance;
        
        public bool IsGameStarted;
        public bool IsGameEnded;
        public bool IsGamePaused;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.OnGameComplete, OnGameComplete);
                Register(CustomEvents.OnStepCompleted, OnStepCompleted);
                Register(GameEvents.OnGamePaused, OnGamePaused);
                Register(GameEvents.OnGameStart, OnGameStarted);
            }

            else
            {
                UnRegister(GameEvents.OnGameComplete, OnGameComplete);
                UnRegister(CustomEvents.OnStepCompleted, OnStepCompleted);
                UnRegister(GameEvents.OnGamePaused, OnGamePaused);
                UnRegister(GameEvents.OnGameStart, OnGameStarted);
            }
        }

        private void OnGameStarted(object[] args)
        {
            IsGameStarted = true;
            IsGamePaused = false;
            IsGameEnded = false;
        }

        private void OnGamePaused(object[] args)
        {
            IsGamePaused = (bool) args[0];
        }

        private void OnStepCompleted(object[] args)
        {
        }

        private void OnGameComplete(object[] obj)
        {
            IsGameEnded = true;
            
            bool status = (bool)obj[0];

            if (status)
            {
                confetti.Play();
            }

            Roar(GameEvents.ActivatePanel, status ? PanelsEnums.GameWin : PanelsEnums.GameOver);
        }

        #endregion
    }
}