#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Enums;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using _ORANGEBEAR_.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Bears
{
    public class MachineSelectorBear : Bear
    {
        #region Serialized Fields

        [Header("Machine Models")] [SerializeField]
        private GameObject[] machineModels;

        [SerializeField] private CollectorType collectorType;

        [Header("Select Machine")] [SerializeField]
        private MachineTypes machineType;

        [SerializeField] private Transform collectTransform;
        [SerializeField] private Vector3[] collectTransformPositions;

        #endregion

        #region Private Variables

        private Renderer _currentRenderer;

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.InitLevel, InitLevel);
                Register(CustomEvents.GetCar, GetCar);
            }

            else
            {
                UnRegister(GameEvents.InitLevel, InitLevel);
                UnRegister(CustomEvents.GetCar, GetCar);
            }
        }

        private void GetCar(object[] args)
        {
            if (collectorType != CollectorType.Player)
            {
                return;
            }

            machineType = (MachineTypes)args[0];
            EnableMachineModel();
        }

        private void InitLevel(object[] args)
        {
            if (collectorType == CollectorType.Player)
            {
                machineType = DataManager.Instance.GetActiveMachine();
            }

            EnableMachineModel();
        }

        #endregion

        #region Private Methods

        private void EnableMachineModel()
        {
            foreach (GameObject machineModel in machineModels)
            {
                machineModel.SetActive(false);
            }

            machineModels[(int)machineType].SetActive(true);
            _currentRenderer = machineModels[(int)machineType].GetComponent<Renderer>();
            collectTransform.localPosition = collectTransformPositions[(int)machineType];
        }

        #endregion

        #region Public Variables

        public void GiveColor(BrickType brickType)
        {
            Material[] materials = _currentRenderer.materials;
            materials[1] = MaterialManager.Instance.materials[(int)brickType];
            _currentRenderer.materials = materials;
        }

        #endregion
    }
}