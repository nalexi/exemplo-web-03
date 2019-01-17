$(function () {

    $("#salvar").on("click", function () {
        $nome = $("#campo-nome").val();
        $.ajax({
            type: 'post',
            url: 'categoria/storerapido',
            data: {
                nome: $nome
            },
            success: function (data) {
                var fim = JSON.parse(data)
            },
        });
    });
});