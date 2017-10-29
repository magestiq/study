using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CourseProjWPFKudlay.exceptions;

namespace CourseProjWPFKudlay.objects
{   //класс в котором храняться методы предобработки графов(матриц)
    static class Comparison
    {   //метод определения числа внутренней устойчивости для графа(матрицы)
        public static int internalStability(int[,] parMatrix)
        {
            Dictionary<int, int> internalMult = new Dictionary<int, int>();

            int internalStabilityNumber = 1; //число внутренней устойчивости матрицы

            if (parMatrix == null) //сначала нужно проверить была ли передана(существует ли) матрица вообще
            {
                throw new NullMatrixException("Нету полного объёма матриц для сравнения!");
            }

            //также нужно проверить есть ли в сравниваемых графах петли
            for (int i = 0; i < parMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < parMatrix.GetLength(1); j++)
                {
                    if ((parMatrix[i, j] == 1) && (parMatrix[j, i] == 1))
                        throw new IncorrectInputException("Введённый граф содержит петли. Вершина имеющая петлю не может принадлежать внутренне устойчивому множеству!");
                }
            }

            //определение числа внутренней устойчивости для матрицы
            for (int i = 0; i < parMatrix.GetLength(1); i++)
                for (int j = 0; j < parMatrix.GetLength(1); j++)
                {
                    if (parMatrix[i, j] == 1)
                    {
                        internalMult.Add(i, j);
                    }
                }


            return internalStabilityNumber;
        }
    }
}
