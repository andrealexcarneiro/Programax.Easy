using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using Programax.Easy.View.Componentes.Enumeradores;
using System.IO;
using DevExpress.XtraGrid;

namespace Programax.Easy.View.Componentes
{
    public class AkilGridControl : GridControl
    {
        #region " VARIÁVEIS PRIVADAS "

        private bool _jahPossuiEventoLinkado;
        private EnumAkilGridControlModoImpressao _enumModoImpressao;

        #endregion

        #region " PROPRIEDADES "

        public EnumAkilGridControlModoImpressao ModoImpressao
        {
            get
            {
                return _enumModoImpressao;
            }
            set
            {
                _enumModoImpressao = value;
            }
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public void ExibaRelatorio()
        {
            if (!_jahPossuiEventoLinkado)
            {
                var gridView = (GridView)this.MainView;
                gridView.PrintInitialize += new DevExpress.XtraGrid.Views.Base.PrintInitializeEventHandler(this.gridView_PrintInitialize);
               


                _jahPossuiEventoLinkado = true;
            }

            this.ShowPrintPreview();
        }

        #endregion

        #region " EVENTOS "

        private void gridView_PrintInitialize(object sender, DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs e)
        {
            PrintingSystemBase pb = e.PrintingSystem as PrintingSystemBase;

            string diretorioLayout = Directory.GetCurrentDirectory() + @"\RelatoriosDevExpress\LayoutsXml\";

            if (_enumModoImpressao == EnumAkilGridControlModoImpressao.PAISAGEM)
            {
                diretorioLayout += "RelatorioBasePaisagem.xml";
            }
            else
            {
                diretorioLayout += "RelatorioBaseDev.xml";
            }

            pb.PageSettings.RestoreFromXml(diretorioLayout);
        }

        #endregion
    }
}
