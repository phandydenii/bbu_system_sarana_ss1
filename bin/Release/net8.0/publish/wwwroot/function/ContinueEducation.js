const tblStudentOtherUniversity = $("#tblStudentOtherUniversity");
const tblStudentReExam = $("#tblStudentReExam");
const tblOtherUniversityScore = $("#tblOtherUniversityScore");
const tblInvoice = $("#tblInvoice");
const tblReExamPaymentDetail = $("#tblReExamPaymentDetail");
const tblExamScoreHistory = $("#tblExamScoreHistory");
$(document).ready(function () {
    fetchStudentOtherUniversity();
    fetchStudentReExam();
    fetchStudentReExamHistory();
    fetchStudentComplementation(1);
});

//=====1-StudentOtherUniversity
function fetchStudentOtherUniversity() {
    tblStudentOtherUniversity.DataTable().clear().destroy();
    tblStudentOtherUniversity.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: `/student/get-students-other-university`,
            type: "POST",
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        },
        columns: [
            {data: "studentId"},
            {data: "studentName"},
            {data: "sex"},
            {
                data: "dateOfBirth",
                render: function (data, type, row) {
                    return `${formatDate(data)}`
                }
            }
        ]
    })
}

tblStudentOtherUniversity.on('click', 'tbody tr', function () {
    tblStudentOtherUniversity.find('tbody tr').removeClass('highlight');
    $(this).addClass('highlight');
    const table = tblStudentOtherUniversity.DataTable();
    const data = table.row(this).data();
    GetOtherUniversityScore(data.studentId);
});

function GetOtherUniversityScore(studentId) {
    if (!studentId) return;
    tblOtherUniversityScore.DataTable().clear().destroy();
    tblOtherUniversityScore.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: `/scores/GetExternalScore/${studentId}`,
            type: "POST",
            dataSrc: function (json) {
                return json.data;
            }
        },
        columns: [
            {data: "externalScoreId", visible: false},
            {data: "termNo"},
            {data: "courseName"},
            {data: "total"},
            {
                data: null,
                render: function (data, type, row) {
                    const totalScore = (row.midTermScore || 0) + (row.finalScore || 0);
                    if (totalScore >= 60) {
                        return `<button class="btn btn-success btn-sm">Pass</button>`;
                    } else {
                        return `<button class="btn btn-danger btn-sm">Fail</button>`;
                    }
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    return `<button class="btn btn-warning btn-sm" id="btnEdit"><i class="fa-solid fa-pen-to-square"></i></button>`;
                }
            }
        ]
    });
}

//====2-StudentReExam
function fetchStudentReExam() {
    tblStudentReExam.DataTable().clear().destroy();
    tblStudentReExam.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: `/student/get-all-student-re-exam-payment`,
            type: "POST",
        },
        columns: [
            {data: "studentId"},
            {data: "studentName"},
            {data: "sex"},
            {
                data: "dateOfBirth",
                render: function (data, type, row) {
                    return `${formatDate(data)}`
                }
            }
        ]
    })
}


tblStudentReExam.on('click', 'tbody tr', function () {
    tblStudentReExam.find('tbody tr').removeClass('highlight');
    $(this).addClass('highlight');
    const table = tblStudentReExam.DataTable();
    const data = table.row(this).data();
    GetReExamPayment(data.studentId);
});

function GetReExamPayment(studentId) {
    if (!studentId) return;
    tblInvoice.DataTable().clear().destroy();
    tblInvoice.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: `/payment/get-student-re-exam-payments/${studentId}`,
            type: "POST",
            dataSrc: function (json) {
                return json.data;
            }
        },
        columns: [
            {data: "invoiceNo"},
            {
                data: "invoiceDate",
                render: function (data, type, row) {
                    return `${formatDate(data)}`
                }
            },
            {data: "paid"},
            {data: "note"}
        ]
    });
}

tblInvoice.on('click', 'tbody tr', function () {
    tblInvoice.find('tbody tr').removeClass('highlight');
    $(this).addClass('highlight');
    const table = tblInvoice.DataTable();
    const data = table.row(this).data();
    GetReExamPaymentDetail(data.studentReExamPaymentId);
});

function GetReExamPaymentDetail(studentReExamPaymentId) {
    if (!studentReExamPaymentId) return;
    tblReExamPaymentDetail.DataTable().clear().destroy();
    tblReExamPaymentDetail.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        
        ajax: {
            url: `/payment/get-student-re-exam-payments-detail/${studentReExamPaymentId}`,
            type: "POST",
            dataSrc: function (json) {
                return json.data;
            }
        },
        columns: [
            {data: "courseFullName"},
            {data: "termNo"},
            {data: "time"},
            {
                data: null,
                render: function (data, type, row) {
                    return ``
                }
            }
        ]
    });
}

