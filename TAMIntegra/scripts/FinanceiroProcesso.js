
            $(document).ready(function () {
                allWells = $('#Security-Setup-step'),
                allWells2 = $('#VerifyEmail-step')
                //,allNextBtn = $('.nextBtn');

                allWells.hide();
                allWells2.hide();

                var menuAtivo = $('#menuAtivo').val();
                $('.menuFinanceiro .sidebar-submenu').toggle();
            });


$('.consultaPedido').click(function () {
    $(".divConsultaPedido").toggle();
    $(".divParametrosPedido").toggle();
});

$('a.modalBTN').click(function () {
    $('#my-modal').modal({
        show: true,
        closeOnEscape: true,
        backdrop: 'static',
        keyboard: false
    })
});




$('#opcoesImportacaoRegular *').attr("disabled", true);

$('#importacaoRegularInput').click(function () {
    //$('#opcoesImportacaoRegular').show();
    $('#opcoesImportacaoRegular *').attr("disabled", false);
});
$('#depositoEspecialInput').click(function () {
    //$('#opcoesImportacaoRegular').hide();
    $('#opcoesImportacaoRegular *').attr("disabled", true);
});



function populaModal(idIntegracao, numeroMov, situacao, aprovacao, status, data, valor, fornecedor, idMov, comprador) {
    $('#idIntegracao').html(idIntegracao);
    $('#numeroMovModal').html(numeroMov);
    $('#situacaoModal').html(situacao);
    $('#aprovacao').html(aprovacao);
    $('#status').html(status);
    $('#data').html(data);
    $('#valor').html(valor);
    $('#fornecedor').html(fornecedor);
    $('#comprador').text(comprador);
    $('#idMov').text(idMov);
    $('.fornecedor').html(fornecedor);
}

function salvarPedidoCompra() {

    var numeroPedido = $('#numeroMovModal').html();
    var dataInicio = $('#strDataInicio').val();
    var dataFim = $('#strDataFim').val();
    var idMov = $('#idMov').html();
    var status = $('#status').html();
    var situacao = $('#situacaoModal').html();
    var id_integracao = $('#idIntegracao').html();
    var origem = '';

    var de = document.getElementById('depositoEspecialInput');
    if (de.checked) {
        origem = 'DE';
    }

    var ir = document.getElementById('importacaoRegularInput');
    if (ir.checked) {
        origem = 'IR';
    }

    $.ajax({
        url: '@Url.Action("AgendarIntegracao", "Compra")',
        type: 'get',
        //data: { nroPedidoModal: numeroPedido, strDataInicio: dataInicio, strDataFim: dataFim, idMov: idMov, statusModal: status, situacaoModal: situacao}
        data: { idMov: idMov, id_integracao: id_integracao, origem: origem },
        success: function (data) {
            $("#myForm").submit();
        },
        error: function (xhr, textStatus, exceptionThrown) {
            //$('#btnagendar').attr("disabled", true);
            alert(exceptionThrown);
        },
    });
}

function Atualiza() {
    $('#myForm').submit();
}

$(function () {
    //$(".datepickerInput").datepicker({ dateFormat: 'dd/mm/yy', language: "pt-BR", locale: 'pt-br' }).val();
    //$(".datepickerInput").datepicker({ dateFormat: 'dd/mm/yy', language: "pt-BR", locale: 'pt-br' });
    $(".datepickerInput").datepicker({
        dateFormat: 'dd/mm/yy',
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        nextText: 'Próximo',
        prevText: 'Anterior'
    });

    $('#importacaoRegularInput').attr("disabled", true);
});

function isValidDate(date, campo) {
    if (date != "" || date != null)
    {
        var temp = date.split('/');
        var d = new Date(temp[1] + '/' + temp[0] + '/' + temp[2]);
        var result = (d && (d.getMonth() + 1) == temp[1] && d.getDate() == Number(temp[0]) && d.getFullYear() == Number(temp[2]));

        if (result == false) {
            $(campo).text("Data inválida.");
            $("#btnFiltrar").attr("disabled", true)
        }
        else {
            $("#btnFiltrar").attr("disabled", false)
            $(campo).text('');
        }
    }
    else
    {
        $(campo).text("Data inválida.");
        $("#btnFiltrar").attr("disabled", true)
    }

}

