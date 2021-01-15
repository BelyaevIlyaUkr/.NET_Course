// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function filterFieldTogglingInGet(isClearingInputRequired) {
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

        elements[0].children[2].placeholder = 'e.g. 22';

        if (isClearingInputRequired) {
            clearInputFieldsOnPage(elements);
        }
    }
}

function inputFieldsOnAddOrUpdatePageToggling(isClearingInputRequired, page) {
    let selectTag = document.getElementById('SelectedInfoType');

    let chose = selectTag.options[selectTag.selectedIndex].value;

    let elements;

    if (page === 'add')
        elements = document.getElementsByClassName('AddPageInputFields');
    else
        elements = document.getElementsByClassName('UpdatePageInputFields');

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

                if (page === 'update' && i == 0) {
                    elements[i].children[0].innerHTML = 'Student ID';
                    elements[i].children[2].placeholder = 'e.g. 22';
                    continue;
                }

                let currentField;

                if (page === 'add')
                    currentField = i;
                else
                    currentField = i - 1;

                switch (currentField) {
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

                if (page === 'update' && i == 0) {
                    elements[i].children[0].innerHTML = 'Course ID';
                    elements[i].children[2].placeholder = 'e.g. 22';
                    continue;
                }

                let currentField;

                if (page === 'add')
                    currentField = i;
                else
                    currentField = i - 1;

                switch (currentField) {
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

                if (page === 'update' && i == 0) {
                    elements[i].children[0].innerHTML = 'Hometask ID';
                    elements[i].children[2].placeholder = 'e.g. 22';
                    continue;
                }

                let currentField;

                if (page === 'add')
                    currentField = i;
                else
                    currentField = i - 1;

                switch (currentField) {
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

                if (page === 'update' && i == 0) {
                    elements[i].children[0].innerHTML = 'Grade ID';
                    elements[i].children[2].placeholder = 'e.g. 22';
                    continue;
                }

                let currentField;

                if (page === 'add')
                    currentField = i;
                else
                    currentField = i - 1;

                switch (currentField) {
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

                if (page === 'update' && i == 0) {
                    elements[i].children[0].innerHTML = 'Lecturer ID';
                    elements[i].children[2].placeholder = 'e.g. 22';
                    continue;
                }

                let currentField;

                if (page === 'add')
                    currentField = i;
                else
                    currentField = i - 1;

                switch (currentField) {
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
        clearInputFieldsOnPage(elements);
    }
}

function clearResultPlaceOnPage() {
    let errorLabel = document.getElementById('error-label');

    if (errorLabel !== null) {
        errorLabel.firstChild.data = '';
        return;
    }

    let resultLabel = document.getElementById('result-label');

    if (resultLabel !== null)
        resultLabel.firstChild.data = '';

    let tableElement = document.getElementsByClassName('table');

    if (tableElement !== null)
        tableElement[0].innerHTML = '';
}

function clearInputFieldsOnPage(elements) {
    for (let i = 0; i < elements.length; i++) {
        elements[i].children[2].value = null;
    }
}

function filterFieldTogglingOnDeletePage(isClearingInputRequired) {
    let selectTag = document.getElementById('SelectedInfoType');

    let chose = selectTag.options[selectTag.selectedIndex].value;

    let elements = document.getElementsByClassName('filter-section-on-delete-page');

    if (chose === "" || chose === 'allStudents' || chose === 'allCourses' || chose === 'allHomeTasks'
        || chose === 'allGrades' || chose === 'allLecturers') {
        elements[0].style.display = 'none';
    }
    else {
        elements[0].style.display = 'inline-block';

        switch (chose) {
            case 'studentWithDefiniteID':
                elements[0].children[0].innerHTML = 'Student ID';
                break;
            case 'courseWithDefiniteID':
                elements[0].children[0].innerHTML = 'Course ID';
                break;
            case 'lecturerWithDefiniteID':
                elements[0].children[0].innerHTML = 'Lecturer ID';
                break;
            case 'hometaskWithDefiniteID':
                elements[0].children[0].innerHTML = 'Hometask ID';
                break;
            case 'gradeWithDefiniteID':
                elements[0].children[0].innerHTML = 'Grade ID';
        }
    }

    if (isClearingInputRequired) {
        clearInputFieldsOnPage(elements);
    }
}

function inputFieldsTogglingOnConnectOrDisconnectPage(isClearingInputRequired) {
    let selectTag = document.getElementById('SelectedInfoType');

    let chose = selectTag.options[selectTag.selectedIndex].value;


    let elements = document.getElementsByClassName('ConnectOrDisconnectPageInputFields');

    if (chose === "" || chose === 'allLecturersFromAllCourses' || chose === 'allStudentsFromAllCourses') {
        for (let i = 0; i < elements.length; i++) {
            elements[i].style.display = 'none';
        }
    }
    else {
        for (let i = 0; i < elements.length; i++) {
            elements[i].style.display = 'inline-block'; 
        }

        if (chose === 'studentToCourse' || chose === 'studentFromCourse')
            elements[0].children[0].innerHTML = 'Student ID';
        else 
            elements[0].children[0].innerHTML = 'Lecturer ID';
    }

    if (isClearingInputRequired) {
        clearInputFieldsOnPage(elements);
    }
}
