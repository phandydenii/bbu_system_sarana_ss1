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
        info: false,
        ajax: {
            url: `/student/get-students-other-university`,
            type: "POST",
            error: function (xhr, status, error) {
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
        ],
        drawCallback: function () {
            const info = this.api().page.info();
            $("#studentCountText").text(
                `Showing ${info.start + 1} to ${info.end} of ${info.recordsDisplay}`
            );
        },
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

    if ($.fn.DataTable.isDataTable("#tblOtherUniversityScore")) {
        tblOtherUniversityScore.DataTable().clear().destroy();
    }

    tblOtherUniversityScore.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,

        paging: true,
        pageLength: 10,
        lengthChange: true,
        searching: true,
        ordering: true,
        info: true,

        ajax: {
            url: "/scores/GetExternalScore/" + studentId,
            type: "POST",
            dataSrc: function (json) {
                console.log("External score response:", json);
                return json.data || [];
            },
            error: function (xhr) {
                console.log(xhr.responseText);
                ShowToastError("Cannot load external score.");
            }
        },

        columns: [
            { data: "externalScoreId", visible: false },
            { data: "termNo" },
            { data: "courseName" },
            { data: "total" },
            { data: "grade" },
            {
                data: null,
                orderable: false,
                searchable: false,
                className: "text-center",
                render: function () {
                    return `
                        <button type="button" class="btn btn-warning btn-sm btnEditExternalScore">
                            <i class="fa-solid fa-pen-to-square"></i>
                        </button>
                    `;
                }
            }
        ]
    });
}

$(document).on("click", "#tblOtherUniversityScore .btnEditExternalScore", function (e) {
    e.preventDefault();
    e.stopPropagation();

    const table = $("#tblOtherUniversityScore").DataTable();

    let rowElement = $(this).closest("tr");

    if (rowElement.hasClass("child")) {
        rowElement = rowElement.prev();
    }

    const selectedRow = table.row(rowElement).data();

    if (!selectedRow) {
        ShowToastError("Cannot get selected score data.");
        return;
    }

    $("#ExternalScoreModal").find(".modal-title").text("Edit External Score");

    $("#externalScore_ExternalScoreId").val(selectedRow.externalScoreId);
    $("#externalScore_StudentId").val(selectedRow.studentId);
    $("#externalScore_TermNo").val(selectedRow.termNo);
    $("#externalScore_CourseCode").val(selectedRow.courseCode);
    $("#externalScore_CourseName").val(selectedRow.courseName);
    $("#externalScore_CourseNameInKhmer").val(selectedRow.courseNameInKhmer);
    $("#externalScore_Credit").val(selectedRow.credit);
    $("#externalScore_Total").val(selectedRow.total);
    $("#externalScore_Grade").val(selectedRow.grade);
    $("#externalScore_YearStart").val(selectedRow.yearStart);
    $("#externalScore_YearEnd").val(selectedRow.yearEnd);

    $("#ExternalScoreModal").modal("show");
});

$(document).on("click", "#btnCloseExternalScore", function (e) {
    e.preventDefault();

    const formElement = $("#externalScoreForm")[0];

    if (formElement) {
        formElement.reset();
    }
    $("#ExternalScoreModal").modal("hide");
});

$(document).on("submit", "#externalScoreForm", function (e) {
    e.preventDefault();
    const formData = $("#externalScoreForm").serialize();
    $.ajax({
        url: "/scores/save-external-score",
        type: "POST",
        data: formData,
        success: function (response) {
            const code = response.status?.code;
            const message = response.status?.message;

            if (code === "200" || code === 200) {
                ShowToastSuccess(message || "Updated successfully!");

                $("#ExternalScoreModal").modal("hide");

                const studentId = $("#externalScore_StudentId").val();
                GetOtherUniversityScore(studentId);
            } else {
                ShowToastError(message || "Update failed.");
            }
        },
        error: function (xhr) {
            ShowToastError("Server error.");
        }
    });
});

