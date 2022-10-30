using _ORANGEBEAR_.EventSystem;
using UnityEngine;

public class AnimationManager : Bear
{
    #region Singleton

    public static AnimationManager Instance;

    #endregion

    #region Serialized Fields

    [SerializeField] private Animator settingsButtonAnimator;
    [SerializeField] private Animator settingsHolderAnimator;
    private static readonly int Play = Animator.StringToHash("Play");

    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Public Methods

    public void PlaySettingsButtonAnimation()
    {
        settingsButtonAnimator.SetBool(Play, !settingsButtonAnimator.GetBool(Play));
        settingsHolderAnimator.SetBool(Play, !settingsHolderAnimator.GetBool(Play));
    }

    #endregion
}