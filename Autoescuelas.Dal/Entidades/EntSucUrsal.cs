#region 
/***********************************************************************************************************
	NOMBRE:       EntSucUrsal
	DESCRIPCION:
		Clase que define un objeto para la Tabla sucursal

	REVISIONES:
		Ver        FECHA       Autor            Descripcion 
		---------  ----------  ---------------  ------------------------------------
		1.0        28/11/2017  Ivan Cruz        Creacion 

*************************************************************************************************************/
#endregion


#region
using System;
using System.Collections.Generic;
using System.IO;

using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
#endregion

namespace Autoescuelas.Dal.Entidades
{
	public class EntSucUrsal : CBaseClass
	{
		public const string StrNombreTabla = "SucUrsal";
		public const string StrAliasTabla = "sucursal";
		public enum Fields
		{
			id_sucursal
			,direccion_sucursal
			,telefono_sucursal
			,id_usuario
			,id_autoescuelas
			,apiestado
			,usucre
			,feccre
			,usumod
			,fecmod

		}
		
		#region Constructoress
		
		public EntSucUrsal()
		{
			//Inicializacion de Variables
			direccion_sucursal = null;
			telefono_sucursal = null;
			id_usuario = null;
			id_autoescuelas = null;
			apiestado = null;
			usucre = null;
			usumod = null;
			fecmod = null;
		}
		
		public EntSucUrsal(EntSucUrsal obj)
		{
			id_sucursal = obj.id_sucursal;
			direccion_sucursal = obj.direccion_sucursal;
			telefono_sucursal = obj.telefono_sucursal;
			id_usuario = obj.id_usuario;
			id_autoescuelas = obj.id_autoescuelas;
			apiestado = obj.apiestado;
			usucre = obj.usucre;
			feccre = obj.feccre;
			usumod = obj.usumod;
			fecmod = obj.fecmod;
		}
		
		#endregion

		
		/// <summary>
		/// identificador de sucursal
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: Yes
		/// Es ForeignKey: No
		/// </summary>
		public int id_sucursal { get; set; } 

		/// <summary>
		/// direccion de sucursal
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String direccion_sucursal { get; set; } 

		/// <summary>
		/// telefono de la sucursal para coordinacion 
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public int? telefono_sucursal { get; set; } 

		/// <summary>
		/// identificador de la tabla usuario
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: Yes
		/// </summary>
		public int? id_usuario { get; set; } 

		/// <summary>
		/// identificador de tabla autoescuela
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: Yes
		/// </summary>
		public int? id_autoescuelas { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna apiestado de la Tabla sucursal
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String apiestado { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna usucre de la Tabla sucursal
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String usucre { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo DateTime que representa a la columna feccre de la Tabla sucursal
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public DateTime feccre { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna usumod de la Tabla sucursal
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String usumod { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo DateTime que representa a la columna fecmod de la Tabla sucursal
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public DateTime? fecmod { get; set; } 

	}
}

