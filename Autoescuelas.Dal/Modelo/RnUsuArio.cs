#region 
/***********************************************************************************************************
	NOMBRE:       RnUsuArio
	DESCRIPCION:
		Clase que implmenta los metodos y operaciones sobre la Tabla usuario

	REVISIONES:
		Ver        FECHA       Autor            Descripcion 
		---------  ----------  ---------------  ------------------------------------
		1.0        28/11/2017  Ivan Cruz        Creacion 

*************************************************************************************************************/
#endregion



#region
using System;
using System.Globalization;
using System.Threading;

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using Autoescuelas.Dal; 
using Autoescuelas.PgConn; 
using Autoescuelas.Dal.Entidades;
using System.Web.UI.WebControls;
#endregion

namespace Autoescuelas.Dal.Modelo
{
	public class RnUsuArio
	{
		#region Reflection
        
		
		/// <summary>
		/// Metodo para castear Dinamicamente un Tipo
		/// </summary>
		/// <param name="valor">Tipo a ser casteado</param>
		/// <param name="myField">Enum de la columna</param>
		/// <returns>Devuelve un objeto del Tipo de la columna especificada en el Enum</returns>
		public object GetColumnType(object valor, EntUsuArio.Fields myField)
		{
			if (DBNull.Value.Equals(valor)) 
				return null;
			Type destino = typeof(EntUsuArio).GetProperty(myField.ToString()).PropertyType;
			var miTipo = Nullable.GetUnderlyingType(destino) ?? destino;
			
			try
			{
				TypeConverter tc = TypeDescriptor.GetConverter(miTipo);
				return tc.ConvertFrom(valor);
			}
			catch (Exception)
			{
				return Convert.ChangeType(valor, miTipo);
			}
		}

		/// <summary>
		/// Metodo para castear Dinamicamente un Tipo
		/// </summary>
		/// <param name="valor">Tipo a ser casteado</param>
		/// <param name="strField">Nombre de la columna</param>
		/// <returns>Devuelve un objeto del Tipo de la columna especificada en el Enum</returns>
		public object GetColumnType(object valor, string strField)
		{
			if (DBNull.Value.Equals(valor)) 
				return null;
			Type destino = typeof(EntUsuArio).GetProperty(strField).PropertyType;
			var miTipo = Nullable.GetUnderlyingType(destino) ?? destino;
			
			try
			{
				TypeConverter tc = TypeDescriptor.GetConverter(miTipo);
				return tc.ConvertFrom(valor);
			}
			catch (Exception)
			{
				return Convert.ChangeType(valor, miTipo);
			}
		}
        
		/// <summary>
		/// 	 Funcion que obtiene la llave primaria unica de la tabla usuario a partir de una cadena
		/// </summary>
		/// <param name="args" type="string[]">
		///     <para>
		/// 		 Cadena desde la que se construye el identificador unico de la tabla usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Identificador unico de la tabla usuario
		/// </returns>
		public string CreatePk(string[] args)
		{
			return args[0];
		}
		
		#endregion 

		#region ObtenerObjeto

		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntUsuArio a partir de la llave primaria
		/// </summary>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public EntUsuArio ObtenerObjeto(Int64 Int64id_usuario)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(EntUsuArio.Fields.id_usuario.ToString());
		
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'" + Int64id_usuario + "'");
			
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntUsuArio a partir del usuario que inserta
		/// </summary>
		/// <param name="strUsuCre">Login o nombre de usuario</param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public EntUsuArio ObtenerObjetoInsertado(string strUsuCre)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(EntUsuArio.Fields.usucre.ToString());
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'" + strUsuCre + "'");
			
			int iIdInsertado = FuncionesMax(EntUsuArio.Fields.id_usuario, arrColumnasWhere, arrValoresWhere);
			