//====2-StudentReExam
function fetchStudentReExam() {
    if ($.fn.DataTable.isDataTable("#tblStudentReExam")) {
        tblStudentReExam.DataTable().clear().destroy();
    }
    tblStudentReExam.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        paging: true,
        pageLength: 10,
        lengthChange: true,
        pagingType: "simple_numbers",
        searching: true,
        ordering: true,
        info: false,

        ajax: {
            url: "/student/get-all-student-re-exam-payment",
            type: "POST",
            error: function (xhr) {
                ShowToastError("Cannot load re-exam students.");
            }
        },

        columns: [
            {data: "studentId"},
            {data: "studentName"},
            {data: "sex"},
            {
                data: "dateOfBirth",
                render: function (data) {
                    return formatDate(data);
                }
            }
        ],

        drawCallback: function () {
            const info = this.api().page.info();

            if (info.recordsDisplay === 0) {
                $("#studentReExamCountText").text("Showing 0 to 0 of 0");
            } else {
                $("#studentReExamCountText").text(
                    `Showing ${info.start + 1} to ${info.end} of ${info.recordsDisplay}`
                );
            }

            this.api().columns.adjust();
        },

        initComplete: function () {
            this.api().columns.adjust();
        }
    });
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

tblInvoice.on("click", "tbody tr", function () {
    const table = tblInvoice.DataTable();
    const data = table.row(this).data();
    if (!data) return;
    tblInvoice.find("tbody tr").removeClass("highlight");
    $(this).addClass("highlight");
    // Set payment id to hidden input for save
    $("#reExamPaymentDetail_StudentReexamPaymentId").val(data.studentReExamPaymentId);
    // Load detail table
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
                orderable: false,
                searchable: false,
                className: "text-center",
                render: function () {
                    return `
                    <button type="button" class="btn btn-warning btn-sm btnEditReExamPaymentDetail">
                        <i class="fas fa-edit"></i>
                    </button>
                    <button type="button" class="btn btn-danger btn-sm btnDeleteReExamPaymentDetail">
                        <i class="fa-solid fa-trash"></i>
                    </button>
                `;
                }
            }
        ]
    });
    // show edit modal
    $(document).on("click", "#tblReExamPaymentDetail .btnEditReExamPaymentDetail", function () {
        const table = $("#tblReExamPaymentDetail").DataTable();
        let rowElement = $(this).closest("tr");
        if (rowElement.hasClass("child")) {
            rowElement = rowElement.prev();
        }
        const selectedRow = table.row(rowElement).data();
        if (!selectedRow) {
            ShowToastError("Cannot get selected data.");
            return;
        }
        $("#editReExamPaymentDetail_CourseId").html($("#reExamPaymentDetail_CourseId").html());
        $("#editReExamPaymentDetail_StudentReexamPaymentDetailId")
            .val(selectedRow.studentReexamPaymentDetailId);
        $("#editReExamPaymentDetail_StudentReexamPaymentId")
            .val(selectedRow.studentReexamPaymentId);
        $("#editReExamPaymentDetail_CourseId")
            .val(selectedRow.courseId)
            .trigger("change");
        $("#editReExamPaymentDetail_TermNo").val(selectedRow.termNo);
        $("#editReExamPaymentDetail_Time").val(selectedRow.time);
        $("#ReExamPaymentDetailModal").modal("show");
    });
    // close edit modal
    $(document).on("click", "#btnCloseEditReExamPaymentDetail", function (e) {
        e.preventDefault();

        $("#editReExamPaymentDetailForm")[0].reset();
        $("#ReExamPaymentDetailModal").modal("hide");
    });
    // update
    $(document).on("submit", "#editReExamPaymentDetailForm", function (e) {
        e.preventDefault();

        const formData = $("#editReExamPaymentDetailForm").serialize();
        const paymentId = $("#editReExamPaymentDetail_StudentReexamPaymentId").val();

        $.ajax({
            url: "/payment/save-re-exam-payment-detail",
            type: "POST",
            data: formData,
            success: function (response) {
                const code = response.status?.code;
                const message = response.status?.message;

                if (code === "200" || code === 200) {
                    ShowToastSuccess(message || "Updated successfully!");

                    $("#ReExamPaymentDetailModal").modal("hide");
                    GetReExamPaymentDetail(paymentId);
                } else {
                    ShowToastError(message || "Update failed.");
                }
            },
            error: function (xhr) {
                ShowToastError("Server error.");
            }
        });
    });
}

