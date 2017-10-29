using CourseProjWPFKudlay.exceptions;
using System.Diagnostics;
namespace CourseProjWPFKudlay.objects
{   //статический класс в котором хранятся методы предобработки графов (матриц)
    static class Preprocess
    {
        //метод гомеоморфного сжатия
        public static int[,] doHomeomorphic(int[,] parMatrix)
        {
            if (parMatrix == null) //если передаваемая матрица null("пуста")
                throw new NullMatrixException("Входная матрица не существует");

            int cnt = 0; //счётчик этапов предобработки
            do
            {
                Debug.WriteLine("Вход в цикл преобработки!");
                cnt = 0;
                for (int i = 0; i < parMatrix.GetLength(1); i++)
                    for (int j = 0; j < parMatrix.GetLength(1); j++)
                    {
                        if ((i != j) && (parMatrix[i, j] == 1)) //если элемент равен 1
                        {
                            for (int k = 0; k < parMatrix.GetLength(1); k++) //то делается проходка по столбцу позиции этого элемента
                            {
                                if ((j != k) && (parMatrix[j, k] == 1)) //если элемент иденкс строки которого равен индексу столбца элемента из начала и он равен 1
                                {
                                    parMatrix[i, j] = 0; //устанровить ноль на позицию начального элемента
                                    parMatrix[j, k] = 0; //устанровить ноль на позицию конечного элемента
                                    parMatrix[i, k] = 1; //устаноить единицу между этими элементами
                                    cnt++; //увеличить счётчик этапов предобработки
                                }
                            }
                        }
                    }
                //пока есть в счётчике предобработки есть изменения(т.е. не равне нулю), то нужно будет проводить предобработку
                //если в результате предобработки счётчик останеться со значением ноль, то предобработка больше не нужна
            } while (cnt != 0); //пока предобработка нужна
            return parMatrix;
        }
    }
}
