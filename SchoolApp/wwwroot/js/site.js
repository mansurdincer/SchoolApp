// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function () {

    var PlaceHolderElement = $('#PlaceHolderHere');

    $('button[data-toggle="ajax-modal"]').click(function (event) {

        var url = $(this).data('url');
        $.get(url).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })

    PlaceHolderElement.on('click', '[data-save="modal"]', function (event) {
        var form = $(this).parents('.modal').find('form');
        var controller = $(this).data('controller');
        var action = $(this).data('action');
        var actionUrl = '/' + controller + '/' + action;
        var dataToSend = form.serialize();
        $.post(actionUrl, dataToSend).done(function (data) {
            PlaceHolderElement.find('.modal').modal('hide');
            location.reload(true);
        });

    });
})

function AddItem(ChildTable) {
    var table = document.getElementById(ChildTable);    
    var rows = table.getElementsByTagName('tr');
    var lastRow = rows[rows.length - 1];
    var newRow = table.insertRow();
    newRow.innerHTML = lastRow.innerHTML;
        
    var inputs = newRow.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        var id = inputs[i].id;
        id = id.replace(/_(\d+)_/, "_" + (rows.length - 2) + "_");
        id = id.replace(/\[(\d+)\]/, "[" + (rows.length - 2) + "]");
        id = id.replace(/-(\d+)/, "-" + (rows.length) - 2);
        inputs[i].id = id;
        inputs[i].name = inputs[i].name.replace(/\[(\d+)\]/, "[" + (rows.length - 2) + "]");
        inputs[i].value = "";
    }
}

function RemoveItem(button) {
    var table = $(button).closest('table');
    var tr = $(button).closest('tr');
    var rows = $(table).find('tbody tr');
    if (rows.length > 1) {
        $(tr).remove();
    }

    var itemIndex = -1;
    $('#ChildTable tr').each(function () {
        var this_row = $(this);

        console.log(itemIndex);
        this_row.find('input[name$=".Id"]').attr('name', 'Children[' + itemIndex + '].Id');

        this_row.find('input[name$=".Name"]').attr('name', 'Children[' + itemIndex + '].Name');

        itemIndex++;
    });
   
}