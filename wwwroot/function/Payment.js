// $(document).ready(function () {
//     document.getElementById("btnSavePayment").disabled = true;
//     document.getElementById("btnNewItem").disabled = true;
// });

// document.getElementById("btnSearchStudent").addEventListener("click", function (e) {
//     $("#StudentModal").modal("show");
//     $("#tblSearchStudent").DataTable().clear().destroy();
//     $("#tblSearchStudent").DataTable({
//         processing: true,
//         serverSide: true,
//         paging: true,
//         lengthChange: true,
//         searching: true,
//         ordering: true,
//         info: true,
//         responsive: true,
//         autoWidth: true,
//         scrollCollapse: true,
//         ajax: {
//             url: "/student/academic/studentlist",
//             type: "POST",
//         },
//         columns: [
//             { data: "student_id" },
//             { data: "student_name" },
//             { data: "student_name_in_khmer" },
//             { data: "sex" },
//             {
//                 data: null,
//                 render: function (data, type, row) {
//                     if (row.status === "REGISTER") {
//                         return ` <span class="badge badge-warning">${row.status}</span>`;
//                     } else if (row.status === "ACTIVE") {
//                         return ` <span class="badge badge-primary">${row.status}</span>`;
//                     } else if (row.status === "QUIT") {
//                         return ` <span class="badge badge-danger">${row.status}</span>`;
//                     } else {
//                         return `<span class="badge badge-secondary">${row.status}</span>`;
//                     }
//                 },
//             },
//             {
//                 data: null,
//                 render: function (data, type, row) {
//                     return `<a id="SelectStudent" class="btn btn-success btn-sm" style="display: block;margin:.2rem 0rem">Select</a>`;
//                 },
//             },
//         ],
//     });
// });
// // document.getElementById("SelectStudent").addEventListener("click", function () {
// //     console.log("test" + e);
// // });
// // function OnSelectStudent(e) {
// //     const $input = $(e);
// //     const $row = $input.closest('tr');
// //     const selectedRow = $("#tblSearchStudent").DataTable().row($row).data();
// //     if (selectedRow) {
// //         document.getElementById("btnSavePayment").disabled = false;
// //         document.getElementById("btnNewItem").disabled = false;
// //         getStudent(selectedRow.student_id);
// //         getInvoice(selectedRow.student_id);
// //         $("#StudentModal").modal("hide");
// //     }

