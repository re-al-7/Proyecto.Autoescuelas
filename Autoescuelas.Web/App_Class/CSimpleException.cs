#region usings

using System;
using System.Runtime.Serialization;

#endregion

namespace Autoescuelas.Web.App_Class
{
    /// <summary>
    /// Tipo de excepcion simple para tipificar los errores lanzados desde la aplicación
    /// </summary>
    [Serializable]
    public class CSimpleException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CSimpleException()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Mensaje de la excepcion</param>
        public CSimpleException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="format">Formato de la excepcion</param>
        /// <param name="args">Array de argumentos</param>
        public CSimpleException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Mensaje de la excepcion</param>
        /// <param name="innerException">Inner exception</param>
        public CSimpleException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="format">Formato de la excepcion</param>
        /// <param name="innerException">Inner excepction</param>
        /// <param name="args">Array de argumentos</param>
        public CSimpleException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="info">Información</param>
        /// <param name="context">Contexto de la excepcion</param>
        protected CSimpleException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}