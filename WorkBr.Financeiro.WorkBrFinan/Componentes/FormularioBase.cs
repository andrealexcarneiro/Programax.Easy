using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Collections;

namespace Programax.Easy.View.Componentes
{
    public partial class FormularioBase : Form
    {
        private FormFundoModal _formFundoModal;

        public FormularioBase()
        {
            InitializeComponent();
        }

        public void AbrirTelaModal()
        {
            _formFundoModal = new FormFundoModal();

            _formFundoModal.AbrirTela(this);
        }

        public void AbrirTelaModal(bool fundoModalCompleto)
        {
            _formFundoModal = new FormFundoModal();

            _formFundoModal.AbrirTela(this, fundoModalCompleto);
        }

        protected bool HouveUmClickOuDuploClickNaLinha(GridView gridView)
        {
            Point pt = gridView.GridControl.PointToClient(Control.MousePosition);

            GridHitInfo info = gridView.CalcHitInfo(pt);

            return info.InRow || info.InRowCell;
        }

        protected Control CloneComponente(Control sourceControl)
        {
            Control result = null;

            try
            {
                Type t = sourceControl.GetType();
                result = (Control)Activator.CreateInstance(t);

                PropertyDescriptorCollection sourceProps = TypeDescriptor.GetProperties(sourceControl);
                PropertyDescriptorCollection destProps = TypeDescriptor.GetProperties(result);

                for (int i = 0; i < sourceProps.Count; i++)
                {
                    if (sourceProps[i].Attributes.Contains(DesignerSerializationVisibilityAttribute.Content))
                    {
                        object sourceValues = sourceProps[i].GetValue(sourceControl);
                        if ((sourceValues is IList) == true)
                        {
                            foreach (object child in (sourceValues as IList))
                            {
                                Control childCtrl = CloneComponente(child as Control);

                                IList destValues = destProps[i].GetValue(result) as IList;

                                System.Diagnostics.Debug.Assert(destValues != null);

                                if (destValues != null)
                                    destValues.Add(childCtrl);
                            }
                        }
                    }
                    else
                    {
                        destProps[sourceProps[i].Name].SetValue(result, sourceProps[i].GetValue(sourceControl));
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
