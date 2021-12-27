using System;

namespace MAI
{
    class Program
    {
        /// <summary>
        /// Вывод ошибки.
        /// </summary>
        /// <param name="msg">Текст ошибки.</param>
        static void PrintErr(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        /// <summary>
        /// Проверяет входные данные.
        /// </summary>
        /// <returns>Введенное пользователем число от 1 до max</returns>
        static int InputInt(int max, string msg)
        {
            int value;

            while (true)
            {
                Console.Write(msg);

                if (!Int32.TryParse(Console.ReadLine(), out value))
                {
                    PrintErr("Введите число!");
                    continue;
                }

                if (!(value >= 1 && value <= max))
                {
                    PrintErr($"Число вне диапазона! (1 <= n <= {max})");
                    continue;
                }

                return value;
            }
        }

        static void Main(string[] args)
        {
            int countCriteria; 
            double multiLine, sumAllLines;
            double[,] matrix;
            double[] multiplication;

            countCriteria = InputInt(Int32.MaxValue, "Введите количество критериев: ");
            matrix = new double[countCriteria, countCriteria];
            multiplication = new double[countCriteria];

            // Заполнение матрицы нулями и единицами по диагонали.
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i == j)
                        matrix[i, j] = 1;
                    else
                        matrix[i, j] = 0;
                }
            }

            // Заполнение критериев пользователем ниже диагонали.
            // Выше диагонали заполняется автоматически.
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 1)
                        break;
                    matrix[i, j] = InputInt(9, $"Введите данные попарного " +
                        $"сравнения критериев {i + 1} и {j + 1}: ");
                    matrix[j, i] = matrix[0, 0] / matrix[i, j];
                }
            }

            // Заполнение критериев пользователем.
            sumAllLines = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                // Перемножение чисел в каждой строке матрицы.
                multiLine = 1;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    multiLine *= matrix[i, j];
                }
                // Нахождение корня произведений всех чисел строки.
                multiplication[i] = Math.Pow(multiLine, 1.0 / countCriteria);
                // Нахождение суммы корней произведений всех строк.
                sumAllLines += multiplication[i];
            }

            // Вывод весового коэффициентадля каждой строки.
            for (int i = 0; i < multiplication.Length; i++)
            {
                Console.WriteLine("Весовой коэффициент для " + (i + 1) + 
                    "-го критерия: " + Math.Round(multiplication[i] / sumAllLines, 2));
            }

            Console.ReadKey();
        }
    }
}
