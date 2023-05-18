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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0; Data Source = dunden hemen onceki gun.mdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbCommand komut2 = new OleDbCommand();
        OleDbCommand komut3 = new OleDbCommand();
        OleDbCommand komut4 = new OleDbCommand();
        //OleDbDataAdapter adaptor = new OleDbDataAdapter();
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen Öğrenci Adını Giriniz!");
                textBox1.Focus();
            }
            else
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Lütfen Öğrenci Soyadını Giriniz!");
                    textBox2.Focus();
                }
                else
                {
                    if (textBox3.Text == "")
                    {
                        MessageBox.Show("Lütfen Öğrencinin Yaşını Giriniz!");
                        textBox3.Focus();
                    }
                    else
                    {
                        if (comboBox1.SelectedIndex == -1)
                        {
                            MessageBox.Show("Lütfen Öğrencinin Cinsiyetini Seçiniz!");
                        }
                        else
                        {
                            if (textBox4.Text == "")
                            {
                                MessageBox.Show("Lütfen Öğrencinin Memleketini Giriniz!");
                            }
                            else
                            {
                                if (textBox5.Text == "")
                                {
                                    MessageBox.Show("Lütfen Annenin Adını Giriniz!");
                                }
                                else
                                {
                                    if (maskedTextBox1.Text == "")
                                    {
                                        MessageBox.Show("Lütfen Annenin Telefon Numarasını Giriniz!");
                                    }
                                    else
                                    {
                                        if (textBox6.Text == "")
                                        {
                                            MessageBox.Show("Lütfen Babanın Adını Giriniz!");
                                        }
                                        else
                                        {
                                            if (maskedTextBox2.Text == "")
                                            {
                                                MessageBox.Show("Lütfen Babanın Telefon Numarasını Giriniz!");
                                            }
                                            else
                                            {
                                                if (comboBox2.SelectedIndex == -1)
                                                {
                                                    MessageBox.Show("Lütfen Öğrencinin Velisini Seçiniz!");
                                                }
                                                else
                                                {
                                                    if (comboBox3.SelectedIndex == -1)
                                                    {
                                                        MessageBox.Show("Lütfen Öğrenicinin Sınıfını Seçiniz!");
                                                    }
                                                    else
                                                    {
                                                        if (textBox7.Text == "")
                                                        {
                                                            MessageBox.Show("Lütfen Öğrencinin Numarasını Giriniz!");
                                                        }
                                                        else
                                                        {
                                                            baglanti.Open();
                                                            komut.Connection = baglanti;
                                                            komut.CommandText = "select * from kimlik_bilgileri where ogr_no =@nosu";
                                                            komut.Parameters.AddWithValue("@nosu", textBox7.Text);
                                                            int ss = Convert.ToInt32(komut.ExecuteScalar());
                                                            baglanti.Close();
                                                            if (ss != 0)
                                                            {
                                                                MessageBox.Show("Öğrenci Kayıtlıdır!");
                                                            }
                                                            else
                                                            {
                                                                if (textBox8.Text == "")
                                                                {
                                                                    MessageBox.Show("Lütfen Ders Adını Giriniz!");
                                                                }
                                                                else
                                                                {
                                                                    if (textBox9.Text == "")
                                                                    {
                                                                        MessageBox.Show("Lütfen Ders Adını Giriniz!");
                                                                    }
                                                                    else
                                                                    {
                                                                        if (textBox10.Text == "")
                                                                        {
                                                                            MessageBox.Show("Lütfen Ders Adını Giriniz!");
                                                                        }
                                                                        else
                                                                        {
                                                                            if (textBox11.Text == "")
                                                                            {
                                                                                MessageBox.Show("Lütfen Ders Adını Giriniz!");
                                                                            }
                                                                            else
                                                                            {
                                                                                if (textBox12.Text == "")
                                                                                {
                                                                                    MessageBox.Show("Lütfen Ders Adını Giriniz!");
                                                                                }
                                                                                else
                                                                                {
                                                                                    baglanti.Open();
                                                                                    komut4.Connection = baglanti;
                                                                                    komut2.Connection = baglanti;
                                                                                    komut3.Connection = baglanti;
                                                                                    komut4.CommandText = "insert into kimlik_bilgileri(ogr_no, ogr_adi, ogr_soyadi, ogr_yasi, ogr_cinsiyet, ogr_memleket) values(" + textBox7.Text + ",@ad, @sad, @yas, @cins, @mem)";
                                                                                    komut4.Parameters.AddWithValue("@ad", textBox1.Text);
                                                                                    komut4.Parameters.AddWithValue("@sad", textBox2.Text);
                                                                                    komut4.Parameters.AddWithValue("@yas", textBox3.Text);
                                                                                    komut4.Parameters.AddWithValue("@cins", comboBox1.SelectedItem.ToString());
                                                                                    komut4.Parameters.AddWithValue("@mem", textBox4.Text);
                                                                                    komut4.ExecuteNonQuery();
                                                                                    komut2.CommandText = "insert into veli_bilgileri(ogr_no, anne_adi, anne_tel, baba_adi, baba_tel, veli_kim) values(@no,@ane,@anet,@bab,@babt,@veli)";
                                                                                    komut2.Parameters.AddWithValue("@no", textBox7.Text);
                                                                                    komut2.Parameters.AddWithValue("@ane", textBox5.Text);
                                                                                    komut2.Parameters.AddWithValue("@anet", maskedTextBox1.Text);
                                                                                    komut2.Parameters.AddWithValue("@bab", textBox6.Text);
                                                                                    komut2.Parameters.AddWithValue("@babt", maskedTextBox2.Text);
                                                                                    komut2.Parameters.AddWithValue("@veli", comboBox2.SelectedItem.ToString());
                                                                                    komut2.ExecuteNonQuery();
                                                                                    komut3.CommandText = "insert into ders_bilgileri(ogr_no, ogr_sinif,ders1,ders2,ders3,ders4,ders5) values(@no,@sin,@ders1,@ders2,@ders3,@ders4,@ders5)";
                                                                                    komut3.Parameters.AddWithValue("@no", textBox7.Text);
                                                                                    komut3.Parameters.AddWithValue("@sin", comboBox3.SelectedItem.ToString());
                                                                                    komut3.Parameters.AddWithValue("@ders1", textBox8.Text);
                                                                                    komut3.Parameters.AddWithValue("@ders2", textBox9.Text);
                                                                                    komut3.Parameters.AddWithValue("@ders3", textBox10.Text);
                                                                                    komut3.Parameters.AddWithValue("@ders4", textBox11.Text);
                                                                                    komut3.Parameters.AddWithValue("@ders5", textBox12.Text);
                                                                                    komut3.ExecuteNonQuery();
                                                                                    temizle();
                                                                                    MessageBox.Show("Öğrenci Kayıt Edildi.");
                                                                                    baglanti.Close();
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        public void temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            maskedTextBox1.Clear();
            maskedTextBox2.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                MessageBox.Show("Lütfen Silinecek Öğrencinin Numarasını Giriniz!");
            }
            else
            {
                baglanti.Open();
                komut4.Connection = baglanti;
                komut2.Connection = baglanti;
                komut3.Connection = baglanti;
                komut4.CommandText = "delete from kimlik_bilgileri where ogr_no=@no";
                komut4.Parameters.AddWithValue("@no", textBox7.Text);
                komut4.ExecuteNonQuery();
                komut2.CommandText = "delete from veli_bilgileri where ogr_no=@no";
                komut2.Parameters.AddWithValue("@no", textBox7.Text);
                komut2.ExecuteNonQuery();
                komut3.CommandText = "delete from ders_bilgileri where ogr_no=@no";
                komut3.Parameters.AddWithValue("@no", textBox7.Text);
                komut3.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Öğrenci Silindi !");
                temizle();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand kontrol = new OleDbCommand("select * from kimlik_bilgileri where ogr_no ='" + textBox7.Text + "'",baglanti);
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

                OleDbCommand komut1 = new OleDbCommand();
                OleDbCommand komut2 = new OleDbCommand();
                OleDbCommand komut3 = new OleDbCommand();
                komut1.Connection = baglanti;
                komut2.Connection = baglanti;
                komut3.Connection = baglanti;
                //öğrenci bilgileri
                komut1.CommandText = "select * from kimlik_bilgileri where ogr_no=@no";
                komut1.Parameters.AddWithValue("@no", textBox7.Text);
                okuma = komut1.ExecuteReader();
                while (okuma.Read())
                {
                    textBox1.Text = okuma["ogr_adi"].ToString();
                    textBox2.Text = okuma["ogr_soyadi"].ToString();
                    textBox3.Text = okuma["ogr_yasi"].ToString();
                    comboBox1.Text = okuma["ogr_cinsiyet"].ToString();
                    textBox4.Text = okuma["ogr_memleket"].ToString();
                }
                okuma.Dispose();

                //veli bilgileri
                komut2.CommandText = "select * from veli_bilgileri where ogr_no = @no";
                komut2.Parameters.AddWithValue("@no", textBox7.Text);
                okuma = komut2.ExecuteReader();
                while (okuma.Read())
                {
                    textBox5.Text = okuma["anne_adi"].ToString();
                    maskedTextBox1.Text = okuma["anne_tel"].ToString();
                    textBox6.Text = okuma["baba_adi"].ToString();
                    maskedTextBox2.Text = okuma["baba_tel"].ToString();
                    comboBox2.Text = okuma["veli_kim"].ToString();
                }
                okuma.Dispose();

                //ders kısımları
                komut3.CommandText = "select * from ders_bilgileri where ogr_no= @no";
                komut3.Parameters.AddWithValue("@no", textBox7.Text);
                okuma = komut3.ExecuteReader();
                while (okuma.Read())
                {
                    comboBox3.Text = okuma["ogr_sinif"].ToString();
                    textBox8.Text = okuma["ders1"].ToString();
                    textBox9.Text = okuma["ders2"].ToString();
                    textBox10.Text = okuma["ders3"].ToString();
                    textBox11.Text = okuma["ders4"].ToString();
                    textBox12.Text = okuma["ders5"].ToString();
                }
                okuma.Dispose();
                baglanti.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                MessageBox.Show("Lütfn Öğrenci Numarasını Giriniz !");
            }
            else
            {
                OleDbConnection deneme = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0; Data Source = dunden hemen onceki gun.mdb");
                deneme.Open();
                OleDbCommand deneme2 = new OleDbCommand("update kimlik_bilgileri set ogr_adi =@ad, ogr_soyadi=@sad, ogr_yasi =@yas, ogr_cinsiyet=@cins, ogr_memleket=@mem where ogr_no ='" + textBox7.Text + "' ", deneme);

                deneme2.Parameters.AddWithValue("@ad", textBox1.Text);
                deneme2.Parameters.AddWithValue("@sad", textBox2.Text);
                deneme2.Parameters.AddWithValue("@yas", textBox3.Text);
                deneme2.Parameters.AddWithValue("@cins", comboBox1.SelectedItem.ToString());
                deneme2.Parameters.AddWithValue("@mem", textBox4.Text);
                deneme2.ExecuteNonQuery();

                OleDbCommand veliupd = new OleDbCommand("update veli_bilgileri set anne_adi=@anead, anne_tel=@anetel, baba_adi=@babad, baba_tel=@babtel, veli_kim=@vel where ogr_no = @no",deneme);
                veliupd.Parameters.AddWithValue("@anead", textBox5.Text);
                veliupd.Parameters.AddWithValue("@anetel", maskedTextBox1.Text);
                veliupd.Parameters.AddWithValue("@babad", textBox6.Text);
                veliupd.Parameters.AddWithValue("@babtel", maskedTextBox2.Text);
                veliupd.Parameters.AddWithValue("@vel", comboBox2.SelectedItem.ToString());
                veliupd.Parameters.AddWithValue("@no", textBox7.Text);
                veliupd.ExecuteNonQuery();

                OleDbCommand dersupd = new OleDbCommand("update ders_bilgileri set ogr_sinif=@sinif, ders1=@d1, ders2=@d2,ders3=@d3,ders4=@d4,ders5=@d5 where ogr_no = @no",deneme);
                dersupd.Parameters.AddWithValue("@sinif", comboBox3.SelectedItem.ToString());
                dersupd.Parameters.AddWithValue("@d1", textBox8.Text);
                dersupd.Parameters.AddWithValue("@d2", textBox9.Text);
                dersupd.Parameters.AddWithValue("@d3", textBox10.Text);
                dersupd.Parameters.AddWithValue("@d4", textBox11.Text);
                dersupd.Parameters.AddWithValue("@d5", textBox12.Text);
                dersupd.Parameters.AddWithValue("@no", textBox7.Text);
                dersupd.ExecuteNonQuery();
                MessageBox.Show("Öğrenci Güncellendi !");
                deneme.Close();
            }
        }

        private void öğrenciNotBilgisiniGirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OgrenciNotBilgileri ogrnot = new OgrenciNotBilgileri();
            ogrnot.Show();
            this.Hide();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void dERSDURUMUToolStripMenuItem_Click(object sender, EventArgs e)
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
