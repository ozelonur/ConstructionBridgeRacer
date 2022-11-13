#region Header

// Developed by Onur ÖZEL

#endregion

using _ORANGEBEAR_.EventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME_.Scripts.Managers
{
    public class SettingsManager : Bear
    {
        #region Singleton

        public static SettingsManager Instance;

        #endregion

        #region Serialized Fields

        [Header("Vibration Settings")] [SerializeField]
        private Button vibrationButton;

        [SerializeField] private Sprite vibrationOnSprite;
        [SerializeField] private Sprite vibrationOffSprite;
        [Header("Sound Settings")]
        [SerializeField] private Button soundButton;

        [SerializeField] private Sprite soundOnSprite;
        [SerializeField] private Sprite soundOffSprite;
        
        [Header("Quit")]
        [SerializeField] private Button quitButton;
        
        [Header("Info")]
        [SerializeField] private Button infoButton;

        [SerializeField] private string URL;

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

            vibrationButton.onClick.AddListener(VibrationButtonClicked);
            soundButton.onClick.AddListener(SoundButtonClicked);
            quitButton.onClick.AddListener(QuitGame);
            infoButton.onClick.AddListener(InfoButtonClicked);
        }

        private void InfoButtonClicked()
        {
            Application.OpenURL(URL);
        }

        private void QuitGame()
        {
            Application.Quit();
        }

        private void SoundButtonClicked()
        {
            if (AudioManager.Instance.IsSoundOn == 1)
            {
                AudioManager.Instance.IsSoundOn = 0;
                AudioManager.Instance.StopMainMusic();
                soundButton.image.sprite = soundOffSprite;
            }
            else
            {
                AudioManager.Instance.IsSoundOn = 1;
                AudioManager.Instance.PlayMainMusic();
                soundButton.image.sprite = soundOnSprite;
            }
        }

        private void Start()
        {
            CheckVibration();
            CheckSound();
        }

        #endregion

        #region Private Methods

        private void VibrationButtonClicked()
        {
            if (VibrationManager.Instance.IsVibrationOn == 1)
            {
                VibrationManager.Instance.IsVibrationOn = 0;
                vibrationButton.image.sprite = vibrationOffSprite;
            }
            else
            {
                VibrationManager.Instance.IsVibrationOn = 1;
                vibrationButton.image.sprite = vibrationOnSprite;
            }
        }

        private void CheckVibration()
        {
            vibrationButton.image.sprite =
                VibrationManager.Instance.IsVibrationOn == 1 ? vibrationOnSprite : vibrationOffSprite;
        }
        
        private void CheckSound()
        {
            soundButton.image.sprite =
                AudioManager.Instance.IsSoundOn == 1 ? soundOnSprite : soundOffSprite;
        }

        #endregion
    }
}