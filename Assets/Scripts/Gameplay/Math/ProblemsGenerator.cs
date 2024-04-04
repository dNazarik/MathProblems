using System;
using System.Collections.Generic;
using _3rdParty.Core;
using Core;
using Gameplay.Configs;
using Gameplay.General;
using UnityEngine;

namespace Gameplay.Math
{
    public class ProblemsGenerator
    {
        private const int AnswerOptionsAmount = 4;
        private const string ProblemFormat = "{0} {1} {2} = ?";

        /// <summary>
        /// The value is dynamic just in case we want to add more
        /// problem types in the future (for instance Exponentiation or Square root)
        /// </summary>
        private readonly int _problemTypesAmount = Enum.GetNames(typeof(ProblemType)).Length;

        private readonly int[] _difficultyMaxRangeNumber = { 10, 100, 1000 };

        private readonly GameModel _gameModel;
        private readonly GameSettings _gameSettings;

        public ProblemsGenerator(GameModel gameModel, GameSettings gameSettings)
        {
            _gameModel = gameModel;
            _gameSettings = gameSettings;
        }

        private int GetMaxNumberForRange(Difficulty difficulty) => _difficultyMaxRangeNumber[(int)difficulty];

        public MathProblem GetGeneratedQuestion()
        {
            var problemType = GetProblemTypeRandomly(_gameSettings.CurrentDifficulty);
            var problemSymbol = GetProblemSymbol(problemType);
            var questionAndAnswer = GenerateQuestionAndAnswer(_gameSettings.CurrentDifficulty, problemType);
            var problemString = string.Format(ProblemFormat, questionAndAnswer.Item1, problemSymbol,
                questionAndAnswer.Item2);
            var restOptions = GenerateRestOptions(questionAndAnswer.Item3);

            MathProblem problem = new MathProblem
            {
                CorrectAnswer = questionAndAnswer.Item3,
                ProblemString = problemString,
                Options = restOptions
            };

            return problem;
        }

        private ProblemType GetProblemTypeRandomly(Difficulty currentDifficulty)
        {
            if (currentDifficulty == Difficulty.Easy)
                return Randomizer.IsRandomHappen() ? ProblemType.Addition : ProblemType.Subtraction;

            var randomValue = Randomizer.GetNumberInRange(0, _problemTypesAmount);

            return (ProblemType)randomValue;
        }

        private static char GetProblemSymbol(ProblemType problemType)
        {
            switch (problemType)
            {
                case ProblemType.Addition:
                default:
                    return Constants.AdditionSymbol;
                case ProblemType.Subtraction:
                    return Constants.SubtractionSymbol;
                case ProblemType.Multiplication:
                    return Constants.MultiplicationSymbol;
                case ProblemType.Division:
                    return Constants.DivisionSymbol;
            }
        }

        private (int, int, int) GenerateQuestionAndAnswer(Difficulty difficulty, ProblemType problemType)
        {
            var answer = Randomizer.GetNumberInRange(0, GetMaxNumberForRange(difficulty));
            var number1 = 0;
            var number2 = 0;

            (int, int) mathProblemNumbers;

            switch (problemType)
            {
                default:
                case ProblemType.Addition:
                    number1 = Randomizer.GetNumberInRange(0, answer);
                    number2 = answer - number1;
                    break;
                case ProblemType.Subtraction:
                    number1 = Randomizer.GetNumberInRange(answer, GetMaxNumberForRange(difficulty));
                    number2 = Mathf.Abs(answer - number1);
                    break;
                case ProblemType.Multiplication:
                    mathProblemNumbers = GetMultiplicationNumbers(answer);
                    number1 = mathProblemNumbers.Item1;
                    number2 = mathProblemNumbers.Item2;
                    break;
                case ProblemType.Division:
                    mathProblemNumbers = GetDivisionNumbers(answer);
                    number1 = mathProblemNumbers.Item1;
                    number2 = mathProblemNumbers.Item2;
                    break;
            }

            return (number1, number2, answer);
        }

        private (int, int) GetDivisionNumbers(int correctAnswer)
        {
            var divisors = FindDivisors(correctAnswer);
            var divisor = divisors.RandomElement();
            var dividend = correctAnswer * divisor;

            return (dividend, divisor);
        }

        private (int, int) GetMultiplicationNumbers(int correctAnswer)
        {
            var divisors = FindDivisors(correctAnswer);
            var multiplier1 = divisors.RandomElement();
            var multiplier2 = correctAnswer / multiplier1;

            return (multiplier1, multiplier2);
        }

        private static List<int> FindDivisors(int number)
        {
            var divisors = new List<int>();

            for (int i = 1; i <= number; i++)
            {
                if (number % i == 0)
                    divisors.Add(i);
            }

            return divisors;
        }

        private int[] GenerateRestOptions(int correctAnswer)
        {
            const int restAnswersAmount = AnswerOptionsAmount - 1;

            var restAnswers = new int[restAnswersAmount];
            var existingOptions = new List<int>(restAnswersAmount) { correctAnswer };

            for (var i = 0; i < restAnswers.Length; i++)
            {
                var option = Randomizer.GetNumberInRangeExcluding(0, GetMaxNumberForRange(_gameSettings.CurrentDifficulty),
                    existingOptions);

                restAnswers[i] = option;

                existingOptions.Add(option);
            }

            return restAnswers;
        }
    }
}