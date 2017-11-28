#region
/***********************************************************************************************************
	NOMBRE:       entSegAplicaciones
	DESCRIPCION:
		Clase que define un objeto para la Tabla SEGAPLICACIONES

	REVISIONES:
		Ver        FECHA       Autor            Descripcion
		---------  ----------  ---------------  ------------------------------------
		1.0        10/01/2017  R Alonzo Vera A  Creacion

*************************************************************************************************************/
#endregion

#region

using System;
using System.ComponentModel.DataAnnotations;
using Autoescuelas.Dal;

#endregion

namespace Integrate.SisPlan.Dal.Entidades
{
    public class EntSegAplicaciones : CBaseClass
    {
        public const string StrNombreTabla = "SEGAPLICACIONES";
        public const string StrAliasTabla = "Sap";

        public enum Fields
        {
            Aplicacionsap
            , Descripcionsap
            , Apiestadosap
            , Apitransaccionsap
            , Usucresap
            , Feccresap
            , Usumodsap
            , Fecmodsap
        }

        #region Constructoress

        public EntSegAplicaciones()
        {
            //Inicializacion de Variables
            Aplicacionsap = null;
            Descripcionsap = null;
            Apiestadosap = null;
            Apitransaccionsap = null;
            Usucresap = null;
            Usumodsap = null;
            Fecmodsap = null;
        }

        public EntSegAplicaciones(EntSegAplicaciones obj)
        {
            Aplicacionsap = obj.Aplicacionsap;
            Descripcionsap = obj.Descripcionsap;
            Apiestadosap = obj.Apiestadosap;
            Apitransaccionsap = obj.Apitransaccionsap;
            Usucresap = obj.Usucresap;
            Feccresap = obj.Feccresap;
            Usumodsap = obj.Usumodsap;
            Fecmodsap = obj.Fecmodsap;
        }

        #endregion

        /// <summary>
        /// 	 Variable local de tipo String que representa a la columna Aplicacionsap de la Tabla SEGAPLICACIONES
        /// </summary>
        private string _aplicacionsap;

        /// <summary>
        /// 	 Código identificador primario
        /// 	 Permite Null: No
        /// 	 Es Calculada: No
        /// 	 Es RowGui: No
        /// 	 Es PrimaryKey: Yes
        /// 	 Es ForeignKey: No
        /// </summary>
        public string Aplicacionsap
        {
            get { return _aplicacionsap; }
            set
            {
                if (_aplicacionsap != value)
                {
                    _aplicacionsap = value;
                }
            }
        }

        /// <summary>
        /// 	 Variable local de tipo String que representa a la columna Descripcionsap de la Tabla SEGAPLICACIONES
        /// </summary>
        private string _descripcionsap;

        /// <summary>
        /// 	 Descripción de aplicación
        /// 	 Permite Null: No
        /// 	 Es Calculada: No
        /// 	 Es RowGui: No
        /// 	 Es PrimaryKey: No
        /// 	 Es ForeignKey: No
        /// </summary>
        public string Descripcionsap
        {
            get { return _descripcionsap; }
            set
            {
                if (_descripcionsap != value)
                {
                    _descripcionsap = value;
                }
            }
        }

        /// <summary>
        /// 	 Variable local de tipo String que representa a la columna Apiestadosap de la Tabla SEGAPLICACIONES
        /// </summary>
        private string _apiestadosap;

        /// <summary>
        /// 	 Estado actual del registro
        /// 	 Permite Null: No
        /// 	 Es Calculada: No
        /// 	 Es RowGui: No
        /// 	 Es PrimaryKey: No
        /// 	 Es ForeignKey: No
        /// </summary>
        public string Apiestadosap
        {
            get { return _apiestadosap; }
            set
            {
                if (_apiestadosap != value)
                {
                    _apiestadosap = value;
                }
            }
        }

        /// <summary>
        /// 	 Variable local de tipo String que representa a la columna Apitransaccionsap de la Tabla SEGAPLICACIONES
        /// </summary>
        private string _apitransaccionsap;

