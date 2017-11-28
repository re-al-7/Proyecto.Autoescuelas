#region 
/***********************************************************************************************************
	NOMBRE:       EntPosTulante
	DESCRIPCION:
		Clase que define un objeto para la Tabla postulante

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
	public class EntPosTulante : CBaseClass
	{
		public const string StrNombreTabla = "PosTulante";
		public const string StrAliasTabla = "postulante";
		public enum Fields
		{
			id_postulante
			,ci_postulante
			,nombre_postulante
			,ap_mat_postulante
			,ap_pat_postulante
			,celular_postulante
			,fecha_nac_postulante
			,apiestado
			,usucre
			,feccre
			,usumod
			,fecmod

		}
		
		#region Constructoress
		
		public EntPosTulante()
		{
			//Inicializacion de Variables
			ci_postulante = null;
			nombre_postulante = null;
			ap_mat_postulante = null;
			ap_pat_postulante = null;
			celular_postulante = null;
			fecha_nac_postulante = null;
			apiestado = null;
			usucre = null;
			usumod = null;
			fecmod = null;
		}
		
		public EntPosTulante(EntPosTulante obj)
		{
			id_postulante = obj.id_postulante;
			ci_postulante = obj.ci_postulante;
			nombre_postulante = obj.nombre_postulante;
			ap_mat_postulante = obj.ap_mat_postulante;
			ap_pat_postulante = obj.ap_pat_postulante;
			celular_postulante = obj.celular_postulante;
			fecha_nac_postulante = obj.fecha_nac_postulante;
			apiestado = obj.apiestado;
			usucre = obj.usucre;
			feccre = obj.feccre;
			usumod = obj.usumod;
			fecmod = obj.fecmod;
		}
		
		#endregion

		
		/// <summary>
		/// identificador de los postulantes
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: Yes
		/// Es ForeignKey: No
		/// </summary>
		public int id_postulante { get; set; } 

		/// <summary>
		/// cedula de identidad del postulante
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public int? ci_postulante { get; set; } 

		/// <summary>
		/// nombre del postulante
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String nombre_postulante { get; set; } 

		/// <summary>
		/// apellido materno del postulante
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String ap_mat_postulante { get; set; } 

		/// <summary>
		/// apellido paterno del postulante
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String ap_pat_postulante { get; set; } 

		/// <summary>
		/// celular del postulante para cualquier eventualidad
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public int? celular_postulante { get; set; } 

		/// <summary>
		/// fecha de nacimiento del postulante
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public DateTime? fecha_nac_postulante { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna apiestado de la Tabla postulante
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String apiestado { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna usucre de la Tabla postulante
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String usucre { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo DateTime que representa a la columna feccre de la Tabla postulante
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public DateTime feccre { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna usumod de la Tabla postulante
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String usumod { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo DateTime que representa a la columna fecmod de la Tabla postulante
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public DateTime? fecmod { get; set; } 

	}
}

