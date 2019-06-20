using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace YolcuBiletBilgileri
{
    public partial class Form1 : Form
    {
        public static SqlConnection baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\YolcuBilet.mdf;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Kullanıcı adını girin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (String.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Kullanıcı adını girin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                baglanti.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Kullanici WHERE KullaniciAdi=@KullaniciAdi AND Sifre=@Sifre", baglanti);
                command.Parameters.AddWithValue("@KullaniciAdi", textBox1.Text);
                command.Parameters.AddWithValue("@Sifre", textBox2.Text);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı veya şifreniz yanlış!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }
    }
}