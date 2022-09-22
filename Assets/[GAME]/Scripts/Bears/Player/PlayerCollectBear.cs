#region Header
// Developed by Onur ÖZEL
#endregion

using _GAME_.Scripts.Bears.Abstracts;

namespace _GAME_.Scripts.Bears.Player
{
    public class PlayerCollectBear : CollectBear
    {
        public override void Collect(params object[] args)
        {
            base.Collect(args);
            
            print("Player Collected");
        }
    }
}