using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Autoescuelas.Dal
{
    public class CBaseClass : INotifyPropertyChanged, ICloneable
    {

        #region IClonable

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

        #region Metodos Privados

        /// <summary>
        /// Obtiene el Hash a partir de un array de Bytes
        /// </summary>
        /// <param name="objectAsBytes"></param>
        /// <returns>string</returns>
        private string ComputeHash(byte[] objectAsBytes)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            try
            {
                byte[] result = md5.ComputeHash(objectAsBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    sb.Append(result[i].ToString("X2"));
                }

                return sb.ToString();
            }
            catch (ArgumentNullException ane)
            {
                return null;
            }
        }

        /// <summary>
        ///     Obtienen el Hash basado en algun algoritmo de Encriptación
        /// </summary>
        /// <typeparam name="T">
        ///     Algoritmo de encriptación
        /// </typeparam>
        /// <param name="cryptoServiceProvider">
        ///     Provvedor de Servicios de Criptografía
        /// </param>
        /// <returns>
        ///     String que representa el Hash calculado
        /// </returns>
        private string computeHash<T>(T cryptoServiceProvider) where T : HashAlgorithm, new()
        {
            DataContractSerializer serializer = new DataContractSerializer(this.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, this);
                cryptoServiceProvider.ComputeHash(memoryStream.ToArray());
                return Convert.ToBase64String(cryptoServiceProvider.Hash);
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Devuelve un String que representa al Objeto
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string hashString;

            //Evitar parametros NULL
            if (this == null)
                throw new ArgumentNullException("Parametro NULL no es valido");

            //Se verifica si el objeto es serializable.
            try
            {
                MemoryStream memStream = new MemoryStream();
                XmlSerializer serializer = new XmlSerializer(typeof(CBaseClass));
                serializer.Serialize(memStream, this);

                //Ahora se obtiene el Hash del Objeto.
                hashString = ComputeHash(memStream.ToArray());

                return hashString;
            }
            catch (AmbiguousMatchException ame)
            {
                throw new ApplicationException("El Objeto no es Serializable. Message:" + ame.Message);
            }
        }

        /// <summary>
        /// Verifica que dos objetos sean identicos
        /// </summary>
        public static bool operator ==(CBaseClass first, CBaseClass second)
        {
            // Verifica si el puntero en memoria es el mismo
            if (Object.ReferenceEquals(first, second))
                return true;

            // Verifica valores nulos
            if ((object)first == null || (object)second == null)
                return false;

            return first.GetHashCode() == second.GetHashCode();
        }

        /// <summary>
        /// Verifica que dos objetos sean distintos
        /// </summary>
        public static bool operator !=(CBaseClass first, CBaseClass second)
        {
            return !(first == second);
        }

        /// <summary>
        /// Compara este objeto con otro
        /// </summary>
        /// <param name="obj">El objeto a comparar</param>
        /// <returns>Devuelve Verdadero si ambos objetos son iguales</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() == this.GetType())
                return obj.GetHashCode() == this.GetHashCode();

            return false;
        }

        #endregion
        

        #region Eventos

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangingEventHandler PropertyChanging;
        protected void RaisePropertyChanging(string prop)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, new PropertyChangingEventArgs(prop));
        }

        #endregion
    }
}
