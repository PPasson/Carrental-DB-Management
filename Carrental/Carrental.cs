using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Carrental
{
    public partial class Form1 : Form
    {
        MySqlConnection con = new MySqlConnection("host=localhost;user=root;password=;database=project");
        MySqlCommand comm;

        public Form1()
        {
            InitializeComponent();
        }


        private void Maintenance_Load_1(object sender, EventArgs e)
        {
            /*maintenance & repairman*/
            open_connection();
            carid_combobox.SelectedIndex = 0;
            load_maintenance_griddata_init();
            load_carid_combobox_init();
            load_repairman_griddata_init();
            load_assignment_griddata_init();
            load_mtn_combobox_init();
            load_rpm_combobox_init();

            /*payment & bill*/
            payment_combobox.SelectedIndex = 0;
            load_bill_griddata_init();
            load_payment_combobox_init();
            load_payment_griddata_init();

            /* CarRental */
            Customer_Box.SelectedIndex = 0;
            Employee_Box.SelectedIndex = 0;
            Bill_Box.SelectedIndex = 0;
            Car_Box.SelectedIndex = 0;
            load_car_rental_griddata_init();
            load_Customer_Box_init();
            load_Employee_Box_init();
            load_Bill_Box_init();
            load_Car_Box_init();

            /* customer */
            load_customer_griddata_init();

            /* employee */
            load_employee_griddata_init();

            /* car */
            load_car_griddata_init();



        }

        private void open_connection()
        {

            con.Open();
            //MessageBox.Show($"MySQL version : {con.ServerVersion}");
        }





        /* ---------------------- maintenance -------------------------- */

        private void clear_maintenance_data()
        {
            maintenanceid_txtbox.Text = "";
            date_picker.Value = DateTime.Now;
            mileage_txtbox.Text = "";
            message_txtbox.Text = "";
            autoparts_txtbox.Text = "";
            carid_combobox.SelectedIndex = 0;
        }

        private void mtn_clear_btn_Click(object sender, EventArgs e)
        {
            clear_maintenance_data();
        }



        private void mtn_insert_btn_Click(object sender, EventArgs e)
        {
            var mtn_id = maintenanceid_txtbox.Text;
            string day = date_picker.Value.ToString("yyyy-MM-dd");
            var ml = mileage_txtbox.Text;
            string ms = message_txtbox.Text;
            string ap = autoparts_txtbox.Text;
            var c_id = carid_combobox.SelectedIndex;

            comm = con.CreateCommand();
            comm.CommandText = "INSERT INTO `project`.`maintenance` (`maintenance_id`, `date`, `mileage`, `message`, `autoparts`, `car_id`) " +
                "VALUES " + "(@maintenance_id, @date, @mileage, @message, @autoparts, @car_id)";

            comm.Parameters.AddWithValue("@maintenance_id", mtn_id);
            comm.Parameters.AddWithValue("@date", day);
            comm.Parameters.AddWithValue("@mileage", ml);
            comm.Parameters.AddWithValue("@message", ms);
            comm.Parameters.AddWithValue("@autoparts", ap);
            comm.Parameters.AddWithValue("@car_id", c_id);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Save Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_maintenance_griddata_init();
            clear_maintenance_data();
        }

        private void mtn_uodate_btn_Click(object sender, EventArgs e)
        {
            var mtn_id = maintenanceid_txtbox.Text;
            string day = date_picker.Value.ToString("yyyy-MM-dd");
            var ml = mileage_txtbox.Text;
            string ms = message_txtbox.Text;
            string ap = autoparts_txtbox.Text;
            var c_id = carid_combobox.SelectedIndex;

            comm = con.CreateCommand();

            comm.CommandText = "UPDATE `project`.`maintenance`" +
                "SET `maintenance_id`=@maintenance_id, `date`=@date, `mileage`=@mileage, `message`=@message, `autoparts`=@autoparts, `car_id`=@car_id " +
                "WHERE `maintenance_id` = @maintenance_id";

            comm.Parameters.AddWithValue("@maintenance_id", mtn_id);
            comm.Parameters.AddWithValue("@date", day);
            comm.Parameters.AddWithValue("@mileage", ml);
            comm.Parameters.AddWithValue("@message", ms);
            comm.Parameters.AddWithValue("@autoparts", ap);
            comm.Parameters.AddWithValue("@car_id", c_id);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Update Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_maintenance_griddata_init();
            clear_maintenance_data();
        }

        private void mtn_delete_btn_Click_1(object sender, EventArgs e)
        {
            var mtn_id = maintenanceid_txtbox.Text;

            comm = con.CreateCommand();
            comm.CommandText = "DELETE FROM  `project`.`maintenance`" +
                "WHERE `maintenance_id` = @maintenance_id";

            comm.Parameters.AddWithValue("@maintenance_id", mtn_id);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Delete Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_maintenance_griddata_init();
            clear_maintenance_data();
        }

        private void load_maintenance_griddata_init()
        {
            string sql = "SELECT * FROM project.maintenance";
            comm = new MySqlCommand(sql, con);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            da.Fill(ds, "maintenance");
            maintenance_dataGridView.DataSource = ds.Tables["maintenance"].DefaultView;
            maintenance_dataGridView.Columns[2].DefaultCellStyle.Format = "yyyy-MM-dd";

        }

        private void load_carid_combobox_init()
        {
            string sql = "SELECT distinct car_id FROM project.car";
            comm = new MySqlCommand(sql, con);

            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                carid_combobox.Items.Add(reader.GetString("car_id"));
            }
            reader.Close();
        }



        private void maintenance_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (maintenance_dataGridView.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                string mtn_id = maintenance_dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                string day = maintenance_dataGridView.SelectedRows[0].Cells[1].Value.ToString();
                string ml = maintenance_dataGridView.SelectedRows[0].Cells[2].Value.ToString();
                string ms = maintenance_dataGridView.SelectedRows[0].Cells[3].Value.ToString();
                string ap = maintenance_dataGridView.SelectedRows[0].Cells[4].Value.ToString();
                string c_id = maintenance_dataGridView.SelectedRows[0].Cells[5].Value.ToString();


                maintenanceid_txtbox.Text = mtn_id;
                mileage_txtbox.Text = ml;
                message_txtbox.Text = ms;
                autoparts_txtbox.Text = ap;
                if (c_id != "")
                {
                    carid_combobox.SelectedIndex = int.Parse(c_id);
                }
                DateTime datee;
                if (day != "")
                {
                    datee = Convert.ToDateTime(day, null);
                    string str = datee.Year + "-" + datee.Month + "-" + datee.Day;
                    date_picker.Value = DateTime.ParseExact(str, "yyyy-M-d", null);
                }

            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void carid_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /*--------------------- end Maintenance ---------------------- */













        /* -----------------Repairman ------------- */

        private void clear_repairman_data()
        {
            repairmanid_txtbox.Text = "";
            repairmanname_txtbox.Text = "";
            expertise_txtbox.Text = "";
        }



        private void rpm_insert_butt_Click(object sender, EventArgs e)
        {
            var rpm_id = repairmanid_txtbox.Text;
            string name = repairmanname_txtbox.Text;
            string exp = expertise_txtbox.Text;

            comm = con.CreateCommand();
            comm.CommandText = "INSERT INTO `project`.`repairman` (`repairman_id`, `repairman_name`, `expertise`) " +
                "VALUES " + "(@repairman_id, @repairman_name, @expertise)";

            comm.Parameters.AddWithValue("@repairman_id", rpm_id);
            comm.Parameters.AddWithValue("@repairman_name", name);
            comm.Parameters.AddWithValue("@expertise", exp);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Save Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_repairman_griddata_init();
            clear_repairman_data();
        }

        private void rpm_clear_butt_Click(object sender, EventArgs e)
        {
            clear_repairman_data();
        }

        private void load_repairman_griddata_init()
        {
            string sql = "SELECT * FROM project.repairman";
            comm = new MySqlCommand(sql, con);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            da.Fill(ds, "repairman");
            repairman_dataGridView.DataSource = ds.Tables["repairman"].DefaultView;

        }

        private void repairman_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (repairman_dataGridView.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                string rpm_id = repairman_dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                string name = repairman_dataGridView.SelectedRows[0].Cells[1].Value.ToString();
                string exp = repairman_dataGridView.SelectedRows[0].Cells[2].Value.ToString();

                repairmanid_txtbox.Text = rpm_id;
                repairmanname_txtbox.Text = name;
                expertise_txtbox.Text = exp;
            }
        }

        private void rpm_update_butt_Click(object sender, EventArgs e)
        {
            var rpm_id = repairmanid_txtbox.Text;
            string name = repairmanname_txtbox.Text;
            string exp = expertise_txtbox.Text;

            comm = con.CreateCommand();

            comm.CommandText = "UPDATE `project`.`repairman`" +
                "SET `repairman_id`=@repairman_id, `repairman_name`=@repairman_name, `expertise`=@expertise " +
                "WHERE `repairman_id` = @repairman_id";

            comm.Parameters.AddWithValue("@repairman_id", rpm_id);
            comm.Parameters.AddWithValue("@repairman_name", name);
            comm.Parameters.AddWithValue("@expertise", exp);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Update Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_repairman_griddata_init();
            clear_repairman_data();
        }

        private void rpm_delete_butt_Click(object sender, EventArgs e)
        {
            var rpm_id = repairmanid_txtbox.Text;

            comm = con.CreateCommand();
            comm.CommandText = "DELETE FROM  `project`.`repairman`" +
                "WHERE `repairman_id` = @repairman_id";

            comm.Parameters.AddWithValue("@repairman_id", rpm_id);


            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Delete Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_repairman_griddata_init();
            clear_repairman_data();
        }

        /* ------------------end repairman -------------------------- */










        /* ------------------------ assignment ---------------------- */




        private void clear_assignment_data()
        {
            mtn_combobox.SelectedIndex = 0;
            rpm_combobox.SelectedIndex = 0;
        }




        private void asm_insert_butt_Click(object sender, EventArgs e)
        {
            var selectedMtn = mtn_combobox.SelectedIndex;
            var selectedRpm = rpm_combobox.SelectedIndex;
            var mtn = selectedMtn.ToString();
            var rpm = selectedRpm.ToString();

            comm = con.CreateCommand();
            comm.CommandText = "INSERT INTO `project`.`maintenance_has_repairman` (`maintenance_id`, `repairman_id`) " +
                "VALUES " + "(@maintenance_id, @repairman_id)";

            comm.Parameters.AddWithValue("@maintenance_id", mtn);
            comm.Parameters.AddWithValue("@repairman_id", rpm);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Save Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_assignment_griddata_init();
            clear_assignment_data();
        }

        private void load_assignment_griddata_init()
        {
            string sql = "SELECT * FROM project.maintenance_has_repairman";
            comm = new MySqlCommand(sql, con);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            da.Fill(ds, "Assignment");
            assignment_dataGridView.DataSource = ds.Tables["Assignment"].DefaultView;

        }

        private void load_mtn_combobox_init()
        {
            string sql = "SELECT distinct maintenance_id FROM project.maintenance";
            comm = new MySqlCommand(sql, con);

            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                mtn_combobox.Items.Add(reader.GetString("maintenance_id"));
            }
            reader.Close();
        }

        private void load_rpm_combobox_init()
        {
            string sql = "SELECT distinct repairman_name FROM project.repairman";
            comm = new MySqlCommand(sql, con);

            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                rpm_combobox.Items.Add(reader.GetString("repairman_name"));
            }
            reader.Close();
        }

        private void assignment_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (assignment_dataGridView.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                string mtn = assignment_dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                string rpm = assignment_dataGridView.SelectedRows[0].Cells[1].Value.ToString();




                if (mtn != "")
                {
                    mtn_combobox.SelectedIndex = int.Parse(mtn);
                }
                if (rpm != "")
                    rpm_combobox.SelectedIndex = int.Parse(rpm);
            }
        }

        private void mtn_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void asm_clear_butt_Click(object sender, EventArgs e)
        {
            clear_assignment_data();
        }

        private void asm_update_butt_Click(object sender, EventArgs e)
        {
            var mtn = mtn_combobox.SelectedIndex;
            var rpm = rpm_combobox.SelectedIndex;

            comm = con.CreateCommand();

            comm.CommandText = "UPDATE `project`.`maintenance_has_repairman`" +
                "SET `maintenance_id`=@maintenance_id, `repairman_id`=@repairman_id " +
                "WHERE `maintenance_id` = @maintenance_id";

            comm.Parameters.AddWithValue("@maintenance_id", mtn);
            comm.Parameters.AddWithValue("@repairman_id", rpm);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Update Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_assignment_griddata_init();
            clear_assignment_data();
        }

        private void asm_delete_butt_Click(object sender, EventArgs e)
        {
            var mtn = mtn_combobox.SelectedIndex;

            comm = con.CreateCommand();
            comm.CommandText = "DELETE FROM  `project`.`maintenance_has_repairman`" +
                "WHERE `maintenance_id` = @maintenance_id";

            comm.Parameters.AddWithValue("@maintenance_id", mtn);


            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Delete Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_assignment_griddata_init();
            clear_assignment_data();
        }


        /* ---------------------- end assignment -------------------------- */





        /* ------------------------ bill --------------------------------*/

        private void load_bill_griddata_init()
        {
            string sql = "SELECT * FROM project.bill";
            comm = new MySqlCommand(sql, con);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            da.Fill(ds, "bill");
            bill_dataGridView.DataSource = ds.Tables["bill"].DefaultView;
            bill_dataGridView.Columns[3].DefaultCellStyle.Format = "yyyy-MM-dd";
        }

        private void load_payment_combobox_init()
        {
            string sql = "SELECT distinct payment_method_id FROM project.payment_method";
            comm = new MySqlCommand(sql, con);

            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                payment_combobox.Items.Add(reader.GetString("payment_method_id"));
            }
            reader.Close();
        }

        private void clear_bill_data()
        {
            bid_textbox.Text = "";
            payment_combobox.SelectedIndex = 0;
            total_textbox.Text = "";
            date_datepicker.Value = DateTime.Now;
            note_textbox.Text = "";
        }

        private void bill_inert_btn_Click(object sender, EventArgs e)
        {
            var b_id = bid_textbox.Text;
            var b_total = total_textbox.Text;
            var selectedCar = payment_combobox.SelectedIndex;
            var b_payment = selectedCar.ToString();
            var note = note_textbox.Text;
            string dob_date = date_datepicker.Value.ToString("yyyy-MM-dd");

            comm = con.CreateCommand();
            comm.CommandText = "INSERT INTO `project`.`bill` (`bill_id`, `payment_method_id`, `total_cost`, `date`, `note`) " +
                "VALUES " + "(@bill_id, @payment_method_id, @total_cost, @date, @note)";

            comm.Parameters.AddWithValue("@bill_id", b_id);
            comm.Parameters.AddWithValue("@payment_method_id", b_payment);
            comm.Parameters.AddWithValue("@total_cost", b_total);
            comm.Parameters.AddWithValue("@date", dob_date);
            comm.Parameters.AddWithValue("@note", note);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Save Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_bill_griddata_init();
            clear_bill_data();
        }

        private void bill_update_btn_Click(object sender, EventArgs e)
        {
            var b_id = bid_textbox.Text;
            var b_total = total_textbox.Text;
            var selectedpayment = payment_combobox.SelectedIndex;
            var b_payment = selectedpayment.ToString();
            var note = note_textbox.Text;
            string b_date = date_datepicker.Value.ToString("yyyy-MM-dd");

            comm = con.CreateCommand();

            comm.CommandText = "UPDATE `project`.`bill`" +
                "SET `payment_method_id`=@payment_method_id, `total_cost`=@total_cost, `date`=@date, `note`=@note " +
                "WHERE `bill_id` = @bill_id";

            comm.Parameters.AddWithValue("@bill_id", b_id);
            comm.Parameters.AddWithValue("@payment_method_id", b_payment);
            comm.Parameters.AddWithValue("@total_cost", b_total);
            comm.Parameters.AddWithValue("@date", b_date);
            comm.Parameters.AddWithValue("@note", note);
            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Update Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_bill_griddata_init();
            clear_bill_data();
        }

        private void bill_delete_btn_Click(object sender, EventArgs e)
        {
            var b_id = bid_textbox.Text;

            comm = con.CreateCommand();
            comm.CommandText = "DELETE FROM  `project`.`bill`" +
                "WHERE `bill_id` = @bill_id";

            comm.Parameters.AddWithValue("@bill_id", b_id);


            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Delete Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_bill_griddata_init();
            clear_bill_data();
        }

        private void bill_clear_btn_Click(object sender, EventArgs e)
        {
            clear_bill_data();
        }

        private void bill_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (bill_dataGridView.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                string b_id = bill_dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                string b_total = bill_dataGridView.SelectedRows[0].Cells[1].Value.ToString();
                string b_date = bill_dataGridView.SelectedRows[0].Cells[2].Value.ToString();
                string b_note = bill_dataGridView.SelectedRows[0].Cells[3].Value.ToString();
                string b_payment = bill_dataGridView.SelectedRows[0].Cells[4].Value.ToString();

                bid_textbox.Text = b_id;
                total_textbox.Text = b_total;
                note_textbox.Text = b_note;

                if (b_payment != "")
                {
                    payment_combobox.SelectedIndex = int.Parse(b_payment);
                }

                if (b_date != "")
                {

                    DateTime date = Convert.ToDateTime(b_date, null);
                    string str = date.Year + "-" + date.Month + "-" + date.Day;
                    date_datepicker.Value = DateTime.ParseExact(str, "yyyy-M-d", null);
                }


            }
        }

        /* ----------------------- payment method--------------------- */

        private void clear_payment_data()
        {
            pid_textbox.Text = "";
            pname_textbox.Text = "";
            pdetail_textbox.Text = "";
        }

        private void load_payment_griddata_init()
        {
            string sql = "SELECT * FROM project.payment_method";
            comm = new MySqlCommand(sql, con);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            da.Fill(ds, "payment_method");
            payment_dataGridView.DataSource = ds.Tables["payment_method"].DefaultView;
        }

        private void pm_insert_btn_Click(object sender, EventArgs e)
        {
            var pid = pid_textbox.Text;
            var pname = pname_textbox.Text;
            var pdetail = pdetail_textbox.Text;


            comm = con.CreateCommand();
            comm.CommandText = "INSERT INTO `project`.`payment_method` (`payment_method_id`, `method_name`, `detail` ) " +
                "VALUES " + "(@payment_method_id, @method_name, @detail)";

            comm.Parameters.AddWithValue("@payment_method_id", pid);
            comm.Parameters.AddWithValue("@method_name", pname);
            comm.Parameters.AddWithValue("@detail", pdetail);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Save Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_payment_griddata_init();
            clear_payment_data();
        }

        private void pm_update_btn_Click(object sender, EventArgs e)
        {
            var p_id = pid_textbox.Text;
            var p_name = pname_textbox.Text;
            var p_detail = pdetail_textbox.Text;

            comm = con.CreateCommand();
            comm.CommandText = "UPDATE `project`.`payment_method` " +
                "SET `method_name`=@method_name, `detail`=@detail " +
                "WHERE `payment_method_id`=@payment_method_id";

            comm.Parameters.AddWithValue("@payment_method_id", p_id);
            comm.Parameters.AddWithValue("@method_name", p_name);
            comm.Parameters.AddWithValue("@detail", p_detail);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Update Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_payment_griddata_init();
            clear_payment_data();
        }

        private void pm_delete_btn_Click(object sender, EventArgs e)
        {
            var pid = pid_textbox.Text;

            comm = con.CreateCommand();
            comm.CommandText = "DELETE FROM  `project`.`payment_method` " +
                "WHERE `payment_method_id` = @payment_method_id";

            comm.Parameters.AddWithValue("@payment_method_id", pid);


            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Delete Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_payment_griddata_init();
            clear_payment_data();
        }

        private void pm_clear_btn_Click(object sender, EventArgs e)
        {
            clear_payment_data();
        }

        private void payment_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (payment_dataGridView.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                string pid = payment_dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                string pname = payment_dataGridView.SelectedRows[0].Cells[1].Value.ToString();
                string pdetail = payment_dataGridView.SelectedRows[0].Cells[2].Value.ToString();

                pid_textbox.Text = pid;
                pname_textbox.Text = pname;
                pdetail_textbox.Text = pdetail;
            }
        }
        /* ------------------------ end payment method ---------------------- */



        /* ---------------------- CarRental -------------------------- */

        private void clear_carrental_data()
        {
            Car_TBox.Text = "";
            RDay.Value = DateTime.Now;
            RRday.Value = DateTime.Now;
            Customer_Box.SelectedIndex = 0;
            Employee_Box.SelectedIndex = 0;
            Bill_Box.SelectedIndex = 0;
            Car_Box.SelectedIndex = 0;
        }

        private void load_car_rental_griddata_init()
        {
            string sql = "SELECT * FROM project.carrental";
            comm = new MySqlCommand(sql, con);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            da.Fill(ds, "carrental");
            CarRental_dataGridView.DataSource = ds.Tables["carrental"].DefaultView;
            CarRental_dataGridView.Columns[1].DefaultCellStyle.Format = "yyyy-MM-dd";
            CarRental_dataGridView.Columns[2].DefaultCellStyle.Format = "yyyy-MM-dd";
        }
        private void load_Customer_Box_init()
        {
            string sql = "SELECT distinct customer_name FROM project.customer";
            comm = new MySqlCommand(sql, con);

            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Customer_Box.Items.Add(reader.GetString("customer_name"));

            }
            reader.Close();

        }

        private void load_Employee_Box_init()
        {
            string sql = "SELECT distinct employee_name FROM project.employee";
            comm = new MySqlCommand(sql, con);

            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Employee_Box.Items.Add(reader.GetString("employee_name"));

            }
            reader.Close();

        }

        private void load_Bill_Box_init()
        {
            string sql = "SELECT distinct bill_id FROM project.bill";
            comm = new MySqlCommand(sql, con);

            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bill_Box.Items.Add(reader.GetString("bill_id"));

            }
            reader.Close();

        }

        private void load_Car_Box_init()
        {
            string sql = "SELECT distinct car_id FROM project.car";
            comm = new MySqlCommand(sql, con);

            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Car_Box.Items.Add(reader.GetString("car_id"));

            }
            reader.Close();

        }

        private void carrenral_insert_btn_Click(object sender, EventArgs e)
        {
            var c_id = Car_TBox.Text;
            string dob_RD1 = RDay.Value.ToString("yyyy-MM-dd");
            string dob_RD2 = RRday.Value.ToString("yyyy-MM-dd");
            var SelectedCus = Customer_Box.SelectedIndex;
            var c_cus = SelectedCus.ToString();
            var SelectedEmp = Employee_Box.SelectedIndex;
            var c_emp = SelectedEmp.ToString();
            var SelectedBil = Bill_Box.SelectedIndex;
            var c_bil = SelectedBil.ToString();
            var SelectedCar = Car_Box.SelectedIndex;
            var c_car = SelectedCar.ToString();

            comm = con.CreateCommand();
            comm.CommandText = "INSERT INTO project.`carrental`(carRental_id,rentDate,returnDate,customer_id,employee_id,bill_id,car_id) " +
                "VALUES " + " (@carRental_id,@rentDate,@returnDate,@customer_id,@employee_id,@bill_id,@car_id)";
            comm.Parameters.AddWithValue("@carRental_id", c_id);
            comm.Parameters.AddWithValue("@rentDate", dob_RD1);
            comm.Parameters.AddWithValue("@returnDate", dob_RD2);
            comm.Parameters.AddWithValue("@customer_id", c_cus);
            comm.Parameters.AddWithValue("@employee_id", c_emp);
            comm.Parameters.AddWithValue("@bill_id", c_bil);
            comm.Parameters.AddWithValue("@car_id", c_car);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Save Data Completed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_car_rental_griddata_init();
            clear_carrental_data();
        }

        private void carrenral_update_btn_Click(object sender, EventArgs e)
        {
            var c_id = Car_TBox.Text;
            string dob_RD1 = RDay.Value.ToString("yyyy-MM-dd");
            string dob_RD2 = RRday.Value.ToString("yyyy-MM-dd");

            var SelectedCus = Customer_Box.SelectedIndex;
            var c_cus = SelectedCus.ToString();
            var SelectedEmp = Employee_Box.SelectedIndex;
            var c_emp = SelectedEmp.ToString();
            var SelectedBil = Bill_Box.SelectedIndex;
            var c_bil = SelectedBil.ToString();
            var SelectedCar = Car_Box.SelectedIndex;
            var c_car = SelectedCar.ToString();

            comm = con.CreateCommand();
            comm.CommandText = "UPDATE project_database.`carrental`" +
                "SET `rentDate`=@rentDate,`returnDate`=@returnDate,`customer_id`=@customer_id,`employee_id`=@employee_id,`bill_id`=@bill_id,`car_id`=@car_id " +
                "WHERE `carRental_id` = @carRental_id";

            comm.Parameters.AddWithValue("@carRental_id", c_id);
            comm.Parameters.AddWithValue("@rentDate", dob_RD1);
            comm.Parameters.AddWithValue("@returnDate", dob_RD2);
            comm.Parameters.AddWithValue("@customer_id", c_cus);
            comm.Parameters.AddWithValue("@employee_id", c_emp);
            comm.Parameters.AddWithValue("@bill_id", c_bil);
            comm.Parameters.AddWithValue("@car_id", c_car);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Save Data Completed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_car_rental_griddata_init();
            clear_carrental_data();
        }

        private void carrenral_delete_btn_Click(object sender, EventArgs e)
        {
            var c_id = Car_TBox.Text;
            comm = con.CreateCommand();
            comm.CommandText = "DELETE FROM project.`carrental`" +
            "WHERE `carRental_id` = @carRental_id";

            comm.Parameters.AddWithValue("@carRental_id", c_id);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Save Data Completed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_car_rental_griddata_init();
            clear_carrental_data();
        }

        private void carrenral_clear_btn_Click(object sender, EventArgs e)
        {
            clear_carrental_data();
        }

        private void CarRental_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CarRental_dataGridView.SelectedRows.Count > 0)
            {
                string c_id = CarRental_dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                string dob_RD1 = CarRental_dataGridView.SelectedRows[0].Cells[1].Value.ToString();
                string dob_RD2 = CarRental_dataGridView.SelectedRows[0].Cells[2].Value.ToString();
                string c_cus = CarRental_dataGridView.SelectedRows[0].Cells[3].Value.ToString();
                string c_emp = CarRental_dataGridView.SelectedRows[0].Cells[4].Value.ToString();
                string c_bil = CarRental_dataGridView.SelectedRows[0].Cells[5].Value.ToString();
                string c_car = CarRental_dataGridView.SelectedRows[0].Cells[6].Value.ToString();

                Car_TBox.Text = c_id;
                if (c_cus != "")
                {
                    Customer_Box.SelectedIndex = int.Parse(c_cus);
                }

                if (c_emp != "")
                {
                    Employee_Box.SelectedIndex = int.Parse(c_emp);
                }

                if (c_bil != "")
                {
                    Bill_Box.SelectedIndex = int.Parse(c_bil);
                }

                if (c_car != "")
                {
                    Car_Box.SelectedIndex = int.Parse(c_car);
                }

                if (dob_RD1 != "")
                {
                    DateTime dateBirth = Convert.ToDateTime(dob_RD1, null);
                    string str = dateBirth.Year + "-" + dateBirth.Month + "-" + dateBirth.Day;
                    RDay.Value = DateTime.ParseExact(str, "yyyy-M-d", null);
                }
                if (dob_RD2 != "")
                {
                    DateTime dateBirth2 = Convert.ToDateTime(dob_RD2, null);
                    string str2 = dateBirth2.Year + "-" + dateBirth2.Month + "-" + dateBirth2.Day;
                    RRday.Value = DateTime.ParseExact(str2, "yyyy-M-d", null);
                }

            }
        }
        /* ------------------------ end car rental ---------------------- */






        /* ---------------------- customer -------------------------- */

        private void load_customer_griddata_init()
        {
            string sql = "SELECT * FROM project.customer";
            comm = new MySqlCommand(sql, con);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            da.Fill(ds, "customer");
            customer_dataGridView.DataSource = ds.Tables["customer"].DefaultView;
            customer_dataGridView.Columns[3].DefaultCellStyle.Format = "yyyy-MM-dd";
            customer_dataGridView.Columns[10].DefaultCellStyle.Format = "yyyy-MM-dd";

        }

        private void clear_customer_data()
        {
            customer_id.Text = "";
            customer_name.Text = "";
            driving_licence_number.Text = "";
            job.Text = "";
            address.Text = "";
            id_card_number.Text = "";
            phone_number.Text = "";
            email.Text = "";
            customer_dob.Value = DateTime.Now;
            register_date.Value = DateTime.Now;
            female_radiobtn.Checked = false;
            male_radiobtn.Checked = false;
        }

        private void customer_insert_btn_Click(object sender, EventArgs e)
        {
            var c_id = customer_id.Text;
            var c_name = customer_name.Text;
            var d_number = driving_licence_number.Text;
            var j = job.Text;
            var add = address.Text;
            var id_card = id_card_number.Text;
            var phone = phone_number.Text;
            var mail = email.Text;
            string DOB = customer_dob.Value.ToString("yyyy-MM-dd");
            string regis_date = register_date.Value.ToString("yyyy-MM-dd");
            var gender = "";
            if (male_radiobtn.Checked)
            {
                gender = male_radiobtn.Text;
            }
            else
            {
                gender = female_radiobtn.Text;
            }

            comm = con.CreateCommand();
            comm.CommandText = "INSERT INTO `project`.`customer` (`customer_id`, `driving_licence_number`, `customer_name`, `dob`, `gender`, `address`, `phone_number`, `job`, `id_card_number`, `email`, `register_date`) " +
                "VALUES " + "(@customer_id, @driving_licence_number, @customer_name, @dob, @gender, @address, @phone_number, @job, @id_card_number, @email, @register_date)";

            comm.Parameters.AddWithValue("@customer_id", c_id);
            comm.Parameters.AddWithValue("@driving_licence_number", d_number);
            comm.Parameters.AddWithValue("@customer_name", c_name);
            comm.Parameters.AddWithValue("@dob", DOB);
            comm.Parameters.AddWithValue("@gender", gender);
            comm.Parameters.AddWithValue("@address", add);
            comm.Parameters.AddWithValue("@job", j);
            comm.Parameters.AddWithValue("@phone_number", phone);
            comm.Parameters.AddWithValue("@id_card_number", id_card);
            comm.Parameters.AddWithValue("@email", mail);
            comm.Parameters.AddWithValue("@register_date", regis_date);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Save Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_customer_griddata_init();
            clear_customer_data();
        }

        private void customer_update_btn_Click(object sender, EventArgs e)
        {
            var c_id = customer_id.Text;
            var c_name = customer_name.Text;
            var d_number = driving_licence_number.Text;
            var j = job.Text;
            var add = address.Text;
            var id_card = id_card_number.Text;
            var phone = phone_number.Text;
            var mail = email.Text;
            var DOB = customer_dob.Value.ToString("yyyy-MM-dd");
            var regis = register_date.Value.ToString("yyyy-MM-dd");
            var gender = "";
            if (male_radiobtn.Checked)
            {
                gender = male_radiobtn.Text;
            }
            else
            {
                gender = female_radiobtn.Text;
            }



            comm = con.CreateCommand();
            comm.CommandText = "UPDATE `project`.`customer` " +
                "SET `driving_licence_number`=@driving_licence_number, `customer_name`=@customer_name, `dob`=@dob, `gender`=@gender, `address`=@address, `phone_number`=@phone_number, `job`=@job, `id_card_number`=@id_card_number, `email`=@email, `register_date`=@register_date " +
                "WHERE `customer_id` = @customer_id";

            comm.Parameters.AddWithValue("@customer_id", c_id);
            comm.Parameters.AddWithValue("@driving_licence_number", d_number);
            comm.Parameters.AddWithValue("@customer_name", c_name);
            comm.Parameters.AddWithValue("@dob", DOB);
            comm.Parameters.AddWithValue("@gender", gender);
            comm.Parameters.AddWithValue("@address", add);
            comm.Parameters.AddWithValue("@phone_number", phone);
            comm.Parameters.AddWithValue("@job", j);
            comm.Parameters.AddWithValue("@id_card_number", id_card);
            comm.Parameters.AddWithValue("@email", mail);
            comm.Parameters.AddWithValue("@register_date", regis);



            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Update Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_customer_griddata_init();
            clear_customer_data();
        }

        private void customer_delete_btn_Click(object sender, EventArgs e)
        {
            var c_id = customer_id.Text;

            comm = con.CreateCommand();
            comm.CommandText = @"DELETE FROM project.customer
				WHERE customer_id = @customer_id";

            comm.Parameters.AddWithValue("@customer_id", c_id);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Delete Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_customer_griddata_init();
            clear_customer_data();
        }

        private void customer_clear_btn_Click(object sender, EventArgs e)
        {
            clear_customer_data();
        }

        private void customer_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (customer_dataGridView.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                string cus_id = customer_dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                string driving_id = customer_dataGridView.SelectedRows[0].Cells[1].Value.ToString();
                string cus_name = customer_dataGridView.SelectedRows[0].Cells[2].Value.ToString();
                string DOB = customer_dataGridView.SelectedRows[0].Cells[3].Value.ToString();
                string c_gender = customer_dataGridView.SelectedRows[0].Cells[4].Value.ToString();
                string add = customer_dataGridView.SelectedRows[0].Cells[5].Value.ToString();
                string phone = customer_dataGridView.SelectedRows[0].Cells[6].Value.ToString();
                string jjob = customer_dataGridView.SelectedRows[0].Cells[7].Value.ToString();
                string id_card = customer_dataGridView.SelectedRows[0].Cells[8].Value.ToString();
                string c_email = customer_dataGridView.SelectedRows[0].Cells[9].Value.ToString();
                string regis = customer_dataGridView.SelectedRows[0].Cells[10].Value.ToString();

                customer_id.Text = cus_id;
                driving_licence_number.Text = driving_id;
                customer_name.Text = cus_name;

                if (DOB != "")
                {
                    DateTime dateBirth = Convert.ToDateTime(DOB, null);
                    string str = dateBirth.Year + "-" + dateBirth.Month + "-" + dateBirth.Day;
                    customer_dob.Value = DateTime.ParseExact(str, "yyyy-M-d", null);
                }

                if (c_gender.Equals("male"))
                {
                    male_radiobtn.Checked = true;
                }
                else { 
                    female_radiobtn.Checked = true; 
                }

                address.Text = add;
                phone_number.Text = phone;
                job.Text = jjob;
                id_card_number.Text = id_card;
                email.Text = c_email;

                if (regis != "")
                {
                    DateTime dateBirth2 = Convert.ToDateTime(DOB, null);
                    string str1 = dateBirth2.Year + "-" + dateBirth2.Month + "-" + dateBirth2.Day;
                    customer_dob.Value = DateTime.ParseExact(str1, "yyyy-M-d", null);
                }
            }
        }

        /* ------------------------ end customer ---------------------- */








        /* ------------------------ Employee ---------------------- */

        private void load_employee_griddata_init()
        {
            string sql = "SELECT * FROM project.employee";
            comm = new MySqlCommand(sql, con);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            da.Fill(ds, "employee");
            employee_dataGridView.DataSource = ds.Tables["employee"].DefaultView;
        }

        private void clear_employee_data()
        {
            eid.Text = "";
            ename.Text = "";
            eposition.Text = "";
        }

        private void employee_insert_btn_Click(object sender, EventArgs e)
        {
            var e_id = eid.Text;
            var e_name = ename.Text;
            var po = eposition.Text;

            comm = con.CreateCommand();
            comm.CommandText = "INSERT INTO `project`.`employee` (`employee_id`, `employee_name`, `position` ) " +
                "VALUES " + "(@employee_id, @employee_name, @position)";

            comm.Parameters.AddWithValue("@employee_id", e_id);
            comm.Parameters.AddWithValue("@employee_name", e_name);
            comm.Parameters.AddWithValue("@position", po);



            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Save Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            load_employee_griddata_init();
            clear_employee_data();
        }

        private void employee_update_btn_Click(object sender, EventArgs e)
        {
            var e_id = eid.Text;
            var e_name = ename.Text;
            var po = eposition.Text;

            comm = con.CreateCommand();
            comm.CommandText = "UPDATE `project`.`employee`" +
                "SET `employee_name`=@employee_name, `position`=@position " +
                "WHERE `employee_id` = @employee_id";

            comm.Parameters.AddWithValue("@employee_id", e_id);
            comm.Parameters.AddWithValue("@employee_name", e_name);
            comm.Parameters.AddWithValue("@position", po);


            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Update Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_employee_griddata_init();
            clear_employee_data();
        }

        private void employee_delete_btn_Click(object sender, EventArgs e)
        {
            var e_id = eid.Text;

            comm = con.CreateCommand();
            comm.CommandText = @"DELETE FROM project.employee
				WHERE employee_id = @employee_id";

            comm.Parameters.AddWithValue("@employee_id", e_id);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Delete Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_employee_griddata_init();
            clear_employee_data();
        }

        private void employee_clear_btn_Click(object sender, EventArgs e)
        {
            clear_employee_data();
        }

        private void employee_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (employee_dataGridView.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                string e_id = employee_dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                string e_name = employee_dataGridView.SelectedRows[0].Cells[1].Value.ToString();
                string e_position = employee_dataGridView.SelectedRows[0].Cells[2].Value.ToString();

                eid.Text = e_id;
                ename.Text = e_name;
                eposition.Text = e_position;


            }
        }

        /* ---------------------------- end employee ------------------------------ */








        /* -------------------------------- car --------------------------- */



        private void load_car_griddata_init()
        {
            string sql = "SELECT * FROM project.car";
            comm = new MySqlCommand(sql, con);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            da.Fill(ds, "car");
            car_dataGridView.DataSource = ds.Tables["car"].DefaultView;
        }
        private void clear_car_data()
        {
            carid_txtbox.Text = "";
            licenseplate_txtbox.Text = "";
            province_txtbox.Text = "";
            vin_txtbox.Text = "";
            carmodel_txtbox.Text = "";
            carbrand_txtbox.Text = "";
            status_txtbox.Text = "";
        }


        private void clear_Click(object sender, EventArgs e)
        {
            clear_car_data();
        }

        private void insert_btn_Click(object sender, EventArgs e)
        {

            var car_id = carid_txtbox.Text;
            var license_plate = licenseplate_txtbox.Text;
            var province = province_txtbox.Text;
            var vin_num = vin_txtbox.Text;
            var car_model = carmodel_txtbox.Text;
            var car_brand = carbrand_txtbox.Text;
            var status = status_txtbox.Text;

            comm = con.CreateCommand();
            comm.CommandText = "INSERT INTO `project`.`car` (`car_id`, `license_plate`, `province`, `VIN_num`, `car_model`, `car_brand`,`status`) " +
                "VALUES " + "(@car_id, @license_plate, @province, @VIN_num, @car_model, @car_brand, @status)";

            comm.Parameters.AddWithValue("@car_id", car_id);
            comm.Parameters.AddWithValue("@license_plate", license_plate);
            comm.Parameters.AddWithValue("@province", province);
            comm.Parameters.AddWithValue("@VIN_num", vin_num);
            comm.Parameters.AddWithValue("@car_model", car_model);
            comm.Parameters.AddWithValue("@car_brand", car_brand);
            comm.Parameters.AddWithValue("@status", status);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Save Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_car_griddata_init();
            clear_car_data();
        }

        private void update_btn_Click(object sender, EventArgs e)
        {
            var car_id = carid_txtbox.Text;
            var license_plate = licenseplate_txtbox.Text;
            var province = province_txtbox.Text;
            var vin_num = vin_txtbox.Text;
            var car_model = carmodel_txtbox.Text;
            var car_brand = carbrand_txtbox.Text;
            var status = status_txtbox.Text;

            comm = con.CreateCommand();
            comm.CommandText = "UPDATE `project`.`car` " + "SET `car_id`=@car_id, `license_plate`=@license_plate, `province`=@province, `VIN_num`=@VIN_num, `car_model`=@car_model, `car_brand`=@car_brand,`status`=@status " +
                "WHERE `car_id`=@car_id";

            comm.Parameters.AddWithValue("@car_id", car_id);
            comm.Parameters.AddWithValue("@license_plate", license_plate);
            comm.Parameters.AddWithValue("@province", province);
            comm.Parameters.AddWithValue("@VIN_num", vin_num);
            comm.Parameters.AddWithValue("@car_model", car_model);
            comm.Parameters.AddWithValue("@car_brand", car_brand);
            comm.Parameters.AddWithValue("@status", status);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Save Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_car_griddata_init();
            clear_car_data();
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            var car_id = carid_txtbox.Text;

            comm = con.CreateCommand();
            comm.CommandText = "DELETE FROM  `project`.`car`" +
                "WHERE `car_id`=@car_id";

            comm.Parameters.AddWithValue("@car_id", car_id);

            try
            {
                int rowsAffected = comm.ExecuteNonQuery();
                MessageBox.Show("Save Data Completed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            load_car_griddata_init();
            clear_car_data();
        }

        private void car_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string car_id = car_dataGridView.SelectedRows[0].Cells[0].Value.ToString();
            string license_plate = car_dataGridView.SelectedRows[0].Cells[1].Value.ToString();
            string province = car_dataGridView.SelectedRows[0].Cells[2].Value.ToString();
            string vin_num = car_dataGridView.SelectedRows[0].Cells[3].Value.ToString();
            string car_model = car_dataGridView.SelectedRows[0].Cells[4].Value.ToString();
            string car_brand = car_dataGridView.SelectedRows[0].Cells[5].Value.ToString();
            string status = car_dataGridView.SelectedRows[0].Cells[6].Value.ToString();

            carid_txtbox.Text = car_id;
            licenseplate_txtbox.Text = license_plate;
            province_txtbox.Text = province;
            vin_txtbox.Text = vin_num;
            carmodel_txtbox.Text = car_model;
            carbrand_txtbox.Text = car_brand;
            status_txtbox.Text = status;
        }

        
    }
}
