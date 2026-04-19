const tblDailyReport = $("#tblDailyReport");
const modalDailyReport = $("#modalDailyReport");
const formDailyReport = $("#formDailyReport");
const btnAddNew = $("#btnAddNew");
const btnClose = $("#btnClose");
$(document).ready(async function () {
    fetchDailyReport();
});

let selectedFiles = [];

function previewFiles() {
    const input = document.getElementById("fileInput");
    const preview = document.getElementById("preview");
    const addBtn = document.getElementById("addImageBtn");

    const newFiles = Array.from(input.files);

    newFiles.forEach(file => {
        const exists = selectedFiles.some(f =>
            f.name === file.name &&
            f.size === file.size &&
            f.lastModified === file.lastModified
        );

        if (!exists) {
            selectedFiles.push(file);
        }
    });

    // remove only previews
    preview.querySelectorAll(".preview-item").forEach(e => e.remove());

    selectedFiles.forEach((file, index) => {
        const div = document.createElement("div");
        div.className = "preview-item";
        div.style.position = "relative";
        div.style.width = "120px";
        div.style.height = "120px";
        div.style.border = "1px solid #ccc";
        div.style.borderRadius = "6px";
        div.style.overflow = "hidden";

        const img = document.createElement("img");
        img.style.width = "100%";
        img.style.height = "100%";
        img.style.objectFit = "cover";

        const reader = new FileReader();
        reader.onload = e => img.src = e.target.result.toString();
        reader.readAsDataURL(file);

        div.appendChild(img);

        // remove button
        const btn = document.createElement("button");
        btn.type = "button";
        btn.style.position = "absolute";
        btn.style.top = "2px";
        btn.style.right = "2px";
        btn.style.background = "transparent";
        btn.style.color = "red";
        btn.style.border = "none";
        btn.style.fontSize = "18px";
        btn.style.cursor = "pointer";
        btn.innerHTML = '<i class="fas fa-times-circle"></i>';

        btn.onclick = () => {
            selectedFiles.splice(index, 1);
            previewFiles();
        };

        div.appendChild(btn);

        preview.insertBefore(div, addBtn);
    });

    // reset input so same file CAN be selected later if removed
    input.value = "";
}


function fetchDailyReport() {
    tblDailyReport.DataTable().clear().destroy();
    tblDailyReport.DataTable({
        paging: true,
        lengthChange: true,
        searching: true,
        ordering: true,
        info: true,
        responsive: true,
        processing: true,
        serverSide: true,
        ajax: {
            url: "/daily-report/gets",
            type: "POST",
            error: function (xhr) {
                console.log(xhr.responseText);
            }
        },
        columns: [
            {data: "id"},
            {
                data: "title",
                render: function (data) {
                    if (data.length > 30) {
                        return `${data.slice(0, 30)}...`;
                    }
                    return data;
                }
            },
            {
                data: "description"
            },
            {data: "createDate",},
            {
                data: null,
                render: function () {
                    return `
                        <ul class="nav nav-pills">
                                    <li class="nav-item dropdown">
                                        <a class="btn btn-default btn-sm dropdown-toggle btn-sm" data-toggle="dropdown"
                                           href="#" aria-expanded="false">
                                            <i class="fa-solid fa-gear"></i><span class="caret"></span>
                                        </a>
                                        <div class="dropdown-menu dropdown-menu-right" style="min-width: 8rem;">
                                            <div class="col-md-12">
                                                <a id="btnEdit" class="btn btn-warning btn-sm"
                                                   style="display: block;margin:.2rem 0"><i class="fas fa-edit"></i>
                                                    ${i18n.edit}</a>
                                                <a id="btnDelete"  class="btn btn-danger btn-sm"
                                                   style="display: block;margin:.2rem 0"><i
                                                        class="fa-solid fa-trash"></i> ${i18n.delete}</a>
                                                <a id="btnView" class="btn btn-info btn-sm"
                                                   style="display: block;margin:.2rem 0"><i
                                                        class="fa-solid fa-eye"></i> ${i18n.view}</a>
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                    `;
                }
            }
        ]
    });
}

btnAddNew.on("click", function (event) {
    event.preventDefault();
    modalDailyReport.modal("show");
});
btnClose.on("click", function () {
    modalDailyReport.modal("hide");
    formDailyReport[0].reset();
});

formDailyReport.on("submit", async function (e) {
    e.preventDefault();
    const formData = new FormData(this);
    selectedFiles.forEach(file => {
        formData.append("images", file);
    });
    // console.log(formData);
    $.ajax({
        url: '/daily-report/save-changes',
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            if (response.status.code === "200") {
                ShowToastSuccess("Saved successfully!");
                formDailyReport[0].reset();
                resetFiles();
                modalDailyReport.modal("hide");
                tblDailyReport.DataTable().ajax.reload();
            } else {
                ShowToastError(response.message);
            }
        },
        error: function (error) {
            console.log(error.responseText);
        }
    });
});


tblDailyReport.on("click", "#btnView", function () {
    const $row = $(this).closest('tr');
    const jsonData = tblDailyReport.DataTable().row($row).data();
    console.log(jsonData);
    $("#dailyReportDto_Id").val(jsonData.id);
    $("#dailyReportDto_Title").val(jsonData.title);
    $("#dailyReportDto_TitleKhmer").val(jsonData.titleKhmer);
    $("#dailyReportDto_Campus").val(jsonData.campus.toString().toLowerCase());
    $("#dailyReportDto_ReportDate").val(formatDateForInput(jsonData.reportDate)).trigger("change");
    $("#dailyReportDto_RequestDate").val(formatDateForInput(jsonData.requestDate)).trigger("change");
    $("#dailyReportDto_Description").val(jsonData.description);
    loadImages(jsonData.images);
    modalDailyReport.modal("show");
})

function loadImages(imageUrls) {
    const preview = document.getElementById("preview");
    const addBtn = document.getElementById("addImageBtn");
    preview.querySelectorAll(".preview-item").forEach(e => e.remove());

    imageUrls.forEach((file, index) => {
        selectedFiles.push(file);
        const div = document.createElement("div");
        div.className = "preview-item";
        div.style.position = "relative";
        div.style.width = "120px";
        div.style.height = "120px";
        div.style.border = "1px solid #ccc";
        div.style.borderRadius = "6px";
        div.style.overflow = "hidden";

        const img = document.createElement("img");
        img.style.width = "100%";
        img.style.height = "100%";
        img.style.objectFit = "cover";
        img.src = file;

        div.appendChild(img);

        // remove button
        const btn = document.createElement("button");
        btn.type = "button";
        btn.style.position = "absolute";
        btn.style.top = "2px";
        btn.style.right = "2px";
        btn.style.background = "transparent";
        btn.style.color = "red";
        btn.style.border = "none";
        btn.style.fontSize = "18px";
        btn.style.cursor = "pointer";
        btn.innerHTML = '<i class="fas fa-times-circle"></i>';

        btn.onclick = () => {
            selectedFiles.splice(index, 1);
            previewFiles();
        };
        div.appendChild(btn);
        preview.insertBefore(div, addBtn);
    });
    
    console.log(selectedFiles);
}

$("#dailyReportDto_Description").on("input", function (event) {
    $("#charCount").text(event.target.value.length);
});

function resetFiles() {
    selectedFiles = [];
    document.getElementById("fileInput").value = "";
    document.getElementById("preview").innerHTML = `<button type="button" class="add-image-btn"
                                onclick="document.getElementById('fileInput').click()">
                            <i class="fas fa-images"></i>
                        </button>`;
    $("#charCount").text(0);
}