function desbloquearPedido(idIntegracao, idMov) {
    $.ajax({
        url: '@Url.Action("DesbloquearPedido", "Compra")',
        type: 'get',
        data: { idIntegracao: idIntegracao, idMov: idMov },
        //success: function (data) { $('#exampleModal').toggle(); }
        success: function (data) { $('#myForm').submit(); },
        error: function (xhr, textStatus, exceptionThrown) {
            //$('#btnagendar').attr("disabled", true);
            alert(exceptionThrown);
        }
    });
}

  
            function Informativo(id_integracao, sp_id, sp_id_despesa_processo)
            {
                $.ajax({
                    url: '@Url.Action("Informativo", "FinanceiroDespesas")',
                    //url: 'ImportacaoDocumento/agendarIntegracao?idMov=' + idMov,
                    //dataType: "json",
                    traditional: true,
                    //data: { idMov: idMov, campos: data2, id_fornecedor: fornecedor, movimento: movimento },
                    data: { id_integracao, sp_id, sp_id_despesa_processo},
                    contentType: 'json',

                    success: function (response) {
                        var credor = response[0].Credor;
                        var processo = response[0].Processo;
                        var cadastro = response[0].Cadastro;
                        var liberacao = response[0].Liberacao;
                        var vencimento = response[0].Vencimento;
                        var despesa = response[0].Despesa;
                        var documento = response[0].Documento;
                        var serie = response[0].Serie;
                        var moeda = response[0].Moeda;
                        var codDespesa = response[0].CodCredorDespesa;
                        var vrDespesa = response[0].VrPrevistoDespesa;
                        vrDespesa = vrDespesa != 0 ? parseFloat(vrDespesa).toLocaleString() : '';
                        var vrAdiantado = response[0].VrAdiantadoDespesa;
                        vrAdiantado = vrAdiantado != 0 ? parseFloat(vrAdiantado).toLocaleString() : '';
                        var vrRealDespesa = response[0].VrRealDespesa;
                        vrRealDespesa = vrRealDespesa != 0 ? parseFloat(vrRealDespesa).toLocaleString() : '';
                        var vrPagarDespesa = response[0].VrPagarDespesa;
                        vrPagarDespesa = vrPagarDespesa != 0 ? parseFloat(vrPagarDespesa).toLocaleString() : '';
                        var processoIntegracao = response[0].ProcessoIntegracao;
                        var id_int = response[0].Id_Integracao;
                        var id = response[0].Id;
                        var data = '';
                        data = response[0].Data;
                        var situacao = response[0].Situacao;
                        var tipo = response[0].Tipo;
                        var complemento = response[0].Complemento;
                        var idIntegracao = response[0].Id_Integracao;
                        var idInt = response[0].IdInt;
                        var codTMVInt = response[0].CodTMVInt;
                        var chaveOrigem = response[0].ChaveOrigem;
                        var identificador = response[0].Identificador;
                        var movimento = response[0].Movimento;
                        var numMovimento = response[0].NumMovimento;
                        var cor = response[0].Cor;
                        var consideracoes = response[0].Consideracoes;
                        var cor = response[0].Cor;
                        if (cor == 'B') {
                            $("#corConsideracoes").css("background-color", "white");
                        }
                        else if (cor == 'A') {
                            $("#corConsideracoes").css("background-color", "lightgoldenrodyellow");
                        }
                        else {
                            $("#corConsideracoes").css("background-color", "orangered");
                        }

                        $('#credor_Modal').html(credor);
                        $('#processo_Modal').html(processo);
                        $('#cadastro_Modal').html(cadastro);
                        $('#liberacao_Modal').html(liberacao);
                        $('#vencimento_Modal').html(vencimento);
                        $('#despesa_Modal').html(despesa);
                        $('#documento_Modal').html(documento);
                        $('#serie_Modal').html(serie);
                        $('#moeda_Modal').html(moeda);
                        $('#cod_Credor_Modal').html(codDespesa);
                        $('#vrDespesa_Modal').html(vrDespesa);
                        $('#vr_adiantado_despesa_Modal').html(vrAdiantado);
                        $('#valor_real_Modal').html(vrRealDespesa);
                        $('#valor_pagar_despesa_Modal').html(vrPagarDespesa);
                        $('#processo_integracao_Modal').html(processoIntegracao);
                        $('#integracao_Modal').html(id_int);
                        $('#id_Modal').html(id);
                        $('#data_Modal').html(data);
                        $('#situacao_Modal').html(situacao);
                        $('#tipo_Modal').html(tipo);
                        $('#complemento_Modal').html(complemento);
                        $('#idint_Modal').html(idInt);
                        $('#codtmvint_Modal').html(codTMVInt);
                        $('#chave_origem_int_Modal').html(chaveOrigem);
                        $('#identificador_Modal').html(identificador);
                        $('#numMovimento_Modal').html(numMovimento);
                        $('#movimento_Modal').html(movimento);
                        $('#consideracoes').html(consideracoes);


                        //$('#situacao_Modal').html(situacao);
                        //$('#data_Modal').html(data);
                        //$('#tipo_Modal').html(tipo);
                        //$('#forma_pagamento_Modal').html(forma_pag);
                        //$('#moeda_Modal').html(moeda);
                        //$('#valor_Modal').html(valor);
                        //$('#cotacao_Modal').html(cotacao);
                        //$('#valor_total_Modal').html(valor_total);
                        //$('#valor_total_reais_Modal').html(valor_total_reais);
                        //$('#cliente_Modal').html(cliente);
                        //$('#consideracoes_Modal').html(consideracoes);

                        $('#modalInformativo').modal({
                            show: true,
                            closeOnEscape: true,
                            backdrop: 'static',
                            keyboard: false
                        })


                    },
                    error: function (xhr, textStatus, exceptionThrown) {
                        //alert(exceptionThrown);
                    },
                });
            }

