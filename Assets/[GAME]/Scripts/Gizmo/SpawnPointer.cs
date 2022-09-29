#region Header

// Developed by Onur ÖZEL

#endregion

using _ORANGEBEAR_.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Gizmo
{
    public class SpawnPointer : Bear
    {
        #region Serialized Fields

        [Header("Gizmo Configurations")] [SerializeField]
        private Color gizmoColor;

        [SerializeField] [Range(0f, 2f)] private float gizmoRadius;

        #endregion

        #region Editor Methods

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, gizmoRadius);
        }
#endif

        #endregion
    }
}