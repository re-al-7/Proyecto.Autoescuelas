#region 
/***********************************************************************************************************
	NOMBRE:       EntTipOlicencia
	DESCRIPCION:
		Clase que define un objeto para la Tabla tipo_licencia

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
	public class EntTipOlicencia : CBaseClass
	{
		public const string StrNombreTabla = "TipO_licencia";
		public const string StrAliasTabla = "tipo_licencia";
		public enum Fields
		{
			id_tipolicencia
			,categoria
			,apiestado
			,usucre
			,feccre
			,usumod
			,fecmod

		}
		
		#region Constructoress
		
		public EntTipOlicencia()
		{
			//Inicializacion de Variables
			categoria = null;
			apiestado = null;
			usucre = null;
			usumod = null;
			fecmod = null;
		}
		
		public EntTipOlicencia(EntTipOlicencia obj)
		{
			id_tipolicencia = obj.id_tipolicencia;
			categoria = obj.categoria;
			apiestado = obj.apiestado;
			usucre = obj.usucre;
			feccre = obj.feccre;
			usumod = obj.usumod;
			fecmod = obj.fecmod;
		}
		
		#endregion
		
		
		/// <summary>
		/// identificacion de tipo de licencia
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: Yes
		/// Es ForeignKey: No
		/// </summary>
		public int id_tipolicencia { get; set; } 

		/// <summary>
		/// se debe anotar las categorias que existen en licencias
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String categoria { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna apiestado de la Tabla tipo_licencia
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String apiestado { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna usucre de la Tabla tipo_licencia
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String usucre { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo DateTime que representa a la columna feccre de la Tabla tipo_licencia
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public DateTime feccre { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna usumod de la Tabla tipo_licencia
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String usumod { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo DateTime que representa a la columna fecmod de la Tabla tipo_licencia
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public DateTime? fecmod { get; set; } 

	}
}

