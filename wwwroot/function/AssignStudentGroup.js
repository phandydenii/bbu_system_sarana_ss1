
const formContainer = $("#formContainer");
$(document).ready(async function () {
    renterForm("associate");
});

function renterForm(form) {
    formContainer.html("");
    if (!form) return;
    $(".select2").select2();
    formContainer.load("/academic/assign-student-group-form/" + form, function () {
        $(".select").select2({
            width: "100%",
            placeholder: "Select an option"
        });
    });
}

$("#rangeButtons").on("click", "button", function (e) {
    $("#rangeButtons").find("button").removeClass("btn-primary").addClass("btn-outline-primary");
    $(this).removeClass("btn-outline-primary").addClass("btn-primary");
    renterForm(e.target.value);
});