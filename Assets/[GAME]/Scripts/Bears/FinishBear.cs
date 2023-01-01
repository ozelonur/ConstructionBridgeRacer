#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Bears.Abstracts;
using _GAME_.Scripts.Enums;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using _ORANGEBEAR_.EventSystem;
using DG.Tweening;
using UnityEngine;
using CameraType = _GAME_.Scripts.Enums.CameraType;

namespace _GAME_.Scripts.Bears
{
    public class FinishBear : Bear
    {
        #region Serialized Fields

        [SerializeField] private Transform[] multipliers;

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            Roar(CustomEvents.GetFinishLine, transform);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag($"Collector")) return;


            CollectBear collectBear = other.GetComponent<CollectBear>();
            Transform collectBearTransform = collectBear.transform;

            int count = collectBear.GetCount();

            if (collectBear.collectorType == CollectorType.Player)
            {
                Roar(CustomEvents.PlayerCanMove, false);
                Roar(CustomEvents.OnFinishLine);
                Roar(CustomEvents.SwitchCamera, CameraType.Finish);
            }

            else
            {
                Roar(CustomEvents.PlayerCanMove, false);
                Roar(CustomEvents.BotCanMove, false);
                Roar(GameEvents.OnGameComplete, false);
            }

            Vector3 targetPos = multipliers[GetMultiplierCount(collectBear.count)].position;

            targetPos.y = collectBearTransform.position.y;

            if (collectBear.collectorType == CollectorType.Player)
            {
                collectBearTransform.DOLocalRotate(Vector3.zero, .3f).SetEase(Ease.Linear)
                    .SetLink(collectBearTransform.gameObject);
                collectBearTransform.DOMove(targetPos, 1.5f * GetMultiplierCount(collectBear.count)).SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        DOVirtual.DelayedCall(.3f, () =>
                        {
                            Roar(GameEvents.OnGameComplete, true);
                            Roar(CustomEvents.ShowEarnedCurrency, count * 10);
                            DataManager.Instance.AddCurrency(count * 10);
                        });
                       
                        Roar(CustomEvents.BotCanMove, false);
                    })
                    .SetLink(collectBearTransform.gameObject);
            }
        }

        #endregion

        #region Private Methods

        private int GetMultiplierCount(int count)
        {
            int multiplierCount;

            if (count <= 0)
            {
                multiplierCount = 0;
            }

            else if (count >= multipliers.Length)
            {
                multiplierCount = multipliers.Length - 1;
            }

            else
            {
                multiplierCount = count - 1;
            }

            return multiplierCount;
        }

        #endregion
    }
}