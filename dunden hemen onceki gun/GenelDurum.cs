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
    public partial class GenelDurum : Form
    {
        public GenelDurum()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0; Data Source = dunden hemen onceki gun.mdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataReader okuma;
        OleDbDataReader okuma2;
        OleDbCommand komut2 = new OleDbCommand();
        public void temizle()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
        
        }
        public void baglan() 
        {
            if (baglanti.State==ConnectionState.Closed)
            {
                baglanti.Open();
            }
            else
            {
                baglanti.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen Öğrenci Numarasını Giriniz!");
                baglanti.Close();
            }
            else
            {
                int toplam = 0, ortalama = 0, dersadeti = 0;
                string belge;
                baglanti.Open();
                OleDbCommand skontrol = new OleDbCommand("select ogr_no from kimlik_bilgileri where ogr_no=@no",baglanti);
                skontrol.Parameters.AddWithValue("@no",textBox1.Text);
                int sonuc = Convert.ToInt32(skontrol.ExecuteScalar());
                if (sonuc==0)
                {
                    MessageBox.Show("Öğrenci Kayıtlı Değildir!");
                    baglanti.Close();
                }
                else
                {
                    temizle();
                    if (baglanti.State==ConnectionState.Open)
                    {
                        baglanti.Close();
                    }
                    baglanti.Open();
                    komut.Connection = baglanti;
                    komut.CommandText = "select ogr_adi, ogr_soyadi from kimlik_bilgileri where ogr_no = '" + textBox1.Text + "'";
                    okuma = komut.ExecuteReader();
                    while (okuma.Read())
                    {
                        textBox2.Text = okuma["ogr_adi"].ToString();
                        textBox3.Text = okuma["ogr_soyadi"].ToString();

                        OleDbCommand velisorgu = new OleDbCommand("select * from veli_bilgileri where ogr_no='"+textBox1.Text+"'",baglanti);
                        OleDbDataReader veliokuma;
                        veliokuma = velisorgu.ExecuteReader();
                        if (veliokuma.Read())
                        {
                            textBox5.Text = veliokuma["anne_adi"].ToString();
                            textBox6.Text = veliokuma["anne_tel"].ToString();
                            textBox7.Text = veliokuma["baba_adi"].ToString();
                            textBox8.Text = veliokuma["baba_tel"].ToString();
                            veliokuma.Close();
                            OleDbCommand sinifbul = new OleDbCommand("select ogr_sinif from ders_bilgileri where ogr_no='"+textBox1.Text+"'",baglanti);
                            OleDbDataReader sinifoku;
                            sinifoku = sinifbul.ExecuteReader();
                            if (sinifoku.Read())
                            {
                                textBox4.Text = sinifoku["ogr_sinif"].ToString();
                            }
                        }
                    }

                    OleDbCommand durum_komut = new OleDbCommand("select ders_adi,ortalama,durum from ders_durum where ogr_no = '"+textBox1.Text+"'",baglanti);
                    OleDbDataReader durumokuma;
                    durumokuma = durum_komut.ExecuteReader();
                    while (durumokuma.Read())
                    {
                        listBox1.Items.Add(durumokuma["ders_adi"].ToString());
                        listBox2.Items.Add(durumokuma["ortalama"].ToString());
                        listBox3.Items.Add(durumokuma["durum"].ToString());
                        toplam = toplam + Convert.ToInt32(durumokuma["ortalama"]);
                        dersadeti++;
                    }
                    if (dersadeti>0)
                    {
                        ortalama = toplam / dersadeti;
                        label14.Text = ortalama.ToString();
                        if (ortalama < 70)
                            belge = "YOK";
                        else if (ortalama < 86)
                            belge = "Teşekkür Belgesi";
                        else belge = "Tadir Belgesi";
                        label15.Text = belge;
                    }
                    baglanti.Close();
                    }
                }
            }
                private void GenelDurum_Load(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
        }

        private void GenelDurum_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void öğrenciKayıtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void notGirşiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OgrenciNotBilgileri not = new OgrenciNotBilgileri();
            not.Show();
            this.Hide();
        }

        private void öğrenciDersDurumuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DersDurumu ders = new DersDurumu();
            ders.Show();
            this.Hide();
        }
    }
}
