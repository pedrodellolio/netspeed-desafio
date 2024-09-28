/**
 * Mostrar tooltip nos botões uma determinada classe
 * @param {string} target
 * @param {string} mensagem
 */
function addTooltipBotoes(target, mensagem) {
    // 
    $(target).hover(
        function (e) {
            if ($(this).is(':disabled')) {
                const tooltip = $(`<div class="custom-tooltip">${mensagem}</div>`).appendTo('body');
                tooltip.css({
                    top: e.pageY + 10 + 'px',
                    left: e.pageX + 10 + 'px',
                }).fadeIn('slow');
            }
        },
        function () {
            $('.custom-tooltip').remove();
        }
    );

    $(document).on('mousemove', function (e) {
        $('.custom-tooltip').css({
            top: e.pageY + 10 + 'px',
            left: e.pageX + 10 + 'px'
        });
    });
}

/**
 * Habilita o duplo clique redirecionar para a edição da linha
 * @param {string} target
 * @param {object} table
 * @param {string} redirectUrl
 */
function habilitaDuploCliqueEditar(target, table, redirectUrl) {
    $(target).on('dblclick', 'tr', function () {
        var data = table.row(this).data();
        window.location.href = config.contextPath + redirectUrl + data.ID;
    });
}