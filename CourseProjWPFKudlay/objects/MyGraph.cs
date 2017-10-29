using System.Windows.Controls;
using CourseProjWPFKudlay.exceptions;
using Microsoft.Msagl.Drawing;
using System.Data;

namespace CourseProjWPFKudlay.objects
{
    class MyGraph
    {
        private int[,] initialMatrix; //начальная матрица графа
        public int[,] InitialMatrix { get => initialMatrix; set => initialMatrix = value; }

        private int[,] preprocessedMatrix; //предобработанная матрица графа
        public int[,] PreprocessedMatrix { get => preprocessedMatrix; set => preprocessedMatrix = value; }

        //печать матрицы (массив матрицы, куда печатать)
        public void printMatrix(int[,] inputMatrix, TextBox textBoxOutput) 
        {
            if (inputMatrix == null) //если печатаемой матрицы нету
                throw new NullMatrixException("Матрица не может быть отображена так как в представлении допущена ошибка!");

            textBoxOutput.Clear();
            for (int i = 0; i < inputMatrix.GetLength(1); i++) 
            {
                for (int j = 0; j < inputMatrix.GetLength(1); j++)
                {
                    textBoxOutput.AppendText(" " + inputMatrix[i, j].ToString() + " ");
                }
                textBoxOutput.AppendText("\n");
            }
        }

        public void printMatrixToDG(int[,] inputMatrix, DataGrid dg)
        {
            var dataTable = new DataTable();

            for (int i = 0; i < inputMatrix.GetLength(1) + 1; i++)
            {
                dataTable.Columns.Add(new DataColumn(i.ToString()));
            }

            var row = dataTable.NewRow();

            for (int i = 0; i < inputMatrix.GetLength(1); i++)
            {
                row[i + 1] = "X" + (i + 1);
            }

            dataTable.Rows.Add(row);

            for (int i = 0; i < inputMatrix.GetLength(1); i++)
            {
                row = dataTable.NewRow();
                row[0] = "X" + (i + 1);
                for (int j = 1; j < inputMatrix.GetLength(1) + 1; j++)
                {
                    row[j] = inputMatrix[i, j - 1];
                }
                dataTable.Rows.Add(row);
            }
            dg.ItemsSource = dataTable.DefaultView;
        }

        //метод рисования графа используя библиотеку MS AGL с помощью метода AddEdge
        public void drawGraph(int[,] inputMatrix, Graph graphOut)
        {
            int countNode;
            for (int i = 0; i < inputMatrix.GetLength(1); i++)
            {
                countNode = 0;
                for (int j = 0; j < inputMatrix.GetLength(1); j++)
                {
                    if (inputMatrix[i,j] == 1)
                    {
                        graphOut.AddEdge((i + 1).ToString(), (j + 1).ToString()); //к значению номера вершины добавляеться единица, чтобы счёт вершин шёл с единицы
                        countNode++;
                    }
                }
                if (countNode == 0)
                    graphOut.AddNode((i + 1).ToString());
            }
        }


    }
}
