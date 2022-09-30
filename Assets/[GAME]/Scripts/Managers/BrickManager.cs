#region Header

// Developed by Onur ÖZEL

#endregion

using System.Collections.Generic;
using System.Linq;
using _GAME_.Scripts.Bears.Brick;
using _GAME_.Scripts.Enums;
using _ORANGEBEAR_.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Managers
{
    public class BrickManager : Bear
    {
        #region Singleton

        public static BrickManager Instance;

        #endregion

        #region Private Variables

        private List<BrickBear> _availableBrickBears = new List<BrickBear>();

        #endregion

        #region Public Variables

        public int currentBrickId;

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
            currentBrickId = 0;
        }

        #endregion

        #region Private Methods

        #endregion


        #region Public Methods

        public void AddAvailableBrickBear(BrickBear brickBear)
        {
            _availableBrickBears.Add(brickBear);
        }

        public void SubtractAvailableBrickBear(BrickBear brickBear)
        {
            _availableBrickBears.Remove(brickBear);
        }

        public BrickBear GetClosestAvailableBrickBear(BrickType brickType, Vector3 position, int botAreaId)
        {
            List<BrickBear> brickBears = _availableBrickBears
                .Where(x => x.brickType == brickType && x.SpawnerId == botAreaId).ToList();
            
            brickBears = brickBears.OrderBy(x => Vector3.Distance(x.transform.position, position)).ToList();

            BrickBear brickBear = brickBears.FirstOrDefault();

            _availableBrickBears.Remove(brickBear);

            return brickBear;
        }

        #endregion
    }
}