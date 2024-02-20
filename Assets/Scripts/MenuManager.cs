using Assets.Scripts;
using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _resultManager;

    [Inject] private Player _player;

    public static event Action<int> OnLoadDialogStep;

    private void Awake()
    {
        ShowResults(false);
    }
    public void LoadDialogStep(int indexStep)
    {
        try
        {
            if (indexStep < 0) return;
            else if (indexStep == 0)
                _player.SetOnDefaultValue();
            else
            {
                List<DataGame> dataList = BinarySerializer.Deserialize();
                if (dataList == null || dataList.Count == 0) return;

                var data = dataList.LastOrDefault(x => x.DialogStepId == indexStep);
                _player.CurrentDialogStepId = indexStep;
                _player.MoodValue = data.MoodValue;
                _player.Money = data.Money;
            }
            OnLoadDialogStep(indexStep);
            CloseMenu();
        }
        catch
        { throw; }

    }
    /// <summary>
    /// Set active result panel on UI
    /// </summary>
    /// <param name="visible"></param>
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
