using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace controlador
{
    public class PokemonNegocio
    {
        AccesoDatos datos = new AccesoDatos();
        List<Pokemon> listaPokemon = new List<Pokemon>();

        public List<Pokemon> getPokemon()
        {
           

            try
            {
                datos.setQuery(@"SELECT p.id,numero,nombre,p.descripcion,urlImagen,e.descripcion as Tipo, d.descripcion as Debilidad,idTipo,IdDebilidad 
                                 from POKEMONS as p
                                 inner join ELEMENTOS e 
                                 on ( p.IdTipo = e.Id)
                                 inner join elementos d 
                                 on(p.IdDebilidad = d.Id)");

                datos.abrirConexion();

                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)datos.Lector["id"];
                    aux.Numero = (int)datos.Lector["numero"];
                    aux.Nombre = (string)datos.Lector["nombre"];
                    aux.Descripcion = (string)datos.Lector["descripcion"];

                    //Si los datos son nulos no los leo
                    if(!(datos.Lector["urlImagen"] is DBNull))
                    aux.UrlImagen = (string)datos.Lector["urlImagen"];
                    aux.Tipo = new Elemento();
                    aux.Tipo.IdElemento = (int)datos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.IdElemento = (int)datos.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string) datos.Lector["Debilidad"];

                    listaPokemon.Add(aux);
                }

                return listaPokemon;
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

        public void agregarPokemon(Pokemon pokemon)
        {

            try
            {
                datos.setQuery("INSERT INTO POKEMONS (numero,nombre,descripcion,idTipo,IdDebilidad) values (" + pokemon.Numero + ",'" + pokemon.Nombre + "','" + pokemon.Descripcion + "',@IdTipo,@IdDebilidad)");
                datos.setVariables("@IdTipo", pokemon.Tipo.IdElemento);
                datos.setVariables("@IdDebilidad", pokemon.Debilidad.IdElemento);
                datos.commitConsulta();
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

        public void modificarPokemon(Pokemon pokemon)
        {
            try
            {
                datos.setQuery("UPDATE POKEMONS set numero = @numero,nombre=@nombre,descripcion = @descripcion,urlImagen = @urlimagen,IdTipo = @IdTipo,IdDebilidad=@IdDebilidad WHERE id = @id");
                datos.setVariables("@numero", pokemon.Numero);
                datos.setVariables("@nombre", pokemon.Nombre);
                datos.setVariables("@descripcion", pokemon.Descripcion);
                datos.setVariables("@urlimagen", pokemon.UrlImagen);
                datos.setVariables("@IdTipo", pokemon.Tipo.IdElemento);
                datos.setVariables("@IdDebilidad", pokemon.Debilidad.IdElemento);
                datos.setVariables("@Id", pokemon.Id);
                datos.commitConsulta();
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
