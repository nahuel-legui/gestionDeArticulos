using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gestionDeArticulos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int indiceActual = 0;
        public List<string> lista = new List<string>();
        private void Form1_Load(object sender, EventArgs e)
        {
            ElectronicaNegocio negocio= new ElectronicaNegocio();
            dgvArticulos.DataSource = negocio.listar();
        }

        private void dgvArticulos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //RowIndex es el indice  de la fila -1 empiezan los titutlos . 
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvArticulos.Rows[e.RowIndex];
                string id=fila.Cells[0].Value.ToString();
                //tiene q devolver una lista 
               
                //para conectarse a esa conexion
                SqlConnection conexion = new SqlConnection();
                //Esto realiza acciones 
                SqlCommand comando = new SqlCommand();
                //Aca voy a obtener un set de datos y los voy a guardar en lector 
                SqlDataReader lector;

                try
                {
                    //Configuramos la cadena de conexion
                    conexion.ConnectionString = "Server=localhost\\SQLEXPRESS;database=CATALOGO_P3_DB;integrated security=true";
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = "Select ImagenUrl from IMAGENES where IdArticulo=@id";
                    comando.Parameters.AddWithValue("@id", id);
                    comando.Connection = conexion;

                    conexion.Open();
                    lector = comando.ExecuteReader();

                    lista.Clear();
                    while (lector.Read())
                    {
                        string url = lector["ImagenUrl"].ToString();
                        lista.Add(url);

                    }

                    conexion.Close();
                    
                    if(lista.Count > 0)
                    {
                        label2.Text = lista[0];
                        pcbImagen.Load(lista[0]);
                    }
                    else
                    {
                        pcbImagen.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRcmPRR4qsDv1v88EIVeYhkD80lS-c_1-5ceQ&s");
                    }
                    
                }
                catch (Exception ex)
                {
                    //Me permite no que se crashee la app y me diga el error que pueda tener
                    throw ex;
                }

            }
        }

        private void btnAdelante_Click(object sender, EventArgs e)
        {
            if (lista.Count > 0)
            {
                indiceActual++;
                if (indiceActual >= lista.Count)
                {
                    indiceActual = 0;
                }
                pcbImagen.Load(lista[indiceActual]);
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            if (lista.Count > 0)
            {
                indiceActual--;
                if (indiceActual <0)
                {
                    indiceActual=lista.Count-1;
                }
                pcbImagen.Load(lista[indiceActual]);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Agregar_Editar agregar= new Agregar_Editar();
            agregar.ShowDialog();
        }
    }
}
