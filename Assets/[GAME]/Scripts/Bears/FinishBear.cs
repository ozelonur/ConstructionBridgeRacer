#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Bears.Abstracts;
using _GAME_.Scripts.GlobalVariables;
using _ORANGEBEAR_.EventSystem;
using DG.Tweening;
using UnityEngine;
using CameraType = _GAME_.Scripts.Enums.CameraType;

namespace _GAME_.Scripts.Bears
{
    public class FinishBear : Bear
    {
        [SerializeField] private Transform[] multipliers;

        #region MonoBehaviour Methods

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Roar(CustomEvents.SwitchCamera, CameraType.Finish);

                CollectBear collectBear = other.GetComponent<CollectBear>();
                Transform collectBearTransform = collectBear.transform;

                Roar(CustomEvents.OnFinishLine);
                Roar(CustomEvents.PlayerCanMove, false);

                print(collectBear.count);

                Vector3 targetPos = multipliers[collectBear.count - 1].position;
                Vector3 collectBearAngles = collectBearTransform.localEulerAngles;

                targetPos.y = collectBearTransform.position.y;
                collectBearTransform.DOLocalRotate(new Vector3(-20, 0, collectBearAngles.x), 1f)
                    .SetEase(Ease.Linear)
                    .SetLoops(2, LoopType.Yoyo).SetLink(collectBear.gameObject);

                collectBearTransform.DOJump(targetPos, 2.5f * collectBear.count, 1, 3f).SetSpeedBased()
                    .OnComplete(() =>
                    {
                        Transform rotateTransform = collectBear.transform.GetChild(0);

                        collectBearTransform.DOMoveZ(collectBearTransform.position.z + 5, .75f)
                            .SetEase(Ease.OutBack).SetLink(collectBear.gameObject);

                        rotateTransform.DOLocalRotate(new Vector3(0, 135, 0), .75f, RotateMode.LocalAxisAdd)
                            .OnComplete(() => { Roar(GameEvents.OnGameComplete, true); }).SetEase(Ease.OutBack)
                            .SetLink(collectBear.gameObject);
                    })
                    .SetEase(Ease.Linear).SetLink(collectBear.gameObject);
            }
        }

        #endregion
    }
}