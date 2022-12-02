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
    public partial class frmPokemon : Form
    {

        List<Pokemon> list = new List<Pokemon>();
        
        public frmPokemon()
        {
            InitializeComponent();
        }

        private void frmPokemon_Load(object sender, EventArgs e)
        {
            cargarPokemones();
        }

        //Cargar pokemones
        private void cargarPokemones()
        {
            PokemonNegocio pokemonNegocio = new PokemonNegocio();
            list = pokemonNegocio.getPokemon();
            dgvPokemon.DataSource = list;
            dgvPokemon.Columns["UrlImagen"].Visible = false;
            dgvPokemon.Columns["Id"].Visible = false;
            cargarImagen(list[0].UrlImagen);
        }

        private void dgvPokemon_SelectionChanged(object sender, EventArgs e)
        {
            Pokemon pokemon = (Pokemon)dgvPokemon.CurrentRow.DataBoundItem;

            cargarImagen(pokemon.UrlImagen);
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaPokemon frmAltaPokemon = new frmAltaPokemon();

            frmAltaPokemon.ShowDialog();
            cargarPokemones();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Pokemon seleccionado = (Pokemon)dgvPokemon.CurrentRow.DataBoundItem; 

            frmAltaPokemon frmModificarPokemon = new frmAltaPokemon(seleccionado);
            frmModificarPokemon.ShowDialog();
            cargarPokemones();
        }
    }
}
