#region Header
// Developed by Onur ÖZEL
#endregion

using _GAME_.Scripts.GlobalVariables;
using _ORANGEBEAR_.Scripts.Bears;
using UnityEngine;

namespace _GAME_.Scripts.Bears
{
    
    public class GameLevelBear : LevelBear
    {
        #region Serialized Fields

        [SerializeField] private Transform centrePoint; 

        #endregion

        #region Event Methods

        public override void InitLevel()
        {
            base.InitLevel();
            
            Roar(CustomEvents.SendCentrePoint, centrePoint);
        }

        #endregion
    }
}