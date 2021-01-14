using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_ConnectionDB.Entities
{
    public class Marcas
    {
        //Se agregó As A Link, de esta forma, si realizo una modificación en cualquiera de los 2 proyectos, se va a ver reflenjado en el otro
        [System.ComponentModel.DataAnnotations.Key]
        public int ID { get; set; }

        public string Nombre { get; set; }
    }
}