//delete
$(document)
    .off("click", "#tblReExamPaymentDetail .btnDeleteReExamPaymentDetail")
    .on("click", "#tblReExamPaymentDetail .btnDeleteReExamPaymentDetail", function (e) {
        e.preventDefault();
        e.stopPropagation();
        const table = $("#tblReExamPaymentDetail").DataTable();
        let rowElement = $(this).closest("tr");
        if (rowElement.hasClass("child")) {
            rowElement = rowElement.prev();
        }

        const selectedRow = table.row(rowElement).data();

        if (!selectedRow) {
            ShowToastError("Cannot get selected data.");
            return;
        }


        const id = selectedRow.studentReexamPaymentDetailId;

        if (!id) {
            ShowToastError("Cannot get detail ID.");
            return;
        }

        if (!confirm("Are you sure you want to delete this subject?")) {
            return;
        }

        $.ajax({
            url: "/payment/delete-re-exam-payment-detail/" + id,
            type: "DELETE",
            success: function (response) {
                const code = response.status?.code;
                const message = response.status?.message;

                if (code === "200" || code === 200) {
                    ShowToastSuccess(message || "Deleted successfully!");

                    const paymentId = $("#reExamPaymentDetail_StudentReexamPaymentId").val();
                    GetReExamPaymentDetail(paymentId);
                } else {
                    ShowToastError(message || "Delete failed.");
                }
            },
            error: function (xhr) {
                ShowToastError("Delete failed.");
            }
        });
    });
// load select Course full name
$(document).ready(function () {
    GetReExamCourses();
});

function GetReExamCourses() {
    const cboCourse = $("#reExamPaymentDetail_CourseId");

    cboCourse.empty();
    cboCourse.append(`<option value="">-- Select Subject --</option>`);

    $.ajax({
        url: "/payment/get-courses",
        type: "POST",
        success: function (response) {

            const courses = response.data || [];

            $.each(courses, function (index, item) {
                cboCourse.append(`
                        <option value="${item.courseId}">
                            ${item.courseFullName}
                        </option>
                    `);
            });

            cboCourse.trigger("change");
        },
        error: function (xhr) {
            ShowToastError("Cannot load subject.");
        }
    });
}

// save ReExam Subject
$(document).on("submit", "#reExamPaymentDetailForm", function (e) {
    e.preventDefault();

    const paymentId = $("#reExamPaymentDetail_StudentReexamPaymentId").val();

    if (!paymentId || paymentId === "0") {
        ShowToastError("Please select invoice first.");
        return;
    }

    const formData = $("#reExamPaymentDetailForm").serialize();

    $.ajax({
        url: "/payment/save-re-exam-payment-detail",
        type: "POST",
        data: formData,
        success: function (response) {
            const code = response.status?.code;
            const message = response.status?.message;

            if (code === "200" || code === 200) {
                ShowToastSuccess(message || "Saved successfully!");

                GetReExamPaymentDetail(paymentId);

                $("#reExamPaymentDetail_CourseId").val("").trigger("change");
                $("#reExamPaymentDetail_TermNo").val(1);
                $("#reExamPaymentDetail_Time").val(1);
            } else {
                ShowToastError(message || "Save failed.");
            }
        },
        error: function (xhr) {
            const message =
                xhr.responseJSON?.status?.message ||
                xhr.responseJSON?.message ||
                "Server error.";

            ShowToastError(message);
        }
    });
});

//======3-StudentReExamHistory
function fetchStudentReExamHistory() {
    if ($.fn.DataTable.isDataTable("#tblExamScoreHistory")) {
        tblExamScoreHistory.DataTable().clear().destroy();
    }

    tblExamScoreHistory.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,

        paging: true,
        pageLength: 10,
        lengthChange: true,
        searching: true,
        ordering: true,
        info: false,

        ajax: {
            url: "/student/get-all-student-re-exam-history",
            type: "POST",
            error: function (xhr) {
            }
        },

        columns: [
            {data: "studentId"},
            {data: "studentName"},
            {data: "sex"},
            {
                data: "dateOfBirth",
                render: function (data) {
                    return formatDate(data);
                }
            }
        ],

        drawCallback: function () {
            const info = this.api().page.info();
            if (info.recordsDisplay === 0) {
                $("#studentReExamHistoryCountText").text("Showing 0 to 0 of 0");
            } else {
                $("#studentReExamHistoryCountText").text(
                    `Showing ${info.start + 1} to ${info.end} of ${info.recordsDisplay}`
                );
            }
        }
    });
}

