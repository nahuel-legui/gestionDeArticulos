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
                        pcbImagen.Load("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAARMAAAC3CAMAAAAGjUrGAAAAMFBMVEXx8/XCy9K/yND09vfw8vTP1tzp7O/i5ure4+fO1dvJ0dfT2d/EzNPt7/Lb4OXo6+4FeM7UAAAFL0lEQVR4nO2c24KrIAxFLdha7///t0dxOlWDSiAKztnrbR4G6SoJBKHZA6zJYncgQeCEAicUOKHACQVOKHBCgRMKnFDghAInFDihwAkFTihwQoETCpxQ4IQCJxQ4ocAJBU4ocEKBEwqcUOCEAicUOKHACQVOKHBCgRMKnFDghAInFDihwAkFTihwQoETCpxQ4IQCJxQ4ocAJBU4ot3Oi1KMq64FnWTVq+EueWzlRquqKVn/J+/ezEfdyHydKPYtc62yF1m1Xymq5ixPVdDnx8eslf1eCVu7hRFXFppAfLW39kNJyByeqOTJirGTvRsbKDZyozsHIpKUQsZK8E1Vu55GTrKTuRL0ZRoyVLviZaTtRVctUMuaVOnCoJO1E1WwjxsorbGZO2Qk7br5WuhApKTvpfZWMy5WAoZKuk6b1NhI4VJJ10uRBSsas0ng+OlUnVaARw9NvqCTqRERJpt9eUtJ0IqPEN36SdNIIKRnIPeafFJ0Ep9c5mr+qTdFJ2CRMpLAn5fScqJeokrFWZkoRdaImwtpw2T9iSnnxuiDoRFXda6hK28JzWTA14ryBxKFlTT9iTlT1W57o3Lta96yED8krRieknCw/DDuEP1TnKBlgzMlCTtZDXr+8pIjOwitK5x7JOKFD3mukiE85ix45S5FxYll46prdiv8ekpsU19wv4kS9LV1ouQPlrPzKliIzTuw9YDYiVfgFSxFx8rR+wcyMomSX9HYpTjlFwonqrB3gBc/JyYQjRcRJYe8Ay4l9rMlLcVi8iTjp7Y/nOBHcMjngWEoi4+TUlcmKw9rnxHzCWMqeU/ltkB9JEZl3SusnYmwQn1fm2GgPeiOzZrM9WZfu/3/BNDznYATLOLENffep+JppeMZBMSZUF9N6ljFM7KF3qpTduBZyQj4W53XTiRsEm1L2dr2k9k9W9Rtjq2BrJj9Zyk7pI7bP9lw8kfH+4KIFLGF77Sa3R90Un0POvHNCcYzsLVMk9+2buni1bd9xjMSJHMPmjCz7zov/fidW5GQ7OS/2e8BoRrLtrBfXScTIMVLsk09cJxEjZ8I6+cR1EmG1tsRaDsZ0EjlyDL0leuxOpulD4JTALtfXORRbnqVO1LDOePdtpoclWPsqulL+wt0P0SNnxFKrrp2opmuXl+5OuHA3PSmByDGQ9ezSydYdM+ELd4YUIsdANnoWTva2RSUv3JlnJRE5I2RbY+6kee1+dTrrhC7cPTZeMUdivZnydaIc3tdqqWuI6USOYZlSfp0oxzVlJxNByUSOYZlSPk6cDzqEXy17JDTn/LBMKRlTSRZ4X2giep2zZnEwZHLiGjifFt6BTtKKHMMspUxO2BkvDzoDm1jkGGa7bsaJx0t9XfgrOfuMlhezwsc48RrKufvhyiXXHatg8T2Zkm0eHzluxO8W4pXHKljkXycBt3h9blFdeqyCx2fPOguLbn6qTWsBu+Czxs/CopsdP4kmkx+mcZ8FRrfuWUqSTSYT005keDucW4iXnzRhMg17iYacC6A0VyZzzIQs0pBrUrn22JoXY4Us0pDjaZMzb+dIMX6/Qi0dHSU0XHySz48heqSaOs60vsvlq2mtpzj9OCh/Trgjew7afgLar63d6ec2SmTZm37+UyV7048K+Gmkm7O10A/8aaSbY7sEr8rYvYoNnX4Sr3EuYJVpVc35Ccu/innZbryMJ1n4v9f4N9FZ39XPZ931GYzMGH9VPHYfAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADp8Q9+nG9anuOrfAAAAABJRU5ErkJggg==");
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
    }
}
