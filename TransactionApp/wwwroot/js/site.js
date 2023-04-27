// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

var minAmount = document.getElementById('minAmount');
var maxAmount = document.getElementById('maxAmount');

minAmount.onkeyup = function () {
    if (maxAmount.value == "" || minAmount.value == "") {
        maxAmount.style.borderColor = "Red";
        minAmount.style.borderColor = "Red";
        document.getElementById('btnSearch').disabled = true;
    } 
    else {
        maxAmount.style.borderColor = "Green";
        minAmount.style.borderColor = "Green";
        document.getElementById('btnSearch').disabled = false;
    }

}
maxAmount.onkeyup = function () {
    if (minAmount.value == "" || maxAmount.value == "") {
        maxAmount.style.borderColor = "Red";
        minAmount.style.borderColor = "Red";
        document.getElementById('btnSearch').disabled = true;
    } 
    else {
        maxAmount.style.borderColor = "Green";
        minAmount.style.borderColor = "Green";
        document.getElementById('btnSearch').disabled = false;
    }
}
function ClearFields() {
    minAmount.value = "";
    maxAmount.value = "";
    document.getElementById('ddlClient').value = "";
    document.getElementById('ddlTransactionType').value = "";
}


$(function () {
    $('.edit').on('click', function () {
        $('.modal_body_edit').load('/Home/Edit/' + $(this).attr('data-bs-id'));
    });
})

$(function () {
    $('.delete').on('click', function () {
        $('.modal_body_delete').load('/Home/Delete/' + $(this).attr('data-bs-id'));
    });
})
