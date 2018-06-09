using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Models;

namespace DefenceOfTheAncientsRPG.Data
{
    public class ItemSQLContext : IItemContext
    {
        public Item CreateItemFromReader(SqlDataReader reader)
        {
            return new Item
            {
                Id = Convert.ToString(reader["Id"]),
                Name = Convert.ToString(reader["Name"]),
                Strength = Convert.ToInt32(reader["Strength"]),
                Agility = Convert.ToInt32(reader["Agility"]),
                Intelligence = Convert.ToInt32(reader["Intelligence"]),
                Damage = Convert.ToInt32(reader["Damage"]),
                Attackspeed = Convert.ToInt32(reader["Attackspeed"]),
                Armor = Convert.ToInt32(reader["Armor"]),
                Health = Convert.ToInt32(reader["Healthpoints"]),
                Mana = Convert.ToInt32(reader["Mana"]),
                Cost = Convert.ToInt32(reader["Cost"])
            };

        }

        public List<Item> GetAllItems()
        {
            List<Item> Items = new List<Item>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM Items";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Items.Add(CreateItemFromReader(reader));
                            }
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return Items;
        }

        public bool Insert(Item item)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = string.Format("INSERT INTO Items (Id, Name, Strength, Agility, Intelligence, Damage, Attackspeed, Armor, Healthpoints, Mana, Cost)" +
                    " VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}','{8}','{9}','{10}')",
                    item.Id, item.Name, item.Strength, item.Agility, item.Intelligence, item.Damage, item.Attackspeed, item.Armor, item.HealthRegen, item.Mana, item.Cost);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                        return true;

                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }


    }
}
