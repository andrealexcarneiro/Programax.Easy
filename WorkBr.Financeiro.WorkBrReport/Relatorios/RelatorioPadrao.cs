using System.Collections.Generic;
using System.IO;
using Microsoft.Reporting.WinForms;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Report.Relatorios.BandasPadroes;

namespace Programax.Easy.Report.Relatorios
{
    public abstract class RelatorioPadrao<T>
    {
        protected string caminhoRelatorio;
        protected string nomeRelatorio;

        private LocalReport _localReport;

        public virtual LocalReport ConstruaRelatorio(LocalReport localReport, List<T> listaDeObjetos)
        {
            _localReport = localReport;

            string caminhoRelatorios = Directory.GetCurrentDirectory() + @"\Relatorios";

            string caminhoCompletoRelatorio = caminhoRelatorios + @"\" + caminhoRelatorio;

            localReport.ReportPath = caminhoCompletoRelatorio;

            localReport.SubreportProcessing += new SubreportProcessingEventHandler(RenderizaTopoRelatorio);

            localReport.SubreportProcessing += new SubreportProcessingEventHandler(RenderizaTitulo);

            InformeDataSets(localReport, listaDeObjetos);

            return localReport;
        }

        protected virtual void RenderizaTopoRelatorio(object sender, SubreportProcessingEventArgs e)
        {
            if (e.ReportPath == "../BandasPadroes/RelatorioComTopoPadrao")
            {
                var repositorio = FabricaDeRepositorios.Crie<IRepositorioEmpresa>();

                var empresa = repositorio.ConsulteUltimaEmpresa();

                e.DataSources.Add(new ReportDataSource("DadosEmpresa", new List<object> { empresa.DadosEmpresa }));
                e.DataSources.Add(new ReportDataSource("EnderecoEmpresaComEmail", new List<object> { empresa.DadosEmpresa.Endereco }));
                //e.DataSources.Add(new ReportDataSource("EnderecoEmpresa", new List<object> { empresa.DadosEmpresa.Endereco.DadosBasicos }));
                e.DataSources.Add(new ReportDataSource("CidadeEmpresa", new List<object> { empresa.DadosEmpresa.Endereco.Cidade }));
                e.DataSources.Add(new ReportDataSource("EstadoEmpresa", new List<object> { empresa.DadosEmpresa.Endereco.Cidade.Estado }));
            }
        }

        protected virtual void RenderizaTitulo(object sender, SubreportProcessingEventArgs e)
        {
            if (e.ReportPath == "../BandasPadroes/RelatorioTitulo")
            {
                TituloAuxiliar tituloAuxiliar = new TituloAuxiliar();
                tituloAuxiliar.Titulo = nomeRelatorio;

                e.DataSources.Add(new ReportDataSource("Titulo", new List<object> { tituloAuxiliar }));
            }
        }

        protected abstract void InformeDataSets(LocalReport localReport, List<T> listaDeObjetos);
    }
}