function Reprocessar(id_integracao, sp_id, sp_id_despesa_processo)
{
    $.ajax({
        url: '@Url.Action("Reprocessar", "FinanceiroDespesas")',
        //url: 'ImportacaoDocumento/agendarIntegracao?idMov=' + idMov,
        //dataType: "json",
        traditional: true,
        //data: { idMov: idMov, campos: data2, id_fornecedor: fornecedor, movimento: movimento },
        data: { id_integracao, sp_id: sp_id, sp_id_despesa_processo: sp_id_despesa_processo },
        contentType: 'json',

        success: function (response) {

            var comentario = response[0].Comentario;
            var item = response[0].Item;

            var event_data = '';
            var dataArray = response;

            if(dataArray.length == 1)
            {
                for (var i = 0; i < dataArray.length; i++){
                    var item = dataArray[i].Item;
                    var arrivalTime = dataArray[i].Comentario;

                    event_data += '<tr>';
                    event_data += '<td>'+dataArray[i].Comentario+'</td>';
                    event_data += '</tr>';
                }
            }
            else
            {
                for (var i = 0; i < dataArray.length; i++){
                    var item = dataArray[i].Item;
                    var arrivalTime = dataArray[i].Comentario;

                    event_data += '<tr>';
                    event_data += '<td>'+dataArray[i].Item+'. '+'</td>';
                    event_data += '<td>'+dataArray[i].Comentario+'</td>';
                    event_data += '</tr>';
                };
            }



            $('#id_fatura_rep').html(id_fatura);
            //$("#tblReprocessar").append(event_data);

            $("#tblReprocessar tr>td").remove();
            $('#tblReprocessar').find('tbody').append(event_data);


            $('#modalReprocessarItens').modal({
                show: true,
                closeOnEscape: true,
                backdrop: 'static',
                keyboard: false
            })
        },
        error: function (xhr, textStatus, exceptionThrown) {
            alert(exceptionThrown);
        },
    });
}

