using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Models;

namespace DefenceOfTheAncientsRPG.Data
{
    public class HeroSQLContext : IHeroContext
    {
        private Hero CreateHeroFromReader(SqlDataReader reader)
        {
            char MainAttribute = Convert.ToChar(reader["MainAttribute"]);
            switch (MainAttribute)
            {
                case 'S':
                    return new StrengthHero
                 (
                 Convert.ToString(reader["Id"]),
                 Convert.ToString(reader["Name"]),
                 Convert.ToInt32(reader["Expierence"]),
                 (float)reader["StrengthGain"],
                 (float)reader["AgilityGain"],
                 (float)reader["IntelligenceGain"]
                 );
                case 'A':
                    return new AgilityHero
                 (
                 Convert.ToString(reader["Id"]),
                 Convert.ToString(reader["Name"]),
                 Convert.ToInt32(reader["Expierence"]),
                 Convert.ToSingle(reader["IntelligenceGain"]),
                 (float)reader["AgilityGain"],
                 (float)reader["IntelligenceGain"]
                 );
                case 'I':
                    return new IntelligenceHero
                 (
                 Convert.ToString(reader["Id"]),
                 Convert.ToString(reader["Name"]),
                 Convert.ToInt32(reader["Expierence"]),
                 Convert.ToSingle(reader["StrengthGain"]),
                 (float)reader["AgilityGain"],
                 (float)reader["IntelligenceGain"]
                 );
                default:
                    return null;
            }
        }

        public Hero GetHeroById(string id)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = string.Format("SELECT * FROM Heroes WHERE Id = '{0}'", id);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return CreateHeroFromReader(reader);
                        }
                    }
                }
            }
            return null;
        }


        public List<Item> GetInventoryByHeroId(string id)
        {
            List<Item> items = new List<Item>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = string.Format("SELECT * FROM Items AS item INNER JOIN LT_Inventory AS inventory" +
                    " ON item.Id = inventory.ItemId WHERE inventory.HeroId = '{0}'",
                    id);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new ItemSQLContext().CreateItemFromReader(reader));
                        }
                    }
                }
            }
            return items;
        }

        public bool Insert(Hero hero)
        {
            char mainAttribute;
            if (hero is StrengthHero) mainAttribute = 'S';
            else if (hero is AgilityHero) mainAttribute = 'A';
            else mainAttribute = 'I';

            using (SqlConnection connection = Database.Connection)
            {
                string query = string.Format("INSERT INTO Heroes (Id, Name, Expierence, StrengthGain, AgilityGain, IntelligenceGain, MainAttribute)" +
                    " VALUES ('{0}', '{1}', {2}, {3}, {4}, {5}, '{6}')",
                    hero.Id, hero.Name, hero.Expierence, hero.StrengthGain, hero.AgilityGain, hero.IntelligenceGain, mainAttribute);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }

        public bool InsertLink(Hero hero, ApplicationUser user)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = string.Format("INSERT INTO LT_UserHero (HeroId, UserId)" +
                    " VALUES ('{0}', '{1}')",
                    hero.Id, user.Id);
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

        public bool UpdateExpierence(Hero hero, int exp)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = string.Format("UPDATE Heroes SET Expierence = {0} WHERE Id = {1}",
                   exp, hero.Id);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }

        public List<Hero> GetAllHeroes()
        {
            List<Hero> heroes = new List<Hero>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM Heroes";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                heroes.Add(CreateHeroFromReader(reader));
                            }
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return heroes;
        }

        public List<Hero> GetHeroesByUserId(string id)
        {
            List<Hero> heroes = new List<Hero>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = string.Format("SELECT * FROM Heroes AS hero INNER JOIN LT_UserHero AS lt" +
                    " ON hero.Id = lt.HeroId WHERE lt.UserId = '{0}'",   
                    id);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                heroes.Add(CreateHeroFromReader(reader));
                            }
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return heroes;
        }
    }
}
