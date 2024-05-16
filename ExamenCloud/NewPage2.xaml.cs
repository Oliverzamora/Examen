using MauizAppUtn.Models;
using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using UTNCloudComputing.Deber;

namespace MauizAppUtn
{
    public partial class NewPage2 : ContentPage
    {
        private string ApiUrl2 = "https://utncloudapi20240516123819.azurewebsites.net/api/Empleados";

        public NewPage2()
        {
            InitializeComponent();
        }

        private async void cmdCreate_Clicked(object sender, EventArgs e)
        {
            try
            {
                var resultado = APIConsumer.CrudCrud<Empleados>.Create(ApiUrl2, new Empleados
                {
                    Id = 0,
                    Nombre = txtClasificacion.Text,
                    Cargo = txtCargo.Text,
                    Salario=float.Parse(txtSalario.Text),
                    Departamento=txtDepartamento.Text,


                });

                if (resultado != null)
                {
                    txtId.Text = resultado.Id.ToString();
                    await DisplayAlert("Éxito", "Empleado creada con éxito", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }

        private async void cmdRead_Clicked(object sender, EventArgs e)
        {
            try
            {
                var resultado = APIConsumer.CrudCrud<Empleados>.Read_ById(ApiUrl2, int.Parse(txtId.Text));
                if (resultado != null)
                {
                    txtId.Text = resultado.Id.ToString();
                    txtClasificacion.Text = resultado.Nombre;
                    txtCargo.Text = resultado.Cargo;
                    txtSalario.Text=resultado.Salario.ToString();
                    txtDepartamento.Text = resultado.Departamento;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }

        private async void cmdUpdate_Clicked(object sender, EventArgs e)
        {
            try
            {
                APIConsumer.CrudCrud<Empleados>.Update(ApiUrl2, int.Parse(txtId.Text), new Empleados
                {
                    Id = int.Parse(txtId.Text),
                    Nombre = txtClasificacion.Text,
                    Cargo = txtCargo.Text,
                    Salario = float.Parse(txtSalario.Text),
                    Departamento = txtDepartamento.Text,
                });
                await DisplayAlert("Éxito", "Empleado actualizada con éxito", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }

        private async void cmdDelete_Clicked(object sender, EventArgs e)
        {
            try
            {
                APIConsumer.CrudCrud<Empleados>.Delete(ApiUrl2, int.Parse(txtId.Text));
                txtId.Text = "";
                txtClasificacion.Text = "";
                txtCargo.Text = "";
                txtSalario.Text = "";
                txtDepartamento.Text = "";
                await DisplayAlert("Éxito", "Empleados eliminada con éxito", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }
    }
}
