using Assets.Scripts;
using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class MenuManager : MonoBehaviour
{
    [Inject] private Player _player;
    [Inject] private DiContainer _diContainer;

    public static event Action<int> OnLoadDialogStep;

    public void LoadDialogStep(int indexStep)
    {
        try
        {
            if (indexStep < 0) return;
            else if (indexStep == 0)
            {
                _player.SetOnDefaultValue();
            }
            else
            {
                List<DataGame> dataList = BinarySerializer.Deserialize();
                if (dataList == null || dataList.Count == 0) return;

                var data = dataList.LastOrDefault(x => x.DialogStepId == indexStep);
                _player.CurrentDialogStepId = indexStep;
                _player.MoodValue = data.MoodValue;
                _player.Money = data.Money;
            }
            OnLoadDialogStep?.Invoke(indexStep);
            CloseMenu();
        }
        catch
        { throw new Exception("Erorr in load dialog step"); }

    }
    /// <summary>
    /// Set active result panel on UI
    /// </summary>
    /// <param name="visible"></param>
    public void OpenResults()
    {
        _diContainer.InstantiatePrefabResource("Prefabs/ResultsPanel", parentTransform: transform.parent);
    }
    public void CleanSaves()
    {
        BinarySerializer.CleanData();
    }

    public void CloseMenu()
    {
        Destroy(gameObject);
    }
}
