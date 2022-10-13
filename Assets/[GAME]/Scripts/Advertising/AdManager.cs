#region Header

// Developed by Onur ÖZEL

#endregion

using _ORANGEBEAR_.EventSystem;

namespace _GAME_.Scripts.Advertising
{
    public class AdManager : Bear
    {
        #region Singleton

        public static AdManager Instance;

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

        private void Start()
        {
            Advertisements.Instance.SetUserConsent(false);
            Advertisements.Instance.Initialize();
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.OnGameComplete, OnGameCompleted);
            }

            else
            {
                UnRegister(GameEvents.OnGameComplete, OnGameCompleted);
            }
        }

        private void OnGameCompleted(object[] args)
        {
            Advertisements.Instance.ShowInterstitial();
        }

        #endregion
    }
}