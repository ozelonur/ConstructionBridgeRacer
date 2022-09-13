#region Header
// Developed by Onur ÖZEL
#endregion

using _GAME_.Scripts.GlobalVariables;
using _ORANGEBEAR_.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Bears.Player
{
    public class PlayerBear : Bear
    {
        #region Serialized Fields

        [Header("Camera Follow Transform")]
        [SerializeField] private Transform cameraFollowTransform;

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            Roar(CustomEvents.GetCameraFollowTransform, cameraFollowTransform);
        }

        #endregion
    }
}