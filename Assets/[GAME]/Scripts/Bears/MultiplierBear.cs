#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Interfaces;
using _ORANGEBEAR_.EventSystem;
using DG.Tweening;
using UnityEngine;

namespace _GAME_.Scripts.Bears
{
    public class MultiplierBear : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private Transform building;

        [SerializeField] private ParticleSystem dustParticle;
        [SerializeField] private Renderer[] multiplierRenderers;
        [SerializeField] private Color color;

        #endregion

        #region Private Variables

        private Collider _collider;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            foreach (var mRenderer in multiplierRenderers)
            {
                mRenderer.material.color = Color.white;
            }

            building.transform.localScale = Vector3.zero;
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out ICollector collector))
            {
                return;
            }

            _collider.enabled = false;
            collector.SubtractCount(building);
            dustParticle.Play();
            
            foreach (var mRenderer in multiplierRenderers)
            {
                mRenderer.material.DOColor(color, 1f).SetLink(mRenderer.gameObject);
            }

            building.transform.DOScale(Vector3.one * .75f, 1f)
                .SetDelay(.2f)
                .SetEase(Ease.OutBack)
                .SetLink(building.gameObject);
        }

        #endregion
    }
}