using System.Data;
using System.Data.SqlClient;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Project_UDW.Models;
using static System.Net.Mime.MediaTypeNames;


namespace Project_UDW.Models
{
    public class DAL
    {
        public Response GetUserItemDetail(SqlConnection conn, string version_update)
        {
            Response response = new Response();
            List<Item> itemList = new List<Item>();

            try
            {
                string query = @"   SELECT * 
                                    FROM item
                                    WHERE nameitem IN (	SELECT nameitem
                                    					FROM update_item_version
                                    					WHERE update_item_version.version_update IN (	SELECT version_update
                                    																	FROM update_head
                                    																	WHERE update_head.version_update = @version_update))";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@version_update", version_update);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Item item = new Item
                    {
                        nameitem = reader["nameitem"].ToString(),
                        picitem = reader["picitem"].ToString(),
                        health = reader["health"].ToString(),
                        atkdame = reader["atkdame"].ToString(),
                        apdame = reader["apdame"].ToString(),
                        atkspeed = reader["atkspeed"].ToString(),
                        crit = reader["crit"].ToString(),
                        armor = reader["armor"].ToString(),
                        magicresis = reader["magicresis"].ToString(),
                        ahaste = reader["ahaste"].ToString(),
                        movespeed = reader["movespeed"].ToString(),
                        noitaiitem = reader["noitaiitem"].ToString(),
                        cost = reader["cost"].ToString(),
                    };
                    itemList.Add(item);
                }

                if (itemList.Count > 0)
                {
                    response.StatusCode = 200;
                    response.Data = itemList;
                }
                else
                {
                    response.StatusCode = 404;
                    response.StatusMessage = "No detail item found for the given region";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return response;
        }
        public Response GetUserDetailChampDetail(SqlConnection conn, string version_update)
        {
            Response response = new Response();
            List<DetailsChampUpdateVersion> detailchampList = new List<DetailsChampUpdateVersion>();

            try
            {
                string query = @"   SELECT detail_update_champ.ChampName, ImageAVA, dame_nt, dame_q, dame_w, dame_e, dame_r, detail_NN
                                    FROM detail_update_champ
                                    INNER JOIN champions ON detail_update_champ.ChampName = champions.ChampName
                                    WHERE detail_update_champ.ChampName IN (SELECT ChampName
                                    					                    FROM update_champ_version
                                    					                    WHERE update_champ_version.version_update IN (	SELECT version_update
                                    																	                    FROM update_head
                                    																	                    WHERE update_head.version_update = @version_update))";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@version_update", version_update);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DetailsChampUpdateVersion detailsChamp = new DetailsChampUpdateVersion
                    {
                        ChampName = reader["detail_update_champ.ChampName"].ToString(),
                        ImageAVA = reader["ImageAVA"].ToString(),
                        dame_nt = reader["dame_nt"].ToString(),
                        dame_q = reader["dame_q"].ToString(),
                        dame_w = reader["dame_w"].ToString(),
                        dame_e = reader["dame_e"].ToString(),
                        dame_r = reader["dame_r"].ToString(),
                        detail_NN = reader["detail_NN"].ToString()
                    };
                    detailchampList.Add(detailsChamp);
                }

                if (detailchampList.Count > 0)
                {
                    response.StatusCode = 200;
                    response.Data = detailchampList;
                }
                else
                {
                    response.StatusCode = 404;
                    response.StatusMessage = "No detail champ found for the given region";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return response;
        }
        public Response GetUserSkillDetail(SqlConnection conn, string version_update)
        {
            Response response = new Response();
            List<Skills> skillList = new List<Skills>();

            try
            {
                string query = @"   SELECT ChampName, Skillnt, Skillq, Skillw, Skille, Skillr, AVAnt, AVAq, AVAw, AVAe, AVAr
                                    FROM skills
                                    WHERE ChampName IN (SELECT ChampName
                                    					FROM update_champ_version
                                    					WHERE update_champ_version.version_update IN (	SELECT version_update
                                    																	FROM update_head
                                    																	WHERE update_head.version_update = @version_update))";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@version_update", version_update);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Skills skill = new Skills
                    {
                        ChampName = reader["ChampName"].ToString(),
                        NoiTai = reader["Skillnt"].ToString(),
                        SkillQ = reader["Skillq"].ToString(),
                        SkillW = reader["Skillw"].ToString(),
                        SkillE = reader["Skille"].ToString(),
                        SkillR = reader["Skillr"].ToString(),
                        AVA_NT = reader["AVAnt"].ToString(),
                        AVA_Q = reader["AVAq"].ToString(),
                        AVA_W = reader["AVAw"].ToString(),
                        AVA_E = reader["AVAe"].ToString(),
                        AVA_R = reader["AVAr"].ToString()
                    };
                    skillList.Add(skill);
                }

