﻿$(document).ready(function () {
    $.validator.addMethod(
        "dataNaoRetroativa",
        function (value, _) {
            if (value) {
                var today = new Date();
                var todayString = today.getFullYear() + '-' + (today.getMonth() + 1).toString().padStart(2, '0') + '-' + today.getDate();
                var selectedDate = value.split('/').reverse().join('-');
                return selectedDate >= todayString;
            }
            return true;
        },
        "A data de abertura não pode ser retroativa."
    );

    $("#DataAbertura").rules("add", {
        dataNaoRetroativa: true
    });

    //bootstrap-datepicker
    $('#DataAbertura').on('changeDate', function () {
        $(this).valid();
    });

    //html input type="date"
    $('#DataAbertura').on('change', function () {
        $(this).valid();
    });

    $('.glyphicon-calendar').closest("div.date").datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: false,
        format: 'dd/mm/yyyy',
        autoclose: true,
        language: 'pt-BR'
    });

    $('#btnCancelar').click(function () {
        Swal.fire({
            html: "Deseja cancelar essa operação? O registro não será salvo.",
            type: "warning",
            showCancelButton: true,
        }).then(function (result) {
            if (result.value) {
                history.back();
            } else {
                console.log("Cancelou a inclusão.");
            }
        });
    });

    $('#btnSalvar').click(function () {
        if ($('#form').valid() != true) {
            FormularioInvalidoAlert();
            return;
        }

        let chamado = SerielizeForm($('#form'));
        let url = $('#form').attr('action');
        //debugger;

        $.ajax({
            type: "POST",
            url: url,
            data: chamado,
            success: function (result) {

                Swal.fire({
                    type: result.Type,
                    title: result.Title,
                    text: result.Message,
                }).then(function () {
                    window.location.href = config.contextPath + result.Controller + '/' + result.Action;
                });

            },
            error: function (result) {

                Swal.fire({
                    text: result,
                    confirmButtonText: 'OK',
                    icon: 'error'
                });

            },
        });
    });

});
