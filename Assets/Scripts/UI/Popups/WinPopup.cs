using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class WinPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text timeLeftText;
    [SerializeField] private TMP_Text rewardAmount;


    public void SetTimeAndReward(float timeLeft, int reward)
    {
        timeLeftText.SetText($"{timeLeft / 60:00}:{timeLeft % 60:00}");
        rewardAmount.SetText(reward.ToString());
    }
}
