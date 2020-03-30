using Mechanic_Motors.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanic_Motors.ServiciosBD
{
    class BDServicios
    {

        private static readonly MechanicMotorsEntities _contexto;

        // Cargamos en el programa las tablas
        static BDServicios()
        {
            _contexto = new MechanicMotorsEntities();
            _contexto.Reparaciones.Load();
            _contexto.Almacen.Load();
        }

        // Obtenemos todos los registros de la base de datos
        public static ObservableCollection<Modelo.Reparacion> GetReparaciones()
        {
            return _contexto.Reparaciones.Local;
        }

        public static ObservableCollection<Modelo.Almacen> GetAlmacen()
        {
            return _contexto.Almacen.Local;
        }

    }
}
