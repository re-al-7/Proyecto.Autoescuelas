#region 
/***********************************************************************************************************
	NOMBRE:       EntSegPaginas
	DESCRIPCION:
		Clase que define un objeto para la Tabla SEGPAGINAS

	REVISIONES:
		Ver        FECHA       Autor            Descripcion 
		---------  ----------  ---------------  ------------------------------------
		1.0        06/05/2017  R Alonzo Vera A  Creacion 

*************************************************************************************************************/
#endregion


#region
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Autoescuelas.Dal;
#endregion

namespace Integrate.SisPlan.Dal.Entidades
{
	public class EntSegPaginas : CBaseClass
	{
		public const string StrNombreTabla = "SEGPAGINAS";
		public const string StrAliasTabla = "Spg";
		public enum Fields
		{
			Aplicacionsap
			,Paginaspg
			,Paginapadrespg
			,Nombrespg
			,Nombremenuspg
			,Descripcionspg
			,Prioridadspg
			,Apiestadospg
			,Apitransaccionspg
			,Usucrespg
			,Feccrespg
			,Usumodspg
			,Fecmodspg

		}
		
		#region Constructoress
		
		public EntSegPaginas()
		{
			//Inicializacion de Variables
			Aplicacionsap = null;
			Paginapadrespg = null;
			Nombrespg = null;
			Nombremenuspg = null;
			Descripcionspg = null;
			Prioridadspg = null;
			Apiestadospg = null;
			Apitransaccionspg = null;
			Usucrespg = null;
			Usumodspg = null;
			Fecmodspg = null;
		}
		
		public EntSegPaginas(EntSegPaginas obj)
		{
			Aplicacionsap = obj.Aplicacionsap;
			Paginaspg = obj.Paginaspg;
			Paginapadrespg = obj.Paginapadrespg;
			Nombrespg = obj.Nombrespg;
			Nombremenuspg = obj.Nombremenuspg;
			Descripcionspg = obj.Descripcionspg;
			Prioridadspg = obj.Prioridadspg;
			Apiestadospg = obj.Apiestadospg;
			Apitransaccionspg = obj.Apitransaccionspg;
			Usucrespg = obj.Usucrespg;
			Feccrespg = obj.Feccrespg;
			Usumodspg = obj.Usumodspg;
			Fecmodspg = obj.Fecmodspg;
		}
		
		#endregion
		
		/// <summary>
		/// 	 Variable local de tipo String que representa a la columna Aplicacionsap de la Tabla SEGPAGINAS
		/// </summary>
		private String _aplicacionsap;
		/// <summary>
		/// 	 Aplicación a la que pertenece la página
		/// 	 Permite Null: No
		/// 	 Es Calculada: No
		/// 	 Es RowGui: No
		/// 	 Es PrimaryKey: No
		/// 	 Es ForeignKey: Yes
		/// </summary>
		public String Aplicacionsap
		{
			get {return _aplicacionsap;}
			set
			{
				if (_aplicacionsap != value)
				{
					RaisePropertyChanging(Fields.Aplicacionsap.ToString());
					_aplicacionsap = value;
					RaisePropertyChanged(Fields.Aplicacionsap.ToString());
				}
			}
		}


		/// <summary>
		/// 	 Variable local de tipo int que representa a la columna Paginaspg de la Tabla SEGPAGINAS
		/// </summary>
		private int _paginaspg;
		/// <summary>
		/// 	 Iidentificador numérico de página
		/// 	 Permite Null: No
		/// 	 Es Calculada: No
		/// 	 Es RowGui: No
		/// 	 Es PrimaryKey: Yes
		/// 	 Es ForeignKey: No
		/// </summary>
		public int Paginaspg
		{
			get {return _paginaspg;}
			set
			{
				if (_paginaspg != value)
				{
					RaisePropertyChanging(Fields.Paginaspg.ToString());
					_paginaspg = value;
					RaisePropertyChanged(Fields.Paginaspg.ToString());
				}
			}
		}


