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

        public bool IsOnStep { get; set; }

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
            }

            else
            {
                UnRegister(GameEvents.OnGameComplete, OnGameComplete);
                UnRegister(CustomEvents.OnStepCompleted, OnStepCompleted);
            }
        }

        private void OnStepCompleted(object[] args)
        {
            IsOnStep = (bool)args[0];
        }

        private void OnGameComplete(object[] obj)
        {
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