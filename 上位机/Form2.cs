using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mysqlx.Session;
using MySqlX.XDevAPI.Common;

namespace competition
{
    

    public partial class Form2 : Form
    {
        private Form1 parentForm;
        sql s = new sql();
        string[] abc;
        public Form2(Form1 form, string[] labels)
        {
            InitializeComponent();
            parentForm = form;
            abc = labels;  // 保存下来
            decimal total = s.CalculateTotalPrice(abc);
            string summary = s.GenerateSummaryText(abc);
            richTextBox1.AppendText(summary);

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            s.UpdateProductStock(abc, "减少");
            DialogResult result = MessageBox.Show("结账成功！感谢下次光临", "提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                parentForm.RefreshProductPanel();  // 通知 Form1 刷新商品库存
                this.Close();
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
