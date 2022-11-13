#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Enums;
using _GAME_.Scripts.Interfaces;
using _GAME_.Scripts.Managers;
using _ORANGEBEAR_.EventSystem;
using DG.Tweening;
using UnityEngine;

namespace _GAME_.Scripts.Bears.Stair
{
    public class StairBear : Bear
    {
        #region Serialized Fields

        [SerializeField] private Transform stairModel;
        [SerializeField] private MeshRenderer stairRenderer;

        #endregion

        #region Private Variables

        private BrickType _stairType;
        private StairBuilderBear _stairBuilderBear;
        private ICollector _collector;
        private bool _isStairBuilt;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            stairModel.transform.localScale = Vector3.zero;
            stairModel.gameObject.SetActive(false);
            _stairBuilderBear = transform.parent.GetComponent<StairBuilderBear>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out ICollector collector)) return;

            if (Quaternion.Angle(transform.rotation, collector.GetRotation()) > 90)
            {
                if (collector.collectorType != CollectorType.Player)
                {
                    _stairBuilderBear.NavmeshObstacleStatus(false);
                }
                return;
            }

            _collector = collector;
            _stairBuilderBear.NavmeshObstacleStatus(true);

            if (!_isStairBuilt)
            {
                switch (collector.collectorType)
                {
                    case CollectorType.Player:
                        PlayerBuild();
                        break;
                    case CollectorType.Bot:
                        BotBuild();
                        break;
                    default:
                        Debug.LogError("Collector Type is not defined");
                        break;
                }
            }

            else
            {
                if (_stairType == collector.collectedBrickType)
                {
                    return;
                }

                switch (collector.collectorType)
                {
                    case CollectorType.Player:
                        PlayerReplace();
                        break;
                    case CollectorType.Bot:
                        BotReplace();
                        break;
                    default:
                        Debug.LogError("Collector Type is not defined");
                        break;
                }
            }
        }

        #endregion

        #region Private Methods

        private void BotBuild()
        {
            if (_collector.GetCount() <= 0)
            {
                _collector.SetTarget();
                _stairBuilderBear.SetStairUsing(false);
                return;
            }

            CommonBuild();
        }

        private void PlayerBuild()
        {
            if (_collector.GetCount() <= 0)
            {
                _stairBuilderBear.SetStairUsing(false);
                return;
            }

            _stairBuilderBear.SetStairUsing(true);
            AudioManager.Instance.PlayMakeStairSound();
            CommonBuild();
        }

        private void BotReplace()
        {
            _stairBuilderBear.ResetCenter(transform.localPosition);

            if (_collector.GetCount() <= 0)
            {
                _collector.SetTarget();
                _stairBuilderBear.SetStairUsing(false);
                return;
            }

            CommonReplace();
        }

        private void PlayerReplace()
        {
            _stairBuilderBear.ResetCenter(transform.localPosition);

            if (_collector.GetCount() <= 0)
            {
                _stairBuilderBear.SetStairUsing(false);
                return;
            }

            _stairBuilderBear.SetStairUsing(true);
            AudioManager.Instance.PlayMakeStairSound();
            CommonReplace();
        }

        private void CommonBuild()
        {
            _stairType = _collector.collectedBrickType;
            stairRenderer.material = MaterialManager.Instance.materials[(int)_stairType];

            _collector.SubtractCount(transform);
            stairModel.gameObject.SetActive(true);

            stairModel.DOScale(Vector3.one, .3f).SetEase(Ease.OutBack);
            _isStairBuilt = true;

            _stairBuilderBear.SetStep();
            _stairBuilderBear.CheckIsStairCompleted(this, _collector);
        }

        private void CommonReplace()
        {
            _stairType = _collector.collectedBrickType;
            stairRenderer.material = MaterialManager.Instance.materials[(int)_stairType];

            _collector.SubtractCount(transform);
            _stairBuilderBear.SetStep();
            _stairBuilderBear.CheckIsStairCompleted(this, _collector);
        }

        #endregion
    }
}