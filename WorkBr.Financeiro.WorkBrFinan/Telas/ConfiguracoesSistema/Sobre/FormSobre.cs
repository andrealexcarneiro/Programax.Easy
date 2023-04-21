using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.ConfiguracoesSistema.Sobre
{
    public partial class FormSobre : FormularioPadrao
    {
        public FormSobre()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void linkSiteProgramax_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.phdsolucao.com.br/");
        }

        private void linkFacebookProgramax_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/phdsolucao/videos/1423442004438206/");
        }

        private void linkMapaGoogleMaps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.com.br/maps/place/R.+Itumbiara,+19+-+Cidade+Jardim,+Goi%C3%A2nia+-+GO,+74413-120/@-16.6877209,-49.3002247,3a,75y,262.76h,79.48t/data=!3m6!1e1!3m4!1sx7hm8RQfY5086S8P-i3Yjg!2e0!7i13312!8i6656!4m5!3m4!1s0x935ef6a0bce31c57:0x9120655144092041!8m2!3d-16.6852277!4d-49.3004527");
        }

        private void linkSkype_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.skype.com/pt_BR/download-skype/skype-for-windows/downloading/");
        }

        private void linkAcessoRemoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.ammyy.com/pt/downloads.html");
        }

        private void linkChamados_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }
    }
}
