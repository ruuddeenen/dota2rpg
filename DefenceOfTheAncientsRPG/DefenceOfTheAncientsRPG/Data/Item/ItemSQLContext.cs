using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Models;

namespace DefenceOfTheAncientsRPG.Data
{
    public class ItemSQLContext
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
    }
}
