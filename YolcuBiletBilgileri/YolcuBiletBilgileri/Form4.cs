using System;
using System.Collections;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace YolcuBiletBilgileri
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        public string tarihvesaat = "-----";
        public string neredennereye = "-----";

        private int toplamtutar;
        private int bilet_fiyat;
        private string ucret;
        private ArrayList secilenKoltuklar = new ArrayList();
        private Form3 frm = (Form3)Application.OpenForms["Form3"];

        private void Form4_Load(object sender, EventArgs e)
        {
            label4.Text = tarihvesaat;
            label6.Text = neredennereye;
            bilet_fiyat = Convert.ToInt32(frm.fiyat);
            try
            {
                Form1.baglanti.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Bilet WHERE SeferId = @SeferId", Form1.baglanti);
                cmd.Parameters.AddWithValue("@SeferId", frm.seferId);
                SqlDataReader oku = cmd.ExecuteReader();
                while (oku.Read())
                {
                    Controls.Find("button" + oku[8], true)[0].BackColor = Color.Red;
                }
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

        private void Button37_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button38_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.baglanti.Open();

                foreach (var id in secilenKoltuklar)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Bilet (SeferId,Tc,Adi,Soyadi,Yas,Telefon,Cinsiyet,KoltukNo) Values(@SeferId,@Tc,@Adi,@Soyadi,@Yas,@Telefon,@Cinsiyet,@KoltukNo)", Form1.baglanti);
                    cmd.Parameters.AddWithValue("@SeferId", frm.seferId);
                    cmd.Parameters.AddWithValue("@Tc", frm.textBox1.Text);
                    cmd.Parameters.AddWithValue("@Adi", frm.textBox2.Text);
                    cmd.Parameters.AddWithValue("@Soyadi", frm.textBox3.Text);
                    cmd.Parameters.AddWithValue("@Yas", frm.textBox4.Text);
                    cmd.Parameters.AddWithValue("@Telefon", frm.textBox5.Text);
                    cmd.Parameters.AddWithValue("@Cinsiyet", frm.comboBox1.Text);
                    cmd.Parameters.AddWithValue("@KoltukNo", id);
                    cmd.ExecuteNonQuery();

                    Controls.Find("button" + id, true)[0].BackColor = Color.Red;
                }

                MessageBox.Show(secilenKoltuklar.Count + "tane bilet satın alındı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Button1_Click(object sender, EventArgs e)
        {
            if (((Button)sender).BackColor == Color.Chartreuse)
            {
                ((Button)sender).BackColor = Color.Orange;
                if (!secilenKoltuklar.Contains(((Button)sender).Text))
                {
                    secilenKoltuklar.Add(((Button)sender).Text);
                }
                toplamtutar += bilet_fiyat;
                koltukYazdir();
            }
            else if (((Button)sender).BackColor == Color.Orange)
            {
                ((Button)sender).BackColor = Color.Chartreuse;
                if (secilenKoltuklar.Contains(((Button)sender).Text))
                {
                    secilenKoltuklar.Remove(((Button)sender).Text);
                    toplamtutar += bilet_fiyat;
                }
            }

            toolStripStatusLabel2.Text = toplamtutar + " TL";
        }

        private void koltukYazdir()
        {
            string koltuk = "";
            foreach (var i in secilenKoltuklar)
            {
                koltuk += i + ",";
            }
            if (secilenKoltuklar.Count >= 1)
            {
                koltuk = koltuk.Remove(koltuk.Length - 1, 1);
            }
            label2.Text = koltuk;
        }
    }
}