using Assets.Scripts.Models;
using TMPro;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moodText;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _stepText;

    private void OnEnable()
    {
        ShowResult();
    }
    public void ShowResult()
    {
        var player = Player.InitializePlayer();
        _moodText.text = player.MoodValue.ToString() + "/100";
        _moneyText.text = player.Money.ToString();
        _stepText.text = player.CurrentDialogStepId.ToString();
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }
}