		/// <summary>
		/// 	 Variable local de tipo int que representa a la columna Paginapadrespg de la Tabla SEGPAGINAS
		/// </summary>
		private int? _paginapadrespg;
		/// <summary>
		/// 	 Código de página padre
		/// 	 Permite Null: Yes
		/// 	 Es Calculada: No
		/// 	 Es RowGui: No
		/// 	 Es PrimaryKey: No
		/// 	 Es ForeignKey: No
		/// </summary>
		public int? Paginapadrespg
		{
			get {return _paginapadrespg;}
			set
			{
				if (_paginapadrespg != value)
				{
					RaisePropertyChanging(Fields.Paginapadrespg.ToString());
					_paginapadrespg = value;
					RaisePropertyChanged(Fields.Paginapadrespg.ToString());
				}
			}
		}


		/// <summary>
		/// 	 Variable local de tipo String que representa a la columna Nombrespg de la Tabla SEGPAGINAS
		/// </summary>
		private String _nombrespg;
		/// <summary>
		/// 	 Identificador alfanumérico de página, debe terminar en .aspx
		/// 	 Permite Null: No
		/// 	 Es Calculada: No
		/// 	 Es RowGui: No
		/// 	 Es PrimaryKey: No
		/// 	 Es ForeignKey: No
		/// </summary>
		public String Nombrespg
		{
			get {return _nombrespg;}
			set
			{
				if (_nombrespg != value)
				{
					RaisePropertyChanging(Fields.Nombrespg.ToString());
					_nombrespg = value;
					RaisePropertyChanged(Fields.Nombrespg.ToString());
				}
			}
		}


		/// <summary>
		/// 	 Variable local de tipo String que representa a la columna Nombremenuspg de la Tabla SEGPAGINAS
		/// </summary>
		private String _nombremenuspg;
		/// <summary>
		/// 	 Nombre del menu para que se despliegue en la interfaz del usuario
		/// 	 Permite Null: Yes
		/// 	 Es Calculada: No
		/// 	 Es RowGui: No
		/// 	 Es PrimaryKey: No
		/// 	 Es ForeignKey: No
		/// </summary>
		public String Nombremenuspg
		{
			get {return _nombremenuspg;}
			set
			{
				if (_nombremenuspg != value)
				{
					RaisePropertyChanging(Fields.Nombremenuspg.ToString());
					_nombremenuspg = value;
					RaisePropertyChanged(Fields.Nombremenuspg.ToString());
				}
			}
		}


		/// <summary>
		/// 	 Variable local de tipo String que representa a la columna Descripcionspg de la Tabla SEGPAGINAS
		/// </summary>
		private String _descripcionspg;
		/// <summary>
		/// 	 Descripción corta del funcionamiento de la página
		/// 	 Permite Null: No
		/// 	 Es Calculada: No
		/// 	 Es RowGui: No
		/// 	 Es PrimaryKey: No
		/// 	 Es ForeignKey: No
		/// </summary>
		public String Descripcionspg
		{
			get {return _descripcionspg;}
			set
			{
				if (_descripcionspg != value)
				{
					RaisePropertyChanging(Fields.Descripcionspg.ToString());
					_descripcionspg = value;
					RaisePropertyChanged(Fields.Descripcionspg.ToString());
				}
			}
		}


		/// <summary>
		/// 	 Variable local de tipo int que representa a la columna Prioridadspg de la Tabla SEGPAGINAS
		/// </summary>
		private int? _prioridadspg;
		/// <summary>
		/// 	 Orden en el que se va a ordenar los elemento s del menu. De menor a mayor
		/// 	 Permite Null: Yes
		/// 	 Es Calculada: No
		/// 	 Es RowGui: No
		/// 	 Es PrimaryKey: No
		/// 	 Es ForeignKey: No
		/// </summary>
		public int? Prioridadspg
		{
			get {return _prioridadspg;}
			set
			{
				if (_prioridadspg != value)
				{
					RaisePropertyChanging(Fields.Prioridadspg.ToString());
					_prioridadspg = value;
					RaisePropertyChanged(Fields.Prioridadspg.ToString());
				}
			}
		}


		/// <summary>
		/// 	 Variable local de tipo String que representa a la columna Apiestadospg de la Tabla SEGPAGINAS
		/// </summary>
		private String _apiestadospg;
		/// <summary>
		/// 	 Estado actual del registro
		/// 	 Permite Null: No
		/// 	 Es Calculada: No
		/// 	 Es RowGui: No
		/// 	 Es PrimaryKey: No
		/// 	 Es ForeignKey: No
		/// </summary>
		public String Apiestadospg
		{
			get {return _apiestadospg;}
			set
			{
				if (_apiestadospg != value)
				{
					RaisePropertyChanging(Fields.Apiestadospg.ToString());
					_apiestadospg = value;
					RaisePropertyChanged(Fields.Apiestadospg.ToString());
				}
			}
		}


