function AbreAdmin(acao, controller, id, largura, altura) {
    
    var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
    var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

    var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
    var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

    var left = ((width / 2) - (largura / 2)) + dualScreenLeft;
    var top = ((height / 2) - (altura / 2)) + dualScreenTop;

    window.open('/' + controller + '/' + acao + '/' + id, 'Admin', 'width=' + largura + ', height=' + altura + ', top=' + top + ', left=' + left + ', scrollbars=yes, status=no, toolbar=no, location=no, directories=no, menubar=no, resizable=no, fullscreen=no');
}

function AbreRAB(acao, controller, id, largura, altura) {

    var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
    var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

    var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
    var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

    var left = ((width / 2) - (largura / 2)) + dualScreenLeft;
    var top = ((height / 2) - (altura / 2)) + dualScreenTop;

    window.open('/' + controller + '/' + acao + '/' + id, 'Admin', 'width=' + largura + ', height=' + altura + ', top=' + top + ', left=' + left + ', scrollbars=yes, status=no, toolbar=no, location=no, directories=no, menubar=no, resizable=no, fullscreen=no');
}

function AbreURL(url, titulo, largura, altura) {

    var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
    var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

    var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
    var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

    var left = ((width / 2) - (largura / 2)) + dualScreenLeft;
    var top = ((height / 2) - (altura / 2)) + dualScreenTop;

    window.open(url, titulo, 'width=' + largura + ', height=' + altura + ', top=' + top + ', left=' + left + ', scrollbars=yes, status=no, toolbar=no, location=no, directories=no, menubar=no, resizable=no, fullscreen=no');
}


$(document).ready(function () {

    $('#AdicionaModulo').click(function () {
        $('#IdLista option:selected').appendTo('#IdModulo');
    });

    $('#RemoveModulo').click(function () {
        $('#IdModulo option:selected').appendTo('#IdLista');
    });

    $('#salvaPerfil').click(function () {
        $('#IdModulo option').prop('selected', true);
    });


    $("#Cpf_cnpj").keydown(function () {
        try {
            $("#Cpf_cnpj").unmask();
        } catch (e) { }

        var tamanho = $("#Cpf_cnpj").val().length;

        if (tamanho < 11) {
            $("#Cpf_cnpj").mask("999.999.999-99");
        } else if (tamanho >= 11) {
            $("#Cpf_cnpj").mask("99.999.999/9999-99");
        }

        // ajustando foco
        var elem = this;
        setTimeout(function () {
            // mudo a posição do seletor
            elem.selectionStart = elem.selectionEnd = 10000;
        }, 0);
        // reaplico o valor para mudar o foco
        var currentValue = $(this).val();
        $(this).val('');
        $(this).val(currentValue);
    });
});


function checkAll(ele, obj) {
    var checkboxes = document.getElementsByName(obj);

    if (ele.checked) {
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].type == 'checkbox') {
                checkboxes[i].checked = true;
            }
        }
    } else {
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].type == 'checkbox') {
                checkboxes[i].checked = false;
            }
        }
    }
}

