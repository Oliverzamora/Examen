namespace UTNCloudComputing.Deber
{
	public class Empleados
	{
		public int Id {  get; set; }
		public string ?Nombre { get; set; }
		public string ?Cargo { get; set; }
		public float ? Salario { get; set;}
		public string ?Departamento { get; set; }
		public List <Proyectos_Asignados>? Asignados { get; set;}
	}
}
