using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Logica
{
    class LogicaCita
    {
        private static LogicaCita logicaCita;

        public static LogicaCita GetInstance()
        {
            if (logicaCita == null)
            {
                logicaCita = new LogicaCita();
            }
            return logicaCita;
        }


        public List<Model.Cita> ListaCitas()
        {
            using (var db = new Model.Context())
            {

                try
                {
                    return db.CitaSet.OrderBy(b => b.Id).ToList();
                }
                catch (Exception e)
                {
                    throw new Exception("No hay citas guardadas.", e);
                }

            }

        }

        // Agregar un cita a la lista 
        // params: objeto de la clase AccesoDatos.Model.Cita
        public void AgregarCita(Cita cita)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    db.CitaSet.Add(cita);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("No se ha podido agregar la cita ", e);
                }
            }

        }

        public void ActualizarCita(Cita cita)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    var citaTmp = db.CitaSet.FirstOrDefault(x => x.Id == cita.Id);
                    db.Entry(citaTmp).State = EntityState.Modified;
                    db.Entry(citaTmp).CurrentValues.SetValues(cita);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("No se ha podido actualizar la cita ", e);

                }
            }
        }

        public void EliminarCita(Cita cita)
        {
            using (var db = new Model.Context())
            {
                try
                {
                    var citaTmp = db.CitaSet.FirstOrDefault(x => x.Id == cita.Id);
                    db.CitaSet.Remove(citaTmp);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("No se ha podido eliminar la cita ", e);

                }
            }
        }
    }
}
}