// // }
// function getStudent(student_id) {
//     $.ajax({
//         url: "/student/academic/student_id/" + student_id,
//         type: "GET",
//         data: { student_id: student_id },
//         success: function (data) {
//             // console.log(data);
//             if (data) {
//                 const student = data["student"];
//                 const degree = data["degree"];
//                 const school = data["school"];
//                 const field = data["field"];
//                 const promotion = data["promotion"];
//                 const stage = data["stage"];
//                 const term = data["term"];
//                 const group = data["group"];
//                 const groupRoom = data["groupRoom"];
//                 const registry = data["registry"];
//                 $("#invoice_student_id").val(student.student_id);
//                 $("#student_student_name").val(`${student.student_name_in_khmer} - ${student.student_name}`);
//                 $("#student_sex").val(student.sex);
//                 $("#student_date_of_birth").val(formatDate(student.date_of_birth, "DD-MMM-YYYY"));
//                 $("#invoice_degree_id").val(degree.degree);
//                 $("#invoice_school_id").val(school.school_name);
//                 $("#invoice_promotion_id").val(promotion.promotion_no);
//                 $("#invoice_stage_id").val(stage.stage_no);
//                 $("#invoice_field_id").val(field.field_name);
//                 if (student.status === "ACTIVE") {
//                     $("#invoice_promotion_id").val(promotion.promotion_no);
//                     $("#invoice_stage_id").val(stage.stage_no);
//                     $("#invoice_term_no").val(term.term_no);
//                     $("#group_study_time").val(group.study_time);
//                     $("#invoice_startdate").val(formatDate(term.start_date, "DD-MMM-YYYY"));
//                     $("#invoice_enddate").val(formatDate(term.end_date, "DD-MMM-YYYY"));
//                     $("#invoice_group_id").val(group.group_name);
//                     $("#groupRoom_room_name").val(groupRoom.room_name);
//                 } else if (student.status === "REGISTER") {
//                     $("#invoice_promotion_id").val(registry.promotion_no);
//                     $("#invoice_stage_id").val(registry.stage_no);
//                     $("#invoice_term_no").val(registry.term_no);
//                     $("#group_study_time").val(registry.study_time);
//                     $("#invoice_startdate").val("");
//                     $("#invoice_enddate").val("");
//                     $("#invoice_group_id").val("");
//                     $("#groupRoom_room_name").val("");
//                 }
//             } else {
//                 alert("Student not found.");
//             }
//         },
//         error: function () {
//             alert("Error fetching student data.");
//         }
//     });
// }
// function getInvoice(student_id) {
//     $("#tblInvoice").DataTable().clear().destroy();
//     $("#tblInvoice").DataTable({
//         processing: true,
//         serverSide: true,
//         paging: true,
//         lengthChange: false,
//         searching: false,
//         ordering: false,
//         info: false,
//         responsive: true,
//         autoWidth: true,
//         ajax: {
//             url: "/invoice/getinvoicebystudentid",
//             type: "POST",
//             data: function (d) {
//                 d.student_id = student_id;
//             }
//         },
//         columns: [
//             { data: "invoice_no" },
//             {
//                 data: null,
//                 render: function (data, type, row) {
//                     const dt = formatDate(row.invoice_date, "DD-MMM-YYYY");
//                     return `${dt}`
//                 }
//             },
//             { data: "student_id", className: "hide-col" },
//             { data: "term_no" },
//             { data: "grand_total" },
//             { data: "totaldiscount" },
//             { data: "vat" },
//             {
//                 data: null,
//                 render: function (data, type, row) {
//                     return row.owe > 0 ? `<button type="button" class="btn btn-danger btn-sm">${row.owe}</button>` : 0;
//                 }
//             },
//             { data: "totalriel" },
//             { data: "totaldollar" },
//             { data: "totalbath", className: "hide-col" },
//             { data: "totalriel" },
//             { data: "totaldollar" },
//             { data: "totalbath", className: "hide-col" },
//             {
//                 data: null,
//                 render: function (data, type, row) {
//                     return `
//                                 <div class="input-group-prepend">
//                                     <button type="button" class="btn btn-primary dropdown-toggle btn-sm" data-toggle="dropdown">
//                                         Action
//                                     </button>
//                                         <div class="dropdown-menu dropdown-menu-right" style="min-width: 8rem;">
//                                         <div class="col-md-12">
//                                             <a onclick="OnEdit(this)" class="btn btn-success btn-sm" style="display: block;margin:.2rem 0rem"><i class='fas fa-edit'></i> Edit</a>
//                                             ${row.owe <= 0 ?
//                             `<a onclick="OnDelete('${row.invoice_id}')" class="btn btn-warning btn-sm" style="display: block;margin:.2rem 0rem">Change Student</a>` :
//                             `<a onclick="OnDelete('${row.invoice_id}')" class="btn btn-warning btn-sm" style="display: block;margin:.2rem 0rem">Return</a>`
//                         }
//                                             <a onclick="OnDelete('${row.invoice_id}')" class="btn btn-danger btn-sm" style="display: block;margin:.2rem 0rem"><i class="fa-solid fa-trash"></i> Delete</a>
//                                         </div>
//                                     </div>
//                                 </div>
//                             `;
//                 }
//             }
//         ]
//     });
// }

