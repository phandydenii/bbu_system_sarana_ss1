const frmStudent = $("#frmStudent");
const actionButtons = $("#actionButtons");
const btnEdit = $("#btnEdit");
let currentStudentInfo = {
    studentId: "",
    termNo: "",
    stageId: "",
    fieldId: "",
    groupId: "",
    promotionId:"",
    todayDate: ""
};
let isStudentDropdownDoneLoaded = false;
let needReloadStudentList = false;
function resetStudentDetailModal() {
    currentStudentInfo = {
        studentId: "",
        termNo: "",
        stageId: "",
        fieldId: "",
        groupId: "",
        promotionId: "",
        todayDate: ""
    };
    // Reset form
    if ($("#frmStudent").length){$("#frmStudent")[0].reset();}
    // Clear text inputs outside form
    $("#txtStudentId").val("");
    $("#txtDegree").val("");
    $("#txtSchool").val("");
    $("#txtPromotion").val("");
    $("#txtStage").val("");
    $("#txtTerm").val("");
    $("#txtStudyTime").val("");
    $("#txtRoom").val("");
    $("#txtGroup").val("");
    $("#txtFieldGroup").val("");
    // Show all tab headers again
    $("#student-history-tabs .nav-item").removeAttr("style");
    // Reset active tab state
    $(".student-history-pane").removeAttr("style");
    $("#student-history-tabs .nav-link").removeClass("active"); 
    $(".student-history-pane").removeClass("show active");
    // Destroy all DataTables
    const tables = [
        "#tblStudentScholarship",
        "#tblStudentIssueLetter",
        "#tblStudentCertificate",
        "#tblBranchHistory",
        "#tblSuppress",
        "#tblStudentPayment",
        "#tblSuspend",
        "#tblStudentGroup",
        "#tblStudentExtend",
        "#tblComplement",
        "#tblQuit"
    ];
    tables.forEach(function (tableId) {
        if ($.fn.DataTable.isDataTable(tableId)) {
            $(tableId).DataTable().clear().destroy();
        }
        $(tableId + " tbody").empty();
    });
    // Reset buttons
    btnEdit.data("editing", false);
    btnEdit.html('<i class="fas fa-pen"></i>');
    actionButtons.find("button").not("#btnEdit").addClass("d-none");
    $("#frmStudent :input").prop("disabled", true);
}

async function BindData() {
    await BindSelectOptions("/Province/get-provinces", "student_PlaceOfBirthId", "provinceId", "provinceName");
    await BindSelectOptions("/Nationality/get-nationalities", "student_NationalityId", "nationalityId", "nationalityName");
    await BindSelectOptions("/Province/get-provinces", "student_FromProvinceId", "provinceId", "provinceName");
    await BindSelectOptions("/high-school/get-high-school", "student_FromHighSchoolNameInKhmer", "highSchoolNameInKhmer", "highSchoolNameInKhmer");
    await BindSelectOptions("/student-job/get-student-jobs", "student_JobId", "jobId", "jobName");
    await BindSelectOptions("/disability/get-disabilities", "student_DisabilityId", "disabilityId", "disabilityName");
    await BindSelectOptions("/race/get-races", "student_RaceId", "raceId", "raceName");
}
async function LoadStudentDetail(studId) {
    if (!studId) return;
    showLoading();
    $("#txtStudentId").val(studId); 
    if (!isStudentDropdownDoneLoaded) {
        await BindData();
        isStudentDropdownDoneLoaded = true;
    }
    await getStudent(studId);
    await Promise.allSettled([
        fetchStudentScholarship(studId),
        fetchStudentGroup(studId),
        fetchStudentExtend(studId),
        fetchStudentPayment(studId),
        fetchStudentBranchHistory(studId),
        fetchStudentIssueLetter(studId),
        fetchStudentCertificate(studId),
        fetchSuppress(studId),
        fetchSuspend(studId),
        fetchQuit(studId),
        fetchComplement(studId)
    ]); 
    $('#student-history-tabs a:visible:first').tab('show');
    $('#frmStudent :input').prop('disabled', true);
    hideLoading(1);
} 

