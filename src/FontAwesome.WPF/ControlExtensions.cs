using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace FontAwesome.WPF
{
    /// <summary>
    /// Control extensions
    /// </summary>
    public static class ControlExtensions
    {
        /// <summary>
        /// The key used for storing the spinner Storyboard.
        /// </summary>
        private static readonly string SpinnerStoryBoardName = String.Format("{0}Spinner", typeof(FontAwesome).Name);

        /// <summary>
        /// Start the spinning animation
        /// </summary>
        /// <typeparam name="T">FrameworkElement and ISpinable</typeparam>
        /// <param name="control">Control to apply the rotation </param>
        public static void BeginSpin<T>(this T control)
            where T : FrameworkElement, ISpinable
        {
            if (!(control.RenderTransform is TransformGroup
                      && ((TransformGroup)control.RenderTransform).Children.OfType<RotateTransform>().Any()))
            {
                var transformGroup = new TransformGroup();
                transformGroup.Children.Add(new RotateTransform(0.0));
                control.RenderTransform = transformGroup;
                control.RenderTransformOrigin = new Point(0.5, 0.5);
            }

            var storyboard = new Storyboard();

            var animation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                AutoReverse = false,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new Duration(TimeSpan.FromSeconds(Awesome.GetSpinDuration(control)))
            };
            storyboard.Children.Add(animation);

            Storyboard.SetTarget(animation, control);
            Storyboard.SetTargetProperty(animation,
                new PropertyPath("(0).(1)[0].(2)", UIElement.RenderTransformProperty,
                    TransformGroup.ChildrenProperty, RotateTransform.AngleProperty));

            storyboard.Begin();
            control.Resources.Add(SpinnerStoryBoardName, storyboard);
            
        }
        /// <summary>
        /// Stop the spinning animation 
        /// </summary>
        /// <typeparam name="T">FrameworkElement and ISpinable</typeparam>
        /// <param name="control">Control to stop the rotation.</param>
        public static void StopSpin<T>(this T control)
            where T : FrameworkElement, ISpinable
        {
            var storyboard = control.Resources[SpinnerStoryBoardName] as Storyboard;

            if (storyboard == null) return;

            storyboard.Stop();

            control.Resources.Remove(SpinnerStoryBoardName);
        }

    }
}
