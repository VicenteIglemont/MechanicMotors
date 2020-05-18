using Mechanic_Motors.Modelo;
using Mechanic_Motors.ServiciosBD;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public CollectionViewSource GetCitas()
        {
            CollectionViewSource collection = new CollectionViewSource();
            collection.Source = BDServicios.GetCitas();
            return collection;
        }

        public CollectionViewSource GetDudas()
        {
            CollectionViewSource collection = new CollectionViewSource();
            collection.Source = BDServicios.GetDudas();
            return collection;
        }

        public int GetMaxIdReparacion()
        {

            int maxId = 0;
            ObservableCollection<Reparacion> reparaciones = BDServicios.GetReparaciones();

            foreach(Reparacion r in reparaciones)
            {
                if(r.IdReparacion > maxId)
                {
                    maxId = r.IdReparacion;
                }
            }

            return maxId + 1;
        }

        public int GetMaxIdPieza()
        {
            int maxId = 0;
            ObservableCollection<Pieza> piezas = BDServicios.GetAlmacen();

            foreach(Pieza p in piezas)
            {
                if(p.IdPieza > maxId)
                {
                    maxId = p.IdPieza;
                }
            }

            return maxId + 1;
        }

        public int IncrementarPieza(Pieza piezaIncrementada)
        {
            return BDServicios.IncrementarPieza(piezaIncrementada);
        }

        internal int DecrementarPieza(Pieza piezaDecrementada)
        {
            return BDServicios.DecrementarPieza(piezaDecrementada);
        }
    }
}
