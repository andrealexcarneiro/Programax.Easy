using Programax.Easy.Negocio.Financeiro.ConfiguracaoBoletoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ConfiguracaoBoletoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using System.Collections.Generic;

namespace Programax.Easy.Servico.Financeiro.ConfiguracaoBoletoServ
{
    public class ConversorConfiguracaoBoleto : ConversorDeObjetoBasico<ConfiguracaoBoleto>, IConversorDeObjeto<ConfiguracaoBoleto>
    {
        public ConfiguracaoBoleto CopieObjetoParaPersistencia(ConfiguracaoBoleto objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioConfiguracaoBoleto>();

            var perfilDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new ConfiguracaoBoleto();

            if (objetoDeNegocio == perfilDaBase)
            {
                return perfilDaBase;
            }

            var listaInstrucoesBoleto = CopieListaInstrucoesBoleto(objetoDeNegocio, perfilDaBase);

            CopieTodasAsPropriedades(objetoDeNegocio, perfilDaBase);

            perfilDaBase.ListaInstrucoes = listaInstrucoesBoleto;

            return perfilDaBase;
        }

        private IList <InstrucoesBoleto> CopieListaInstrucoesBoleto(ConfiguracaoBoleto objetoDeNegocio, ConfiguracaoBoleto perfilDaBase)
        {   
            var listaInstrucoesBoleto = perfilDaBase.ListaInstrucoes;

            listaInstrucoesBoleto.Clear();
            
            foreach (var item in objetoDeNegocio.ListaInstrucoes)
            {
                var itemCopiado = new InstrucoesBoleto();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.ConfiguracaoBoleto = perfilDaBase;
                listaInstrucoesBoleto.Add(itemCopiado);
            }

            return listaInstrucoesBoleto;
        }

    }
}
