using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Infraestrutura.Negocio.Validacoes;
using Programax.Easy.View.Componentes;
using Programax.Easy.Report.RelatoriosDevExpress;
using DevExpress.XtraReports.UI;
using DevExpress.LookAndFeel;

namespace Programax.Easy.View.ClassesAuxiliares
{
    public static class TratamentosDeTela
    {
        private static Control _controleValidar;

        public static void TrateInclusaoEAtualizacao(Action actionSalvar, string mensagemDeSucesso = "", string tituloMensagemDeSucesso = "", string tituloMensagemDeErro = "", bool exibirMensagemDeSucesso = true, Control controleValidar = null)
        {
            try
            {
                _controleValidar = controleValidar;

                ValideCamposObrigatorios();

                actionSalvar();

                if (exibirMensagemDeSucesso)
                {
                    if (string.IsNullOrEmpty(mensagemDeSucesso))
                    {
                        mensagemDeSucesso = "Cadastro salvo com sucesso!";
                    }

                    if (string.IsNullOrEmpty(tituloMensagemDeSucesso))
                    {
                        tituloMensagemDeSucesso = "Cadastro salvo.";
                    }

                    MessageBox.Show(mensagemDeSucesso, tituloMensagemDeSucesso, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (ExceptionInconsistencias ex)
            {
                StringBuilder textoComTodasAsInconsistencias = new StringBuilder("Foram encontradas as seguintes inconsistências:");

                foreach (var inconsistencia in ex.Inconsistencias.ListaDeInconsistencias)
                {
                    textoComTodasAsInconsistencias.Append("\n\n");

                    textoComTodasAsInconsistencias.Append(inconsistencia.Mensagem);
                }

                if (string.IsNullOrEmpty(tituloMensagemDeErro))
                {
                    tituloMensagemDeErro = "Inconsistências ao salvar item!";
                }

                MessageBox.Show(textoComTodasAsInconsistencias.ToString(), tituloMensagemDeErro, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro inesperado - " + ex.Message);
            }
        }

        public static void TrateInclusaoEAtualizacao(Action actionSalvar, Form form, bool fecharFormAoConcluirOperacao = false, string mensagemDeSucesso = "")
        {
            try
            {
                actionSalvar();

                if (string.IsNullOrEmpty(mensagemDeSucesso))
                {
                    mensagemDeSucesso = "Cadastro salvo com sucesso!";
                }

                MessageBox.Show(mensagemDeSucesso, "Cadastro salvo.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                if (fecharFormAoConcluirOperacao)
                {
                    form.Close();
                }
            }
            catch (ExceptionInconsistencias ex)
            {
                StringBuilder textoComTodasAsInconsistencias = new StringBuilder("Foram encontradas as seguintes inconsistências:");

                foreach (var inconsistencia in ex.Inconsistencias.ListaDeInconsistencias)
                {
                    textoComTodasAsInconsistencias.Append("\n\n");

                    textoComTodasAsInconsistencias.Append(inconsistencia.Mensagem);
                }

                MessageBox.Show(textoComTodasAsInconsistencias.ToString(), "Inconsistências ao salvar item!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro inesperado - " + ex.Message);
            }
        }

        public static void TrateExclusao(Action actionExclusao)
        {
            try
            {
                if (MessageBox.Show("Deseja excluir item?", "Deseja excluir?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    actionExclusao();

                    MessageBox.Show("Item excluído com sucesso!", "Exclusão com sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (ExceptionInconsistencias ex)
            {
                StringBuilder textoComTodasAsInconsistencias = new StringBuilder("Foram encontradas as seguintes inconsistências:");

                foreach (var inconsistencia in ex.Inconsistencias.ListaDeInconsistencias)
                {
                    textoComTodasAsInconsistencias.Append("\n\n");

                    textoComTodasAsInconsistencias.Append(inconsistencia.Mensagem);
                }

                MessageBox.Show(textoComTodasAsInconsistencias.ToString(), "Inconsistências ao excluir item!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro inesperado - " + ex.Message);
            }
        }

        public static void TrateExclusao(Action actionExclusao, Form form)
        {
            try
            {
                if (MessageBox.Show("Deseja excluir item?", "Deseja excluir?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    actionExclusao();

                    MessageBox.Show("Item excluído com sucesso!", "Exclusão com sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    form.Close();
                }
            }
            catch (ExceptionInconsistencias ex)
            {
                StringBuilder textoComTodasAsInconsistencias = new StringBuilder("Foram encontradas as seguintes inconsistências:");

                foreach (var inconsistencia in ex.Inconsistencias.ListaDeInconsistencias)
                {
                    textoComTodasAsInconsistencias.Append("\n\n");

                    textoComTodasAsInconsistencias.Append(inconsistencia.Mensagem);
                }

                MessageBox.Show(textoComTodasAsInconsistencias.ToString(), "Inconsistências ao excluir item!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro inesperado - " + ex.Message);
            }
        }

        public static void ExibirRelatorio(RelatorioBaseDev relatorio)
        {
            relatorio.GereRelatorio();

            using (ReportPrintTool printTool = new ReportPrintTool(relatorio))
            {
                printTool.ShowRibbonPreviewDialog();
                printTool.ShowRibbonPreview(UserLookAndFeel.Default);
                printTool.Dispose();
            }
        }

        private static void ValideCamposObrigatorios()
        {
            if (_controleValidar != null)
            {
                List<Inconsistencia> listaDeInconsistencias = new List<Inconsistencia>();
                bool todosOsCamposEstaoPreenchidos = TodosCamposObrigatoriosEstaoPreenchidos(_controleValidar, listaDeInconsistencias);

                if (!todosOsCamposEstaoPreenchidos)
                {
                    InconsistenciasDeValidacao inconsistencias = new InconsistenciasDeValidacao();
                    inconsistencias.ListaDeInconsistencias = listaDeInconsistencias;

                    ExceptionInconsistencias ex = new ExceptionInconsistencias();
                    ex.Inconsistencias = inconsistencias;

                    throw ex;
                }
            }
        }

        private static bool TodosCamposObrigatoriosEstaoPreenchidos(Control controle, List<Inconsistencia> listaDeInconsistencias)
        {
            bool todosControlesEstaoPreenchidos = true;

            foreach (Control item in controle.Controls)
            {
                if (item is AkilTextEdit)
                {
                    var akiltextEdit = (AkilTextEdit)item;

                    akiltextEdit.Inconsistencias.Clear();

                    if (!akiltextEdit.CampoEstahPreenchido())
                    {
                        string mensagemInconsistencia = "não foi informado(a).";

                        if (akiltextEdit.LabelText != null)
                        {
                            mensagemInconsistencia = akiltextEdit.LabelText.Text + " não foi informado(a).";

                            Inconsistencia inconsistencia = new Inconsistencia();
                            inconsistencia.Mensagem = mensagemInconsistencia;

                            listaDeInconsistencias.Add(inconsistencia);
                        }

                        akiltextEdit.Inconsistencias.Add(mensagemInconsistencia);

                        if (todosControlesEstaoPreenchidos)
                        {
                            todosControlesEstaoPreenchidos = false;
                        }
                    }
                }
                else if (item is AkilLookUpEdit)
                {
                    var akilLookUpEdit = (AkilLookUpEdit)item;

                    if (!akilLookUpEdit.CampoEstahPreenchido())
                    {
                        if (akilLookUpEdit.LabelText != null)
                        {
                            Inconsistencia inconsistencia = new Inconsistencia();
                            inconsistencia.Mensagem = akilLookUpEdit.LabelText.Text + " não foi informado(a). ";

                            listaDeInconsistencias.Add(inconsistencia);
                        }

                        if (todosControlesEstaoPreenchidos)
                        {
                            todosControlesEstaoPreenchidos = false;
                        }
                    }
                }
                else
                {
                    var retorno = TodosCamposObrigatoriosEstaoPreenchidos(item, listaDeInconsistencias);

                    if (todosControlesEstaoPreenchidos)
                    {
                        todosControlesEstaoPreenchidos = retorno;
                    }
                }
            }

            return todosControlesEstaoPreenchidos;
        }
    }
}
