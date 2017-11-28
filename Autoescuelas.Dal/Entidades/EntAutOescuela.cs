#region 
/***********************************************************************************************************
	NOMBRE:       EntAutOescuela
	DESCRIPCION:
		Clase que define un objeto para la Tabla autoescuela

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
	public class EntAutOescuela : CBaseClass
	{
		public const string StrNombreTabla = "AutOescuela";
		public const string StrAliasTabla = "autoescuela";
		public enum Fields
		{
			id_autoescuelas
			,dir_autoescuela
			,depa_autoescuela
			,loc_autoescuela
			,telefono_autoescuela
			,nombre_autoescuela
			,apiestado
			,usucre
			,feccre
			,usumod
			,fecmod

		}
		
		#region Constructoress
		
		public EntAutOescuela()
		{
			//Inicializacion de Variables
			dir_autoescuela = null;
			depa_autoescuela = null;
			loc_autoescuela = null;
			telefono_autoescuela = null;
			nombre_autoescuela = null;
			apiestado = null;
			usucre = null;
			usumod = null;
			fecmod = null;
		}
		
		public EntAutOescuela(EntAutOescuela obj)
		{
			id_autoescuelas = obj.id_autoescuelas;
			dir_autoescuela = obj.dir_autoescuela;
			depa_autoescuela = obj.depa_autoescuela;
			loc_autoescuela = obj.loc_autoescuela;
			telefono_autoescuela = obj.telefono_autoescuela;
			nombre_autoescuela = obj.nombre_autoescuela;
			apiestado = obj.apiestado;
			usucre = obj.usucre;
			feccre = obj.feccre;
			usumod = obj.usumod;
			fecmod = obj.fecmod;
		}
		
		#endregion
		
		/// <summary>
		/// identificador de autoescuela
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: Yes
		/// Es ForeignKey: No
		/// </summary>
		public int id_autoescuelas { get; set; } 

		/// <summary>
		/// direccion de autoescuela
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String dir_autoescuela { get; set; } 

		/// <summary>
		/// departamento de la Autoescuela
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String depa_autoescuela { get; set; } 

		/// <summary>
		/// localidad de la autoescuela
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String loc_autoescuela { get; set; } 

		/// <summary>
		/// telefono de la Autoescuela
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String telefono_autoescuela { get; set; } 

		/// <summary>
		/// denominacion de la autoescuela
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String nombre_autoescuela { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna apiestado de la Tabla autoescuela
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String apiestado { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna usucre de la Tabla autoescuela
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String usucre { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo DateTime que representa a la columna feccre de la Tabla autoescuela
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public DateTime feccre { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna usumod de la Tabla autoescuela
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String usumod { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo DateTime que representa a la columna fecmod de la Tabla autoescuela
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public DateTime? fecmod { get; set; } 

	}
}

