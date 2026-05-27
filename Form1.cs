using gestionDeArticulos.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            listarGrilla();
            cargarCombos();
        }
        private void listarGrilla()
        {
            NegocioArticulo negocio = new NegocioArticulo();
            dgvArticulos.DataSource = negocio.listar();
            dgvArticulos.Columns[4].Visible = false;
            dgvArticulos.Columns[5].Visible = false;
            dgvArticulos.Columns[6].Visible = false;
        }

        private void dgvArticulos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvArticulos.Rows[e.RowIndex];
                string id = fila.Cells[0].Value.ToString();
                NegocioImagenes negocioImg= new NegocioImagenes();
                

                    lista=negocioImg.getImagenes(id);

                    if (lista.Count > 0)
                    {
                        
                        pcbImagen.Load(lista[0]);
                    }
                    else
                    {
                        pcbImagen.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRcmPRR4qsDv1v88EIVeYhkD80lS-c_1-5ceQ&s");
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
                if (indiceActual < 0)
                {
                    indiceActual = lista.Count - 1;
                }
                pcbImagen.Load(lista[indiceActual]);

            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Agregar_Editar agregar = new Agregar_Editar();
            agregar.ShowDialog();

            // refrescar grilla
            listarGrilla();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                articulos seleccionado = (articulos)dgvArticulos.CurrentRow.DataBoundItem;
                Agregar_Editar editar = new Agregar_Editar(seleccionado);
                editar.ShowDialog();

                listarGrilla();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            articulos seleccionado = (articulos)dgvArticulos.CurrentRow.DataBoundItem;
            NegocioArticulo Neg=new NegocioArticulo();

            Neg.eliminarArticulo(seleccionado.idArticulos);
            MessageBox.Show("Producto eliminado correctamente");
            listarGrilla();

        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                articulos seleccionado = (articulos)dgvArticulos.CurrentRow.DataBoundItem;
                Detalle detalle = new Detalle(seleccionado);
                detalle.ShowDialog();

                listarGrilla();
            }
        }

        private void txtNombreFiltro_TextChanged(object sender, EventArgs e)
        {
            NegocioArticulo neg = new NegocioArticulo();
            dgvArticulos.DataSource=neg.listarXFiltroNombre(txtNombreFiltro.Text);


        }
        private void cargarCombos()
        {
            NegocioArticulo neArt = new NegocioArticulo();
            DataTable dtMarca = neArt.rellenarCbMarca();
            DataRow filaDefault = dtMarca.NewRow();
            filaDefault["Id"] = 0;
            filaDefault["Descripcion"] = "Elegir Marca";
            dtMarca.Rows.InsertAt(filaDefault,0);
            cbMarcaFiltro.DataSource = dtMarca;
            cbMarcaFiltro.ValueMember = "Id";
            cbMarcaFiltro.DisplayMember = "Descripcion";

            DataTable dtCategoria = neArt.rellenarCbCategoria();
            DataRow filaDefaultC = dtCategoria.NewRow();
            filaDefaultC["Id"] = 0;
            filaDefaultC["Descripcion"] = "Elegir Categoria";
            dtCategoria.Rows.InsertAt(filaDefaultC, 0);
            cbCategoriaFiltro.DataSource = dtCategoria;
            cbCategoriaFiltro.ValueMember = "Id";
            cbCategoriaFiltro.DisplayMember = "Descripcion";
        }

        private void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            listarGrilla();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            int idMarca = int.Parse (cbMarcaFiltro.SelectedValue.ToString());
            int idCategoria= int.Parse(cbCategoriaFiltro.SelectedValue.ToString());
            if (idMarca == 0 && idCategoria == 0) {

                MessageBox.Show("DEBE ELEGIR AL MENOS UNA MARCA O UNA CATEGORIA");
            }
            else
            {
                NegocioArticulo neg= new NegocioArticulo();
                dgvArticulos.DataSource = neg.listarXMarcaXCategoria(idMarca,idCategoria);
            }

        }
    }
    }