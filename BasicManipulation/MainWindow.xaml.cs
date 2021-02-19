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
using System.Collections;
namespace BasicManipulation
{
    public partial class MainWindow : Window
    {
        ArrayList history { get; set; }
        void Window_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            history = new ArrayList();
            e.ManipulationContainer = this;
            e.Handled = true;
        }

        void Window_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            var o = e.ManipulationOrigin;
            history.Add(new KlikHistory(o.X, o.Y));
            Rectangle rectToMove = e.OriginalSource as Rectangle;
            Matrix rectsMatrix = ((MatrixTransform)rectToMove.RenderTransform).Matrix;
            rectsMatrix.RotateAt(e.DeltaManipulation.Rotation,
                                 e.ManipulationOrigin.X,
                                 e.ManipulationOrigin.Y);
            rectsMatrix.ScaleAt(e.DeltaManipulation.Scale.X,
                                e.DeltaManipulation.Scale.X,
                                e.ManipulationOrigin.X,
                                e.ManipulationOrigin.Y);
            rectsMatrix.Translate(e.DeltaManipulation.Translation.X,
                                  e.DeltaManipulation.Translation.Y);
            rectToMove.RenderTransform = new MatrixTransform(rectsMatrix);

            Rect containingRect =
                new Rect(((FrameworkElement)e.ManipulationContainer).RenderSize);

            Rect shapeBounds =
                rectToMove.RenderTransform.TransformBounds(
                    new Rect(rectToMove.RenderSize));
            if (e.IsInertial && !containingRect.Contains(shapeBounds))
            {
                e.Complete();
            }

            e.Handled = true;
        }

        void Window_InertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            var o = e.ManipulationOrigin;
            history.Add(new KlikHistory(o.X, o.Y));
            var text = string.Join(",", history.ToArray());
            MessageBoxResult messageBoxResult = MessageBox.Show(text);
            e.Handled = true;
        }
    }
}
