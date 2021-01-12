// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function filterFieldTogglingInGet() {
    let selectTag = document.getElementById('SelectedInfoType');

    let chose = selectTag.options[selectTag.selectedIndex].value;

    let elements = document.getElementsByClassName('filter-section-in-get');

    if (chose === "" || chose === 'allStudents' || chose === 'allCourses' || chose === 'allHomeTasks'
        || chose === 'allGrades' || chose === 'allLecturers') {
        elements[0].style.display = 'none';
    }
    else {
        elements[0].style.display = 'inline-block';

        switch (chose) {
            case 'allCoursesForStudent':
                elements[0].children[0].innerHTML = 'StudentID';
                break;
            case 'allStudentsInCourse':
            case 'allLecturersForCourse':
                elements[0].children[0].innerHTML = 'CourseID';
                break;
            case 'allCoursesWithDefiniteLecturer':
                elements[0].children[0].innerHTML = 'LecturerID';
        }
    }
}

function inputFieldsInAddToggling(isClearingInputRequired) {
    let selectTag = document.getElementById('SelectedInfoType');

    let chose = selectTag.options[selectTag.selectedIndex].value;

    let elements = document.getElementsByClassName('AddPageInputFields');

    let label, placeholder;
    switch (chose) {
        case "":
            for (let i = 0; i < elements.length; i++) {
                elements[i].style.display = 'none';
            }
            break;
        case 'student':
            for (let i = 0; i < elements.length; i++) {
                elements[i].style.display = 'inline-block';
                switch (i) {
                    case 0:
                        label = 'First Name';
                        placeholder = 'e.g. Illia';
                        break;
                    case 1:
                        label = 'Last Name';
                        placeholder = 'e.g. Bieliaiev';
                        break;
                    case 2:
                        label = 'Phone Number';
                        placeholder = 'e.g. +380433333311';
                        break;
                    case 3:
                        label = 'Email';
                        placeholder = 'e.g. bel.i2000@omg.com';
                        break;
                    case 4:
                        label = 'Github';
                        placeholder = 'e.g. BelyaevIlyaUkr';
                        break;
                }
                elements[i].children[0].innerHTML = label;
                elements[i].children[2].placeholder = placeholder;
            }
            break;
        case 'course':
            for (let i = 0; i < elements.length; i++) {
                elements[i].style.display = 'inline-block';
                switch (i) {
                    case 0:
                        label = 'Name';
                        placeholder = 'e.g. .NET';
                        break;
                    case 1:
                        label = 'Start Date';
                        placeholder = 'e.g. 15/05/2021';
                        break;
                    case 2:
                        label = 'End Date';
                        placeholder = 'e.g. 27/09/2021';
                        break;
                    case 3:
                        label = 'Passing Score';
                        placeholder = 'e.g. 80';
                        break;
                    case 4:
                        elements[i].style.display = 'none';
                        break;
                }
                elements[i].children[0].innerHTML = label;
                elements[i].children[2].placeholder = placeholder;
            }
            break;
        case 'hometask':
            for (let i = 0; i < elements.length; i++) {
                elements[i].style.display = 'inline-block';
                switch (i) {
                    case 0:
                        label = 'Name';
                        placeholder = 'e.g. Generics';
                        break;
                    case 1:
                        label = 'Description';
                        placeholder = 'e.g. The best hometask';
                        break;
                    case 2:
                        label = 'Task Date';
                        placeholder = 'e.g. 26/10/2020';
                        break;
                    case 3:
                        label = 'Serial Number';
                        placeholder = 'e.g. 5';
                        break;
                    case 4:
                        label = 'CourseID';
                        placeholder = 'e.g. 24';
                        break;
                }
                elements[i].children[0].innerHTML = label;
                elements[i].children[2].placeholder = placeholder;
            }
            break;
        case 'grade':
            for (let i = 0; i < elements.length; i++) {
                elements[i].style.display = 'inline-block';
                switch (i) {
                    case 0:
                        label = 'Grade Date';
                        placeholder = 'e.g. 26/09/2020';
                        break;
                    case 1:
                        label = 'Is Complete';
                        placeholder = 'e.g. yes';
                        break;
                    case 2:
                        label = 'Hometask ID';
                        placeholder = 'e.g. 8';
                        break;
                    case 3:
                        label = 'Student ID';
                        placeholder = 'e.g. 22';
                        break;
                    case 4:
                        elements[i].style.display = 'none';
                        break;
                }
                elements[i].children[0].innerHTML = label;
                elements[i].children[2].placeholder = placeholder;
            }
            break;
        case 'lecturer':
            for (let i = 0; i < elements.length; i++) {
                elements[i].style.display = 'inline-block';
                switch (i) {
                    case 0:
                        label = 'Name';
                        placeholder = 'e.g. John';
                        break;
                    case 1:
                        label = 'Birth Date';
                        placeholder = 'e.g. 21/10/1991';
                        break;
                    case 2:
                    case 3:
                    case 4:
                        elements[i].style.display = 'none';
                        break;
                }
                elements[i].children[0].innerHTML = label;
                elements[i].children[2].placeholder = placeholder;
            }
            break;
    }

    if (isClearingInputRequired) {
        clearInputFieldsAddPage(elements);
    }
}

function clearResultPlaceInAddPage() {
    let errorLabel = document.getElementById('error-label');

    if (errorLabel !== null) {
        errorLabel.firstChild.data = '';
        return;
    }

    let elements = document.getElementsByClassName('table');

    elements[0].innerHTML = '';

    let congratulationalLabel = document.getElementById('congratulational-label');

    congratulationalLabel.firstChild.data = '';
}

function clearInputFieldsAddPage(elements) {
    for (let i = 0; i < elements.length; i++) {
        elements[i].children[2].value = null;
    }
}
