#region 
/***********************************************************************************************************
	NOMBRE:       EntExaMen
	DESCRIPCION:
		Clase que define un objeto para la Tabla examen

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
	public class EntExaMen : CBaseClass
	{
		public const string StrNombreTabla = "ExaMen";
		public const string StrAliasTabla = "examen";
		public enum Fields
		{
			id_examen
			,nota_exa_prac
			,nota_exa_teo
			,id_sucursal
			,id_vehiculo
			,id_tipoexamen
			,id_postulante
			,id_tipolicencia
			,id_usuario
			,apiestado
			,usucre
			,feccre
			,usumod
			,fecmod

		}
		
		#region Constructoress
		
		public EntExaMen()
		{
			//Inicializacion de Variables
			nota_exa_prac = null;
			nota_exa_teo = null;
			id_sucursal = null;
			id_vehiculo = null;
			id_tipoexamen = null;
			id_postulante = null;
			id_tipolicencia = null;
			id_usuario = null;
			apiestado = null;
			usucre = null;
			usumod = null;
			fecmod = null;
		}
		
		public EntExaMen(EntExaMen obj)
		{
			id_examen = obj.id_examen;
			nota_exa_prac = obj.nota_exa_prac;
			nota_exa_teo = obj.nota_exa_teo;
			id_sucursal = obj.id_sucursal;
			id_vehiculo = obj.id_vehiculo;
			id_tipoexamen = obj.id_tipoexamen;
			id_postulante = obj.id_postulante;
			id_tipolicencia = obj.id_tipolicencia;
			id_usuario = obj.id_usuario;
			apiestado = obj.apiestado;
			usucre = obj.usucre;
			feccre = obj.feccre;
			usumod = obj.usumod;
			fecmod = obj.fecmod;
		}
		
		#endregion
		
		
		/// <summary>
		/// identificador de la tabla examen
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: Yes
		/// Es ForeignKey: No
		/// </summary>
		public int id_examen { get; set; } 

		/// <summary>
		/// la columna es para anotar la nota que saco el postulante en el examen practico
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public int? nota_exa_prac { get; set; } 

		/// <summary>
		/// la columna es para anotar la nota que saco el postulante en el examen teorico
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public int? nota_exa_teo { get; set; } 

		/// <summary>
		/// clave foranea de la tanla sucursal
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: Yes
		/// </summary>
		public int? id_sucursal { get; set; } 

		/// <summary>
		/// clave foranea de la tabla vehiculo
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: Yes
		/// </summary>
		public int? id_vehiculo { get; set; } 

		/// <summary>
		/// identificador de la tabla tipo examen
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: Yes
		/// </summary>
		public int? id_tipoexamen { get; set; } 

		/// <summary>
		/// identificador de la tabla postulante
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: Yes
		/// </summary>
		public int? id_postulante { get; set; } 

		/// <summary>
		/// identificador de la tabla tipo licencia
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: Yes
		/// </summary>
		public int? id_tipolicencia { get; set; } 

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
		/// Propiedad publica de tipo String que representa a la columna apiestado de la Tabla examen
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String apiestado { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna usucre de la Tabla examen
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String usucre { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo DateTime que representa a la columna feccre de la Tabla examen
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public DateTime feccre { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna usumod de la Tabla examen
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String usumod { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo DateTime que representa a la columna fecmod de la Tabla examen
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public DateTime? fecmod { get; set; } 

	}
}

