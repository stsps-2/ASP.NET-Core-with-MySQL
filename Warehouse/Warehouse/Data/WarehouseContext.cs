using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Warehouse.Data.Models;
using Warehouse.Helpers;

namespace Warehouse.Data
{
    public class WarehouseContext
    {
        public string ConnectionString { get; set; }

        public WarehouseContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public IEnumerable<Product> GetProducts()
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from product", conn);

                return GetData(cmd);
            }
        }

        public IEnumerable<Product> GetProductByName(string name)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from product where Name = ${ name } limit 1", conn);

                return GetData(cmd);
            }
        }

        public IEnumerable<Product> GetShoppingList()
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from product where Quantity = 0", conn);

                return GetData(cmd);
            }
        }

        public void DeleteProduct(int productId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"delete from product where ProductId = ${ productId }", conn);
            }
        }

        public void CreateProduct(Product product)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                    $"insert into product (Name, Quantity, DefaultValue) values(${ product.Name }, ${ product.Quantity }, ${ product.DefaultValue })", conn);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(int productId, int quantity)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"update product set Quantity = Quantity + ${ quantity }", conn);
                cmd.ExecuteNonQuery();
            }
        }

        #region Helpers
        private IEnumerable<Product> GetData(MySqlCommand cmd)
        {
            List<Product> list = new List<Product>();

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Product()
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        Name = reader["Name"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        DefaultValue = reader["DefaultValue"].ToString().ToNullable<int>()
                    });
                }
            }

            return list;
        }
        #endregion
    }
}
