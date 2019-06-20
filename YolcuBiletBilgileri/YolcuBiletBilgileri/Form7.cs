using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace YolcuBiletBilgileri
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private int kullaniciId = -1;

        public void VerileriGetir()
        {
            try
            {
                Form1.baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Kullanici", Form1.baglanti);
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

        public void Temizle()
        {
            kullaniciId = -1;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Kullanıcı adını girin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Şifeyi girin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Form1.baglanti.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Kullanici(KullaniciAdi, Sifre) VALUES(@KullaniciAdi, @Sifre)", Form1.baglanti);
                cmd.Parameters.AddWithValue("@KullaniciAdi", textBox1.Text);
                cmd.Parameters.AddWithValue("@Sifre", textBox2.Text);
                cmd.ExecuteScalar();

                MessageBox.Show("Yeni kullanıcı eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
            finally
            {
                Form1.baglanti.Close();
                VerileriGetir();
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            VerileriGetir();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 1)
            {
                MessageBox.Show("Bu kullanıcıyı silemezsiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Seçilen sefer silinsin mi?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Kullanici WHERE Id=@Id", Form1.baglanti);
                cmd.Parameters.AddWithValue("@Id", kullaniciId);
                Form1.baglanti.Open();
                cmd.ExecuteNonQuery();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
            finally
            {
                Form1.baglanti.Close();
                VerileriGetir();

                button2.Enabled = true;
                button3.Enabled = true;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Kullanıcı adını girin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Şifeyi girin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Form1.baglanti.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Kullanici SET KullaniciAdi=@KullaniciAdi, Sifre=@Sifre WHERE Id=@Id", Form1.baglanti);
                cmd.Parameters.AddWithValue("@KullaniciAdi", textBox1.Text);
                cmd.Parameters.AddWithValue("@Sifre", textBox2.Text);
                cmd.Parameters.AddWithValue("@Id", kullaniciId);
                cmd.ExecuteScalar();

                MessageBox.Show("Kullanıcı düzenlendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
            finally
            {
                Form1.baglanti.Close();
                VerileriGetir();

                button2.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            kullaniciId = Convert.ToInt32(dataGridView1.CurrentRow?.Cells[0].Value);
            textBox1.Text = dataGridView1.CurrentRow?.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow?.Cells[2].Value.ToString();

            button2.Enabled = true;
            button3.Enabled = true;
        }
    }
}