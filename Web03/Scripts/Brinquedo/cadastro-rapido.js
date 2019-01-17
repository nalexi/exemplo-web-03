$(function () {

    $("#salvar").on("click", function () {
        $nome = $("#campo-nome").val();
        $marca = $("#campo-marca").val();
        $preco = $("#campo-preco").val();
        $estoque = $("#campo-estoque").val();
        $.ajax({
            type: 'post',
            url: 'brinquedo/storerapido',
            data: {
                nome: $nome,
                marca: $marca,
                preco: $preco,
                estoque: $estoque
            },
            success: function (data) {
                var fim = JSON.parse(data)
            },
        });
    });
});