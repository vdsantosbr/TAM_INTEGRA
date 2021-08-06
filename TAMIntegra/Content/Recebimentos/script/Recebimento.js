
var listimo =[];
var listCodTMV =[];
var  TotalNotas=  0;
function RecebeGrid(cliente)
{
    //alert(cliente);

    $(document).ready(function () {

        var val = cliente;
        var url = "/RecebimentoAvalaraParte_3/RecebeInformacoesTela_3";
        //alert(val + "novo ");
        $.post(url, { cnpj: val }, function (data) 
        {
            location='/Recebimento/'
        });

    });

}

function RecebeImod(idvanca, imov, tipopagina, codtmv){
   

  if(tipopagina>0){

    
   // let result = confirm("Deseja abrir um pedido de Remessa ?");
    //if (result) {

      
      listimo.push("1"); 
      listCodTMV.push("1");
      
      $(document).ready(function () {
        var url = "/RecebimentoAvalaraParte_3/Index/";       
        $.post(url, { IdMove: listimo, IdAvalara:idvanca , pedidoremessa:tipopagina, IdCodtmv: listCodTMV}, function (data) {
          //alert('ENTROU')
           location='/RecebimentoAvalaraPedidos/PedidoRecebido/?id=' + idvanca
        });
        
    });

    return;
   // }

  }
const index = listimo.indexOf(imov);
const indexcod = listCodTMV.indexOf(codtmv);

if(idvanca==0){

if(index>-1){
    if (index > -1) 
        listimo.splice(index, 1);    
}  
  else{
    listimo.push(imov);   
  }   
  
  /*
  if(indexcod>-1){
    if (indexcod > -1) 
    listCodTMV.splice(indexcod, 1);    
}  
  else{
    listCodTMV.push(codtmv);   
  }   
*/
  listCodTMV.push(codtmv);   
  //alert(listCodTMV);

}
else
{
    
    $(document).ready(function () {
        var url = "/RecebimentoAvalaraParte_3/Index/";       
        $.post(url, { IdMove: listimo, IdAvalara:idvanca , pedidoremessa:0 ,IdCodtmv: listCodTMV }, function (data) {
          //alert('ENTROU')
           location='/RecebimentoAvalaraPedidos/PedidoRecebido/?id=' + idvanca
        });

    });
}

}


function AbreModal(codigo, cnpj){  
  
    
    var url = "/RecebimentoAvalaraDetalhe/";       
    $.post(url, { numeronota: codigo, CNPJ: cnpj }, function (data) {
      
        $('#modaldetalhe').modal({
          show: 'false'
      }); 
        var iframe = document.getElementById('iframecomplete');
        //$('#iframecomplete').src="http://www.vanderlei.com.br";
       // alert("http://localhost:8080/RecebimentoAvalaraDetalhe/?numeronota=" + codigo)
        iframe.src="/RecebimentoAvalaraDetalhe/?numeronota=" + codigo + "&CNPJ=" + cnpj ;
        //iframe.src="http://www.vanderleisantos.com.br";
    });
  
  
}

function AbreModalPedido(codigo, Nota, CNPJ ){  
 
    
  var url = "/RecebimentoAvalaraDetalhe/DetalhePedido";       
  $.post(url, { IdMov: codigo }, function (data) {
    //alert('ENTROU')
      $('#modaldetalhepedido').modal({
        show: 'true'
    }); 
      var iframe = document.getElementById('iframecompleteNota');
      var iframeNOTA = document.getElementById('iframecompleteNotaCima');
      //$('#iframecomplete').src="http://www.vanderlei.com.br";      
      iframe.src="/RecebimentoAvalaraDetalhe/DetalhePedido/?IdMov=" + codigo ;
      iframeNOTA.src="/RecebimentoAvalaraDetalhe/?numeronota=" + Nota + "&CNPJ=" + CNPJ  ;
      //iframe.src="http://www.vanderleisantos.com.br";
  });
  
}

function removeItemOnce(arr, value) {
    var index = arr.indexOf(value);
    if (index > -1) {
      arr.splice(index, 1);
    }
    return arr;
  }


function redirecPagRemessa(idavalara, tipopagina)
{
  /*
    indice
    1, entreda
    2, remessa
    
  */

}

function BuscaNota()
{
  let descricaonota = $('#descricaonota').val();

  //alert(descricaonota);

  if(descricaonota.length <=1){
    alert('Por Favor digite ao menos 3 caracteres !')
    //return;
  }

  location='/RecebeInformacoesTela_2/Index?Descricaobusca=' + descricaonota

}



document.addEventListener("keypress", function(e) {
  if(e.key === 'Enter') {
  
      var btn = document.querySelector("#btnBusca");
    
    btn.click();
  
  }
});


