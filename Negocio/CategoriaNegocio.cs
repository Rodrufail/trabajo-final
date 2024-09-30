using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionDB;
using Dominio;

namespace Negocio
{
    public class CategoriaNegocio
    
    {
       public List<Categoria> listar()
        {
        
            List<Categoria> listaCategoria = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Id, Descripcion from Categorias");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = datos.Lector.GetInt32(0);
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    listaCategoria.Add(aux);
                }

            return listaCategoria;

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
