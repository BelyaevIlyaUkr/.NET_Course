// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function filterFieldTogglingFunction(chose) {
    if (chose === 'allStudents' || chose === 'allCourses' || chose === 'allHomeTasks'
        || chose === 'allGrades' || chose === 'allLecturers') {
        document.getElementById('FilterField').style.display = 'none';
        document.getElementById('FilterFieldLabel').style.display = 'none';
    }
    else {
        document.getElementById('FilterField').style.display = 'inline-block';
        document.getElementById('FilterFieldLabel').style.display = 'inline-block';
    }
}