        /// <summary>
        /// 	 Ultima transacción realizada en el registro
        /// 	 Permite Null: No
        /// 	 Es Calculada: No
        /// 	 Es RowGui: No
        /// 	 Es PrimaryKey: No
        /// 	 Es ForeignKey: No
        /// </summary>
        public string Apitransaccionsap
        {
            get { return _apitransaccionsap; }
            set
            {
                if (_apitransaccionsap != value)
                {
                    RaisePropertyChanging(Fields.Apitransaccionsap.ToString());
                    _apitransaccionsap = value;
                    RaisePropertyChanged(Fields.Apitransaccionsap.ToString());
                }
            }
        }

        /// <summary>
        /// 	 Variable local de tipo String que representa a la columna Usucresap de la Tabla SEGAPLICACIONES
        /// </summary>
        private string _usucresap;

        /// <summary>
        /// 	 Usuario que realizó la creación del registro
        /// 	 Permite Null: No
        /// 	 Es Calculada: No
        /// 	 Es RowGui: No
        /// 	 Es PrimaryKey: No
        /// 	 Es ForeignKey: No
        /// </summary>
        public string Usucresap
        {
            get { return _usucresap; }
            set
            {
                if (_usucresap != value)
                {
                    RaisePropertyChanging(Fields.Usucresap.ToString());
                    _usucresap = value;
                    RaisePropertyChanged(Fields.Usucresap.ToString());
                }
            }
        }

        /// <summary>
        /// 	 Variable local de tipo DateTime que representa a la columna Feccresap de la Tabla SEGAPLICACIONES
        /// </summary>
        private DateTime _feccresap;

        /// <summary>
        /// 	 Fecha en la que se realizó la creación del registro
        /// 	 Permite Null: No
        /// 	 Es Calculada: No
        /// 	 Es RowGui: No
        /// 	 Es PrimaryKey: No
        /// 	 Es ForeignKey: No
        /// </summary>
        public DateTime Feccresap
        {
            get { return _feccresap; }
            set
            {
                if (_feccresap != value)
                {
                    RaisePropertyChanging(Fields.Feccresap.ToString());
                    _feccresap = value;
                    RaisePropertyChanged(Fields.Feccresap.ToString());
                }
            }
        }

        /// <summary>
        /// 	 Variable local de tipo String que representa a la columna Usumodsap de la Tabla SEGAPLICACIONES
        /// </summary>
        private string _usumodsap;

        /// <summary>
        /// 	 Usuario que realizó la última modificación del registro
        /// 	 Permite Null: Yes
        /// 	 Es Calculada: No
        /// 	 Es RowGui: No
        /// 	 Es PrimaryKey: No
        /// 	 Es ForeignKey: No
        /// </summary>
        public string Usumodsap
        {
            get { return _usumodsap; }
            set
            {
                if (_usumodsap != value)
                {
                    RaisePropertyChanging(Fields.Usumodsap.ToString());
                    _usumodsap = value;
                    RaisePropertyChanged(Fields.Usumodsap.ToString());
                }
            }
        }

        /// <summary>
        /// 	 Variable local de tipo DateTime que representa a la columna Fecmodsap de la Tabla SEGAPLICACIONES
        /// </summary>
        private DateTime? _fecmodsap;

        /// <summary>
        /// 	 Fecha en la que se realizó la última modificación en el registro
        /// 	 Permite Null: Yes
        /// 	 Es Calculada: No
        /// 	 Es RowGui: No
        /// 	 Es PrimaryKey: No
        /// 	 Es ForeignKey: No
        /// </summary>
        public DateTime? Fecmodsap
        {
            get { return _fecmodsap; }
            set
            {
                if (_fecmodsap != value)
                {
                    RaisePropertyChanging(Fields.Fecmodsap.ToString());
                    _fecmodsap = value;
                    RaisePropertyChanged(Fields.Fecmodsap.ToString());
                }
            }
        }
    }
}