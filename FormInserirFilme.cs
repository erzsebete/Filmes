using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Filmes
{
    public partial class FormInserirFilme : Form
    {
        DBConnect ligacao = new DBConnect();


        public FormInserirFilme()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboGenero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormInserirFilme_Load(object sender, EventArgs e)
        {
            ligacao.PreencherComboGenero(ref comboGenero);
            ligacao.PreencherComboActores(ref comboActores);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (VerificarCampos())
            {
                string id_genero = comboGenero.SelectedItem.ToString().Substring
                (0, comboGenero.SelectedItem.ToString().IndexOf(' '));

                string id_actores = comboActores.SelectedItem.ToString().Substring
                (0, comboActores.SelectedItem.ToString().IndexOf(' '));

                if (ligacao.Inserir(numericUpDown1.Value.ToString(),
                    textBox1.Text, id_genero, textBox2.Text, id_actores, rbuttonvalue))
                {
                    MessageBox.Show("Inseriu com Sucesso!");
                    Limpar();
                }
                else
                {
                    MessageBox.Show("ERRO na inserção!");
                }

            }
        }

        void Limpar()
        {
            numericUpDown1.Value = 0;
            textBox1.Text = "";
            textBox2.Clear();
            comboGenero.SelectedIndex = -1;
            comboActores.SelectedIndex = -1;

            groupBox1.Controls.OfType<RadioButton>().ToList().ForEach(p => p.Checked = false);

            foreach (RadioButton radio in groupBox1.Controls.OfType<RadioButton>().ToList())
            {
                if (radio.Checked == true)
                {
                    radio.Checked = false;
                    break;
                }
            }

        }

        public static string TirarEspacos(string mensagem)
        {
            mensagem = mensagem.Trim();
            mensagem = Regex.Replace(mensagem, @"\s+", " ");
            return mensagem;
        }



        bool VerificarCampos()
        {
            textBox1.Text = TirarEspacos(textBox1.Text);

            if (textBox1.Text.Length < 1)
            {
                MessageBox.Show("Erro no campo Nome!");
                textBox1.Focus();
                return false;
            }

            if (comboGenero.SelectedIndex == -1)
            {
                MessageBox.Show("Erro no campo Genero!");
                comboGenero.Focus();
                return false;
            }

            if (comboActores.SelectedIndex == -1)
            {
                MessageBox.Show("Erro no campo Actores!");
                comboActores.Focus();
                return false;
            }

            textBox2.Text = TirarEspacos(textBox2.Text).Replace(" ", "");

            if (textBox2.Text.Length < 1)
            {
                MessageBox.Show("Erro no campo Ano!");
                textBox1.Focus();
                return false;
            }

            if (rbuttonvalue.Length < 1)
            {
                MessageBox.Show("Erro no campo Pontuação!");
                return false;
            }



            return true;
        }


        string rbuttonvalue = "";        
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            rbuttonvalue = radioButton1.Text;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            rbuttonvalue = radioButton2.Text;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            rbuttonvalue = radioButton3.Text;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            rbuttonvalue = radioButton4.Text;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            rbuttonvalue = radioButton5.Text;
        }

        private void comboActores_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
