using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using controlador;

namespace aplicacion
{
    public partial class frmAltaPokemon : Form
    {
        
        AccesoDatos datos = new AccesoDatos();
        PokemonNegocio pokemonNegocio = new PokemonNegocio();
        Pokemon pokemon = null;

        public frmAltaPokemon()
        {
            InitializeComponent();
        }
        public frmAltaPokemon(Pokemon pokemon)
        {//Sobreescribo el constructor que recibe un pokemon a modificar
            InitializeComponent();
            this.pokemon = pokemon;
            Text = "Modificar Pokemon";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {   //Si el pokemon no existe,lo crea
                if(pokemon == null)
                   pokemon = new Pokemon();
                
                //Si ya existe,sobreescribe los datos
                pokemon.Numero = int.Parse(textNumero.Text);
                pokemon.Nombre = textNombre.Text;
                pokemon.Descripcion = textDescripcion.Text;
                pokemon.Tipo = (Elemento)comboBoxTipo.SelectedItem;
                pokemon.Debilidad = (Elemento)comboBoxDebilidad.SelectedItem;

                if(pokemon.Id != 0)
                {
                    pokemonNegocio.modificarPokemon(pokemon);
                    MessageBox.Show("Modificado correctamente");
                    
                }
                else
                {
                    pokemonNegocio.agregarPokemon(pokemon);
                    MessageBox.Show("Agregado correctamente");
                    
                }

                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaPokemon_Load(object sender, EventArgs e)
        {
            ElementoNegocio elementoNegocio = new ElementoNegocio();
            
            comboBoxTipo.DataSource = elementoNegocio.getElementos();
            comboBoxTipo.ValueMember = "IdElemento";
            comboBoxTipo.DisplayMember = "Descripcion";
            comboBoxDebilidad.DataSource = elementoNegocio.getElementos();
            comboBoxDebilidad.ValueMember = "IdElemento";
            comboBoxDebilidad.DisplayMember = "Descripcion";


            if (pokemon != null)
            {
                textNumero.Text = pokemon.Numero.ToString();
                textNombre.Text = pokemon.Nombre;
                textDescripcion.Text = pokemon.Descripcion;
                textUrlImagen.Text = pokemon.UrlImagen;
                cargarImagen(pokemon.UrlImagen);
                comboBoxTipo.SelectedValue = pokemon.Tipo.IdElemento;
                comboBoxDebilidad.SelectedValue = pokemon.Debilidad.IdElemento;
            }
        }

        //Cuando salgo del input de la URL me carga la imagen
        private void textUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(textUrlImagen.Text);
        }
        void cargarImagen(string urlImagen)
        {
            try
            {
                pictureBoxPokemon.Load(urlImagen);
            }
            catch (Exception)
            {

                pictureBoxPokemon.Load("https://t3.ftcdn.net/jpg/02/48/42/64/360_F_248426448_NVKLywWqArG2ADUxDq6QprtIzsF82dMF.jpg");
            }
        }
    }
}
