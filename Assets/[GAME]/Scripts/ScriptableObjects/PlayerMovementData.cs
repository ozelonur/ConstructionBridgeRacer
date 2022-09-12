#region Header
// Developed by Onur ÖZEL
#endregion

using UnityEngine;

namespace _GAME_.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/Player Movement Data", order = 1)]
    public class PlayerMovementData : ScriptableObject
    {
        public int movementSpeed;
        public int rotationSpeed;

    }
}