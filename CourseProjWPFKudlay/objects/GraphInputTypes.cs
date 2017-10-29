using System;
using CourseProjWPFKudlay.exceptions;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace CourseProjWPFKudlay.objects
{   //класс ввода графа с помощью FI представления
    static class GraphFiInput
    {
        private static int[,] matrix; //матрица полученная из строки представления FI
        static int[] intArr; //преобразованная строка представления FI 
        static int zeroCounter; //счётчик количества нулей
        static char[] num = new char[11] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', ' ' }; //масив допустимых символов
        

        public static int[,] fiInput(string textBoxInputString)
        {
            if (textBoxInputString == "") //если пернеданный параметр строки ввода пуст
            {
                throw new IncorrectInputException("Введите данные");
            }

            //ручная проверка на только цыфры 
            int sameCount = 0; //счётчик совпадений
            for (int i = 0; i < textBoxInputString.Length; i++) //берём i-ый элемент строки представления
            {
                sameCount = 0; //обнулить счётчик для нового элемента
                for (int j = 0; j < num.Length; j++) //берём i-ый элемент допустымых символов
                {
                    if (textBoxInputString[i] == num[j]) //если есть совпадение то символ входит в заданный масив допустимых символов
                    {
                        sameCount++;
                    }
                }
                if (sameCount == 0) //если i-ый символ из строки представления не нашёл совпадения в массиве допустимых чисел
                    throw new IncorrectInputException("В представлении имеються недопустимые символы!");
            }

            //убираються лишние пробелы между числами и оставляеться только один
            string[] strArr = string.Join(" ", textBoxInputString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)).Split(' ');          
            intArr = Array.ConvertAll(strArr, int.Parse); //конвертация строкового массива в целочисленный
            ///
            ///Отлавливание NullReferneceEx переделать под IncorrectInputException
            ///
            try
            {
                zeroCounter = 0; //обнуление счётчика нулей в массиве FI
                for (int i = 0; i < intArr.Length; i++) //подсчёт количества нулей в массиве FI
                {
                    if (intArr[i] == 0)
                        zeroCounter++;
                }

                ///
                ///ПРОВЕРКА НА КОЛ-ВО РЁБЕР МЕНЬШЕ 50 И ВЕРИШН МЕНЬШЕ 20
                ///Врешин - разделитеолей больше 20
                ///Рёбер - размер-кол-во разделителй больше 50
                ///
                if (zeroCounter > 20)
                    throw new IncorrectInputException("Количество вершин недопустимо!");

                if ((intArr.Length - zeroCounter) > 50)
                    throw new IncorrectInputException("Количество рёбер недопустимо!");

                //определение максимального числа в массиве представления
                int max = 0; 
                for (int i = 0; i < intArr.Length; i++)
                {
                    if (intArr[i] > max)
                        max = intArr[i];
                }

                //в представлении не может быть отрицательных чисел
                for (int i = 0; i < intArr.Length; i++)
                {
                    if (intArr[i] < 0)
                        throw new IncorrectInputException("В представлении не может быть отрицательных чисел!");
                }

                //также количество нулей-разделителей обязательно должо быть больше или равно максимального числа в представлении
                if (zeroCounter < max)
                    throw new IncorrectInputException("Количество разделителей должно быть больше или равно максимального числа в представлении!");

                matrix = new int[zeroCounter, zeroCounter]; //инициализация размерности матрицы

                //построение матрицы смежности
                int iCount = 0; //счёт строки
                for (int i = 0; i < intArr.Length; i++)
                {
                    if (intArr[i] != 0) // если элемент не символ "0"(разделитель)
                    {
                        matrix[intArr[i] - 1, iCount] = 1; // на позицию текущей строки и столбца установить единицу
                    }
                    else //если символ "0"(разделитель)
                    {
                        iCount++; //перейти на следующую строку в матрице
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return matrix;
        }
    }
}