tblExamScoreHistory.on('click', 'tbody tr', function () {
    tblExamScoreHistory.find('tbody tr').removeClass('highlight');
    $(this).addClass('highlight');
    const table = tblExamScoreHistory.DataTable();
    const data = table.row(this).data();
    GetFailScore(data.studentId);
});

//const tblStudentMark = $("#tblStudentMark");
function GetFailScore(studentId) {
    if (!studentId) return;

    if ($.fn.DataTable.isDataTable("#tblStudentMark")) {
        $("#tblStudentMark").DataTable().clear().destroy();
    }

    $("#tblStudentMark").DataTable({
        processing: true,
        serverSide: true,

        responsive: false,
        autoWidth: false,
        scrollX: true,
        scrollCollapse: true,

        paging: true,
        searching: true,
        ordering: true,
        info: true,

        ajax: {
            url: "/scores/GetComplementFailedCourseScores/" + studentId,
            type: "POST",
            dataSrc: function (json) {
                return json.data || [];
            },
            error: function (xhr) {
                ShowToastError("Cannot load student mark.");
            }
        },

        columns: [
            {data: "termNo"},
            {data: "courseFullName"},
            {
                data: "midTermScore",
                render: function (data) {
                    if (data === null || data === undefined) return "0.00";
                    return Number(data).toFixed(2);
                }
            },
            {
                data: "finalScore",
                render: function (data) {
                    if (data === null || data === undefined) return "0.00";
                    return Number(data).toFixed(2);
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    const totalScore =
                        Number(row.midTermScore || 0) +
                        Number(row.finalScore || 0);

                    return totalScore.toFixed(2);
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    const totalScore =
                        Number(row.midTermScore || 0) +
                        Number(row.finalScore || 0);

                    if (totalScore >= 60) {
                        return `<button type="button" class="btn btn-success btn-sm">Pass</button>`;
                    }

                    return `<button type="button" class="btn btn-danger btn-sm">Fail</button>`;
                }
            },
            {
                data: null,
                orderable: false,
                searchable: false,
                className: "text-center",
                render: function () {
                    return `
                            <button type="button" class="btn btn-warning btn-sm btnEditStudentMark">
                                <i class="fa-solid fa-pen-to-square"></i>
                            </button>
                        `;
                }
            }
        ],

        initComplete: function () {
            this.api().columns.adjust();
        },

        drawCallback: function () {
            this.api().columns.adjust();
        }
    });
}

