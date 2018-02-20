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
using System.Threading;

namespace EMUSQL
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        private MySqlConnection con;
        static string server;
        static string database;
        static string uid;
        static string pass;
        static DataTable table;

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_queryexecute_Click(object sender, EventArgs e)
        {
            status.Text = "Executing...";
            Query();
            status.Text = "Querying...";
            QueryAll();
            status.Text = "Connected";
        }

        public void Query()
        {
            string queryinfo = @querytext.Text;
            table = new DataTable();
            MySqlCommand cmd = new MySqlCommand(queryinfo, con);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                adapter.Fill(table);
                datagrid.DataSource = table;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }


        }

        public void QueryAll()
        {
            string queryinfo = @"SELECT * FROM " + txt_tablename.Text;
            table = new DataTable();
            MySqlCommand cmd = new MySqlCommand(queryinfo, con);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            try
            {
                adapter.Fill(table);
                datagrid.DataSource = table;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }


        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            try
            {
                server = txt_server.Text;
                database = txt_database.Text;
                uid = txt_username.Text;
                pass = txt_pass.Text;
                string context = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={pass};";
                con = new MySqlConnection(context);
                status.ForeColor = Color.DarkGreen;
                status.Text = "Connected";
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void btn_selectall_Click(object sender, EventArgs e)
        {
            status.Text = "Querying...";
            QueryAll();
            status.Text = "Connected";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void hcshowall_Click(object sender, EventArgs e)
        {
            codesnippet.Text = "Code : SELECT * FROM 'table name'";
        }

        private void CheckKeyword(string word, Color color, int startIndex)
        {
            if (this.codesnippet.Text.Contains(word))
            {
                int index = -1;
                int selectStart = this.codesnippet.SelectionStart;

                while ((index = this.codesnippet.Text.IndexOf(word, (index + 1))) != -1)
                {
                    this.codesnippet.Select((index + startIndex), word.Length);
                    this.codesnippet.SelectionColor = color;
                    this.codesnippet.Select(selectStart, 0);
                    this.codesnippet.SelectionColor = Color.Black;
                }
            }
        }

        private void codesnippet_TextChanged(object sender, EventArgs e)
        {
            this.CheckKeyword("select", Color.DarkRed, 0);
            this.CheckKeyword("SELECT", Color.DarkRed, 0);
            this.CheckKeyword("insert", Color.DarkRed, 0);
            this.CheckKeyword("INSERT", Color.DarkRed, 0);
            this.CheckKeyword("delete", Color.DarkRed, 0);
            this.CheckKeyword("DELETE", Color.DarkRed, 0);
            this.CheckKeyword("into", Color.DarkRed, 0);
            this.CheckKeyword("INTO", Color.DarkRed, 0);
            this.CheckKeyword("where", Color.DarkRed, 0);
            this.CheckKeyword("WHERE", Color.DarkRed, 0);
            this.CheckKeyword("table", Color.DarkRed, 0);
            this.CheckKeyword("TABLE", Color.DarkRed, 0);
            this.CheckKeyword("not", Color.DarkRed, 0);
            this.CheckKeyword("NOT", Color.DarkRed, 0);
            this.CheckKeyword("null", Color.Blue, 0);
            this.CheckKeyword("NULL", Color.Blue, 0);
            this.CheckKeyword("engine", Color.DarkRed, 0);
            this.CheckKeyword("ENGINE", Color.DarkRed, 0);
            this.CheckKeyword("drop", Color.DarkRed, 0);
            this.CheckKeyword("DROP", Color.DarkRed, 0);
            this.CheckKeyword("values", Color.DarkRed, 0);
            this.CheckKeyword("VALUES", Color.DarkRed, 0);
        }

        private void hcinsertrow_Click(object sender, EventArgs e)
        {
            codesnippet.Text = "Code : INSERT INTO `table name` (`id`, `column1`, `column2`) VALUES (NULL, 'something', 'something2')";
        }

        private void hcdeleterow_Click(object sender, EventArgs e)
        {
            codesnippet.Text = "Code : DELETE FROM `table name` WHERE `table name`.`id` = idnumber";
        }

        private void hccreatetable_Click(object sender, EventArgs e)
        {
            codesnippet.Text = "Code : CREATE TABLE `database name`.`new table` ( `id` INT NOT NULL AUTO_INCREMENT , `Column1` VARCHAR(50) NOT NULL , `Column2` INT NOT NULL , PRIMARY KEY (`id`)) ENGINE = InnoDB;";
        }

    }
}
