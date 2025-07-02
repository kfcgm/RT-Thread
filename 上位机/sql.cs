using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using MongoDB.Driver.Core.Configuration;
using Mysqlx.Crud;
using System.Transactions;


namespace competition
{
    internal class sql
    {
        private SqlConnection _connection; // 修正：SqlConnection 首字母大写

        string connectionString = @"Server=.;Database=Goods;User Id=sa;Password=123456;";


        //打开连接
        public void OpenConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(connectionString); // 修正变量名
            }

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        //关闭连接
        public void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        //执行查询
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                // 可以记录日志或抛出异常
                throw new Exception("执行查询失败: " + ex.Message);
            }
        }

        //执行非查询操作
        public int ExecuteNonQuery(string commandText, SqlParameter[] parameters = null)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand(commandText, _connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("执行非查询操作失败: " + ex.Message);
            }
        }

        public DataTable GetProductPrices(string[] productNames)
        {
            if (productNames == null || productNames.Length == 0)
            {
                return new DataTable();
            }

            string[] uniqueNames = productNames.Distinct().ToArray();

            List<SqlParameter> parameters = new List<SqlParameter>();
            List<string> paramNames = new List<string>();

            for (int i = 0; i < uniqueNames.Length; i++)
            {
                string paramName = $"@name{i}";
                parameters.Add(new SqlParameter(paramName, uniqueNames[i]));
                paramNames.Add(paramName);
            }

            string query = $@"
            SELECT name, price 
            FROM Fruit 
            WHERE name IN ({string.Join(",", paramNames)})";

            return ExecuteQuery(query, parameters.ToArray());
        }
        public decimal CalculateTotalPrice(string[] items)
        {
            // 清理输入数据：去空格、转小写
            string[] cleanedItems = items
                .Select(item => item.Trim().ToLower())
                .ToArray();

            // 统计商品数量
            var itemQuantities = cleanedItems
                .GroupBy(item => item)
                .ToDictionary(g => g.Key, g => g.Count());

            // 查询商品单价
            DataTable priceTable = GetProductPrices(cleanedItems.Distinct().ToArray());

            // 转换为单价字典（统一小写）
            var priceDict = new Dictionary<string, decimal>();
            foreach (DataRow row in priceTable.Rows)
            {
                string dbName = row["Name"].ToString().Trim().ToLower();
                if (decimal.TryParse(row["Price"].ToString(), out decimal price))
                {
                    priceDict[dbName] = price;
                }
                else
                {
                    Console.WriteLine($"警告: 商品 {dbName} 价格无效");
                }
            }

            // 计算总价
            decimal totalPrice = 0;
            foreach (var item in itemQuantities)
            {
                string name = item.Key;
                int quantity = item.Value;

                if (priceDict.TryGetValue(name, out decimal price))
                {
                    totalPrice += price * quantity;
                }
                else
                {
                    Console.WriteLine($"警告: 商品 {name} 不存在于数据库");
                }
            }

            return totalPrice;
        }

        public string GenerateSummaryText(string[] inputItems)
        {
            // 1. 统计商品数量
            var itemQuantities = inputItems
                .GroupBy(item => item)
                .ToDictionary(g => g.Key, g => g.Count());

            // 2. 查询商品单价（复用之前的 GetProductPrices 方法）
            DataTable priceTable = GetProductPrices(itemQuantities.Keys.ToArray());

            // 3. 转换为单价字典
            var priceDict = priceTable.Rows
                .Cast<DataRow>()
                .ToDictionary(
                    row => row["Name"].ToString().Trim(),
                    row => Convert.ToDecimal(row["Price"])
                );

            // 4. 构建格式化字符串
            StringBuilder sb = new StringBuilder();

            // 标题行
            sb.AppendLine("商品名称    数量    单价    总价");
            sb.AppendLine("--------------------------------");

            // 遍历商品
            decimal total = 0;
            foreach (var item in itemQuantities)
            {
                string name = item.Key;
                int quantity = item.Value;

                if (priceDict.TryGetValue(name, out decimal price))
                {
                    decimal itemTotal = quantity * price;
                    total += itemTotal;

                    // 对齐格式（使用固定宽度）
                    sb.AppendLine($"{name.PadRight(8)}    {quantity.ToString().PadRight(4)}    ¥{price:F2}    ¥{itemTotal:F2}");
                }
                else
                {
                    sb.AppendLine($"[警告] 商品 {name} 不存在");
                }
            }

            // 合计行
            sb.AppendLine("--------------------------------");
            sb.AppendLine($"合计                ¥{total:F2}");

            return sb.ToString();
        }

        public void UpdateProductStock(string[] productNames, string action)
        {
            if (productNames == null || productNames.Length == 0)
            {
                Console.WriteLine("商品列表为空，无法更新库存。");
                return;
            }

            if (action != "增加" && action != "减少")
            {
                Console.WriteLine("无效的操作类型，必须是 \"增加\" 或 \"减少\"。");
                return;
            }

            // 标准化输入（去空格、统一小写）
            string[] cleanedItems = productNames
                .Select(item => item.Trim().ToLower())
                .ToArray();

            // 统计数量
            var itemQuantities = cleanedItems
                .GroupBy(name => name)
                .ToDictionary(g => g.Key, g => g.Count());

            // 获取商品当前数据
            string[] uniqueNames = itemQuantities.Keys.ToArray();
            List<SqlParameter> parameters = new List<SqlParameter>();
            List<string> paramNames = new List<string>();

            for (int i = 0; i < uniqueNames.Length; i++)
            {
                string paramName = $"@name{i}";
                parameters.Add(new SqlParameter(paramName, uniqueNames[i]));
                paramNames.Add(paramName);
            }

            string query = $@"
        SELECT id, name, number 
        FROM Fruit 
        WHERE LOWER(name) IN ({string.Join(",", paramNames)})";

            DataTable productTable = ExecuteQuery(query, parameters.ToArray());

            foreach (DataRow row in productTable.Rows)
            {
                int id = Convert.ToInt32(row["id"]);
                string name = row["name"].ToString().Trim().ToLower();
                int currentStock = Convert.ToInt32(row["number"]);

                if (itemQuantities.TryGetValue(name, out int quantity))
                {
                    int newStock = action == "增加" ? currentStock + quantity : currentStock - quantity;
                    if (newStock < 0) newStock = 0; // 防止负库存

                    string updateCommand = "UPDATE Fruit SET number = @number WHERE id = @id";
                    SqlParameter[] updateParams = new SqlParameter[]
                    {
                new SqlParameter("@number", newStock),
                new SqlParameter("@id", id)
                    };

                    ExecuteNonQuery(updateCommand, updateParams);
                }
            }
        }




    }
}
