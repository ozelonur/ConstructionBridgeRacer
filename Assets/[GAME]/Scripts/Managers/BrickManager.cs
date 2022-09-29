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
                Register(CustomEvents.SignAvailableBricks, SignAvailableBricks);
            }

            else
            {
                UnRegister(CustomEvents.SignAvailableBricks, SignAvailableBricks);
            }
        }

        private void SignAvailableBricks(object[] args)
        {
            BrickBear brickBear = (BrickBear)args[0];
            AddAvailableBrickBear(brickBear);
        }

        #endregion

        #region Private Methods

        private void AddAvailableBrickBear(BrickBear brickBear)
        {
            _availableBrickBears.Add(brickBear);
        }

        #endregion


        #region Public Methods

        public void SubtractAvailableBrickBear(BrickBear brickBear)
        {
            _availableBrickBears.Remove(brickBear);
        }

        public BrickBear GetClosestAvailableBrickBear(BrickType brickType, Vector3 position)
        {
            List<BrickBear> brickBears = _availableBrickBears.Where(x => x.brickType == brickType).ToList();
            BrickBear brickBear = null;
            float distance = 10000f;

            for (int i = 0; i < brickBears.Count; i++)
            {
                if (!(Vector3.Distance(position, brickBears[i].transform.position) < distance)) continue;

                distance = Vector3.Distance(position, brickBears[i].transform.position);
                brickBear = brickBears[i];
            }

            _availableBrickBears.Remove(brickBear);
            return brickBear;
        }

        #endregion
    }
}