btnEdit.on("click", function (e) {
    e.preventDefault();

    const isEditing = $(this).data("editing") === true;
    const status = $("#student_Status").val();

    if (isEditing) {
        $(this).data("editing", false);
        $(this).html('<i class="fas fa-pen"></i>');

        setActionButtonsByStudentStatus(status, false);

        $('#frmStudent :input').prop('disabled', true);
    } else {
        $(this).data("editing", true);
        $(this).html('<i class="fas fa-check"></i>');

        setActionButtonsByStudentStatus(status, true);

        $('#frmStudent :input').prop('disabled', false);
    }
});
$("#studentDetailModal").on("hidden.bs.modal", function () {
    resetStudentDetailModal();
    if (needReloadStudentList) {
        fetchStudent();
        needReloadStudentList = false;
    }
});
function setStudentStatus(status) {
    const studentStatus = $("#student_Status");
    const badgeClass = StudentStatusBadgeClasses[status] || StudentStatusDefaultBadgeClass;
    const bgClass = badgeClass.replace("badge-", "bg-");
    const allBgClasses = Object.values(StudentStatusBadgeClasses)
        .map(x => x.replace("badge-", "bg-"))
        .join(" ");
    const defaultBgClass = StudentStatusDefaultBadgeClass.replace("badge-", "bg-");
    studentStatus
        .removeClass(allBgClasses)
        .removeClass(defaultBgClass)
        .addClass(bgClass)
        .val(status || "");
}
function setActionButtonsByStudentStatus(status, isEditing = false) {
    actionButtons.find("button").addClass("d-none");
    $("#btnEdit").removeClass("d-none"); 
    if (!isEditing) return; 
    $("#btnUpdate").removeClass("d-none");

    switch (status) {
        case "CHANGE BRANCH":
            actionButtons.find("button").addClass("d-none");
            $("#btnEdit").removeClass("d-none");
            $("#btnUpdate").removeClass("d-none");
            return;
        case "QUIT":
            actionButtons.find("button").removeClass("d-none");
            $("#btnQuit").addClass("d-none"); 
            return;
        case "SUSPEND":
            actionButtons.find("button").removeClass("d-none");
            $("#btnSuspend").addClass("d-none"); 
            return; 
        case "SUPPRESS":
            actionButtons.find("button").removeClass("d-none");
            $("#btnSuppress").addClass("d-none");
            return; 
        default:
            actionButtons.find("button").removeClass("d-none");
            return;
    }
}

async function getStudent(student_id) {
    return $.ajax({
        url: "/student/academic/student-id/" + student_id,
        type: "GET",
        data: {student_id: student_id},
        success:async function (result) {
            const data = result.data;
            if (data) {
                const student = data["student"];
                const contactPerson = data["contactPerson"];
                const degree = data["degree"];
                const school = data["school"];
                const field = data["field"];
                const promotion = data["promotion"];
                const stage = data["stage"];
                const term = data["term"];
                const group = data["group"];
                const groupRoom = data["groupRoom"];
                const registry = data["registry"];
                const fieldGroup = data["fieldGroup"];
                currentStudentInfo = {
                    studentId: student.studentId,
                    termNo: term.termNo,
                    stageId: stage.stageId,
                    fieldId: field.fieldId,
                    groupId: group.groupId,
                    promotionId: promotion.promotionId,
                    todayDate: new Date().toISOString().split("T")[0]
                };
                $("#txtDegree").val(degree.degreeName);
                $("#txtField").val(field.fieldName);
                $("#txtGroup").val(group.groupName); 
                setStudentStatus(student.status);
                setActionButtonsByStudentStatus(student.status, false);
                $("#student_FieldId").val(student.fieldId).trigger("change");
                if(school.isFoundationSchool ===1){
                    $("#txtSchool").val(`${school.schoolName} (${registry.schoolName})`); 
                    $("#txtFieldGroup").val(`${fieldGroup.fieldName} (${field.fieldName}`);
                }else{
                    $("#txtSchool").val(school.schoolName);
                    $("#txtFieldGroup").val(fieldGroup.fieldName);
                }
                $("#txtPromotion").val(promotion.promotionNo);
                $("#txtStage").val(stage.stageNo);
                $("#txtTerm").val(term.termNo);
                $("#txtStudyTime").val(group.studyTime);
                $("#txtRoom").val(groupRoom.roomName); 
                $("#student_StudentId").val(student.studentId);
                $("#student_StudentName").val(`${student.studentName}`);
                $("#student_StudentNameInKhmer").val(`${student.studentNameInKhmer}`);
                $("#student_Sex").val(student.sex);
                $("#student_Phone").val(student.phone); 
                $("#student_Email").val(student.email).trigger("change");
                $("#student_Address").val(student.address).trigger("change");
                $("#student_AddressInKhmer").val(student.addressInKhmer).trigger("change");
                $("#student_Note").val(student.note);
                $("#student_CheckCompleteTerm").val(student.checkCompleteTerm);
                $("#student_CheckComplete").val(student.checkComplete);
                $("#student_CheckCompleteNote").val(student.checkCompleteNote);
                $("#student_DateOfBirth").val(formatDateForInput(student.dateOfBirth)).trigger("change");
                $("#student_MaritalStatus").val(student.maritalStatus);
                $("#student_HighSchoolGraduatedYear").val(student.highSchoolGraduatedYear);
                $("#student_FromHighSchoolNameInKhmer").val(student.fromHighSchoolNameInKhmer).trigger("change");
                $("#student_PlaceOfBirthId").val(student.placeOfBirthId).trigger("change");
                $("#student_NationalityId").val(student.nationalityId).trigger("change");
                $("#student_FromProvinceId").val(student.fromProvinceId).trigger("change");
                $("#student_RaceId").val(student.raceId).trigger("change");
                $("#student_JobId").val(student.jobId).trigger("change");
                $("#student_DisabilityId").val(student.disabilityId).trigger("change");
                $("#registry_RegistrationId").val(registry.registrationId);
                $("#registry_HighSchoolTableNo").val(registry.highSchoolTableNo).trigger("change");
                $("#registry_HighSchoolResult").val(registry.highSchoolResult).trigger("change");
                $("#contactPerson_ContactPersonName").val(contactPerson.contactPersonName).trigger("change");
                $("#contactPerson_Job").val(contactPerson.job).trigger("change");
                $("#contactPerson_Phone").val(contactPerson.phone).trigger("change");
                $("#contactPerson_Address").val(contactPerson.address).trigger("change");
                $("#student_FatherNameInKhmer").val(student.fatherNameInKhmer);
                $("#student_FatherOccupationInKhmer").val(student.fatherOccupationInKhmer).trigger("change");
                $("#student_MotherNameInKhmer").val(student.motherNameInKhmer).trigger("change");
                $("#student_MotherOccupationInKhmer").val(student.motherOccupationInKhmer).trigger("change");
                $("#student_IsContinuedStudent").prop("checked",student.isContinuedStudent===1);
                $("#student_AssociateToBachelor").prop("checked",student.associateToBachelor===1);
                $("#student_IsReceivePhoto").prop("checked",student.isPhotoReceived === 1);  
            } else {
                ShowToastError("Student not found.");
            }
        },
        error: function () {
            ShowToastError("Error fetching student data.");
        }
    });
}