			return ObtenerObjeto(iIdInsertado);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public EntUsuArio ObtenerObjeto(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public EntUsuArio ObtenerObjeto(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count == 1)
				{
					EntUsuArio obj = new EntUsuArio();
					obj = crearObjeto(table.Rows[0]);
					return obj;
				}
				else if (table.Rows.Count > 1)
					throw new Exception("Se ha devuelto mas de un objeto");
				else
					return null;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public EntUsuArio ObtenerObjeto(Hashtable htbFiltro)
		{
			return ObtenerObjeto(htbFiltro, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public EntUsuArio ObtenerObjeto(Hashtable htbFiltro, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count == 1)
				{
					EntUsuArio obj = new EntUsuArio();
					obj = crearObjeto(table.Rows[0]);
					return obj;
				}
				else if (table.Rows.Count > 1)
					throw new Exception("Se ha devuelto mas de un objeto");
				else
					return null;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public EntUsuArio ObtenerObjeto(EntUsuArio.Fields searchField, object searchValue)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public EntUsuArio ObtenerObjeto(EntUsuArio.Fields searchField, object searchValue, string strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un Business Object del Tipo EntUsuArio a partir de su llave promaria
		/// </summary>
		/// <returns>
		/// 	Objeto del Tipo EntUsuArio
		/// </returns>
		public EntUsuArio ObtenerObjeto(Int64 Int64id_usuario, ref CTrans localTrans )
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(EntUsuArio.Fields.id_usuario.ToString());
		
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'" + Int64id_usuario + "'");
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public EntUsuArio ObtenerObjeto(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans localTrans)
		{
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public EntUsuArio ObtenerObjeto(Hashtable htbFiltro, ref CTrans localTrans)
		{
			return ObtenerObjeto(htbFiltro, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public EntUsuArio ObtenerObjeto(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales,  ref CTrans localTrans)
		{
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public EntUsuArio ObtenerObjeto(Hashtable htbFiltro, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count == 1)
				{
					EntUsuArio obj = new EntUsuArio();
					obj = crearObjeto(table.Rows[0]);
					return obj;
				}
				else if (table.Rows.Count > 1)
					throw new Exception("Se ha devuelto mas de un objeto");
				else
					return null;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public EntUsuArio ObtenerObjeto(EntUsuArio.Fields searchField, object searchValue, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public EntUsuArio ObtenerObjeto(EntUsuArio.Fields searchField, object searchValue, string strParamAdicionales, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
		}
		

		#endregion

		#region ObtenerLista

		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerLista()
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerLista(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerLista(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			return ObtenerLista(arrColumnasWhere, arrValoresWhere, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerLista(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearLista(table);
				}
				else
					return new List<EntUsuArio>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerLista(EntUsuArio.Fields searchField, object searchValue)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerLista(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerLista(EntUsuArio.Fields searchField, object searchValue, string strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerLista(arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerLista(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans localTrans)
		{
			return ObtenerLista(arrColumnasWhere, arrValoresWhere, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerLista(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count > 0)
				{
					return crearLista(table);
				}
				else
					return new List<EntUsuArio>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerLista(EntUsuArio.Fields searchField, object searchValue, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerLista(arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerLista(EntUsuArio.Fields searchField, object searchValue, string strParamAdicionales, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerLista(arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerLista(Hashtable htbFiltro)
		{
			return ObtenerLista(htbFiltro, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerLista(Hashtable htbFiltro, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearLista(table);
				}
				else
					return new List<EntUsuArio>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerLista(Hashtable htbFiltro, ref CTrans localTrans)
		{
			return ObtenerLista(htbFiltro, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerLista(Hashtable htbFiltro, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count > 0)
				{
					return crearLista(table);
				}
				else
					return new List<EntUsuArio>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerListaDesdeVista(String strVista)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerListaDesdeVista(String strVista, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerListaDesdeVista(String strVista, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add("*");
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + strVista, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearListaRevisada(table);
				}
				else
					return new List<EntUsuArio>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerListaDesdeVista(String strVista, Hashtable htbFiltro)
		{
			return ObtenerListaDesdeVista(strVista, htbFiltro, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerListaDesdeVista(String strVista, Hashtable htbFiltro, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add("*");
				
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + strVista, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearListaRevisada(table);
				}
				else
					return new List<EntUsuArio>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerListaDesdeVista(String strVista, EntUsuArio.Fields searchField, object searchValue)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerListaDesdeVista(String strVista, EntUsuArio.Fields searchField, object searchValue, string strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerListaDesdeVista(String strVista, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans localTrans)
		{
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerListaDesdeVista(String strVista, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add("*");
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + strVista, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count > 0)
				{
					return crearListaRevisada(table);
				}
				else
					return new List<EntUsuArio>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerListaDesdeVista(String strVista, Hashtable htbFiltro, ref CTrans localTrans)
		{
			return ObtenerListaDesdeVista(strVista, htbFiltro, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerListaDesdeVista(String strVista, Hashtable htbFiltro, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add("*");
				
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + strVista, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count > 0)
				{
					return crearListaRevisada(table);
				}
				else
					return new List<EntUsuArio>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerListaDesdeVista(String strVista, EntUsuArio.Fields searchField, object searchValue, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntUsuArio> ObtenerListaDesdeVista(String strVista, EntUsuArio.Fields searchField, object searchValue, string strParamAdicionales, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
		}
		

		#endregion 

		#region ObtenerCola y Obtener Pila

		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntUsuArio> ObtenerCola()
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerCola(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntUsuArio> ObtenerCola(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			return ObtenerCola(arrColumnasWhere, arrValoresWhere, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntUsuArio> ObtenerCola(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearCola(table);
				}
				else
					return new Queue<EntUsuArio>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntUsuArio> ObtenerCola(EntUsuArio.Fields searchField, object searchValue)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerCola(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntUsuArio> ObtenerCola(EntUsuArio.Fields searchField, object searchValue, string strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerCola(arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntUsuArio> ObtenerCola(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans localTrans)
		{
			return ObtenerCola(arrColumnasWhere, arrValoresWhere, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntUsuArio> ObtenerCola(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count > 0)
				{
					return crearCola(table);
				}
				else
					return new Queue<EntUsuArio>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntUsuArio> ObtenerCola(EntUsuArio.Fields searchField, object searchValue, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerCola(arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Stack que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntUsuArio> ObtenerCola(EntUsuArio.Fields searchField, object searchValue, string strParamAdicionales, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerCola(arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntUsuArio> ObtenerPila()
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerPila(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntUsuArio> ObtenerPila(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			return ObtenerPila(arrColumnasWhere, arrValoresWhere, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntUsuArio> ObtenerPila(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearPila(table);
				}
				else
					return new Stack<EntUsuArio>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntUsuArio> ObtenerPila(EntUsuArio.Fields searchField, object searchValue)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerPila(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntUsuArio> ObtenerPila(EntUsuArio.Fields searchField, object searchValue, string strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerPila(arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntUsuArio> ObtenerPila(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans localTrans)
		{
			return ObtenerPila(arrColumnasWhere, arrValoresWhere, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntUsuArio> ObtenerPila(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count > 0)
				{
					return crearPila(table);
				}
				else
					return new Stack<EntUsuArio>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntUsuArio> ObtenerPila(EntUsuArio.Fields searchField, object searchValue, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerPila(arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntUsuArio> ObtenerPila(EntUsuArio.Fields searchField, object searchValue, string strParamAdicionales, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerPila(arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
		}
		

		#endregion 

		#region ObtenerDataTable

		/// <summary>
		/// 	 Funcion que llena un DataTable con los registros de una tabla usuario
		/// </summary>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de usuario
		/// </returns>
		public DataTable NuevoDataTable()
		{
			try
			{
				DataTable table = new DataTable ();
				DataColumn dc;
				dc = new DataColumn(EntUsuArio.Fields.id_usuario.ToString(),typeof(EntUsuArio).GetProperty(EntUsuArio.Fields.id_usuario.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntUsuArio.Fields.id_rol.ToString(),typeof(EntUsuArio).GetProperty(EntUsuArio.Fields.id_rol.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntUsuArio.Fields.id_sucursal.ToString(),typeof(EntUsuArio).GetProperty(EntUsuArio.Fields.id_sucursal.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntUsuArio.Fields.login.ToString(),typeof(EntUsuArio).GetProperty(EntUsuArio.Fields.login.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntUsuArio.Fields.password.ToString(),typeof(EntUsuArio).GetProperty(EntUsuArio.Fields.password.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntUsuArio.Fields.nombres.ToString(),typeof(EntUsuArio).GetProperty(EntUsuArio.Fields.nombres.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntUsuArio.Fields.apellidos.ToString(),typeof(EntUsuArio).GetProperty(EntUsuArio.Fields.apellidos.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntUsuArio.Fields.apiestado.ToString(),typeof(EntUsuArio).GetProperty(EntUsuArio.Fields.apiestado.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntUsuArio.Fields.usucre.ToString(),typeof(EntUsuArio).GetProperty(EntUsuArio.Fields.usucre.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntUsuArio.Fields.feccre.ToString(),typeof(EntUsuArio).GetProperty(EntUsuArio.Fields.feccre.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntUsuArio.Fields.usumod.ToString(),typeof(EntUsuArio).GetProperty(EntUsuArio.Fields.usumod.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntUsuArio.Fields.fecmod.ToString(),typeof(EntUsuArio).GetProperty(EntUsuArio.Fields.fecmod.ToString()).PropertyType);
				table.Columns.Add(dc);

				
				return table;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que genera un DataTable con determinadas columnas de una usuario
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de usuario
		/// </returns>
		public DataTable NuevoDataTable(ArrayList arrColumnas)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add("'1'");
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add("'2'");
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere);
				
				return table;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registros de una tabla usuario
		/// </summary>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de usuario
		/// </returns>
		public DataTable ObtenerDataTable()
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerDataTable(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registros de una tabla y n condicion WHERE usuario
		/// </summary>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de usuario
		/// </returns>
		public DataTable ObtenerDataTable(String strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerDataTable(arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla usuario
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de usuario
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnas)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add("'1'");
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add("'1'");
				
				return ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla usuario
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="strParametrosAdicionales" type="string">
		///     <para>
		/// 		 Parametros adicionales
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de usuario
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnas, string strParametrosAdicionales)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add("'1'");
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add("'1'");
				
				return ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla usuario
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de usuario
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				return ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla usuario
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de usuario
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnas, Hashtable htbFiltro)
		{
			try
			{
				return ObtenerDataTable(arrColumnas, htbFiltro, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla usuario
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de usuario
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				return ObtenerDataTable(arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla usuario
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de usuario
		/// </returns>
		public DataTable ObtenerDataTable(Hashtable htbFiltro)
		{
			try
			{
				return ObtenerDataTable(htbFiltro, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla usuario
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParametrosAdicionales" type="string">
		///     <para>
		/// 		 Parametros adicionales
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de usuario
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParametrosAdicionales)
		{
			try
			{
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
				
				return table;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla usuario
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParametrosAdicionales" type="string">
		///     <para>
		/// 		 Parametros adicionales
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de usuario
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnas, Hashtable htbFiltro, string strParametrosAdicionales)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
				
				return table;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla usuario
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParametrosAdicionales" type="string">
		///     <para>
		/// 		 Parametros adicionales
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de usuario
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParametrosAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
				
				return ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla usuario
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParametrosAdicionales" type="string">
		///     <para>
		/// 		 Parametros adicionales
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de usuario
		/// </returns>
		public DataTable ObtenerDataTable(Hashtable htbFiltro, string strParametrosAdicionales)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
				
				return ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla usuario
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de usuario
		/// </returns>
		public DataTable ObtenerDataTableOr(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
				return ObtenerDataTableOr(arrColumnas, arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla usuario
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de usuario
		/// </returns>
		public DataTable ObtenerDataTableOr(ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				return ObtenerDataTableOr(arrColumnas, arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla usuario
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParametrosAdicionales" type="string">
		///     <para>
		/// 		 Parametros adicionales
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de usuario
		/// </returns>
		public DataTable ObtenerDataTableOr(ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParametrosAdicionales)
		{
			try
			{
				CConn local = new CConn();
				DataTable table = local.cargarDataTableOr(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
				
				return table;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos a partir de un filtro realizado por algun campo
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <returns>
		/// 	DataTable que cumple con los filtros de los parametros
		/// </returns>
		public DataTable ObtenerDataTable(EntUsuArio.Fields searchField, object searchValue)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				
				return ObtenerDataTable(arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos a partir de un filtro realizado por algun campo
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParametrosAdicionales" type="string">
		///     <para>
		/// 		 Parametros adicionales
		///     </para>
		/// </param>
		/// <returns>
		/// 	DataTable que cumple con los filtros de los parametros
		/// </returns>
		public DataTable ObtenerDataTable(EntUsuArio.Fields searchField, object searchValue, string strParametrosAdicionales)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				
				return ObtenerDataTable(arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos a partir de un filtro realizado por algun campo
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParametrosAdicionales" type="string">
		///     <para>
		/// 		 Parametros adicionales
		///     </para>
		/// </param>
		/// <returns>
		/// 	DataTable que cumple con los filtros de los parametros
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnas, EntUsuArio.Fields searchField, object searchValue, string strParametrosAdicionales)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				
				return ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos a partir de un filtro realizado por algun campo
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <returns>
		/// 	DataTable que cumple con los filtros de los parametros
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnas, EntUsuArio.Fields searchField, object searchValue)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				
				return ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		

		#endregion 

		#region ObtenerDiccionario

		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntUsuArio> ObtenerDiccionario()
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntUsuArio> ObtenerDiccionario(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntUsuArio> ObtenerDiccionario(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearDiccionario(table);
				}
				else
					return new Dictionary<string, EntUsuArio>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntUsuArio> ObtenerDiccionario(EntUsuArio.Fields searchField, object searchValue)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntUsuArio> ObtenerDiccionario(EntUsuArio.Fields searchField, object searchValue, string strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntUsuArio> ObtenerDiccionario(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans localTrans)
		{
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntUsuArio> ObtenerDiccionario(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count > 0)
				{
					return crearDiccionario(table);
				}
				else
					return new Dictionary<string, EntUsuArio>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntUsuArio> ObtenerDiccionario(EntUsuArio.Fields searchField, object searchValue, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntUsuArio a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntUsuArio> ObtenerDiccionario(EntUsuArio.Fields searchField, object searchValue, string strParamAdicionales, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
		}
		
		public Dictionary<String, EntUsuArio> ObtenerDiccionarioKey(EntUsuArio.Fields dicKey)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerDiccionarioKey(arrColumnasWhere, arrValoresWhere, dicKey);
		}
		
		public Dictionary<String, EntUsuArio> ObtenerDiccionarioKey(String strParamAdic, EntUsuArio.Fields dicKey)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerDiccionarioKey(arrColumnasWhere, arrValoresWhere, strParamAdic, dicKey);
		}
		
		public Dictionary<String, EntUsuArio> ObtenerDiccionarioKey(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, EntUsuArio.Fields dicKey)
		{
			return ObtenerDiccionarioKey(arrColumnasWhere, arrValoresWhere, "", dicKey);
		}
		
		public Dictionary<String, EntUsuArio> ObtenerDiccionarioKey(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales, EntUsuArio.Fields dicKey)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearDiccionario(table, dicKey);
				}
				else
					return new Dictionary<string, EntUsuArio>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		public Dictionary<String, EntUsuArio> ObtenerDiccionarioKey(EntUsuArio.Fields searchField, object searchValue, EntUsuArio.Fields dicKey)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerDiccionarioKey(arrColumnasWhere, arrValoresWhere, dicKey);
		}
		
		public Dictionary<String, EntUsuArio> ObtenerDiccionarioKey(EntUsuArio.Fields searchField, object searchValue, string strParamAdicionales, EntUsuArio.Fields dicKey)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerDiccionarioKey(arrColumnasWhere, arrValoresWhere, strParamAdicionales, dicKey);
		}
		

		#endregion 

		#region ObjetoASp

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla usuario a partir de una clase del tipo Eusuario
		/// </summary>
		/// <param name="strNombreSp" type="System.string">
		///     <para>
		/// 		 Nombre del Procedimiento a ejecutar sobre el SP
		///     </para>
		/// </param>
		/// <param name="obj" type="Entidades.EntUsuArio">
		///     <para>
		/// 		 Clase desde la que se va a ejecutar el SP de la tabla usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor de registros afectados en el Procedimiento de la tabla usuario
		/// </returns>
		public int EjecutarSpDesdeObjeto(string strNombreSp, EntUsuArio obj)
		{
			try
			{
				
				ArrayList arrNombreParam = new ArrayList();
				arrNombreParam.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.id_rol.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.login.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.password.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.nombres.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.apellidos.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.apiestado.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.usucre.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.feccre.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.usumod.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.fecmod.ToString());
				
				ArrayList arrValoresParam = new ArrayList();
				arrValoresParam.Add(obj.id_usuario == null ? null : "'" + obj.id_usuario + "'");
				arrValoresParam.Add(obj.id_rol);
				arrValoresParam.Add(obj.id_sucursal);
				arrValoresParam.Add(obj.login == null ? null : "'" + obj.login + "'");
				arrValoresParam.Add(obj.password == null ? null : "'" + obj.password + "'");
				arrValoresParam.Add(obj.nombres == null ? null : "'" + obj.nombres + "'");
				arrValoresParam.Add(obj.apellidos == null ? null : "'" + obj.apellidos + "'");
				arrValoresParam.Add(obj.apiestado == null ? null : "'" + obj.apiestado + "'");
				arrValoresParam.Add(obj.usucre == null ? null : "'" + obj.usucre + "'");
				arrValoresParam.Add(obj.feccre == null ? null : "'" + Convert.ToDateTime(obj.feccre).ToString(CParametros.ParFormatoFechaHora) + "'");
				arrValoresParam.Add(obj.usumod == null ? null : "'" + obj.usumod + "'");
				arrValoresParam.Add(obj.fecmod == null ? null : "'" + Convert.ToDateTime(obj.fecmod).ToString(CParametros.ParFormatoFechaHora) + "'");

				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				return local.execStoreProcedure(strNombreSp, arrNombreParam, arrValoresParam);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla usuario a partir de una clase del tipo Eusuario
		/// </summary>
		/// <param name="strNombreSp" type="System.string">
		///     <para>
		/// 		 Nombre del Procedimiento a ejecutar sobre el SP
		///     </para>
		/// </param>
		/// <param name="obj" type="Entidades.EntUsuArio">
		///     <para>
		/// 		 Clase desde la que se va a ejecutar el SP de la tabla usuario
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor de registros afectados en el Procedimiento de la tabla usuario
		/// </returns>
		public int EjecutarSpDesdeObjeto(string strNombreSp, EntUsuArio obj, ref CTrans localTrans)
		{
			try
			{
				
				ArrayList arrNombreParam = new ArrayList();
				arrNombreParam.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.id_rol.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.login.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.password.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.nombres.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.apellidos.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.apiestado.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.usucre.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.feccre.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.usumod.ToString());
				arrNombreParam.Add(EntUsuArio.Fields.fecmod.ToString());
				
				ArrayList arrValoresParam = new ArrayList();
				arrValoresParam.Add(obj.id_usuario == null ? null : "'" + obj.id_usuario + "'");
				arrValoresParam.Add(obj.id_rol);
				arrValoresParam.Add(obj.id_sucursal);
				arrValoresParam.Add(obj.login == null ? null : "'" + obj.login + "'");
				arrValoresParam.Add(obj.password == null ? null : "'" + obj.password + "'");
				arrValoresParam.Add(obj.nombres == null ? null : "'" + obj.nombres + "'");
				arrValoresParam.Add(obj.apellidos == null ? null : "'" + obj.apellidos + "'");
				arrValoresParam.Add(obj.apiestado == null ? null : "'" + obj.apiestado + "'");
				arrValoresParam.Add(obj.usucre == null ? null : "'" + obj.usucre + "'");
				arrValoresParam.Add(obj.feccre == null ? null : "'" + Convert.ToDateTime(obj.feccre).ToString(CParametros.ParFormatoFechaHora) + "'");
				arrValoresParam.Add(obj.usumod == null ? null : "'" + obj.usumod + "'");
				arrValoresParam.Add(obj.fecmod == null ? null : "'" + Convert.ToDateTime(obj.fecmod).ToString(CParametros.ParFormatoFechaHora) + "'");

				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				return local.execStoreProcedure(strNombreSp, arrNombreParam, arrValoresParam, ref localTrans);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}


		#endregion 

		#region FuncionesAgregadas

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] COUNT
		/// </summary>
		/// <param name="refField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesCount(EntUsuArio.Fields refField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(1);
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(1);
				
				return FuncionesCount(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] COUNT
		/// </summary>
		/// <param name="refField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="whereField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Columna que va a filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="valueField" type="System.Object">
		///     <para>
		/// 		 Valor para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesCount(EntUsuArio.Fields refField, EntUsuArio.Fields whereField, object valueField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(whereField.ToString());
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(valueField.ToString());
				
				return FuncionesCount(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] COUNT
		/// </summary>
		/// <param name="refField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesCount(EntUsuArio.Fields refField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add("count(" + refField + ")");
				DataTable dtTemp = ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere);
				if (dtTemp.Rows.Count == 0)
					throw new Exception("La consulta no ha devuelto resultados.");
				if (dtTemp.Rows.Count > 1)
					throw new Exception("Se ha devuelto mas de una fila.");
				if (dtTemp.Rows[0][0] == null)
					return 0;
				if (dtTemp.Rows[0][0] == "")
					return 0;
				return int.Parse(dtTemp.Rows[0][0].ToString());
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] MIN
		/// </summary>
		/// <param name="refField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesMin(EntUsuArio.Fields refField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(1);
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(1);
				
				return FuncionesMin(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] MIN
		/// </summary>
		/// <param name="refField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="whereField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Columna que va a filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="valueField" type="System.Object">
		///     <para>
		/// 		 Valor para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesMin(EntUsuArio.Fields refField, EntUsuArio.Fields whereField, object valueField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(whereField.ToString());
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(valueField.ToString());
				
				return FuncionesMin(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] MIN
		/// </summary>
		/// <param name="refField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesMin(EntUsuArio.Fields refField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add("min(" + refField + ")");
				DataTable dtTemp = ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere);
				if (dtTemp.Rows.Count == 0)
					throw new Exception("La consulta no ha devuelto resultados.");
				if (dtTemp.Rows.Count > 1)
					throw new Exception("Se ha devuelto mas de una fila.");
				if (dtTemp.Rows[0][0] == null)
					return 0;
				if (dtTemp.Rows[0][0] == "")
					return 0;
				return int.Parse(dtTemp.Rows[0][0].ToString());
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] MAX
		/// </summary>
		/// <param name="refField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesMax(EntUsuArio.Fields refField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(1);
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(1);
				
				return FuncionesMax(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] MAX
		/// </summary>
		/// <param name="refField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="whereField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Columna que va a filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="valueField" type="System.Object">
		///     <para>
		/// 		 Valor para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesMax(EntUsuArio.Fields refField, EntUsuArio.Fields whereField, object valueField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(whereField.ToString());
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(valueField.ToString());
				
				return FuncionesMax(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] MAX
		/// </summary>
		/// <param name="refField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesMax(EntUsuArio.Fields refField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add("max(" + refField + ")");
				DataTable dtTemp = ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere);
				if (dtTemp.Rows.Count == 0)
					throw new Exception("La consulta no ha devuelto resultados.");
				if (dtTemp.Rows.Count > 1)
					throw new Exception("Se ha devuelto mas de una fila.");
				if (dtTemp.Rows[0][0] == null)
					return 0;
				if (dtTemp.Rows[0][0] == "")
					return 0;
				return int.Parse(dtTemp.Rows[0][0].ToString());
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] SUM
		/// </summary>
		/// <param name="refField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesSum(EntUsuArio.Fields refField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(1);
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(1);
				
				return FuncionesSum(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] SUM
		/// </summary>
		/// <param name="refField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="whereField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Columna que va a filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="valueField" type="System.Object">
		///     <para>
		/// 		 Valor para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesSum(EntUsuArio.Fields refField, EntUsuArio.Fields whereField, object valueField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(whereField.ToString());
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(valueField.ToString());
				
				return FuncionesSum(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] SUM
		/// </summary>
		/// <param name="refField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesSum(EntUsuArio.Fields refField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add("sum(" + refField + ")");
				DataTable dtTemp = ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere);
				if (dtTemp.Rows.Count == 0)
					throw new Exception("La consulta no ha devuelto resultados.");
				if (dtTemp.Rows.Count > 1)
					throw new Exception("Se ha devuelto mas de una fila.");
				if (dtTemp.Rows[0][0] == null)
					return 0;
				if (dtTemp.Rows[0][0] == "")
					return 0;
				return int.Parse(dtTemp.Rows[0][0].ToString());
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] AVG
		/// </summary>
		/// <param name="refField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesAvg(EntUsuArio.Fields refField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(1);
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(1);
				
				return FuncionesAvg(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] AVG
		/// </summary>
		/// <param name="refField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="whereField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Columna que va a filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="valueField" type="System.Object">
		///     <para>
		/// 		 Valor para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesAvg(EntUsuArio.Fields refField, EntUsuArio.Fields whereField, object valueField)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(whereField.ToString());
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(valueField.ToString());
				
				return FuncionesAvg(refField, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que devuelve el resultado de la funcion [SQL] AVG
		/// </summary>
		/// <param name="refField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntUsuArio que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesAvg(EntUsuArio.Fields refField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add("avg(" + refField + ")");
				DataTable dtTemp = ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere);
				if (dtTemp.Rows.Count == 0)
					throw new Exception("La consulta no ha devuelto resultados.");
				if (dtTemp.Rows.Count > 1)
					throw new Exception("Se ha devuelto mas de una fila.");
				if (dtTemp.Rows[0][0] == null)
					return 0;
				if (dtTemp.Rows[0][0] == "")
					return 0;
				return int.Parse(dtTemp.Rows[0][0].ToString());
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}


		#endregion 

		#region ABMs

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla usuario a partir de una clase del tipo Eusuario
		/// </summary>
		/// <param name="obj" type="Entidades.EntUsuArio">
		///     <para>
		/// 		 Clase desde la que se van a insertar los valores a la tabla usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionusuario
		/// </returns>
		public bool Insert(EntUsuArio obj, bool bValidar = true)
		{
			try
			{
				ArrayList arrNombreParam = new ArrayList();
				ArrayList arrValoresParam = new ArrayList();
				arrNombreParam.Add("id_usuario");
				arrValoresParam.Add(null);
				arrNombreParam.Add(EntUsuArio.Fields.id_rol.ToString());
				arrValoresParam.Add(obj.id_rol);
				
				arrNombreParam.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrValoresParam.Add(obj.id_sucursal);
				
				arrNombreParam.Add(EntUsuArio.Fields.login.ToString());
				arrValoresParam.Add(obj.login);
				
				arrNombreParam.Add(EntUsuArio.Fields.password.ToString());
				arrValoresParam.Add(obj.password);
				
				arrNombreParam.Add(EntUsuArio.Fields.nombres.ToString());
				arrValoresParam.Add(obj.nombres);
				
				arrNombreParam.Add(EntUsuArio.Fields.apellidos.ToString());
				arrValoresParam.Add(obj.apellidos);
				
				arrNombreParam.Add(EntUsuArio.Fields.usucre.ToString());
				arrValoresParam.Add(obj.usucre);
				
				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				string nombreSp = CListadoSP.UsuArio.sp_usuario_Ins.ToString();
				return (local.execStoreProcedure(nombreSp, arrNombreParam, arrValoresParam) > 0);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla usuario a partir de una clase del tipo Eusuario
		/// </summary>
		/// <param name="obj" type="Entidades.EntUsuArio">
		///     <para>
		/// 		 Clase desde la que se van a insertar los valores a la tabla usuario
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionusuario
		/// </returns>
		public bool Insert(EntUsuArio obj, ref CTrans localTrans, bool bValidar = true)
		{
			try
			{
				ArrayList arrNombreParam = new ArrayList();
				ArrayList arrValoresParam = new ArrayList();
				arrNombreParam.Add("id_usuario");
				arrValoresParam.Add("");
				arrNombreParam.Add(EntUsuArio.Fields.id_rol.ToString());
				arrValoresParam.Add(obj.id_rol);
				
				arrNombreParam.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrValoresParam.Add(obj.id_sucursal);
				
				arrNombreParam.Add(EntUsuArio.Fields.login.ToString());
				arrValoresParam.Add(obj.login);
				
				arrNombreParam.Add(EntUsuArio.Fields.password.ToString());
				arrValoresParam.Add(obj.password);
				
				arrNombreParam.Add(EntUsuArio.Fields.nombres.ToString());
				arrValoresParam.Add(obj.nombres);
				
				arrNombreParam.Add(EntUsuArio.Fields.apellidos.ToString());
				arrValoresParam.Add(obj.apellidos);
				
				arrNombreParam.Add(EntUsuArio.Fields.usucre.ToString());
				arrValoresParam.Add(obj.usucre);
				
				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				string nombreSp = CListadoSP.UsuArio.sp_usuario_Ins.ToString();
				return (local.execStoreProcedure(nombreSp, arrNombreParam, arrValoresParam, ref localTrans) > 0);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla usuario a partir de una clase del tipo Eusuario
		/// </summary>
		/// <param name="obj" type="Entidades.EntUsuArio">
		///     <para>
		/// 		 Clase desde la que se van a insertar los valores a la tabla usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor que indica la cantidad de registros actualizados en usuario
		/// </returns>
		public int Update(EntUsuArio obj, bool bValidar = true)
		{
			try
			{
				ArrayList arrNombreParam = new ArrayList();
				ArrayList arrValoresParam = new ArrayList();
				arrNombreParam.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrValoresParam.Add(obj.id_usuario);
				
				arrNombreParam.Add(EntUsuArio.Fields.id_rol.ToString());
				arrValoresParam.Add(obj.id_rol);
				
				arrNombreParam.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrValoresParam.Add(obj.id_sucursal);
				
				arrNombreParam.Add(EntUsuArio.Fields.login.ToString());
				arrValoresParam.Add(obj.login);
				
				arrNombreParam.Add(EntUsuArio.Fields.password.ToString());
				arrValoresParam.Add(obj.password);
				
				arrNombreParam.Add(EntUsuArio.Fields.nombres.ToString());
				arrValoresParam.Add(obj.nombres);
				
				arrNombreParam.Add(EntUsuArio.Fields.apellidos.ToString());
				arrValoresParam.Add(obj.apellidos);
				
				arrNombreParam.Add(EntUsuArio.Fields.usumod.ToString());
				arrValoresParam.Add(obj.usumod);
				
				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				string nombreSp = CListadoSP.UsuArio.sp_usuario_Upd.ToString();
				return local.execStoreProcedure(nombreSp, arrNombreParam, arrValoresParam);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla usuario a partir de una clase del tipo Eusuario
		/// </summary>
		/// <param name="obj" type="Entidades.EntUsuArio">
		///     <para>
		/// 		 Clase desde la que se van a insertar los valores a la tabla usuario
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionusuario
		/// </returns>
		public int Update(EntUsuArio obj, ref CTrans localTrans, bool bValidar = true)
		{
			try
			{
				ArrayList arrNombreParam = new ArrayList();
				ArrayList arrValoresParam = new ArrayList();
				arrNombreParam.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrValoresParam.Add(obj.id_usuario);
				
				arrNombreParam.Add(EntUsuArio.Fields.id_rol.ToString());
				arrValoresParam.Add(obj.id_rol);
				
				arrNombreParam.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrValoresParam.Add(obj.id_sucursal);
				
				arrNombreParam.Add(EntUsuArio.Fields.login.ToString());
				arrValoresParam.Add(obj.login);
				
				arrNombreParam.Add(EntUsuArio.Fields.password.ToString());
				arrValoresParam.Add(obj.password);
				
				arrNombreParam.Add(EntUsuArio.Fields.nombres.ToString());
				arrValoresParam.Add(obj.nombres);
				
				arrNombreParam.Add(EntUsuArio.Fields.apellidos.ToString());
				arrValoresParam.Add(obj.apellidos);
				
				arrNombreParam.Add(EntUsuArio.Fields.usumod.ToString());
				arrValoresParam.Add(obj.usumod);
				
				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				string nombreSp = CListadoSP.UsuArio.sp_usuario_Upd.ToString();
				return local.execStoreProcedure(nombreSp, arrNombreParam, arrValoresParam, ref localTrans);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla usuario a partir de una clase del tipo Eusuario
		/// </summary>
		/// <param name="obj" type="Entidades.EntUsuArio">
		///     <para>
		/// 		 Clase desde la que se van a insertar los valores a la tabla usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionusuario
		/// </returns>
		public int Delete(EntUsuArio obj, bool bValidar = true)
		{
			try
			{
				ArrayList arrNombreParam = new ArrayList();
				ArrayList arrValoresParam = new ArrayList();
				arrNombreParam.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrValoresParam.Add(obj.id_usuario);
				
				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				string nombreSp = CListadoSP.UsuArio.sp_usuario_Del.ToString();
				return local.execStoreProcedure(nombreSp, arrNombreParam, arrValoresParam);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla usuario a partir de una clase del tipo Eusuario
		/// </summary>
		/// <param name="obj" type="Entidades.EntUsuArio">
		///     <para>
		/// 		 Clase desde la que se van a insertar los valores a la tabla usuario
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionusuario
		/// </returns>
		public int Delete(EntUsuArio obj, ref CTrans localTrans, bool bValidar = true)
		{
			try
			{
				ArrayList arrNombreParam = new ArrayList();
				ArrayList arrValoresParam = new ArrayList();
				arrNombreParam.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrValoresParam.Add(obj.id_usuario);
				
				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				string nombreSp = CListadoSP.UsuArio.sp_usuario_Del.ToString();
				return local.execStoreProcedure(nombreSp, arrNombreParam, arrValoresParam, ref localTrans);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta o actualiza un registro un nuevo registro en la tabla usuario a partir de una clase del tipo Eusuario
		/// </summary>
		/// <param name="obj" type="Entidades.EntUsuArio">
		///     <para>
		/// 		 Clase desde la que se van a insertar o actualizar los valores a la tabla usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionusuario
		/// </returns>
		public int InsertUpdate(EntUsuArio obj)
		{
			try
			{
				bool esInsertar = true;
				
					esInsertar = (esInsertar && (obj.id_usuario == null));
				
				if (esInsertar)
					return Insert(obj) ? 1 : 0;
				else
					return Update(obj);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta o actualiza un registro un nuevo registro en la tabla usuario a partir de una clase del tipo Eusuario
		/// </summary>
		/// <param name="obj" type="Entidades.EntUsuArio">
		///     <para>
		/// 		 Clase desde la que se van a insertar o actualizar los valores a la tabla usuario
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion usuario
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionusuario
		/// </returns>
		public int InsertUpdate(EntUsuArio obj, ref CTrans localTrans)
		{
			try
			{
				bool esInsertar = false;
				
					esInsertar = (esInsertar && (obj.id_usuario == null));
				
				if (esInsertar)
					return Insert(obj, ref localTrans) ? 1 : 0;
				else
					return Update(obj, ref localTrans);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}


		#endregion 

		#region Llenado de elementos

		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla usuario
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo DropDownList en el que se van a cargar los datos de la tabla usuario
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(1);
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(1);
				
				CargarDropDownList(ref cmb, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla usuario
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo DropDownList en el que se van a cargar los datos de la tabla usuario
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(1);
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(1);
				
				CargarDropDownList(ref cmb, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla usuario
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo DropDownList en el que se van a cargar los datos de la tabla usuario
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				CargarDropDownList(ref cmb, arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla usuario
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo DropDownList en el que se van a cargar los datos de la tabla usuario
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla usuario
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla usuario
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntUsuArio.Fields valueField, EntUsuArio.Fields textField)
		{
			try
			{
				CargarDropDownList(ref cmb, valueField, textField, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla usuario
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla usuario
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntUsuArio.Fields valueField, EntUsuArio.Fields textField, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(valueField.ToString());
				arrColumnas.Add(textField.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(1);
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(1);
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla usuario
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla usuario
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="String">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntUsuArio.Fields valueField, String textField)
		{
			try
			{
				CargarDropDownList(ref cmb, valueField, textField, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla usuario
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla usuario
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="String">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntUsuArio.Fields valueField, String textField, String strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(valueField.ToString());
				arrColumnas.Add(textField);
				
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(1);
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(1);
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla usuario
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla usuario
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntUsuArio.Fields valueField, EntUsuArio.Fields textField, EntUsuArio.Fields searchField, object searchValue)
		{
			try
			{
				CargarDropDownList(ref cmb, valueField, textField.ToString(), searchField, searchValue);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla usuario
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla usuario
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="String">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntUsuArio.Fields valueField, String textField, EntUsuArio.Fields searchField, object searchValue)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(valueField.ToString());
				arrColumnas.Add(textField);
				
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla usuario
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla usuario
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntUsuArio.Fields valueField, EntUsuArio.Fields textField, EntUsuArio.Fields searchField, object searchValue, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(valueField.ToString());
				arrColumnas.Add(textField.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla usuario
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla usuario
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchField" type="String">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntUsuArio.Fields valueField, String textField, EntUsuArio.Fields searchField, object searchValue, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(valueField.ToString());
				arrColumnas.Add(textField);
				
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla usuario
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla usuario
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntUsuArio.Fields valueField, EntUsuArio.Fields textField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(valueField.ToString());
				arrColumnas.Add(textField.ToString());
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla usuario
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla usuario
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="String">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntUsuArio.Fields valueField, String textField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(valueField.ToString());
				arrColumnas.Add(textField);
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla usuario
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla usuario
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntUsuArio.Fields valueField, EntUsuArio.Fields textField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(valueField.ToString());
				arrColumnas.Add(textField.ToString());
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla usuario
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla usuario
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="String">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntUsuArio.Fields valueField, String textField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(valueField.ToString());
				arrColumnas.Add(textField);
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla usuario
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla usuario
		///     </para>
		/// </param>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Columns.Count > 0)
				{
					cmb.DataValueField = table.Columns[0].ColumnName;
					cmb.DataTextField = table.Columns[1].ColumnName;
					cmb.DataSource = table;
					cmb.DataBind();

				}
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla usuario
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
			
				
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add("'1'");
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add("'1'");
				
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla usuario
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		  Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, String strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
			
				
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add("'1'");
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add("'1'");
				
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla usuario
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, ArrayList arrColumnas)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add("'1'");
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add("'1'");
				
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla usuario
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="string">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, ArrayList arrColumnas, String strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add("'1'");
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add("'1'");
				
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla usuario
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
			
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla usuario
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="string">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, String strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
			
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla usuario
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla usuario
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="string">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				CConn local = new CConn();
				DbDataReader dsReader = local.cargarDataReaderAnd(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				dtg.DataSource = dsReader;
				dtg.DataBind();

			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla usuario
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, EntUsuArio.Fields searchField, object searchValue)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla usuario
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="string">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, EntUsuArio.Fields searchField, object searchValue, String strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntUsuArio.Fields.id_usuario.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_rol.ToString());
				arrColumnas.Add(EntUsuArio.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntUsuArio.Fields.login.ToString());
				arrColumnas.Add(EntUsuArio.Fields.password.ToString());
				arrColumnas.Add(EntUsuArio.Fields.nombres.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apellidos.ToString());
				arrColumnas.Add(EntUsuArio.Fields.apiestado.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usucre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.feccre.ToString());
				arrColumnas.Add(EntUsuArio.Fields.usumod.ToString());
				arrColumnas.Add(EntUsuArio.Fields.fecmod.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla usuario
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView
		///     </para>
		/// </param>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, ArrayList arrColumnas, EntUsuArio.Fields searchField, object searchValue)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla usuario
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView
		///     </para>
		/// </param>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntUsuArio.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="string">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, ArrayList arrColumnas, EntUsuArio.Fields searchField, object searchValue, String strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnasWhere = new ArrayList();
				arrColumnasWhere.Add(searchField.ToString());
				
				ArrayList arrValoresWhere = new ArrayList();
				arrValoresWhere.Add(searchValue);
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla usuario
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarGridViewOr(ref GridView dtg, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				CargarGridViewOr(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla usuario
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView 
		///     </para>
		/// </param>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="arrValoresWhere" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="strParametrosAdicionales" type="string">
		///     <para>
		/// 		 Array de las valores WHERE para filtrar el resultado
		///     </para>
		/// </param>
		public void CargarGridViewOr(ref GridView dtg, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParametrosAdicionales)
		{
			try
			{
				CConn local = new CConn();
				DbDataReader dsReader = local.cargarDataReaderOr(CParametros.schema + EntUsuArio.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
				
				dtg.DataSource = dsReader;
				dtg.DataBind();

			}
			catch (Exception exp)
			{
				throw exp;
			}
		}


		#endregion 

		#region Funciones Internas

		/// <summary>
		/// 	 Funcion que devuelve un objeto a partir de un DataRow
		/// </summary>
		/// <param name="row" type="System.Data.DataRow">
		///     <para>
		/// 		 DataRow con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Objeto usuario
		/// </returns>
		internal EntUsuArio crearObjeto(DataRow row)
		{
			EntUsuArio obj = new EntUsuArio();
			obj.id_usuario = (long) GetColumnType(row[EntUsuArio.Fields.id_usuario.ToString()], EntUsuArio.Fields.id_usuario);
			obj.id_rol = (int) GetColumnType(row[EntUsuArio.Fields.id_rol.ToString()], EntUsuArio.Fields.id_rol);
			obj.id_sucursal = (int) GetColumnType(row[EntUsuArio.Fields.id_sucursal.ToString()], EntUsuArio.Fields.id_sucursal);
			obj.login = (string) GetColumnType(row[EntUsuArio.Fields.login.ToString()], EntUsuArio.Fields.login);
			obj.password = (string) GetColumnType(row[EntUsuArio.Fields.password.ToString()], EntUsuArio.Fields.password);
			obj.nombres = (string) GetColumnType(row[EntUsuArio.Fields.nombres.ToString()], EntUsuArio.Fields.nombres);
			obj.apellidos = (string) GetColumnType(row[EntUsuArio.Fields.apellidos.ToString()], EntUsuArio.Fields.apellidos);
			obj.apiestado = (string) GetColumnType(row[EntUsuArio.Fields.apiestado.ToString()], EntUsuArio.Fields.apiestado);
			obj.usucre = (string) GetColumnType(row[EntUsuArio.Fields.usucre.ToString()], EntUsuArio.Fields.usucre);
			obj.feccre = (DateTime) GetColumnType(row[EntUsuArio.Fields.feccre.ToString()], EntUsuArio.Fields.feccre);
			obj.usumod = (string) GetColumnType(row[EntUsuArio.Fields.usumod.ToString()], EntUsuArio.Fields.usumod);
			obj.fecmod = (DateTime?) GetColumnType(row[EntUsuArio.Fields.fecmod.ToString()], EntUsuArio.Fields.fecmod);
			return obj;
		}

		/// <summary>
		/// 	 Funcion que devuelve un objeto a partir de un DataRow
		/// </summary>
		/// <param name="row" type="System.Data.DataRow">
		///     <para>
		/// 		 DataRow con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Objeto usuario
		/// </returns>
		internal EntUsuArio crearObjetoRevisado(DataRow row)
		{
			EntUsuArio obj = new EntUsuArio();
			if (row.Table.Columns.Contains(EntUsuArio.Fields.id_usuario.ToString()))
				obj.id_usuario = (long) GetColumnType(row[EntUsuArio.Fields.id_usuario.ToString()], EntUsuArio.Fields.id_usuario);
			if (row.Table.Columns.Contains(EntUsuArio.Fields.id_rol.ToString()))
				obj.id_rol = (int) GetColumnType(row[EntUsuArio.Fields.id_rol.ToString()], EntUsuArio.Fields.id_rol);
			if (row.Table.Columns.Contains(EntUsuArio.Fields.id_sucursal.ToString()))
				obj.id_sucursal = (int) GetColumnType(row[EntUsuArio.Fields.id_sucursal.ToString()], EntUsuArio.Fields.id_sucursal);
			if (row.Table.Columns.Contains(EntUsuArio.Fields.login.ToString()))
				obj.login = (string) GetColumnType(row[EntUsuArio.Fields.login.ToString()], EntUsuArio.Fields.login);
			if (row.Table.Columns.Contains(EntUsuArio.Fields.password.ToString()))
				obj.password = (string) GetColumnType(row[EntUsuArio.Fields.password.ToString()], EntUsuArio.Fields.password);
			if (row.Table.Columns.Contains(EntUsuArio.Fields.nombres.ToString()))
				obj.nombres = (string) GetColumnType(row[EntUsuArio.Fields.nombres.ToString()], EntUsuArio.Fields.nombres);
			if (row.Table.Columns.Contains(EntUsuArio.Fields.apellidos.ToString()))
				obj.apellidos = (string) GetColumnType(row[EntUsuArio.Fields.apellidos.ToString()], EntUsuArio.Fields.apellidos);
			if (row.Table.Columns.Contains(EntUsuArio.Fields.apiestado.ToString()))
				obj.apiestado = (string) GetColumnType(row[EntUsuArio.Fields.apiestado.ToString()], EntUsuArio.Fields.apiestado);
			if (row.Table.Columns.Contains(EntUsuArio.Fields.usucre.ToString()))
				obj.usucre = (string) GetColumnType(row[EntUsuArio.Fields.usucre.ToString()], EntUsuArio.Fields.usucre);
			if (row.Table.Columns.Contains(EntUsuArio.Fields.feccre.ToString()))
				obj.feccre = (DateTime) GetColumnType(row[EntUsuArio.Fields.feccre.ToString()], EntUsuArio.Fields.feccre);
			if (row.Table.Columns.Contains(EntUsuArio.Fields.usumod.ToString()))
				obj.usumod = (string) GetColumnType(row[EntUsuArio.Fields.usumod.ToString()], EntUsuArio.Fields.usumod);
			if (row.Table.Columns.Contains(EntUsuArio.Fields.fecmod.ToString()))
				obj.fecmod = (DateTime?) GetColumnType(row[EntUsuArio.Fields.fecmod.ToString()], EntUsuArio.Fields.fecmod);
			return obj;
		}

		/// <summary>
		/// 	 Funcion que crea una Lista de objetos a partir de un DataTable
		/// </summary>
		/// <param name="dtusuario" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Lista de Objetos usuario
		/// </returns>
		internal List<EntUsuArio> crearLista(DataTable dtusuario)
		{
			List<EntUsuArio> list = new List<EntUsuArio>();
			
			EntUsuArio obj = new EntUsuArio();
			foreach (DataRow row in dtusuario.Rows)
			{
				obj = crearObjeto(row);
				list.Add(obj);
			}
			return list;
		}
		
		/// <summary>
		/// 	 Funcion que crea una Lista de objetos a partir de un DataTable y con solo algunas columnas
		/// </summary>
		/// <param name="dtusuario" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Lista de Objetos usuario
		/// </returns>
		internal List<EntUsuArio> crearListaRevisada(DataTable dtusuario)
		{
			List<EntUsuArio> list = new List<EntUsuArio>();
			
			EntUsuArio obj = new EntUsuArio();
			foreach (DataRow row in dtusuario.Rows)
			{
				obj = crearObjetoRevisado(row);
				list.Add(obj);
			}
			return list;
		}
		
		/// <summary>
		/// 	 Funcion que crea una Lista de objetos a partir de un DataTable
		/// </summary>
		/// <param name="dtusuario" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Cola de Objetos usuario
		/// </returns>
		internal Queue<EntUsuArio> crearCola(DataTable dtusuario)
		{
			Queue<EntUsuArio> cola = new Queue<EntUsuArio>();
			
			EntUsuArio obj = new EntUsuArio();
			foreach (DataRow row in dtusuario.Rows)
			{
				obj = crearObjeto(row);
				cola.Enqueue(obj);
			}
			return cola;
		}
		
		/// <summary>
		/// 	 Funcion que crea una Lista de objetos a partir de un DataTable
		/// </summary>
		/// <param name="dtusuario" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Pila de Objetos usuario
		/// </returns>
		internal Stack<EntUsuArio> crearPila(DataTable dtusuario)
		{
			Stack<EntUsuArio> pila = new Stack<EntUsuArio>();
			
			EntUsuArio obj = new EntUsuArio();
			foreach (DataRow row in dtusuario.Rows)
			{
				obj = crearObjeto(row);
				pila.Push(obj);
			}
			return pila;
		}
		
		/// <summary>
		/// 	 Funcion que crea un Dicionario a partir de un DataTable
		/// </summary>
		/// <param name="dtusuario" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Diccionario de Objetos usuario
		/// </returns>
		internal Dictionary<String, EntUsuArio> crearDiccionario(DataTable dtusuario)
		{
			Dictionary<String, EntUsuArio>  miDic = new Dictionary<String, EntUsuArio>();
			
			EntUsuArio obj = new EntUsuArio();
			foreach (DataRow row in dtusuario.Rows)
			{
				obj = crearObjeto(row);
				miDic.Add(obj.id_usuario.ToString(), obj);
			}
			return miDic;
		}
		
		/// <summary>
		/// 	 Funcion que crea un Dicionario a partir de un DataTable
		/// </summary>
		/// <param name="dtusuario" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 HashTable de Objetos usuario
		/// </returns>
		internal Hashtable crearHashTable(DataTable dtusuario)
		{
			Hashtable miTabla = new Hashtable();
			
			EntUsuArio obj = new EntUsuArio();
			foreach (DataRow row in dtusuario.Rows)
			{
				obj = crearObjeto(row);
				miTabla.Add(obj.id_usuario.ToString(), obj);
			}
			return miTabla;
		}
		
		/// <summary>
		/// 	 Funcion que crea un Dicionario a partir de un DataTable y solo con columnas existentes
		/// </summary>
		/// <param name="dtusuario" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Diccionario de Objetos usuario
		/// </returns>
		internal Dictionary<String, EntUsuArio> crearDiccionarioRevisado(DataTable dtusuario)
		{
			Dictionary<String, EntUsuArio>  miDic = new Dictionary<String, EntUsuArio>();
			
			EntUsuArio obj = new EntUsuArio();
			foreach (DataRow row in dtusuario.Rows)
			{
				obj = crearObjetoRevisado(row);
				miDic.Add(obj.id_usuario.ToString(), obj);
			}
			return miDic;
		}
		
		internal Dictionary<String, EntUsuArio> crearDiccionario(DataTable dtusuario, EntUsuArio.Fields dicKey)
		{
			Dictionary<String, EntUsuArio>  miDic = new Dictionary<String, EntUsuArio>();
			
			EntUsuArio obj = new EntUsuArio();
			foreach (DataRow row in dtusuario.Rows)
			{
				obj = crearObjeto(row);
				
				var nameOfProperty = dicKey.ToString();
				var propertyInfo = obj.GetType().GetProperty(nameOfProperty);
				var value = propertyInfo.GetValue(obj, null);
				
				miDic.Add(value.ToString(), obj);
			}
			return miDic;
		}
		
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
		
		protected void Finalize()
		{
			Dispose();
		}
		#endregion

	}
}

