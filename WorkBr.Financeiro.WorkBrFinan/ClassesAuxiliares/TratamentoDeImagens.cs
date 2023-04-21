using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.ClassesAuxiliares
{
    public static class TratamentoDeImagens
    {
        public static string BuscaImagem(PictureBox pPictureBox, string pFilter)
        {
            try
            {
                OpenFileDialog _ofd = new OpenFileDialog();
                _ofd.Filter = pFilter;

                if (_ofd.ShowDialog() == DialogResult.OK)
                {
                    if (pPictureBox != null)
                        pPictureBox.Image = Image.FromFile(_ofd.FileName.ToString());
                    return _ofd.FileName.ToString();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return "";
        }

        public static string BuscaImagem(string pFilter)
        {
            try
            {
                OpenFileDialog _ofd = new OpenFileDialog();
                _ofd.Filter = pFilter;
                if (_ofd.ShowDialog() == DialogResult.OK)
                {
                    return _ofd.FileName.ToString();
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return "";
        }

        public static PictureBox ConvertByteToImagem(byte[] pValue)
        {
            try
            {
                PictureBox _Pic = new PictureBox();
                if (pValue == null) { return null; }
                byte[] _bytImagem = (byte[])pValue;
                MemoryStream _Ms = new MemoryStream();
                _Ms.Write(_bytImagem, 0, _bytImagem.Length);
                _Pic.Image = Image.FromStream(_Ms);
                return _Pic;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        public static byte[] ConvertImagemToByte(PictureBox pPictureBox)
        {
            try
            {
                if (pPictureBox == null) { return null; }

                return ConvertImagemToByte(pPictureBox.Image);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public static byte[] ConvertImagemToByte(Image imagem)
        {
            try
            {
                if (imagem == null) { return null; }
                MemoryStream _Ms = new MemoryStream();
                imagem.Save(_Ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] _Imagem = new Byte[_Ms.Length];
                _Ms.Position = 0;
                _Ms.Read(_Imagem, 0, Convert.ToInt32(_Ms.Length));
                return _Imagem;

            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
    }
}
