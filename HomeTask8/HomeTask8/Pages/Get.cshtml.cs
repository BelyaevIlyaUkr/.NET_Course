using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudyManager.Models;
using StudyManager.DataAccess.ADO;
using Microsoft.Extensions.Configuration;

namespace HomeTask8.Pages
{
    public class GetModel : PageModel
    {
        public void OnGet(string info_type)
        {
            /*switch (info_type)
            {
               
                case "all_students":
                    Repository.GetAllStudentsAsync()
            }*/
        }
    }
}
