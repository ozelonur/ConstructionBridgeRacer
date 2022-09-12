#region Header

// Developed by Onur ÖZEL

#endregion

using System.IO;
using _ORANGEBEAR_.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace _ORANGEBEAR_.Scripts.Editor
{
    public class LevelCreator : EditorWindow
    {
        #region Fields

        private GameObject _level;
        private Level _levelData;

        private DirectoryInfo _directoryInfo;

        private int _count;

        #endregion

        [MenuItem("Orange Bear/Create Level")]
        public static void OpenLevelCreatorWindow()
        {
            GetWindow<LevelCreator>("LevelCreator");
        }

        private void OnGUI()
        {
            GUILayout.Label("Level Prefab", EditorStyles.boldLabel);
            _level = (GameObject) EditorGUILayout.ObjectField("Level", _level, typeof(GameObject), false);
            GUILayout.Label("Level Data", EditorStyles.boldLabel);
            _levelData = (Level) EditorGUILayout.ObjectField("Level", _levelData, typeof(Level), false);

            if (GUILayout.Button("Create Level"))
            {
                CreateLevel();
            }
        }

        #region Private Methods

        private void CreateLevel()
        {
            DirectoryInfo info = new DirectoryInfo("Assets/[GAME]/Levels");
            var fileInfo = info.GetFiles();

            _count = fileInfo.Length;

            if (_count == 1)
            {
                _count = 1;
            }

            else
            {
                _count = ((_count - 1) / 4) + 1;
            }


            GameObject levelReference = (GameObject) PrefabUtility.InstantiatePrefab(_level);
            GameObject pVariant =
                PrefabUtility.SaveAsPrefabAsset(levelReference, $"Assets/[GAME]/Levels/_Level {_count}.prefab");
            GameObject.DestroyImmediate(levelReference);

            string dataPath = AssetDatabase.GetAssetPath(_levelData.GetInstanceID());

            AssetDatabase.CopyAsset(dataPath, $"Assets/[GAME]/Levels/Level {_count}.asset");

            Level asset = AssetDatabase.LoadAssetAtPath<Level>($"Assets/[GAME]/Levels/Level {_count}.asset");
            asset.LevelPrefab = pVariant;

            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
        }

        #endregion
    }
}