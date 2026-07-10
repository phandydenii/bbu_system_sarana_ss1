// function campus() {
//     const campus = localStorage.getItem("campus");
//     const token = localStorage.getItem("token");
//     console.log(campus);
//     if (campus == "") {
//         window.location.href = "/user/login";
//     }
//     if (token == "") {
//         window.location.href = "/user/login";
//     }
//     return campus;
// }

// debounce search
window.MyApp = window.MyApp || {};
MyApp.utils = {
    debounce : function (func, delay) {
        let timer; 
        return function (...args) {
            clearTimeout(timer); 
            timer = setTimeout(() => {
                func.apply(this, args);
            }, delay);
        };
    },
    dataTableDebounceSearch : function (tableSelector, delay = 500) {
        const table = $(tableSelector).DataTable();
        const input = $(tableSelector + "_filter input");
        const debouncedSearch = this.debounce(function (value) {
            table.search(value).draw();
        }, delay);
        input.off(); // remove ALL old datatable events
        input.on("input", function () {
            debouncedSearch(this.value);
        });
    }
}; 
// end of debounce search


function formatDate(dateStr, formart = "DD-MMM-YYYY") {
    if (!dateStr) return "";
    return moment(dateStr).format(formart);
}

function alertMessage(msg) {
    alert(msg);
}

function validateForm(formId) {
    let form = document.getElementById(formId);
    let inputs = form.querySelectorAll("[required]");
    let isValid = true;

    // remove old error borders
    inputs.forEach(input => input.classList.remove("is-invalid"));

    for (let input of inputs) {
        if (!input.value.trim()) {
            input.classList.add("is-invalid");

            // if Select2 dropdown, open it
            if ($(input).hasClass("select2")) {
                $(input).select2("open");
            }

            input.focus();
            isValid = false;
        } else {
            input.classList.add("is-valid");
        }
    }

    return isValid;
}

function showSkeleton() {
    $("#page-skeleton").show();
    $("#page-content").hide();
}
function hideSkeleton(timeout = 3) {
    setTimeout(()=>{
        $("#page-skeleton").hide();
        $("#page-content").show();
    }, timeout*1000);
}
function showLoading() {
    document.getElementById('loadingOverlay').style.display = 'flex';
}

function hideLoading(timeout=3) {
    // Simulate async task (e.g., AJAX request)
    setTimeout(() => {
        document.getElementById('loadingOverlay').style.display = 'none';
    }, timeout * 1000); // 2 seconds delay
}

function isNotValid(value) {
    return (
        value === "" ||           // empty string
        value == null ||          // null or undefined
        Number.isNaN(value)       // NaN
    );
}

function toNum(value) {
    const num = parseFloat(value);
    return isNaN(num) ? 0 : num;
}
async function BindSelectOptions(url, cbo, key, val, requestData = { isAll: true }, placeholder = "Select"
) {
    const selectOptions = $(`#${cbo}`);
    try {
        selectOptions.empty();
        selectOptions.append(`<option value="" disabled selected>${placeholder}</option>`);
        const response = await $.ajax({
            url: url,
            method: "POST",
            data: requestData
        });
        if (response.status.code === "200" && response.data && response.data.length > 0) {
            response.data.forEach(item => {
                selectOptions.append(`<option value="${item[key]}">${item[val]}</option>`);
            });
        } else {
            ShowToastError(response.responseText || "No data");
        }
        selectOptions.val("").trigger("change");
    } catch (err) {
        ShowToastError(err.responseText || err);
    }
}

async function BindSelectOptions1(url, cbo, key, val) {
    try {
        const response = await $.ajax({
            url: url,
            method: 'POST',
            data: { isAll: true }
        });
        if (response.status.code === "200" && response.data && response.data.length > 0) {
            const selectOptions = $(`#${cbo}`);
            selectOptions.empty();
            selectOptions.append("<option value='' disabled selected>Select</option>");
            response.data.forEach(item => {
                selectOptions.append(`<option value='${item}'>${item}</option>`);
            });
            selectOptions.trigger("change");
        }else{
            console.log(response.responseText);
        }
    } catch (err) {
        console.log(err.responseText);
    }
}
function formatDateForInput(dateString) {
    return new Date(dateString).toISOString().slice(0, 10);
}