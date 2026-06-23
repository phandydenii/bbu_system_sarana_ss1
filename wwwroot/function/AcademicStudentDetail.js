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
    $("#txtStudentId").val(studId);
    await getStudent(studId);
    fetchStudentScholarship(studId);
    fetchStudentGroup(studId);
    fetchStudentExtend(studId);
    fetchStudentPayment(studId);
    fetchStudentBranchHistory(studId);
    fetchStudentIssueLetter(studId);
    fetchStudentCertificate(studId);
    fetchSuppress(studId);
    fetchSuspend(studId);
    fetchQuit(studId);
    fetchComplement(studId); 
    $('#custom-tabs-four-tab a:first').tab('show');
    $('#frmStudent :input').prop('disabled', true);
}
$(document).ready(async function () {
    frmStudent.prop("readonly", true);
    await BindData(); 
    $('#custom-tabs-four-tab a:first').tab('show');
    $('#frmStudent :input').prop('disabled', true);
 }); 
btnEdit.on("click", function (e) {
    e.preventDefault(); 
    const isEditing = $(this).data("editing") === true; 
    if (isEditing) {
        $(this).data("editing", false);
        $(this).html('<i class="fas fa-pen"></i>'); 
        actionButtons.find("button").not("#btnEdit").addClass("d-none");  
        $('#frmStudent :input').prop('disabled', true);
    } else {
        $(this).data("editing", true);
        $(this).html('<i class="fas fa-check"></i>');  
        const status = $("#student_Status").val(); 
        if (status === "CHANGE BRANCH") {
            actionButtons.find("button").addClass("d-none"); 
            $("#btnEdit").removeClass("d-none");
            $("#btnUpdate").removeClass("d-none");
        } else {
            actionButtons.find("button").removeClass("d-none");
        } 
        $('#frmStudent :input').prop('disabled', false);
    }
});
$("#studentDetailModal").on("hidden.bs.modal", function () {
    btnEdit.data("editing", false);
    btnEdit.html('<i class="fas fa-pen"></i>');
    actionButtons.find("button").not("#btnEdit").addClass("d-none");
    $("#frmStudent :input").prop("disabled", true);
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

async function getStudent(student_id) {
    $.ajax({
        url: "/student/academic/student-id/" + student_id,
        type: "GET",
        data: {student_id: student_id},
        success: function (result) {
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
                GetField(degree.degreeId, school.schoolId);
                setStudentStatus(student.status);
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
                $("#txtGroup").val(group.groupName);
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
                GetGroup(promotion.promotionId, term.termNo);
                $("#cboGroup").val(group.groupId).trigger("change");
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
// 1. suppress
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
            ShowToastSuccess(suppressId > 0 ? "Express saved successfully." : "Suppress saved successfully."); 
            $("#modalSuppress").modal("hide"); 
            if ($.fn.DataTable.isDataTable("#tblSuppress")) {
                $("#tblSuppress").DataTable().ajax.reload(null, false);
            }
        } else {
            ShowToastError(response.status?.message || "Save failed.");
        }
    } catch (err) {
        ShowToastError(err.responseText || "Error saving change suppress.");
    } finally {
        btnSubmit.prop("disabled", false);
    }
});
// 2. suspend
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
            ShowToastSuccess("Suspend saved successfully.");
            setStudentStatus("SUSPEND");
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
// 3. quit
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
            ShowToastSuccess("Quit saved successfully.");
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
// 4. change branch
$(document).on("click", "#btnChangeBranch", async function (e) {
    e.preventDefault();
    $("#frmChangeBranch")[0].reset();
    $("#changeBranchStudentId").val(currentStudentInfo.studentId);
    $("#changeBranchTermNo").val(currentStudentInfo.termNo);
    $("#changeBranchFromDate").val(currentStudentInfo.todayDate);
    await BindSelectOptions("/branch/get-branch","cboChangeBranchId","branchId","branchName",{isAll:true},"Select Branch");
    $("#modalChangeBranch").modal("show");
});
$(document).on("submit", "#frmChangeBranch", async function (e) {
    e.preventDefault();
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
            setStudentStatus("CHANGE BRANCH");
            actionButtons.find("button").addClass("d-none");
            $("#btnEdit").removeClass("d-none");
            $("#btnUpdate").removeClass("d-none");
            fetchStudentBranchHistory(studentId);
            ShowToastSuccess("Change branch saved successfully.");
            $("#modalChangeBranch").modal("hide");
        } else {
            ShowToastError(response.status?.message || response.message || "Save failed.");
        }
    } catch (err) {
        ShowToastError(err.responseText || "Error saving change branch.");
    } finally {
        btnSubmit.prop("disabled", false);
    }
});
// 5. adjust
$(document).on("click", "#btnAdjust", async function (e) {
    e.preventDefault();
    $("#frmAdjustGroup")[0].reset();
    $("#adjustStudentId").val(currentStudentInfo.studentId);
    $("#adjustTermNo").val(currentStudentInfo.termNo);
    await BindSelectOptions("/group/get-groups","cboAdjustGroupId","groupId","groupName",{isAll:true},"Select Group");
    $("#modalAdjustGroup").modal("show");
});
$(document).on("submit", "#modalAdjustGroup", async function (e) {
    e.preventDefault();
    const studentId = $("#suppressStudentId").val();
    if (!studentId) return ShowToastInfo("Please select student.");
    const btnSubmit = $("#modalAdjustGroup button[type='submit']");
    btnSubmit.prop("disabled", true);
    try {
        const response = await $.ajax({
            url: "/student/adjust",
            method: "POST",
            data: $("#frmAdjustGroup").serialize()
        });
        if (response.status.code === "200") {
            ShowToastSuccess("Adjust saved successfully.");
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
// 6. change school
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

async function GetField(degreeId, schoolId) {
    const cboField = $("#student_FieldId");
    cboField.empty();
    cboField.append("<option value='' disabled>Select</option>");
    const fields = await Field.GetFields({isAll: true, degreeId, schoolId});
    fields.forEach(item => {
        cboField.append(`<option value='${item.fieldId}'>${item.fieldName}</option>`);
    });
}

async function GetGroup(promotionId, termNo) {
    const cboGroup = $("#cboGroup");
    const groups = await Group.GetGroups();
    const stages = await Stage.GetStages();
    const terms = await Term.GetTerms();
    const filteredGroups = groups.filter(g =>
        stages.some(s => s.stageId === g.stageId && s.promotionId === promotionId) &&
        terms.some(t => t.stageId === g.stageId && t.termNo === termNo)
    );
    filteredGroups.forEach(item => {
        cboGroup.append(`<option value='${item.groupId}'>${item.groupName}</option>`);
    });
}
// region "Tab History"
//=====1-_TabScholarship
function fetchStudentScholarship(studentId) {
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
                    $("#scholarship-tab").closest("li").remove();
                    $("#scholarship").remove();
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
function fetchStudentIssueLetter(studentId) {
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
                    $("#issue-letter-tab").closest("li").remove();
                    $("#issue-letter").remove();
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
function fetchStudentCertificate(studentId) {
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
                    $("#certificate-tab").closest("li").remove();
                    $("#certificate").remove();
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
function fetchStudentBranchHistory(studentId) {
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
                    $("#branch-history-tab").closest("li").remove();
                    $("#branch-history").remove();
                    $("#btnChangeBranch").data("action","changeBranch");
                    $("#lblChangeBranch").text("Change Branch");
                }else{
                    $("#btnChangeBranch").data("action","return");
                    $("#lblChangeBranch").text("Return");
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
        ]
    });
}

//===5-TabSuppressHistory
function fetchSuppress(studentId) {
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
                    $("#suppress-history-tab").closest("li").remove();
                    $("#suppress-history").remove();
                    $("#btnSuppress").data("action","suppress");
                    $("#lblSuppress").text("Suppress");
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
function fetchStudentPayment(studentId) {
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
                    $("#payment-tab").closest("li").remove();
                    $("#payment").remove();
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
function fetchSuspend(studentId) {
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
                    $("#suspend-tab").closest("li").remove();
                    $("#suspend").remove();
                    $("#btnSuspend").data("action","suspend");
                    $("#lblSuspend").text("Suspend");
                }else{
                    $("#btnSuspend").data("action","resume");
                    $("#lblSuspend").text("Resume");
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
function fetchStudentGroup(studentId) {
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
                    $("#group-history-tab").closest("li").remove();
                    $("#group-history").remove();
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
        ]
    });
}


//====10-_TabExtendFrom
function fetchStudentExtend(studentId) {
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
                // console.log(json.data);
                if (!json.data || json.data.length === 0) {
                    $("#extend-from-tab").closest("li").remove();
                    $("#extend-from").remove();
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
function fetchComplement(studentId) {
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
                    $("#complementation-tab").closest("li").remove();
                    $("#complementation").remove();
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
function fetchQuit(studentId) {
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
                    $("#quit-tab").closest("li").remove();
                    $("#quit").remove();
                    $("#btnQuit").data("action","quit");
                    $("#lblQuit").text("Quit");
                }else{
                    $("#btnQuit").data("action","resume");
                    $("#lblQuit").text("Resume");
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
                window.location.reload();
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