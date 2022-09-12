#region Header
// Developed by Onur ÖZEL
#endregion

using System.Collections;
using _ORANGEBEAR_.EventSystem;

namespace _ORANGEBEAR_.Scripts.Bears
{
    public class LevelBear : Bear
    {
        #region Public Methods

        public void InitLevel()
        {
            StartCoroutine(Delay());
        }

        #endregion


        #region Private Methods

        private IEnumerator Delay()
        {
            yield return null;
            Roar(GameEvents.InitLevel);
        }

        #endregion
    }
}