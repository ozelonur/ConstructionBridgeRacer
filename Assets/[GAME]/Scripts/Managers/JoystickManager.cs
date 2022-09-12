#region Header
// Developed by Onur ÖZEL
#endregion

using UnityEngine;

namespace _GAME_.Scripts.Managers
{
    public class JoystickManager : MonoBehaviour
    {
        #region Instance

        public static JoystickManager Instance;

        #endregion

        #region Public Variables

        public Joystick joystick;

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
    }
}