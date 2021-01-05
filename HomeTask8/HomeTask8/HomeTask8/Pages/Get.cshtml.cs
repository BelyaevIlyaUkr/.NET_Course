using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace HomeTask8.Pages
{
    public class GetModel : PageModel
    {
        public IConfiguration Configuration { get; }

        public ArrayList Objects { get; set; }

        [Display(Name = "Information type")]
        public string SelectedInfoType { get; set; }

        public List<SelectListItem> InfoTypes { get; }

        public GetModel(IConfiguration configuration)
        {
            Configuration = configuration;

            Objects = new ArrayList();

            InfoTypes = new List<SelectListItem> {
                new SelectListItem { Text = "Choose Type", Disabled = true, Selected = true },
                new SelectListItem { Value = "allStudents", Text = "All students" },
                new SelectListItem { Value = "allCourses", Text = "All courses" },
                new SelectListItem { Value = "allLecturers", Text = "All lecturers"  },
                new SelectListItem { Value = "allHomeTasks", Text = "All hometasks"  },
                new SelectListItem { Value = "allGrades", Text = "All grades"  },
            };
        }

        public void OnGet()
        {
        }
    }
}
