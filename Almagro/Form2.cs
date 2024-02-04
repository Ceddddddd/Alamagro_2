using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Almagro
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            label4.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static string ConnectionString= "Data Source=DESKTOP-2DKQGSL\\SQLEXPRESS;Initial Catalog=Almagro;Integrated Security=True";
        private void button1_Click(object sender, EventArgs e)
        {
            string password = textBox2.Text;
            string user = textBox1.Text;
            string query = "SELECT COUNT(*) FROM users WHERE username = @Username AND password = @Password";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", user);
                command.Parameters.AddWithValue("@Password", password);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        // Credentials are correct
                        this.Hide();
                        Form1 form1 = new Form1();
                        form1.ShowDialog();
                    }
                    else
                    {
                        // Credentials are incorrect
                        MessageBox.Show("Invalid Credentials");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); // Handle exception appropriately
                }
            }
        }
    }
}
