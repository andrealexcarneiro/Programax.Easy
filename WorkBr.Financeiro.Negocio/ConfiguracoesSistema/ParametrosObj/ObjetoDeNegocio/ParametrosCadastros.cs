using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio
{
    public class ParametrosCadastros
    {
        public virtual bool PermiteCadastroParceiroComMesmoCpfCnpj { get; set; }

        public virtual bool PermiteCadastroParceiroSemCpfCnpj { get; set; }

        public virtual bool PermiteVendaDiretaNoPDV { get; set; }

        public virtual bool ValidaEndereco { get; set; }

        public virtual bool ValorVendaManual { get; set; }

        public virtual bool LiberarCampoQtde { get; set; }

        public virtual bool AbrirQuantEstoqueItens { get; set; }

        public virtual bool MostrarGrupoTribPesquisaItens { get; set; }

        public virtual int PrefixoEan13CodigoBarras { get; set; }

        public virtual int TamahoCodigoBarras { get; set; }

        public virtual string CaminhoACBR { get; set; }

        public virtual EnumTipoCodigoBarrasBalanca TipoCodigoBarrasBalanca { get; set; }

        public virtual EnumVinculoProdutoCodigoBarrasBalanca VinculoProdutoCodigoBarrasBalanca { get; set; }
    }
}
