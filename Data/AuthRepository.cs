using System.Data;
using AspNetCoreWebAPI.Models;
using AspNetCoreWebAPI.DataLayer;

namespace AspNetCoreWebAPI.Data
{
    public class AuthRepository : IAuthRepository
    {
 public async Task<Users> Login(string username, string Pasword)
        {
            Users MyUser = null;
            DataConnection dc = new DataConnection();
            DataTable dt = await Task.Run(() => dc.GetData("SELECT * FROM Users where Username = '" + username + "'"));
            foreach (DataRow dr in dt.Rows)
            {
                MyUser = new Users();
                MyUser.UserID = Convert.ToInt32(dr["UserID"]);
                MyUser.Username = dr["Username"].ToString();
                MyUser.PasswordHash = (byte[])dr["PasswordHash"];
                MyUser.PasswordSalt = (byte[])dr["PasswordSalt"];
            }
            if (MyUser == null)
            {
                return null;
            }
            else
            {
                if (!VerifyPasswordHash(Pasword, MyUser.PasswordHash, MyUser.PasswordSalt))
                {
                    return null;
                }
                else
                {
                    return MyUser;
                }
            }

        }

        public async Task<Users> Register(Users user, string Password)
        {
            long retid = 0;
            DataConnection dc = new DataConnection();
            byte[] PasswordHash, PasswordSalt;
            CreatePasswordHash(Password, out PasswordHash, out PasswordSalt);
            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;
            await Task.Run(() => dc.SaveByProperty("Users", user, true, ref retid));
            return user;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < ComputedHash.Length; i++)
                {
                    if (ComputedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            DataConnection dc = new DataConnection();
            DataTable dt = await Task.Run(() => dc.GetData("SELECT * FROM Users where Username = '" + username + "'"));
            if (dt.Rows.Count>0) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}