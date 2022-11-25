#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using _GAME_.Scripts.ScriptableObjects;
using _ORANGEBEAR_.Scripts.Bears;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME_.Scripts.Bears
{
    public class GameUIBear : UIBear
    {
        #region Serialized Fields

        [SerializeField] private TMP_Text currencyText;
        [SerializeField] private TMP_Text earnedCoinText;

        [Header("Shop Panel")] [SerializeField]
        private GameObject garagePanel;

        [SerializeField] private Button garageButton;
        [SerializeField] private Button closeGarageButton;
        [SerializeField] private Button previousButton;
        [SerializeField] private Button nextCarButton;
        [SerializeField] private TMP_Text carNameText;
        [SerializeField] private TMP_Text carPriceText;
        [SerializeField] private Image carImage;
        [SerializeField] private Button buyButton;
        [SerializeField] private GameObject lockImage;
        [SerializeField] private TMP_Text buyButtonText;

        [Header("Step Complete Panel")] [SerializeField]
        private GameObject stepCompletePanel;

        [SerializeField] private Button nextStepButton;

        [Header("Ads")] [SerializeField] private Button getCoinButton;

        [Header("Settings Panel")] [SerializeField]
        private GameObject settingsPanel;

        [SerializeField] private Button settingsButton;
        [SerializeField] private Button closeSettingsButton;

        [Header("Fail Panel")] [SerializeField]
        private TMP_Text failLevelText;

        [Header("Info Text")] [SerializeField] private TMP_Text infoText;

        #endregion

        #region Private Variables

        private int _index;
        private int _earnedCurrency;

        #endregion

        #region MonoBehaviour Methods

        protected override void Awake()
        {
            base.Awake();

            infoText.transform.localScale = Vector3.zero;

            #region Garage

            garageButton.onClick.AddListener(OnGarageButtonClicked);
            closeGarageButton.onClick.AddListener(OnCloseGarageButtonClicked);
            nextCarButton.onClick.AddListener(OnNextButtonClicked);
            previousButton.onClick.AddListener(OnPreviousButtonClicked);
            buyButton.onClick.AddListener(OnClickBuy);
            garagePanel.SetActive(false);

            #endregion

            nextStepButton.onClick.AddListener(OnNextStepButtonClicked);
            getCoinButton.onClick.AddListener(OnGetCoinButtonClicked);
            stepCompletePanel.SetActive(false);

            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            closeSettingsButton.onClick.AddListener(OnCloseSettingsButtonClicked);
            settingsPanel.SetActive(false);
        }

        private void OnCloseSettingsButtonClicked()
        {
            settingsPanel.SetActive(false);
        }

        private void OnSettingsButtonClicked()
        {
            AnimationManager.Instance.PlaySettingsButtonAnimation();
        }

        private void OnGetCoinButtonClicked()
        {
            Advertisements.Instance.ShowRewardedVideo(VideoRewarded);
        }

        private void VideoRewarded(bool arg0)
        {
            DataManager.Instance.AddCurrency(50);
        }

        private void OnNextStepButtonClicked()
        {
            Advertisements.Instance.ShowInterstitial();
            Roar(CustomEvents.OnStepCompleted, false);
        }

        private void OnClickBuy()
        {
            if (DataManager.Instance.vehicles[_index].unlocked)
            {
                DataManager.Instance.SetActiveMachine(_index);
                Roar(CustomEvents.GetCar, DataManager.Instance.vehicles[_index].vehicleType);
                OnCloseGarageButtonClicked();
                StartGame();
                return;
            }

            if (DataManager.Instance.Currency < DataManager.Instance.vehicles[_index].vehiclePrice)
            {
                return;
            }

            DataManager.Instance.SubtractCurrency(DataManager.Instance.vehicles[_index].vehiclePrice);

            Unlock();
        }

        private void OnPreviousButtonClicked()
        {
            if (_index <= 0)
            {
                return;
            }

            _index--;

            previousButton.gameObject.SetActive(_index > 0);
            nextCarButton.gameObject.SetActive(_index < 9);

            GetVehicleData();
        }

        private void OnNextButtonClicked()
        {
            if (_index > 9)
            {
                return;
            }

            _index++;

            previousButton.gameObject.SetActive(_index > 0);
            nextCarButton.gameObject.SetActive(_index < 9);

            GetVehicleData();
        }

        private void OnCloseGarageButtonClicked()
        {
            garagePanel.SetActive(false);
            _index = 0;
        }

        private void OnGarageButtonClicked()
        {
            garagePanel.SetActive(true);

            switch (_index)
            {
                case 0:
                    previousButton.gameObject.SetActive(false);
                    nextCarButton.gameObject.SetActive(true);
                    break;
                case > 9:
                    previousButton.gameObject.SetActive(true);
                    nextCarButton.gameObject.SetActive(false);
                    break;
            }

            GetVehicleData();
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            base.CheckRoarings(status);
            if (status)
            {
                Register(CustomEvents.ShowCurrency, ShowCurrency);
                Register(CustomEvents.ShowEarnedCurrency, ShowEarnedCurrency);
                Register(CustomEvents.OnStepCompleted, OnStepCompleted);
                Register(CustomEvents.GiveInfoText, GiveInfoText);
            }

            else
            {
                UnRegister(CustomEvents.ShowCurrency, ShowCurrency);
                UnRegister(CustomEvents.ShowEarnedCurrency, ShowEarnedCurrency);
                UnRegister(CustomEvents.OnStepCompleted, OnStepCompleted);
                UnRegister(CustomEvents.GiveInfoText, GiveInfoText);
            }
        }

        private void GiveInfoText(object[] args)
        {
            bool status = (bool)args[0];
            string text = (string)args[1];

            infoText.text = text;

            infoText.color = status ? Color.green : Color.red;

            DOTween.Kill("InfoTextTween", true);

            infoText.transform.DOScale(Vector3.one, .3f)
                .OnComplete(() =>
                {
                    infoText.transform.DOScale(Vector3.zero, .3f)
                        .SetEase(Ease.InBack).SetDelay(.5f)
                        .SetLink(infoText.gameObject);
                })
                .SetEase(Ease.OutBack).SetLink(infoText.gameObject).SetId("InfoTextTween");
        }

        private void OnStepCompleted(object[] args)
        {
            bool status = (bool)args[0];

            stepCompletePanel.SetActive(status);
        }


        private void ShowEarnedCurrency(object[] args)
        {
            _earnedCurrency = (int)args[0];
            int currentCount = 0;

            DOTween.To(() => currentCount, count1 => currentCount = count1, _earnedCurrency, 1f).OnUpdate(() =>
            {
                earnedCoinText.text = "+" + currentCount;
                if (Time.frameCount % 10 == 0)
                {
                    AudioManager.Instance.PlayCoinCollectSound();
                }
            });
        }

        private void ShowCurrency(object[] args)
        {
            int currency = (int)args[0];
            currencyText.text = currency.ToString();
        }

        #endregion

        #region Private Methods

        private void GetVehicleData()
        {
            VehicleData vehicleData = DataManager.Instance.GetVehicleData(_index);

            carNameText.text = vehicleData.vehicleName;

            carPriceText.text = vehicleData.unlocked ? "PURCHASED" : vehicleData.vehiclePrice.ToString();

            carImage.sprite = vehicleData.vehicleSprite;
            lockImage.SetActive(!vehicleData.unlocked);

            buyButtonText.text = vehicleData.unlocked ? "PLAY" : "BUY";
        }

        private void Unlock()
        {
            DataManager.Instance.vehicles[_index].unlocked = true;
            GetVehicleData();

            DataManager.Instance.SaveData();
        }

        protected override void GetLevelNumber(object[] obj)
        {
            base.GetLevelNumber(obj);
            failLevelText.text = "LEVEL " + obj[0];
        }

        protected override void Claimed(bool arg0)
        {
            base.Claimed(arg0);
            DataManager.Instance.AddCurrency(_earnedCurrency * 5);
        }

        #endregion
    }
}