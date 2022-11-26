#region Header

// Developed by Onur ÖZEL

#endregion

using System.Collections;
using System.Collections.Generic;
using _GAME_.Scripts.Enums;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using _ORANGEBEAR_.EventSystem;
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
            Transform spawnPoint = (Transform)args[0];
            BrickBear brick = (BrickBear)args[1];

            if (spawnerID != brick.spawnerId)
            {
                return;
            }

            StartCoroutine(Spawn(spawnPoint.position, brick));
        }

        #endregion

        #region Private Methods

        private IEnumerator Spawn(Vector3 spawnPoint, BrickBear oldBrick)
        {
            yield return new WaitForSeconds(2f);
            BrickBear brickBear = PoolManager.Instance.GetBrick();
            brickBear.InitBrick(oldBrick.brickType, brickMaterials[(int)oldBrick.brickType], spawnerID);
            brickBear.SetPosition(spawnPoint);
        }

        private void SpawnOnInit()
        {
            foreach (BrickType t in brickTypes)
            {
                for (int j = 0; j < 7; j++)
                {
                    BrickBear brickBear = PoolManager.Instance.GetBrick();
                    brickBear.InitBrick(t, brickMaterials[(int)t], spawnerID);
                    int index = Random.Range(0, spawnTransforms.Count);
                    brickBear.SetPosition(spawnTransforms[index].position, false);
                    spawnTransforms.RemoveAt(index);
                }
            }
        }

        #endregion
    }
}