using TMPro;
using UnityEngine;

public class Currency : MonoBehaviour
{
    [SerializeField] private TMP_Text text;


    private void Start()
    {
        GameManager.Instance.MoneyChanged += OnMoneyChanged;
        SetText(GameManager.Instance.moneyAmount);
    }

    private void SetText(int amount)
    {
        text.SetText(amount.ToString());
    }

    private void OnMoneyChanged(int amount)
    {
        SetText(amount);
    }
}
