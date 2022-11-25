#region Header

// Developed by Onur ÖZEL

#endregion

using System.Collections.Generic;
using _GAME_.Scripts.Bears.Stair;
using _ORANGEBEAR_.EventSystem;

namespace _GAME_.Scripts.Managers
{
    public class StairManager : Bear
    {
        #region Singleton

        public static StairManager Instance;

        #endregion

        #region Private Variables

        private List<StairBuilderBear> _stairBuilderBears;
        private bool _isCleared;

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

            _stairBuilderBears = new List<StairBuilderBear>();
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.OnGameComplete, OnGameComplete);
            }

            else
            {
                UnRegister(GameEvents.OnGameComplete, OnGameComplete);
            }
        }

        private void OnGameComplete(object[] args)
        {
            _isCleared = false;
        }

        #endregion

        #region Public Methods

        public void AddStair(StairBuilderBear stairBuilderBear)
        {
            if (!_isCleared)
            {
                _stairBuilderBears.Clear();
                _isCleared = true;
            }
            _stairBuilderBears.Add(stairBuilderBear);
        }

        public StairBuilderBear GetStair(int stairId)
        {
            StairBuilderBear stairBuilderBear = _stairBuilderBears.Find(x => x.stairID == stairId && !x.IsStairUsing());

            return stairBuilderBear;
        }

        public int GetStairCount()
        {
            return _stairBuilderBears.Count;
        }

        #endregion
    }
}