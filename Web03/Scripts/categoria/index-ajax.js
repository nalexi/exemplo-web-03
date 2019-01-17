$(function () {
    $.ajax({
        type: 'get',
        url: '/categoria/obterdados',
        success: function (resultado) {
            var dados = JSON.parse(resultado)
            $("#tabela-categoria").empty();
            for (var i = 0; i < dados.length; i++) {
                var categoria = dados[i];
                adicionarLinha(categoria);
            }
        }
    });
    $("#modal-cadastro-categoria-salvar").on("click", function () {
        salvar();
        $("#modal-cadastro-categoria").modal("hide");
    });
    $("#modal-cadastro-categoria-salvar-novo").on("click", function () {
        salvar();
        $("#modal-cadastro-categoria-nome").focus();
    });
    function salvar() {
        $nome = $("#modal-cadastro-categoria-nome").val().trim();
        $.ajax({
            type: 'post',
            url: '/categoria/storerapido',
            data: {
                nome: $nome,
            },
            success: function (data) {
                var categoria = JSON.parse(data);
                adicionarLinha(categoria);
                $("#modal-cadastro-categoria-nome").val("");
            }
        });
    }
    function adicionarLinha(categoria) {
        $colunaCodigo = "<td>" + categoria.Id + "</td>";
        $colunaNome = "<td>" + categoria.Nome + "</td>";
        $colunaAcao = "<td>\
                      <button class='btn btn-primary'>Editar</button>\
                      <button class=\"btn btn-danger botao-apagar\" data-id = '"+ categoria.Id + "'>Apagar</button>\
                      </td>";
        $linha = "<tr>" + $colunaCodigo + $colunaNome + $colunaAcao + "</tr>";
        $("#tabela-categoria").append($linha);
    }
    $("#tabela-categoria").on("click", ".botao-apagar", function () {
        $botao = $(this);
        $id = $botao.data("id");
        $.ajax({
            type: 'get',
            url: '/categoria/deleteajax',
            data: {
                id: $id
            },
            success: function (data) {
                $botao.parent().parent().remove();
            }
        });
    });

    $("#modal-cadastro-categoria").on('shown.bs.modal', function (e) {
        $("#modal-cadastro-categoria-nome").focus();
    }); 
});