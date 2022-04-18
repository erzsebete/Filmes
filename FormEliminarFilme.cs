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
    public partial class FormEliminarFilme : Form
    {
        readonly DBConnect ligacao = new DBConnect();

        string rbuttonvalue = "";

        public FormEliminarFilme()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormEliminarFilme_Load(object sender, EventArgs e)
        {
            textBox1.Visible = false;

            textBox2.Visible = false;

            button1.Enabled = false;

            ligacao.PreencherComboGenero(ref comboGenero);
            ligacao.PreencherComboActores(ref comboActores);

            groupBox1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            rbuttonvalue = radioButton5.Text;
        }

        private void comboActores_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (VerificarCampos())
            {
                string id_genero = comboGenero.SelectedItem.ToString().Substring
                (0, comboGenero.SelectedItem.ToString().IndexOf(' '));

                string id_actores = comboActores.SelectedItem.ToString().Substring
                (0, comboActores.SelectedItem.ToString().IndexOf(' '));

                ValorRBtn();

                if (ligacao.Delete(numericUpDown1.Value.ToString(),
                    textBox1.Text, id_genero, textBox2.Text, id_actores, rbuttonvalue))
                {
                    MessageBox.Show("Filme Eliminado com Sucesso!");
                    Limpar();
                }
                else
                {
                    MessageBox.Show("ERRO na eliminação!");
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

        private void button3_Click(object sender, EventArgs e)
        {
            string idfilme, nome = "", genero = "", ano = "", idactores = "", pontuacao = "";

            idfilme = numericUpDown1.Value.ToString();


            if (ligacao.PesquisaFilme(idfilme, ref nome, ref genero, ref ano, ref idactores, ref pontuacao))
            {

                textBox1.Visible = true;
                textBox1.Text = nome;
                comboGenero.Text = genero;
                comboActores.Text = idactores;
                textBox2.Visible = true;
                textBox2.Text = ano;
                rbuttonvalue = pontuacao;
                RadioButton(rbuttonvalue);
                groupBox1.Enabled = true;
                button1.Enabled = true;

            }
            else
            {
                MessageBox.Show("Não foi encontrado nenhum registo!", "Pesquisa",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void RadioButton(string rbuttonvalue)
        {

            switch (rbuttonvalue)
            {
                case "1":
                    radioButton1.Checked = true;
                    break;

                case "2":
                    radioButton2.Checked = true;
                    break;
                case "3":
                    radioButton3.Checked = true;
                    break;
                case "4":
                    radioButton4.Checked = true;
                    break;
                case "5":
                    radioButton5.Checked = true;
                    break;
            }
        }


        public void ValorRBtn()
        {

            if (radioButton1.Checked)
                rbuttonvalue = "1";
            else if (radioButton2.Checked)
                rbuttonvalue = "2";
            else if (radioButton3.Checked)
                rbuttonvalue = "3";
            else if (radioButton4.Checked)
                rbuttonvalue = "4";
            else if (radioButton5.Checked)
                rbuttonvalue = "5";
        }




    }
}
