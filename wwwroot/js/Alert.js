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

function ShowBoxConfirm(message = "Confirmation") {
    let check = false;
    bootbox.confirm({
        title: "Confirm",
        message: message,
        // size: "small",
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-success'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {
                check = true;
            }
        }
    });
    return check;
}