$(document).ready(function () {

    // 1. ROW SELECTION & HISTORY HIGHLIGHT HANDLER
    $("#tblStudentMark")
        .off("click", "tbody tr")
        .on("click", "tbody tr", function () {
            const table = $("#tblStudentMark").DataTable();
            const data = table.row(this).data();

            if (!data) return;

            $("#tblStudentMark tbody tr").removeClass("highlight");
            $(this).addClass("highlight");
            

            if (typeof GetScoreHistory === "function") {
                GetScoreHistory(data.studentId, data.courseId);
            }
        });

    // 2. EDIT BUTTON CLICK HANDLER (Consolidated with data fixes)
    $(document)
        .off("click", "#tblStudentMark .btnEditStudentMark")
        .on("click", "#tblStudentMark .btnEditStudentMark", function (e) {
            e.preventDefault();
            e.stopPropagation();

            const table = $("#tblStudentMark").DataTable();
            let rowElement = $(this).closest("tr");

            // Handle responsive layout child rows if necessary
            if (rowElement.hasClass("child")) {
                rowElement = rowElement.prev();
            }

            const selectedRow = table.row(rowElement).data();

            if (!selectedRow) {
                ShowToastError("Cannot get selected mark data.");
                return;
            }
            

            // FIX: Use 'scoreId' directly from your DataTables dataset object
            const targetScoreId = selectedRow.scoreId;

            // Safely map values into the form inputs
            $("#studentMark_ComplementFailedCourseScoreId").val(targetScoreId);
            $("#studentMark_StudentId").val(selectedRow.studentId);
            $("#studentMark_CourseId").val(selectedRow.courseId);
            $("#studentMark_TermNo").val(selectedRow.termNo);

            // Map presentation items
            $("#studentMark_CourseName").val(selectedRow.courseFullName);
            $("#studentMark_TermNoDisplay").val(selectedRow.termNo);
            $("#studentMark_MidTermScore").val(selectedRow.midTermScore);
            $("#studentMark_FinalScore").val(selectedRow.finalScore);
            

            $("#StudentMarkModal").modal("show");
        });

    // 3. MODAL CLOSE & RESET HANDLER
    $(document)
        .off("click", "#btnCloseStudentMark")
        .on("click", "#btnCloseStudentMark", function (e) {
            e.preventDefault();
            e.stopPropagation();

            const formElement = $("#studentMarkForm")[0];
            if (formElement) {
                formElement.reset();
            }

            $("#StudentMarkModal").modal("hide");
        });

    // 4. AJAX FORM SUBMISSION HANDLER
    $(document)
        .off("submit", "#studentMarkForm")
        .on("submit", "#studentMarkForm", function (e) {
            e.preventDefault();
            e.stopPropagation();

            const form = $(this);

            // FIX: Selects submit button via attribute because it sits outside the <form> tags
            const btn = $("button[form='studentMarkForm']");

            if (btn.prop("disabled")) return false;

            btn.prop("disabled", true);

            $.ajax({
                url: "/scores/save-complement-failed-course-score",
                type: "POST",
                data: form.serialize(),
                success: function (response) {
                    const code = response.status?.code;
                    const message = response.status?.message;

                    if (code === "200" || code === 200) {
                        ShowToastSuccess(message || "Updated successfully!");

                        $("#StudentMarkModal").modal("hide");

                        const studentId = $("#studentMark_StudentId").val();
                        const courseId = $("#studentMark_CourseId").val();

                        // Call external update routines if they exist
                        if (typeof GetFailScore === "function") GetFailScore(studentId);
                        if (typeof GetScoreHistory === "function") GetScoreHistory(studentId, courseId);
                    } else {
                        ShowToastError(message || "Update failed.");
                    }
                },
                error: function (xhr) {
                    ShowToastError("Server error.");
                },
                complete: function () {
                    btn.prop("disabled", false);
                }
            });

            return false;
        });
});

const tblShoreHistory = $("#tblShoreHistory");

function GetScoreHistory(studentId, courseId) {
    if (!studentId || !courseId) return;

    if ($.fn.DataTable.isDataTable("#tblShoreHistory")) {
        tblShoreHistory.DataTable().clear().destroy();
    }

    tblShoreHistory.DataTable({
        processing: true,
        serverSide: true,

        responsive: false,
        autoWidth: false,
        scrollX: true,
        scrollCollapse: true,

        paging: false,
        searching: false,
        lengthChange: false,
        info: false,
        ordering: false,

        ajax: {
            url: "/scores/get-score-history/" + studentId + "/" + courseId,
            type: "POST",
            data: function (d) {
                d.start = 0;
                d.length = 1000;
                d["search[value]"] = "";
            },
            dataSrc: function (json) {
                return json.data || [];
            },
            error: function (xhr) {
                ShowToastError("Cannot load score history.");
            }
        },

        columns: [
            {data: "termNo"},
            {data: "courseFullName"},
            {
                data: "midTermScore",
                render: function (data) {
                    return Number(data || 0).toFixed(2);
                }
            },
            {
                data: "finalScore",
                render: function (data) {
                    return Number(data || 0).toFixed(2);
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    const total =
                        Number(row.midTermScore || 0) +
                        Number(row.finalScore || 0);

                    return total.toFixed(2);
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    const total =
                        Number(row.midTermScore || 0) +
                        Number(row.finalScore || 0);

                    return total >= 60
                        ? `<button type="button" class="btn btn-success btn-sm">Pass</button>`
                        : `<button type="button" class="btn btn-danger btn-sm">Fail</button>`;
                }
            },
            {
                data: "time",
                render: function (data) {
                    return data || "";
                }
            },
            {
                data: null,
                orderable: false,
                searchable: false,
                className: "text-center",
                render: function () {
                    return `
                    <button type="button" class="btn btn-warning btn-sm btnEditScoreHistory">
                        <i class="fa-solid fa-pen-to-square"></i>
                    </button>
                `;
                }
            }
        ],

        initComplete: function () {
            this.api().columns.adjust();
        },

        drawCallback: function () {
            this.api().columns.adjust();
        }
    });
}

