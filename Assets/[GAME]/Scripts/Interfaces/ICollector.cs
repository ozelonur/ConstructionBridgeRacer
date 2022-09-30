#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Enums;
using UnityEngine;

namespace _GAME_.Scripts.Interfaces
{
    public interface ICollector
    {
        public CollectorType collectorType { get; set; }
        void Collect(params object[] args);

        int GetCount();

        void SubtractCount(Transform target);

        int GetAreaId();

        void SetAreaId();

        void SetTarget();
    }
}