#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using _GAME_.Scripts.ScriptableObjects;
using _ORANGEBEAR_.Scripts.Bears;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME_.Scripts.Bears
{
    public class GameUIBear : UIBear
    {
        #region Serialized Fields

        [SerializeField] private TMP_Text currencyText;

        [Header("Garage Panel")] [SerializeField]
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

        #endregion

        #region Private Variables

        private int index;

        #endregion

        #region MonoBehaviour Methods

        protected override void Awake()
        {
            base.Awake();
            garageButton.onClick.AddListener(OnGarageButtonClicked);
            closeGarageButton.onClick.AddListener(OnCloseGarageButtonClicked);
            nextCarButton.onClick.AddListener(OnNextButtonClicked);
            previousButton.onClick.AddListener(OnPreviousButtonClicked);
            buyButton.onClick.AddListener(OnClickBuy);
            garagePanel.SetActive(false);
        }

        private void OnClickBuy()
        {
            if (DataManager.Instance.Vehicles[index].unlocked)
            {
                Roar(CustomEvents.GetCar, DataManager.Instance.Vehicles[index].vehicleType);
                OnCloseGarageButtonClicked();
                StartGame();
                return;
            }

            if (DataManager.Instance.Currency < DataManager.Instance.Vehicles[index].vehiclePrice)
            {
                print("Not Enough Money");
                return;
            }

            DataManager.Instance.SubtractCurrency(DataManager.Instance.Vehicles[index].vehiclePrice);
            print("Purchased");

            Unlock();
        }

        private void OnPreviousButtonClicked()
        {
            if (index <= 0)
            {
                return;
            }

            index--;

            previousButton.gameObject.SetActive(index > 0);
            nextCarButton.gameObject.SetActive(index < 9);

            GetVehicleData();
        }

        private void OnNextButtonClicked()
        {
            if (index > 9)
            {
                return;
            }

            index++;

            previousButton.gameObject.SetActive(index > 0);
            nextCarButton.gameObject.SetActive(index < 9);

            GetVehicleData();
        }

        private void OnCloseGarageButtonClicked()
        {
            garagePanel.SetActive(false);
            index = 0;
        }

        private void OnGarageButtonClicked()
        {
            garagePanel.SetActive(true);

            switch (index)
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
            if (status)
            {
                Register(CustomEvents.ShowCurrency, ShowCurrency);
            }

            else
            {
                UnRegister(CustomEvents.ShowCurrency, ShowCurrency);
            }
        }

        private void ShowCurrency(object[] args)
        {
            currencyText.text = "<sprite=0>" + args[0];
        }

        #endregion

        #region Private Methods

        private void GetVehicleData()
        {
            VehicleData vehicleData = DataManager.Instance.GetVehicleData(index);

            carNameText.text = vehicleData.vehicleName;
            carPriceText.text = vehicleData.vehiclePrice.ToString();
            carImage.sprite = vehicleData.vehicleSprite;
            carPriceText.gameObject.SetActive(!vehicleData.unlocked);
            lockImage.SetActive(!vehicleData.unlocked);

            buyButtonText.text = vehicleData.unlocked ? "PLAY" : "BUY";
        }

        private void Unlock()
        {
            DataManager.Instance.Vehicles[index].unlocked = true;
            GetVehicleData();

            DataManager.Instance.SaveData();
        }

        #endregion
    }
}