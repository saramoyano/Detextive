using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detextive.Logica
{
    public class LogicaProyecto
    {


        private static LogicaProyecto logicaProyecto;

        public static LogicaProyecto GetInstance()
        {
            if (logicaProyecto == null)
            {
                logicaProyecto = new LogicaProyecto();
            }
            return logicaProyecto;
        }




        //        using (var db = new Model.Context())
        //{
        //    var blogs = db.
        //        .Where(b => b.Rating > 3)
        //        .OrderBy(b => b.Url)
        //        .ToList();
        //}

        //using (var db = new Model.Context())
        //{
        //    var blog = new Blog { Url = "http://sample.com" };
        //db.Blogs.Add(blog);
        //    db.SaveChanges();
        //}
    }
}
