using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;
using ConexionDB;

namespace presentacion
{
    public partial class frmVerDetalles : Form
    {
        private Articulo articuloSeleccionado;

        public frmVerDetalles(Articulo articulo)
        {
            InitializeComponent();
            articuloSeleccionado = articulo;
            cargarDetalles(); 
        }
        //public frmVerDetalles()
        //{
        //    InitializeComponent();

                  

        //}
        private void cargarDetalles()
        {
            if (articuloSeleccionado != null)
            {
                txtId.Text = articuloSeleccionado.Id.ToString();
                txtCodigo.Text = articuloSeleccionado.Codigo;
                txtNombre.Text = articuloSeleccionado.Nombre;
                txtDescripcion.Text = articuloSeleccionado.Descripcion;
                txtMarca.Text = articuloSeleccionado.Marca.Descripcion;
                txtCategoria.Text = articuloSeleccionado.Categoria.Descripcion;
                txtPrecio.Text = articuloSeleccionado.Precio.ToString();
            }
        }


    }
}
