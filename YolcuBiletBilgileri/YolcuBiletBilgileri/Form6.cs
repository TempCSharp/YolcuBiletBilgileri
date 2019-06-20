using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace YolcuBiletBilgileri
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private int seferId = -1;

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            string[] iller = "Adana,Adıyaman,Afyon,Ağrı,Amasya,Ankara,Antalya,Artvin,Aydın,Balıkesir,Bilecik,Bingöl,Bitlis,Bolu,Burdur,Bursa,Çanakkale,Çankırı,Çorum,Denizli,Diyarbakır,Edirne,Elazığ,Erzincan,Erzurum,Eskişehir,Gaziantep,Giresun,Gümüşhane,Hakkari,Hatay,Isparta,Mersin,İstanbul,İzmir,Kars,Kastamonu,Kayseri,Kırklareli,Kırşehir,Kocaeli,Konya,Kütahya,Malatya,Manisa,Kahramanmaraş,Mardin,Muğla,Muş,Nevşehir,Niğde,Ordu,Rize,Sakarya,Samsun,Siirt,Sinop,Sivas,Tekirdağ,Tokat,Trabzon,Tunceli,Şanlıurfa,Uşak,Van,Yozgat,Zonguldak,Aksaray,Bayburt,Karaman,Kırıkkale,Batman,Şırnak,Bartın,Ardahan,Iğdır,Yalova,Karabük,Kilis,Osmaniye,Düzce".Split(',');

            comboBox1.Items.AddRange(iller);
            comboBox2.Items.AddRange(iller);

            VerileriGetir();

            dateTimePicker1.Value = DateTime.Now;
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

        public void Temizle()
        {
            seferId = -1;
            textBox1.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            comboBox4.SelectedIndex = -1;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Araç adını girin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Aracın nereden kalkacağını seçin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Aracın nereye gideceğini seçin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (comboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("Aracın kalkış saatini seçin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (comboBox4.SelectedIndex == -1)
            {
                MessageBox.Show("Araç türünü seçin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Form1.baglanti.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Sefer(AracAdi, Nereden, Nereye, Saat, Tarih, AracTuru) VALUES(@AracAdi, @Nereden, @Nereye, @Saat, @Tarih, @AracTuru)", Form1.baglanti);
                cmd.Parameters.AddWithValue("@AracAdi", textBox1.Text);
                cmd.Parameters.AddWithValue("@Nereden", comboBox1.Text);
                cmd.Parameters.AddWithValue("@Nereye", comboBox2.Text);
                cmd.Parameters.AddWithValue("@Saat", comboBox3.Text);
                cmd.Parameters.AddWithValue("@Tarih", dateTimePicker1.Value.ToShortDateString());
                cmd.Parameters.AddWithValue("@AracTuru", comboBox4.Text);
                cmd.ExecuteScalar();

                MessageBox.Show("Yeni sefer eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void DataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            seferId = (int)dataGridView1.CurrentRow?.Cells[0].Value;
            textBox1.Text = dataGridView1.CurrentRow?.Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow?.Cells[2].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow?.Cells[3].Value.ToString();
            comboBox3.Text = dataGridView1.CurrentRow?.Cells[4].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow?.Cells[5].Value.ToString();
            comboBox4.Text = dataGridView1.CurrentRow?.Cells[6].Value.ToString();

            button3.Enabled = true;
            button4.Enabled = true;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seçilen sefer silinsin mi? Eğer seferi silerseniz satın alınan biletler de silinecektir", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Sefer WHERE Id=@Id", Form1.baglanti);
                cmd.Parameters.AddWithValue("@Id", seferId);
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
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Araç adını girin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Aracın nereden kalkacağını seçin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Aracın nereye gideceğini seçin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (comboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("Aracın kalkış saatini seçin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (comboBox4.SelectedIndex == -1)
            {
                MessageBox.Show("Araç türünü seçin!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Form1.baglanti.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Sefer SET AracAdi=@AracAdi, Nereden=@Nereden, Nereye=@Nereye, Saat=@Saat, Tarih=@Tarih, AracTuru=@AracTuru WHERE Id=@Id", Form1.baglanti);
                cmd.Parameters.AddWithValue("@AracAdi", textBox1.Text);
                cmd.Parameters.AddWithValue("@Nereden", comboBox1.Text);
                cmd.Parameters.AddWithValue("@Nereye", comboBox2.Text);
                cmd.Parameters.AddWithValue("@Saat", comboBox3.Text);
                cmd.Parameters.AddWithValue("@Tarih", dateTimePicker1.Value.ToShortDateString());
                cmd.Parameters.AddWithValue("@AracTuru", comboBox4.Text);
                cmd.Parameters.AddWithValue("@Id", seferId);
                cmd.ExecuteScalar();

                MessageBox.Show("Sefer güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox3.Text))
            {
                VerileriGetir();
            }

            try
            {
                Form1.baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SEFER WHERE AracAdi LIKE @Ara OR Nereden LIKE @Ara OR Nereye LIKE @Ara OR Saat LIKE @Ara OR Tarih LIKE @Ara OR AracTuru LIKE @Ara", Form1.baglanti);
                da.SelectCommand.Parameters.AddWithValue("@Ara", $"%{textBox3.Text.ToLower()}%");
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
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }
    }
}