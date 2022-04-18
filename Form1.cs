using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Filmes
{
    public partial class Form1 : Form
    {
        //DBConnect ligacao = new DBConnect();
        FormListarFilmes formListarFilmes = new FormListarFilmes();
        FormInserirFilme formInserirFilme = new FormInserirFilme();
        FormAlterarFilme formAlterarFilme = new FormAlterarFilme();
        FormEliminarFilme formEliminarFilme = new FormEliminarFilme();



        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (formListarFilmes.IsDisposed)
            {
                formListarFilmes = new FormListarFilmes();
            }

            formListarFilmes.Show();
            formListarFilmes.Activate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (formInserirFilme.IsDisposed)
            {
                formInserirFilme = new FormInserirFilme();
            }

            formInserirFilme.Show();
            formInserirFilme.Activate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (formAlterarFilme.IsDisposed)
            {
                formAlterarFilme = new FormAlterarFilme();
            }

            formAlterarFilme.Show();
            formAlterarFilme.Activate();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (formEliminarFilme.IsDisposed)
            {
                formEliminarFilme = new FormEliminarFilme();
            }

            formEliminarFilme.Show();
            formEliminarFilme.Activate();
        }
    }
}
