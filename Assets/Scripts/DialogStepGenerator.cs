using Assets.Scripts.Models;
using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Scripts
{
    public class DialogStepGenerator
    {
        public static event Action<int> OnChangedMood;
        public static event Action<int> OnChangedMoney;

        private int _currentStepIndex = 0;

        private DialogStep[] _steps;
        
        public int CurrentStepIndex => _currentStepIndex;
        public DialogStepGenerator()
        {
            InitAllSteps();
            BinarySerializer.PathString = Path.Combine(Application.dataPath, "Data");
            BinarySerializer.FileName = "dataGame.dat";
        }
        public void SaveAnswer(int indexAswer)
        {
            try
            {
                if (_steps == null || _currentStepIndex >= _steps.Length) return;

                var step = _steps[_currentStepIndex];
                step.SavedAnswer = step.Answers[indexAswer];
                OnChangedMood(step.SavedAnswer.Points);
                OnChangedMoney(step.SavedAnswer.Money);
                var savedData = new DataGame();
                var player = Player.InitializePlayer();
                savedData.DialogStepId = player.CurrentDialogStepId = _currentStepIndex;
                savedData.MoodValue = player.MoodValue;
                savedData.Money = player.Money;
                

                BinarySerializer.Serialize(savedData);
            }
            catch
            { throw; }
        }
        public DialogStep NextStep()
        {
            try
            {
                if (_steps != null && ++_currentStepIndex < _steps.Length)
                    return _steps[_currentStepIndex];

                return null;
            }
            catch
            { throw; }
        }
        public DialogStep LoadStep(int indexStep)
        {
            try
            {
                if (_steps != null && indexStep >= 0 && indexStep < _steps.Length)
                {
                    _currentStepIndex = indexStep;
                    return _steps[indexStep];
                }

                return null;
            }
            catch
            { throw; }
        }
        private void InitAllSteps()
        {
            _steps = new DialogStep[] {
            new DialogStep
            {
                Id = 0,
                Question = "Добрый вечер, рады вас видеть. Будете чего-нибудь заказывать ?",
                Answers = new Answer[3]
                { 
                    new Answer { Text = "Привет", Points = 10 },
                    new Answer { Text = "Добрый вечер. Да, сейчас посмотрю, что есть.", Points = 13 },
                    new Answer { Text = "Нет, я уже ухожу", Points = 1 },
                },
                HasTimer = true
            },
            new DialogStep
            {
                Id = 1,
                Question = "Может вы все-таки посмотрите наше горячее предложение ?",
                Answers = new Answer[3]
                {
                    new Answer { Text = "Я очень голоден, а здесь ничего интересного.", Points = 2 },
                    new Answer { Text = "Съел бы даже динозавра", Points = 15 },
                    new Answer { Text = "А что вы можете предложить ?", Points = 9 },
                },
            },
            new DialogStep
            {
                Id = 2,
                Question = "Для начала я принесу вам стакан лимонада и как наш частый гость вы получите 350 бонусов к вашему заказу",
                Answers = new Answer[3]
                {
                    new Answer { Text = "Ох, звучит отлично", Points = 14, Money = 350 },
                    new Answer { Text = "Нет, я ухожу", Points = 3 },
                    new Answer { Text = "Спасибо большое, у вас очень приятная улыбка", Points = 20, Money = 350 },
                },
            },
            new DialogStep
            {
                Id = 3,
                Question = "Подождите 5 минут и вы почувствуете себя, как в сказке",
                Answers = new Answer[3]
                {
                    new Answer { Text = "Не люблю сказки", Points = 1 },
                    new Answer { Text = "А вы знете толк в убеждении посетителей", Points = 15 },
                    new Answer { Text = "Пожалуй закажу еще мясную нарезку с гребным соусом", Points = 10, Money = -400 },
                },
                HasTimer = true
            },
            new DialogStep
            {
                Id = 4,
                Question = "Внезапно вывозят большой торт с тремя красавицами внутри. И на счет 3 они выскакивают из него с громкими поздравлениями ко дню благодарения..",
                Answers = new Answer[3]
                {
                    new Answer { Text = "Неужели за это еще платить придется", Points = 3, Money = -600 },
                    new Answer { Text = "-Запеть веселую песню-", Points = 15, Money = 250 },
                    new Answer { Text = "Вот это я понимаю сказка", Points = 25 },
                },
            },
        };
        }

    }
}
