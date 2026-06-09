function ShowToastSuccess(message = "Success") {
    toastr.success(message);
}

function ShowToastError(message = "Error") {
    toastr.error(message);
}

function ShowToastWarning(message = "Warning") {
    toastr.warning(message);
}

function ShowToastInfo(message = "Informatin") {
    toastr.info(message);
}

function ShowBoxConfirm(message = "Confirmation", onYes = null) {
    bootbox.confirm({
        title: "Confirm",
        message: message,
        buttons: {
            confirm: {
                label: "Yes",
                className: "btn-success"
            },
            cancel: {
                label: "No",
                className: "btn-danger"
            }
        },
        callback: function (result) {
            if (result && typeof onYes === "function") {
                onYes();
            }
        }
    });
}
