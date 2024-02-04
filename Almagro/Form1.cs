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

namespace Almagro
{
    public partial class Form1 : Form
    {
        public Form1()
        {   
            InitializeComponent();
            panel9.Hide();
            displayRooms();
            dataGridView2.Hide();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-2DKQGSL\\SQLEXPRESS;Initial Catalog=Almagro;Integrated Security=True");

        private void button1_Click_1(object sender, EventArgs e)
        {
            panel9.Hide();
            displayReservation();

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

            // Get the Graphics object
            Graphics g = e.Graphics;

            // Create a pen with desired attributes
            Pen pen = new Pen(Color.Black, 1); // Black color, 2 pixels width

            // Define the start and end points of the line (horizontal line)
            int y = 50; // Y coordinate of the line
            Point startPoint = new Point(1, y);
            Point endPoint = new Point(300, y);

            // Draw the line
            g.DrawLine(pen, startPoint, endPoint);

            // Dispose of the pen to release resources
            pen.Dispose();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Create a pen with desired attributes
            Pen pen = new Pen(Color.Black, 1); // Black color, 2 pixels width

            // Define the start and end points of the line (horizontal line)
            int y = 50; // Y coordinate of the line
            Point startPoint = new Point(15, y);
            Point endPoint = new Point(180, y);

            // Draw the line
            g.DrawLine(pen, startPoint, endPoint);

            // Dispose of the pen to release resources
            pen.Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void lbl_profile_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            displayRooms();
            panel9.Hide();
        }
        private void displayRooms()
        {
            dataGridView2.Hide();
            con.Open();
            string query = "Select * from rooms";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            SqlCommandBuilder com = new SqlCommandBuilder(adapter);
            dataGridView1.RowTemplate.Height = 27; 
            var table = new DataTable();
            adapter.Fill(table);
            dataGridView1.Size = main.Size;
            dataGridView1.DataSource = table;
            for (int i = 0; i < 5; i++)
            {
                dataGridView1.Columns[i].Width = 132;
            }
            con.Close();
        }
        private void displayReservation()
        {
            dataGridView2.Hide();
            con.Open();
            string query = "SELECT reservation_id AS 'Reservation ID', \r\n       r.room_id AS 'Room Number', \r\n       c.email AS 'Reserved to', \r\n       reserved_date AS 'Date Reserved'\r\nFROM reservation r\r\nINNER JOIN Customer c ON r.customer_id = c.customer_id;";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            SqlCommandBuilder com = new SqlCommandBuilder(adapter);
            dataGridView1.RowTemplate.Height = 50; 
            var table = new DataTable();
            adapter.Fill(table);
            dataGridView1.Size = main.Size;
            dataGridView1.DataSource = table;
            for (int i = 0; i < 4; i++)
            {
                dataGridView1.Columns[i].Width = 190;
                
            }
            con.Close();


        }
        private void displayCustomer()
        {
            panel9.Show();
            dataGridView2.Show();
            con.Open();
            string query = "select * from Customer";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            SqlCommandBuilder com = new SqlCommandBuilder(adapter);
            dataGridView1.RowTemplate.Height = 27;
            var table = new DataTable();
            adapter.Fill(table);
            dataGridView2.Size = main.Size;
            dataGridView2.DataSource = table;
            for (int i = 0; i < 5; i++)
            {
                dataGridView2.Columns[i].Width = 152;
            }
            con.Close();


        }

        private void button3_Click(object sender, EventArgs e)
        {   
            displayCustomer();
            panel9.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView2.Hide();
            string selectQuery = "select * from rooms where available = 'Available'";
            DataTable reservationsTable = new DataTable();
            con.Open();
            using (SqlCommand command = new SqlCommand(selectQuery, con))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(reservationsTable);
                    for (int i = 0; i < 4; i++)
                    {
                        dataGridView1.Columns[i].Width = 132;
                    }
                }
            }
            con.Close();
            dataGridView1.DataSource = reservationsTable;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
          
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure a valid row is clicked and it's not a header row
        }

        private int room_id;
        private void button5_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-2DKQGSL\\SQLEXPRESS;Initial Catalog=Almagro;Integrated Security=True";

            // Variables holding data for the queries
            try { room_id = int.Parse(textBox6.Text); }
            catch {
                MessageBox.Show("Enter room number");
                 return;
            }
           
            int customer_id = int.Parse(textBox1.Text);
            string f_name = textBox2.Text;
            string l_name = textBox3.Text;
            string phone = textBox4.Text;
            string email = textBox5.Text;
            DateTime selectedDate = dateTimePicker1.Value.Date;
            string formattedDate = selectedDate.ToString("MM-dd-yyyy"); // Format the date as 'YYYY-MM-DD'
            

