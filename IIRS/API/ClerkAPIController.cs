using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IIRS.DAL;
using IIRS.Repository;
using IIRS.Core;
using IIRS.Core.Model;
using System.Web;


namespace IIRS.API
{
    public class ClerkAPIController : ApiController
    {

        public void GET()
        {
            
        }
        [HttpPost]
        public dynamic AddCustomer(User user)
        {
            ClerkRepository _rep = new ClerkRepository();
            _rep.AddCustomer(user);
            return true;
        }
        [HttpPost]
        public dynamic GetCustomer([FromBody]DataTableRequest<GetCustomer_Result> request)
        {
            ClerkRepository _rep = new ClerkRepository();
            request.data = _rep.GetCustomer(request.search.value);
            var data = request.data.OrderBy(x => x.UserId).Skip(request.start).Take(request.length);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = request.draw,
                recordsTotal = request.data.Count,
                recordsFiltered = request.data.Count,
                data = data,
                length = request.length
            });
        

            
        }
        [HttpPost]
        public dynamic GetFiles([FromBody]DataTableRequest<GetFiles_Result> request)
        {
            try
            {
                ClerkRepository _rep = new ClerkRepository();
                request.data = _rep.GetCustomerFiles(request.model.UserId, request.search.value);
                var data = request.data.OrderBy(x => x.UserId).Skip(request.start).Take(request.length);
                return Json(new
                {
                    // this is what datatables wants sending back
                    draw = request.draw,
                    recordsTotal = request.data.Count,
                    recordsFiltered = request.data.Count,
                    data = data,
                    length = request.length
                });
            }
            catch(Exception e)
            {
                return false;
            }
        }

        [HttpPost]
        public dynamic UploadFile()
        {
            try
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    // Get the uploaded image from the Files collection
                    var httpPostedFile = HttpContext.Current.Request.Files["UploadedFile"];

                    string extension = System.IO.Path.GetExtension(httpPostedFile.FileName);
                    if (httpPostedFile != null)
                    {
                        // Validate the uploaded image(optional)

                        // Get the complete file path
                        ClerkRepository _rep = new ClerkRepository();
                        File file = new File() { UserId =Convert.ToInt32(HttpContext.Current.Request.Form["UserId"])};
                        file.FileName = httpPostedFile.FileName;
                        _rep.UploadFile(file);
                        file.FileName = file.FileId + extension;
                        var fileSavePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), file.FileName);
                        _rep.UploadFile(file);
                        // Save the uploaded file to "UploadedFiles" folder
                        httpPostedFile.SaveAs(fileSavePath);
                        
                    }
                }
            }
            catch(Exception e)
            {

            }

            return true;
        }

        [HttpPost]
        public dynamic SendOrder([FromBody]Order order)
        {
            ClerkRepository _rep = new ClerkRepository();
            //order.UserId = IIRS.Common.SessionManager.LoggedInUser.UserID;
            //_rep.SendOrder(order);
            return true;
        }


    }
}
