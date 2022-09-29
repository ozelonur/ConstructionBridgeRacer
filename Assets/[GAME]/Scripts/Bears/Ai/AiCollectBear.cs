#region Header
// Developed by Onur ÖZEL
#endregion

using _GAME_.Scripts.Bears.Abstracts;

namespace _GAME_.Scripts.Bears.Ai
{
    public class AiCollectBear : CollectBear
    {
        #region Private Variables

        private AiMovementActor _aiMovementActor;

        #endregion

        #region MonoBehaviour Methods

        protected override void Awake()
        {
            base.Awake();
            _aiMovementActor = GetComponent<AiMovementActor>();
        }

        #endregion
        public override void Collect(params object[] args)
        {
            base.Collect(args);
            _aiMovementActor.ScanCollectable();
        }
    }
}