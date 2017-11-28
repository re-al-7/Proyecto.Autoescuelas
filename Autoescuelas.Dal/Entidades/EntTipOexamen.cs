#region 
/***********************************************************************************************************
	NOMBRE:       EntTipOexamen
	DESCRIPCION:
		Clase que define un objeto para la Tabla tipo_examen

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
	public class EntTipOexamen : CBaseClass
	{
		public const string StrNombreTabla = "TipO_examen";
		public const string StrAliasTabla = "tipo_examen";
		public enum Fields
		{
			id_tipoexamen
			,des_tipoexamen
			,apiestado
			,usucre
			,feccre
			,usumod
			,fecmod

		}
		
		#region Constructoress
		
		public EntTipOexamen()
		{
			//Inicializacion de Variables
			des_tipoexamen = null;
			apiestado = null;
			usucre = null;
			usumod = null;
			fecmod = null;
		}
		
		public EntTipOexamen(EntTipOexamen obj)
		{
			id_tipoexamen = obj.id_tipoexamen;
			des_tipoexamen = obj.des_tipoexamen;
			apiestado = obj.apiestado;
			usucre = obj.usucre;
			feccre = obj.feccre;
			usumod = obj.usumod;
			fecmod = obj.fecmod;
		}
		
		#endregion
		
		
		/// <summary>
		/// identificacion de tipo de examen
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: Yes
		/// Es ForeignKey: No
		/// </summary>
		public int id_tipoexamen { get; set; } 

		/// <summary>
		/// descripcion de tipo examen puede ser teorico o practico o ambos
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String des_tipoexamen { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna apiestado de la Tabla tipo_examen
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String apiestado { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna usucre de la Tabla tipo_examen
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String usucre { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo DateTime que representa a la columna feccre de la Tabla tipo_examen
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public DateTime feccre { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna usumod de la Tabla tipo_examen
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String usumod { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo DateTime que representa a la columna fecmod de la Tabla tipo_examen
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public DateTime? fecmod { get; set; } 

	}
}

