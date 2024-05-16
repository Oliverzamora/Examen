using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTNCloudComputing.Deber
{
    public class Proyectos_Asignados
    {
        public int Id { get; set; }
        public string? Nombre_Proyecto { get; set; }
        public int Horas_Asignadas { get; set; }
        public DateTime Fecha_Asignacion { get; set; }
        public int? EmpleadosId { get; set; }
        public Empleados? Empleados { get; set; }



    }
}