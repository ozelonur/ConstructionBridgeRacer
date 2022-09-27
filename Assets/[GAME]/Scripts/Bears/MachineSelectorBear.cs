#region Header
// Developed by Onur ÖZEL
#endregion

using _ORANGEBEAR_.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Bears
{
    public class MachineSelectorBear : Bear
    {
        #region Serialized Fields

        [Header("Machine Models")][SerializeField]
        private GameObject[] machineModels;

        
        
        [Header("Select Machine")][SerializeField]
        private MachineTypes machineType;

        [SerializeField] private Transform collectTransform;
        [SerializeField] private Vector3[] collectTransformPositions;

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
            EnableMachineModel();
        }

        #endregion

        #region Private Methods

        private void EnableMachineModel()
        {
            foreach (var machineModel in machineModels)
            {
                machineModel.SetActive(false);
            }
            
            machineModels[(int) machineType].SetActive(true);
            collectTransform.localPosition = collectTransformPositions[(int) machineType];
        }

        #endregion
    }
}