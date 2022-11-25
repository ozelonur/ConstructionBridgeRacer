#region Header
// Developed by Onur ÖZEL
#endregion

using System;
using _ORANGEBEAR_.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Managers
{
    public class AudioManager : Bear
    {
        #region Singleton

        public static AudioManager Instance;

        #endregion

        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private AudioSource brickCollectSound;

        [SerializeField] private AudioSource coinCollectSound;
        [SerializeField] private AudioSource makeStairSound;
        [SerializeField] private AudioSource mainMusic;

        #endregion

        #region Public Variables

        public int IsSoundOn
        {
            get => PlayerPrefs.GetInt("Sound", 1);
            set => PlayerPrefs.SetInt("Sound", value);
        }

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if (IsSoundOn == 1)
            {
                mainMusic.Play();
            }
        }

        #endregion

        #region Public Methods

        public void PlayBrickCollectSound()
        {
            if (IsSoundOn != 1)
            {
                return;
            }
            brickCollectSound.Play();
        }
        
        public void PlayCoinCollectSound()
        {
            if (IsSoundOn != 1)
            {
                return;
            }
            
            coinCollectSound.Play();
        }
        
        public void PlayMakeStairSound()
        {
            if (IsSoundOn != 1)
            {
                return;
            }
            
            makeStairSound.Play();
        }

        public void StopMainMusic()
        {
            mainMusic.Stop();
        }
        
        public void PlayMainMusic()
        {
            if (IsSoundOn != 1)
            {
                return;
            }
            
            mainMusic.Play();
        }

        #endregion
    }
}