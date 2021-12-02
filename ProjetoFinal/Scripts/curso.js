$(document).ready(function () {
    var id = $("#CursoId option:selected").val();
    if (id !== "" && id !== undefined) {
        ObterInformacoesCurso(id);
    }

    $("#CursoId").change(function () {
        var cursoId = $("#CursoId option:selected").val();

        if (cursoId !== "" && cursoId !== undefined) {
            ObterInformacoesCurso(cursoId);
        }
    })
})

function ObterInformacoesCurso(cursoId) {
    var url = "/Curso/Informacoes";
    var data = { cursoId: cursoId };
    $("#valor").empty();
    $("#vagas").empty();

    $.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: data
    }).done(function (data) {
        console.log(data);
        if (data.Result != null) {
            $("#valor").append("Valor: R$ " + data.Result.Valor);
            $("#vagas").append("Vagas: " + data.Result.QuantidadeVagas);
        }
    })
}