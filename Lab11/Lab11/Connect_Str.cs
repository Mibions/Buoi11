using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Lab11
{
    public class Connect_Str
    {
        private string connectionString;
        private SqlConnection connection;
        private string chuoiKetNoi = @"Data Source=PIOTISK\SQLEXPRESS_19;Initial Catalog=SUGONG;Integrated Security=True";
        public string GetConnection()
        {
            return chuoiKetNoi;
        }
        public Connect_Str()
        {
            connectionString = chuoiKetNoi;
            connection = new SqlConnection(connectionString);
        }
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public void CloseConnection()
        {
            connection.Close();
        }

        public DataTable Read_Database(string cmd)
        {
            OpenConnection();
            DataTable da = new DataTable();
            try
            {
                SqlCommand sc = new SqlCommand(cmd, connection);
                SqlDataAdapter sda = new SqlDataAdapter(sc);
                sda.Fill(da);
            }
            catch (Exception)
            {
                da = null;
            }
            CloseConnection();
            return da;
        }
        public void Add_Database(Product pr)
        {
            DataTable da = new DataTable();
            string query = "INSERT INTO PRODUCT (ID_PRODUCT, NAME_PRODUCT, PRICE_PRODUCT, DETAIL_PRODUCT) " +
                           "VALUES (@ID_PRODUCT, @NAME_PRODUCT, @PRICE_PRODUCT, @DETAIL_PRODUCT)";

            OpenConnection();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ID_PRODUCT", pr.ID_PRODUCT);
                command.Parameters.AddWithValue("@NAME_PRODUCT", pr.NAME_PRODUCT);
                command.Parameters.AddWithValue("@PRICE_PRODUCT", pr.PRICE_PRODUCT);
                command.Parameters.AddWithValue("@DETAIL_PRODUCT", pr.DETAIL_PRODUCT);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Thêm dữ liệu thành công");
                }
                else
                    MessageBox.Show("Lỗi không thể thêm dữ liệu");
            }
            CloseConnection();
        }

        public void DeleteProduct(Product pr)
        {
            try
            {
                string query = "DELETE FROM PRODUCT WHERE ID_PRODUCT = @ID_PRODUCT";

                OpenConnection();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@ID_PRODUCT", pr.ID_PRODUCT);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        MessageBox.Show("Xóa dữ liệu thành công");
                    else
                        MessageBox.Show("Không tìm thấy dữ liệu để xóa");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
            CloseConnection();
        }

        public void UpdateProduct(Product pr)
        {
            try
            {
                string query = "UPDATE PRODUCT SET NAME_PRODUCT = @NAME_PRODUCT, " +
                               "PRICE_PRODUCT = @PRICE_PRODUCT, DETAIL_PRODUCT = @DETAIL_PRODUCT " +
                               "WHERE ID_PRODUCT = @ID_PRODUCT";

                OpenConnection();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_PRODUCT", pr.ID_PRODUCT);
                    command.Parameters.AddWithValue("@NAME_PRODUCT", pr.NAME_PRODUCT);
                    command.Parameters.AddWithValue("@PRICE_PRODUCT", pr.PRICE_PRODUCT);
                    command.Parameters.AddWithValue("@DETAIL_PRODUCT", pr.DETAIL_PRODUCT);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        MessageBox.Show("Cập nhật dữ liệu thành công");
                    else
                        MessageBox.Show("Không tìm thấy dữ liệu để cập nhật");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

    }
}