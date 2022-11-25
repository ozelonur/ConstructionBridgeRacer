#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Bears;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.ScriptableObjects;
using _ORANGEBEAR_.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Managers
{
    public class DataManager : Bear
    {
        #region Singleton

        public static DataManager Instance;

        #endregion

        #region Serialized Fields

        [SerializeField] private int defaultCurrencyAmount;

        #endregion

        #region Public Variables

        public VehicleData[] vehicles;

        #endregion

        #region Private Variables

        private int _currentIndex;
        private int _activeMachineIndex;

        #endregion

        #region Properties

        public int Currency
        {
            get => PlayerPrefs.GetInt("Currency", 0);
            private set => PlayerPrefs.SetInt("Currency", value);
        }

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            LoadData();
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.InitLevel, InitLevel);
            }

            else
            {
                UnRegister(GameEvents.InitLevel, InitLevel);
            }
        }

        private void InitLevel(object[] args)
        {
            if (Currency <= defaultCurrencyAmount)
            {
                Currency = defaultCurrencyAmount;
            }

            Roar(CustomEvents.ShowCurrency, Currency);
        }

        #endregion

        #region Public Methods

        public void AddCurrency(int amount)
        {
            Currency += amount;
            Roar(CustomEvents.ShowCurrency, Currency);
        }

        public void SubtractCurrency(int amount)
        {
            Currency -= amount;
            Roar(CustomEvents.ShowCurrency, Currency);
        }

        public VehicleData GetVehicleData(int index)
        {
            _currentIndex = index;
            return vehicles[index];
        }

        public MachineTypes GetCurrentMachine()
        {
            return vehicles[_currentIndex].vehicleType;
        }

        public MachineTypes GetActiveMachine()
        {
            return vehicles[_activeMachineIndex].vehicleType;
        }
        
        public void SetActiveMachine(int index)
        {
            _activeMachineIndex = index;
        }
        
        public void SaveData()
        {
            foreach (var vehicleData in vehicles)
            {
                PlayerPrefs.SetInt(vehicleData.vehicleType.ToString(), vehicleData.unlocked ? 1 : 0);
            }
        }

        #endregion

        #region Private Methods
        private void LoadData()
        {
            foreach (var vehicleData in vehicles)
            {
                if (vehicleData.vehicleType == MachineTypes.Forklift)
                {
                    PlayerPrefs.SetInt(vehicleData.vehicleType.ToString(), vehicleData.unlocked ? 1 : 0);
                }
                vehicleData.unlocked = PlayerPrefs.GetInt(vehicleData.vehicleType.ToString(), 0) == 1;
            }
        }

        #endregion
    }
}