#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Bears.Abstracts;
using _GAME_.Scripts.Enums;
using _GAME_.Scripts.GlobalVariables;
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

            if (collectBear.collectorType == CollectorType.Player)
            {
                Roar(CustomEvents.PlayerCanMove, false);
                Roar(CustomEvents.SwitchCamera, CameraType.Finish);
            }

            else
            {
                Roar(CustomEvents.BotCanMove, false);
            }

            Vector3 targetPos = multipliers[GetMultiplierCount(collectBear.count)].position;
            Vector3 collectBearAngles = collectBearTransform.localEulerAngles;

            targetPos.y = collectBearTransform.position.y;


            collectBearTransform.DOLocalRotate(new Vector3(-20, 0, collectBearAngles.x), 3f)
                .SetEase(Ease.Linear)
                .SetLoops(2, LoopType.Yoyo).SetLink(collectBear.gameObject);
            collectBearTransform.DOJump(targetPos, GetJumpPower(collectBear.count), 1, 6f).SetSpeedBased()
                .OnComplete(() =>
                {
                    Transform rotateTransform = collectBear.transform.GetChild(0);

                    collectBearTransform.DOMoveZ(collectBearTransform.position.z + 5, .75f)
                        .SetEase(Ease.OutBack).SetLink(collectBear.gameObject);

                    rotateTransform.DOLocalRotate(new Vector3(0, 135, 0), .75f, RotateMode.LocalAxisAdd)
                        .OnComplete(() =>
                        {
                            switch (collectBear.collectorType)
                            {
                                case CollectorType.Player:
                                    Roar(GameEvents.OnGameComplete, true);
                                    Roar(CustomEvents.BotCanMove,false);
                                    break;
                                case CollectorType.Bot:
                                    Roar(GameEvents.OnGameComplete, false);
                                    Roar(CustomEvents.PlayerCanMove,false);
                                    break;
                                default:
                                    Debug.LogError("Collector Type is not defined");
                                    break;
                            }
                        }).SetEase(Ease.OutBack)
                        .SetLink(collectBear.gameObject);
                })
                .SetEase(Ease.Linear).SetLink(collectBear.gameObject);
        }

        #endregion

        #region Private Methods

        private float GetJumpPower(int count)
        {
            float jumpPower = 2.5f;

            if (count < 3)
            {
                jumpPower = 10f;
            }

            else
            {
                jumpPower *= count;
            }

            return jumpPower;
        }

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