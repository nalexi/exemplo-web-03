$(function () {
    $.ajax({
        type: 'get',
        url: '/brinquedo/obterdados',
        success: function (resultado) {
            var dados = JSON.parse(resultado)
            $("#tabela-brinquedo").empty();
            for (var i = 0; i < dados.length; i++) {
                var brinquedo = dados[i];
                adicionarLinha(brinquedo);
            }
        }
    });
    $("#modal-cadastro-brinquedo-salvar").on("click", function () {
        salvar();
        $("#modal-cadastro-brinquedo").modal("hide");
    });
    $("#modal-cadastro-brinquedo-salvar-novo").on("click", function () {
        salvar();
        $("#modal-cadastro-brinquedo-nome").focus();
    });
    function salvar() {
        $nome = $("#modal-cadastro-brinquedo-nome").val().trim();
        $marca = $("#modal-cadastro-brinquedo-marca").val();
        $preco = $("#modal-cadastro-brinquedo-preco").val();
        $estoque = $("#modal-cadastro-brinquedo-estoque").val();
        $.ajax({
            type: 'post',
            url: '/brinquedo/storerapido',
            data: {
                nome: $nome,
                marca: $marca,
                preco: $preco,
                estoque: $estoque
            },
            success: function (data) {
                var brinquedo = JSON.parse(data);
                adicionarLinha(brinquedo);
                $("#modal-cadastro-brinquedo-nome").val("");
                $("#modal-cadastro-brinquedo-marca").val("");
                $("#modal-cadastro-brinquedo-preco").val("");
                $("#modal-cadastro-brinquedo-estoque").val("");
            }
        });
    }
    function adicionarLinha(brinquedo) {
        $colunaCodigo = "<td>" + brinquedo.Id + "</td>";
        $colunaNome = "<td>" + brinquedo.Nome + "</td>";
        $colunaMarca = "<td>" + brinquedo.Marca + "</td>";
        $colunaPreco = "<td>" + brinquedo.Preco + "</td>";
        $colunaEstoque = "<td>" + brinquedo.Estoque + "</td>";
        $colunaAcao = "<td>\
            <button class='btn btn-primary'>Editar</button>\
            <button class=\"btn btn-danger botao-apagar\" data-id= '"+ brinquedo.Id + "'>Apagar</button>\
               </td > ";
        $linha = "<tr>" + $colunaCodigo + $colunaNome + $colunaMarca + $colunaPreco + $colunaEstoque + $colunaAcao + "</tr>";
        $("#tabela-brinquedo").append($linha);
    }
    $("#tabela-brinquedo").on("click", ".botao-apagar", function () {
        $botao = $(this);
        $id = $botao.data("id");
        $.ajax({
            type: 'get',
            url: '/brinquedo/deleteajax',
            data: {
                id: $id
            },
            success: function (data) {
                $botao.parent().parent().remove();
            }
        });
    });

    $("#modal-cadastro-brinquedo").on('shown.bs.modal', function (e) {
        $("#modal-cadastro-brinquedo-nome").focus();
    });
});