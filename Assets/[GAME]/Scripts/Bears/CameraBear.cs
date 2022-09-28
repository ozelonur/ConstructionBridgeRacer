#region Header

// Developed by Onur ÖZEL

#endregion

using System.Collections.Generic;
using _GAME_.Scripts.GlobalVariables;
using _ORANGEBEAR_.EventSystem;
using Cinemachine;
using UnityEngine;
using CameraType = _GAME_.Scripts.Enums.CameraType;

namespace _GAME_.Scripts.Bears
{
    public class CameraBear : Bear
    {
        #region SerilizeFields

        [Header("Virtual Cameras")] [SerializeField]
        private CinemachineVirtualCamera mainVirtualCamera;

        [SerializeField] private CinemachineVirtualCamera finishVirtualCamera;

        #endregion

        #region Private Variables

        private Transform _followTarget;

        private Dictionary<CameraType, CinemachineVirtualCamera> _virtualCameras;

        private float _shakeTimer;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _virtualCameras = new Dictionary<CameraType, CinemachineVirtualCamera>
            {
                { CameraType.Main, mainVirtualCamera },
                { CameraType.Finish, finishVirtualCamera }
            };
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.GetCameraFollowTransform, GetFollowTarget);
                Register(CustomEvents.SwitchCamera, SwitchCamera);
            }

            else
            {
                UnRegister(CustomEvents.GetCameraFollowTransform, GetFollowTarget);
                UnRegister(CustomEvents.SwitchCamera, SwitchCamera);
            }
        }

        private void SwitchCamera(object[] args)
        {
            CameraType cameraType = (CameraType)args[0];

            SetCamera(cameraType);
        }

        private void GetFollowTarget(object[] obj)
        {
            _followTarget = (Transform)obj[0];

            SetTarget(mainVirtualCamera, _followTarget);
            SetTarget(finishVirtualCamera, _followTarget);

            SetCamera(CameraType.Main);
        }

        #endregion

        #region Private Methods

        private void SetCamera(CameraType cameraType)
        {
            foreach (KeyValuePair<CameraType, CinemachineVirtualCamera> cameraValue in _virtualCameras)
            {
                cameraValue.Value.Priority = cameraValue.Key == cameraType ? 11 : 0;
            }
        }

        private void SetTarget(CinemachineVirtualCamera virtualCamera, Transform target)
        {
            virtualCamera.m_Follow = target;
        }

        #endregion
    }
}