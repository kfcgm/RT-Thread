using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Amazon.Runtime.Internal.Util;
using MongoDB.Driver.Core.Servers;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using STTech.BytesIO.Core;
using STTech.BytesIO.Tcp;
using ZstdSharp.Unsafe;
using static Mysqlx.Datatypes.Scalar.Types;


namespace competition
{
  
    public partial class Form1 : Form
    {
        
     

        private Panel containerPanel; // 专门用于商品展示的容器
        private TcpServer server;
        sql s =new sql();
        private List<(string path, string name, int price, int number)> data = new List<(string, string, int, int)>();
        string[] abc;
        // 统计每个商品的出现次数
        DataTable result;
        string[] c;
        string[] labels;
        int i = 0;
        public Form1()
        {
            InitializeComponent();
           
            CheckForIllegalCrossThreadCalls = false;
            server = new TcpServer();
            server.Port = 8234;
            server.Started += Server_Started;
            server.Closed += Server_Closed;
            server.ClientConnected += Server_ClientConnected;
            server.ClientDisconnected += Server_ClientDisconnected;
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
            //数据库连接
            try
            {
                s.OpenConnection();
                //MessageBox.Show("连接成功");

            }
            catch (Exception error)
            { MessageBox.Show(error.ToString()); }
            //查询数据库的数据
            string query = "SELECT * FROM Fruit";
            try
            {

                result = s.ExecuteQuery(query);
                foreach (DataRow row in result.Rows) {
                    string path = row["image"].ToString();
                    //string fullPath = Path.Combine(basePath, path);
                    string name = row["name"].ToString();
                    int price = Convert.ToInt32(row["price"]);
                    int number = Convert.ToInt32(row["number"]);
                    data.Add((path, name, price, number));
                }
            }
            catch (Exception ex) { }
          
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void Server_ClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            Print($"客户端[{e.Client.Host};{e.Client.Port}]断开连接");
            
        }

        private void Client_OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            TcpClient tcpClient = (TcpClient)sender;
            Print($"来自客户端[{tcpClient.RemoteEndPoint}]的消息;{e.Data.EncodeToString("GBK")}");

            string rawData = e.Data.EncodeToString("GBK");

            // 使用 ";result=" 分割图像与结果
            int splitIndex = rawData.IndexOf(";result=");
            if (splitIndex == -1)
            {
                Print("接收数据格式错误：未找到 ;result= 分隔符！");
                return;
            }

            string hexImage = rawData.Substring(0, splitIndex);
            string jsonPart = rawData.Substring(splitIndex + 8).Trim();

            // 图像处理
            try
            {
                byte[] imageBytes = Enumerable.Range(0, hexImage.Length / 2)
                    .Select(i => Convert.ToByte(hexImage.Substring(i * 2, 2), 16))
                    .ToArray();
                using (var ms = new MemoryStream(imageBytes))
                {
                    Image img = Image.FromStream(ms);
                    JpgImage.Image = img;
                }
            }
            catch (Exception ex)
            {
                Print("图像处理异常：" + ex.Message);
            }

            // JSON解析
            try
            {
                List<FruitResult> results = JsonConvert.DeserializeObject<List<FruitResult>>(jsonPart);
                labels = results.Select(r => r.label).ToArray();
                string resultText = string.Join(", ", results.Select(r => r.label));
                textBox2.Text = resultText;
                Print($"识别结果：{resultText}");
              
                    for (; i < labels.Length; i++)
                    {
                    if(i!= labels.Length)
                        textBox2.Text = textBox2.Text + labels[i];

                    }
            }
            catch (Exception ex)
            {
                Print("JSON解析错误：" + ex.Message);
                textBox2.Text = "解析错误：" + ex.Message;
            }
        }
        public class FruitResult
        {
            public string label { get; set; }
            public float confidence { get; set; }
        }

        private void Server_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            Print($"客户端[{e.Client.Host};{e.Client.Port}]连接成功");
          