// region "Sub Modal of Student Detail Modal handling"
// =====1. suppress
$(document).on("click", "#btnSuppress", function (e) {
    e.preventDefault();
    $("#frmSuppress")[0].reset();
    $("#suppressStudentId").val(currentStudentInfo.studentId);
    $("#suppressTermNo").val(currentStudentInfo.termNo);
    $("#suppressFromDate").val(currentStudentInfo.todayDate); 
    $("#expressDateGroup").hide();
    $("#expressDate").val(""); 
    $("#modalSuppress .modal-title").html(`<i class="fas fa-exclamation-triangle mr-1"></i> Suppress`);  
    $("#modalSuppress button[type='submit']").html(`<i class="fas fa-check"></i> OK`);
    $("#modalSuppress").modal("show");
});
$(document).on("click", ".btn-edit-suppress", function (e) {
    e.preventDefault(); 
    const table = $("#tblSuppress").DataTable();
    const row = table.row($(this).closest("tr")).data();  
    if (!row) return ShowToastError("Cannot get suppress data.");  
    const suppressId = parseInt(row.suppressId || 0); 
    $("#frmSuppress")[0].reset();  
    $("#SuppressId").val(suppressId);
    $("#suppressStudentId").val(row.studentId);
    $("#suppressTermNo").val(row.termNo);
    $("#suppressFromDate").val(formatDateForInput(row.suppressDate));
    $("#suppressReason").val(row.reasonOfSuppress); 
    if (suppressId > 0) {
        $("#expressDateGroup").show(); 
        $("#expressDate").val(formatDateForInput(row.expressDate) || currentStudentInfo.todayDate);
        $("#modalSuppress .modal-title").html(`<i class="fas fa-check-circle mr-1"></i> Express`); 
        $("#modalSuppress button[type='submit']").html(`<i class="fas fa-check"></i> Express`);
    } else {
        $("#expressDateGroup").hide();
        $("#expressDate").val(""); 
        $("#modalSuppress .modal-title").html(`<i class="fas fa-exclamation-triangle mr-1"></i> Suppress`); 
        $("#modalSuppress button[type='submit']").html(`<i class="fas fa-check"></i> OK`);
    } 
    $("#modalSuppress").modal("show");
});
$(document).on("submit", "#frmSuppress", async function (e) {
    e.preventDefault(); 
    const studentId = $("#suppressStudentId").val(); 
    if (!studentId) return ShowToastInfo("Please select student."); 
    const suppressId = parseInt($("#SuppressId").val() || 0); 
    const btnSubmit = $("#modalSuppress button[type='submit']"); 
    btnSubmit.prop("disabled", true); 
    try {
        const response = await $.ajax({
            url: "/student/suppress",
            method: "POST",
            data: $("#frmSuppress").serialize()
        }); 
        if (response.status.code === "200") {
            needReloadStudentList = true;
            ShowToastSuccess(suppressId > 0 ? "Express saved successfully." : "Suppress saved successfully."); 
            $("#tblSuppress").DataTable().ajax.reload();
            $("#modalSuppress").modal("hide");
        } else {
            ShowToastError(response.status?.message || "Save failed.");
        }
    } catch (err) {
        ShowToastError(err.responseText || "Error saving change suppress.");
    } finally {
        btnSubmit.prop("disabled", false);
    }
});
// =====2. suspend
$(document).on("click", "#btnSuspend", function (e) {
    e.preventDefault();
    $("#frmSuspend")[0].reset();
    $("#suspendStudentId").val(currentStudentInfo.studentId);
    $("#suspendPromotionId").val(currentStudentInfo.promotionId);
    $("#suspendGroupId").val(currentStudentInfo.groupId);
    $("#suspendTermNo").val(currentStudentInfo.termNo);
    $("#suspendFromDate").val(currentStudentInfo.todayDate);
    $('#suspendToDate').val(currentStudentInfo.todayDate);
    $("#modalSuspend").modal("show");
});
$(document).on("submit", "#modalSuspend", async function (e) {
    e.preventDefault();
    const studentId = $("#suspendStudentId").val();
    if (!studentId) return ShowToastInfo("Please select student.");
    const btnSubmit = $("#modalSuspend button[type='submit']");
    btnSubmit.prop("disabled", true);
    try {
        const response = await $.ajax({
            url: "/student/suspend",
            method: "POST",
            data: $("#frmSuspend").serialize()
        });
        if (response.status.code === "200") {
            needReloadStudentList = true;
            ShowToastSuccess("Suspend saved successfully.");
            setStudentStatus("SUSPEND"); 
            $("#tblSuspend").DataTable().ajax.reload();
            $("#modalSuspend").modal("hide");
        } else {
            ShowToastError(response.status?.message || response.message || "Save failed.");
        }
    } catch (err) {
        ShowToastError(err.responseText || "Error saving change suspend.");
    } finally {
        btnSubmit.prop("disabled", false);
    }
});
// =====3. quit
$(document).on("click", "#btnQuit", function (e) {
    e.preventDefault();
    $("#frmQuit")[0].reset();
    $("#quitStudentId").val(currentStudentInfo.studentId);
    $("#quitTermNo").val(currentStudentInfo.termNo);
    $("#quitFromDate").val(currentStudentInfo.todayDate);
    $("#quitGroupId").val(currentStudentInfo.groupId);
    $("#quitPromotionId").val(currentStudentInfo.promotionId);
    $("#modalQuit").modal("show");
});
$(document).on("submit", "#frmQuit", async function (e) {
    e.preventDefault();
    const studentId = $("#quitStudentId").val();
    if (!studentId) return ShowToastInfo("Please select student.");
    const btnSubmit = $("#frmQuit button[type='submit']");
    btnSubmit.prop("disabled", true);
    try {
        const response = await $.ajax({
            url: "/student/quit",
            method: "POST",
            data: $("#frmQuit").serialize()
        });
        if (response.status.code === "200") {
            needReloadStudentList = true;
            setStudentStatus("QUIT");
            ShowToastSuccess("Quit saved successfully."); 
            $("#tblQuit").DataTable().ajax.reload();
            $("#modalQuit").modal("hide");
        } else {
            ShowToastError(response.status?.message || response.message || "Save failed.");
        }
    } catch (err) {
        ShowToastError(err.responseText || "Error saving change quit.");
    } finally {
        btnSubmit.prop("disabled", false);
    }
});
// =====4. change branch
$(document).on("click", "#btnChangeBranch", async function (e) {
    e.preventDefault();
    $("#frmChangeBranch")[0].reset();
    $("#changeBranchStudentId").val(currentStudentInfo.studentId);
    $("#changeBranchTermNo").val(currentStudentInfo.termNo);
    $("#changeBranchFromDate").val(currentStudentInfo.todayDate);
    await BindSelectOptions("/branch/get-branches","cboChangeBranchId","branchId","branchName",{isAll:true},"Select Branch");
    $("#modalChangeBranch").modal("show");
});
$(document).on("click", ".btn-edit-change-branch", async function (e) {
    e.preventDefault();
    await BindSelectOptions(
        "/branch/get-branch",
        "cboChangeBranchId",
        "branchId",
        "branchName",
        { isAll: true },
        "Select Branch"
    );
    const table = $("#tblBranchHistory").DataTable();
    const row = table.row($(this).closest("tr")).data();
    if (!row) return ShowToastError("Cannot get change branch data.");
    const changeBranchId = parseInt(row.changeBranchId || 0);
    $("#frmChangeBranch")[0].reset();
    $("#changeBranchId").val(changeBranchId);
    $("#changeBranchStudentId").val(row.studentId);
    $("#cboChangeBranchId").val(String(row.toBranchId || "")).trigger("change");
    $("#changeBranchTermNo").val(row.termNo);
    $("#changeBranchFromDate").val(formatDateForInput(row.fromDate));
    $("#changeBranchReturnDateGroup").hide();
    $("#returnDate").val("");
    $("#modalChangeBranch .modal-title").html(`<i class="fas fa-undo-alt-circle mr-1"></i> Return Change Branch`);
    $("#modalChangeBranch button[type='submit']").html(`<i class="fas fa-save"></i> Return`);
    $("#modalChangeBranch").modal("show");
});
$(document).on("submit", "#frmChangeBranch", async function (e) {
    e.preventDefault();
    const changeBranchId = $("#changeBranchId").val();
    const branchId = $("#cboChangeBranchId").val();
    const fromDate = $("#changeBranchFromDate").val();
    const studentId = $("#changeBranchStudentId").val();
    if (!branchId) return ShowToastInfo("Please select branch.");
    if (!fromDate) return ShowToastInfo("Please select from date.");
    if (!studentId) return ShowToastInfo("Please select student.");
    const btnSubmit = $("#frmChangeBranch button[type='submit']");
    btnSubmit.prop("disabled", true);
    try {
        const response = await $.ajax({
            url: "/student/change-branch",
            method: "POST",
            data: $("#frmChangeBranch").serialize()
        });
        if (response.status.code === "200") {
            needReloadStudentList = true;
            if (changeBranchId > 0){
                ShowToastSuccess("Return change branch saved successfully.");  
            }else {
                setStudentStatus("CHANGE BRANCH");
                actionButtons.find("button").addClass("d-none");
                $("#btnEdit").removeClass("d-none");
                $("#btnUpdate").removeClass("d-none");
                ShowToastSuccess("Change branch saved successfully.");
            } 
            $("#tblBranchHistory").DataTable().ajax.reload();
            $("#modalChangeBranch").modal("hide");
        } else {
            ShowToastError(response.status?.message || "Save failed.");
        } 
    } catch (err) {
        ShowToastError(err.responseText || "Error saving change branch.");
    } finally {
        btnSubmit.prop("disabled", false);
    }
});
// =====5. adjust
$(document).on("click", "#btnAdjustGroup", async function (e) {
    e.preventDefault();
    $("#frmAdjustGroup")[0].reset();
    await BindSelectOptions("/group/get-groups","cboAdjustGroupId","groupId","groupName",{isAll:true},"Select Group");
    const table = $("#tblStudentGroup").DataTable();
    const row = table.row($(this).closest("tr")).data();  
    if (!row) return ShowToastError("Cannot get student group data.");
    $("#adjustStudentId").val(currentStudentInfo.studentId); 
    $("#adjustStudentGroupId").val(row.studentGroupId); 
    $("#cboAdjustGroupId").val(row.groupId).trigger("change");
    $("#adjustTermNo").val(row.termNo);
    await getStudentScore(row.studentGroupId);
    $("#modalAdjustGroup").modal("show");
});
async function getStudentScore(studentGroupId){
    const tblScore = $("#tblAdjustStudentScore");
    tblScore.DataTable().clear().destroy();
    tblScore.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        searching: false,
        lengthChange: false,
        pageLength: 10,
        ajax: {
            url: "/scores/get-scores/"+ studentGroupId,
            type: "POST",
            dataSrc: function (json) {
                return json.data || [];
            },
            error: function (xhr) {
                ShowToastError(xhr.responseText || "Error loading student scores.");
            }
        },
        columns: [
            { data: "scoreId", visible: false },
            { data: "studentGroupId", visible: false },
            { data: "courseId", visible: false },
            { data: "courseName" },
            {
                data: "midTermScore",
                className: "text-center",
                render: function (data) {
                    return data ?? 0;
                }
            },
            {
                data: "finalScore",
                className: "text-center",
                render: function (data) {
                    return data ?? 0;
                }
            },
            { data: "type", className: "text-center" },
            {
                data: "isAllow",
                className: "text-center",
                render: function (data) {
                    return data
                        ? `<span class="badge badge-success">Allow</span>`
                        : `<span class="badge badge-secondary">No</span>`;
                }
            }
        ]
    });
} 
$(document).on("submit", "#modalAdjustGroup", async function (e) {
    e.preventDefault();
    const studentId = $("#adjustStudentId").val();
    if (!studentId) return ShowToastInfo("Please select student.");
    const btnSubmit = $("#modalAdjustGroup button[type='submit']");
    btnSubmit.prop("disabled", true);
    try {
        const response = await $.ajax({
            url: "/student/adjust-group",
            method: "POST",
            data: $("#frmAdjustGroup").serialize()
        });
        if (response.status.code === "200") {
            ShowToastSuccess("Adjust saved successfully.");
            await fetchStudentGroup(studentId);
            $("#modalAdjustGroup").modal("hide");
        } else {
            ShowToastError(response.status?.message || response.message || "Save failed.");
        }
    } catch (err) {
        ShowToastError(err.responseText || "Error saving change adjust.");
    } finally {
        btnSubmit.prop("disabled", false);
    }
});
// =====6. change school
$(document).on("click", "#btnChangeSchool", async function (e) {
    e.preventDefault();
    $("#frmChangeSchool")[0].reset();
    $("#changeSchoolStudentId").val(currentStudentInfo.studentId);
    $("#changeSchoolTermNo").val(currentStudentInfo.termNo);
    await BindSelectOptions("/degree/get-degrees", "cboChangeDegreeId", "degreeId", "degreeName",{isAll: true},"Select Degree");
    await BindSelectOptions("/school/get-schools", "cboChangeSchoolId", "schoolId", "schoolName",{isAll: true},"Select School");
    $("#modalChangeSchool").modal("show");
});
$(document).on("change", "#cboChangeSchoolId, #cboChangeDegreeId", async function () {
    const degreeId = $("#cboChangeDegreeId").val();
    const schoolId = $("#cboChangeSchoolId").val();
    if (!degreeId || !schoolId) return;
    await BindSelectOptions(
        "/Field/get-fields",
        "cboChangeFieldId",
        "fieldId",
        "fieldName",
        {
            isAll: true,
            degreeId: degreeId,
            schoolId: schoolId
        },
        "Select Field"
    );
});
$(document).on("change", "#cboChangeFieldId", async function () {
    const fieldId = $("#cboChangeFieldId").val();
    if (!fieldId) return;
    await BindSelectOptions(
        "/group/get-groups",
        "cboChangeGroupId",
        "groupId",
        "groupName",
        {
            isAll: true,
            fieldId: fieldId,
        },
        "Select Group"
    );
});
// endregion

