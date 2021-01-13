using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using StudyManager.Models;
using StudyManager.DataAccess.ADO;
using System.Globalization;
using System.Text.RegularExpressions;
using PhoneNumbers;
using System.Net.Mail;

namespace HomeTask8.Pages
{
    public class AddModel : PageModel
    {
        SqlConnection Connection { get; }

        [Display(Name = "Object type")]
        [BindProperty(SupportsGet = true)]
        public string SelectedInfoType { get; set; }

        public List<SelectListItem> InfoTypes { get; }

        [BindProperty]
        public string FirstInputField { get; set; }

        [BindProperty]
        public string SecondInputField { get; set; }

        [BindProperty]
        public string ThirdInputField { get; set; }

        [BindProperty]
        public string FourthInputField { get; set; }

        [BindProperty]
        public string FifthInputField { get; set; }

        public string ExceptionMessage { get; set; }

        public List<object> Objects { get; set; }

        public AddModel(IConfiguration configuration)
        {
            //Creating connection object one time, here, because it is performance expensive operation.
            Connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

            InfoTypes = new List<SelectListItem> {
                new SelectListItem { Value = "student", Text = "student" },
                new SelectListItem { Value = "course", Text = "course" },
                new SelectListItem { Value = "lecturer", Text = "lecturer" },
                new SelectListItem { Value = "hometask", Text = "hometask" },
                new SelectListItem { Value = "grade", Text = "grade" },
            };

            Objects = new List<object>();
        }

