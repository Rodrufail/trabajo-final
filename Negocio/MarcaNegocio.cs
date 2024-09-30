using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using ConexionDB;

namespace Negocio
{
    public class MarcaNegocio
    {

        public List<Marca> listar()
        {

            List<Marca> listaMarca = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Id, Descripcion from MARCAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = datos.Lector.GetInt32(0);
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    listaMarca.Add(aux);

                }

            return listaMarca;

            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            { 
                datos.cerrarConexion();
            
            }




        }
    }
}
