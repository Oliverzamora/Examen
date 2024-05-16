using Microsoft.Maui.Controls;
using MauizAppUtn.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UTNCloudComputing.Deber;

namespace MauizAppUtn
{
    public partial class NewPage1 : ContentPage
    {
        private string ApiUrl = "https://utncloudapi20240516123819.azurewebsites.net/api/Proyectos_Asignados";
        private string ApiUrl2 = "https://utncloudapi20240516123819.azurewebsites.net/api/Empleados";
        private List<Empleados> empleados; // Lista para almacenar las clasificaciones

        public NewPage1()
        {
            InitializeComponent();
            CargarClasificaciones(); // Cargar las clasificaciones al iniciar la página
        }

        private async void CargarClasificaciones()
        {
            try
            {
                empleados = APIConsumer.CrudCrud<Empleados>.GetAll(ApiUrl2); // Obtener todas las clasificaciones
                foreach (var empleado in empleados)
                {
                    pickerClasificacion.Items.Add(empleado.Nombre.ToString()); // Agregar la descripción de cada clasificación al Picker
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción si ocurre algún error al cargar las clasificaciones
                Console.WriteLine($"Error al cargar las clasificaciones: {ex.Message}");
            }
        }

        private async void cmdDeleteProd_Clicked(object sender, EventArgs e)
        {
            try
            {
                APIConsumer.CrudCrud<Proyectos_Asignados>.Delete(ApiUrl, int.Parse(txtIdProducto.Text));
                LimpiarCampos();
                await DisplayAlert("Éxito", "Producto eliminado con éxito", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al eliminar el producto: {ex.Message}", "OK");
            }
        }

        private async void cmdUpdateProd_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (pickerClasificacion.SelectedIndex == -1)
                {
                    await DisplayAlert("Error", "Debe seleccionar una clasificación", "OK");
                    return;
                }

                var selectedClasificacion = empleados[pickerClasificacion.SelectedIndex];

                APIConsumer.CrudCrud<Proyectos_Asignados>.Update(ApiUrl, int.Parse(txtIdProducto.Text), new Proyectos_Asignados
                {
                    Id = int.Parse(txtIdProducto.Text),
                    Nombre_Proyecto = txtNombreProducto.Text,
                    Horas_Asignadas = int.Parse(txtExistencia.Text),
                    Fecha_Asignacion = datePickerFechaAsignacion.Date,
                    EmpleadosId = selectedClasificacion.Id // Obtener la ID de la clasificación seleccionada
                });
                await DisplayAlert("Éxito", "Producto actualizado con éxito", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al actualizar el producto: {ex.Message}", "OK");
            }
        }

        private void cmdReadProd_Clicked(object sender, EventArgs e)
        {
            var prod = APIConsumer.CrudCrud<Proyectos_Asignados>.Read_ById(ApiUrl, int.Parse(txtIdProducto.Text));
            if (prod != null)
            {
                MostrarProducto(prod);
            }
        }

        private async void cmdCreateProd_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (pickerClasificacion.SelectedIndex == -1)
                {
                    await DisplayAlert("Error", "Debe seleccionar una clasificación", "OK");
                    return;
                }

                var selectedClasificacion = empleados[pickerClasificacion.SelectedIndex];

                var prod = APIConsumer.CrudCrud<Proyectos_Asignados>.Create(ApiUrl, new Proyectos_Asignados
                {
                    Id = 0,
                    Nombre_Proyecto = txtNombreProducto.Text,
                    Horas_Asignadas = int.Parse(txtExistencia.Text),
                    Fecha_Asignacion = datePickerFechaAsignacion.Date,
                    EmpleadosId = selectedClasificacion.Id // Obtener la ID de la clasificación seleccionada
                });

                if (prod != null)
                {
                    MostrarProducto(prod);
                    await DisplayAlert("Éxito", "Producto creado con éxito", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al crear el producto: {ex.Message}", "OK");
            }
        }

        private void MostrarProducto(Proyectos_Asignados prod)
        {
            txtIdProducto.Text = prod.Id.ToString();
            txtNombreProducto.Text = prod.Nombre_Proyecto;
            txtExistencia.Text = prod.Horas_Asignadas.ToString();
            datePickerFechaAsignacion.Date = prod.Fecha_Asignacion;

            // Seleccionar la clasificación correspondiente en el Picker
            pickerClasificacion.SelectedIndex = empleados.FindIndex(c => c.Id == prod.EmpleadosId);
        }

        private void LimpiarCampos()
        {
            txtIdProducto.Text = "";
            txtNombreProducto.Text = "";
            txtExistencia.Text = "";
            datePickerFechaAsignacion.Date = DateTime.Now;
            pickerClasificacion.SelectedItem = null;
        }
    }
}
