using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mechanic_Motors.Vista
{
    public static class ComandosPersonalizados
    {
        public static readonly RoutedUICommand NuevaPiezaCommand = new RoutedUICommand
            (
                "NuevaPiezaCommand",
                "NuevaPiezaCommand",
                typeof(ComandosPersonalizados),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.P, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand NuevaReparacionCommand = new RoutedUICommand
            (
                "NuevaReparacionCommand",
                "NuevaReparacionCommand",
                typeof(ComandosPersonalizados),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.R, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand NuevaFacturaCommand = new RoutedUICommand
            (
                "NuevaFacturaCommand",
                "NuevaFacturaCommand",
                typeof(ComandosPersonalizados),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.F, ModifierKeys.Control)
                }
            );
    }
}