function abrirProcesso(codProcesso) {
    alert('ok');
    $('#modal_processo').html(codProcesso);
    //$("#tblFatura").find("tr:gt(0)").remove();
    $.ajax({
        url: '@Url.Action("abrirProcesso", "FinanceiroProcesso")',
        //url: 'ImportacaoDocumento/agendarIntegracao?idMov=' + idMov,
        //dataType: "json",
        traditional: true,
        data: { codProcesso: codProcesso },
        contentType: 'json',

        success: function (response) {
            for (var x = 0; x < response.ListDeclaracao.length; x++) {

                //$('#tblDeclaracao tbody').append('<tr></tr>');
                //$('#tblDeclaracao tbody').append('<td style="padding-right: 40px;" class="sticky-col first-col">' + response.ListDeclaracao[x].NF_COD_PROCESSO + '</td>');
                //$('#tblDeclaracao tbody').append('<td style="padding-right: 40px;" class="sticky-col second-col">' + response.ListDeclaracao[x].TIPO_DECLARACAO + '</td>');
                //$('#tblDeclaracao tbody').append('<td style="padding-right: 50px;" class="sticky-col third-col">' + response.ListDeclaracao[x].NF_NUMERO_DI + '</td>');
                //$('#tblDeclaracao tbody').append('<td style="padding-right: 40px;padding-left: 40px;width:100px">' + response.ListDeclaracao[x].NF_TAXA_DOLAR_REGISTRO_DI + '</td>');
                //$('#tblDeclaracao tbody').append('<td hidden>' + response.ListDeclaracao[x].NF_DATA_REGISTRO_DI + '</td>');
                //$('#tblDeclaracao tbody').append('<td hidden>' + response.ListDeclaracao[x].NF_NUM_CONHECIMENTO + '</td>');
                //$('#tblDeclaracao tbody').append('<td hidden>' + response.ListDeclaracao[x].NF_DATA_CONHECIMENTO + '</td>');
            }
            if (response.ListDeclaracao.length > 0)
            {
                let data_di_modal = new Date(parseInt(response.ListDeclaracao[0].NF_DATA_REGISTRO_DI.substr(6)));
                let ftmdata_di_modal = ("0" + data_di_modal.getDate()).slice(-2) + '/' +
                     ("0" + (data_di_modal.getMonth() + 1)).slice(-2) + "/" +
                    data_di_modal.getFullYear();

                let data_conhecimento_modal = new Date(parseInt(response.ListDeclaracao[0].NF_DATA_CONHECIMENTO.substr(6)));
                let ftmdata_conhecimento_modal = ("0" + data_conhecimento_modal.getDate()).slice(-2) + '/' +
                     ("0" + (data_conhecimento_modal.getMonth() + 1)).slice(-2) + "/" +
                    data_conhecimento_modal.getFullYear();

                let taxadi = response.ListDeclaracao[0].NF_TAXA_DOLAR_REGISTRO_DI;
                taxadi = taxadi != 0 ? parseFloat(taxadi).toLocaleString('pt-br', { minimumFractionDigits: 2 }) : '';

                $('#di_modal').html(response.ListDeclaracao[0].NF_NUMERO_DI);
                $('#data_di_modal').html(ftmdata_di_modal);
                $('#declaracao_modal').html(response.ListDeclaracao[0].TIPO_DECLARACAO);
                $('#dt_conhecimento_modal').html(ftmdata_conhecimento_modal);
                $('#conhecimento_modal').html(response.ListDeclaracao[0].NF_NUM_CONHECIMENTO);
                $('#taxa_di_modal').html(taxadi.toLocaleString('pt-br', { minimumFractionDigits: 2, maximumFractionDigits: 4 }));
            }
                            

            for (var x = 0; x < response.ListFatura.length; x++) {

                var vrPrevisto = response.ListFatura[x].FAT_TAXA_DI;
                vrPrevisto = vrPrevisto != 0 ? parseFloat(vrPrevisto).toLocaleString() : '';
                let date = new Date(parseInt(response.ListFatura[x].FAT_DATA_VENCIMENTO.substr(6)));
                let ftmVencimento = ("0" + date.getDate()).slice(-2) + '/' +
                     ("0" + (date.getMonth() + 1)).slice(-2) + "/" +
                    date.getFullYear();

                $('#tblFatura tbody').append('<tr>');

                if (x % 2 === 0)
                {
                    $('#tblFatura tbody').append('<td class="fat_importsys sticky-col first-col" style="background-color:#f2f2f2">' + response.ListFatura[x].ImportSys + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_recnucleus sticky-col second-col" style="background-color:#f2f2f2">' + response.ListFatura[x].REC_NUCLEUS + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_docfluxus sticky-col third-col" style="background-color:#f2f2f2">' + response.ListFatura[x].doc_fluxus + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_exportador"  style="padding-right: 40px;background-color:#f2f2f2">' + response.ListFatura[x].EXPORTADOR + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_num_fatura" style="background-color:#f2f2f2">' + response.ListFatura[x].FAT_NUM_FATURA + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_cod_moeda" style="text-align: center;background-color:#f2f2f2">' + response.ListFatura[x].FAT_COD_MOEDA + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_cond_pagto" style="text-align: left;padding-left:2px;background-color:#f2f2f2">' + response.ListFatura[x].FAT_COND_PAGTO + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_data_vencimento" style="text-align: center;background-color:#f2f2f2">' + ftmVencimento + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_taxa_di" style="padding-right:6px;text-align:center;background-color:#f2f2f2">' + response.ListFatura[x].FAT_TAXA_DI.toLocaleString('pt-br', { minimumFractionDigits: 2 }) + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_vmcv_total" style="padding-right:30px;text-align:right;background-color:#f2f2f2">' + response.ListFatura[x].FAT_VMCV_TOTAL.toLocaleString('pt-br', { minimumFractionDigits: 2 }) + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_data_fatura" style="text-align: left;background-color:#f2f2f2">' + response.ListFatura[x].STR_DATA_FATURA + '</td> <br>');
                    $('#tblFatura tbody').append('<td class="fat_conhecimento" style="text-align: left;background-color:#f2f2f2">' + response.ListFatura[x].CONHECIMENTO + '</td> <br>');
                    $('#tblFatura tbody').append('<td class="fat_tipo_conhecimento" style="text-align: left; padding-left:8px;background-color:#f2f2f2">' + response.ListFatura[x].TIPO_CONHECIMENTO + '</td> <br>');
                    $('#tblFatura tbody').append('<td class="fat_incoterm" style="text-align: left;padding-left:8px;background-color:#f2f2f2">' + response.ListFatura[x].FAT_INCOTERM + '</td> <br>');
                    $('#tblFatura tbody').append('<td hidden class="fat_id_integracao" style="background-color:#f2f2f2">' + response.ListFatura[x].ID_INTEGRACAO + '</td> <br>');
                    $('#tblFatura tbody').append('<td class="fat_situacao" style="text-align: left;padding-left:8px;background-color:#f2f2f2">' + response.ListFatura[x].SITUACAO + '</td> <br>');
                    $('#tblFatura tbody').append('<td class="fat_data" style="padding-left:8px;background-color:#f2f2f2">' + response.ListFatura[x].STR_DATA + '</td> <br>');
                }
                else
                {
                    $('#tblFatura tbody').append('<td class="fat_importsys sticky-col first-col">' + response.ListFatura[x].ImportSys + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_recnucleus sticky-col second-col">' + response.ListFatura[x].REC_NUCLEUS + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_docfluxus sticky-col third-col">' + response.ListFatura[x].doc_fluxus + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_exportador"  style="padding-right: 40px;">' + response.ListFatura[x].EXPORTADOR + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_num_fatura">' + response.ListFatura[x].FAT_NUM_FATURA + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_cod_moeda" style="text-align: center">' + response.ListFatura[x].FAT_COD_MOEDA + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_cond_pagto" style="text-align: left;padding-left:2px">' + response.ListFatura[x].FAT_COND_PAGTO + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_data_vencimento" style="text-align: center;">' + ftmVencimento + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_taxa_di" style="padding-right:6px;text-align:center">' + response.ListFatura[x].FAT_TAXA_DI.toLocaleString('pt-br', { minimumFractionDigits: 2 }) + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_vmcv_total" style="padding-right:30px;text-align:right">' + response.ListFatura[x].FAT_VMCV_TOTAL.toLocaleString('pt-br', { minimumFractionDigits: 2 }) + '</td>');
                    $('#tblFatura tbody').append('<td class="fat_data_fatura" style="text-align: left">' + response.ListFatura[x].STR_DATA_FATURA + '</td> <br>');
                    $('#tblFatura tbody').append('<td class="fat_conhecimento" style="text-align: left">' + response.ListFatura[x].CONHECIMENTO + '</td> <br>');
                    $('#tblFatura tbody').append('<td class="fat_tipo_conhecimento" style="text-align: left; padding-left:8px">' + response.ListFatura[x].TIPO_CONHECIMENTO + '</td> <br>');
                    $('#tblFatura tbody').append('<td class="fat_incoterm" style="text-align: left;padding-left:8px">' + response.ListFatura[x].FAT_INCOTERM + '</td> <br>');
                    $('#tblFatura tbody').append('<td hidden class="fat_id_integracao">' + response.ListFatura[x].ID_INTEGRACAO + '</td> <br>');
                    $('#tblFatura tbody').append('<td class="fat_situacao" style="text-align: left;padding-left:8px">' + response.ListFatura[x].SITUACAO + '</td> <br>');
                    $('#tblFatura tbody').append('<td class="fat_data" style="padding-left:8px">' + response.ListFatura[x].STR_DATA + '</td> <br>');
                }

                               
                                
                $('#tblFatura tbody').append('</tr>');

            }
                          
            for (var x = 0; x < response.ListImpostos.length; x++) {


                let date = new Date(parseInt(response.ListImpostos[x].VENCIMENTO.substr(6)));
                let ftmVencimento = ("0" + date.getDate()).slice(-2) + '/' +
                     ("0" + (date.getMonth() + 1)).slice(-2) + "/" +
                    date.getFullYear()

                let date2 = new Date(parseInt(response.ListImpostos[x].DATA_LIBERACAO.substr(6)));
                let fmtDataLiberacao = ("0" + date.getDate()).slice(-2) + '/' +
                     ("0" + (date.getMonth() + 1)).slice(-2) + "/" +
                    date.getFullYear();

                let date3 = new Date(parseInt(response.ListImpostos[x].DATA_CADASTRO.substr(6)));
                let fmtDataCadastro = ("0" + date.getDate()).slice(-2) + '/' +
                     ("0" + (date.getMonth() + 1)).slice(-2) + "/" +
                    date.getFullYear();

                let vrPrevisto = response.ListImpostos[x].VR_PREVISTO;
                vrPrevisto = vrPrevisto != 0 ? parseFloat(vrPrevisto).toLocaleString() : '';


                $('#tblImpostos tbody').append('<tr></tr>');
                if (x % 2 === 0)
                {
                    $('#tblImpostos tbody').append('<td class="imp_importsys sticky-col first-col" style="background-color:#f2f2f2">' + response.ListImpostos[x].ImportSys + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_recnucleus sticky-col second-col" style="background-color:#f2f2f2">' + response.ListImpostos[x].REC_NUCLEUS + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_docfluxus sticky-col third-col" style="background-color:#f2f2f2">' + response.ListImpostos[x].doc_fluxus + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_exportador"  style="padding-right: 40px;padding-left:6px;background-color:#f2f2f2">' + response.ListImpostos[x].EXPORTADOR + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_credor"  style="padding-right: 40px;text-align:center;padding-left:7px;background-color:#f2f2f2">' + response.ListImpostos[x].CREDOR + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_vencimento"  style="padding-right: 40px;padding-left: 10px;background-color:#f2f2f2">' + ftmVencimento + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_descricao"  style="padding-right: 40px;text-align:left;padding-left:8px;background-color:#f2f2f2">' + response.ListImpostos[x].DESCRICAO + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_moeda"  style="padding-right: 15px;padding-left:10px;background-color:#f2f2f2">' + response.ListImpostos[x].MOEDA + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_vr_previsto"  style="padding-right: 10px;text-align:right;background-color:#f2f2f2">' + parseFloat(vrPrevisto).toLocaleString('pt-br', { minimumFractionDigits: 2 }) + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_vr_adiantado"  style="padding-right: 5px;text-align:right;background-color:#f2f2f2">' + response.ListImpostos[x].VR_ADIANTADO.toLocaleString('pt-br', { minimumFractionDigits: 2 }) + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_vr_real"  style="padding-right: 2px;text-align:right;background-color:#f2f2f2">' + response.ListImpostos[x].VR_REAL.toLocaleString('pt-br', { minimumFractionDigits: 2 }) + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_vr_a_pagar"  style="padding-right: 2px;text-align:right;background-color:#f2f2f2">' + response.ListImpostos[x].VR_A_PAGAR.toLocaleString('pt-br', { minimumFractionDigits: 2 }) + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_sp_num_documento"  style="text-align:left;padding-left:8px;background-color:#f2f2f2">' + response.ListImpostos[x].SP_NUM_DOCUMENTO + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_data_cadastro"  style="text-align:left;padding-left:8px;background-color:#f2f2f2">' + fmtDataCadastro + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_data_liberacao"  style="text-align:left;padding-left:8px;background-color:#f2f2f2">' + fmtDataLiberacao + '</td>');
                    $('#tblImpostos tbody').append('<td style="text-align:left;padding-left:8px;background-color:#f2f2f2">' + response.ListImpostos[x].SITUACAO + '</td>');
                    $('#tblImpostos tbody').append('<td hidden class="imp_sp_id_integracao"  style="text-align:center;background-color:#f2f2f2">' + response.ListImpostos[x].ID_INTEGRACAO + '</td>');
                    $('#tblImpostos tbody').append('<td hidden class="imp_nucleus_id"  style="text-align:center;background-color:#f2f2f2">' + response.ListImpostos[x].NUCLEUS_ID + '</td>');
                    $('#tblImpostos tbody').append('<td hidden class="imp_fluxus_id"  style="text-align:center;background-color:#f2f2f2">' + response.ListImpostos[x].FLUXUS_ID + '</td>');
                }
                else
                {
                    $('#tblImpostos tbody').append('<td class="imp_importsys sticky-col first-col">' + response.ListImpostos[x].ImportSys + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_recnucleus sticky-col second-col">' + response.ListImpostos[x].REC_NUCLEUS + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_docfluxus sticky-col third-col">' + response.ListImpostos[x].doc_fluxus + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_exportador"  style="padding-right: 40px;padding-left:6px">' + response.ListImpostos[x].EXPORTADOR + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_credor"  style="padding-right: 40px;text-align:center;padding-left:7px">' + response.ListImpostos[x].CREDOR + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_vencimento"  style="padding-right: 40px;padding-left: 10px;">' + ftmVencimento + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_descricao"  style="padding-right: 40px;text-align:left;padding-left:8px">' + response.ListImpostos[x].DESCRICAO + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_moeda"  style="padding-right: 15px;padding-left:10px">' + response.ListImpostos[x].MOEDA + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_vr_previsto"  style="padding-right: 10px;text-align:right">' + parseFloat(vrPrevisto).toLocaleString('pt-br', { minimumFractionDigits: 2 }) + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_vr_adiantado"  style="padding-right: 5px;text-align:right">' + response.ListImpostos[x].VR_ADIANTADO.toLocaleString('pt-br', { minimumFractionDigits: 2 }) + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_vr_real"  style="padding-right: 2px;text-align:right">' + response.ListImpostos[x].VR_REAL.toLocaleString('pt-br', { minimumFractionDigits: 2 }) + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_vr_a_pagar"  style="padding-right: 2px;text-align:right">' + response.ListImpostos[x].VR_A_PAGAR.toLocaleString('pt-br', { minimumFractionDigits: 2 }) + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_sp_num_documento"  style="text-align:left;padding-left:8px">' + response.ListImpostos[x].SP_NUM_DOCUMENTO + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_data_cadastro"  style="text-align:left;padding-left:8px">' + fmtDataCadastro + '</td>');
                    $('#tblImpostos tbody').append('<td class="imp_data_liberacao"  style="text-align:left;padding-left:8px">' + fmtDataLiberacao + '</td>');
                    $('#tblImpostos tbody').append('<td style="text-align:left;padding-left:8px">' + response.ListImpostos[x].SITUACAO + '</td>');
                    $('#tblImpostos tbody').append('<td hidden class="imp_sp_id_integracao"  style="text-align:center">' + response.ListImpostos[x].ID_INTEGRACAO + '</td>');
                    $('#tblImpostos tbody').append('<td hidden class="imp_nucleus_id"  style="text-align:center">' + response.ListImpostos[x].NUCLEUS_ID + '</td>');
                    $('#tblImpostos tbody').append('<td hidden class="imp_fluxus_id"  style="text-align:center">' + response.ListImpostos[x].FLUXUS_ID + '</td>');
                }
                                

            }
     
            for (var x = 0; x < response.ListDespesas.length; x++) {


                let date = new Date(parseInt(response.ListDespesas[x].VENCIMENTO.substr(6)));
                let ftmVencimento = ("0" + date.getDate()).slice(-2) + '/' +
                     ("0" + (date.getMonth() + 1)).slice(-2) + "/" +
                    date.getFullYear()

                let date2 = new Date(parseInt(response.ListDespesas[x].DATA_LIBERACAO.substr(6)));
                let fmtDataLiberacao = ("0" + date.getDate()).slice(-2) + '/' +
                     ("0" + (date.getMonth() + 1)).slice(-2) + "/" +
                    date.getFullYear()

                let date3 = new Date(parseInt(response.ListDespesas[x].DATA_CADASTRO.substr(6)));
                let fmtDataCadastro = ("0" + date.getDate()).slice(-2) + '/' +
                     ("0" + (date.getMonth() + 1)).slice(-2) + "/" +
                    date.getFullYear();

                let vrPrevisto = response.ListDespesas[x].VR_PREVISTO;
                vrPrevisto = vrPrevisto != 0 ? parseFloat(vrPrevisto).toLocaleString() : '';

                $('#tblDespesas tbody').append('<tr></tr>');
                $('#tblDespesas tbody').append('<td class="imp_importsys sticky-col first-col">' + response.ListDespesas[x].ImportSys + '</td>');
                $('#tblDespesas tbody').append('<td class="imp_recnucleus sticky-col second-col">' + response.ListDespesas[x].REC_NUCLEUS + '</td>');
                $('#tblDespesas tbody').append('<td class="imp_docfluxus sticky-col third-col">' + response.ListDespesas[x].doc_fluxus + '</td>');
                $('#tblDespesas tbody').append('<td class="imp_exportador"  style="padding-right: 40px;padding-left:5px">' + response.ListDespesas[x].EXPORTADOR + '</td>');
                $('#tblDespesas tbody').append('<td class="imp_vencimento"  style="padding-right: 40px;">' + ftmVencimento + '</td>');
                $('#tblDespesas tbody').append('<td class="imp_descricao"  style="padding-right: 40px;">' + response.ListDespesas[x].DESCRICAO + '</td>');
                $('#tblDespesas tbody').append('<td class="imp_moeda"  style="text-align:center">' + response.ListDespesas[x].MOEDA + '</td>');
                $('#tblDespesas tbody').append('<td class="imp_vr_previsto"  style="text-align:right">' + parseFloat(vrPrevisto).toLocaleString('pt-br', { minimumFractionDigits: 2 }) + '</td>');
                $('#tblDespesas tbody').append('<td class="imp_vr_adiantado"  style="text-align:right">' + response.ListDespesas[x].VR_ADIANTADO.toLocaleString('pt-br', { minimumFractionDigits: 2 }) + '</td>');
                $('#tblDespesas tbody').append('<td class="imp_vr_real" style="text-align:right">' + response.ListDespesas[x].VR_REAL.toLocaleString('pt-br', { minimumFractionDigits: 2 }) + '</td>');
                $('#tblDespesas tbody').append('<td class="imp_vr_a_pagar" style="text-align:right;padding-right:15px">' + response.ListDespesas[x].VR_A_PAGAR.toLocaleString('pt-br', { minimumFractionDigits: 2 }) + '</td>');
                $('#tblDespesas tbody').append('<td class="imp_sp_num_documento"  style="padding-right: 40px;padding-left:28px">' + response.ListDespesas[x].SP_NUM_DOCUMENTO + '</td>');
                $('#tblDespesas tbody').append('<td class="imp_data_cadastro"  style="padding-right: 40px;">' + fmtDataCadastro + '</td>');
                $('#tblDespesas tbody').append('<td class="imp_data_liberacao"  style="padding-right: 40px;">' + fmtDataLiberacao + '</td>');
                $('#tblDespesas tbody').append('<td style="padding-right: 40px;">' + response.ListDespesas[x].SITUACAO + '</td>');
                $('#tblDespesas tbody').append('<td hidden class="imp_sp_id_integracao"  style="padding-right: 40px;">' + response.ListDespesas[x].ID_INTEGRACAO + '</td>');
                $('#tblDespesas tbody').append('<td hidden class="imp_nucleus_id"  style="padding-right: 40px;">' + response.ListDespesas[x].NUCLEUS_ID + '</td>');
                $('#tblDespesas tbody').append('<td hidden class="imp_fluxus_id"  style="padding-right: 40px;">' + response.ListDespesas[x].FLUXUS_ID + '</td>');

            }

            for (var x = 0; x < response.ListEstoque.length; x++) {
                let date = new Date(parseInt(response.ListDespesas[x].VENCIMENTO.substr(6)));
                let ftmVencimento = ("0" + date.getDate()).slice(-2) + '/' +
                     ("0" + (date.getMonth() + 1)).slice(-2) + "/" +
                    date.getFullYear()

                let date2 = new Date(parseInt(response.ListDespesas[x].DATA_LIBERACAO.substr(6)));
                let fmtDataLiberacao = ("0" + date.getDate()).slice(-2) + '/' +
                     ("0" + (date.getMonth() + 1)).slice(-2) + "/" +
                    date.getFullYear()

                let date3 = new Date(parseInt(response.ListDespesas[x].DATA_CADASTRO.substr(6)));
                let fmtDataCadastro = ("0" + date.getDate()).slice(-2) + '/' +
                     ("0" + (date.getMonth() + 1)).slice(-2) + "/" +
                    date.getFullYear();

                let vrPrevisto = response.ListDespesas[x].VR_PREVISTO;
                vrPrevisto = vrPrevisto != 0 ? parseFloat(vrPrevisto).toLocaleString() : '';

                $('#tblEstoque tbody').append('<tr></tr>');
                $('#tblEstoque tbody').append('<td style="padding-right: 40px;" class="sticky-col first-col">' + response.ListEstoque[x].BROKERSYS + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 40px;" class="sticky-col second-col">' + response.ListEstoque[x].EMISSAO + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 40px;" class="sticky-col third-col">' + response.ListEstoque[x].RECFISICO + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 20px;text-align: right;">' + response.ListEstoque[x].VR_FATURA.toLocaleString('pt-br', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 20px;text-align: right;">' + response.ListEstoque[x].TAXA_DI.toLocaleString('pt-br', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 20px;text-align: right;">' + response.ListEstoque[x].II.toLocaleString('pt-br', { minimumFractionDigits: 2, maximumFractionDigits: 4 }) + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 20px;text-align: right;">' + response.ListEstoque[x].COFINS.toLocaleString('pt-br', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 20px;text-align: right;">' + response.ListEstoque[x].IPI.toLocaleString('pt-br', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 20px;text-align: right;">' + response.ListEstoque[x].PIS.toLocaleString('pt-br', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 20px;text-align: right;">' + response.ListEstoque[x].FRETEINTER.toLocaleString('pt-br', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 20px;text-align: right;">' + response.ListEstoque[x].SEGURO.toLocaleString('pt-br', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 20px;text-align: right;">' + response.ListEstoque[x].VR_PRODUTO.toLocaleString('pt-br', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 20px;text-align: right;">' + response.ListEstoque[x].ICMS.toLocaleString('pt-br', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 20px;text-align: right;">' + response.ListEstoque[x].VR_NOTA.toLocaleString('pt-br', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 6px;text-align: right;">' + response.ListEstoque[x].DESPESA_COMPL.toLocaleString('pt-br', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 8px;text-align:right">' + response.ListEstoque[x].VR_REC_FISICO.toLocaleString('pt-br', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 40px;" hidden>' + response.ListEstoque[x].ID_INTEGRACAO + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 40px;padding-left:10px">' + response.ListEstoque[x].SITUACAO + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 40px;" hidden>' + response.ListEstoque[x].NF_ID + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 40px;" hidden>' + response.ListEstoque[x].IDMOV_EMISSAONF + '</td>');
                $('#tblEstoque tbody').append('<td style="padding-right: 40px;" hidden>' + response.ListEstoque[x].IDMOV_RECFISICO + '</td>');
            }

            for (var x = 0; x < response.ListInformeLACCTB.length; x++) {
                $('#tblEstoqueLACCTB tbody').append('<tr></tr>');
                if (x % 2 === 0)
                {
                    $('#tblEstoqueLACCTB tbody ').append('<td style="text-align: left;padding-left:6px;background-color:#f2f2f2">' + response.ListInformeLACCTB[x].DESPESA + '</td>');
                    $('#tblEstoqueLACCTB tbody').append('<td style="text-align: left;padding-left:6px;background-color:#f2f2f2">' + response.ListInformeLACCTB[x].DEBITO.toLocaleString('pt-br', { minimumFractionDigits: 2, maximumFractionDigits: 4 }) + '</td>');
                    $('#tblEstoqueLACCTB tbody').append('<td style="text-align: left;padding-left:6px;background-color:#f2f2f2">' + response.ListInformeLACCTB[x].CREDITO.toLocaleString('pt-br', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '</td>');
                }
                else
                {
                    $('#tblEstoqueLACCTB tbody ').append('<td style="text-align: left;padding-left:6px">' + response.ListInformeLACCTB[x].DESPESA + '</td>');
                    $('#tblEstoqueLACCTB tbody').append('<td style="text-align: left;padding-left:6px">' + response.ListInformeLACCTB[x].DEBITO.toLocaleString('pt-br', { minimumFractionDigits: 2, maximumFractionDigits: 4 }) + '</td>');
                    $('#tblEstoqueLACCTB tbody').append('<td style="text-align: left;padding-left:6px">' + response.ListInformeLACCTB[x].CREDITO.toLocaleString('pt-br', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '</td>');
                }
                                   
                                   
                                
                              
                              
            }

            for (var x = 0; x < response.ListHistorico.length; x++) {
                let date = new Date(parseInt(response.ListHistorico[x].DATA.substr(6)));
                let ftmData = ("0" + date.getDate()).slice(-2) + '/' +
                     ("0" + (date.getMonth() + 1)).slice(-2) + "/" +
                    date.getFullYear()

                $('#tblHistorico tbody').append('<tr></tr>');
                $('#tblHistorico tbody').append('<td style="padding-right: 40px;" class="sticky-col first-col">' + response.ListHistorico[x].REGISTRO + '</td>');
                $('#tblHistorico tbody').append('<td style="padding-right: 40px;" class="sticky-col second-col">' + response.ListHistorico[x].IMPORTSYS + '</td>');
                $('#tblHistorico tbody').append('<td style="padding-right: 50px;" class="sticky-col third-col">' + response.ListHistorico[x].DOCUMENTO + '</td>');
                $('#tblHistorico tbody').append('<td style="padding-right: 40px;padding-left: 40px;width:100px;text-align:right">' + response.ListHistorico[x].VALOR.toLocaleString('pt-br', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '</td>');
                $('#tblHistorico tbody').append('<td style="padding-right: 40px;">' + ftmData + '</td>');
                $('#tblHistorico tbody').append('<td hidden>' + response.ListHistorico[x].ID_INTEGRACAO + '</td>');
                $('#tblHistorico tbody').append('<td hidden>' + response.ListHistorico[x].ID_INT + '</td>');
                $('#tblHistorico tbody').append('<td hidden>' + response.ListHistorico[x].CODTMV + '</td>');
                $('#tblHistorico tbody').append('<td hidden>' + response.ListHistorico[x].CHAVEORIGEM_INT + '</td>');
            }

                            

            $('#modalProcesso').modal({
                show: true,
                closeOnEscape: true,
                backdrop: 'static',
                keyboard: false
            })
                         

        },
        error: function (xhr, textStatus, exceptionThrown) {
            //alert(exceptionThrown);
        },
    });
}
$('#modalProcesso').on('hidden.bs.modal', function () {
    $('#tbodyFatura').empty();
    $('#tbodyImpostos').empty();
    $('#tbodyDespesa').empty();
    $('#tbodyEstoque').empty();
    $('#tbodyEstoqueLACCTB').empty();
    $('#tbodyHistorico').empty();
    $('#tbodyDeclaracao').empty();
});
