using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using System.Collections.Generic;
using Programax.Infraestrutura.Negocio.Utils;
using System.Linq;
using System.Text.RegularExpressions;
using NFe.Classes;

namespace Programax.Easy.Report.RelatoriosDevExpress.Fiscal
{
    public partial class RelatorioCartaCorrecao : RelatorioBaseDev
    {
        #region " VARIÁVEIS PRIVADAS "
        NFe.Classes.Servicos.Consulta.procEventoNFe _sdados;
        
        int _numeroNota;
        #endregion

        #region " CONSTRUTOR "

        public RelatorioCartaCorrecao(NFe.Classes.Servicos.Consulta.procEventoNFe sDados, int NumeroNota)
        {
            InitializeComponent();
            _sdados = sDados;
            _numeroNota = NumeroNota;
            CarregueDadosRelatorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            DadosCartaCorrecao Dados = new DadosCartaCorrecao();
            List<DadosCartaCorrecao> ListaDeDados = new List<DadosCartaCorrecao>();

            Dados.OrgaoRecepcao = "52-GOIÁS";
            Dados.Ambiente = _sdados.retEvento.infEvento.tpAmb.ToString();
            Dados.Versao = _sdados.evento.versao;
            Dados.ChaveAcesso = _sdados.retEvento.infEvento.chNFe;
            Dados.IdEvento = _sdados.evento.infEvento.Id;
            Dados.AutorEvento = _sdados.evento.infEvento.CNPJ !=null? _sdados.evento.infEvento.CNPJ : _sdados.evento.infEvento.CPF;
            Dados.DataEvento = _sdados.evento.infEvento.dhEvento.ToString();
            Dados.TipoEvento = _sdados.evento.infEvento.tpEvento.ToString() + " - " + _sdados.evento.infEvento.detEvento.descEvento;
            Dados.SequencialEvento = _sdados.evento.infEvento.nSeqEvento.ToString();
            Dados.DescricaoEvento = _sdados.evento.infEvento.detEvento.descEvento;
            Dados.VersaoEvento = _sdados.evento.versao;
            Dados.TextoCartaCorrecao = _sdados.evento.infEvento.detEvento.xCorrecao;
            Dados.MensagemAutorizacao = _sdados.retEvento.infEvento.cStat + " - " + _sdados.retEvento.infEvento.xMotivo;
            Dados.Protocolo = _sdados.retEvento.infEvento.nProt;
            Dados.DataAutorizacao = _sdados.retEvento.infEvento.dhRegEvento.ToString();
            Dados.NumeroNotaFiscal = _numeroNota.ToString();
            Dados.CondicoesUso = _sdados.evento.infEvento.detEvento.xCondUso;

            ListaDeDados.Add(Dados);

            ConteudoRelatorio.DataSource = ListaDeDados;
        }

        #endregion
        
        #region " CLASSES AUXILIARES "

        public class DadosCartaCorrecao
        {           
            public string OrgaoRecepcao { get; set; }

            public string Ambiente { get; set; }

            public string Versao { get; set; }

            public string ChaveAcesso { get; set; }

            public string NumeroNotaFiscal { get; set; }

            public string IdEvento { get; set; }

            public string AutorEvento { get; set; }
            
            public string DataEvento { get; set; }

            public string TipoEvento { get; set; }
            
            public string SequencialEvento { get; set; }

            public string DescricaoEvento { get; set; }

            public string VersaoEvento { get; set; }

            public string TextoCartaCorrecao { get; set; }

            public string MensagemAutorizacao { get; set; }

            public string Protocolo { get; set; }

            public string DataAutorizacao { get; set; }

            public string CondicoesUso { get; set; }

        }

        #endregion
    }
}