function salvaranalise(cnpj){


    
    if ($("#tipomovimento option").filter(':selected').val() == "0") {
      alert('Escolha o tipo movimento');
      $("#tipomovimento option").focus();
      return;
   }

   if ($("#classefinanceira option").filter(':selected').val() == "0") {
    alert('Escolha a classificação financeira');
    $("#classefinanceira option").focus();
    return;
   }

    if ($("#formapagamento option").filter(':selected').val() == "0") {
      alert('Escolha a forma de pagamento');
      $("#formapagamento option").focus();
      return;
  }
  

      $(document).ready(function () {
        var url = "/RecebimentoAvalaraParte_3/SalvarAnalise/";       
        $.post(url, { cnpj: cnpj }, function (data) {        
            alert('Análise Salva com sucesso!!!');
            location='/RecebimentoAvalara/Index'
        });

    });
    

   // document.getElementById('btnsubmit').submit();
 
}

function desativaativa(codigo){

  if(localStorage.getItem('notafiscal')=="" || localStorage.getItem('notafiscal')==undefined)
  {
      alert('Selecione uma Nota para iniciar o processo de Vinculação com pedido !')
      return;
  }
  
  
  document.getElementById('itemCorrespondente-'+codigo).disabled  = false ;
  document.getElementById('itemCFOP-'+codigo).disabled  = false ;
  document.getElementById('pqtd-'+codigo).disabled  = false ;
  document.getElementById('pqtd-'+codigo).style.borderColor ="red";

  //getValues();

}

function iniciaprocesso (item, notafiscal, itemsnota, valorquantidade)
{

let montapedido = item+notafiscal;

//localStorage.removeItem('notafiscal');
//alert(valorquantidade)

if(localStorage.getItem('notafiscal') =="" || localStorage.getItem('notafiscal') == undefined)
{    
  localStorage.setItem('notafiscal',montapedido);
  localStorage.setItem('valorquantidade',valorquantidade)
  
}

//alert("storage " + localStorage.getItem('valorquantidade'));

if(localStorage.getItem('notafiscal')!= montapedido){
  localStorage.removeItem('notafiscal'); 
  localStorage.removeItem('ValorQuantidade'); 

 var result = confirm('Existe uma análise para Vinculo aberta\n Deseja selecionar este ITEM  '+ item +' ? ')
  if(result)
  {  
    localStorage.setItem('notafiscal',montapedido);
    localStorage.setItem('valorquantidade',valorquantidade);

   
  }



}


}

function validaquantidade(qtdapoio, codigo)
{

  
  let campo = document.getElementById('pqtd-'+codigo) 

  if(campo.value > qtdapoio){
    alert("Atenção !\nValor da quantidade não pode ser maior que Quantidade Pedido")
    document.getElementById('pqtd-'+codigo).value  = qtdapoio ;
    //campo.focus();
    return;
  }
  
}


function vinculadados(tiponotafiscal) {

  
    /*let result = confirm("Deseja vincular este pedido ?");
    if (result) {

      let totalpedidos = 0;*/


      if ($("#tipomovimento option").filter(':selected').val() == "0") {
        alert('Escolha o tipo movimento');
        //msg(tipoMsg.Sucesso, "Escolha o tipo movimento");
        $("#tipomovimento option").focus();
        return;
     }

     if ($("#classefinanceira option").filter(':selected').val() == "0") {
      alert('Escolha a classificação financeira');
      $("#classefinanceira option").focus();
      return;
     }

      if ($("#formapagamento option").filter(':selected').val() == "0") {
        alert('Escolha a forma de pagamento');
        $("#formapagamento option").focus();
        return;
    }

    if(tiponotafiscal==1)
    {
      if(localStorage.getItem('notafiscal')==undefined || localStorage.getItem('notafiscal')== ""){
        alert('Não há itens vinculados a NF');
        return;
      }
  
  
  
      //validando quantidade enviada
      let somaquantidade = 0;
      let chepedidos = document.querySelectorAll('[name=checkpedidos]:checked');
  
      if(chepedidos.length==0)
      {
        alert('Não há item selecionado para vinculo')
        return
      }
  
      let values = [];
      for (var i = 0; i < chepedidos.length; i++) {
        // utilize o valor aqui, adicionei ao array para exemplo
        somaquantidade +=  parseFloat(document.getElementById('pqtd-'+chepedidos[i].value).value);


        if ($("#itemCorrespondente-"+chepedidos[i].value+" option").filter(':selected').val() == "0") {
          alert('Preencher Item Correspondente do item selecionado');
          $("#itemCorrespondente-"+chepedidos[i].value+" option").focus();
          return;

         }
        if ($("#itemCFOP-"+chepedidos[i].value+" option").filter(':selected').val() == "0") {
          alert('Preencher CFOP do item selecionado');
          $("#itemCFOP-"+chepedidos[i].value+" option").focus();
          return;
         }
      } 
      
      if(parseFloat(somaquantidade)!=parseFloat(localStorage.getItem('valorquantidade'))){  
        alert('A quantidade selecionada para recebimento ( ' + parseFloat(somaquantidade.toFixed(4)) + ' )  naã corresponde com a quantidade  ( ' + localStorage.getItem('valorquantidade') + ' )  a ser recebida. \n\nPor favor refaça análise.' )
        return;
  
      }

    }

    else
    {

    

      let checados = document.querySelectorAll('[name=checkncm]:checked');
    

      let values = [];
      for (var i = 0; i < checados.length; i++) {
        
       

         if ($("#itemRM-"+checados[i].value+" option").filter(':selected').val() == "0") {
          alert('Selecione item RM do item selecionado');
          $("#m-"+checados[i].value+" option").focus();
          return;
         }

         if ($("#itemCFOP-"+checados[i].value+" option").filter(':selected').val() == "0") {
          alert('Preencher CFOP do item selecionado');
          $("#itemCFOP-"+checados[i].value+" option").focus();
          return;
         }


      } 


    }

    

      alert('Pedido Vinculado com sucesso!');
      document.getElementById('btnsubmit').submit();


    }
