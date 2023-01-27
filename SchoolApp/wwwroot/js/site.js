﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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