// function GetInvoiceDetail(invoice_id) {
//     $("#tblInvoiceDetail").DataTable().clear().destroy();
//     $.ajax({
//         url: "/invoice/getinvoicedetail/" + invoice_id,
//         type: 'GET',
//         dataType: 'json',
//         success: function (data) {
//             // console.log(data);
//             data.forEach(item => {
//                 let listdata = [
//                     item.invoicedetail_id,
//                     item.invoice_id,
//                     item.product_id,
//                     item.product_name,
//                     item.product_name_khmer,
//                     item.qty,
//                     item.price_khr,
//                     item.price_usd,
//                     item.total_khr,
//                     item.total_usd,
//                     item.type,
//                     item.vat,
//                     item.discount_percent,
//                     item.discount_khr,
//                     item.discount_usd,
//                     item.owe_khr,
//                     item.owe_usd,
//                     item.grand_total_khr,
//                     item.grand_total_usd,
//                     item.pay_khr,
//                     item.pay_usd,
//                     item.pay_bath,
//                     item.tuitionfees,
//                     item.card_certificate,
//                     item.category_id,
//                     item.other
//                 ];
//                 // AddRow(listdata);
//             });
//         },
//         error: function (xhr, status, error) {
//             console.error('AJAX Error:', error);
//         }
//     });
// }
// function DeleteItem(btn) {
//     const row = btn.closest('tr');
//     if (row && confirm("Are you sure you want to delete this row?")) {
//         row.remove();
//     }
// }


// function OnEdit(e) {
//     const $input = $(e);
//     const $row = $input.closest('tr');
//     const selectedRow = $("#tblInvoice").DataTable().row($row).data();
//     GetInvoiceDetail(selectedRow.invoice_id);

//     document.getElementById("btnNewItem").classList.add("btn-warning");
//     document.getElementById("btnNewItem").value = "add_item";
//     document.getElementById("btnNewItem").innerText = "Add Item";

//     document.getElementById("btnSavePayment").classList.add("btn-success");
//     document.getElementById("btnSavePayment").value = "update_payment";
//     document.getElementById("btnSavePayment").innerText = "Update Payment";
// }

// document.getElementById("paymentForm").addEventListener("submit", function (e) {
//     e.preventDefault();
//     const form = $(this);
//     const formData = form.serialize();
//     $.ajax({
//         url: '/invoice/createpayment',
//         method: 'POST',
//         data: formData,
//         success: function (response) {
//             if (response.code === "200") {
//                 showSuccessToast("Saved successfully!");
//                 $("#tblInvoice").DataTable().ajax.reload();
//                 $("#tblInvoiceDetail").DataTable().clear().destroy();
//             } else {
//                 showErrorToast(response.message);
//             }
//         },
//         error: function (error) {
//             if (error.responseJSON && error.responseJSON.message) {
//                 showErrorToast(error.responseJSON.message);
//             }

//         }
//     });
// });


$(document).ready(function () {
    document.getElementById("btnSavePayment").disabled = true;
    document.getElementById("btnNewItem").disabled = true;
    const tblInvoice = $("#tblInvoice");
    tblInvoice.DataTable().clear().destroy();
    tblInvoice.DataTable({
        paging: true,
        lengthChange: true,
        searching: false,
        ordering: false,
        info: false,
        responsive: true,
        autoWidth: true,
    });
});

document.getElementById("btnSearchStudent").addEventListener("click", function (e) {
    $("#StudentModal").modal("show");
    $("#tblSearchStudent").DataTable().clear().destroy();
    $("#tblSearchStudent").DataTable({
        processing: true,
        serverSide: true,
        paging: true,
        lengthChange: true,
        searching: true,
        ordering: true,
        info: true,
        responsive: true,
        autoWidth: true,
        scrollCollapse: true,
        ajax: {
            url: "/student/academic/studentlist",
            type: "POST",
        },
        columns: [
            {data: "student_id"},
            {data: "student_name"},
            {data: "student_name_in_khmer"},
            {data: "sex"},
            {
                data: null,
                render: function (data, type, row) {
                    if (row.status === "REGISTER") {
                        return ` <span class="badge badge-warning">${row.status}</span>`;
                    } else if (row.status === "ACTIVE") {
                        return ` <span class="badge badge-primary">${row.status}</span>`;
                    } else if (row.status === "QUIT") {
                        return ` <span class="badge badge-danger">${row.status}</span>`;
                    } else {
                        return `<span class="badge badge-secondary">${row.status}</span>`;
                    }
                },
            },
            {
                data: null,
                render: function (data, type, row) {
                    return `<a onclick="OnSelectStudent(this)" class="btn btn-success btn-sm" style="display: block;margin:.2rem 0rem">Select</a>`;
                },
            },
        ],
    });
});

