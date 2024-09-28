$(document).ready(function () {
    addValidacaoDataNaoRetroativa();
    addDatePicker();
    addAutocompleteSolicitante();
    addBotaoCancelar();
    addBotaoSalvar();
    addBotaoExcluir();
});


function addValidacaoDataNaoRetroativa() {
    $.validator.addMethod(
        'dataNaoRetroativa',
        function (value, _) {
            if (value) {
                var today = new Date();
                var todayString = today.getFullYear() + '-' + (today.getMonth() + 1).toString().padStart(2, '0') + '-' + today.getDate();
                var selectedDate = value.split('/').reverse().join('-');
                return selectedDate >= todayString;
            }
            return true;
        },
        'A data de abertura não pode ser retroativa.'
    );

    $('#DataAbertura').rules('add', {
        dataNaoRetroativa: true
    });

    $('#DataAbertura').on('change', function () {
        $(this).valid();
    });
}

function addDatePicker() {
    $('.glyphicon-calendar').closest('div.date').datepicker({
        todayBtn: 'linked',
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: false,
        format: 'dd/mm/yyyy',
        autoclose: true,
        language: 'pt-BR'
    });
}

function addAutocompleteSolicitante() {
    const MIN_CARACTERES = 2;  // Número mínimo de caracteres para fazer a busca
    $('#Solicitante').on('input', function () {
        var input = $(this).val();
        $('#autocomplete-list').empty();

        if (input.length < MIN_CARACTERES) {
            $("#autocomplete-list").removeClass("custom-autocomplete");
            return;
        };

        $.ajax({
            url: config.contextPath + 'Chamados/ListarSolicitantes',
            type: 'GET',
            data: { termo: input },
            success: function (data) {
                if (data.length === 0) return;

                var ul = $('<ul id="autocomplete-list" class="autocomplete-list"></ul>');
                $("#solicitante-group").append(ul);

                data.forEach(function (solicitante) {
                    $('#autocomplete-list').append('<div class="autocomplete-item">' + solicitante + '</div>');
                });
                $('#autocomplete-list div').on('click', function () {
                    $('#Solicitante').val($(this).text());
                    $('#autocomplete-list').empty();
                    ul.remove();
                });
            },
            error: function (error) {
                console.error('Erro ao buscar solicitantes: ', error);
            }
        });
    });

    // Fechar autocomplete ao clicar fora do campo
    $(document).on('click', function (e) {
        if (!$(e.target).closest('#Solicitante').length) {
            $('#autocomplete-list').empty();
            $(".autocomplete-list").remove();
        }
    });
}

function addBotaoCancelar() {
    $('#btnCancelar').click(function () {
        Swal.fire({
            html: 'Deseja cancelar essa operação? O registro não será salvo.',
            type: 'warning',
            showCancelButton: true,
        }).then(function (result) {
            if (result.value) {
                history.back();
            } else {
                console.log('Cancelou a inclusão.');
            }
        });
    });
}
function addBotaoSalvar() {
    $('#btnSalvar').click(function () {
        if ($('#form').valid() != true) {
            FormularioInvalidoAlert();
            return;
        }

        let chamado = SerielizeForm($('#form'));
        let url = $('#form').attr('action');
        //debugger;

        $.ajax({
            type: 'POST',
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
}

function addBotaoExcluir() {
    $('#btnExcluir').click(function () {

        let idRegistro = $('#ID').val();
        let assunto = $('#assunto').val();

        if (!idRegistro || idRegistro <= 0) {
            return;
        }

        if (idRegistro) {
            Swal.fire({
                text: "Tem certeza de que deseja excluir " + assunto + " ?",
                type: "warning",
                showCancelButton: true,
            }).then(function (result) {

                if (result.value) {
                    $.ajax({
                        url: config.contextPath + 'Chamados/Excluir/' + idRegistro,
                        type: 'DELETE',
                        contentType: 'application/json',
                        error: function (result) {

                            Swal.fire({
                                text: result,
                                confirmButtonText: 'OK',
                                icon: 'error'
                            });

                        },
                        success: function (result) {

                            Swal.fire({
                                type: result.Type,
                                title: result.Title,
                                text: result.Message,
                            }).then(function () {
                                window.location.href = config.contextPath + 'Chamados/'
                            });
                        }
                    });
                } else {
                    console.log("Cancelou a exclusão.");
                }

            });
        }
    });
}