//======3-StudentReExamHistory
function fetchStudentReExamHistory() {
    tblExamScoreHistory.DataTable().clear().destroy();
    tblExamScoreHistory.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: `/student/get-all-student-re-exam-history`,
            type: "POST",
        },
        columns: [
            {data: "studentId"},
            {data: "studentName"},
            {data: "sex"},
            {
                data: "dateOfBirth",
                render: function (data, type, row) {
                    return `${formatDate(data)}`
                }
            }
        ]
    })
}
tblExamScoreHistory.on('click', 'tbody tr', function () {
    tblExamScoreHistory.find('tbody tr').removeClass('highlight');
    $(this).addClass('highlight');
    const table = tblExamScoreHistory.DataTable();
    const data = table.row(this).data();
    GetFailScore(data.studentId);
});
const tblStudentMark = $("#tblStudentMark");
function GetFailScore(studentId) {
    if (!studentId) return;
    tblStudentMark.DataTable().clear().destroy();
    tblStudentMark.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        
        ajax: {
            url: `/scores/get-fail-score/${studentId}`,
            type: "POST",
            dataSrc: function (json) {
                return json.data;
            }
        },
        columns: [
            {data: "termNo"},
            {data: "courseFullName"},
            {data: "midTermScore"},
            {data: "finalScore"},
            {data: "midTermScore"},
            {data: "midTermScore"},
            {
                data: null,
                render: function (data, type, row) {
                    return ``
                }
            }
        ]
    });
}

tblStudentMark.on('click', 'tbody tr', function () {
    tblStudentMark.find('tbody tr').removeClass('highlight');
    $(this).addClass('highlight');
    const table = tblStudentMark.DataTable();
    const data = table.row(this).data();
    GetScoreHistory(data.studentId, data.courseId);
});
const tblShoreHistory = $("#tblShoreHistory");
function GetScoreHistory(studentId,courseId) {
     if (!studentId || !courseId) return;
    tblShoreHistory.DataTable().clear().destroy();
    tblShoreHistory.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        paging: false,        // ❌ disable paging
        searching: false,     // ❌ disable search box
        lengthChange: false,  // ❌ disable page size dropdown
        info: false,          // ❌ disable "Showing X of Y"
        
        ajax: {
            url: `/scores/get-score-history/${studentId}/${courseId}`,
            type: "POST",
            data: {
                draw: 1,
                start: 0,
                length: 0,
                'search[value]': ''
            },
            dataSrc: function (json) {
                return json.data;
            }
        },
        columns: [
            {data: "termNo"},
            {data: "courseFullName"},
            {data: "midTermScore"},
            {data: "finalScore"},
            {data: "midTermScore"},
            {data: "midTermScore"},
            {data: "midTermScore"},
            {
                data: null,
                render: function (data, type, row) {
                    return ``
                }
            }
        ]
    });
}

//=====4-ComplementationScore
const tblComplementationScore = $("#tblComplementationScore");
const tblComplementationStudent = $("#tblComplementationStudent");


$("#rangeButtons").on("click", "button", function (e) {
    // remove active style from all buttons
    $("#rangeButtons").find("button").removeClass("btn-primary").addClass("btn-outline-primary");
    // add active style to clicked button
    $(this).removeClass("btn-outline-primary").addClass("btn-primary");
    fetchStudentComplementation($(this).val());
});

function fetchStudentComplementation(id) {
    let url = "/student/GetAllComplementSemesterStudents";
    if (parseInt(id) === 2) {
        url = "/student/GetAllComplementOrientedCourseStudents";
    } else if (parseInt(id) === 3) {
        url = "/student/GetAllComplementFailedCourseStudents";
    }
    tblComplementationScore.DataTable().clear().destroy();
    tblComplementationStudent.DataTable().clear().destroy();
    tblComplementationStudent.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: url,
            type: "POST",
        },
        columns: [
            {data: "studentId"},
            {data: "studentName"},
            {data: "sex"},
            {
                data: "dateOfBirth",
                render: function (data, type, row) {
                    return `${formatDate(data)}`
                }
            }
        ]
    })
}

tblComplementationStudent.on('click', 'tbody tr', function () {
    tblComplementationStudent.find('tbody tr').removeClass('highlight');
    $(this).addClass('highlight');
    const table = tblComplementationStudent.DataTable();
    const data = table.row(this).data();
    
    const buttons = document.querySelectorAll('#rangeButtons .btn');
    buttons.forEach(button => {
        if (button.classList.contains('btn-primary')) {
            GetComplementScore(button.value, data.studentId);
        }
    });
});

function GetComplementScore(id, studentId) {
    // console.log(id + "-" + studentId);
    let url = "/scores/GetComplementSemesterScores";
    if (parseInt(id) === 2) {
        url = "/scores/GetComplementOrientedCourseScores";
    } else if (parseInt(id) === 3) {
        url = "/scores/GetComplementFailedCourseScores";
    }

    tblComplementationScore.DataTable().clear().destroy();
    tblComplementationScore.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: `${url}/${studentId}`,
            type: "POST",
            dataSrc: function (json) {
                return json.data;
            }
        },
        columns: [ 
            {data: "termNo"},
            {data: "courseFullName"},
            {
                data: "midTermScore",
            },
            {
                data: "finalScore",
            },
            {
                data: null,
                render: function (data, type, row) {
                    const totalScore = (row.midTermScore || 0) + (row.finalScore || 0);
                    if (totalScore >= 60) {
                        return `<button class="btn btn-success btn-sm">Pass</button>`;
                    } else {
                        return `<button class="btn btn-danger btn-sm">Fail</button>`;
                    }
                }
            }
        ]
    });
}