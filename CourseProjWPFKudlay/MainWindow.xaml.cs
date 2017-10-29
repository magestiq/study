using System.Windows;
using CourseProjWPFKudlay.objects;
using CourseProjWPFKudlay.exceptions;
using Microsoft.Win32;
using System.IO;
using System.Linq;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.Drawing;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System;

namespace CourseProjWPFKudlay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyGraph m1 = new MyGraph(); //экземпляр графа №1
        MyGraph m2 = new MyGraph(); //экземпляр графа №2
        OpenFileDialog ofd = new OpenFileDialog();

        public MainWindow()
        {
            InitializeComponent();
            textBox1_input.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(textBox1_input_PreviewTextInput);
            textBox2_input.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(textBox2_input_PreviewTextInput);
        }

        private void drawInitialGraph(MyGraph mg, DataGrid dataGridOutput, WindowsFormsHost host)
        {
            Graph gr_1 = new Graph(); //создание экземпляра класса для рисования графа
            GViewer gv_1 = new GViewer(); //создание экземпляра класса для просмотра графа
            //mg.printMatrix(mg.InitialMatrix, textBoxOutput); //печать матрицы (начальной)
            mg.printMatrixToDG(mg.InitialMatrix, dataGridOutput);
            mg.drawGraph(mg.InitialMatrix, gr_1); //рисование графа (начального)
            gv_1.ToolBarIsVisible = false; //скрытие тул-бара просмотрщика графа
            gv_1.Graph = gr_1; //присовение компоненте просмотра, какой граф отрисовывать
            host.Child = gv_1;   //присвоение хосту дочернего обьекта просмотрщика графов 
        }

        private void drawPreprocessedGraph(MyGraph mg, WindowsFormsHost host)
        {
            Graph gr_1 = new Graph(); //создание экземпляра класса для рисования графа
            GViewer gv_1 = new GViewer(); //создание экземпляра класса для просмотра графа
            mg.PreprocessedMatrix = Preprocess.doHomeomorphic(mg.InitialMatrix); //проведения предобработки
            mg.drawGraph(mg.PreprocessedMatrix, gr_1); //рисование графа (предобработанного)

            gv_1.ToolBarIsVisible = false; //скрытие тул-бара просмотрщика графа
            gv_1.Graph = gr_1; //присовение компоненте просмотра, какой граф отрисовывать
            host.Child = gv_1;   //присвоение хосту дочернего обьекта просмотрщика графов   
        }

        private void loadFile(TextBox textBox)
        {
            
            ofd.Filter = "grf files (*.grf)|*.grf"; //фильтровать файлы с расширением файла графа
            if (ofd.ShowDialog() == true)//вызов диалога открытия файла
            {
                string fn = ofd.FileName;
                textBox.Text = File.ReadLines(fn).First(); //считываем первую строку из файла
            }
        }

        private void button1_browse_Click(object sender, RoutedEventArgs e)
        {
            loadFile(textBox1_input);
        }

        private void button2_browse_Click(object sender, RoutedEventArgs e)
        {
            loadFile(textBox2_input);
        }

        private void button1_build_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                m1.InitialMatrix = GraphFiInput.fiInput(textBox1_input.Text); //ввод представления в матрицу
            }
            catch (IncorrectInputException ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                drawInitialGraph(m1, dataGridInitial1, graphDraw_1);
            }
            catch (NullMatrixException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_preproc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                drawPreprocessedGraph(m1, graphDrawPre_1);
                m1.printMatrixToDG(m1.PreprocessedMatrix, dataGridPrep1);

            }
            catch (NullMatrixException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_build_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                m2.InitialMatrix = GraphFiInput.fiInput(textBox2_input.Text);
            }
            catch (IncorrectInputException ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
               drawInitialGraph(m2, dataGridInitial2, graphDraw_2);
            }
            catch (NullMatrixException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_preproc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                drawPreprocessedGraph(m2, graphDrawPre_2);
                m2.printMatrixToDG(m2.PreprocessedMatrix, dataGridPrep2);
            }
            catch (NullMatrixException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Comparison.internalStability(m1.PreprocessedMatrix) == Comparison.internalStability(m2.PreprocessedMatrix)) //сравнение матриц
                    MessageBox.Show("Число внутренней устойчивости обеих графов равно. Графы эквивалентны!");
                else MessageBox.Show("Число внутренней устойчивости обеих графов не равно. Графы неэквиваленты!");
            }
            catch (NullMatrixException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (IncorrectInputException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_input_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
                e.Handled = true;
        }

        private void textBox2_input_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
                e.Handled = true;
        }
    }
}