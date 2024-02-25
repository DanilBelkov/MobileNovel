using Assets.Scripts;
using Assets.Scripts.Models;
using System;
using TMPro;
using UnityEngine;
using Zenject;

public class SelectedHandler : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _prefab;

    [SerializeField] private GameObject _questionPanel;

    [SerializeField] private GameObject _answerPanel_1;
    [SerializeField] private GameObject _answerPanel_2;
    [SerializeField] private GameObject _answerPanel_3;

    private TextMeshProUGUI _questionPanelText;
    private TextMeshProUGUI _answerPanelText_1;
    private TextMeshProUGUI _answerPanelText_2;
    private TextMeshProUGUI _answerPanelText_3;

    private DialogStepGenerator _generator;

    [Inject] private Player _player;
    [Inject] private DiContainer _diConainer;

    public static event Action<bool?> OnSetTimer;

    private void Awake()
    {
        _questionPanelText = _questionPanel.GetComponentInChildren<TextMeshProUGUI>();
        _answerPanelText_1 = _answerPanel_1.GetComponentInChildren<TextMeshProUGUI>();
        _answerPanelText_2 = _answerPanel_2.GetComponentInChildren<TextMeshProUGUI>();
        _answerPanelText_3 = _answerPanel_3.GetComponentInChildren<TextMeshProUGUI>();

        _generator = new DialogStepGenerator();
        OnLoadDialogStep(0);
    }
    private void OnEnable()
    {
        MenuManager.OnLoadDialogStep += OnLoadDialogStep;
        TimerManager.OnMissDialog += OnMissDialodStep;
    }
    private void OnDisable()
    {
        MenuManager.OnLoadDialogStep -= OnLoadDialogStep;
        TimerManager.OnMissDialog -= OnMissDialodStep;
    }

    public void OpenMenu()
    {
        _diConainer.InstantiatePrefabResource("Prefabs/LoadPanel", parentTransform: _canvas.transform);
    }
    public void OnAnswerSelected(int indexAnswer)
    {
        _generator.SaveDataPlayer(_player);
        _generator.ApplyAnswer(--indexAnswer);
        NextDialogStep();
    }
    private void OnLoadDialogStep(int indexStep)
    {
        SetDialogStep(_generator.LoadStep(indexStep));
        SetActiveDialog(true);
    }
    /// <summary>
    /// Set UI text in dialog step 
    /// </summary>
    /// <param name="step">Dialog step data</param>
    private void SetDialogStep(DialogStep step)
    {
        _questionPanelText.text = step?.Question;
        _answerPanelText_1.text = step?.Answers[0].Text;
        _answerPanelText_2.text = step?.Answers[1].Text;
        _answerPanelText_3.text = step?.Answers[2].Text;
        OnSetTimer?.Invoke(step?.HasTimer);
    }
    private void OnMissDialodStep()
    {
        _generator.SaveDataPlayer(_player);
        _generator.ApplyAnswer(0);
        NextDialogStep();
    }
    private void NextDialogStep()
    {
        try
        {
            var step = _generator.NextStep();
            if (step == null)
            {
                SetActiveDialog(false);
            }
            SetDialogStep(step);
        }
        catch
        {
            throw new Exception("Error in next dialog step");
        }
    }
    private void SetActiveDialog(bool value)
    {
        _questionPanel.SetActive(value);
        _answerPanel_1.SetActive(value);
        _answerPanel_2.SetActive(value);
        _answerPanel_3.SetActive(value);
    }
    
}
