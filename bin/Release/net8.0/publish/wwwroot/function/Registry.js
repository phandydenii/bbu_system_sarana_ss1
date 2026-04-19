import {TABLE_CONFIG} from "../TableConfig/Registry.js";

const columns = TABLE_CONFIG.COLUMNS;
const urls = TABLE_CONFIG.URLS;

document.addEventListener("DOMContentLoaded", function () {
    fetchStudent();
});


function fetchStudent() {
    const tblRegistry = $("#tblRegistry");
    tblRegistry.DataTable().clear().destroy();
    tblRegistry.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: urls.GET_STUDENT_LIST,
            type: "POST",
            data: function (d) {
                d.from = $('#dtpFrom').val();
                d.to = $('#dtpTo').val();
            },
            dataSrc: function (json) {
                return json.data;
            }
        },
        columns: [
            {data: columns.STUDENT_ID},
            {data: columns.STUDENT_NAME},
            {data: columns.STUDENT_NAME_IN_KHMER},
            {data: columns.SEX},
            {
                data: null,
                render: function (data, type, row) {
                    const dt = formatDate(row.dateOfBirth);
                    return `${dt}`
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    const dt = formatDate(row.registrationDate);
                    return `${dt}`
                }
            },
            {data: columns.DEGREE_NAME},
            {data: columns.SCHOOL_NAME},
            {
                data: null,
                render: function (data, type, row) {
                    if (row.status === "REGISTER") {
                        return ` <span class="badge badge-warning">${row.status}</span>`;
                    } else if (row.status === "ACTIVE") {
                        return ` <span class="badge badge-primary">${row.status}</span>`;
                    } else if (row.status === "QUIT") {
                        return ` <span class="badge badge-danger">${row.status}</span>`;
                    }

                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    // if (row.status === "ACTIVE") {
                    //     return `<i class="fa-regular fa-circle-check"></i>`;
                    // }
                    return `
                        <a href="/registry/details/${row.studentId}" class="btn btn-success btn-sm" style="display: block;margin:.2rem 0">Manage</a>
                        `;
                }
            }
        ],
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
    });
}