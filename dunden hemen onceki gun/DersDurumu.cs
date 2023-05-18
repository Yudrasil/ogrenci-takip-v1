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
    public partial class DersDurumu : Form
    {
        public DersDurumu()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0; Data Source = dunden hemen onceki gun.mdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataAdapter adaptor;
        DataSet ds;
        DataTable dt = new DataTable();

        public void tablodoldur()
        {
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "select ogr_no, ogr_sinif, ders1,ders2,ders3,ders4,ders5 from ders_bilgileri ";
            komut.ExecuteNonQuery();
            adaptor = new OleDbDataAdapter(komut);
            adaptor.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add(dataGridView1.CurrentRow.Cells[2].Value.ToString());
            comboBox1.Items.Add(dataGridView1.CurrentRow.Cells[3].Value.ToString());
            comboBox1.Items.Add(dataGridView1.CurrentRow.Cells[4].Value.ToString());
            comboBox1.Items.Add(dataGridView1.CurrentRow.Cells[5].Value.ToString());
            comboBox1.Items.Add(dataGridView1.CurrentRow.Cells[6].Value.ToString());
        }

        private void öğrenciKayıtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }

        private void notGirişiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OgrenciNotBilgileri not = new OgrenciNotBilgileri();
            not.Show();
            this.Hide();
        }

        private void öğrenciGenelDurumuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenelDurum geneld = new GenelDurum();
            geneld.Show();
            this.Hide();
        }

        private void DersDurumu_Load(object sender, EventArgs e)
        {
            tablodoldur();
        }

        private void DersDurumu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ders ;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            OleDbDataReader okuma;
            OleDbCommand komut1 = new OleDbCommand();
            baglanti.Open();
            komut1.Connection = baglanti;
            komut1.CommandText = "select yazili1, yazili2,yazili3,sozlu1,sozlu2 from not_bilgileri where ders_adi='"+comboBox1.SelectedItem.ToString()+"'";
            okuma = komut1.ExecuteReader();
            while (okuma.Read())
            {
                textBox1.Text = okuma["yazili1"].ToString();
                textBox2.Text = okuma["yazili2"].ToString();
                textBox3.Text = okuma["yazili3"].ToString();
                textBox4.Text = okuma["sozlu1"].ToString();
                textBox5.Text = okuma["sozlu2"].ToString();
            }
            baglanti.Close();
            int not1, not2, not3, not4, not5, ortalama;
            not1 = Convert.ToInt32(textBox1.Text);
            not2 = Convert.ToInt32(textBox2.Text);
            not3 = Convert.ToInt32(textBox3.Text);
            not4 = Convert.ToInt32(textBox4.Text);
            not5 = Convert.ToInt32(textBox5.Text);
            ortalama = ((not1 + not2 + not3 + not4 + not5) / 5);
            label10.Text = ortalama.ToString();
            if (ortalama >= 50)
            {
                label11.ForeColor = Color.Green;
                label11.Text = "Geçti";
            }
            else
            {
                label11.ForeColor = Color.Red;
                label11.Text = "Kaldı";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand skontrol = new OleDbCommand("select * from ders_durum where ders_adi=@ders and ogr_no=@no ",baglanti);
            skontrol.Parameters.AddWithValue("@ders",comboBox1.SelectedItem.ToString());
            skontrol.Parameters.AddWithValue("@no",dataGridView1.CurrentRow.Cells[0].Value.ToString());
            int sa = Convert.ToInt32(skontrol.ExecuteScalar());
            if (sa == 0)
            {
                string no = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                komut.Connection = baglanti;
                komut.CommandText = "insert into ders_durum(ogr_no,ders_adi,ortalama,durum) values(@no,@dersa,@ort,@durum)";
                komut.Parameters.AddWithValue("@no", no);
                komut.Parameters.AddWithValue("@dersa", comboBox1.SelectedItem.ToString());
                komut.Parameters.AddWithValue("@ort", label10.Text);
                komut.Parameters.AddWithValue("@durum", label11.Text);
                komut.ExecuteNonQuery();
                komut.Dispose();
                MessageBox.Show("Kaydedildi!");
                baglanti.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
            else
            {

                MessageBox.Show("Bu Ders İçin Zaten Not Girilmiştir!");
                baglanti.Close();
            }
        }
    }
}