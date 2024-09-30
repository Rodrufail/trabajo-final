using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;
using ConexionDB;
using System.Security.Cryptography.X509Certificates;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar() 
        
        { 
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, ImagenUrl, Precio from ARTICULOS A, CATEGORIAS C, MARCAS M where A.IdMarca = M.Id AND A.IdCategoria = C.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    
                    Articulo aux = new Articulo();
                    aux.Id = datos.Lector.GetInt32(0);
                    if (!(datos.Lector["Codigo"] is DBNull))
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca = new Marca();
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = datos.Lector.GetDecimal(datos.Lector.GetOrdinal("Precio"));

                    lista.Add(aux);
                    
                    
                } 
            
                return lista;  
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
     
        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            Marca marca = new Marca();
            Categoria categoria = new Categoria();
               
            try
            {
               if (nuevo != null) 
                {
                datos.setearConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values (@codigo, @nombre, @descripcion, @IdMarca, @IdCategoria, @ImagenUrl, @precio)");
                datos.setearParametros("@Codigo", nuevo.Codigo);
                datos.setearParametros("@nombre", nuevo.Nombre);
                datos.setearParametros("descripcion", nuevo.Descripcion);
                datos.setearParametros("@IdMarca", nuevo.Marca.Id);
                datos.setearParametros("@IdCategoria", nuevo.Categoria.Id);
                datos.setearParametros("@ImagenUrl", nuevo.ImagenUrl);
                datos.setearParametros("@precio", nuevo.Precio);
                datos.ejecutarAccion();

                
                }
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

        public void modificar (Articulo modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @cod, Nombre = @nombre, Descripcion = @desc, IdMarca = @idMarca, IdCategoria = @cate, ImagenUrl = @img, Precio = @precio where id = @id");
                datos.setearParametros("@cod", modificar.Codigo);
                datos.setearParametros("@nombre", modificar.Nombre);
                datos.setearParametros("@desc", modificar.Descripcion);
                datos.setearParametros("@idMarca", modificar.Marca.Id);
                datos.setearParametros("@cate", modificar.Categoria.Id);
                datos.setearParametros("@img", modificar.ImagenUrl);
                datos.setearParametros("@precio",modificar.Precio);
                datos.setearParametros("@id", modificar.Id);

                datos.ejecutarAccion();
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
        public void eliminar (int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from ARTICULOS where id = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, ImagenUrl, Precio from ARTICULOS A, CATEGORIAS C, MARCAS M where A.IdMarca = M.Id AND A.IdCategoria = C.Id AND ";
                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Precio > " + filtro;
                        break;
                        case "Menor a":
                            consulta += "Precio < " + filtro;
                        break;
                        case "Igual a":
                            consulta += "Precio = " + filtro;
                        break;
                    }

                }
                else if (campo == "Nombre")
                  {  
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Nombre like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "Nombre like '%" + filtro + "'";
                            break;
                        case "Contiene":
                            consulta += "Nombre like '%" + filtro + "%'";
                            break;
                    }
                }else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "M.Descripcion like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "M.Descripcion like '%" + filtro + "'";
                            break;
                        case "Contiene":
                            consulta += "M.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {

                    Articulo aux = new Articulo();
                    aux.Id = datos.Lector.GetInt32(0);
                    if (!(datos.Lector["Codigo"] is DBNull))
                        aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca = new Marca();
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = datos.Lector.GetDecimal(datos.Lector.GetOrdinal("Precio"));

                    lista.Add(aux);


                }
                return lista;
              }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
