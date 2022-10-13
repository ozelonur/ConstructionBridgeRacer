﻿#region Header

// Developed by Onur ÖZEL

#endregion

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
            }

            else
            {
                UnRegister(GameEvents.OnGameComplete, OnGameComplete);
            }
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