const Ids = {
    LETTER_CATEGORY_ID: "letterCategory_CategoryId",
    LETTER_CATEGORY_NAME: "letterCategory_CategoryName",
    UNIT_PRICE: "letterCategory_UnitPrice",
    IS_ADMIN: "letterCategory_IsAdmin",
    IS_FOUNDATION: "letterCategory_IsFoundation",
    IS_SHORT_COURSE: "letterCategory_IsShortCourse",
    ACTIVE: "letterCategory_Active",
};
$(document).ready(function () {
    fetchLetterCategory();
});

function fetchLetterCategory() {
    let tblLetterCategory = $("#tblLetterCategory");
    tblLetterCategory.DataTable().clear().destroy();
    tblLetterCategory.DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        ajax: {
            url: "/LetterCategory/get-letter-category",
            type: "POST",
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        },
        columns: [
            {data: "categoryId"},
            {data: "categoryName"},
            {data: "unitPrice"},
            {
                data: null,
                render: function (data, type, row) {
                    let badges = [];
                    badges.push(row.isAdmin ? '<span class="badge bg-success">Admin</span>' : '');
                    badges.push(row.isFoundation ? '<span class="badge bg-info">Foundation</span>' : '');
                    badges.push(row.isShortCourse ? '<span class="badge bg-primary">Short Course</span>' : '');
                    return badges.filter(b => b).join(' ');
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    return row.active ? '<span class="badge bg-success">Active</span>' : '<span class="badge bg-danger">Inactive</span>';
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    const rowData = JSON.stringify(row).replace(/"/g, '&quot;');
                    return `<a class="btn btn-warning btn-sm" onclick="OnEditCategory('${rowData}')"><i class="fas fa-edit"></i></a>
                            <a class="btn btn-danger btn-sm" onclick="OnDelete(${row.categoryid})"><i class="fas fa-trash"></i></a>`;
                }
            }
        ]
    });
}

document.getElementById("btnAddNew").addEventListener("click", function () {
    $("#LetterCategoryModal").modal("show");
    $("#LetterCategoryModal .modal-title").text("Add New Letter Category");
});

function OnEditCategory(row) {
    row = JSON.parse(row);
    console.log(row);
    $("#LetterCategoryModal").modal("show");
    $("#LetterCategoryModal .modal-title").text("Edit Letter Category");

    document.getElementById(Ids.LETTER_CATEGORY_ID).value = row.categoryId;
    document.getElementById(Ids.LETTER_CATEGORY_NAME).value = row.categoryName;
    document.getElementById(Ids.UNIT_PRICE).value = row.unitPrice;

    document.getElementById("isAdmin").checked = row.isAdmin;
    document.getElementById("isFound").checked = row.isFoundation;
    document.getElementById("isShort").checked = row.isShortCourse;
}

function OnDelete(categoryId) {
    if (confirm("Are you sure you want to delete this category?")) {
        $.ajax({
            url: `/LetterCategory/delete/${categoryId}`,
            type: "POST",
            success: function (response) {
                if (response.success) {
                    alert("Category deleted successfully.");
                    fetchLetterCategory();
                } else {
                    alert("Error deleting category: " + response.message);
                }
            },
            error: function () {
                alert("An error occurred while deleting the category.");
            }
        });
    }
}


document.getElementById("frmLetterCategory").addEventListener("submit", function (e) {
    e.preventDefault();

    const form = $(this);
    const formData = form.serialize();

    $.ajax({
        url: '/LetterCategory/post-letter-category',
        method: 'POST',
        data: formData,
        success: function (response) {
            if (response.code === "200") {
                ShowToastSuccess("Saved successfully!");
                form[0].reset();
                $("#LetterCategoryModal").modal("hide");
                $("#tblLetterCategory").DataTable().ajax.reload();
            } else {
                ShowToastError(response.message);
            }
        },
        error: function (error) {
            if (error.responseJSON && error.responseJSON.message) {
                ShowToastError(error.responseJSON.message);
            }
        }
    });
});