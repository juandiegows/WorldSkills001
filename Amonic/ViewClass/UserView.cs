using Amonic.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amonic.ViewClass
{
    class UserView
    {
        public UserView(Users x)
        {
            ID = x.ID;
            Activo = x.Active;
            Apellido = x.LastName;
            Correo = x.Email;
            if (x.Birthdate != null)
                Edad = DateTime.Now.Year - x.Birthdate.Value.Year;
            Nombre = x.FirstName;
            Oficina = x.Offices.Title;
            Rol = x.Roles.Title;
        }
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Nullable<int> Edad { get; set; }
        public string Rol { get; set; }
        [DisplayName("Dirrección de correo")]
        public string Correo { get; set; }
        public string Oficina { get; set; }
        public Nullable<int> Activo { get; set; }
    }
}
