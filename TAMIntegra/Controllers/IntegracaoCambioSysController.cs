using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMIntegra.App_Start;
using Entities;
using Business;
using System.Globalization;
using System.IO;
using ClosedXML.Excel;
using System.Data;

namespace TAMINTEGRA.Controllers
{
    public class IntegracaoCambioSysController : Controller
    {
        private IntegracaoCambioSys intCambio = new IntegracaoCambioSys();

        [CustomAuthorize(Roles = "frmContabilizacaoVC")]
        public ActionResult Index()
        {
            List<IntegracaoCambioSys> lstGRid = new List<IntegracaoCambioSys>();
            List<IntegracaoCambioSys> lstRegistros = new List<IntegracaoCambioSys>();

            lstGRid.Add(new IntegracaoCambioSys
            {
                Ano = 2020,
                Mes = 8,
                Fechamento = "Agosto/2020",
                Dt_contabilizacao = new DateTime(2020, 08, 31),
                Situacao = "Aberto",
                Responsavel = "Daniela Torres"
            });

            lstRegistros.Add(new IntegracaoCambioSys
            {
                Id_integracao = 7755,
                Data = new DateTime(2020, 09, 03),
                Nome = "CI - OUT - TXT - Interface de Contabilização",
                Qtd_registros = 1656
            });

            lstRegistros.Add(new IntegracaoCambioSys
            {
                Id_integracao = 4759,
                Data = new DateTime(2020, 09, 04),
                Nome = "CI - OUT - TXT - Interface de Contabilização",
                Qtd_registros = 1302
            });

            intCambio.lstGrid = lstGRid;
           intCambio.lstRegistroIntegracao = lstRegistros;

            return View(intCambio);
        }
    }
}