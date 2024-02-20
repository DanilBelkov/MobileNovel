using Assets.Scripts;
using System;
using TMPro;
using UnityEngine;

public class SelectedHandler : MonoBehaviour
{

    [SerializeField] private GameObject _questionPanel;

    [SerializeField] private GameObject _answerPanel_1;
    [SerializeField] private GameObject _answerPanel_2;
    [SerializeField] private GameObject _answerPanel_3;

    private TextMeshProUGUI _questionPanelText;
    private TextMeshProUGUI _answerPanelText_1;
    private TextMeshProUGUI _answerPanelText_2;
    private TextMeshProUGUI _answerPanelText_3;

    private DialogStepGenerator _generator;

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

    public void OnAnswerSelected(int indexAnswer)
    {
        _generator.SaveAnswer(--indexAnswer);
        NextDialogStep();
    }
    private void OnLoadDialogStep(int indexStep)
    {
        SetDialogStep(_generator.LoadStep(indexStep));
        SetActiveDialog(true);
    }
    private void SetDialogStep(DialogStep step)
    {
        _questionPanelText.text = step?.Question;
        _answerPanelText_1.text = step?.Answers[0].Text;
        _answerPanelText_2.text = step?.Answers[1].Text;
        _answerPanelText_3.text = step?.Answers[2].Text;
        OnSetTimer(step?.HasTimer);
    }
    private void OnMissDialodStep()
    {
        _generator.SaveAnswer(0);
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
            throw;
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
