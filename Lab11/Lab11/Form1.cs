using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab11
{
    public partial class Form1 : Form
    {
        private Connect_Str cnt = new Connect_Str();
        public Form1()
        {
            InitializeComponent();
        }
        private void loaddata()
        {
            DataTable dt = cnt.Read_Database("SELECT ID_PRODUCT, NAME_PRODUCT, PRICE_PRODUCT, DETAIL_PRODUCT from PRODUCT");
            if (dt != null)
            {
                dataGridView1.DataSource = dt;
            }
            dataGridView1.Columns[0].HeaderText = "Mã";
            dataGridView1.Columns[1].HeaderText = "Tên";
            dataGridView1.Columns[2].HeaderText = "Giá";
            dataGridView1.Columns[3].HeaderText = "Chi Tiết";
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            dataGridView1.Enabled = true;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = 0;
            i = dataGridView1.CurrentRow.Index;
            textBox1.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
        }
        private void Show_Click(object sender, EventArgs e)
        {
            loaddata();
        }
        private void button_Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) ||
                string.IsNullOrEmpty(textBox2.Text) ||
                string.IsNullOrEmpty(textBox3.Text) ||
                string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Product pro = new Product()
                {
                    ID_PRODUCT = textBox1.Text.ToString(),
                    NAME_PRODUCT = textBox2.Text.ToString(),
                    PRICE_PRODUCT = float.Parse(textBox3.Text.ToString()),
                    DETAIL_PRODUCT = textBox4.Text.ToString(),
                };
                cnt.Add_Database(pro);
                Clear_Textbox();
            }
        }
        private void Delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) ||
                string.IsNullOrEmpty(textBox2.Text) ||
                string.IsNullOrEmpty(textBox3.Text) ||
                string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Product pro = new Product()
                {
                    ID_PRODUCT = textBox1.Text.ToString(),
                    NAME_PRODUCT = textBox2.Text.ToString(),
                    PRICE_PRODUCT = float.Parse(textBox3.Text.ToString()),
                    DETAIL_PRODUCT = textBox4.Text.ToString(),
                };
                cnt.DeleteProduct(pro);
                Clear_Textbox();
            }
        }
        private void Update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) ||
                string.IsNullOrEmpty(textBox2.Text) ||
                string.IsNullOrEmpty(textBox3.Text) ||
                string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Product pro = new Product()
                {
                    ID_PRODUCT = textBox1.Text.ToString(),
                    NAME_PRODUCT = textBox2.Text.ToString(),
                    PRICE_PRODUCT = float.Parse(textBox3.Text.ToString()),
                    DETAIL_PRODUCT = textBox4.Text.ToString(),
                };
                cnt.UpdateProduct(pro);
                Clear_Textbox();
            }
        }
        private void Refresh_Click(object sender, EventArgs e)
        {
            Clear_Textbox();
            loaddata();
        }
        private void Clear_Textbox()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
