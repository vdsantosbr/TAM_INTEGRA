﻿using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Data
{
    public class CompraAprovacaoDAL
    {
        private DatabaseContext db = new DatabaseContext();
        public List<CompraAprovacao> Lista()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return db.Database.SqlQuery<CompraAprovacao>("EXEC STO_S_RM_COMPRADOR").ToList();
            }
        }

        public List<CompraAprovacao> PedidoCompraLista(DateTime? data_Inicio, DateTime? data_Termino, string codCFO, string codVen, string codTmv, string numeroMov, int idPerfil,
            string formulario, string situacao, int id_integracao, int idMov)
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                SqlParameter pDataInicio = new SqlParameter("@data_Inicio", (data_Inicio == null) ? (object)DBNull.Value : data_Inicio);
                SqlParameter pDataTermino = new SqlParameter("@data_Termino", (data_Termino == null) ? (object)DBNull.Value : data_Termino);
                SqlParameter pCodCFO = new SqlParameter("@codCFO", (codCFO == null) ? "" : codCFO);
                SqlParameter pCodVen = new SqlParameter("@codVen", (codVen == null) ? "" : codVen);
                SqlParameter pCodTmv = new SqlParameter("@codTmv", (codTmv == null) ? "" : codTmv);
                SqlParameter pNumeroMov = new SqlParameter("@numeroMov", (numeroMov == null) ? "" : numeroMov);
                SqlParameter pIdPerfil = new SqlParameter("@id_Perfil", (idPerfil == 0) ? (object)DBNull.Value : idPerfil);
                SqlParameter pFormulario = new SqlParameter("@formulario", (formulario == null) ? (object)DBNull.Value : formulario);
                SqlParameter pSituacao = new SqlParameter("@situacao", (situacao == null) ? "" : situacao);
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? 0 : id_integracao);
                SqlParameter pIdMov = new SqlParameter("@idMov", (idMov == 0) ? 0 : idMov);
                //db.Database.CommandTimeout = 120;


                try
                {
                    var linha = db.Database.SqlQuery<CompraAprovacao>("STO_S_RM_PEDIDO_COMPRA @data_Inicio, @data_Termino, @codCFO, @codVen, @codTmv, @numeroMov, @id_Perfil,@formulario,@situacao,@id_integracao,@idMov", pDataInicio, pDataTermino, pCodCFO, pCodVen, pCodTmv, pNumeroMov, pIdPerfil, pFormulario, pSituacao, pIdIntegracao, pIdMov).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<CompraAprovacao>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return new List<CompraAprovacao>();
                }
                //var linha = db.Database.SqlQuery<Comprador>("exec STO_S_RM_PEDIDO_COMPRA NULL,  NULL,  NULL,  NULL,  NULL,  NULL,  NULL,  NULL,  NULL").ToList();

                //if (linha.Count > 0)
                //{
                //    return linha;
                //}
                //else
                //{
                //    return null;
                //}
            }
        }

        public List<CompraAprovacao> TipoAprovacao()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<CompraAprovacao>("STO_S_LIBERACAOPO_Mvto").ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<CompraAprovacao>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return new List<CompraAprovacao>();
                }
            }
        }

        public List<CompraAprovacao> Pedido(string tipo_movimento = "", string pedido = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                SqlParameter pTipoMovimento = new SqlParameter("@CODTMV", (tipo_movimento == null) ? (object)DBNull.Value : tipo_movimento);
                SqlParameter pPedido = new SqlParameter("@NUMEROMOV", (pedido == null) ? (object)DBNull.Value : pedido);


                try
                {
                    var linha = db.Database.SqlQuery<CompraAprovacao>("STO_S_LIBERACAOPO_Pedido @CODTMV, @NUMEROMOV", pTipoMovimento, pPedido).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<CompraAprovacao>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return new List<CompraAprovacao>();
                }
            }
        }
        public List<CompraAprovacao> Item(string tipo_movimento = "", string pedido = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                SqlParameter pTipoMovimento = new SqlParameter("@CODTMV", (tipo_movimento == null) ? (object)DBNull.Value : tipo_movimento);
                SqlParameter pPedido = new SqlParameter("@NUMEROMOV", (pedido == null) ? (object)DBNull.Value : pedido);


                try
                {
                    var linha = db.Database.SqlQuery<CompraAprovacao>("STO_S_LIBERACAOPO_Item @CODTMV, @NUMEROMOV", pTipoMovimento, pPedido).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<CompraAprovacao>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return new List<CompraAprovacao>();
                }
            }
        }
        public List<CompraAprovacao> Aprovacao(string tipo_movimento = "", string pedido = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                SqlParameter pTipoMovimento = new SqlParameter("@CODTMV", (tipo_movimento == null) ? (object)DBNull.Value : tipo_movimento);
                SqlParameter pPedido = new SqlParameter("@NUMEROMOV", (pedido == null) ? (object)DBNull.Value : pedido);


                try
                {
                    var linha = db.Database.SqlQuery<CompraAprovacao>("STO_S_LIBERACAOPO_Aprovacao @CODTMV, @NUMEROMOV", pTipoMovimento, pPedido).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<CompraAprovacao>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return new List<CompraAprovacao>();
                }
            }
        }
        public List<CompraAprovacao> Justificativa(string tipo_movimento = "", string pedido = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                SqlParameter pTipoMovimento = new SqlParameter("@CODTMV", (tipo_movimento == null) ? (object)DBNull.Value : tipo_movimento);
                SqlParameter pPedido = new SqlParameter("@NUMEROMOV", (pedido == null) ? (object)DBNull.Value : pedido);


                try
                {
                    var linha = db.Database.SqlQuery<CompraAprovacao>("STO_S_LIBERACAOPO_Validacao @CODTMV, @NUMEROMOV", pTipoMovimento, pPedido).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<CompraAprovacao>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return new List<CompraAprovacao>();
                }
            }
        }
        public Integracao CancelarPedido(int idMov = 0, int idPessoa = 0, string justificativa = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pJustificativa = new SqlParameter("@JUSTIFICATIVA", justificativa);
                SqlParameter pIdMov = new SqlParameter("@idMov", idMov);
                SqlParameter pIdPessoa = new SqlParameter("@id_Pessoa", idPessoa);

                var linha = db.Database.SqlQuery<Integracao>("EXEC STO_U_LIBERACAOPO_Cancelar @idMov, @id_Pessoa, @Justificativa",
                    pIdMov, pIdPessoa, pJustificativa).ToList();

                if (linha.Count > 0)
                {
                    return linha.First();
                }
                else
                {
                    return null;
                }
            }
        }
        public Integracao EditarPedido(int idMov = 0, int idPessoa = 0, int motivo = 0, string justificativa = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pJustificativa = new SqlParameter("@JUSTIFICATIVA", justificativa);
                SqlParameter pIdMov = new SqlParameter("@idMov", idMov);
                SqlParameter pIdPessoa = new SqlParameter("@id_Pessoa", idPessoa);
                SqlParameter pMotivo = new SqlParameter("@idLiberacaoPOMotivo", motivo);

                try
                {
                    var linha = db.Database.SqlQuery<Integracao>("EXEC STO_I_LIBERACAOPO_Editar @idMov, @id_Pessoa, @idLiberacaoPOMotivo, @Justificativa",
                    pIdMov, pIdPessoa, pMotivo, pJustificativa).ToList();

                    if (linha.Count > 0)
                    {
                        return linha.First();
                    }
                    else
                    {
                        return null;
                    }

                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
        public Integracao LiberarPedido(int idLiberacaoPO = 0, int motivo = 0, int idMov = 0, int idPessoa = 0, string justificativa = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pJustificativa = new SqlParameter("@JUSTIFICATIVA", justificativa == "" ? "": justificativa);
                SqlParameter pIdMov = new SqlParameter("@idMov", idMov);
                SqlParameter pIdPessoa = new SqlParameter("@id_Pessoa", idPessoa);
                SqlParameter pMotivo = new SqlParameter("@idLiberacaoPOMotivo", motivo);
                SqlParameter pidLiberacaoPO = new SqlParameter("@idLiberacaoPO", idLiberacaoPO);


                var linha = db.Database.SqlQuery<Integracao>("EXEC STO_U_LIBERACAOPO_Liberar @idLiberacaoPO, @idLiberacaoPOMotivo, @idMov, @id_Pessoa, @Justificativa",
                    pidLiberacaoPO, pMotivo, pIdMov, pIdPessoa, pJustificativa).ToList();

                if (linha.Count > 0)
                {
                    return linha.First();
                }
                else
                {
                    return null;
                }
            }
        }
        public List<Motivo> Motivo()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return db.Database.SqlQuery<Motivo>("EXEC STO_S_LIBERACAOPO_Motivo").ToList();
            }
        }

    }
}
