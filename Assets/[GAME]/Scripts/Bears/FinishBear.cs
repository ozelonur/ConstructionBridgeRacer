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
                Roar(CustomEvents.OnFinishLine);
                Roar(CustomEvents.PlayerCanMove, false);

                print(collectBear.count);

                Vector3 targetPos = multipliers[collectBear.count - 1].position;
                targetPos.y = collectBear.transform.position.y;
                collectBear.transform.DOJump(targetPos, 3 * collectBear.count, 1, 3f).SetSpeedBased()
                    .OnComplete(() =>
                    {
                        Transform rotateTransform = collectBear.transform.GetChild(0);

                        collectBear.transform.DOMoveZ(collectBear.transform.position.z + 5, .75f)
                            .SetEase(Ease.OutBack);
                        
                        rotateTransform.DOLocalRotate(new Vector3(0,135,0), .75f, RotateMode.LocalAxisAdd)
                            .OnComplete(() =>
                            {
                                Roar(GameEvents.OnGameComplete, true);
                            }).SetEase(Ease.OutBack);
                    })
                    .SetEase(Ease.Linear);
            }
        }

        #endregion
    }
}