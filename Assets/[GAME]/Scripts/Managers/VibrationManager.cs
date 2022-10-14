#region Header

// Developed by Onur ÖZEL

#endregion

using _ORANGEBEAR_.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Managers
{
    public class VibrationManager : Bear
    {
        #region Singleton

        public static VibrationManager Instance;

        #endregion

        #region Public Variables

        public int IsVibrationOn
        {
            get => PlayerPrefs.GetInt("Vibration", 1);
            set => PlayerPrefs.SetInt("Vibration", value);
        }

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        #region Public Methods

        public void Vibrate()
        {
            if (IsVibrationOn != 1)
            {
                return;
            }

            Handheld.Vibrate();
        }

        #endregion
    }
}