                if (skillList.Count > 0)
                {
                    response.StatusCode = 200;
                    response.Data = skillList;
                }
                else
                {
                    response.StatusCode = 404;
                    response.StatusMessage = "No skill found for the given region";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return response;
        }
        public Response AddTeam(Team team, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                string imageteam = Path.GetFileName(team.avateam);
                SqlCommand cmd = new SqlCommand("sp_addteam", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mateam", team.mateam);
                cmd.Parameters.AddWithValue("@tenteam", team.tenteam);
                cmd.Parameters.AddWithValue("@makhuvuc", team.makhuvuc);
                cmd.Parameters.AddWithValue("@avateam", imageteam);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response UpdateTeam(Team team, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                string imageteam = Path.GetFileName(team.avateam);
                SqlCommand cmd = new SqlCommand("sp_updateteam", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(team.mateam))
                {
                    cmd.Parameters.AddWithValue("@mateam", team.mateam);
                }
                if (!string.IsNullOrEmpty(team.tenteam))
                {
                    cmd.Parameters.AddWithValue("@tenteam", team.tenteam);
                }
                if (!string.IsNullOrEmpty(team.makhuvuc))
                {
                    cmd.Parameters.AddWithValue("@makhuvuc", team.makhuvuc);
                }
                if (!string.IsNullOrEmpty(imageteam))
                {
                    cmd.Parameters.AddWithValue("@avateam", imageteam);
                }
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response DeleteTeam(Team_Delete delteam, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_deleteteam", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mateam", delteam.mateam);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response GetTeam(SqlConnection conn)
        {
            Response response = new Response();
            List<Team> teamList = new List<Team>();

            try
            {
                string query = "SELECT * FROM team";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Team team = new Team
                            {
                                mateam = reader["mateam"].ToString(),
                                tenteam = reader["tenteam"].ToString(),
                                makhuvuc = reader["makhuvuc"].ToString(),
                                avateam = reader["avateam"].ToString()
                            };
                            teamList.Add(team);
                        }
                    }
                }
                response.StatusCode = 200;
                response.Data = teamList;
                response.StatusMessage = "Team retrieved successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return response;
        }
        public Response GetUserTeamDetail(SqlConnection conn, string makhuvuc)
        {
            Response response = new Response();
            List<Team> teamList = new List<Team>();

            try
            {
                string query = @"SELECT * 
                         FROM team
                         WHERE team.makhuvuc = @makhuvuc";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@makhuvuc", makhuvuc);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Team team = new Team
                    {
                        mateam = reader["mateam"].ToString(),
                        tenteam = reader["tenteam"].ToString(),
                        makhuvuc = reader["makhuvuc"].ToString(),
                        avateam = reader["avateam"].ToString()
                    };
                    teamList.Add(team);
                }

                if (teamList.Count > 0)
                {
                    response.StatusCode = 200;
                    response.Data = teamList;
                }
                else
                {
                    response.StatusCode = 404;
                    response.StatusMessage = "No teams found for the given region";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return response;
        }
        public Response GetChampionDetail(SqlConnection conn, string champName)
        {
            Response response = new Response();
            try
            {
                conn.Open();
                string query = "SELECT ChampName, NickName, Describle, Role, Level, ImageDD, ImageAVA FROM champions WHERE ChampName = @ChampName";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ChampName", champName);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Champions champion = new Champions
                    {
                        ChampName = reader["ChampName"].ToString(),
                        NickName = reader["NickName"].ToString(),
                        Describle = reader["Describle"].ToString(),
                        Role = reader["Role"].ToString(),
                        Level = reader["Level"].ToString(),
                        ImageDD = reader["ImageDD"].ToString(),
                        ImageAVA = reader["ImageAVA"].ToString()
                    };
                    response.StatusCode = 200;
                    response.Data = champion;
                }
                else
                {
                    response.StatusCode = 404;
                    response.StatusMessage = "Champion not found";
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = ex.Message;
            }

            return response;
        }
        public Response AddKhuVuc(KhuVuc khuvuc, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                string imagekhuvuc = Path.GetFileName(khuvuc.avakhuvuc);
                SqlCommand cmd = new SqlCommand("sp_addkhuvuc", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@makhuvuc", khuvuc.makhuvuc);
                cmd.Parameters.AddWithValue("@tenkhuvuc", khuvuc.tenkhuvuc);
                cmd.Parameters.AddWithValue("@avakhuvuc", imagekhuvuc);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response UpdateKhuVuc(KhuVuc khuvuc, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                string imagekhuvuc = Path.GetFileName(khuvuc.avakhuvuc);
                SqlCommand cmd = new SqlCommand("sp_updatekhuvuc", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(khuvuc.makhuvuc))
                {
                    cmd.Parameters.AddWithValue("@makhuvuc", khuvuc.makhuvuc);
                }
                if (!string.IsNullOrEmpty(khuvuc.tenkhuvuc))
                {
                    cmd.Parameters.AddWithValue("@tenkhuvuc", khuvuc.tenkhuvuc);
                }
                if (!string.IsNullOrEmpty(imagekhuvuc))
                {
                    cmd.Parameters.AddWithValue("@avakhuvuc", imagekhuvuc);
                }
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response DeleteKhuVuc(KhuVuc_Delete delkhuvuc, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_deletekhuvuc", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@makhuvuc", delkhuvuc.makhuvuc);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response GetKhuVuc(SqlConnection conn)
        {
            Response response = new Response();
            List<KhuVuc> khuvucList = new List<KhuVuc>();

            try
            {
                string query = "SELECT * FROM khuvuc";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            KhuVuc khuvuc = new KhuVuc
                            {
                                makhuvuc = reader["makhuvuc"].ToString(),
                                tenkhuvuc = reader["tenkhuvuc"].ToString(),
                                avakhuvuc = reader["avakhuvuc"].ToString()
                            };
                            khuvucList.Add(khuvuc);
                        }
                    }
                }
                response.StatusCode = 200;
                response.Data = khuvucList;
                response.StatusMessage = "Khu vuc retrieved successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return response;
        }
        public Response Registration(Users users, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_register", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", users.Username);
                cmd.Parameters.AddWithValue("@Email", users.Email);
                cmd.Parameters.AddWithValue("@Password", users.Password);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();

                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response Login(Users users, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("sp_login", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
                da.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "User is valid";
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "User is invalid";
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response GetProfileUser(SqlConnection conn)
        {
            Response response = new Response();
            List<Users> profileuserlist = new List<Users>();
            try
            {
                string query = "SELECT Username, Email, Password FROM users";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Users user = new Users
                            {
                                Username = reader["Username"].ToString(),
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString(),
                            };
                            profileuserlist.Add(user);
                        }
                    }
                }
                response.StatusCode = 200;
                response.Data = profileuserlist;
                response.StatusMessage = "Get profile successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return response;
        }
        public Response AddChampions(Champions champions, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                string imageDDFileName = Path.GetFileName(champions.ImageDD);
                string imageAVAFileName = Path.GetFileName(champions.ImageAVA);
                SqlCommand cmd = new SqlCommand("sp_addchamp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@champname", champions.ChampName);
                cmd.Parameters.AddWithValue("@nickname", champions.NickName);
                cmd.Parameters.AddWithValue("@describle", champions.Describle);
                cmd.Parameters.AddWithValue("@role", champions.Role);
                cmd.Parameters.AddWithValue("@level", champions.Level);
                cmd.Parameters.AddWithValue("@imagedd", imageDDFileName);
                cmd.Parameters.AddWithValue("@imageava", imageAVAFileName);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response AddNews(News news, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                string imagepicnewmain = Path.GetFileName(news.picnewmain);
                string imagepicnewblur = Path.GetFileName(news.picnewblur);
                SqlCommand cmd = new SqlCommand("sp_addnews", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@namenews", news.namenews);
                cmd.Parameters.AddWithValue("@picnewmain", imagepicnewmain);
                cmd.Parameters.AddWithValue("@picnewblur", imagepicnewblur);
                cmd.Parameters.AddWithValue("@maintitle", news.maintitle);
                cmd.Parameters.AddWithValue("@destitle", news.destitle);
                cmd.Parameters.AddWithValue("@mota", news.mota);
                cmd.Parameters.AddWithValue("@title1", news.title1);
                cmd.Parameters.AddWithValue("@title1_con1", news.title1_con1);
                cmd.Parameters.AddWithValue("@title1_con2", news.title1_con2);
                cmd.Parameters.AddWithValue("@title2", news.title2);
                cmd.Parameters.AddWithValue("@title2_con1", news.title2_con1);
                cmd.Parameters.AddWithValue("@title2_con2", news.title2_con2);
                cmd.Parameters.AddWithValue("@title3", news.title3);
                cmd.Parameters.AddWithValue("@title3_con1", news.title3_con1);
                cmd.Parameters.AddWithValue("@title3_con2", news.title3_con2);
                cmd.Parameters.AddWithValue("@title4", news.title4);
                cmd.Parameters.AddWithValue("@title4_con1", news.title4_con1);
                cmd.Parameters.AddWithValue("@title4_con2", news.title4_con2);
                cmd.Parameters.AddWithValue("@title5", news.title5);
                cmd.Parameters.AddWithValue("@title5_con1", news.title5_con1);
                cmd.Parameters.AddWithValue("@title5_con2", news.title5_con2);
                cmd.Parameters.AddWithValue("@tomtat", news.tomtat);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response UpdateNews(News updatenews, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                string imagepicnewmain = Path.GetFileName(updatenews.picnewmain);
                string imagepicnewblur = Path.GetFileName(updatenews.picnewblur);
                SqlCommand cmd = new SqlCommand("sp_updatenews", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(updatenews.namenews))
                {
                    cmd.Parameters.AddWithValue("@namenews", updatenews.namenews);
                }
                if (!string.IsNullOrEmpty(imagepicnewmain))
                {
                    cmd.Parameters.AddWithValue("@picnewmain", imagepicnewmain);
                }
                if (!string.IsNullOrEmpty(imagepicnewblur))
                {
                    cmd.Parameters.AddWithValue("@picnewblur", imagepicnewblur);
                }
                if (!string.IsNullOrEmpty(updatenews.maintitle))
                {
                    cmd.Parameters.AddWithValue("@maintitle", updatenews.maintitle);
                }
                if (!string.IsNullOrEmpty(updatenews.destitle))
                {
                    cmd.Parameters.AddWithValue("@destitle", updatenews.destitle);
                }
                if (!string.IsNullOrEmpty(updatenews.mota))
                {
                    cmd.Parameters.AddWithValue("@mota", updatenews.mota);
                }
                if (!string.IsNullOrEmpty(updatenews.title1))
                {
                    cmd.Parameters.AddWithValue("@title1", updatenews.title1);
                }
                if (!string.IsNullOrEmpty(updatenews.title1_con1))
                {
                    cmd.Parameters.AddWithValue("@title1_con1", updatenews.title1_con1);
                }
                if (!string.IsNullOrEmpty(updatenews.title1_con2))
                {
                    cmd.Parameters.AddWithValue("@title1_con2", updatenews.title1_con2);
                }
                if (!string.IsNullOrEmpty(updatenews.title2))
                {
                    cmd.Parameters.AddWithValue("@title2", updatenews.title2);
                }
                if (!string.IsNullOrEmpty(updatenews.title2_con1))
                {
                    cmd.Parameters.AddWithValue("@title2_con1", updatenews.title2_con1);
                }
                if (!string.IsNullOrEmpty(updatenews.title2_con2))
                {
                    cmd.Parameters.AddWithValue("@title2_con2", updatenews.title2_con2);
                }
                if (!string.IsNullOrEmpty(updatenews.title3))
                {
                    cmd.Parameters.AddWithValue("@title3", updatenews.title3);
                }
                if (!string.IsNullOrEmpty(updatenews.title3_con1))
                {
                    cmd.Parameters.AddWithValue("@title3_con1", updatenews.title3_con1);
                }
                if (!string.IsNullOrEmpty(updatenews.title3_con2))
                {
                    cmd.Parameters.AddWithValue("@title3_con2", updatenews.title3_con2);
                }
                if (!string.IsNullOrEmpty(updatenews.title4))
                {
                    cmd.Parameters.AddWithValue("@title4", updatenews.title4);
                }
                if (!string.IsNullOrEmpty(updatenews.title4_con1))
                {
                    cmd.Parameters.AddWithValue("@title4_con1", updatenews.title4_con1);
                }
                if (!string.IsNullOrEmpty(updatenews.title4_con2))
                {
                    cmd.Parameters.AddWithValue("@title4_con2", updatenews.title4_con2);
                }
                if (!string.IsNullOrEmpty(updatenews.title5))
                {
                    cmd.Parameters.AddWithValue("@title5", updatenews.title5);
                }
                if (!string.IsNullOrEmpty(updatenews.title5_con1))
                {
                    cmd.Parameters.AddWithValue("@title5_con1", updatenews.title5_con1);
                }
                if (!string.IsNullOrEmpty(updatenews.title5_con2))
                {
                    cmd.Parameters.AddWithValue("@title5_con2", updatenews.title5_con2);
                }
                if (!string.IsNullOrEmpty(updatenews.tomtat))
                {
                    cmd.Parameters.AddWithValue("@tomtat", updatenews.tomtat);
                }
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response DeleteNews(News_Delete delnews, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_deletenews", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@namenews", delnews.namenews);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response GetNews(SqlConnection conn)
        {
            Response response = new Response();
            List<News> newsList = new List<News>();

            try
            {
                string query = "SELECT * FROM news";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            News news = new News
                            {
                                namenews = reader["namenews"].ToString(),
                                picnewmain = reader["picnewmain"].ToString(),
                                picnewblur = reader["picnewblur"].ToString(),
                                maintitle = reader["maintitle"].ToString(),
                                destitle = reader["destitle"].ToString(),
                                mota = reader["mota"].ToString(),
                                title1 = reader["title1"].ToString(),
                                title1_con1 = reader["title1_con1"].ToString(),
                                title1_con2 = reader["title1_con2"].ToString(),
                                title2 = reader["title2"].ToString(),
                                title2_con1 = reader["title2_con1"].ToString(),
                                title2_con2 = reader["title2_con2"].ToString(),
                                title3 = reader["title3"].ToString(),
                                title3_con1 = reader["title3_con1"].ToString(),
                                title3_con2 = reader["title3_con2"].ToString(),
                                title4 = reader["title4"].ToString(),
                                title4_con1 = reader["title4_con1"].ToString(),
                                title4_con2 = reader["title4_con2"].ToString(),
                                title5 = reader["title5"].ToString(),
                                title5_con1 = reader["title5_con1"].ToString(),
                                title5_con2 = reader["title5_con2"].ToString(),
                                tomtat = reader["tomtat"].ToString()
                            };
                            newsList.Add(news);
                        }
                    }
                }
                response.StatusCode = 200;
                response.Data = newsList;
                response.StatusMessage = "News retrieved successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return response;
        }
        public Response UpdateChamp(Champions updatechamp, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                string imageDDFileName = Path.GetFileName(updatechamp.ImageDD);
                string imageAVAFileName = Path.GetFileName(updatechamp.ImageAVA);
                SqlCommand cmd = new SqlCommand("sp_updatechamp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(updatechamp.ChampName)) 
                {
                    cmd.Parameters.AddWithValue("@champname", updatechamp.ChampName);
                }
                if (!string.IsNullOrEmpty(updatechamp.NickName)) 
                {
                    cmd.Parameters.AddWithValue("@nickname", updatechamp.NickName);
                }
                if (!string.IsNullOrEmpty(updatechamp.Describle))
                {
                    cmd.Parameters.AddWithValue("@describle", updatechamp.Describle);
                }
                if (!string.IsNullOrEmpty(updatechamp.Role))
                {
                    cmd.Parameters.AddWithValue("@role", updatechamp.Role);
                }
                if (!string.IsNullOrEmpty(updatechamp.Level))
                {
                    cmd.Parameters.AddWithValue("@level", updatechamp.Level);
                }
                if (!string.IsNullOrEmpty(imageDDFileName))
                {
                    cmd.Parameters.AddWithValue("@imagedd", imageDDFileName);
                }
                if (!string.IsNullOrEmpty(imageAVAFileName))
                {
                    cmd.Parameters.AddWithValue("@imageava", imageAVAFileName);
                }
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response UpdateSkill(Skills updateskill, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                string imageNT = Path.GetFileName(updateskill.AVA_NT);
                string imageQ = Path.GetFileName(updateskill.AVA_Q);
                string imageW = Path.GetFileName(updateskill.AVA_W);
                string imageE = Path.GetFileName(updateskill.AVA_E);
                string imageR = Path.GetFileName(updateskill.AVA_R);
                string vdnt = Path.GetFileName(updateskill.Vdnt);
                string vdq = Path.GetFileName(updateskill.Vdq);
                string vdw = Path.GetFileName(updateskill.Vdw);
                string vde = Path.GetFileName(updateskill.Vde);
                string vdr = Path.GetFileName(updateskill.Vdr);
                SqlCommand cmd = new SqlCommand("sp_updateskill", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(updateskill.ChampName)) 
                {
                    cmd.Parameters.AddWithValue("@champname", updateskill.ChampName);
                }
                if (!string.IsNullOrEmpty(updateskill.NoiTai)) 
                {
                    cmd.Parameters.AddWithValue("@skillnt", updateskill.NoiTai);
                }
                if (!string.IsNullOrEmpty(updateskill.SkillQ)) 
                {
                    cmd.Parameters.AddWithValue("@skillq", updateskill.SkillQ);
                }
                if (!string.IsNullOrEmpty(updateskill.SkillW)) 
                {
                    cmd.Parameters.AddWithValue("@skillw", updateskill.SkillW);
                }
                if (!string.IsNullOrEmpty(updateskill.SkillE)) 
                {
                    cmd.Parameters.AddWithValue("@skille", updateskill.SkillE);
                }
                if (!string.IsNullOrEmpty(updateskill.SkillR)) 
                {
                    cmd.Parameters.AddWithValue("@skillr", updateskill.SkillR);
                }
                if (!string.IsNullOrEmpty(imageNT)) 
                {
                    cmd.Parameters.AddWithValue("@avant", imageNT);
                }
                if (!string.IsNullOrEmpty(imageQ)) 
                {
                    cmd.Parameters.AddWithValue("@avaq", imageQ);
                }
                if (!string.IsNullOrEmpty(imageW)) 
                {
                    cmd.Parameters.AddWithValue("@avaw", imageW);
                }
                if (!string.IsNullOrEmpty(imageE)) 
                {
                    cmd.Parameters.AddWithValue("@avae", imageE);
                }
                if (!string.IsNullOrEmpty(imageR)) 
                {
                    cmd.Parameters.AddWithValue("@avar", imageR);
                }
                if (!string.IsNullOrEmpty(vdnt))
                {
                    cmd.Parameters.AddWithValue("@vdnt", vdnt);
                }
                if (!string.IsNullOrEmpty(vdq))
                {
                    cmd.Parameters.AddWithValue("@vdq", vdq);
                }
                if (!string.IsNullOrEmpty(vdw))
                {
                    cmd.Parameters.AddWithValue("@vdw", vdw);
                }
                if (!string.IsNullOrEmpty(vde))
                {
                    cmd.Parameters.AddWithValue("@vde", vde);
                }
                if (!string.IsNullOrEmpty(vdr))
                {
                    cmd.Parameters.AddWithValue("@vdr", vdr);
                }
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response UpdateSkin(Skin updateskin, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                string skin1 = Path.GetFileName(updateskin.Skin1);
                string skin2 = Path.GetFileName(updateskin.Skin2);
                string skin3 = Path.GetFileName(updateskin.Skin3);
                string skin4 = Path.GetFileName(updateskin.Skin4);
                string skin5 = Path.GetFileName(updateskin.Skin5);
                string skin6 = Path.GetFileName(updateskin.Skin6);
                string skin7 = Path.GetFileName(updateskin.Skin7);
                string skin8 = Path.GetFileName(updateskin.Skin8);
                string skin9 = Path.GetFileName(updateskin.Skin9);
                string skin10 = Path.GetFileName(updateskin.Skin10);
                string skin11 = Path.GetFileName(updateskin.Skin11);
                string skin12 = Path.GetFileName(updateskin.Skin12);
                string skin13 = Path.GetFileName(updateskin.Skin13);
                string skin14 = Path.GetFileName(updateskin.Skin14);
                string skin15 = Path.GetFileName(updateskin.Skin15);
                string skin16 = Path.GetFileName(updateskin.Skin16);
                string skin17 = Path.GetFileName(updateskin.Skin17);
                string skin18 = Path.GetFileName(updateskin.Skin18);
                SqlCommand cmd = new SqlCommand("sp_updateskin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(updateskin.ChampName)) 
                {
                    cmd.Parameters.AddWithValue("@champname", updateskin.ChampName);
                }
                if (!string.IsNullOrEmpty(skin1))
                {
                    cmd.Parameters.AddWithValue("@skin1", skin1);
                } 
                if (!string.IsNullOrEmpty(skin2))
                {
                    cmd.Parameters.AddWithValue("@skin2", skin2);
                } 
                if (!string.IsNullOrEmpty(skin3))
                {
                    cmd.Parameters.AddWithValue("@skin3", skin3);
                } 
                if (!string.IsNullOrEmpty(skin4))
                {
                    cmd.Parameters.AddWithValue("@skin4", skin4);
                } 
                if (!string.IsNullOrEmpty(skin5))
                {
                    cmd.Parameters.AddWithValue("@skin5", skin5);
                } 
                if (!string.IsNullOrEmpty(skin6))
                {
                    cmd.Parameters.AddWithValue("@skin6", skin6);
                } 
                if (!string.IsNullOrEmpty(skin7))
                {
                    cmd.Parameters.AddWithValue("@skin7", skin7);
                } 
                if (!string.IsNullOrEmpty(skin8))
                {
                    cmd.Parameters.AddWithValue("@skin8", skin8);
                } 
                if (!string.IsNullOrEmpty(skin9))
                {
                    cmd.Parameters.AddWithValue("@skin9", skin9);
                } 
                if (!string.IsNullOrEmpty(skin10))
                {
                    cmd.Parameters.AddWithValue("@skin10", skin10);
                } 
                if (!string.IsNullOrEmpty(skin11))
                {
                    cmd.Parameters.AddWithValue("@skin11", skin11);
                } 
                if (!string.IsNullOrEmpty(skin12))
                {
                    cmd.Parameters.AddWithValue("@skin12", skin12);
                } 
                if (!string.IsNullOrEmpty(skin13))
                {
                    cmd.Parameters.AddWithValue("@skin13", skin13);
                } 
                if (!string.IsNullOrEmpty(skin14))
                {
                    cmd.Parameters.AddWithValue("@skin14", skin14);
                } 
                if (!string.IsNullOrEmpty(skin15))
                {
                    cmd.Parameters.AddWithValue("@skin15", skin15);
                } 
                if (!string.IsNullOrEmpty(skin16))
                {
                    cmd.Parameters.AddWithValue("@skin16", skin16);
                } 
                if (!string.IsNullOrEmpty(skin17))
                {
                    cmd.Parameters.AddWithValue("@skin17", skin17);
                } 
                if (!string.IsNullOrEmpty(skin18))
                {
                    cmd.Parameters.AddWithValue("@skin18", skin18);
                } 
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response AddUpChampVer(UpChampVer upchampver, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_addupchampver", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@champname", upchampver.ChampName);
                cmd.Parameters.AddWithValue("@version_update", upchampver.version_update);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response DeleteUpChampVer(UpChampVer_Delete delupchampver, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_deleteupchampver", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@version_update", delupchampver.version_update);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response UpdateUpChampVer(UpChampVer upupchampver, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_updateupchampver", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(upupchampver.ChampName))
                {
                    cmd.Parameters.AddWithValue("@champname", upupchampver.ChampName);
                }
                if (!string.IsNullOrEmpty(upupchampver.version_update))
                {
                    cmd.Parameters.AddWithValue("@version_update", upupchampver.version_update);
                }
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response GetUpChampVer(SqlConnection conn)
        {
            Response response = new Response();
            List<UpChampVer> upchampverList = new List<UpChampVer>();

            try
            {
                string query = "SELECT ChampName, version_update FROM update_champ_version";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UpChampVer upchampver = new UpChampVer
                            {
                                ChampName = reader["ChampName"].ToString(),
                                version_update = reader["version_update"].ToString()
                            };
                            upchampverList.Add(upchampver);
                        }
                    }
                }
                response.StatusCode = 200;
                response.Data = upchampverList;
                response.StatusMessage = "Update champion version retrieved successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return response;
        }
        public Response AddUpItemVer(UpItemVer upitemver, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_addupitemver", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nameitem", upitemver.nameitem);
                cmd.Parameters.AddWithValue("@version_update", upitemver.version_update);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response DeleteUpItemVer(UpItemVer_Delete delupitemver, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_deleteupitemver", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@version_update", delupitemver.version_update);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response UpdateUpItemVer(UpItemVer upupitemver, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_updateupitemver", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(upupitemver.nameitem))
                {
                    cmd.Parameters.AddWithValue("@nameitem", upupitemver.nameitem);
                }
                if (!string.IsNullOrEmpty(upupitemver.version_update))
                {
                    cmd.Parameters.AddWithValue("@version_update", upupitemver.version_update);
                }
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response GetUpItemVer(SqlConnection conn)
        {
            Response response = new Response();
            List<UpItemVer> upitemverList = new List<UpItemVer>();

            try
            {
                string query = "SELECT nameitem, version_update FROM update_item_version";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UpItemVer upitemver = new UpItemVer
                            {
                                nameitem = reader["nameitem"].ToString(),
                                version_update = reader["version_update"].ToString()
                            };
                            upitemverList.Add(upitemver);
                        }
                    }
                }
                response.StatusCode = 200;
                response.Data = upitemverList;
                response.StatusMessage = "Update item version retrieved successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return response;
        }
        public Response GetNewsDetail(SqlConnection conn, string namenews)
        {
            Response response = new Response();
            try
            {
                conn.Open();
                string query = @"SELECT * FROM news WHERE namenews = @namenews";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@namenews", namenews);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    News news = new News
                    {
                        namenews = reader["namenews"].ToString(),
                        picnewmain = reader["picnewmain"].ToString(),
                        picnewblur = reader["picnewblur"].ToString(),
                        maintitle = reader["maintitle"].ToString(),
                        destitle = reader["destitle"].ToString(),
                        mota = reader["mota"].ToString(),
                        title1 = reader["title1"].ToString(),
                        title1_con1 = reader["title1_con1"].ToString(),
                        title1_con2 = reader["title1_con2"].ToString(),
                        title2 = reader["title2"].ToString(),
                        title2_con1 = reader["title2_con1"].ToString(),
                        title2_con2 = reader["title2_con2"].ToString(),
                        title3 = reader["title3"].ToString(),
                        title3_con1 = reader["title3_con1"].ToString(),
                        title3_con2 = reader["title3_con2"].ToString(),
                        title4 = reader["title4"].ToString(),
                        title4_con1 = reader["title4_con1"].ToString(),
                        title4_con2 = reader["title4_con2"].ToString(),
                        title5 = reader["title5"].ToString(),
                        title5_con1 = reader["title5_con1"].ToString(),
                        title5_con2 = reader["title5_con2"].ToString(),
                        tomtat = reader["tomtat"].ToString()
                    };
                    response.StatusCode = 200;
                    response.Data = news;
                }
                else
                {
                    response.StatusCode = 404;
                    response.StatusMessage = "News name not found";
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = ex.Message;
            }

            return response;
        }
        public Response GetUpdateDetail(SqlConnection conn, string version_update)
        {
            Response response = new Response();
            try
            {
                conn.Open();
                string query = @"
        SELECT 
            version_update, picheadblur, picheadmain, maintitle, maindestitle, picupdate, 
            upversiontitle1, upversiontitle_con1, upversiontitle2, upversiontitle_con2, 
            upversiontitle3, upversiontitle_con3, upversiontitle4, upversiontitle_con4, 
            upversiontitle5, upversiontitle_con5, nangcaptrainghiem, sualoi
        FROM 
            update_head 
        WHERE 
            version_update = @version_update";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@version_update", version_update);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Update_Head update = new Update_Head
                    {
                        version_update = reader["version_update"].ToString(),
                        picheadblur = reader["picheadblur"].ToString(),
                        picheadmain = reader["picheadmain"].ToString(),
                        maintitle = reader["maintitle"].ToString(),
                        maindestitle = reader["maindestitle"].ToString(),
                        picupdate = reader["picupdate"].ToString(),
                        upversiontitle1 = reader["upversiontitle1"].ToString(),
                        upversiontitle_con1 = reader["upversiontitle_con1"].ToString(),
                        upversiontitle2 = reader["upversiontitle2"].ToString(),
                        upversiontitle_con2 = reader["upversiontitle_con2"].ToString(),
                        upversiontitle3 = reader["upversiontitle3"].ToString(),
                        upversiontitle_con3 = reader["upversiontitle_con3"].ToString(),
                        upversiontitle4 = reader["upversiontitle4"].ToString(),
                        upversiontitle_con4 = reader["upversiontitle_con4"].ToString(),
                        upversiontitle5 = reader["upversiontitle5"].ToString(),
                        upversiontitle_con5 = reader["upversiontitle_con5"].ToString(),
                        nangcaptrainghiem = reader["nangcaptrainghiem"].ToString(),
                        sualoi = reader["sualoi"].ToString(),
                    };
                    response.StatusCode = 200;
                    response.Data = update;
                }
                else
                {
                    response.StatusCode = 404;
                    response.StatusMessage = "Version not found";
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = ex.Message;
            }

            return response;
        }
        public Response GetSkillDetail(SqlConnection conn, string champName)
        {
            Response response = new Response();
            try
            {
                conn.Open();
                string query = "SELECT * FROM skills WHERE ChampName = @ChampName";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ChampName", champName);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Skills skill = new Skills
                    {
                        ChampName = reader["ChampName"].ToString(),
                        NoiTai = reader["Skillnt"].ToString(),
                        SkillQ = reader["Skillq"].ToString(),
                        SkillW = reader["Skillw"].ToString(),
                        SkillE = reader["Skille"].ToString(),
                        SkillR = reader["Skillr"].ToString(),
                        AVA_NT = reader["AVAnt"].ToString(),
                        AVA_Q = reader["AVAq"].ToString(),
                        AVA_W = reader["AVAw"].ToString(),
                        AVA_E = reader["AVAe"].ToString(),
                        AVA_R = reader["AVAr"].ToString(),
                        Vdnt = reader["Vdnt"].ToString(),
                        Vdq = reader["Vdq"].ToString(),
                        Vdw = reader["Vdw"].ToString(),
                        Vde = reader["Vde"].ToString(),
                        Vdr = reader["Vdr"].ToString()
                    };
                    response.StatusCode = 200;
                    response.Data = skill;
                }
                else
                {
                    response.StatusCode = 404;
                    response.StatusMessage = "Skill not found";
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = ex.Message;
            }

            return response;
        }
        public Response GetSkinDetail(SqlConnection conn, string champName)
        {
            Response response = new Response();
            try
            {
                conn.Open();
                string query = "SELECT * FROM skins WHERE ChampName = @ChampName";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ChampName", champName);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Skin skin = new Skin();
                    skin.ChampName = reader["ChampName"].ToString();

                    for (int i = 1; i <= 18; i++)
                    {
                        string propertyName = "Skin" + i;
                        typeof(Skin).GetProperty(propertyName).SetValue(skin, reader[propertyName].ToString());
                    }

                    response.StatusCode = 200;
                    response.Data = skin;
                }
                else
                {
                    response.StatusCode = 404;
                    response.StatusMessage = "Skin not found";
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = ex.Message;
            }

            return response;
        }
        public Response GetChampions(SqlConnection conn)
        {
            Response response = new Response();
            List<Champions> championsList = new List<Champions>();

            try
            {
                string query = "SELECT ChampName, NickName, Describle, Role, Level, ImageDD, ImageAVA FROM champions";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Champions champion = new Champions
                            {
                                ChampName = reader["ChampName"].ToString(),
                                NickName = reader["NickName"].ToString(),
                                Describle = reader["Describle"].ToString(), 
                                Role = reader["Role"].ToString(),
                                Level = reader["Level"].ToString(),
                                ImageDD = reader["ImageDD"].ToString(),
                                ImageAVA = reader["ImageAVA"].ToString()
                            };
                            championsList.Add(champion);
                        }
                    }
                }
                response.StatusCode = 200;
                response.Data = championsList;
                response.StatusMessage = "Champions retrieved successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return response;
        }
        public Response AddSkills(Skills skills, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                string imageAVA_NT = Path.GetFileName(skills.AVA_NT);
                string imageAVA_Q = Path.GetFileName(skills.AVA_Q);
                string imageAVA_W = Path.GetFileName(skills.AVA_W);
                string imageAVA_E = Path.GetFileName(skills.AVA_E);
                string imageAVA_R = Path.GetFileName(skills.AVA_R);
                string vdnt = Path.GetFileName(skills.Vdnt);
                string vdq = Path.GetFileName(skills.Vdq);
                string vdw = Path.GetFileName(skills.Vdw);
                string vde = Path.GetFileName(skills.Vde);
                string vdr = Path.GetFileName(skills.Vdr);
                SqlCommand cmd = new SqlCommand("sp_addskill", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@champname", skills.ChampName);
                cmd.Parameters.AddWithValue("@skillnt", skills.NoiTai);
                cmd.Parameters.AddWithValue("@skillq", skills.SkillQ);
                cmd.Parameters.AddWithValue("@skillw", skills.SkillW);
                cmd.Parameters.AddWithValue("@skille", skills.SkillE);
                cmd.Parameters.AddWithValue("@skillr", skills.SkillR);
                cmd.Parameters.AddWithValue("@avant", imageAVA_NT);
                cmd.Parameters.AddWithValue("@avaq", imageAVA_Q);
                cmd.Parameters.AddWithValue("@avaw", imageAVA_W);
                cmd.Parameters.AddWithValue("@avae", imageAVA_E);
                cmd.Parameters.AddWithValue("@avar", imageAVA_R);
                cmd.Parameters.AddWithValue("@vdnt", vdnt);
                cmd.Parameters.AddWithValue("@vdq", vdq);
                cmd.Parameters.AddWithValue("@vdw", vdw);
                cmd.Parameters.AddWithValue("@vde", vde);
                cmd.Parameters.AddWithValue("@vdr", vdr);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response GetSkills(SqlConnection conn)
        {
            Response response = new Response();
            List<Skills> skillslist = new List<Skills>();

            try
            {
                string query = "SELECT * FROM skills";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Skills skills = new Skills
                            {
                                ChampName = reader["ChampName"].ToString(),
                                NoiTai = reader["Skillnt"].ToString(),
                                SkillQ = reader["Skillq"].ToString(),
                                SkillW = reader["Skillw"].ToString(),
                                SkillE = reader["Skille"].ToString(),
                                SkillR = reader["Skillr"].ToString(),
                                AVA_NT = reader["AVAnt"].ToString(),
                                AVA_Q = reader["AVAq"].ToString(),
                                AVA_W = reader["AVAw"].ToString(),
                                AVA_E = reader["AVAe"].ToString(),
                                AVA_R = reader["AVAr"].ToString(),
                                Vdnt = reader["Vdnt"].ToString(),
                                Vdq = reader["Vdq"].ToString(),
                                Vdw = reader["Vdw"].ToString(),
                                Vde = reader["Vde"].ToString(),
                                Vdr = reader["Vdr"].ToString()
                            };
                            skillslist.Add(skills);
                        }
                    }
                }
                response.StatusCode = 200;
                response.Data = skillslist;
                response.StatusMessage = "Skills retrieved successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return response;
        }
        public Response AddSkin(Skin skin, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                string skin1 = Path.GetFileName(skin.Skin1);
                string skin2 = Path.GetFileName(skin.Skin2);
                string skin3 = Path.GetFileName(skin.Skin3);
                string skin4 = Path.GetFileName(skin.Skin4);
                string skin5 = Path.GetFileName(skin.Skin5);
                string skin6 = Path.GetFileName(skin.Skin6);
                string skin7 = Path.GetFileName(skin.Skin7);
                string skin8 = Path.GetFileName(skin.Skin8);
                string skin9 = Path.GetFileName(skin.Skin9);
                string skin10 = Path.GetFileName(skin.Skin10);
                string skin11 = Path.GetFileName(skin.Skin11);
                string skin12 = Path.GetFileName(skin.Skin12);
                string skin13 = Path.GetFileName(skin.Skin13);
                string skin14 = Path.GetFileName(skin.Skin14);
                string skin15 = Path.GetFileName(skin.Skin15);
                string skin16 = Path.GetFileName(skin.Skin16);
                string skin17 = Path.GetFileName(skin.Skin17);
                string skin18 = Path.GetFileName(skin.Skin18);
                SqlCommand cmd = new SqlCommand("sp_addskin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@champname", skin.ChampName);
                cmd.Parameters.AddWithValue("@skin1", skin1);
                cmd.Parameters.AddWithValue("@skin2", skin2);
                cmd.Parameters.AddWithValue("@skin3", skin3);
                cmd.Parameters.AddWithValue("@skin4", skin4);
                cmd.Parameters.AddWithValue("@skin5", skin5);
                cmd.Parameters.AddWithValue("@skin6", skin6);
                cmd.Parameters.AddWithValue("@skin7", skin7);
                cmd.Parameters.AddWithValue("@skin8", skin8);
                cmd.Parameters.AddWithValue("@skin9", skin9);
                cmd.Parameters.AddWithValue("@skin10", skin10);
                cmd.Parameters.AddWithValue("@skin11", skin11);
                cmd.Parameters.AddWithValue("@skin12", skin12);
                cmd.Parameters.AddWithValue("@skin13", skin13);
                cmd.Parameters.AddWithValue("@skin14", skin14);
                cmd.Parameters.AddWithValue("@skin15", skin15);
                cmd.Parameters.AddWithValue("@skin16", skin16);
                cmd.Parameters.AddWithValue("@skin17", skin17);
                cmd.Parameters.AddWithValue("@skin18", skin18);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response GetSkin(SqlConnection conn)
        {
            Response response = new Response();
            List<Skin> skinlist = new List<Skin>();

            try
            {
                string query = "SELECT * FROM skins";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Skin skin = new Skin
                            {
                                ChampName = reader["ChampName"].ToString(),
                                Skin1 = reader["skin1"].ToString(),
                                Skin2 = reader["skin2"].ToString(),
                                Skin3 = reader["skin3"].ToString(),
                                Skin4 = reader["skin4"].ToString(),
                                Skin5 = reader["skin5"].ToString(),
                                Skin6 = reader["skin6"].ToString(),
                                Skin7 = reader["skin7"].ToString(),
                                Skin8 = reader["skin8"].ToString(),
                                Skin9 = reader["skin9"].ToString(),
                                Skin10 = reader["skin10"].ToString(),
                                Skin11 = reader["skin11"].ToString(),
                                Skin12 = reader["skin12"].ToString(),
                                Skin13 = reader["skin13"].ToString(),
                                Skin14 = reader["skin14"].ToString(),
                                Skin15 = reader["skin15"].ToString(),
                                Skin16 = reader["skin16"].ToString(),
                                Skin17 = reader["skin17"].ToString(),
                                Skin18 = reader["skin18"].ToString(),

                            };
                            skinlist.Add(skin);
                        }
                    }
                }
                response.StatusCode = 200;
                response.Data = skinlist;
                response.StatusMessage = "Skin retrieved successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return response;
        }
        public Response DeleteChamp(Champions_Delete delchamp, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_deletechamp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@champname", delchamp.ChampName);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response DeleteSkill (Skills_Delete delskill, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_deleteskill", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@champname", delskill.ChampName);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response DeleteSkin(Skin_Delete delskin, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_deleteskin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@champname", delskin.ChampName);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response AddItem(Item item, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                string imageitem = Path.GetFileName(item.picitem);
                SqlCommand cmd = new SqlCommand("sp_additem", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nameitem", item.nameitem);
                cmd.Parameters.AddWithValue("@picitem", imageitem);
                cmd.Parameters.AddWithValue("@health", item.health);
                cmd.Parameters.AddWithValue("@atkdame", item.atkdame);
                cmd.Parameters.AddWithValue("@apdame", item.apdame);
                cmd.Parameters.AddWithValue("@atkspeed", item.atkspeed);
                cmd.Parameters.AddWithValue("@crit", item.crit);
                cmd.Parameters.AddWithValue("@armor", item.armor);
                cmd.Parameters.AddWithValue("@magicresis", item.magicresis);
                cmd.Parameters.AddWithValue("@ahaste", item.ahaste);
                cmd.Parameters.AddWithValue("@movespeed", item.movespeed);
                cmd.Parameters.AddWithValue("@noitaiitem", item.noitaiitem);
                cmd.Parameters.AddWithValue("@cost", item.cost);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response GetItem(SqlConnection conn)
        {
            Response response = new Response();
            List<Item> itemlist = new List<Item>();

            try
            {
                string query = "SELECT * FROM item";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Item item = new Item
                            {
                                nameitem = reader["nameitem"].ToString(),
                                picitem = reader["picitem"].ToString(),
                                health = reader["health"].ToString(),
                                atkdame = reader["atkdame"].ToString(),
                                apdame = reader["apdame"].ToString(),
                                atkspeed = reader["atkspeed"].ToString(),
                                crit = reader["crit"].ToString(),
                                armor = reader["armor"].ToString(),
                                magicresis = reader["magicresis"].ToString(),
                                ahaste = reader["ahaste"].ToString(),
                                movespeed = reader["movespeed"].ToString(),
                                noitaiitem = reader["noitaiitem"].ToString(),
                                cost = reader["cost"].ToString(),
                            };
                            itemlist.Add(item);
                        }
                    }
                }
                response.StatusCode = 200;
                response.Data = itemlist;
                response.StatusMessage = "Item added successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return response;
        }
        public Response DeleteItem(Item_Delete delitem, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_deleteitem", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nameitem", delitem.nameitem);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response UpdateItem(Item updateitem, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                string imageitem = Path.GetFileName(updateitem.picitem);
                SqlCommand cmd = new SqlCommand("sp_updateitem", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(updateitem.nameitem))
                {
                    cmd.Parameters.AddWithValue("@nameitem", updateitem.nameitem);
                }
                if (!string.IsNullOrEmpty(imageitem))
                {
                    cmd.Parameters.AddWithValue("@picitem", imageitem);
                }
                if (!string.IsNullOrEmpty(updateitem.health))
                {
                    cmd.Parameters.AddWithValue("@health", updateitem.health);
                }
                if (!string.IsNullOrEmpty(updateitem.atkdame))
                {
                    cmd.Parameters.AddWithValue("@atkdame", updateitem.atkdame);
                }
                if (!string.IsNullOrEmpty(updateitem.apdame))
                {
                    cmd.Parameters.AddWithValue("@apdame", updateitem.apdame);
                }
                if (!string.IsNullOrEmpty(updateitem.atkspeed))
                {
                    cmd.Parameters.AddWithValue("@atkspeed", updateitem.atkspeed);
                }
                if (!string.IsNullOrEmpty(updateitem.crit))
                {
                    cmd.Parameters.AddWithValue("@crit", updateitem.crit);
                }
                if (!string.IsNullOrEmpty(updateitem.armor))
                {
                    cmd.Parameters.AddWithValue("@armor", updateitem.armor);
                }
                if (!string.IsNullOrEmpty(updateitem.magicresis))
                {
                    cmd.Parameters.AddWithValue("@magicresis", updateitem.magicresis);
                }
                if (!string.IsNullOrEmpty(updateitem.ahaste))
                {
                    cmd.Parameters.AddWithValue("@ahaste", updateitem.ahaste);
                }
                if (!string.IsNullOrEmpty(updateitem.movespeed))
                {
                    cmd.Parameters.AddWithValue("@movespeed", updateitem.movespeed);
                }
                if (!string.IsNullOrEmpty(updateitem.noitaiitem))
                {
                    cmd.Parameters.AddWithValue("@noitaiitem", updateitem.noitaiitem);
                }
                if (!string.IsNullOrEmpty(updateitem.cost))
                {
                    cmd.Parameters.AddWithValue("@cost", updateitem.cost);
                }
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response AddUpHead(Update_Head uphead, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                string imageheadblur = Path.GetFileName(uphead.picheadblur);
                string imageheadmain = Path.GetFileName(uphead.picheadmain);
                string imageupdate = Path.GetFileName(uphead.picupdate);
                SqlCommand cmd = new SqlCommand("sp_add_update_head", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@version_update", uphead.version_update);
                cmd.Parameters.AddWithValue("@picheadblur", imageheadblur);
                cmd.Parameters.AddWithValue("@picheadmain", imageheadmain);
                cmd.Parameters.AddWithValue("@maintitle", uphead.maintitle);
                cmd.Parameters.AddWithValue("@maindestitle", uphead.maindestitle);
                cmd.Parameters.AddWithValue("@picupdate", imageupdate);
                cmd.Parameters.AddWithValue("@upversiontitle1", uphead.upversiontitle1);
                cmd.Parameters.AddWithValue("@upversiontitle_con1", uphead.upversiontitle_con1);
                cmd.Parameters.AddWithValue("@upversiontitle2", uphead.upversiontitle2);
                cmd.Parameters.AddWithValue("@upversiontitle_con2", uphead.upversiontitle_con2);
                cmd.Parameters.AddWithValue("@upversiontitle3", uphead.upversiontitle3);
                cmd.Parameters.AddWithValue("@upversiontitle_con3", uphead.upversiontitle_con3);
                cmd.Parameters.AddWithValue("@upversiontitle4", uphead.upversiontitle4);
                cmd.Parameters.AddWithValue("@upversiontitle_con4", uphead.upversiontitle_con4);
                cmd.Parameters.AddWithValue("@upversiontitle5", uphead.upversiontitle5);
                cmd.Parameters.AddWithValue("@upversiontitle_con5", uphead.upversiontitle_con5);
                cmd.Parameters.AddWithValue("@nangcaptrainghiem", uphead.nangcaptrainghiem);
                cmd.Parameters.AddWithValue("@sualoi", uphead.sualoi);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response GetUpHead(SqlConnection conn)
        {
            Response response = new Response();
            List<Update_Head> upheadlist = new List<Update_Head>();

            try
            {
                string query = "SELECT * FROM update_head";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Update_Head uphead = new Update_Head
                            {
                                version_update = reader["version_update"].ToString(),
                                picheadblur = reader["picheadblur"].ToString(),
                                picheadmain = reader["picheadmain"].ToString(),
                                maintitle = reader["maintitle"].ToString(),
                                maindestitle = reader["maindestitle"].ToString(),
                                picupdate = reader["picupdate"].ToString(),
                                upversiontitle1 = reader["upversiontitle1"].ToString(),
                                upversiontitle_con1 = reader["upversiontitle_con1"].ToString(),
                                upversiontitle2 = reader["upversiontitle2"].ToString(),
                                upversiontitle_con2 = reader["upversiontitle_con2"].ToString(),
                                upversiontitle3 = reader["upversiontitle3"].ToString(),
                                upversiontitle_con3 = reader["upversiontitle_con3"].ToString(),
                                upversiontitle4 = reader["upversiontitle4"].ToString(),
                                upversiontitle_con4 = reader["upversiontitle_con4"].ToString(),
                                upversiontitle5 = reader["upversiontitle5"].ToString(),
                                upversiontitle_con5 = reader["upversiontitle_con5"].ToString(),
                                nangcaptrainghiem = reader["nangcaptrainghiem"].ToString(),
                                sualoi = reader["sualoi"].ToString(),
                            };
                            upheadlist.Add(uphead);
                        }
                    }
                }
                response.StatusCode = 200;
                response.Data = upheadlist;
                response.StatusMessage = "Update head added successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return response;
        }
        public Response DeleteUpHead(Update_Head_Delete upheaddel, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_delete_update_head", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@version_update", upheaddel.version_update);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response UpdateUpHead(Update_Head upuphead, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                string imageheadblur = Path.GetFileName(upuphead.picheadblur);
                string imageheadmain = Path.GetFileName(upuphead.picheadmain);
                string imageupdate = Path.GetFileName(upuphead.picupdate);
                SqlCommand cmd = new SqlCommand("sp_update_update_head", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(upuphead.version_update))
                {
                    cmd.Parameters.AddWithValue("@version_update", upuphead.version_update);
                }
                if (!string.IsNullOrEmpty(imageheadblur))
                {
                    cmd.Parameters.AddWithValue("@picheadblur", imageheadblur);
                }
                if (!string.IsNullOrEmpty(imageheadmain))
                {
                    cmd.Parameters.AddWithValue("@picheadmain", imageheadmain);
                }
                if (!string.IsNullOrEmpty(upuphead.maintitle))
                {
                    cmd.Parameters.AddWithValue("@maintitle", upuphead.maintitle);
                }
                if (!string.IsNullOrEmpty(upuphead.maindestitle))
                {
                    cmd.Parameters.AddWithValue("@maindestitle", upuphead.maindestitle);
                }
                if (!string.IsNullOrEmpty(imageupdate))
                {
                    cmd.Parameters.AddWithValue("@picupdate", imageupdate);
                }
                if (!string.IsNullOrEmpty(upuphead.upversiontitle1))
                {
                    cmd.Parameters.AddWithValue("@upversiontitle1", upuphead.upversiontitle1);
                }
                if (!string.IsNullOrEmpty(upuphead.upversiontitle_con1))
                {
                    cmd.Parameters.AddWithValue("@upversiontitle_con1", upuphead.upversiontitle_con1);
                }
                if (!string.IsNullOrEmpty(upuphead.upversiontitle2))
                {
                    cmd.Parameters.AddWithValue("@upversiontitle2", upuphead.upversiontitle2);
                }
                if (!string.IsNullOrEmpty(upuphead.upversiontitle_con2))
                {
                    cmd.Parameters.AddWithValue("@upversiontitle_con2", upuphead.upversiontitle_con2);
                }
                if (!string.IsNullOrEmpty(upuphead.upversiontitle3))
                {
                    cmd.Parameters.AddWithValue("@upversiontitle3", upuphead.upversiontitle3);
                }
                if (!string.IsNullOrEmpty(upuphead.upversiontitle_con3))
                {
                    cmd.Parameters.AddWithValue("@upversiontitle_con3", upuphead.upversiontitle_con3);
                }
                if (!string.IsNullOrEmpty(upuphead.upversiontitle4))
                {
                    cmd.Parameters.AddWithValue("@upversiontitle4", upuphead.upversiontitle4);
                }
                if (!string.IsNullOrEmpty(upuphead.upversiontitle_con4))
                {
                    cmd.Parameters.AddWithValue("@upversiontitle_con4", upuphead.upversiontitle_con4);
                }
                if (!string.IsNullOrEmpty(upuphead.upversiontitle5))
                {
                    cmd.Parameters.AddWithValue("@upversiontitle5", upuphead.upversiontitle5);
                }
                if (!string.IsNullOrEmpty(upuphead.upversiontitle_con5))
                {
                    cmd.Parameters.AddWithValue("@upversiontitle_con5", upuphead.upversiontitle_con5);
                }
                if (!string.IsNullOrEmpty(upuphead.nangcaptrainghiem))
                {
                    cmd.Parameters.AddWithValue("@nangcaptrainghiem", upuphead.nangcaptrainghiem);
                }
                if (!string.IsNullOrEmpty(upuphead.sualoi))
                {
                    cmd.Parameters.AddWithValue("@sualoi", upuphead.sualoi);
                }
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response AddDetailsUpChamp(DetailsChamp detailsupchamp, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_add_details_update_champ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ChampName", detailsupchamp.ChampName);
                cmd.Parameters.AddWithValue("@dame_nt", detailsupchamp.dame_nt);
                cmd.Parameters.AddWithValue("@dame_q", detailsupchamp.dame_q);
                cmd.Parameters.AddWithValue("@dame_w", detailsupchamp.dame_w);
                cmd.Parameters.AddWithValue("@dame_e", detailsupchamp.dame_e);
                cmd.Parameters.AddWithValue("@dame_r", detailsupchamp.dame_r);
                cmd.Parameters.AddWithValue("@detail_NN", detailsupchamp.detail_NN);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response GetDetailsUpChamp(SqlConnection conn)
        {
            Response response = new Response();
            List<DetailsChamp> detailsupchamplist = new List<DetailsChamp>();

            try
            {
                string query = "SELECT * FROM detail_update_champ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DetailsChamp item = new DetailsChamp
                            {
                                ChampName = reader["ChampName"].ToString(),
                                dame_nt = reader["dame_nt"].ToString(),
                                dame_q = reader["dame_q"].ToString(),
                                dame_w = reader["dame_w"].ToString(),
                                dame_e = reader["dame_e"].ToString(),
                                dame_r = reader["dame_r"].ToString(),
                                detail_NN = reader["detail_NN"].ToString(),
                            };
                            detailsupchamplist.Add(item);
                        }
                    }
                }
                response.StatusCode = 200;
                response.Data = detailsupchamplist;
                response.StatusMessage = "Details champ added successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return response;
        }
        public Response DeleteDetailsUpChamp(DetailsChamp_Delete detailsupchampdel, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_delete_details_update_champ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ChampName", detailsupchampdel.ChampName);
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
        public Response UpdateDetailsUpChamp(DetailsChamp updetailsupchamp, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_update_details_update_champ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(updetailsupchamp.ChampName))
                {
                    cmd.Parameters.AddWithValue("@ChampName", updetailsupchamp.ChampName);
                }
                if (!string.IsNullOrEmpty(updetailsupchamp.dame_nt))
                {
                    cmd.Parameters.AddWithValue("@dame_nt", updetailsupchamp.dame_nt);
                }
                if (!string.IsNullOrEmpty(updetailsupchamp.dame_q))
                {
                    cmd.Parameters.AddWithValue("@dame_q", updetailsupchamp.dame_q);
                }
                if (!string.IsNullOrEmpty(updetailsupchamp.dame_w))
                {
                    cmd.Parameters.AddWithValue("@dame_w", updetailsupchamp.dame_w);
                }
                if (!string.IsNullOrEmpty(updetailsupchamp.dame_e))
                {
                    cmd.Parameters.AddWithValue("@dame_e", updetailsupchamp.dame_e);
                }
                if (!string.IsNullOrEmpty(updetailsupchamp.dame_r))
                {
                    cmd.Parameters.AddWithValue("@dame_r", updetailsupchamp.dame_r);
                }
                if (!string.IsNullOrEmpty(updetailsupchamp.detail_NN))
                {
                    cmd.Parameters.AddWithValue("@detail_NN", updetailsupchamp.detail_NN);
                }
                cmd.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string mess = (string)cmd.Parameters["@ErrorMessage"].Value;
                response.StatusCode = i > 0 ? 200 : 100;
                response.StatusMessage = mess;
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }
            return response;
        }
    }
}
