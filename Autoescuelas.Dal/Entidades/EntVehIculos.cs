#region 
/***********************************************************************************************************
	NOMBRE:       EntVehIculos
	DESCRIPCION:
		Clase que define un objeto para la Tabla vehiculos

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
	public class EntVehIculos : CBaseClass
	{
		public const string StrNombreTabla = "VehIculos";
		public const string StrAliasTabla = "vehiculos";
		public enum Fields
		{
			id_vehiculo
			,placa_vehiculo
			,color_vehiculo
			,capacidad_tonelaje
			,capacidad_personas
			,modelo_vehiculo
			,marca_vehiculo
			,id_autoescuelas
			,apiestado
			,usucre
			,feccre
			,usumod
			,fecmod

		}
		
		#region Constructoress
		
		public EntVehIculos()
		{
			//Inicializacion de Variables
			placa_vehiculo = null;
			color_vehiculo = null;
			capacidad_tonelaje = null;
			capacidad_personas = null;
			modelo_vehiculo = null;
			marca_vehiculo = null;
			id_autoescuelas = null;
			apiestado = null;
			usucre = null;
			usumod = null;
			fecmod = null;
		}
		
		public EntVehIculos(EntVehIculos obj)
		{
			id_vehiculo = obj.id_vehiculo;
			placa_vehiculo = obj.placa_vehiculo;
			color_vehiculo = obj.color_vehiculo;
			capacidad_tonelaje = obj.capacidad_tonelaje;
			capacidad_personas = obj.capacidad_personas;
			modelo_vehiculo = obj.modelo_vehiculo;
			marca_vehiculo = obj.marca_vehiculo;
			id_autoescuelas = obj.id_autoescuelas;
			apiestado = obj.apiestado;
			usucre = obj.usucre;
			feccre = obj.feccre;
			usumod = obj.usumod;
			fecmod = obj.fecmod;
		}
		
		#endregion
		
		
		/// <summary>
		/// identificador de vehiculo
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: Yes
		/// Es ForeignKey: No
		/// </summary>
		public int id_vehiculo { get; set; } 

		/// <summary>
		/// placa del vehiculo
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String placa_vehiculo { get; set; } 

		/// <summary>
		/// color del vehiculo
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String color_vehiculo { get; set; } 

		/// <summary>
		/// capacidad de tonelaje del vehiculo
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String capacidad_tonelaje { get; set; } 

		/// <summary>
		/// capacidad de personas del vehiculo es para ver si cumple con lo requerido
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String capacidad_personas { get; set; } 

		/// <summary>
		/// modelo de vehiculo ya que los vehiculos no deben ser muy antiguos
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String modelo_vehiculo { get; set; } 

		/// <summary>
		/// marca del vehiculo
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String marca_vehiculo { get; set; } 

		/// <summary>
		/// clave foranea de la tabla autoescuela
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: Yes
		/// </summary>
		public int? id_autoescuelas { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna apiestado de la Tabla vehiculos
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String apiestado { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna usucre de la Tabla vehiculos
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String usucre { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo DateTime que representa a la columna feccre de la Tabla vehiculos
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public DateTime feccre { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo String que representa a la columna usumod de la Tabla vehiculos
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String usumod { get; set; } 

		/// <summary>
		/// Propiedad publica de tipo DateTime que representa a la columna fecmod de la Tabla vehiculos
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public DateTime? fecmod { get; set; } 

	}
}

