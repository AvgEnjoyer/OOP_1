using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_1
{
    
    public partial class Form1 : Form
    {
       private Grid GR = new Grid();
       private _26BasedSystem sys26 = new _26BasedSystem();
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public Form1()
        {
            InitializeComponent();
            InitTable(GR.RowCount, GR.ColCount);
        }
        private void InitTable(int row,int col)
        {
            dataGridView1.AllowUserToAddRows = false;
            for (int i = 0;i<col;i++)
            {
                string colname = sys26.To26Sys(i);
                dataGridView1.Columns.Add(colname, colname);
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridView1.RowCount = row;
            for (int i = 0; i < row; i++)
                dataGridView1.Rows[i].HeaderCell.Value = i.ToString();
            GR.SetGrid(row, col);

            dataGridView1.AllowUserToAddRows = false;
        }
        

      

        private void EnterButton_Click(object sender, EventArgs e)
        {
            int col = dataGridView1.SelectedCells[0].ColumnIndex;
            int row = dataGridView1.SelectedCells[0].RowIndex;
            string expr = ExprTextBox.Text;
            if (expr == "")
                return;
            GR.ChangeCellWithAllPointers(row, col, expr, dataGridView1);
            dataGridView1[col, row].Value = GR.grid[row][col].Value;

        }
        private void dataGridView1_Cell_Click(object sender,EventArgs e)
        {
            int col = dataGridView1.SelectedCells[0].ColumnIndex;
            int row = dataGridView1.SelectedCells[0].RowIndex;

            string expr = GR.grid[row][col].Expression;
            string value = GR.grid[row][col].Value;

            ExprTextBox.Text = expr;
            ExprTextBox.Focus();
        }

        private void AddRow_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            if(dataGridView1.Columns.Count==0)
            {
                MessageBox.Show("There no columns!");
                return;
            }
            dataGridView1.Rows.Add(row);
            RefreshRowNumbers();
            GR.AddRow(dataGridView1);
        }

        private void RefreshRowNumbers()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = i.ToString();
            }
        }

        private void DeleteRow_Click(object sender, EventArgs e)
        {
            int curRow = GR.RowCount - 1;
            if (!GR.DeleteRow(dataGridView1))
                return;

            dataGridView1.Rows.RemoveAt(curRow);
         //   RefreshRowNumbers();
        }

        private void AddColumn_Click(object sender, EventArgs e)
        {
            string colname = sys26.To26Sys(GR.ColCount);
            dataGridView1.Columns.Add(colname, colname);
            GR.AddCol(dataGridView1);
        }

        private void DeleteColumn_Click(object sender, EventArgs e)
        {
            int curCol = GR.ColCount - 1;
            if (!GR.DeleteCol(dataGridView1))
                return;
            dataGridView1.Columns.RemoveAt(curCol);
        }
        private void SaveFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "GridFile|*.grd";
            saveFileDialog.Title = "Save Grid File";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                FileStream fs = (FileStream)saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(fs);
                GR.Save(sw);
                sw.Close();
                fs.Close();
            }
        }
        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DGridFile|*.grd";
            openFileDialog.Title = "Select Grid File";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            StreamReader sr = new StreamReader(openFileDialog.FileName);
            GR.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            Int32.TryParse(sr.ReadLine(), out int row);
            Int32.TryParse(sr.ReadLine(), out int col);
            InitTable(row, col);
            GR.Open(row, col, sr, dataGridView1);
            sr.Close();

        }

       

        private void save_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void open_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
    }
}
