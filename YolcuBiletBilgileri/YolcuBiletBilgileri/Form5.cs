using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace YolcuBiletBilgileri
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private int biletId = -1;

        private void Button2_Click(object sender, EventArgs e)
        {
            if (biletId != -1 && MessageBox.Show("Seçilen bilet silinsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Bilet WHERE Id=@Id", Form1.baglanti);
                cmd.Parameters.AddWithValue("@Id", biletId);
                Form1.baglanti.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Bilet iptal edildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
            finally
            {
                Form1.baglanti.Close();
                VerileriGetir();
                biletId = -1;
                toolStripStatusLabel2.Text = "-";
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            VerileriGetir();
        }

        public void VerileriGetir()
        {
            try
            {
                Form1.baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Bilet", Form1.baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
            finally
            {
                Form1.baglanti.Close();
            }
        }

        private void DataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            biletId = Convert.ToInt32(dataGridView1.CurrentRow?.Cells[0].Value);
            toolStripStatusLabel2.Text = biletId.ToString();
            button2.Enabled = true;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                VerileriGetir();
            }

            try
            {
                Form1.baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Bilet WHERE SeferId LIKE @Ara OR Tc LIKE @Ara OR Adi LIKE @Ara OR Soyadi LIKE @Ara OR Yas LIKE @Ara OR Cinsiyet LIKE @Ara OR KoltukNo LIKE @Ara", Form1.baglanti);
                da.SelectCommand.Parameters.AddWithValue("@Ara", $"%{textBox1.Text.ToLower()}%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
            finally
            {
                Form1.baglanti.Close();
                button2.Enabled = false;
            }
        }
    }
}