        public async Task OnPostAsync()
        {
            try
            {
                Connection.Open();

                ExceptionMessage = null;

                Objects.Clear();

                switch (SelectedInfoType)
                {
                    case "student":
                        if (IsAnyVisibleFieldNull(5))
                            throw new Exception("all input fields must be filled");

                        FirstInputField = FirstInputField.Trim();
                        SecondInputField = SecondInputField.Trim();
                        ThirdInputField = ThirdInputField.Trim();
                        FourthInputField = FourthInputField.Trim();
                        FifthInputField = FifthInputField.Trim();

                        if (!IsValidFirstOrLastName(FirstInputField))
                            throw new Exception("incorrect first name");

                        if (!IsValidFirstOrLastName(SecondInputField))
                            throw new Exception("incorrect last name");

                        if (!IsValidPhoneNumber(ThirdInputField))
                            throw new Exception("incorrect phone number");

                        if (!IsValidEmailAddress(FourthInputField))
                            throw new Exception("incorrect email address");

                        var newStudent = new Student
                        {
                            FirstName = FirstInputField,
                            LastName = SecondInputField,
                            PhoneNumber = ThirdInputField,
                            Email = FourthInputField,
                            Github = FifthInputField
                        };

                        await Repository.CreateStudentAsync(Connection, newStudent);
                        Objects.AddRange(await Repository.GetAllStudentsAsync(Connection));
                        break;
                    case "course":
                        if (IsAnyVisibleFieldNull(4))
                            throw new Exception("all input fields must be filled");

                        FirstInputField = FirstInputField.Trim();
                        SecondInputField = SecondInputField.Trim();
                        ThirdInputField = ThirdInputField.Trim();
                        FourthInputField = FourthInputField.Trim();

                        if (!ValidateDateTimeAndGetParsed(SecondInputField, out DateTime resultStartDate))
                            throw new Exception("incorrect start date");

                        if (!ValidateDateTimeAndGetParsed(ThirdInputField, out DateTime resultEndDate))
                            throw new Exception("incorrect end date");

                        if(!ValidateIntAndGetParsed(FourthInputField, out int resultPassingScore))
                            throw new Exception("incorrect passing score");

                        var newCourse = new Course
                        {
                            Name = FirstInputField,
                            StartDate = resultStartDate,
                            EndDate = resultEndDate,
                            PassingScore = resultPassingScore
                        };

                        await Repository.CreateCourseAsync(Connection, newCourse);
                        Objects.AddRange(await Repository.GetAllCoursesAsync(Connection));
                        break;
                    case "lecturer":
                        if (IsAnyVisibleFieldNull(2))
                            throw new Exception("all input fields must be filled");

                        FirstInputField = FirstInputField.Trim();
                        SecondInputField = SecondInputField.Trim();

                        if (!IsValidFirstOrLastName(FirstInputField))
                            throw new Exception("incorrect name");

                        if (!ValidateDateTimeAndGetParsed(SecondInputField, out DateTime resultBirthDate))
                            throw new Exception("incorrect birth date");

                        var newLecturer = new Lecturer
                        {
                            Name = FirstInputField,
                            BirthDate = resultBirthDate
                        };

                        await Repository.CreateLecturerAsync(Connection, newLecturer);
                        Objects.AddRange(await Repository.GetAllLecturersAsync(Connection));
                        break;
                    case "hometask":
                        if (IsAnyVisibleFieldNull(5))
                            throw new Exception("all input fields must be filled");

                        FirstInputField = FirstInputField.Trim();
                        SecondInputField = SecondInputField.Trim();
                        ThirdInputField = ThirdInputField.Trim();
                        FourthInputField = FourthInputField.Trim();
                        FifthInputField = FifthInputField.Trim();

                        if (!ValidateDateTimeAndGetParsed(ThirdInputField, out DateTime resultTaskDate))
                            throw new Exception("incorrect task date");

                        if (!ValidateIntAndGetParsed(FourthInputField, out int resultSerialNumber))
                            throw new Exception("incorrect serial number");

                        if (!ValidateIntAndGetParsed(FifthInputField, out int resultCourseID))
                            throw new Exception("incorrect course ID input number");

                        var newHometask = new HomeTask
                        {
                            Name = FirstInputField,
                            Description = SecondInputField,
                            TaskDate = resultTaskDate,
                            SerialNumber = resultSerialNumber,
                            CourseID = resultCourseID
                        };

                        await Repository.CreateHomeTaskAsync(Connection, newHometask);
                        Objects.AddRange(await Repository.GetAllHomeTasksAsync(Connection));
                        break;
                    case "grade":
                        if (IsAnyVisibleFieldNull(4))
                            throw new Exception("all input fields must be filled");

                        FirstInputField = FirstInputField.Trim();
                        SecondInputField = SecondInputField.Trim();
                        ThirdInputField = ThirdInputField.Trim();
                        FourthInputField = FourthInputField.Trim();

                        if (!ValidateDateTimeAndGetParsed(FirstInputField, out DateTime resultGradeDate))
                            throw new Exception("incorrect grade date");

                        if (!ValidateBoolAndGetParsed(SecondInputField, out bool resultIsComplete))
                            throw new Exception("incorrect is complete");

                        if (!ValidateIntAndGetParsed(ThirdInputField, out int resultHometaskID))
                            throw new Exception("incorrect hometask ID input number");

                        if (!ValidateIntAndGetParsed(FourthInputField, out int resultStudentID))
                            throw new Exception("incorrect student ID input number");

                        var newGrade = new Grade
                        {
                            GradeDate = resultGradeDate,
                            IsComplete = resultIsComplete,
                            HomeTaskID = resultHometaskID,
                            StudentID = resultStudentID
                        };

                        await Repository.CreateGradeAsync(Connection, newGrade);
                        Objects.AddRange(await Repository.GetAllGradesAsync(Connection));
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage = "Error: " + ex.Message;
            }
            finally
            {
                Connection.Close();
            }
        }

        bool IsValidFirstOrLastName(string name)
        {
            if (!Regex.IsMatch(name, @"^[\p{L}\p{M}'\s\.\-]+$"))
                return false;

            return true;
        }

        bool IsValidPhoneNumber(string number)
        {
            var phoneUtil = PhoneNumberUtil.GetInstance();

            PhoneNumber ukrNumberProto;

            try
            {
                ukrNumberProto = phoneUtil.Parse(number, "UA");
            }
            catch (Exception)
            {
                return false;
            }

            if (!phoneUtil.IsValidNumber(ukrNumberProto))
                return false;

            return true;
        }

        bool IsValidEmailAddress(string emailAddressString)
        {
            try
            {
                MailAddress m = new MailAddress(emailAddressString);
            }
            catch (FormatException)
            {
                return false;
            }

            return true;
        }

        bool ValidateDateTimeAndGetParsed(string dateString, out DateTime resultDateTime)
        {
            if (!DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out resultDateTime))
                return false;

            return true;
        }

        bool ValidateIntAndGetParsed(string numberString, out int resultNumber)
        {
            if (!int.TryParse(numberString, out resultNumber))
                return false;
            
            return true;
        }

        bool ValidateBoolAndGetParsed(string boolString, out bool resultBool)
        {
            resultBool = false;

            if (boolString.ToLower() == "yes")
                resultBool = true;
            else if (boolString.ToLower() != "no")
                return false;

            return true;
        }

        bool IsAnyVisibleFieldNull(int numberOfVisibleFields)
        {
            List<string> fields = new List<string> { FirstInputField, SecondInputField, ThirdInputField,
                FourthInputField, FifthInputField};
            
            for(int i = 0; i < numberOfVisibleFields; i++)
            {
                if (fields[i] == null)
                    return true;
            }

            return false;
        }
    }
}
