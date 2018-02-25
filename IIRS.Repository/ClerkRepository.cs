using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IIRS.DAL;
using IIRS.Core.Model;

namespace IIRS.Repository
{
    public class ClerkRepository:Connection
    {
        public void AddCustomer(User user)
        {
            try
            {
                user.Password = "123";
                user.Username = "Customer";
                _db.Users.Add(user);
                _db.UserInRoles.Add(new UserInRole() { RoleId = 3,UserId=user.UserId});
                _db.SaveChanges();
            }
            catch { };
        }

        public List<GetCustomer_Result> GetCustomer(string search)
        {

         return   _db.GetCustomer(search).ToList();
        }

        public List<GetFiles_Result> GetCustomerFiles(int? UserId,string search)
        {
            List<GetFiles_Result> fileList = _db.GetFiles(search, UserId).ToList();
            return fileList;
        }

        public File UploadFile(File file)
        {
            if (file.FileId == 0)
            {
                _db.Files.Add(file);
                _db.SaveChanges();
                return file;
            }
            else
            {
                File fileData = _db.Files.FirstOrDefault(x => x.FileId == file.FileId);
                fileData.FileName = file.FileName;
                _db.SaveChanges();
                return file;
            }
        }

        public File SendOrder(File file)
        {
           File fileData= _db.Files.FirstOrDefault(x => x.FileId == file.FileId);
            fileData.Description = file.Description;
            fileData.IsSend = true;
            _db.SaveChanges();
            return file;
        }
    }
}
