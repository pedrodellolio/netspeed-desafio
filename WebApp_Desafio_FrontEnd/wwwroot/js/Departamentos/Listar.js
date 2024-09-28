const URL_EDITAR = 'Departamentos/Editar/';

$(document).ready(function () {
    const table = addTabela();
    inicializaBotoes(table);
    habilitaSelecaoLinhas(table);
    addBotaoRelatorio();
    addBotaoAdicionar();
    addBotaoEditar(table);
    habilitaDuploCliqueEditar('#dataTables-Chamados tbody', table, URL_EDITAR);
    addBotaoExcluir(table);
    addTooltipBotoes('.show-tooltip', 'Selecione um chamado');
});

function addTabela() {
    return $('#dataTables-Departamentos').DataTable({
        paging: false,
        ordering: false,
        info: false,
        searching: false,
        processing: true,
        serverSide: true,
        ajax: config.contextPath + 'Departamentos/Datatable',
        columns: [
            { data: 'ID' },
            { data: 'Descricao', title: 'Descrição' },
        ],
        drawCallback: function (settings) {
            $('#dataTables-Departamentos tbody tr').css('cursor', 'pointer');
        }
    });
}

function inicializaBotoes(table) {
    // Desabilitar botões se não houver uma linha selecionada (estado inicial)
    var registroSelecionado = table.row('.selected').data();
    $('#btnExcluir').prop('disabled', !registroSelecionado);
    $('#btnEditar').prop('disabled', !registroSelecionado);
}

function habilitaSelecaoLinhas(table) {
    $('#dataTables-Departamentos tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
        inicializaBotoes(table);
    });
}
function addBotaoEditar(table) {
    $('#btnEditar').click(function () {
        var data = table.row('.selected').data();
        window.location.href = config.contextPath + URL_EDITAR + data.ID;
    });
}

function addBotaoAdicionar(table) {
    $('#btnAdicionar').click(function () {
        window.location.href = config.contextPath + 'Departamentos/Cadastrar';
    });
}

function addBotaoRelatorio(table) {
    $('#btnRelatorio').click(function () {
        window.location.href = config.contextPath + 'Departamentos/Report';
    });
}

function addBotaoExcluir(table) {
    $('#btnExcluir').click(function () {

        let data = table.row('.selected').data();
        let idRegistro = data.ID;
        if (!idRegistro || idRegistro <= 0) {
            return;
        }

        if (idRegistro) {
            Swal.fire({
                text: "Tem certeza de que deseja excluir " + data.Descricao + " ?",
                type: "warning",
                showCancelButton: true,
            }).then(function (result) {

                if (result.value) {
                    $.ajax({
                        url: config.contextPath + 'Departamentos/Excluir/' + idRegistro,
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
                                table.draw();
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