            e.Client.OnDataReceived += Client_OnDataReceived;
        }

        private void Server_Closed(object sender, EventArgs e)
        {
          
        }

        private void Server_Started(object sender, EventArgs e)
        {
           
        }

        private void StartWork_Click(object sender, EventArgs e)
        {
            server.StartAsync();
            LineType.Text = "连接成功";
            button2.Enabled = true;
            button4.Enabled = true;
         
        }

        private void Print(string msg)
        {
            
            tbLog.AppendText($"[{DateTime.Now}] {msg}\r\n");
        }

        private void CloseWork_Click(object sender, EventArgs e)
        {
            server.CloseAsync();
            LineType.Text = "断开连接";
            button2.Enabled = false;
            button4.Enabled = false;
        }

        private string[] Color(string jsonData) {
            List<string> colors = JsonConvert.DeserializeObject<List<string>>(jsonData);
            return colors?.ToArray() ?? new string[0];

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }




       

        private void Form1_Load(object sender, EventArgs e)
        {
            // 创建一个普通容器 Panel 来手动控制商品面板的位置
             containerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };
            this.Controls.Add(containerPanel);

            // 每个商品模块的宽度和间隔
            int itemWidth = 180;  // 每个商品模块的宽度
            int spacing = 10;  // 每个商品之间的间隔

            // 第一个商品面板的位置 (x: 655, y: 50)
            int x = 655;
            int y = 50;

            // 动态生成每个商品模块
            foreach (var item in data)
            {
                try
                {
                    // 创建商品面板并应用自定义设置
                    Panel panel = CreateImageWithDescriptionPanel(item.path, item.name,item.price ,item.number);
                    panel.Width = itemWidth;  // 设置面板的宽度
                    panel.Margin = new Padding(spacing);  // 设置面板之间的间隔

                    // 第一个商品面板放置在指定的位置
                    if (containerPanel.Controls.Count == 0)
                    {
                        panel.Location = new Point(x, y);
                    }
                    else
                    {
                        // 如果已经有商品面板，更新位置，按顺序排列
                        x += itemWidth + spacing;  // 更新 x 坐标
                        if (x + itemWidth > containerPanel.Width)  // 如果当前行满了，换行
                        {
                            x = 655;  // 重置 x 坐标
                            y += panel.Height + spacing;  // 增加 y 坐标
                        }
                        panel.Location = new Point(x, y);
                    }

                    // 将商品面板添加到容器中
                    containerPanel.Controls.Add(panel);
                }
                catch (Exception ex)
                {
                    // 记录错误日志或提示用户
                    MessageBox.Show($"加载商品 [{item.name}] 失败: {ex.Message}");
                }
            }
        }



        private Panel CreateImageWithDescriptionPanel(string imagePath, string title, int price,int count)
        {
           

            Panel panel = new Panel
            {
                Size = new Size(150, 180),
                BorderStyle = BorderStyle.FixedSingle
            };

            PictureBox pic = new PictureBox
            {
                Size = new Size(120, 120),
                Location = new Point(15, 10),
                SizeMode = PictureBoxSizeMode.Zoom
            };

            Label lblTitle = new Label
            {
                Text = title,
                Location = new Point(10, 135),
                Font = new Font("微软雅黑", 10, FontStyle.Bold),
                AutoSize = true
            };

            Label lblCount = new Label
            {
                Text = $"价格{price},数量x{count}",
                Location = new Point(10, 155),
                //ForeColor = Color.Gray,
                AutoSize = true
            };

            // -------------------------------
            // 关键异常处理：图片加载逻辑
            // -------------------------------
            try
            {
                // 检查路径是否为空或不存在
                if (string.IsNullOrEmpty(imagePath))
                    throw new ArgumentException("图片路径不能为空");

                if (!File.Exists(imagePath))
                    throw new FileNotFoundException("图片文件不存在", imagePath);

                // 加载图片
                pic.Image = Image.FromFile(imagePath);
            }
            catch (Exception ex)
            {
               
                throw new InvalidOperationException(ex.ToString());
            }

            // 添加控件到 Panel
            panel.Controls.Add(pic);
            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblCount);

            return panel;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "售卖状态";
            button1.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "入库状态";
            button1.Enabled = false;
            MessageBox.Show("入库成功！", "提示", MessageBoxButtons.OKCancel);
            s.UpdateProductStock(labels , "增加");
            RefreshProductPanel();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (labels == null || labels.Length == 0)
            {
                MessageBox.Show("识别结果为空，无法结算！");
                return;
            }

            Form2 form2 = new Form2(this, labels);
            form2.ShowDialog();

        }

        public void RefreshProductPanel()
        {
            // 清空旧数据
            data.Clear();
            result = null;

            // 从数据库重新查询
            string query = "SELECT * FROM Fruit";
            try
            {
                result = s.ExecuteQuery(query);
                foreach (DataRow row in result.Rows)
                {
                    string path = row["image"].ToString();
                    string name = row["name"].ToString();
                    int price = Convert.ToInt32(row["price"]);
                    int number = Convert.ToInt32(row["number"]);
                    data.Add((path, name, price, number));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("刷新失败：" + ex.Message);
                return;
            }

            // 找到原来的容器 Panel（假设你在 Form1_Load 中添加到了 Controls）
            var containerPanel = this.Controls
                .OfType<Panel>()
                .FirstOrDefault(p => p.AutoScroll && p.Dock == DockStyle.Fill);

            if (containerPanel == null)
            {
                MessageBox.Show("找不到商品容器控件！");
                return;
            }

            // 清空原有控件
            containerPanel.Controls.Clear();

            // 重新生成商品面板
            int itemWidth = 180;
            int spacing = 10;
            int x = 655;
            int y = 50;

            foreach (var item in data)
            {
                try
                {
                    Panel panel = CreateImageWithDescriptionPanel(item.path, item.name, item.price, item.number);
                    panel.Width = itemWidth;
                    panel.Margin = new Padding(spacing);

                    if (containerPanel.Controls.Count == 0)
                    {
                        panel.Location = new Point(x, y);
                    }
                    else
                    {
                        x += itemWidth + spacing;
                        if (x + itemWidth > containerPanel.Width)
                        {
                            x = 655;
                            y += panel.Height + spacing;
                        }
                        panel.Location = new Point(x, y);
                    }

                    containerPanel.Controls.Add(panel);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"加载商品 [{item.name}] 失败: {ex.Message}");
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void JpgImage_Click(object sender, EventArgs e)
        {

        }

        private void tbLog_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
