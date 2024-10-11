using System;
using System.Windows;
using System.Windows.Controls;

namespace ShapeAreaCalculator
{
    // Базовый класс "Фигура"
    public abstract class Shape
    {
        // Абстрактный метод для вычисления площади
        public abstract double CalculateArea();
    }

    // Производный класс "Круг"
    public class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        // Переопределение метода для вычисления площади круга
        public override double CalculateArea()
        {
            return Math.PI * Math.Pow(Radius, 2);
        }
    }

    // Производный класс "Прямоугольник"
    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        // Переопределение метода для вычисления площади прямоугольника
        public override double CalculateArea()
        {
            return Width * Height;
        }
    }

    // Производный класс "Треугольник"
    public class Triangle : Shape
    {
        public double BaseLength { get; set; }
        public double Height { get; set; }

        public Triangle(double baseLength, double height)
        {
            BaseLength = baseLength;
            Height = height;
        }

        // Переопределение метода для вычисления площади треугольника
        public override double CalculateArea()
        {
            return 0.5 * BaseLength * Height;
        }
    }

    // Делегат для динамического вызова метода вычисления площади
    public delegate double AreaDelegate();

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработка события выбора фигуры
        private void ShapeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HideAllPanels(); // Скрыть все панели с параметрами
            var selectedShape = (ComboBoxItem)ShapeComboBox.SelectedItem;

            switch (selectedShape.Content.ToString())
            {
                case "Круг":
                    CirclePanel.Visibility = Visibility.Visible; // Показать панель для круга
                    break;
                case "Прямоугольник":
                    RectanglePanel.Visibility = Visibility.Visible; // Показать панель для прямоугольника
                    break;
                case "Треугольник":
                    TrianglePanel.Visibility = Visibility.Visible; // Показать панель для треугольника
                    break;
            }
        }

        // Скрыть все панели с параметрами
        private void HideAllPanels()
        {
            CirclePanel.Visibility = Visibility.Collapsed;
            RectanglePanel.Visibility = Visibility.Collapsed;
            TrianglePanel.Visibility = Visibility.Collapsed;
        }

        // Обработка события нажатия кнопки "Вычислить площадь"
        private void CalculateAreaButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedShape = (ComboBoxItem)ShapeComboBox.SelectedItem;

            Shape shape = null;
            AreaDelegate areaDelegate = null;

            try
            {
                switch (selectedShape.Content.ToString())
                {
                    case "Круг":
                        double radius = double.Parse(CircleRadiusTextBox.Text);
                        shape = new Circle(radius);
                        break;
                    case "Прямоугольник":
                        double width = double.Parse(RectangleWidthTextBox.Text);
                        double height = double.Parse(RectangleHeightTextBox.Text);
                        shape = new Rectangle(width, height);
                        break;
                    case "Треугольник":
                        double baseLength = double.Parse(TriangleBaseTextBox.Text);
                        double triangleHeight = double.Parse(TriangleHeightTextBox.Text);
                        shape = new Triangle(baseLength, triangleHeight);
                        break;
                }

                if (shape != null)
                {
                    // Использование делегата для вычисления площади
                    areaDelegate = shape.CalculateArea;
                    double area = areaDelegate();
                    ResultTextBlock.Text = $"Площадь: {area}"; // Вывод результата на русском
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message); // Сообщение об ошибке
            }
        }
    }
}