#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Bears.Abstracts;
using _GAME_.Scripts.Bears.Brick;
using _GAME_.Scripts.Enums;
using _GAME_.Scripts.Managers;

namespace _GAME_.Scripts.Bears.Player
{
    public class PlayerCollectBear : CollectBear
    {
        #region MonoBehaviour Methods

        protected override void Awake()
        {
            base.Awake();
            collectorType = CollectorType.Player;
        }

        #endregion

        public override void Collect(params object[] args)
        {
            base.Collect(args);
            BrickManager.Instance.SubtractAvailableBrickBear((BrickBear)args[1]);
        }
    }
}