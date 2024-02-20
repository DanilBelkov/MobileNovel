using Assets.Scripts;
using Assets.Scripts.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DataGameManager : MonoBehaviour
{
    [SerializeField] private Image _moodBarImage;
    [SerializeField] private TextMeshProUGUI _moneyText;

    private int _maxValue = 100;
    private int _minValue = 0;
    [Inject] private Player _player;

    private void Awake()
    {
        UpdateMoodBar();
        UpdateMoneyText();
    }
   
    private void ChangeMoney(int value)
    {
        var money = _player.Money + value;
        if (money < _minValue)
            money = _minValue;
        _player.Money = money;
    }
    private void ChangeMood(int value)
    {
        var moodValue = _player.MoodValue + value;
        if (moodValue > _maxValue)
            moodValue = _maxValue;
        else if (moodValue < _minValue)
            moodValue = _minValue;
        _player.MoodValue = moodValue;
    }
    private void OnEnable()
    {
        DialogStepGenerator.OnChangedMood += ChangeMood;
        DialogStepGenerator.OnChangedMoney += ChangeMoney;
        Player.OnUpdate += OnUpdate;
    }
    private void OnDisable()
    {
        DialogStepGenerator.OnChangedMood -= ChangeMood;
        DialogStepGenerator.OnChangedMoney -= ChangeMoney;
        Player.OnUpdate -= OnUpdate;
    }
    private void OnUpdate()
    {

        UpdateMoneyText();
        UpdateMoodBar();
    }
    private void UpdateMoneyText()
    {
        _moneyText.text = _player.Money.ToString();
    }
    private void UpdateMoodBar()
    {
        _moodBarImage.fillAmount = _player.MoodValue * 0.01f;
    }
}
