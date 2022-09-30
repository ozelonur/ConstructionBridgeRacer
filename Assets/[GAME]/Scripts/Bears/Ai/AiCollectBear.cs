﻿#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Bears.Abstracts;
using _GAME_.Scripts.Enums;

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
            collectorType = CollectorType.Bot;
            _aiMovementActor = GetComponent<AiMovementActor>();
        }

        #endregion

        public override void Collect(params object[] args)
        {
            base.Collect(args);
            _aiMovementActor.ScanCollectable();
        }

        #region Public Methods

        public override int GetAreaId()
        {
            base.GetAreaId();
            return _aiMovementActor.AreaId;
        }

        public override void SetAreaId()
        {
            _aiMovementActor.AreaId++;
        }

        public override void SetTarget()
        {
            _aiMovementActor.ScanCollectable();
        }

        #endregion
    }
}