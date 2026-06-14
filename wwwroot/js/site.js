// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function renderTruncate(data, maxLength = 30) {
    if (data === null || data === undefined) return "";

    let text = data.toString();

    if (text.length > maxLength) {
        return `
            <span class="dt-truncate" title="${text}">
                ${text.substring(0, maxLength)}...
            </span>
        `;
    }

    return `
        <span class="dt-truncate" title="${text}">
            ${text}
        </span>
    `;
}

function adjustDataTables() {
    setTimeout(function () {
        $.fn.dataTable
            .tables({ visible: true, api: true })
            .columns.adjust();
    }, 300);

    setTimeout(function () {
        $.fn.dataTable
            .tables({ visible: true, api: true })
            .columns.adjust();
    }, 700);
}

$(window).on("resize", function () {
    adjustDataTables();
});

$("[data-widget='pushmenu']").on("click", function () {
    adjustDataTables();
});

$(document).on("collapsed.lte.pushmenu shown.lte.pushmenu", function () {
    adjustDataTables();
});