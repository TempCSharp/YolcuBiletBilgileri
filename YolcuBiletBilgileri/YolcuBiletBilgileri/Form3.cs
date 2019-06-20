using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace YolcuBiletBilgileri
{
    public partial class Form3 : Form
    {
        public int seferId = -1;
        public string koltukNo = "";
        public string fiyat;

        public Form3()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("TC Kimlik No girin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (String.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Müşteri adını girin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (String.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Müşteri soyadını girin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (String.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("Müşteri yaşını girin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (String.IsNullOrWhiteSpace(textBox5.Text))
                {
                    MessageBox.Show("Müşteri telefonunu girin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Cinsiyet boş olamaz!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (label8.Text == "-----" || label10.Text == "-----" || label12.Text == "-----")
                {
                    MessageBox.Show("Lütfen bir sefer seçin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (seferId == -1 || koltukNo == "")
                {
                    Form4 frm = new Form4();
                    frm.tarihvesaat = dataGridView1.CurrentRow?.Cells[5].Value + " - " + dataGridView1.CurrentRow?.Cells[4].Value;
                    frm.neredennereye = dataGridView1.CurrentRow?.Cells[2].Value + " - " + dataGridView1.CurrentRow?.Cells[3].Value;
                    frm.ShowDialog();
                    return;
                }

                Form1.baglanti.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Bilet (SeferId,Tc,Adi,Soyadi,Yas,Telefon,Cinsiyet,KoltukNo) Values(@SeferId,@Tc,@Adi,@Soyadi,@Yas,@Telefon,@Cinsiyet,@KoltukNo)", Form1.baglanti);
                cmd.Parameters.AddWithValue("@SeferId", seferId);
                cmd.Parameters.AddWithValue("@Tc", textBox1.Text);
                cmd.Parameters.AddWithValue("@Adi", textBox2.Text);
                cmd.Parameters.AddWithValue("@Soyadi", textBox3.Text);
                cmd.Parameters.AddWithValue("@Yas", textBox4.Text);
                cmd.Parameters.AddWithValue("@Telefon", textBox5.Text);
                cmd.Parameters.AddWithValue("@Cinsiyet", comboBox1.Text);
                cmd.Parameters.AddWithValue("@KoltukNo", koltukNo);
                cmd.ExecuteScalar();

                MessageBox.Show("Yeni sefer eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Form1.baglanti.Close();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            VerileriGetir();
        }

        public void Temizle()
        {
            seferId = -1;
            koltukNo = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.SelectedIndex = -1;
        }

        public void VerileriGetir()
        {
            try
            {
                Form1.baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Sefer", Form1.baglanti);
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

            seferId = Convert.ToInt32(dataGridView1.CurrentRow?.Cells[0].Value);

            switch (dataGridView1.CurrentRow?.Cells[6].Value)
            {
                case "Ekonomik":
                    fiyat = "60";
                    break;

                case "Business":
                    fiyat = "80";
                    break;

                case "VIP":
                    fiyat = "100";
                    break;

                default:
                    fiyat = "0";
                    break;
            }

            label8.Text = dataGridView1.CurrentRow?.Cells[1].Value + " (" + dataGridView1.CurrentRow?.Cells[6].Value + " - " + fiyat + " TL)";
            label10.Text = dataGridView1.CurrentRow?.Cells[5].Value + " - " + dataGridView1.CurrentRow?.Cells[4].Value;
            label12.Text = dataGridView1.CurrentRow?.Cells[2].Value + " - " + dataGridView1.CurrentRow?.Cells[3].Value;
        }
    }
}