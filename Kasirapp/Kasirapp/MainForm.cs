/*
 * Created by SharpDevelop.
 * User: MSI Alpha
 * Date: 24/05/2021
 * Time: 0:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Kasirapp
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{

		SqlConnection co = new SqlConnection("Server = MSI; Database = Dbkasir; integrated security = true");
		SqlCommand mycommand = new SqlCommand();
		SqlDataAdapter myadapter = new SqlDataAdapter();
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			Bersihkan();
			readdata();
			MunculSatuan();
			
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void Bersihkan() {
			textBox1.Text="";
			textBox2.Text="";
			textBox3.Text="0";
			textBox4.Text="0";
			textBox5.Text="0";
			comboBox1.Text="";
			textBox7.Text="";
			readdata();
		}
		
		
		void MunculSatuan() {
			comboBox1.Items.Add("Unit");
			comboBox1.Items.Add("Pcs");
			comboBox1.Items.Add("Slot");

		}
	
		void Nootomatis() {
			long hitung;
			string urutan;
			SqlDataReader rd;
			mycommand.Connection = co;
                myadapter.SelectCommand = mycommand;
                mycommand.CommandText = "select KodeBarang from Table_Barang where KodeBarang in(select max(kodeBarang) from Table_Barang) order by desc"; 
                rd = mycommand.ExecuteReader();
                rd.Read();
                if (rd.HasRows) {
                	hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["KodeBarang"].ToString().Length - 3,3)) +1;
                	string kodeurutan = "000" +hitung;
                	urutan = "BRG" +kodeurutan.Substring(kodeurutan.Length - 3, 3);
                }
                else {
                	urutan = "BRG001";
                }
                rd.Close();
                textBox1.Text = urutan;
                co.Close();
		}
		
		void readdata() {
			
			try{
				 mycommand.Connection = co;
                myadapter.SelectCommand = mycommand;
                mycommand.CommandText = "select * from Table_Barang";
                DataSet ds= new DataSet();
                if (myadapter.Fill(ds,"Table_Barang")>0){
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = "Table_Barang";
                }
                co.Close();
            }
            catch (Exception ex){
                MessageBox.Show(ex.ToString());
			}	
		}
		
		void caribarang() {
			
			try{
				 mycommand.Connection = co;
                myadapter.SelectCommand = mycommand;
                mycommand.CommandText = "select * from Table_Barang where kodebarang like '%"+textBox7.Text+"%' or namabarang like '%"+textBox7.Text+"%'";
                DataSet ds= new DataSet();
                if (myadapter.Fill(ds,"Table_Barang")>0){
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = "Table_Barang";
                }
                co.Close();
            }
            catch (Exception ex){
                MessageBox.Show(ex.ToString());
			}	
		}
		
		void Button1Click(object sender, EventArgs e)
		{ 
			if(textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" ||textBox4.Text.Trim() == "" ||textBox5.Text.Trim() == "" ||comboBox1.Text.Trim() == "")
			{
				MessageBox.Show("Isi setiap kolom yang tersedia terlebih dahulu");
			}
			else
			{
				try{
					mycommand.Connection = co;
                	myadapter.SelectCommand = mycommand;
                	mycommand.CommandText = "INSERT INTO TABLE_BARANG VALUES ('"+textBox1.Text+"', '"+textBox2.Text+"','"+textBox3.Text+"','"+textBox4.Text+"','"+textBox5.Text+"','"+comboBox1.Text+"')";
                	DataSet ds= new DataSet();
                	if (myadapter.Fill(ds,"Table_Barang")>0){
                    	dataGridView1.DataSource = ds;
                    	dataGridView1.DataMember = "Table_Barang";
                }
                MessageBox.Show("Data berhasil di input");
                    	readdata();
                    	Bersihkan();
                co.Close();
            }
            catch (Exception ex){
                MessageBox.Show("Data Gagal di input");
			}
				
			}
		}
		
		
		
		void DataGridView1CellClick(object sender, DataGridViewCellEventArgs e)
		{
		try 
			{
				DataGridViewRow row = this.dataGridView1.Rows [e.RowIndex];
				textBox1.Text = row.Cells["kodeBarang"].Value.ToString();
				textBox2.Text = row.Cells["NamaBarang"].Value.ToString();
				textBox3.Text = row.Cells["HargaJual"].Value.ToString();
				textBox4.Text = row.Cells["HargaBeli"].Value.ToString();
				textBox5.Text = row.Cells["JumlahBarang"].Value.ToString();
				comboBox1.Text = row.Cells["SatuanBarang"].Value.ToString();
			}
			catch (Exception ex) {
				MessageBox.Show(ex.ToString());
			}	
		}
		
		
		
		
		
		void Button2Click(object sender, EventArgs e)
		{
			if(textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" ||textBox4.Text.Trim() == "" ||textBox5.Text.Trim() == "" ||comboBox1.Text.Trim() == "")
			{
				MessageBox.Show("Isi setiap kolom yang tersedia terlebih dahulu");
			}
			else
			{
				try{
					mycommand.Connection = co;
                	myadapter.SelectCommand = mycommand;
                	mycommand.CommandText = "UPDATE Table_Barang SET KodeBarang='"+textBox1.Text+"',NamaBarang='"+textBox2.Text+"',HargaJual='"+textBox3.Text+"',HargaBeli='"+textBox4.Text+"',JumlahBarang='"+textBox5.Text+"',SatuanBarang='"+comboBox1.Text+"'where KodeBarang='"+textBox1.Text+"'";
                	DataSet ds= new DataSet();
                	if (myadapter.Fill(ds,"Table_Barang")>0){
                    	dataGridView1.DataSource = ds;
                    	dataGridView1.DataMember = "Table_Barang";
                }
                MessageBox.Show("Update Data berhasil!");
                    	readdata();
                    	Bersihkan();
                co.Close();
            }
            catch (Exception ex){
					MessageBox.Show(ex.ToString());
			}
				
			}
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Yakin ingin menghapus "+textBox2.Text+"?", "Hapus Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				try{
					mycommand.Connection = co;
                	myadapter.SelectCommand = mycommand;
                	mycommand.CommandText = "DELETE FROM Table_Barang where KodeBarang='"+textBox1.Text+"'";
                	DataSet ds= new DataSet();
                	if (myadapter.Fill(ds,"Table_Barang")>0){
                    	dataGridView1.DataSource = ds;
                    	dataGridView1.DataMember = "Table_Barang";
                }
                MessageBox.Show("Hapus Data berhasil!");
                    	readdata();
                    	Bersihkan();
                co.Close();
            }
				catch (Exception ex){
					MessageBox.Show(ex.ToString());
			}
			}
		}
		
		void TextBox7TextChanged(object sender, EventArgs e)
		{
			caribarang();
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			Bersihkan();
		}
	}
}
