#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Bears.Abstracts;
using _GAME_.Scripts.Enums;
using UnityEngine;

namespace _GAME_.Scripts.Bears.Player
{
    public class PlayerCollectBear : CollectBear
    {
        #region Private Variables

        private PlayerMovementBear _playerMovementBear;

        #endregion
        #region MonoBehaviour Methods

        protected override void Awake()
        {
            base.Awake();
            collectorType = CollectorType.Player;
            _playerMovementBear = GetComponent<PlayerMovementBear>();
        }

        #endregion

        public override Quaternion GetRotation()
        {
            return _playerMovementBear.GetRotateTransform().rotation;
        }
    }
}