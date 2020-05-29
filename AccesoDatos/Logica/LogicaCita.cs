using AccesoDatos.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AccesoDatos.Logica
{
    public class LogicaCita
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

        // Obtener la lista de Citas
        // params: ninguno
        public ObservableCollection<Model.Cita> ListaCitas()
        {
            ObservableCollection<Cita> citas = new ObservableCollection<Cita>();
            using (var db = new Model.Context())
            {
                try
                {
                    List<Cita> lCitas = db.CitaSet.OrderBy(b => b.Id).ToList();
                    foreach (Cita cita in lCitas)
                    {
                        citas.Add(cita);
                    }
                    return citas;
                }
                catch (Exception e)
                {
                    throw new Exception("No hay citas guardadas.", e);
                }

            }

        }


        // Obtener una Cita de la lista según filtro aplicado 
        // params: objeto de la clase AccesoDatos.Model.Etiqueta
        public ObservableCollection<Model.Cita> ListaCitasFiltro(Etiqueta eti)
        {
            ObservableCollection<Cita> citas = new ObservableCollection<Cita>();
            using (var db = new Model.Context())
            {                
                try
                {
                    List<Cita> lCitas = db.CitaSet.Where(b => b.IdEtiqueta.Equals(eti.Id)).ToList();
                    foreach (Cita c in lCitas)
                    {
                        citas.Add(c);
                    }
                    return citas;
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


        // Actualiza una cita de la lista 
        // params: objeto de la clase AccesoDatos.Model.Cita con los datos actualizados
        public void ActualizarCita(Model.Cita cita)
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


        // Elimina una cita de la lista 
        // params: objeto de la clase AccesoDatos.Model.Cita 
        public void EliminarCita(Model.Cita cita)
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
