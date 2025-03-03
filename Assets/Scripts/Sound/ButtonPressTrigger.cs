using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonPressTrigger : MonoBehaviour
{
    private Button button;


    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        SoundPlayer.Instance.PlayButtonPress();
    }
}