function OnSelectStudent(e) {
    const $input = $(e);
    const $row = $input.closest('tr');
    const selectedRow = $("#tblSearchStudent").DataTable().row($row).data();
    if (selectedRow) {
        document.getElementById("btnSavePayment").disabled = false;
        document.getElementById("btnNewItem").disabled = false;
        getStudent(selectedRow.student_id);
        getInvoice(selectedRow.student_id);
        $("#StudentModal").modal("hide");
    }

}

function getStudent(student_id) {
    $.ajax({
        url: "/student/academic/student_id/" + student_id,
        type: "GET",
        data: {student_id: student_id},
        success: function (data) {
            // console.log(data);
            if (data) {
                const student = data["student"];
                const degree = data["degree"];
                const school = data["school"];
                const field = data["field"];
                const promotion = data["promotion"];
                const stage = data["stage"];
                const term = data["term"];
                const group = data["group"];
                const groupRoom = data["groupRoom"];
                const registry = data["registry"];
                $("#invoice_student_id").val(student.student_id);
                $("#student_student_name").val(`${student.student_name_in_khmer} - ${student.student_name}`);
                $("#student_sex").val(student.sex);
                $("#student_date_of_birth").val(formatDate(student.date_of_birth, "DD-MMM-YYYY"));
                $("#invoice_degree_id").val(degree.degree);
                $("#invoice_school_id").val(school.school_name);
                $("#invoice_promotion_id").val(promotion.promotion_no);
                $("#invoice_stage_id").val(stage.stage_no);
                $("#invoice_field_id").val(field.field_name);
                if (student.status === "ACTIVE") {
                    $("#invoice_promotion_id").val(promotion.promotion_no);
                    $("#invoice_stage_id").val(stage.stage_no);
                    $("#invoice_term_no").val(term.term_no);
                    $("#group_study_time").val(group.study_time);
                    $("#invoice_startdate").val(formatDate(term.start_date, "DD-MMM-YYYY"));
                    $("#invoice_enddate").val(formatDate(term.end_date, "DD-MMM-YYYY"));
                    $("#invoice_group_id").val(group.group_name);
                    $("#groupRoom_room_name").val(groupRoom.room_name);
                } else if (student.status === "REGISTER") {
                    $("#invoice_promotion_id").val(registry.promotion_no);
                    $("#invoice_stage_id").val(registry.stage_no);
                    $("#invoice_term_no").val(registry.term_no);
                    $("#group_study_time").val(registry.study_time);
                    $("#invoice_startdate").val("");
                    $("#invoice_enddate").val("");
                    $("#invoice_group_id").val("");
                    $("#groupRoom_room_name").val("");
                }
            } else {
                alert("Student not found.");
            }
        },
        error: function () {
            alert("Error fetching student data.");
        }
    });
}

