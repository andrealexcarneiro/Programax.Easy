using System;
using System.Windows.Forms;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Financeiro.PerfilEmissaoBoletoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.PerfilEmissaoBoletoSev;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using System.Collections.Generic;

namespace Programax.Easy.View.Telas.Financeiro.Boletos
{
    public partial class FormAdicionarPerfilEmissaoBoletos : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private int _idPerfil;
        

        #endregion

        #region " CONSTRUTOR "

        public FormAdicionarPerfilEmissaoBoletos(int idPerfilParaAlterar=0, string descricaoParaAlterar="")
        {
            InitializeComponent();

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            _idPerfil = idPerfilParaAlterar != 0 ? idPerfilParaAlterar : _idPerfil;

            txtDescricao.Text = descricaoParaAlterar != "" ? descricaoParaAlterar : string.Empty;

            this.NomeDaTela = "Adicionar Perfil de Emissão de Boletos";

        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            PerfilEmissaoBoleto Perfil = new PerfilEmissaoBoleto();

            Perfil.Id = _idPerfil;
            Perfil.Descricao = txtDescricao.Text;
            Perfil.DataCadastro = txtDataCadastro.Text.ToDate();
            Perfil.PessoaId = Sessao.PessoaLogada.Id;
            
            Action actionSalvar = () =>
            {
                ServicoPerfilEmissaoBoleto servicoPerfil = new ServicoPerfilEmissaoBoleto();

                if (Perfil.Id == 0)
                {
                    servicoPerfil.Cadastre(Perfil);
                }
                else
                {
                    servicoPerfil.Atualize(Perfil);
                }
                
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }
       
        #endregion

        #region " MÉTODOS AUXILIARES "

        private void LimpeFormulario()
        {
            EditePerfil(null);
            
        }

        private void EditePerfil(PerfilEmissaoBoleto perfil)
        {
            if (perfil != null)
            {
                _idPerfil = perfil.Id;

                txtDescricao.Text = perfil.Descricao;
                txtDataCadastro.Text = perfil.DataCadastro.ToString("dd/MM/yyyy");
               
                txtDescricao.Focus();
            }
            else
            {
                _idPerfil = 0;
                txtDescricao.Text = string.Empty;
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            }
        }
        
        #endregion
    }
}
