using Assets.Scripts;
using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _resultManager;

    public static event Action<int> OnLoadDialogStep;

    private void Awake()
    {
        ShowResults(false);
    }
    public void LoadDialogStep(int indexStep)
    {
        List<DataGame> dataList = BinarySerializer.Deserialize();
        if (dataList == null || dataList.Count == 0) return;

        var data = dataList.LastOrDefault(x => x.DialogStepId == indexStep);
        var player = Player.InitializePlayer();
        player.CurrentDialogStepId = indexStep;
        player.MoodValue = data.MoodValue;
        player.Money = data.Money;

        OnLoadDialogStep(indexStep);
        CloseMenu();

    }
    public void ShowResults(bool visible)
    {
        _resultManager.SetActive(visible);
    }
    public void CleanSaves()
    {
        BinarySerializer.CleanData();
    }
    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }
    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
