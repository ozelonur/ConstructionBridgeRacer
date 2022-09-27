#region Header
// Developed by Onur ÖZEL
#endregion

using UnityEngine;

namespace _GAME_.Scripts.Interfaces
{
    public interface ICollector
    {
        void Collect(params  object[] args);
        
        int GetCount();
        
        void SubtractCount(Transform target);
    }
}