// region "Tab History"
//=====1-_TabScholarship
async function fetchStudentScholarship(studentId) { 
    const tblStudentScholarship = $("#tblStudentScholarship");
    tblStudentScholarship.DataTable().clear().destroy();
    tblStudentScholarship.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: "/student-scholarship/get-student-scholarship/" + studentId,
            type: "POST",
            dataSrc: function (json) {
                if (!json.data || json.data.length === 0) {
                    $("#scholarship-tab").closest("li").hide();
                    $("#scholarship").hide();
                }else{
                    $("#scholarship-tab").closest("li").removeAttr("style");
                    $("#scholarship").removeAttr("style");
                }
                return json.data;
            },
            error: function (xhr, status, error) {
                ShowToastError(xhr.responseText);
            }
        },
        columns: [
            {data: "studentScholarshipId"},
            {data: "studentId"},
            {data: "termNo"},
            {data: "isFullScholarship"},
            {data: "amount"},
            {data: "sponsorName"},
        ]
    });
}
//====2-_TabIssueLetter
async function fetchStudentIssueLetter(studentId) { 
    const tblStudentIssueLetter = $("#tblStudentIssueLetter");
    tblStudentIssueLetter.DataTable().clear().destroy();
    tblStudentIssueLetter.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: "/student-letters/get-student-letter/" + studentId,
            type: "POST",
            dataSrc: function (json) {
                if (!json.data || json.data.length === 0) {
                    $("#issue-letter-tab").closest("li").hide();
                    $("#issue-letter").hide();
                }else{
                    $("#issue-letter-tab").closest("li").removeAttr("style");
                    $("#issue-letter").removeAttr("style");
                }
                return json.data;
            },
            error: function (xhr, status, error) {
                ShowToastError(xhr.responseText);
            }
        },
        columns: [
            {data: "studentLetterId"},
            {data: "studentId"},
            {data: "letterName"},
            {
                data: "doneDate1",
                render: function (data) {
                    return formatDate(data);
                }
            },
            {
                data: "doneDate2",
                render: function (data) {
                    return formatDate(data);
                }
            },
            {data: "issuedNo"}, 
            {
                data: "issuedDate",
                render: function (data) {
                    return formatDate(data);
                }
            },
            {data: "author"},
        ]
    });
}
//====3-_TabCertificate
async function fetchStudentCertificate(studentId) { 
    const tblStudentCertificate = $("#tblStudentCertificate");
    tblStudentCertificate.DataTable().clear().destroy();
    tblStudentCertificate.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: "/student-certificate/get-student-certificate/" + studentId,
            type: "POST",
            dataSrc: function (json) { 
                if (!json.data || json.data.length === 0) {
                    $("#certificate-tab").closest("li").hide();
                    $("#certificate").hide();
                }else{
                    $("#certificate-tab").closest("li").removeAttr("style");
                    $("#certificate").removeAttr("style");
                }
                return json.data;
            },
            error: function (xhr, status, error) {
                ShowToastError(xhr.responseText);
            }
        },
        columns: [
            {data: "studentCertificateId"},
            {data: "studentId"},
            {data: "certificateName"},
            {data: "grade"},
            {data: "isReceived"},
            {data: "certificateIssueNo"},
        ]
    });
}
//====4-_TabBranchHistory
async function fetchStudentBranchHistory(studentId) { 
    const tblBranchHistory = $("#tblBranchHistory");
    tblBranchHistory.DataTable().clear().destroy();
    tblBranchHistory.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: "/change-branch/get-student-change-branch/" + studentId,
            type: "POST",
            dataSrc: function (json) {
                if (!json.data || json.data.length === 0) {
                    $("#branch-history-tab").closest("li").hide();
                    $("#branch-history").hide();
                }else{
                    $("#branch-history-tab").closest("li").removeAttr("style");
                    $("#branch-history").removeAttr("style");
                }
                return json.data;
            },
            error: function (xhr, status, error) {
                ShowToastError(xhr.responseText);
            }
        },
        columns: [
            {data: "changeBranchId"},
            {data: "studentId"},
            {data: "toBranchId"},
            {data: "branchName"},
            {data: "termNo"}, 
            {
                data: "fromDate",
                render: function (data) {
                    return formatDate(data);
                }
            },
            {
                data: "returnDate",
                render: function (data) {
                    return formatDate(data);
                }
            }, 
            {data: "degreeId"},
            {data: "schoolId"},
            {data: "fieldId"},
            {data: "promotionId"},
            {data: "stageId"},
            {data: "groupId"},
            {
                data: null,
                className: "text-center",
                render: function () {
                    return `
                        <button type="button"
                                class="btn btn-sm btn-primary btn-edit-change-branch"
                                title="Edit">
                            <i class="fa fa-undo"></i> Return
                        </button>
                    `;
                }
            }
        ]
    });
}
//===5-TabSuppressHistory
async function fetchSuppress(studentId) { 
    const tblSuppress = $("#tblSuppress");
    tblSuppress.DataTable().clear().destroy();
    tblSuppress.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: "/student-suppress/get-student-suppress/" + studentId,
            type: "POST",
            dataSrc: function (json) {
                if (!json.data || json.data.length === 0) {
                    $("#suppress-history-tab").closest("li").hide();
                    $("#suppress-history").hide();
                }else{
                    $("#suppress-history-tab").closest("li").removeAttr("style");
                    $("#suppress-history").removeAttr("style");
                }
                return json.data;
            },
            error: function (xhr, status, error) {
                ShowToastError(xhr.responseText);
            }
        },
        columns: [
            {data: "suppressId"},
            {data: "studentId"},
            {data: "termNo"},
            {
                data: "suppressDate",
                render: function (data) {
                    return formatDate(data);
                }
            },
            {
                data: "expressDate",
                render: function (data) {
                    return formatDate(data);
                }
            }, 
            {data: "reasonOfSuppress"},
            {
                data: null, 
                className: "text-center",
                render: function () {
                    return `
                        <button type="button"
                                class="btn btn-sm btn-primary btn-edit-suppress"
                                title="Edit">
                            <i class="fa fa-edit"></i>
                        </button>
                    `;
                }
            }
        ]
    });
}
//==6-TabAbsence
//====7-_TabPayment
async function fetchStudentPayment(studentId) {  
    const tblStudentPayment = $("#tblStudentPayment");
    tblStudentPayment.DataTable().clear().destroy();
    tblStudentPayment.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: "/payment/get-student-payments/" + studentId,
            type: "POST",
            dataSrc: function (json) { 
                if (!json.data || json.data.length === 0) {
                    $("#payment-tab").closest("li").hide();
                    $("#payment").hide();
                }else{
                    $("#payment-tab").closest("li").removeAttr("style");
                    $("#payment").removeAttr("style");
                }
                return json.data;
            },
            error: function (xhr, status, error) {
                ShowToastError(xhr.responseText);
            }
        },
        columns: [
            {data: "paymentId"},
            {data: "studentId"},
            {data: "invoiceNo"},
            {
                data: "invoiceDate",
                render: function (data) {
                    return formatDate(data);
                }
            },
            {data: "termNo"},
            {data: "paid"},
            {data: "deposit"},
            {data: "note"},
            {data: "isInsurance"},
            {data: "guardian"},
        ]
    });
}
//====8-_TabSuspend
async function fetchSuspend(studentId) { 
    const tblSuspend = $("#tblSuspend");
    tblSuspend.DataTable().clear().destroy();
    tblSuspend.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: "/student-suspend/get-student-suspend/" + studentId,
            type: "POST",
            dataSrc: function (json) {
                if (!json.data || json.data.length === 0) {
                    $("#suspend-tab").closest("li").hide();
                    $("#suspend").hide();
                }else{
                    $("#suspend-tab").closest("li").removeAttr("style");
                    $("#suspend").removeAttr("style");
                } 
                return json.data;
            },
            error: function (xhr, status, error) {
                ShowToastError(xhr.responseText);
            }
        },
        columns: [
            {data: "suspendId"},
            {data: "studentId"},
            {data: "termNo"},
            {data: "promotionNo"},
            {data: "groupName"},
            {
                data: "fromDate",
                render: function (data) {
                    return formatDate(data);
                }
            },
            {
                data: "toDate",
                render: function (data) {
                    return formatDate(data);
                }
            },
            {data: "reasonOfSuspend"}
        ]
    });
}
//===9_TabGroupHistory
async function fetchStudentGroup(studentId) { 
    const tblStudentGroup = $("#tblStudentGroup");
    tblStudentGroup.DataTable().clear().destroy();
    tblStudentGroup.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: "/student-group/get-student-group/" + studentId,
            type: "POST",
            dataSrc: function (json) {
                if (!json.data || json.data.length === 0) {
                    $("#group-history-tab").closest("li").hide();
                    $("#group-history").hide();
                }else{
                    $("#group-history-tab").closest("li").removeAttr("style");
                    $("#group-history").removeAttr("style");
                }
                return json.data;
            },
            error: function (xhr, status, error) {
                ShowToastError(xhr.responseText);
            }
        },
        columns: [
            {data: "studentGroupId"},
            {data: "studentId"},
            {data: "termNo"},
            {data: "groupName"},
            {
                data: null,
                className: "text-center",
                render: function () {
                    return (window.studentPrivileges?.isFull === true || window.studentPrivileges?.isAdjustGroup === true)
                        ? `<button type="button"
                                    class="btn btn-sm btn-primary"
                                    id="btnAdjustGroup"
                                    title="Edit">
                                 <i class="fas fa-sliders-h"></i> Adjust
                            </button>`
                        : "";
                }
            }
        ]
    });
}
//====10-_TabExtendFrom
async function fetchStudentExtend(studentId) { 
    const tblStudentExtend = $("#tblStudentExtend");
    tblStudentExtend.DataTable().clear().destroy();
    tblStudentExtend.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: "/extend/get-student-extend/" + studentId,
            type: "POST",
            dataSrc: function (json) { 
                if (!json.data || json.data.length === 0) {
                    $("#extend-from-tab").closest("li").hide();
                    $("#extend-from").hide();
                }else{
                    $("#extend-from-tab").closest("li").removeAttr("style");
                    $("#extend-from").removeAttr("style");
                } 
                return json.data;
            },
            error: function (xhr, status, error) {
                ShowToastError(xhr.responseText);
            }
        },
        columns: [
            {data: "extendId"},
            {data: "studentId"},
            {data: "termNo"},
            {data: "extendFrom"},
            {data: "from"},
            {data: "isCertificateReceived"},
            {data: "isTranscriptReceived"},
            {
                data: "extendDate",
                render: function (data) {
                    return formatDate(data);
                }
            },
        ]
    });
}
//====11-_TabComplement
async function fetchComplement(studentId) { 
    const tblComplement = $("#tblComplement");
    tblComplement.DataTable().clear().destroy();
    tblComplement.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: "/student-compliment-payment/get-student-complement-payment/" + studentId,
            type: "POST",
            dataSrc: function (json) {
                if (!json.data || json.data.length === 0) {
                    $("#complementation-tab").closest("li").hide();
                    $("#complementation").hide();
                }else{
                    $("#complementation-tab").closest("li").removeAttr("style");
                    $("#complementation").removeAttr("style");
                }
                return json.data;
            },
            error: function (xhr, status, error) {
                ShowToastError(xhr.responseText);
            }
        },
        columns: [
            {data: "studentComplementPaymentId"},
            {data: "studentId"},
            {data: "invoiceNo"},
            {
                data: "invoiceDate",
                render: function (data) {
                    return formatDate(data);
                }
            },
            {data: "semester"},
            {data: "paid"},
            {data: "deposit"},
            {data: "discount"},
            {data: "reason"},
            {data: "note"},
        ]
    });
}
//====12-_TabQuit
async function fetchQuit(studentId) { 
    const tblQuit = $("#tblQuit");
    tblQuit.DataTable().clear().destroy();
    tblQuit.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: "/student-quit/get-student-quit/" + studentId,
            type: "POST",
            dataSrc: function (json) {
                if (!json.data || json.data.length === 0) {
                    $("#quit-tab").closest("li").hide();
                    $("#quit").hide();
                }else{
                    $("#quit-tab").closest("li").removeAttr("style");
                    $("#quit").removeAttr("style");
                } 
                return json.data;
            },
            error: function (xhr, status, error) {
                ShowToastError(xhr.responseText);
            }
        },
        columns: [
            {data: "quitId"},
            {data: "studentId"},
            {data: "termNo"},
            {data: "promotionNo"},
            {data: "groupName"},
            {
                data: "quitDate",
                render: function (data) {
                    return formatDate(data);
                }
            },
            {data: "reasonOfQuit"}
        ]
    });
}
// endregion

frmStudent.on("submit", function (event) {
    event.preventDefault();
    const form = $(this);
    const formData = form.serialize();
    const btnUpdate = $("#btnUpdate"); 
    if (btnUpdate.prop("disabled")) return;
    btnUpdate.prop("disabled", true);
    showLoading();
    $.ajax({
        url: '/student/update-student',
        method: 'PATCH',
        data: formData,
        success: function (response) {
            if (response.status.code === "200") {
                ShowToastSuccess("Saved successfully!"); 
                needReloadStudentList = true;
                $("#studentDetailModal").modal("hide");
            } else {
                ShowToastError(response.message);
                btnUpdate.prop("disabled", false);
            }
        }, 
        error: function (error) {
            if (error.responseJSON && error.responseJSON.message) {
                ShowToastError(error.responseJSON.message);
            } else {
                ShowToastError("Something went wrong.");
            } 
            btnUpdate.prop("disabled", false);
        },
        complete: function () {
            hideLoading(2);
        }
    }); 
});