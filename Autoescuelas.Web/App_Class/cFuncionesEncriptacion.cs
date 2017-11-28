#region

/* ****************************************************** */
/* GENERADO POR: ReAl ClassGenerator
/* SISTEMA: AP
/* AUTOR: R. Alonzo Vera
/* FECHA: 04/10/2010  -  18:15
/* ****************************************************** */

#endregion

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Autoescuelas.Web.App_Class
{
    public static class cFuncionesEncriptacion
    {
        public static string generarMD5(string cadena)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5CryptoServiceProvider md5;

            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(cadena);
            encodedBytes = md5.ComputeHash(originalBytes);

            return BitConverter.ToString(encodedBytes).Replace("-", "");
        }

        /// <summary>
        ///     Función que crea un archivo encriptado a partir de un path dado y máscara de Bytes
        /// </summary>
        /// <param name="path" type="string">
        ///     <para>
        ///         Dirección física del archivo
        ///     </para>
        /// </param>
        /// <param name="mask" type="byte[]">
        ///     <para>
        ///         Máscara de Bytes
        ///     </para>
        /// </param>
        public static void cifrarXor(string path, byte[] mask)
        {
            FileInfo file = new FileInfo(path);
            file.CopyTo(path + ".cif");
            BufferedStream bsr = new BufferedStream(File.OpenRead(path));
            BufferedStream bsw = new BufferedStream(File.OpenWrite(path + ".cif"));

            while (true)
            {
                int read = bsr.ReadByte();

                if (read < 0)
                {
                    break;
                }
                byte b = (byte)read;

                for (int i = 0; i < mask.Length; i++)
                {
                    b = (byte)(b ^ mask[i]);
                }
                bsw.WriteByte(b);
            }
            bsr.Close();
            bsw.Close();
        }

        /// <summary>
        ///     Función que crea un descifra un archivo encriptado a partir de un path dado y máscara de Bytes
        /// </summary>
        /// <param name="path" type="string">
        ///     <para>
        ///         Dirección física del archivo
        ///     </para>
        /// </param>
        /// <param name="mask" type="byte[]">
        ///     <para>
        ///         Máscara de Bytes
        ///     </para>
        /// </param>
        public static void descifrarXor(string path, byte[] mask)
        {
            FileInfo file = new FileInfo(path);
            file.CopyTo(path + ".decif");
            BufferedStream bsr = new BufferedStream(File.OpenRead(path));
            BufferedStream bsw = new BufferedStream(File.OpenWrite(path + ".decif"));

            while (true)
            {
                int read = bsr.ReadByte();

                if (read < 0)
                {
                    break;
                }
                byte b = (byte)read;

                for (int i = mask.Length - 1; i >= 0; i--)
                {
                    b = (byte)(b ^ mask[i]);
                }
                bsw.WriteByte(b);
            }
            bsr.Close();
            bsw.Close();
        }
    }
}
