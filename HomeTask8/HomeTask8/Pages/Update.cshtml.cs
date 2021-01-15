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
    public class UpdateModel : PageModel
    {
        SqlConnection Connection { get; }

        [Display(Name = "Object type")]
        [BindProperty]
        public string SelectedInfoType { get; set; }

        public List<SelectListItem> InfoTypes { get; }

        [BindProperty]
        public int FirstInputField { get; set; }

        [BindProperty]
        public string SecondInputField { get; set; }

        [BindProperty]
        public string ThirdInputField { get; set; }

        [BindProperty]
        public string FourthInputField { get; set; }

        [BindProperty]
        public string FifthInputField { get; set; }

        [BindProperty]
        public string SixthInputField { get; set; }

        public string ExceptionMessage { get; set; }

        public string ResultMessage { get; set; }

        public List<object> Objects { get; set; }

        public UpdateModel(IConfiguration configuration)
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

                ResultMessage = "Object was updated successfully";

                switch (SelectedInfoType)
                {
                    case null:
                        throw new Exception("type of information isn't chosed");
                    case "student":
                        if (IsAnyVisibleInputFieldNotFilled(6))
                            throw new Exception("all input fields must be filled (first field with non-zero value)");

                        SecondInputField = SecondInputField.Trim();
                        ThirdInputField = ThirdInputField.Trim();
                        FourthInputField = FourthInputField.Trim();
                        FifthInputField = FifthInputField.Trim();
                        SixthInputField = SixthInputField.Trim();

                        if (!IsValidFirstOrLastName(SecondInputField))
                            throw new Exception("incorrect first name");

                        if (!IsValidFirstOrLastName(ThirdInputField))
                            throw new Exception("incorrect last name");

                        if (!IsValidPhoneNumber(FourthInputField))
                            throw new Exception("incorrect phone number");

                        if (!IsValidEmailAddress(FifthInputField))
                            throw new Exception("incorrect email address");

                        var studentToUpdate = new Student
                        {
                            StudentID = FirstInputField,
                            FirstName = SecondInputField,
                            LastName = ThirdInputField,
                            PhoneNumber = FourthInputField,
                            Email = FifthInputField,
                            Github = SixthInputField
                        };

                        var numberOfAffectedStudentRows = await Repository.UpdateStudentAsync(Connection, studentToUpdate);

                        if (numberOfAffectedStudentRows == 0)
                            ResultMessage = "There isn't student with such ID in database";
                        else
                            Objects.AddRange(await Repository.GetAllStudentsAsync(Connection));
                        break;
                    case "course":
                        if (IsAnyVisibleInputFieldNotFilled(6))
                            throw new Exception("all input fields must be filled (first field with non-zero value)");

                        SecondInputField = SecondInputField.Trim();
                        ThirdInputField = ThirdInputField.Trim();
                        FourthInputField = FourthInputField.Trim();
                        FifthInputField = FifthInputField.Trim();

                        if (!ValidateDateTimeAndGetParsed(ThirdInputField, out DateTime resultStartDate))
                            throw new Exception("incorrect start date");

                        if (!ValidateDateTimeAndGetParsed(FourthInputField, out DateTime resultEndDate))
                            throw new Exception("incorrect end date");

                        if (!ValidateIntAndGetParsed(FifthInputField, out int resultPassingScore))
                            throw new Exception("incorrect passing score");

                        var courseToUpdate = new Course
                        {
                            CourseID = FirstInputField,
                            Name = SecondInputField,
                            StartDate = resultStartDate,
                            EndDate = resultEndDate,
                            PassingScore = resultPassingScore
                        };

                        var numberOfAffectedCourseRows = await Repository.UpdateCourseAsync(Connection, courseToUpdate);

                        if (numberOfAffectedCourseRows == 0)
                            ResultMessage = "There isn't course with such ID in database";
                        else
                            Objects.AddRange(await Repository.GetAllCoursesAsync(Connection));
                        break;
                    case "lecturer":
                        if (IsAnyVisibleInputFieldNotFilled(3))
                            throw new Exception("all input fields must be filled (first field with non-zero value)");

                        SecondInputField = SecondInputField.Trim();
                        ThirdInputField = ThirdInputField.Trim();

                        if (!IsValidFirstOrLastName(SecondInputField))
                            throw new Exception("incorrect name");

                        if (!ValidateDateTimeAndGetParsed(ThirdInputField, out DateTime resultBirthDate))
                            throw new Exception("incorrect birth date");

                        var lecturerToUpdate = new Lecturer
                        {
                            LecturerID = FirstInputField,
                            Name = SecondInputField,
                            BirthDate = resultBirthDate
                        };

                        var numberOfAffectedLecturerRows = await Repository.UpdateLecturerAsync(Connection, lecturerToUpdate);

                        if (numberOfAffectedLecturerRows == 0)
                            ResultMessage = "There isn't lecturer with such ID in database";
                        else
                            Objects.AddRange(await Repository.GetAllLecturersAsync(Connection));
                        break;
                    case "hometask":
                        if (IsAnyVisibleInputFieldNotFilled(6))
                            throw new Exception("all input fields must be filled (first field with non-zero value)");

                        SecondInputField = SecondInputField.Trim();
                        ThirdInputField = ThirdInputField.Trim();
                        FourthInputField = FourthInputField.Trim();
                        FifthInputField = FifthInputField.Trim();
                        SixthInputField = SixthInputField.Trim();

                        if (!ValidateDateTimeAndGetParsed(FourthInputField, out DateTime resultTaskDate))
                            throw new Exception("incorrect task date");

                        if (!ValidateIntAndGetParsed(FifthInputField, out int resultSerialNumber))
                            throw new Exception("incorrect serial number");

                        if (!ValidateIntAndGetParsed(SixthInputField, out int resultCourseID))
                            throw new Exception("incorrect course ID input number");

                        var hometaskToUpdate = new HomeTask
                        {
                            HomeTaskID = FirstInputField,
                            Name = SecondInputField,
                            Description = ThirdInputField,
                            TaskDate = resultTaskDate,
                            SerialNumber = resultSerialNumber,
                            CourseID = resultCourseID
                        };

                        var numberOfAffectedHometaskRows = await Repository.UpdateHomeTaskAsync(Connection, hometaskToUpdate);

                        if (numberOfAffectedHometaskRows == 0)
                            ResultMessage = "There isn't hometask with such ID in database";
                        else
                            Objects.AddRange(await Repository.GetAllHomeTasksAsync(Connection));
                        break;
                    case "grade":
                        if (IsAnyVisibleInputFieldNotFilled(5))
                            throw new Exception("all input fields must be filled (first field with non-zero value)");

                        SecondInputField = SecondInputField.Trim();
                        ThirdInputField = ThirdInputField.Trim();
                        FourthInputField = FourthInputField.Trim();
                        FifthInputField = FifthInputField.Trim();

                        if (!ValidateDateTimeAndGetParsed(SecondInputField, out DateTime resultGradeDate))
                            throw new Exception("incorrect grade date");

                        if (!ValidateBoolAndGetParsed(ThirdInputField, out bool resultIsComplete))
                            throw new Exception("incorrect is complete");

                        if (!ValidateIntAndGetParsed(FourthInputField, out int resultHometaskID))
                            throw new Exception("incorrect hometask ID input number");

                        if (!ValidateIntAndGetParsed(FifthInputField, out int resultStudentID))
                            throw new Exception("incorrect student ID input number");

                        var gradeToUpdate = new Grade
                        {
                            GradeDate = resultGradeDate,
                            IsComplete = resultIsComplete,
                            HomeTaskID = resultHometaskID,
                            StudentID = resultStudentID
                        };

                        var numberOfAffectedGradeRows = await Repository.UpdateGradeAsync(Connection, gradeToUpdate);

                        if (numberOfAffectedGradeRows == 0)
                            ResultMessage = "There isn't grade with such ID in database";
                        else
                        {
                            Objects.AddRange(await Repository.GetAllGradesAsync(Connection));
                        }
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

        bool IsAnyVisibleInputFieldNotFilled(int numberOfVisibleFields)
        {
            if (FirstInputField == 0)
                return true;

            List<string> allfieldsExceptFirst = new List<string> { SecondInputField, ThirdInputField, FourthInputField,
                FifthInputField, SixthInputField};

            for (int i = 0; i < numberOfVisibleFields - 1 ; i++)
            {
                if (allfieldsExceptFirst[i] == null)
                    return true;
            }

            return false;
        }
    }
}
