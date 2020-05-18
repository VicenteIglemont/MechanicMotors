using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanic_Motors.Modelo
{
    class ClasesParciales
    {
        public partial class Almacen : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
        }

        public partial class Reparacion : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
        }

    }
}
