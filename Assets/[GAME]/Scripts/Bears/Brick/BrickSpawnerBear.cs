#region Header

// Developed by Onur ÖZEL

#endregion

using System.Collections.Generic;
using _GAME_.Scripts.Enums;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using _ORANGEBEAR_.EventSystem;
using DG.Tweening;
using UnityEngine;

namespace _GAME_.Scripts.Bears.Brick
{
    public class BrickSpawnerBear : Bear
    {
        #region Serialized Variables

        [SerializeField] private List<Transform> spawnTransforms;
        [SerializeField] private BrickType[] brickTypes;
        [SerializeField] private Material[] brickMaterials;
        [SerializeField] private int spawnerID;

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            SpawnOnInit();
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.SpawnBrick, SpawnBrick);
            }

            else
            {
                UnRegister(CustomEvents.SpawnBrick, SpawnBrick);
            }
        }

        private void SpawnBrick(object[] args)
        {
            Vector3 spawnPoint = (Vector3)args[0];
            BrickBear brick = (BrickBear)args[1];

            if (spawnerID != brick.spawnerId)
            {
                return;
            }

            Spawn(spawnPoint, brick);
        }

        #endregion

        #region Private Methods

        private void Spawn(Vector3 spawnPoint, BrickBear oldBrick)
        {
            DOVirtual.DelayedCall(2, () =>
            {
                BrickBear brickBear = PoolManager.Instance.GetBrick();
                brickBear.InitBrick(spawnPoint, oldBrick.brickType, brickMaterials[(int)oldBrick.brickType], spawnerID);
            }).SetLink(gameObject);

        }

        private void SpawnOnInit()
        {
            foreach (BrickType t in brickTypes)
            {
                for (int j = 0; j < 7; j++)
                {
                    BrickBear brickBear = PoolManager.Instance.GetBrick();
                    int index = Random.Range(0, spawnTransforms.Count);
                    brickBear.InitBrick(spawnTransforms[index].position,t, brickMaterials[(int)t], spawnerID);
                    spawnTransforms.RemoveAt(index);
                }
            }
        }

        #endregion
    }
}