#region 
/***********************************************************************************************************
	NOMBRE:       EntUsuArio
	DESCRIPCION:
		Clase que define un objeto para la Tabla usuario

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
	public class EntUsuArio : CBaseClass
	{
		public const string StrNombreTabla = "UsuArio";
		public const string StrAliasTabla = "usuario";
		public enum Fields
		{
			id_usuario
			,id_rol
			,id_sucursal
			,login
			,password
			,nombres
			,apellidos
			,apiestado
			,usucre
			,feccre
			,usumod
			,fecmod

		}
		
		#region Constructoress
		
		public EntUsuArio()
		{
			//Inicializacion de Variables
			login = null;
			password = null;
			nombres = null;
			apellidos = null;
			apiestado = null;
			usucre = null;
			usumod = null;
			fecmod = null;
		}
		
		public EntUsuArio(EntUsuArio obj)
		{
			id_usuario = obj.id_usuario;
			id_rol = obj.id_rol;
			id_sucursal = obj.id_sucursal;
			login = obj.login;
			password = obj.password;
			nombres = obj.nombres;
			apellidos = obj.apellidos;
			apiestado = obj.apiestado;
			usucre = obj.usucre;
			feccre = obj.feccre;
			usumod = obj.usumod;
			fecmod = obj.fecmod;
		}
		
		#endregion
		

		
		/// <summary>
		/// Identificador primario de registro
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: Yes
		/// Es ForeignKey: No
		/// </summary>
		public Int64 id_usuario { get; set; } 

		/// <summary>
		/// Identificador del Rol: 1 Administrador, 2 Instructor
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public int id_rol { get; set; } 

		/// <summary>
		/// Identificador de la sucursal a la que pertenece el usuario
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public int id_sucursal { get; set; } 

		/// <summary>
		/// Nombre de usuario en el sistema
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String login { get; set; } 

		/// <summary>
		/// contraseña encriptada en hash
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String password { get; set; } 

		/// <summary>
		/// Nombre de la persona que se registra
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String nombres { get; set; } 

		/// <summary>
		/// Apellidos de la persona
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String apellidos { get; set; } 

		/// <summary>
		/// Estado actual del registro
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String apiestado { get; set; } 

		/// <summary>
		/// Usuario que realizó la creación del registro
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String usucre { get; set; } 

		/// <summary>
		/// Fecha en la que se realizó la creación del registro
		/// Permite Null: No
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public DateTime feccre { get; set; } 

		/// <summary>
		/// Usuario que realizó la última modificación del registro
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public String usumod { get; set; } 

		/// <summary>
		/// Fecha en la que se realizó la última modificación del registro
		/// Permite Null: Yes
		/// Es Calculada: No
		/// Es RowGui: No
		/// Es PrimaryKey: No
		/// Es ForeignKey: No
		/// </summary>
		public DateTime? fecmod { get; set; } 

	}
}

