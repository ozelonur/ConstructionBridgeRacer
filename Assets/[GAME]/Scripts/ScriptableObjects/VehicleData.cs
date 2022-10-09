#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Bears;

namespace _GAME_.Scripts.ScriptableObjects
{
    [System.Serializable]
    public class VehicleData
    {
        public MachineTypes vehicleType;
        public bool unlocked;
        public string vehicleName;
        public int vehiclePrice;
        public float vehicleSpeed;
    }
}