using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Filmes
{
    internal class DBConnect
    {

        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string port;

        public DBConnect()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "192.168.1.99";
            database = "bdfilmes";
            uid = "idCsharp";
            password = "1234";
            port = "3306";

            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";";

            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void PreencherDataGridView(ref DataGridView dgv)
        {

            string query = "SELECT id_filmes, filmes, concat(tbgenero.id_genero, ' - ', genero) as Genero, ano, " +
                "concat(tbactores.id_actores, ' - ', actores) as Actores, pontuação as Pontuação " +
                "FROM tbfilmes left join tbgenero  on tbgenero.id_genero = tbfilmes.id_genero " +
                "left join tbactores on tbactores.id_actores = tbfilmes.id_actores " +
                "order by id_filmes";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    int idxLinha = 0;
                    while (dr.Read())
                    {
                        dgv.Rows.Add();
                        dgv.Rows[idxLinha].Cells["id_filmes"].Value = dr[0].ToString();
                        dgv.Rows[idxLinha].Cells["filmes"].Value = dr[1].ToString();
                        dgv.Rows[idxLinha].Cells["Genero"].Value = dr[2].ToString();
                        dgv.Rows[idxLinha].Cells["ano"].Value = dr[3].ToString();
                        dgv.Rows[idxLinha].Cells["Actores"].Value = dr[4].ToString();
                        dgv.Rows[idxLinha].Cells["Pontuação"].Value = dr[5].ToString();

                        idxLinha++;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void PreencherComboGenero(ref ComboBox combo)
        {
            string query = "select id_genero, genero from tbgenero order by id_genero";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        combo.Items.Add(dr["id_genero"].ToString() + " - " + dr[1].ToString());
                    }
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void PreencherComboActores(ref ComboBox combo)
        {
            string query = "select id_actores, actores from tbactores order by id_actores";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        combo.Items.Add(dr["id_actores"].ToString() + " - " + dr[1].ToString());
                    }
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool Inserir(string idfilmes, string nome, string idgenero, string ano, string idactores, string pontuacao)
        {
            bool flag = true;
            

            string query =
                "insert into tbfilmes values (" + idfilmes + ", '" + nome + "', " + idgenero + ",' " + ano + "', " + idactores + ", "+ pontuacao + ")";


            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();

                    //CloseConnection();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                flag = false;
            }
            finally
            {
                CloseConnection();
            }

            return flag;
        }


        public bool Update(string idfilmes, string nome, string idgenero, string ano, string idactores, string pontuacao)
        {
            bool flag = true;

            string query =
                "Update tbfilmes set filmes ='" + nome + "', id_genero = " + idgenero + ", ano='" + ano + "', id_actores=" + idactores + ",pontuação= " + pontuacao + 
                " where id_filmes = " + idfilmes;

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();

                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                flag = false;
            }
            finally
            {
                CloseConnection();
            }

            return flag;
        }


        public bool PesquisaFilme(string idfilme, ref string nome, ref string genero, ref string ano, ref string idactores, ref string pontuacao)
       

        {
            //string query = "SELECT Fornecedor, concat(forn.ID_Distrito, ' - ', distrito) as Distrito, EMAIL " +
            //    "FROM fornecedores forn, distritos dis WHERE ID_Fornecedor = " + idpesquisa + " and forn.ID_Distrito = dis.ID_Distrito";

            string query = "SELECT filmes as filmes, concat(tbgenero.id_genero, ' - ', genero) as Genero, concat(tbactores.id_actores, ' - ', actores) as Actores, " +
                " ano as ano,  pontuação as Pontuação  " + " FROM tbfilmes left join tbgenero  on tbgenero.id_genero = tbfilmes.id_genero " +
                " left join tbactores on tbactores.id_actores = tbfilmes.id_actores " +
                " WHERE id_filmes = " + idfilme ;




            bool flag = false;

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        nome = dataReader["filmes"].ToString();
                        genero = dataReader["Genero"].ToString(); 
                        ano = dataReader["ano"].ToString();
                        idactores = dataReader["Actores"].ToString();
                        pontuacao = dataReader["Pontuação"].ToString();
                        flag = true;

                    }
                    dataReader.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return flag;

        }


        public bool Delete(string idfilmes, string nome, string idgenero, string ano, string idactores, string pontuacao)
        {
            bool flag = true;

            string query =
                "delete from tbfilmes where id_filmes= " + idfilmes;

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                flag = false;
            }
            finally
            {
                CloseConnection();
            }

            return flag;
        }












    }
}