            // Queries
            string reservationQuery = $"INSERT INTO reservation VALUES ({customer_id}, {room_id}, {customer_id}, '{formattedDate}')";
            string insertQuery = $"INSERT INTO Customer VALUES ({customer_id}, '{f_name}','{l_name}','{phone}', '{email}')";
            string insertreserve= $"Insert into reservation values((select customer_id from Customer),{room_id},{customer_id},{formattedDate})";
            string updateQuery = $"UPDATE rooms SET available = 'Not Available' WHERE room_id = {room_id}";
            string checkQuery = $"select available from rooms where room_id = {room_id}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                    try
                {
                    using (SqlCommand command = new SqlCommand(checkQuery, connection))
                    {
                        object result = command.ExecuteScalar();

                        // Check if the room is not available
                        if (result != null && result.ToString() == "Not Available")
                        {
                            MessageBox.Show("Room is not available", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Execute the insert query
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Execute the update query
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    using (SqlCommand command = new SqlCommand(insertreserve, connection))
                    {
                        object result = command.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }  
                // Execute the reservation query
               

                connection.Close();
                displayCustomer();
            }
        }

        private void signout_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.ShowDialog();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            int customer_id = int.Parse(textBox1.Text);
            string f_name = textBox2.Text;
            string l_name = textBox3.Text;
            string phone = textBox4.Text;
            string email = textBox5.Text;
            DateTime selectedDate = dateTimePicker1.Value.Date;
            string formattedDate = selectedDate.ToString("MM-dd-yyyy"); // Format the date as 'YYYY-MM-DD'
            string connectionString = "Data Source=DESKTOP-2DKQGSL\\SQLEXPRESS;Initial Catalog=Almagro;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Delete customer
                string editQuery = $"UPDATE Customer SET firstname = '{f_name}', lastname = '{l_name}', phone = '{phone}', email = '{email}' WHERE customer_id = {customer_id}";
                SqlCommand Command = new SqlCommand(editQuery, connection);
                Command.ExecuteNonQuery();
                displayCustomer();
               
            }
        }
        
        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        

        private void Button7_Click(object sender, EventArgs e)
        {
            int customer_id = int.Parse(textBox1.Text);
            string connectionString = "Data Source=DESKTOP-2DKQGSL\\SQLEXPRESS;Initial Catalog=Almagro;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string updateRoomQuery = $"UPDATE rooms SET available = 'Available' WHERE room_id = (SELECT room_id FROM reservation WHERE customer_id = {customer_id})";
                SqlCommand updateRoomCommand = new SqlCommand(updateRoomQuery, connection);
                updateRoomCommand.ExecuteNonQuery();
                // Delete the reservation for the specified customer_id
                string deleteReservationQuery = $"DELETE FROM reservation WHERE customer_id = {customer_id}";
                SqlCommand deleteReservationCommand = new SqlCommand(deleteReservationQuery, connection);
                deleteReservationCommand.ExecuteNonQuery();

                // Delete the customer record
                string deleteCustomerQuery = $"DELETE FROM Customer WHERE customer_id = {customer_id}";
                SqlCommand deleteCustomerCommand = new SqlCommand(deleteCustomerQuery, connection);
                deleteCustomerCommand.ExecuteNonQuery();

                // Update the room status to 'Available' based on the reservation being deleted
               

                // Delete the reservation record again (optional, if needed)
                string deleteReservationAgainQuery = $"DELETE FROM reservation WHERE customer_id = {customer_id}";
                SqlCommand deleteReservationAgainCommand = new SqlCommand(deleteReservationAgainQuery, connection);
                deleteReservationAgainCommand.ExecuteNonQuery();

                MessageBox.Show("Delete executed successfully.");

                displayCustomer();
            }

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView2.CurrentRow.Selected = true;
                textBox1.Text = dataGridView2.Rows[e.RowIndex].Cells["customer_id"].FormattedValue.ToString();
                textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells["firstname"].FormattedValue.ToString();
                textBox3.Text = dataGridView2.Rows[e.RowIndex].Cells["lastname"].FormattedValue.ToString();
                textBox4.Text = dataGridView2.Rows[e.RowIndex].Cells["phone"].FormattedValue.ToString();
                textBox5.Text = dataGridView2.Rows[e.RowIndex].Cells["email"].FormattedValue.ToString();


            }
        }
    }
}