//}


function getValues() {
  var pacote = document.querySelectorAll('[name=checkpedidos]:checked');
  var values = [];
  for (var i = 0; i < pacote.length; i++) {
    
    values.push(pacote[i].value);
  }
 // alert(values);
}

// adicionar ação ao clique no checkbox


function EnviarPedidoAnalisado( idavalara ){
  
  var url = "/RecebimentoAvalaraPedidos/PedidoAnalisado/";       
  $.post(url, { IdAvalara: idavalara }, function (data) {        
      //alert('Análise Salva com sucesso!!!');
      location='/RecebimentoAvalaraPedidos/PedidoAnalisadoSessao'
  });


}

function ReiniciarAnalise(idavalara, notafiscal)
{
  let result  = confirm("Esta acao ira desfazer os vinculos de recebimento desta nota e reclassifica la como NF SEM VINCULO DE RECEBIMENTO.\n\n DESEJA CONTINUAR ?");

  if(result)
  {

    $(document).ready(function () {
      var url = "/RecebeInformacoesTela_2/DesvincularAnalise/";       
      $.post(url, { id: idavalara, NotaFiscal:notafiscal }, function (data) {        
         // alert('Análise Salva com sucesso!!!');
          location='/RecebeInformacoesTela_2/Index/?DescricaoBusca=' + notafiscal
      });

  });  

  }

}

function Chamaloading(){

   var url = "/AtualiaAvalara/RetornaChavesNFePelaDataEntradaXml/";       
  $.post(url,  function (data) {        
      //alert('Download de notas realizado com sucesso!');            

      GetTotais();
  });

}



function GetTotais() {

  $('#inicioDownload').html("<h3> <b>Download Iniciado...</b></h3>")

  
  

  $.ajax({
    url: "/RecebimentoAvalara/RetornaContadores",
    type: "GET",
    dataType: "json",
    dataSrc: "",

    success: function (data) {  
      
      const obj = JSON.parse(data)

      //console.log(obj.length)              
      console.log(obj.retChavesNfe.Quantidade)        
      console.log(obj)          
      console.log(obj.retorno)   
      TotalNotas= obj.retChavesNfe.Quantidade;

     
       $('#valortotal').html("<h3> <b>Total de Notas : 0 de  " + TotalNotas + " </b></h3>")
       $('#valortotalcontada').html(TotalNotas)

      let contador = 0;
      for (let index = 0; index <  obj.retChavesNfe.retorno.ChaveNFe.length; index++) {

      const element = obj.retChavesNfe.retorno.ChaveNFe[index];                     
        
      var url = "/AtualiaAvalara/DownloadNFe/";       
        $.post(url, { numeronfe: element}, function (data) {     
        
          $("#progress").css("width", (contador/TotalNotas * 100) +"%");
          console.log(contador);         



          contador ++;

          $('#valortotal').html("<h3> <b>Total de Notas : " + contador + "   de  " + TotalNotas + " </b></h3>")

          
          if(TotalNotas==contador+1 )
          $('#inicioDownload').html("<h3><b> Download Finalizado...</b></h3>")          


      });     

      }    
              

        
    }

    
      


    
});


}