function filtraLista(caixaTexto, lista) {
    var input, filter, ul, li, a, i;
    input = document.getElementById(caixaTexto);
    filter = input.value.toUpperCase();
    ul = document.getElementById(lista);
    li = ul.getElementsByTagName("li");
    for (i = 0; i < li.length; i++) {
        a = li[i].getElementsByTagName("a")[0];
        if (a.innerHTML.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }
}

function getEndereco(campo, valor, url, ddl) {

    if ((valor != "") && (valor != "0")) {
        var dataObj = {};
        dataObj[campo] = valor;

        $.ajax({
            url: url,
            data: dataObj,
            cache: false,
            type: "POST",
            success: function (data) {
                //var markup = "<option value=''>-- Selecione --</option>";
                for (var x = 0; x < data.length; x++) {
                    markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
                }
                $("#" + ddl + "").html(markup).show();
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
            }
        });
    } else {
        $("#" + ddl + "").empty();
        var markup = "<option value=''></option>";
        $("#" + ddl + "").html(markup).show();
    }
}


function MostraDataLembrete(checkbox) {

    var elem = document.getElementById("spanLembrete");

    if (checkbox) {
        elem.style.display = "block";
    } else {
        elem.style.display = "none";
    }
}

function ExibePorcentagem(val) {
    if (val > 0) {
        $("#divPorcentagem").attr("style", "display:block");
    } else {
        $("#divPorcentagem").attr("style", "display:none");
    }
    
}

function AtualizaPorcentagem(val) {
    $("#porcentagemLabel").text(val);
}

function BuscaPorcentagem(idConversao) {

    var dataObj = {};
    dataObj["idConversao"] = idConversao;

    if (idConversao != "")
    {
        $.ajax({
            //url: '/TAMIntegra/EntidadeTratativa/BuscaConversao/',
            url: '/EntidadeTratativa/BuscaConversao/',
            data: dataObj,
            cache: false,
            type: "POST",
            success: function (data) {
                if (data != null) {
                    //$("#porcentagemLabel").text(data.PorcentagemInicial);
                    $("#porcentagem").prop('value', data.PorcentagemInicial);
                    $("#porcentagem").prop('min', data.PorcentagemInicial);
                    $("#porcentagem").prop('max', data.PorcentagemFinal);
                    AtualizaPorcentagem(document.getElementById("porcentagem").value);
                }
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
                return false;
            }
        });
    }    
}

function BuscaMinMaxPorcentagem(idConversao) {

    var dataObj = {};
    dataObj["idConversao"] = idConversao;

    if (idConversao != "") {
        $.ajax({
            //url: '/TAMIntegra/EntidadeTratativa/BuscaConversao/',
            url: '/EntidadeTratativa/BuscaConversao/',
            data: dataObj,
            cache: false,
            type: "POST",
            success: function (data) {
                if (data != null) {
                    //$("#porcentagemLabel").text(data.PorcentagemInicial);
                    //$("#porcentagem").prop('value', data.PorcentagemInicial);
                    $("#porcentagem").prop('min', data.PorcentagemInicial);
                    $("#porcentagem").prop('max', data.PorcentagemFinal);
                    //AtualizaPorcentagem(document.getElementById("porcentagem").value);
                }
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
                return false;
            }
        });
    }
}

function validaEmail(nomeForm) {
    
    var dataObj = {};
    var dataRes = {};
    var msg = "";
    var id;
    var dllIdTipo;

    id = document.getElementById("Id").value;
    ddlIdTipo = document.getElementById("IdTipo").value;

    if (ddlIdTipo == 1) {
        if ((id == "") || (id == 0)) {
            dataObj["txtEmail"] = document.getElementById("Email").value;

            $.ajax({
                url: '/EntidadeEmail/Valida/',
                data: dataObj,
                cache: false,
                type: "POST",
                success: function (data) {
                    if (data.length > 0) {
                        msg += "As seguintes entidades possuem o mesmo e-mail informado: \n";
                        for (var x = 0; x < data.length; x++) {
                            msg += "- " + data[x].Entidade + "\n\n";
                        }
                    }

                    validaTelefone(nomeForm, msg);
                },
                error: function (xhr, status, error) {
                    alert(xhr.responseText);
                    return false;
                }
            });
        } else {
            $("#" + nomeForm + "").submit();
        }
    } else {
        $("#" + nomeForm + "").submit();
    }
}

function validaTelefone(nomeForm, msg) {

    var dataObj = {};
    var dataRes = {};
    //var msg;

    id = document.getElementById("Id").value;

    if ((id == "") || (id == 0)) {
        dataObj["txtDDD"] = document.getElementById("DDD").value;
        dataObj["txtTelefone"] = document.getElementById("Telefone").value;

        $.ajax({
            url: '/EntidadeTelefone/Valida/',
            data: dataObj,
            cache: false,
            type: "POST",
            success: function (data) {
                if (data.length > 0) {
                    msg += "As seguintes entidades possuem o mesmo telefone informado: \n";
                    for (var x = 0; x < data.length; x++) {
                        msg += "- " + data[x].Entidade + "\n\n";
                    }
                }

                validaEmailTelefone(msg, nomeForm);
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
                return false;
            }
        });
    }    
}

function validaEmailTelefone(msg, nomeForm) {
    if (msg == "") {
        $("#" + nomeForm + "").submit();
    } else {
        msg += "Deseja salvar mesmo assim?";

        if (confirm(msg)) {
            $("#" + nomeForm + "").submit();
        } else {
            return false;
        }
    }
}


$(document).ready(function () {
    $('.chosen-select').chosen({
        'placeholder_text_multiple': 'Selecione...',
        'placeholder_text_single': "-- Selecione --",
        'no_results_text': 'Nenhum resultado encontrado!',
        width : '95%',
    });
});


function BuscaChosen(ddlOrigem, ddlDestino, idsSelecionadosPost, url) {

    var ids = $("#" + ddlOrigem).val();
    var idsSelecionados = $("#" + ddlDestino).val();
    var dataObj = {};
    var ddl = ddlDestino;
    if (ids > "0") {
        
        dataObj[ddlOrigem] = ids;

        $.ajax({
            url: url,
            data: dataObj,
            cache: false,
            type: "POST",
            async: false,
            success: function (data) {

                $("#" + ddl).empty();

                $.each(data, function (index, val) {
                    $("#" + ddl).append('<option value="' + val.Value + '">' + val.Text + '</option>');
                });

                if (idsSelecionadosPost != null) {
                    $("#" + ddl).val(idsSelecionadosPost);
                } else {
                    $("#" + ddl).val(idsSelecionados);
                }
                
                $("#" + ddl).trigger("chosen:updated");
                $("#" + ddl).trigger("liszt:updated");
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
            }
        });
    } else {
        LimpaDDL(ddl);
    }
}

function LimpaDDL(ddl) {
    $("#" + ddl).empty();
    $("#" + ddl).trigger("chosen:updated");
}

function buscaRegistro(entidade, id) {

    var dataObj = {};
    var url = "";
    dataObj["id"] = id;

    switch (entidade) {
        case "EntidadeAeronaveTreinamentoStatus":
            url = "/EntidadeAeronaveTreinamentoStatus/BuscaPorId/";            
        break;
    }

    if (id != "") {
        $.ajax({
            url: url,
            data: dataObj,
            cache: false,
            type: "POST",
            success: function (data) {
                if (data != null) {
                    $("#id").val(data.Id);
                    $("#ano").val(data.Ano);
                    $("#idTreinamentoStatus").val(data.IdTreinamentoStatus);
                    $("#idTreinamentoConcorrente").val(data.IdTreinamentoConcorrente);
                    VerificaConcorrente(data.IdTreinamentoStatus);
                }
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
                return false;
            }
        });
    }
}

function VerificaConcorrente(idTreinamentoStatus) {

    var dataObj = {};
    dataObj["idTreinamentoStatus"] = idTreinamentoStatus;

    if (idTreinamentoStatus != "") {
        $.ajax({
            url: '/TreinamentoStatus/VerificaConcorrente/',
            data: dataObj,
            cache: false,
            type: "POST",
            success: function (data) {
                if (data != false) {
                    //AtualizaPorcentagem(document.getElementById("porcentagem").value);
                    $("#divConcorrente").attr("style", "display:block");
                } else {
                    $("#divConcorrente").attr("style", "display:none");
                }
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
                return false;
            }
        });
    }
}

function alteraStatus(idForm, objeto, id) {
    var dataObj = {};
    var url = "";
    dataObj["id"] = id;

    switch (objeto) {
        case "Entidade":
            url = 'Entidade/AlteraStatus';
        break;
    }

    if (id != "") {
        $.ajax({
            url: url,
            data: dataObj,
            cache: false,
            type: "POST",
            success: function (data) {
                if (data == true) {
                    $("#" + idForm + "").submit();
                } else {
                    alert("Erro na operação!");
                }
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
                return false;
            }
        });
    }
}


function AlteraTipoRelacao(entidade, idEntidade, idTipoRelacao, param3) {
    switch (entidade) {
        case "EntidadeAeronave":
            $("#txtAeronave").val(param3);            
        break;
        case "EntidadeEntidade":
            $("#txtEntidade").val(param3);
        break;
    }

    $("#idTipoRelacao").val(idTipoRelacao);
    $("#IdTipoRelacaoOld").val(idTipoRelacao);
    $('#salvaPerfil').val("Salvar edição");
}

function AlteraEntidadePrincipal(idEntidadePai, idEntidadeFilha, idTipoRelacao) {

    var dataObj = {};
    var url = "";
    dataObj["idEntidadePai"] = idEntidadePai;
    dataObj["idEntidadeFilha"] = idEntidadeFilha;
    dataObj["idTipoRelacao"] = idTipoRelacao;

    if (idEntidadePai != "") {

        alert("Aguarde...");

        $.ajax({
            url: '/EntidadeEntidade/AlteraEntidadePrincipal',
            data: dataObj,
            cache: false,
            type: "POST",
            success: function (data) {
                if (data == true) {
                    alert("Entidade principal alterada com sucesso!");
                } else {
                    alert("Erro na operação!");
                }
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
                return false;
            }
        });
    }

}