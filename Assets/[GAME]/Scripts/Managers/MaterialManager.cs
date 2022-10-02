#region Header

// Developed by Onur ÖZEL

#endregion

using _ORANGEBEAR_.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Managers
{
    public class MaterialManager : Bear
    {
        #region Singleton

        public static MaterialManager Instance;

        #endregion

        #region Public Variables

        public Material[] materials;

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