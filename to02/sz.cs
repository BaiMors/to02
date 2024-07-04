using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace to02
{
    internal class sz
    {
        public void Main()
        {
            try
            {
                //Console.WriteLine("Метод северо-западного угла");
                //severo_zapadnii_ugol();
                //Console.WriteLine();
                Console.WriteLine("Метод минимальной стоимости");
                min_stoimost();
                //Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

        }

        /// <summary>
        /// метод реализации метода северо-западного угла
        /// </summary>
        static void min_stoimost()
        {
            string path = "sz_1.csv";
            kod_prufera kod_Prufera = new kod_prufera();
            string[] lines = File.ReadAllLines(path);
            int stroki = lines.Length;
            int stolbci = lines[0].Split(';').Length;
            int[,] mas = kod_Prufera.readToIntDvumMas(path, stroki, stolbci);
            int[] rows = new int[mas.GetLength(0)];
            int[] columns = new int[mas.GetLength(1)];
            int[,] perev = new int[mas.GetLength(0), mas.GetLength(1)];

            for (int i = 0; i < columns.Length; i++)
            {
                Console.WriteLine("Введите количество запрашиваемого товара для " + i + " покупателя");
                columns[i] = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < rows.Length; i++)
            {
                Console.WriteLine("Введите количество имеющегося товара у " + i + " поставщика");
                rows[i] = Convert.ToInt32(Console.ReadLine());
            }

            if (proverka_na_otkritost(rows, columns) == 2)
            {
                Console.WriteLine("Задача открытая. Перепроверьте данные");
                System.Environment.Exit(0);
            }

            int mi, mj;
            Console.WriteLine();
            int[,] mas2 = new int[mas.GetLength(0), mas.GetLength(1)];
            Array.Copy(mas, mas2, mas.Length);
            while (rows.Count(x => x == 0) < mas.GetLength(0) && columns.Count(x => x == 0) < mas.GetLength(1))
            {
                found_min(mas2, out mi, out mj);
                mas2[mi, mj] = sum_el_dvum(mas);
                if (columns[mj] >= rows[mi] && rows[mi] != 0)
                {
                    perev[mi, mj] = columns[mj] - (columns[mj] - rows[mi]);
                    columns[mj] -= perev[mi, mj];
                    rows[mi] -= perev[mi, mj];
                }
                else if (columns[mj] <= rows[mi] && columns[mj] != 0)
                {
                    perev[mi, mj] = rows[mi] - (rows[mi] - columns[mj]);
                    rows[mi] -= perev[mi, mj];
                    columns[mj] -= perev[mi, mj];
                }
            }

            vivod(mas, perev);
        }

        /// <summary>
        /// метод нахождения минимального элемента в двумерном массиве
        /// </summary>
        /// <param name="mas">массив тарифов на перевозку</param>
        /// <param name="mi">первый индекс минимального элемента</param>
        /// <param name="mj">второй индекс миниального элемента</param>
        /// <returns>минимальный элемент массива</returns>
        static int found_min(int[,] mas, out int mi, out int mj)
        {
            mi = 0;
            mj = 0;
            int min = mas[0, 0];
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    if (min > mas[i, j])
                    {
                        min = mas[i, j];
                        mi = i;
                        mj = j;
                    }
                }
            }
            return min;
        }

        /// <summary>
        /// метод нахождения суммы элементов двумерного массива
        /// </summary>
        /// <param name="mas">двумерный массив, сумму элементов которого требуется посчитать</param>
        /// <returns>сумма элементов</returns>
        static int sum_el_dvum(int[,] mas)
        {
            int sum = 0;
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    sum += mas[i, j];
                }
            }
            return sum;
        }

        static void severo_zapadnii_ugol()
        {
            string path = "sz_1.csv";
            kod_prufera kod_Prufera = new kod_prufera();
            string[] lines = File.ReadAllLines(path);
            int stroki = lines.Length;
            int stolbci = lines[0].Split(';').Length;
            int[,] mas = kod_Prufera.readToIntDvumMas(path, stroki, stolbci);
            int[] rows = new int[mas.GetLength(0)];
            int[] columns = new int[mas.GetLength(1)];
            int[,] perev = new int[mas.GetLength(0), mas.GetLength(1)];

            for (int i = 0; i < columns.Length; i++)
            {
                Console.WriteLine("Введите количество запрашиваемого товара для " + i + " покупателя");
                columns[i] = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < rows.Length; i++)
            {
                Console.WriteLine("Введите количество имеющегося товара у " + i + " поставщика");
                rows[i] = Convert.ToInt32(Console.ReadLine());
            }

            if (proverka_na_otkritost(rows, columns) == 2)
            {
                Console.WriteLine("Задача открытая. Перепроверьте данные");
                System.Environment.Exit(0);
            }

            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    if (columns[j] >= rows[i] && rows[i] != 0)
                    {
                        perev[i, j] = columns[j] - (columns[j] - rows[i]);
                        columns[j] -= perev[i, j];
                        rows[i] -= perev[i, j];
                    }
                    else if (columns[j] <= rows[i] && columns[j] != 0)
                    {
                        perev[i, j] = rows[i] - (rows[i] - columns[j]);
                        rows[i] -= perev[i, j];
                        columns[j] -= perev[i, j];
                    }
                    if (rows[i] == 0) break;
                }
            }

            vivod(mas, perev);
        }

        /// <summary>
        /// метод проверки задачи на открытость
        /// </summary>
        /// <param name="rows">массив ресурсов на складах</param>
        /// <param name="columns">массив требуемых ресурсов покупателям</param>
        /// <returns>1 - задача закрытая, 2 - задача закрытая</returns>
        static int proverka_na_otkritost(int[] rows, int[] columns)
        {
            if (rows.Sum() == columns.Sum()) { return 1; }
            else { return 2; }
        }

        /// <summary>
        /// метод нахождения целевой функции
        /// </summary>
        /// <param name="mas">массив тарифов на перевозку</param>
        /// <param name="perev">массив значений перевозок</param>
        /// <returns>значение целевой функции задачи</returns>
        static int cel_fun(int[,] mas, int[,] perev)
        {
            int cel_fun = 0;
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    cel_fun += perev[i, j] * mas[i, j];
                }
            }
            return cel_fun;
        }

        /// <summary>
        /// метод вывода на экран двумерный массив со значениями перевозок
        /// </summary>
        /// <param name="mas">исходный массив с тарифами на перевозку</param>
        /// <param name="perev">полученный массив со значениями перевозок</param>
        static void vivod(int[,] mas, int[,] perev)
        {
            Console.WriteLine("Полученная матрица: ");
            for (int i = 0; i < perev.GetLength(0); i++)
            {
                for (int j = 0; j < perev.GetLength(1); j++)
                {
                    Console.Write(perev[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Целевая функция: " + cel_fun(mas, perev));
        }
    }
}
