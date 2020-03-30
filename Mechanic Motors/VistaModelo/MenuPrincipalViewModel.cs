using Mechanic_Motors.ServiciosBD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Mechanic_Motors.VistaModelo
{
    class MenuPrincipalViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CollectionViewSource GetReparaciones()
        {
            CollectionViewSource collection = new CollectionViewSource();
            collection.Source = BDServicios.GetReparaciones();
            return collection;
        }

        public CollectionViewSource GetAlmacen()
        {
            CollectionViewSource collection = new CollectionViewSource();
            collection.Source = BDServicios.GetAlmacen();
            return collection;
        }

    }
}
