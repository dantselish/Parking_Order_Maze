using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsPopup : MonoBehaviour
{
    [SerializeField] private Button closeBtn;
    [SerializeField] private TMP_Text levelsCount;
    [SerializeField] private TMP_Text carsCrashed;
    [SerializeField] private TMP_Text movesMade;


    private void Awake()
    {
        closeBtn.onClick.AddListener(OnCloseClicked);
    }

    private void OnEnable()
    {
        levelsCount.SetText((GameManager.Instance.levelIndex + 1).ToString());
        carsCrashed.SetText(GameManager.Instance.AchievementsManager.carsCrashed.ToString());
        movesMade.SetText(GameManager.Instance.AchievementsManager.movesMade.ToString());
    }

    private void OnCloseClicked()
    {
        gameObject.SetActive(false);
    }
}
