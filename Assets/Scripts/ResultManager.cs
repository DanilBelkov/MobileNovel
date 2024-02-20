using Assets.Scripts.Models;
using TMPro;
using UnityEngine;
using Zenject;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moodText;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _stepText;

    [Inject] private Player _player;

    private void OnEnable()
    {
        ShowResult();
    }
    /// <summary>
    /// Update UI text in result panel
    /// </summary>
    public void ShowResult()
    {
        _moodText.text = _player.MoodValue.ToString() + "/100";
        _moneyText.text = _player.Money.ToString();
        _stepText.text = _player.CurrentDialogStepId.ToString();
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }
}
