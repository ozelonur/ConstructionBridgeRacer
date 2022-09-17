#region Header
// Developed by Onur ÖZEL
#endregion

using System.Collections.Generic;
using _GAME_.Scripts.Bears.Brick;
using _GAME_.Scripts.Enums;
using _GAME_.Scripts.Interfaces;
using _ORANGEBEAR_.EventSystem;
using DG.Tweening;
using UnityEngine;

namespace _GAME_.Scripts.Bears.Abstracts
{
    public class CollectBear : Bear, ICollector
    {
        #region Serialized Fields

        [SerializeField] private BrickType allowedBrickType;
        [SerializeField] private Transform collectTransform;

        #endregion
        
        #region Protected Variables
        
        protected Vector3 _offsetPoint;
        protected List<BrickBear> _stackedBear;

        #endregion

        #region Public Variables

        public int count;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _stackedBear = new List<BrickBear>();
        }

        #endregion
        
        public virtual void Collect(params object[] args)
        {
            BrickType brickType = (BrickType)args[0];
            BrickBear brickBear = (BrickBear)args[1];
            
            Transform brickTransform = brickBear.transform;

            if (allowedBrickType != brickType)
            {
                return;
            }
            
            brickBear.isCollected = true;
            
            brickTransform.parent = collectTransform;
            _offsetPoint = Vector3.zero + Vector3.up * (.25f * count);
            
            brickTransform.DOLocalJump(_offsetPoint, 2,1,.5f).OnComplete(() =>
                {
                    _stackedBear.Add(brickBear);
                })
                .SetEase(Ease.OutBack);
            
            brickTransform.DOLocalRotate(new Vector3(0, 0, 0), .5f).SetLink(gameObject);

            count++;
        }
    }
}