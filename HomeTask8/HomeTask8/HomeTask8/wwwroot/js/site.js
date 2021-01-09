// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function filterFieldTogglingFunction() {
    var selectTag = document.getElementById('SelectedInfoType');

    var chose = selectTag.options[selectTag.selectedIndex].value;

    var elements = document.getElementsByClassName('filter_section');

    if (chose === "" || chose === 'allStudents' || chose === 'allCourses' || chose === 'allHomeTasks'
        || chose === 'allGrades' || chose === 'allLecturers') {
        for (var i = 0; i < elements.length; i++) {
            elements[i].style.display = 'none';
        }
    }
    else {
        for (var i = 0; i < elements.length; i++) {
            elements[i].style.display = 'inline-block';
        }

        switch (chose) {
            case 'allCoursesForStudent':
                elements[0].children[0].innerText = 'StudentID';
                break;
            case 'allStudentsInCourse':
            case 'allLecturersForCourse':
                elements[0].children[0].innerText = 'CourseID';
                break;
            case 'allCoursesWithDefiniteLecturer':
                elements[0].children[0].innerText = 'LecturerID';
        }
    }
}