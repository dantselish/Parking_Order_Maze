using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private AchievementsPopup achievementsPopup;
    [SerializeField] private VolumeButton sfxBtn;
    [SerializeField] private Button policyBtn;
    [SerializeField] private Button playBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private Button achievementsBtn;


    private void Start()
    {
        Subscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        sfxBtn.Button.onClick.AddListener(OnSFXClicked);
        policyBtn.onClick.AddListener(OnPolicyClicked);
        playBtn.onClick.AddListener(OnPlayClicked);
        quitBtn.onClick.AddListener(OnQuitClicked);
        achievementsBtn.onClick.AddListener(OnAchievementsClicked);
    }

    private void Unsubscribe()
    {
        sfxBtn.Button.onClick.RemoveListener(OnSFXClicked);
        policyBtn.onClick.RemoveListener(OnPolicyClicked);
        playBtn.onClick.RemoveListener(OnPlayClicked);
        quitBtn.onClick.RemoveListener(OnQuitClicked);
        achievementsBtn.onClick.RemoveListener(OnAchievementsClicked);
    }

    private void OnSFXClicked()
    {
        GameManager.Instance.AudioManager.ToggleSFX();
        sfxBtn.SetSprite(GameManager.Instance.AudioManager.isSFXMuted);
    }

    private void OnPolicyClicked()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=OlIhcnTovz4&ab_channel=UnityToBrain");
    }

    private void OnPlayClicked()
    {
        GameManager.Instance.SceneLoader.LoadGameScene();
    }

    private void OnQuitClicked()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void OnAchievementsClicked()
    {
        achievementsPopup.gameObject.SetActive(true);
    }
}