		/// <summary>
		/// 	 Variable local de tipo String que representa a la columna Apitransaccionspg de la Tabla SEGPAGINAS
		/// </summary>
		private String _apitransaccionspg;
		/// <summary>
		/// 	 Ultima transacción realizada en el registro
		/// 	 Permite Null: No
		/// 	 Es Calculada: No
		/// 	 Es RowGui: No
		/// 	 Es PrimaryKey: No
		/// 	 Es ForeignKey: No
		/// </summary>
		public String Apitransaccionspg
		{
			get {return _apitransaccionspg;}
			set
			{
				if (_apitransaccionspg != value)
				{
					RaisePropertyChanging(Fields.Apitransaccionspg.ToString());
					_apitransaccionspg = value;
					RaisePropertyChanged(Fields.Apitransaccionspg.ToString());
				}
			}
		}


		/// <summary>
		/// 	 Variable local de tipo String que representa a la columna Usucrespg de la Tabla SEGPAGINAS
		/// </summary>
		private String _usucrespg;
		/// <summary>
		/// 	 Usuario que realizó la creación del registro
		/// 	 Permite Null: No
		/// 	 Es Calculada: No
		/// 	 Es RowGui: No
		/// 	 Es PrimaryKey: No
		/// 	 Es ForeignKey: No
		/// </summary>
		public String Usucrespg
		{
			get {return _usucrespg;}
			set
			{
				if (_usucrespg != value)
				{
					RaisePropertyChanging(Fields.Usucrespg.ToString());
					_usucrespg = value;
					RaisePropertyChanged(Fields.Usucrespg.ToString());
				}
			}
		}


		/// <summary>
		/// 	 Variable local de tipo DateTime que representa a la columna Feccrespg de la Tabla SEGPAGINAS
		/// </summary>
		private DateTime _feccrespg;
		/// <summary>
		/// 	 Fecha en la que se realizó la creación del registro
		/// 	 Permite Null: No
		/// 	 Es Calculada: No
		/// 	 Es RowGui: No
		/// 	 Es PrimaryKey: No
		/// 	 Es ForeignKey: No
		/// </summary>
		public DateTime Feccrespg
		{
			get {return _feccrespg;}
			set
			{
				if (_feccrespg != value)
				{
					RaisePropertyChanging(Fields.Feccrespg.ToString());
					_feccrespg = value;
					RaisePropertyChanged(Fields.Feccrespg.ToString());
				}
			}
		}


		/// <summary>
		/// 	 Variable local de tipo String que representa a la columna Usumodspg de la Tabla SEGPAGINAS
		/// </summary>
		private String _usumodspg;
		/// <summary>
		/// 	 Usuario que realizó la última modificación del registro
		/// 	 Permite Null: Yes
		/// 	 Es Calculada: No
		/// 	 Es RowGui: No
		/// 	 Es PrimaryKey: No
		/// 	 Es ForeignKey: No
		/// </summary>
		public String Usumodspg
		{
			get {return _usumodspg;}
			set
			{
				if (_usumodspg != value)
				{
					RaisePropertyChanging(Fields.Usumodspg.ToString());
					_usumodspg = value;
					RaisePropertyChanged(Fields.Usumodspg.ToString());
				}
			}
		}


		/// <summary>
		/// 	 Variable local de tipo DateTime que representa a la columna Fecmodspg de la Tabla SEGPAGINAS
		/// </summary>
		private DateTime? _fecmodspg;
		/// <summary>
		/// 	 Fecha en la que se realizó la última modificación en el registro
		/// 	 Permite Null: Yes
		/// 	 Es Calculada: No
		/// 	 Es RowGui: No
		/// 	 Es PrimaryKey: No
		/// 	 Es ForeignKey: No
		/// </summary>
		public DateTime? Fecmodspg
		{
			get {return _fecmodspg;}
			set
			{
				if (_fecmodspg != value)
				{
					RaisePropertyChanging(Fields.Fecmodspg.ToString());
					_fecmodspg = value;
					RaisePropertyChanged(Fields.Fecmodspg.ToString());
				}
			}
		}


	}
}