function getInvoice(student_id) {
    $("#tblInvoice").DataTable().clear().destroy();
    $("#tblInvoice").DataTable({
        processing: true,
        serverSide: true,
        paging: true,
        lengthChange: false,
        searching: false,
        ordering: false,
        info: false,
        responsive: true,
        autoWidth: true,
        ajax: {
            url: "/invoice/getinvoicebystudentid",
            type: "POST",
            data: function (d) {
                d.student_id = student_id;
            }
        },
        columns: [
            {data: "invoice_id", class: "hide-col"},
            {data: "invoice_no"},
            {
                data: null,
                render: function (data, type, row) {
                    const dt = formatDate(row.invoice_date, "DD-MMM-YYYY");
                    return `${dt}`
                }
            },
            {data: "student_id", class: "hide-col"},
            {data: "term_no"},
            {data: "grand_total"},
            {data: "totaldiscount"},
            {data: "vat"},
            {
                data: null,
                render: function (data, type, row) {
                    return row.owe > 0 ? `<button type="button" class="btn btn-danger btn-sm">${row.owe}</button>` : 0;
                }
            },
            {data: "totalriel"},
            {data: "totaldollar"},
            {data: "totalbath", class: "hide-col"},
            {data: "totalriel"},
            {data: "totaldollar"},
            {data: "totalbath", class: "hide-col"},
            {
                data: null,
                render: function (data, type, row) {
                    return `
                                <div class="input-group-prepend">
                                    <button type="button" class="btn btn-primary dropdown-toggle btn-sm" data-toggle="dropdown">
                                        Action
                                    </button>
                                        <div class="dropdown-menu dropdown-menu-right" style="min-width: 8rem;">
                                        <div class="col-md-12">
                                            <a onclick="OnEdit(this)" class="btn btn-success btn-sm" style="display: block;margin:.2rem 0rem"><i class='fas fa-edit'></i> Edit</a>
                                            ${row.owe <= 0 ?
                        `<a onclick="OnDelete('${row.invoice_id}')" class="btn btn-warning btn-sm" style="display: block;margin:.2rem 0rem">Change Student</a>` :
                        `<a onclick="OnDelete('${row.invoice_id}')" class="btn btn-warning btn-sm" style="display: block;margin:.2rem 0rem">Return</a>`
                    }
                                            <a onclick="OnDelete('${row.invoice_id}')" class="btn btn-danger btn-sm" style="display: block;margin:.2rem 0rem"><i class="fa-solid fa-trash"></i> Delete</a>
                                        </div>
                                    </div>
                                </div>
                            `;
                }
            }
        ]
    });
}

function GetInvoiceDetail(invoice_id) {
    $("#tblInvoiceDetail").DataTable().clear().destroy();
    $.ajax({
        url: "/invoice/getinvoicedetail/" + invoice_id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            // console.log(data);
            data.forEach(item => {
                let listdata = [
                    item.invoicedetail_id,
                    item.invoice_id,
                    item.product_id,
                    item.product_name,
                    item.product_name_khmer,
                    item.qty,
                    item.price_khr,
                    item.price_usd,
                    item.total_khr,
                    item.total_usd,
                    item.type,
                    item.vat,
                    item.discount_percent,
                    item.discount_khr,
                    item.discount_usd,
                    item.owe_khr,
                    item.owe_usd,
                    item.grand_total_khr,
                    item.grand_total_usd,
                    item.pay_khr,
                    item.pay_usd,
                    item.pay_bath,
                    item.tuitionfees,
                    item.card_certificate,
                    item.category_id,
                    item.other
                ];
                AddItemPayment(listdata);
            });
        },
        error: function (xhr, status, error) {
            console.error('AJAX Error:', error);
        }
    });
}

function DeleteItem(btn) {
    const row = btn.closest('tr');
    if (row && confirm("Are you sure you want to delete this row?")) {
        row.remove();
    }
}

// document.getElementById("NewPayment").addEventListener("click", function (e) {
//     $("#PaymentModal").modal("show");
// });

function OnEdit(e) {
    const $input = $(e);
    const $row = $input.closest('tr');
    const selectedRow = $("#tblInvoice").DataTable().row($row).data();
    GetInvoiceDetail(selectedRow.invoice_id);
    $("#PaymentModal").modal("show");
    // document.getElementById("btnNewItem").classList.add("btn-warning");
    document.getElementById("btnNewItem").value = "add_item";
    document.getElementById("btnNewItem").innerText = "Add Item";

    document.getElementById("btnSavePayment").classList.add("btn-success");
    document.getElementById("btnSavePayment").value = "update_payment";
    document.getElementById("btnSavePayment").innerText = "Update Payment";
}

