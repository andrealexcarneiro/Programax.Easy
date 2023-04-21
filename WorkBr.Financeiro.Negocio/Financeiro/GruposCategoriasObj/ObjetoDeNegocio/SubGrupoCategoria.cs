﻿using System;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio
{
    [Serializable]
    public class SubGrupoCategoria : ObjetoDeNegocioBase
    {
        public virtual string Descricao { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual string Ativo { get; set; }

        public virtual GrupoCategoria Grupo { get; set; }
    }
}