$(document)
    .off("click", "#tblShoreHistory .btnEditScoreHistory")
    .on("click", "#tblShoreHistory .btnEditScoreHistory", function (e) {
        e.preventDefault();
        e.stopPropagation();

        const table = $("#tblShoreHistory").DataTable();

        let rowElement = $(this).closest("tr");

        if (rowElement.hasClass("child")) {
            rowElement = rowElement.prev();
        }

        const selectedRow = table.row(rowElement).data();

        if (!selectedRow) {
            ShowToastError("Cannot get selected score history data.");
            return;
        }

        const scoreHistoryId =
            selectedRow.scoreHistoryId ||
            selectedRow.ScoreHistoryId ||
            0;

        if (Number(scoreHistoryId) <= 0) {
            ShowToastError("Cannot get Score History ID.");
            return;
        }

        $("#scoreHistory_ScoreHistoryId").val(scoreHistoryId);
        $("#scoreHistory_StudentId").val(selectedRow.studentId);
        $("#scoreHistory_CourseId").val(selectedRow.courseId);
        $("#scoreHistory_TermNo").val(selectedRow.termNo);
        $("#scoreHistory_Time").val(selectedRow.time || 1);

        $("#scoreHistory_CourseName").val(selectedRow.courseFullName || selectedRow.courseId);
        $("#scoreHistory_TermNoDisplay").val(selectedRow.termNo);

        $("#scoreHistory_MidTermScore").val(selectedRow.midTermScore);
        $("#scoreHistory_FinalScore").val(selectedRow.finalScore);

        $("#ScoreHistoryModal").modal("show");
    });

$(document)
    .off("submit", "#scoreHistoryForm")
    .on("submit", "#scoreHistoryForm", function (e) {
        e.preventDefault();
        e.stopPropagation();

        const form = $(this);
        const btn = $("button[form='scoreHistoryForm']");

        if (btn.prop("disabled")) return false;

        btn.prop("disabled", true);

        $.ajax({
            url: "/scores/save/score-history",
            type: "POST",
            data: form.serialize(),

            success: function (response) {
                const code = response.status?.code;
                const message = response.status?.message;

                if (code === "200" || code === 200) {
                    ShowToastSuccess(message || "Score history updated successfully!");

                    const studentId = $("#scoreHistory_StudentId").val();
                    const courseId = $("#scoreHistory_CourseId").val();

                    $("#ScoreHistoryModal").modal("hide");

                    GetScoreHistory(studentId, courseId);
                } else {
                    ShowToastError(message || "Update failed.");
                }
            },

            error: function (xhr) {

                const message =
                    xhr.responseJSON?.status?.message ||
                    xhr.responseJSON?.message ||
                    "Server error.";

                ShowToastError(message);
            },

            complete: function () {
                btn.prop("disabled", false);
            }
        });

        return false;
    });

$(document)
    .off("click", "#btnCloseScoreHistory")
    .on("click", "#btnCloseScoreHistory", function (e) {
        e.preventDefault();

        const form = $("#scoreHistoryForm")[0];

        if (form) {
            form.reset();
        }

        $("#ScoreHistoryModal").modal("hide");
    });

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

    if ($.fn.DataTable.isDataTable("#tblComplementationScore")) {
        tblComplementationScore.DataTable().clear().destroy();
    }

    if ($.fn.DataTable.isDataTable("#tblComplementationStudent")) {
        tblComplementationStudent.DataTable().clear().destroy();
    }

    tblComplementationStudent.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,

        paging: true,
        pageLength: 10,
        lengthChange: true,
        searching: true,
        ordering: true,
        info: false,

        ajax: {
            url: url,
            type: "POST",
            error: function (xhr) {
            }
        },

        columns: [
            {data: "studentId"},
            {data: "studentName"},
            {data: "sex"},
            {
                data: "dateOfBirth",
                render: function (data) {
                    return formatDate(data);
                }
            }
        ],

        drawCallback: function () {
            const info = this.api().page.info();

            if (info.recordsDisplay === 0) {
                $("#studentComplementationCountText").text("Showing 0 to 0 of 0");
            } else {
                $("#studentComplementationCountText").text(
                    `Showing ${info.start + 1} to ${info.end} of ${info.recordsDisplay}`
                );
            }
        }
    });
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
        ],
    });
}