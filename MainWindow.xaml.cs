/* 
 * Peter McEwen
 * May 24, 2019
 * Finds zeros of parabola, and graphs it
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace U5_Quadratics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Line[] Parabola;
        Ellipse pZero1;
        Ellipse pZero2;
        bool HasRun = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnQuadratics_Click(object sender, RoutedEventArgs e)
        {
            if (HasRun)
            {
                for (int i = 0; i < Parabola.Length; i++)
                {
                    canvas.Children.Remove(Parabola[i]);
                }
                canvas.Children.Remove(pZero1);
                canvas.Children.Remove(pZero2);
            }
            Parabola = new Line[100];

            string tempAvalue = txtAvalue.Text.Substring(19);
            string tempBvalue = txtBvalue.Text.Substring(19);
            string tempCvalue = txtCvalue.Text.Substring(19);
            double.TryParse(tempAvalue, out double Avalue);
            double.TryParse(tempBvalue, out double Bvalue);
            double.TryParse(tempCvalue, out double Cvalue);

            double discriminant = (Bvalue * Bvalue) - (4 * Avalue * Cvalue);
            double Zero1 = 0;
            double Zero2 = 0;

            if (discriminant >= 0)
            {
                Zero1 = ((-1 * Bvalue) + Math.Sqrt(discriminant)) / (2 * Avalue);
                Zero2 = ((-1 * Bvalue) - Math.Sqrt(discriminant)) / (2 * Avalue);
                if (Zero1 == Zero2)
                {
                    lblOutput.Content = "The only zero is " + Zero1 
                        + Environment.NewLine + "(Each line is 5 units)";
                }
                else
                {
                    lblOutput.Content = "The roots are " + Zero1 + " and " + Zero2 
                        + Environment.NewLine + "(Each line is 5 units)";
                }

                pZero1 = new Ellipse();
                pZero1.Fill = Brushes.Black;
                pZero1.Width = 8;
                pZero1.Height = 8;
                Canvas.SetTop(pZero1, 176);
                Canvas.SetLeft(pZero1, Zero1 * 2 + 296);
                canvas.Children.Add(pZero1);

                pZero2 = new Ellipse();
                pZero2.Fill = Brushes.Black;
                pZero2.Width = 8;
                pZero2.Height = 8;
                Canvas.SetTop(pZero2, 176);
                Canvas.SetLeft(pZero2, Zero2 * 2 + 296);
                canvas.Children.Add(pZero2);

                double previousX, previousY;
                double currentX = 200;
                double currentY = -1 * (Avalue * (-10 * -10) + Bvalue * -10 + Cvalue) + 180;
                for (int i = 1; i < 100; i++)
                {
                    int XgraphPos = -50 + i;
                    previousX = currentX;
                    previousY = currentY;
                    currentX = i * 2 + 200;
                    currentY = -1 * (Avalue * (XgraphPos * XgraphPos) + Bvalue * XgraphPos + Cvalue) + 180;
                    if (previousY > 80 && currentY > 80 && currentX > 200 && previousX > 200 &&
                        previousY < 280 && currentY < 280 && currentX < 400 && previousY < 400)
                    {
                        Parabola[i] = DrawLine(currentX, currentY, previousX, previousY);
                        canvas.Children.Add(Parabola[i]);
                    }
                }
            }
            else
            {
                lblOutput.Content = "There are no zeros in this equation";
            }

            for (int i = 0; i < 21; i++)
            {
                canvas.Children.Add(DrawLine(i * 10 + 200, 80, i * 10 + 200, 280));
                canvas.Children.Add(DrawLine(200, i * 10 + 80, 400, i * 10 + 80));
            }
            HasRun = true;
        }

        private Line DrawLine (double x1, double y1, double x2, double y2)
        {
            Line l = new Line();
            l.X1 = x1;
            l.Y1 = y1;
            l.X2 = x2;
            l.Y2 = y2;
            l.Stroke = Brushes.Black;
            if (l.X1 == 300 && l.X2 == 300|| l.Y1 == 180 && l.Y2 == 180)
            {
                l.StrokeThickness = 2;
            }
            else
            {
                l.StrokeThickness = 1;
            }
            
            return l;
        }
    }
}
