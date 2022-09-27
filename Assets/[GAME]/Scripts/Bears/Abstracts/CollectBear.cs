﻿#region Header

// Developed by Onur ÖZEL

#endregion

using System.Collections.Generic;
using System.Linq;
using _GAME_.Scripts.Bears.Brick;
using _GAME_.Scripts.Enums;
using _GAME_.Scripts.Interfaces;
using _ORANGEBEAR_.EventSystem;
using DG.Tweening;
using UnityEngine;

namespace _GAME_.Scripts.Bears.Abstracts
{
    public abstract class CollectBear : Bear, ICollector
    {
        #region Serialized Fields

        [SerializeField] private BrickType allowedBrickType;
        [SerializeField] private Transform collectTransform;

        #endregion

        #region Protected Variables

        protected Vector3 _offsetPoint;
        public List<BrickBear> stackedBear;

        #endregion

        #region Public Variables

        public int count;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            stackedBear = new List<BrickBear>();
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
            brickBear.collider.enabled = false;

            brickTransform.parent = collectTransform;
            _offsetPoint = Vector3.zero + Vector3.up * (.1f * count);

            brickTransform.DOLocalJump(_offsetPoint, 2, 1, .5f).SetSpeedBased().OnComplete(() => { stackedBear.Add(brickBear); })
                .SetEase(Ease.OutBack);

            brickTransform.DOScale(Vector3.one * .5f, .4f).SetLink(gameObject);

            brickTransform.DOLocalRotate(new Vector3(0, 0, 0), .2f).SetLink(gameObject);

            count++;
        }

        public int GetCount()
        {
            return count;
        }

        public void SubtractCount(Transform target)
        {
            stackedBear.Last().transform.parent = target;
            stackedBear.Last().transform.DOLocalJump(Vector3.zero, 5, 1, .5f).SetSpeedBased().SetEase(Ease.OutBack);
            stackedBear.Last().transform.DOScale(Vector3.zero, .4f).SetLink(gameObject);
            stackedBear.Remove(stackedBear.Last());
            count--;
            
        }
    }
}