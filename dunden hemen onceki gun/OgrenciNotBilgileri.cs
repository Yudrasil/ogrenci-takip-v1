using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace dunden_hemen_onceki_gun
{
    public partial class OgrenciNotBilgileri : Form
    {
        public OgrenciNotBilgileri()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0; Data Source = dunden hemen onceki gun.mdb");
        OleDbCommand komut1 = new OleDbCommand();
        OleDbCommand komut2 = new OleDbCommand();

        public void temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            comboBox1.SelectedIndex = -1;
        }
        void bagln()
        {
            if (baglanti.State == ConnectionState.Open)
            {
                baglanti.Close();
            }
                baglanti.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand kontrol = new OleDbCommand("select * from kimlik_bilgileri where ogr_no ='" + textBox1.Text + "'", baglanti);
            int sonuc = Convert.ToInt32(kontrol.ExecuteScalar());
            if (sonuc == 0)
            {
                MessageBox.Show("Öğrenci Kayıtlı Değildir !");
                temizle();
                baglanti.Close();
            }
            else
            {
                OleDbDataReader okuma;
                OleDbDataReader okuma2;
                komut1.Connection = baglanti;
                komut1.CommandText = "select * from kimlik_bilgileri where ogr_no=@no";
                komut1.Parameters.AddWithValue("@no", textBox1.Text);
                okuma = komut1.ExecuteReader();
                while (okuma.Read())
                {
                    label11.Text = okuma["ogr_adi"].ToString();
                    label12.Text = okuma["ogr_soyadi"].ToString();
                }
                komut2.Connection = baglanti;
                komut2.CommandText = "select * from ders_bilgileri where ogr_no=@no";
                komut2.Parameters.AddWithValue("@no", textBox1.Text);
                okuma2 = komut2.ExecuteReader();
                while (okuma2.Read())
                {
                    label13.Text = okuma2["ogr_sinif"].ToString();
                    comboBox1.Items.Add(okuma2["ders1"].ToString());
                    comboBox1.Items.Add(okuma2["ders2"].ToString());
                    comboBox1.Items.Add(okuma2["ders3"].ToString());
                    comboBox1.Items.Add(okuma2["ders4"].ToString());
                    comboBox1.Items.Add(okuma2["ders5"].ToString());
                }
            }
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bagln();
            string kontrol = "select * from not_bilgileri where ders_adi='" + comboBox1.SelectedItem.ToString() + "' ";
            OleDbCommand skont = new OleDbCommand(kontrol, baglanti);
            bool sonuc = Convert.ToBoolean(skont.ExecuteScalar());
            if (sonuc == true)
            {
                MessageBox.Show("Seçtiğiniz Derse Ait Not Bilgisi Vardır!");
            }
            else
            {
                OleDbCommand notkomut = new OleDbCommand();
                bagln();
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Lütfen Öğrenci Numarasını Giriniz!");
                    baglanti.Close();
                }
                else if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen Not Girilecek Dersi Seçiniz!");
                    baglanti.Close();
                }
                else if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
                {
                    MessageBox.Show("Lütfen Not Bilgisini Giriniz!");
                    baglanti.Close();
                }
                else
                {
                    notkomut.Connection = baglanti;
                    notkomut.CommandText = "insert into not_bilgileri(ogr_no,ders_adi, yazili1,yazili2,yazili3,sozlu1,sozlu2) values(@no,@dersad,@not1,@not2,@not3,@not4,@not5) ";
                    notkomut.Parameters.AddWithValue("@no", textBox1.Text);
                    notkomut.Parameters.AddWithValue("@dersad", comboBox1.SelectedItem.ToString());

                    if (Convert.ToInt32(textBox2.Text) < 0 || Convert.ToInt32(textBox2.Text) > 100)
                    {
                        MessageBox.Show("Lütfen Geçerli Bir Not Giriniz!");
                        baglanti.Close();
                    }
                    else
                    {
                        notkomut.Parameters.AddWithValue("@not1", textBox2.Text);
                    }
                    if (Convert.ToInt32(textBox3.Text) < 0 || Convert.ToInt32(textBox3.Text) > 100)
                    {
                        MessageBox.Show("Lütfen Geçerli Bir Not Giriniz!");
                        baglanti.Close();
                    }
                    else
                    {
                        notkomut.Parameters.AddWithValue("@not2", textBox3.Text);
                    }
                    if (Convert.ToInt32(textBox4.Text) < 0 || Convert.ToInt32(textBox4.Text) > 100)
                    {
                        MessageBox.Show("Lütfen Geçerli Bir Not Giriniz!");
                        baglanti.Close();
                    }
                    else
                    {
                        notkomut.Parameters.AddWithValue("@not3", textBox4.Text);
                    }
                    if (Convert.ToInt32(textBox5.Text) < 0 || Convert.ToInt32(textBox5.Text) > 100)
                    {
                        MessageBox.Show("Lütfen Geçerli Bir Not Giriniz!");
                        baglanti.Close();
                    }
                    else
                    {
                        notkomut.Parameters.AddWithValue("@not4", textBox5.Text);
                    }
                    if (Convert.ToInt32(textBox6.Text) < 0 || Convert.ToInt32(textBox6.Text) > 100)
                    {
                        MessageBox.Show("Lütfen Geçerli Bir Not Giriniz!");
                        baglanti.Close();
                    }
                    else
                    {
                        notkomut.Parameters.AddWithValue("@not5", textBox6.Text);
                    }
                    notkomut.ExecuteNonQuery();
                    MessageBox.Show("Öğrenci Notu Girildi!", "Başarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    baglanti.Close();
                }
            }
        }

        private void OgrenciNotBilgileri_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen Öğrenci Numarasını Giriniz!");
                baglanti.Close();
            }
            else
            {
                string ders = comboBox1.SelectedItem.ToString();
                OleDbCommand notkomut = new OleDbCommand();
                bagln();
                notkomut.Connection = baglanti;
                notkomut.CommandText = "delete from not_bilgileri where ders_adi = @dersadi";
                notkomut.Parameters.AddWithValue("@dersadi",ders);
                notkomut.ExecuteNonQuery();
                MessageBox.Show(ders+" İsimli Derse Ait Notlar Silindi!");
                baglanti.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbDataReader oku;
            OleDbCommand d = new OleDbCommand();
            bagln();
            d.Connection = baglanti;
            d.CommandText = "select yazili1,yazili2,yazili3,sozlu1,sozlu2 from not_bilgileri where ders_adi=@ad";
            string ders = comboBox1.SelectedItem.ToString();
            d.Parameters.AddWithValue("@ad",ders);
            oku = d.ExecuteReader();
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            while (oku.Read())
            {
                textBox2.Text = oku["yazili1"].ToString();
                textBox3.Text = oku["yazili2"].ToString();
                textBox4.Text = oku["yazili3"].ToString();
                textBox5.Text = oku["sozlu1"].ToString();
                textBox6.Text = oku["sozlu2"].ToString();
            }
            baglanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bagln();
            if (textBox1.Text=="")
            {
                MessageBox.Show("Lütfen Öğrenci Numarasını Giriniz!");
            }
            else if (textBox2.Text==""||textBox3.Text==""||textBox4.Text==""||textBox5.Text==""||textBox6.Text=="")
            {
                MessageBox.Show("Lütfen Notları Giriniz!");
            }
            else
            {
                OleDbCommand gunkomut = new OleDbCommand();
                gunkomut.Connection = baglanti;
                gunkomut.CommandText = "update not_bilgileri set yazili1='" + textBox2.Text + "', yazili2='" + textBox3.Text + "', yazili3='" + textBox4.Text + "', sozlu1='" + textBox5.Text + "', sozlu2='" + textBox6.Text + "' where ders_adi='" + comboBox1.SelectedItem.ToString() +"'  ";
                gunkomut.Parameters.AddWithValue("@no",textBox1.Text);
                gunkomut.ExecuteNonQuery();
                gunkomut.Dispose();
                MessageBox.Show("Güncellendi!");
                baglanti.Close();
            }
        }

        private void aNAMENÜToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }

        private void dERSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DersDurumu dersd = new DersDurumu();
            dersd.Show();
            this.Hide();
        }

        private void gENELDURUMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenelDurum geneld = new GenelDurum();
            geneld.Show();
            this.Hide();
        }
    }
}