document.getElementById("paymentForm").addEventListener("submit", function (e) {
    e.preventDefault();
    const form = $(this);
    const formData = form.serialize();
    $.ajax({
        url: '/invoice/createpayment',
        method: 'POST',
        data: formData,
        success: function (response) {
            if (response.code === "200") {
                showSuccessToast("Saved successfully!");
                $("#tblInvoice").DataTable().ajax.reload();
                $("#tblInvoiceDetail").DataTable().clear().destroy();
            } else {
                showErrorToast(response.message);
            }
        },
        error: function (error) {
            if (error.responseJSON && error.responseJSON.message) {
                showErrorToast(error.responseJSON.message);
            }

        }
    });
});

function AddItemPayment(params) {
    const tableBody = document.querySelector('#tblInvoiceDetail tbody');
    var i = document.querySelectorAll('#tblInvoiceDetail tbody tr').length;
    const row = document.createElement('tr');
    row.innerHTML = `
                <td data-name="invoice_detail_id" class="hide-col"><input type="text" name="invoiceDetails[${i}].invoice_detail_id" value="${params[0]}" /></td>
                <td data-name="invoice_id" class="hide-col"><input type="text" name="invoiceDetails[${i}].invoice_id" value="${params[1]}" /></td>
                <td data-name="product_id" class="hide-col"><input type="text" name="invoiceDetails[${i}].product_id" value="${params[2]}" /></td>
                <td data-name="product_name">${params[3]}</td>
                <td data-name="product_name_kher" class="hide-col">${params[4]}</td>
                <td data-name="qty">
                    <span class="view-mode">${params[5]}</span>
                    <input type="number" oninput="OnChangeQty(this)" name="invoiceDetails[${i}].qty" value="${params[5]}" class="form-control edit-mode" style="display:none;" min="1"/>
                </td>
                <td data-name="price_khr">
                    ${params[6]}
                    <input type="hidden" name="invoiceDetails[${i}].price_khr" value="${params[6]}" class="form-control edit-mode" style="display:none;" readonly/>
                </td>
                <td data-name="price_usd">
                    ${params[7]}
                    <input type="hidden" name="invoiceDetails[${i}].price_usd" value="${params[7]}" class="form-control edit-mode" style="display:none;" readonly/>
                </td>
                <td data-name="total_khr" class="hide-col">
                    <span class="view-mode">${params[8]}</span>
                    <input type="number" name="invoiceDetails[${i}].total_khr" value="${params[8]}" class="form-control edit-mode" style="display:none;" readonly/>
                </td>
                <td data-name="total_usd" class="hide-col">
                    <span class="view-mode">${params[9]}</span>
                    <input type="number" name="invoiceDetails[${i}].total_usd" value="${params[9]}" class="form-control edit-mode" style="display:none;" readonly/>
                </td>
                <td data-name="type" class="hide-col">
                    <span class="view-mode">${params[10]}</span>
                    <input type="number" name="invoiceDetails[${i}].type" value="${params[10]}" class="form-control edit-mode" style="display:none;" readonly/>
                </td>
                <td data-name="vat" class="hide-col">
                    <span class="view-mode">${params[11]}</span>
                    <input type="number" name="invoiceDetails[${i}].vat" value="${params[11]}" class="form-control edit-mode" style="display:none;" readonly/>
                </td>
                <td data-name="discount_percent">
                    <span class="view-mode">${params[12]}</span>
                    <input type="number" oninput="OnChangeDisPer(this)" name="invoiceDetails[${i}].discount_percent" value="${params[12]}" class="form-control edit-mode" style="display:none;" min="0"/>
                </td>
                <td data-name="discount_khr" class="hide-col">
                    <span class="view-mode">${params[13]}</span>
                    <input type="number" name="invoiceDetails[${i}].discount_khr" value="${params[13]}" class="form-control edit-mode" style="display:none;" readonly/>
                </td>
                <td data-name="discount_usd" class="hide-col">
                    <span class="view-mode">${params[14]}</span>
                    <input type="number" name="invoiceDetails[${i}].discount_usd" value="${params[14]}" class="form-control edit-mode" style="display:none;" readonly/>
                </td>
                <td data-name="owe_khr">
                    <span class="view-mode">${params[15]}</span>
                    <input type="number" oninput="OnChangeOweKhr(this)" name="invoiceDetails[${i}].owe_khr" value="${params[15]}" class="form-control edit-mode" style="display:none;" min="0"/>
                </td>
                <td data-name="owe_usd">
                    <span class="view-mode">${params[16]}</span>
                    <input type="number" oninput="OnChangeOweUsd(this)" name="invoiceDetails[${i}].owe_usd" value="${params[16]}" class="form-control edit-mode" style="display:none;" min="0"/>
                </td>
                <td data-name="other_khr">
                    <span class="view-mode">${params[17]}</span>
                    <input type="text" name="invoiceDetails[${i}].other_khr" value="${params[25]}" class="form-control edit-mode" style="display:none;"/>
                </td>
                <td data-name="other_usd">
                    <span class="view-mode">${params[18]}</span>
                    <input type="text" name="invoiceDetails[${i}].other_usd" value="${params[26]}" class="form-control edit-mode" style="display:none;"/>
                </td>
                <td data-name="grand_total_khr" class="hide-col">
                    <span class="view-mode">${params[19]}</span>
                    <input type="number" name="invoiceDetails[${i}].grand_total_khr" value="${params[17]}" class="form-control edit-mode" style="display:none;" readonly/>
                </td>
                <td data-name="grand_total_usd" class="hide-col">
                    <span class="view-mode">${params[20]}</span >
                    <input type="number" name="invoiceDetails[${i}].grand_total_usd" value="${params[18]}" class="form-control edit-mode" style="display:none;" readonly />
                </td >
                <td data-name="pay_khr">
                    <span class="view-mode">${params[21]}</span>
                    <input type="number" oninput="OnChangePayR(this)" name="invoiceDetails[${i}].pay_khr" value="${params[19]}" class="form-control edit-mode" style="display:none;" min="0"/>
                </td>
                <td data-name="pay_usd">
                    <span class="view-mode">${params[22]}</span>
                    <input type="text" name="invoiceDetails[${i}].pay_usd" value="${params[20]}" class="form-control edit-mode" style="display:none;" readonly/>
                </td>
                <td data-name="pay_bath" class="hide-col">
                    <span class="view-mode">${params[23]}</span>
                    <input type="text" name="invoiceDetails[${i}].pay_bath" value="${params[21]}" class="form-control edit-mode" style="display:none;" readonly/>
                </td>
                <td data-name="tuitionfees" class="hide-col">
                    <span class="view-mode">${params[24]}</span>
                    <input type="text" name="invoiceDetails[${i}].tuitionfees" value="${params[22]}" class="form-control edit-mode" style="display:none;" readonly/>
                </td>
                <td data-name="card_certificate" class="hide-col">
                    <span class="view-mode">${params[25]}</span>
                    <input type="text" name="invoiceDetails[${i}].card_certificate" value="${params[23]}" class="form-control edit-mode" style="display:none;" readonly/>
                </td>
                <td data-name="category_id" class="hide-col">
                    <span class="view-mode">${params[26]}</span>
                    <input type="text" name="invoiceDetails[${i}].category_id" value="${params[24]}" class="form-control edit-mode" style="display:none;" readonly/>
                </td>
               
                <td>
                    <a onclick="ToggleEditMode(this)" class="btn btn-warning btn-xs" style="margin:.2rem 0rem" data-editing="false"><i class="fa-solid fa-pen-to-square" ></i></a>
                    <a onclick="DeleteItem(this)" class="btn btn-danger btn-xs" style="margin:.2rem 0rem"><i class="fa-solid fa-trash"></i></a>
                </td>
    `;
    tableBody.appendChild(row);
    i += 1;
    CalculateGrandTotal();
} 