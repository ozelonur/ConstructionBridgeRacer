#region Header

// Developed by Onur ÖZEL

#endregion

using System.Collections.Generic;
using System.Linq;
using _GAME_.Scripts.Bears.Brick;
using _GAME_.Scripts.Enums;
using _GAME_.Scripts.GlobalVariables;
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
        private bool _isCleared;

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
                Register(GameEvents.OnGameComplete, OnGameCompleted);
                Register(CustomEvents.AddBrickToList, AddBrickEvent);
                Register(CustomEvents.DestroyAllBricks, DestroyAllBricks);
            }

            else
            {
                UnRegister(GameEvents.InitLevel, InitLevel);
                UnRegister(GameEvents.OnGameComplete, OnGameCompleted);
                UnRegister(CustomEvents.AddBrickToList, AddBrickEvent);
                UnRegister(CustomEvents.DestroyAllBricks, DestroyAllBricks);
            }
        }

        private void DestroyAllBricks(object[] args)
        {
            _availableBrickBears.Clear();
        }

        private void AddBrickEvent(object[] args)
        {
            BrickBear brick = (BrickBear) args[0];
            
            AddAvailableBrickBear(brick);
        }

        private void OnGameCompleted(object[] args)
        {
            _isCleared = false;
        }

        private void InitLevel(object[] args)
        {
            currentBrickId = 0;
            
        }

        #endregion


        #region Public Methods

        public void AddAvailableBrickBear(BrickBear brickBear)
        {
            if (!_isCleared)
            {
                _availableBrickBears.Clear();
                _isCleared = true;
            }
            
            _availableBrickBears.Add(brickBear);
        }

        public void SubtractAvailableBrickBear(BrickBear brickBear)
        {
            _availableBrickBears.Remove(brickBear);
        }

        public BrickBear GetClosestAvailableBrickBear(BrickType brickType, Vector3 position, int botAreaId)
        {
            List<BrickBear> brickBears = _availableBrickBears
                .Where(x => x.brickType == brickType && x.spawnerId == botAreaId).ToList();
            
            brickBears = brickBears.OrderBy(x => Vector3.Distance(x.transform.position, position)).ToList();

            BrickBear brickBear = brickBears.FirstOrDefault();


            return brickBear;
        }

        #endregion
    }
}