#region 
/***********************************************************************************************************
	NOMBRE:       RnSucUrsal
	DESCRIPCION:
		Clase que implmenta los metodos y operaciones sobre la Tabla sucursal

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
	public class RnSucUrsal
	{
		#region Reflection
        
		/// <summary>
		/// Metodo para castear Dinamicamente un Tipo
		/// </summary>
		/// <param name="valor">Tipo a ser casteado</param>
		/// <param name="myField">Enum de la columna</param>
		/// <returns>Devuelve un objeto del Tipo de la columna especificada en el Enum</returns>
		public object GetColumnType(object valor, EntSucUrsal.Fields myField)
		{
			if (DBNull.Value.Equals(valor)) 
				return null;
			Type destino = typeof(EntSucUrsal).GetProperty(myField.ToString()).PropertyType;
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
			Type destino = typeof(EntSucUrsal).GetProperty(strField).PropertyType;
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
		/// 	 Funcion que obtiene la llave primaria unica de la tabla sucursal a partir de una cadena
		/// </summary>
		/// <param name="args" type="string[]">
		///     <para>
		/// 		 Cadena desde la que se construye el identificador unico de la tabla sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Identificador unico de la tabla sucursal
		/// </returns>
		public string CreatePk(string[] args)
		{
			return args[0];
		}
		
		#endregion 

		#region ObtenerObjeto

		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntSucUrsal a partir de la llave primaria
		/// </summary>
		/// <returns>
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public EntSucUrsal ObtenerObjeto(int intid_sucursal)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(EntSucUrsal.Fields.id_sucursal.ToString());
		
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(intid_sucursal);
			
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntSucUrsal a partir del usuario que inserta
		/// </summary>
		/// <param name="strUsuCre">Login o nombre de usuario</param>
		/// <returns>
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public EntSucUrsal ObtenerObjetoInsertado(string strUsuCre)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(EntSucUrsal.Fields.usucre.ToString());
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'" + strUsuCre + "'");
			
			int iIdInsertado = FuncionesMax(EntSucUrsal.Fields.id_sucursal, arrColumnasWhere, arrValoresWhere);
			
			return ObtenerObjeto(iIdInsertado);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public EntSucUrsal ObtenerObjeto(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public EntSucUrsal ObtenerObjeto(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count == 1)
				{
					EntSucUrsal obj = new EntSucUrsal();
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
		/// 	Funcion que obtiene los datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public EntSucUrsal ObtenerObjeto(Hashtable htbFiltro)
		{
			return ObtenerObjeto(htbFiltro, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public EntSucUrsal ObtenerObjeto(Hashtable htbFiltro, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count == 1)
				{
					EntSucUrsal obj = new EntSucUrsal();
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
		/// 	Funcion que obtiene los datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public EntSucUrsal ObtenerObjeto(EntSucUrsal.Fields searchField, object searchValue)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public EntSucUrsal ObtenerObjeto(EntSucUrsal.Fields searchField, object searchValue, string strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un Business Object del Tipo EntSucUrsal a partir de su llave promaria
		/// </summary>
		/// <returns>
		/// 	Objeto del Tipo EntSucUrsal
		/// </returns>
		public EntSucUrsal ObtenerObjeto(int intid_sucursal, ref CTrans localTrans )
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(EntSucUrsal.Fields.id_sucursal.ToString());
		
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(intid_sucursal);
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public EntSucUrsal ObtenerObjeto(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans localTrans)
		{
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public EntSucUrsal ObtenerObjeto(Hashtable htbFiltro, ref CTrans localTrans)
		{
			return ObtenerObjeto(htbFiltro, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public EntSucUrsal ObtenerObjeto(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales,  ref CTrans localTrans)
		{
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public EntSucUrsal ObtenerObjeto(Hashtable htbFiltro, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count == 1)
				{
					EntSucUrsal obj = new EntSucUrsal();
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
		/// 	Funcion que obtiene los datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public EntSucUrsal ObtenerObjeto(EntSucUrsal.Fields searchField, object searchValue, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerObjeto(arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene los datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public EntSucUrsal ObtenerObjeto(EntSucUrsal.Fields searchField, object searchValue, string strParamAdicionales, ref CTrans localTrans)
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
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntSucUrsal> ObtenerLista()
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerLista(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		public List<EntSucUrsal> ObtenerLista(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			return ObtenerLista(arrColumnasWhere, arrValoresWhere, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		public List<EntSucUrsal> ObtenerLista(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearLista(table);
				}
				else
					return new List<EntSucUrsal>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		public List<EntSucUrsal> ObtenerLista(EntSucUrsal.Fields searchField, object searchValue)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerLista(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		public List<EntSucUrsal> ObtenerLista(EntSucUrsal.Fields searchField, object searchValue, string strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerLista(arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntSucUrsal> ObtenerLista(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans localTrans)
		{
			return ObtenerLista(arrColumnasWhere, arrValoresWhere, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntSucUrsal> ObtenerLista(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count > 0)
				{
					return crearLista(table);
				}
				else
					return new List<EntSucUrsal>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntSucUrsal> ObtenerLista(EntSucUrsal.Fields searchField, object searchValue, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerLista(arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntSucUrsal> ObtenerLista(EntSucUrsal.Fields searchField, object searchValue, string strParamAdicionales, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerLista(arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntSucUrsal> ObtenerLista(Hashtable htbFiltro)
		{
			return ObtenerLista(htbFiltro, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		public List<EntSucUrsal> ObtenerLista(Hashtable htbFiltro, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearLista(table);
				}
				else
					return new List<EntSucUrsal>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntSucUrsal> ObtenerLista(Hashtable htbFiltro, ref CTrans localTrans)
		{
			return ObtenerLista(htbFiltro, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntSucUrsal> ObtenerLista(Hashtable htbFiltro, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
				
				ArrayList arrColumnasWhere = new ArrayList();
				ArrayList arrValoresWhere = new ArrayList();
				
				foreach (DictionaryEntry entry in htbFiltro)
				{
					arrColumnasWhere.Add(entry.Key.ToString());
					arrValoresWhere.Add(entry.Value.ToString());
				}
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count > 0)
				{
					return crearLista(table);
				}
				else
					return new List<EntSucUrsal>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntSucUrsal> ObtenerListaDesdeVista(String strVista)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		public List<EntSucUrsal> ObtenerListaDesdeVista(String strVista, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		public List<EntSucUrsal> ObtenerListaDesdeVista(String strVista, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
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
					return new List<EntSucUrsal>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		public List<EntSucUrsal> ObtenerListaDesdeVista(String strVista, Hashtable htbFiltro)
		{
			return ObtenerListaDesdeVista(strVista, htbFiltro, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		public List<EntSucUrsal> ObtenerListaDesdeVista(String strVista, Hashtable htbFiltro, string strParamAdicionales)
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
					return new List<EntSucUrsal>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		public List<EntSucUrsal> ObtenerListaDesdeVista(String strVista, EntSucUrsal.Fields searchField, object searchValue)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		public List<EntSucUrsal> ObtenerListaDesdeVista(String strVista, EntSucUrsal.Fields searchField, object searchValue, string strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntSucUrsal> ObtenerListaDesdeVista(String strVista, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans localTrans)
		{
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntSucUrsal> ObtenerListaDesdeVista(String strVista, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales, ref CTrans localTrans)
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
					return new List<EntSucUrsal>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntSucUrsal> ObtenerListaDesdeVista(String strVista, Hashtable htbFiltro, ref CTrans localTrans)
		{
			return ObtenerListaDesdeVista(strVista, htbFiltro, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntSucUrsal> ObtenerListaDesdeVista(String strVista, Hashtable htbFiltro, string strParamAdicionales, ref CTrans localTrans)
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
					return new List<EntSucUrsal>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntSucUrsal> ObtenerListaDesdeVista(String strVista, EntSucUrsal.Fields searchField, object searchValue, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerListaDesdeVista(strVista, arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="strVista" type="String">
		///     <para>
		/// 		 Nombre de la Vista desde que se van ha obtener los datos
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.List que cumple con los filtros de los parametros
		/// </returns>
		public List<EntSucUrsal> ObtenerListaDesdeVista(String strVista, EntSucUrsal.Fields searchField, object searchValue, string strParamAdicionales, ref CTrans localTrans)
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
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntSucUrsal> ObtenerCola()
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerCola(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		public Queue<EntSucUrsal> ObtenerCola(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			return ObtenerCola(arrColumnasWhere, arrValoresWhere, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		public Queue<EntSucUrsal> ObtenerCola(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearCola(table);
				}
				else
					return new Queue<EntSucUrsal>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		public Queue<EntSucUrsal> ObtenerCola(EntSucUrsal.Fields searchField, object searchValue)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerCola(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		public Queue<EntSucUrsal> ObtenerCola(EntSucUrsal.Fields searchField, object searchValue, string strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerCola(arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntSucUrsal> ObtenerCola(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans localTrans)
		{
			return ObtenerCola(arrColumnasWhere, arrValoresWhere, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntSucUrsal> ObtenerCola(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count > 0)
				{
					return crearCola(table);
				}
				else
					return new Queue<EntSucUrsal>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Queue que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntSucUrsal> ObtenerCola(EntSucUrsal.Fields searchField, object searchValue, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerCola(arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Stack que cumple con los filtros de los parametros
		/// </returns>
		public Queue<EntSucUrsal> ObtenerCola(EntSucUrsal.Fields searchField, object searchValue, string strParamAdicionales, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerCola(arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntSucUrsal> ObtenerPila()
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerPila(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		public Stack<EntSucUrsal> ObtenerPila(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			return ObtenerPila(arrColumnasWhere, arrValoresWhere, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		public Stack<EntSucUrsal> ObtenerPila(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearPila(table);
				}
				else
					return new Stack<EntSucUrsal>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		public Stack<EntSucUrsal> ObtenerPila(EntSucUrsal.Fields searchField, object searchValue)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerPila(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		public Stack<EntSucUrsal> ObtenerPila(EntSucUrsal.Fields searchField, object searchValue, string strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerPila(arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntSucUrsal> ObtenerPila(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans localTrans)
		{
			return ObtenerPila(arrColumnasWhere, arrValoresWhere, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntSucUrsal> ObtenerPila(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count > 0)
				{
					return crearPila(table);
				}
				else
					return new Stack<EntSucUrsal>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntSucUrsal> ObtenerPila(EntSucUrsal.Fields searchField, object searchValue, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerPila(arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Stack que cumple con los filtros de los parametros
		/// </returns>
		public Stack<EntSucUrsal> ObtenerPila(EntSucUrsal.Fields searchField, object searchValue, string strParamAdicionales, ref CTrans localTrans)
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
		/// 	 Funcion que llena un DataTable con los registros de una tabla sucursal
		/// </summary>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de sucursal
		/// </returns>
		public DataTable NuevoDataTable()
		{
			try
			{
				DataTable table = new DataTable ();
				DataColumn dc;
				dc = new DataColumn(EntSucUrsal.Fields.id_sucursal.ToString(),typeof(EntSucUrsal).GetProperty(EntSucUrsal.Fields.id_sucursal.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntSucUrsal.Fields.direccion_sucursal.ToString(),typeof(EntSucUrsal).GetProperty(EntSucUrsal.Fields.direccion_sucursal.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntSucUrsal.Fields.telefono_sucursal.ToString(),typeof(EntSucUrsal).GetProperty(EntSucUrsal.Fields.telefono_sucursal.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntSucUrsal.Fields.id_usuario.ToString(),typeof(EntSucUrsal).GetProperty(EntSucUrsal.Fields.id_usuario.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntSucUrsal.Fields.id_autoescuelas.ToString(),typeof(EntSucUrsal).GetProperty(EntSucUrsal.Fields.id_autoescuelas.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntSucUrsal.Fields.apiestado.ToString(),typeof(EntSucUrsal).GetProperty(EntSucUrsal.Fields.apiestado.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntSucUrsal.Fields.usucre.ToString(),typeof(EntSucUrsal).GetProperty(EntSucUrsal.Fields.usucre.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntSucUrsal.Fields.feccre.ToString(),typeof(EntSucUrsal).GetProperty(EntSucUrsal.Fields.feccre.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntSucUrsal.Fields.usumod.ToString(),typeof(EntSucUrsal).GetProperty(EntSucUrsal.Fields.usumod.ToString()).PropertyType);
				table.Columns.Add(dc);

				dc = new DataColumn(EntSucUrsal.Fields.fecmod.ToString(),typeof(EntSucUrsal).GetProperty(EntSucUrsal.Fields.fecmod.ToString()).PropertyType);
				table.Columns.Add(dc);

				
				return table;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que genera un DataTable con determinadas columnas de una sucursal
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de sucursal
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
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere);
				
				return table;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registros de una tabla sucursal
		/// </summary>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de sucursal
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
		/// 	 Funcion que llena un DataTable con los registros de una tabla y n condicion WHERE sucursal
		/// </summary>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de sucursal
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
		/// 	 Funcion que llena un DataTable con los registro de una tabla sucursal
		/// </summary>
		/// <param name="arrColumnas" type="System.Collections.ArrayList">
		///     <para>
		/// 		 Array de las columnas que se va a seleccionar para mostrarlas en el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de sucursal
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
		/// 	 Funcion que llena un DataTable con los registro de una tabla sucursal
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
		/// 	 DataTable con los datos obtenidos de sucursal
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
		/// 	 Funcion que llena un DataTable con los registro de una tabla sucursal
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
		/// 	 DataTable con los datos obtenidos de sucursal
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
		/// 	 Funcion que llena un DataTable con los registro de una tabla sucursal
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
		/// 	 DataTable con los datos obtenidos de sucursal
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
		/// 	 Funcion que llena un DataTable con los registro de una tabla sucursal
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
		/// 	 DataTable con los datos obtenidos de sucursal
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
		/// 	 Funcion que llena un DataTable con los registro de una tabla sucursal
		/// </summary>
		/// <param name="htbFiltro" type="System.Collections.Hashtable">
		///     <para>
		/// 		 Hashtable que contienen los pares para filtrar el resultado
		///     </para>
		/// </param>
		/// <returns>
		/// 	 DataTable con los datos obtenidos de sucursal
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
		/// 	 Funcion que llena un DataTable con los registro de una tabla sucursal
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
		/// 	 DataTable con los datos obtenidos de sucursal
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParametrosAdicionales)
		{
			try
			{
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
				
				return table;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla sucursal
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
		/// 	 DataTable con los datos obtenidos de sucursal
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
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
				
				return table;
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla sucursal
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
		/// 	 DataTable con los datos obtenidos de sucursal
		/// </returns>
		public DataTable ObtenerDataTable(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParametrosAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
				
				return ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla sucursal
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
		/// 	 DataTable con los datos obtenidos de sucursal
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
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
				
				return ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla sucursal
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
		/// 	 DataTable con los datos obtenidos de sucursal
		/// </returns>
		public DataTable ObtenerDataTableOr(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
				return ObtenerDataTableOr(arrColumnas, arrColumnasWhere, arrValoresWhere, "");
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que llena un DataTable con los registro de una tabla sucursal
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
		/// 	 DataTable con los datos obtenidos de sucursal
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
		/// 	 Funcion que llena un DataTable con los registro de una tabla sucursal
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
		/// 	 DataTable con los datos obtenidos de sucursal
		/// </returns>
		public DataTable ObtenerDataTableOr(ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParametrosAdicionales)
		{
			try
			{
				CConn local = new CConn();
				DataTable table = local.cargarDataTableOr(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
				
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
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		public DataTable ObtenerDataTable(EntSucUrsal.Fields searchField, object searchValue)
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
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		public DataTable ObtenerDataTable(EntSucUrsal.Fields searchField, object searchValue, string strParametrosAdicionales)
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
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		public DataTable ObtenerDataTable(ArrayList arrColumnas, EntSucUrsal.Fields searchField, object searchValue, string strParametrosAdicionales)
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
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		public DataTable ObtenerDataTable(ArrayList arrColumnas, EntSucUrsal.Fields searchField, object searchValue)
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
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntSucUrsal> ObtenerDiccionario()
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		public Dictionary<String, EntSucUrsal> ObtenerDiccionario(ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
		{
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere, "");
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		public Dictionary<String, EntSucUrsal> ObtenerDiccionario(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearDiccionario(table);
				}
				else
					return new Dictionary<string, EntSucUrsal>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		public Dictionary<String, EntSucUrsal> ObtenerDiccionario(EntSucUrsal.Fields searchField, object searchValue)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		public Dictionary<String, EntSucUrsal> ObtenerDiccionario(EntSucUrsal.Fields searchField, object searchValue, string strParamAdicionales)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere, strParamAdicionales);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntSucUrsal> ObtenerDiccionario(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans localTrans)
		{
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere, "", ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntSucUrsal> ObtenerDiccionario(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales, ref CTrans localTrans)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
				
				if (table.Rows.Count > 0)
				{
					return crearDiccionario(table);
				}
				else
					return new Dictionary<string, EntSucUrsal>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntSucUrsal> ObtenerDiccionario(EntSucUrsal.Fields searchField, object searchValue, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere, ref localTrans);
		}
		
		/// <summary>
		/// 	Funcion que obtiene un conjunto de datos de una Clase EntSucUrsal a partir de condiciones WHERE
		/// </summary>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo System.Collections.Generic.Dictionary que cumple con los filtros de los parametros
		/// </returns>
		public Dictionary<String, EntSucUrsal> ObtenerDiccionario(EntSucUrsal.Fields searchField, object searchValue, string strParamAdicionales, ref CTrans localTrans)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerDiccionario(arrColumnasWhere, arrValoresWhere, strParamAdicionales, ref localTrans);
		}
		
		public Dictionary<String, EntSucUrsal> ObtenerDiccionarioKey(EntSucUrsal.Fields dicKey)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerDiccionarioKey(arrColumnasWhere, arrValoresWhere, dicKey);
		}
		
		public Dictionary<String, EntSucUrsal> ObtenerDiccionarioKey(String strParamAdic, EntSucUrsal.Fields dicKey)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add("'1'");
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add("'1'");
			
			return ObtenerDiccionarioKey(arrColumnasWhere, arrValoresWhere, strParamAdic, dicKey);
		}
		
		public Dictionary<String, EntSucUrsal> ObtenerDiccionarioKey(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, EntSucUrsal.Fields dicKey)
		{
			return ObtenerDiccionarioKey(arrColumnasWhere, arrValoresWhere, "", dicKey);
		}
		
		public Dictionary<String, EntSucUrsal> ObtenerDiccionarioKey(ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales, EntSucUrsal.Fields dicKey)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
				
				CConn local = new CConn();
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				if (table.Rows.Count > 0)
				{
					return crearDiccionario(table, dicKey);
				}
				else
					return new Dictionary<string, EntSucUrsal>();
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		public Dictionary<String, EntSucUrsal> ObtenerDiccionarioKey(EntSucUrsal.Fields searchField, object searchValue, EntSucUrsal.Fields dicKey)
		{
			ArrayList arrColumnasWhere = new ArrayList();
			arrColumnasWhere.Add(searchField.ToString());
			
			ArrayList arrValoresWhere = new ArrayList();
			arrValoresWhere.Add(searchValue);
			
			return ObtenerDiccionarioKey(arrColumnasWhere, arrValoresWhere, dicKey);
		}
		
		public Dictionary<String, EntSucUrsal> ObtenerDiccionarioKey(EntSucUrsal.Fields searchField, object searchValue, string strParamAdicionales, EntSucUrsal.Fields dicKey)
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
		/// 	Funcion que inserta un nuevo registro en la tabla sucursal a partir de una clase del tipo Esucursal
		/// </summary>
		/// <param name="strNombreSp" type="System.string">
		///     <para>
		/// 		 Nombre del Procedimiento a ejecutar sobre el SP
		///     </para>
		/// </param>
		/// <param name="obj" type="Entidades.EntSucUrsal">
		///     <para>
		/// 		 Clase desde la que se va a ejecutar el SP de la tabla sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor de registros afectados en el Procedimiento de la tabla sucursal
		/// </returns>
		public int EjecutarSpDesdeObjeto(string strNombreSp, EntSucUrsal obj)
		{
			try
			{
				
				ArrayList arrNombreParam = new ArrayList();
				arrNombreParam.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrNombreParam.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrNombreParam.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrNombreParam.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrNombreParam.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrNombreParam.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrNombreParam.Add(EntSucUrsal.Fields.usucre.ToString());
				arrNombreParam.Add(EntSucUrsal.Fields.feccre.ToString());
				arrNombreParam.Add(EntSucUrsal.Fields.usumod.ToString());
				arrNombreParam.Add(EntSucUrsal.Fields.fecmod.ToString());
				
				ArrayList arrValoresParam = new ArrayList();
				arrValoresParam.Add(obj.id_sucursal);
				arrValoresParam.Add(obj.direccion_sucursal == null ? null : "'" + obj.direccion_sucursal + "'");
				arrValoresParam.Add(obj.telefono_sucursal);
				arrValoresParam.Add(obj.id_usuario);
				arrValoresParam.Add(obj.id_autoescuelas);
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
		/// 	Funcion que inserta un nuevo registro en la tabla sucursal a partir de una clase del tipo Esucursal
		/// </summary>
		/// <param name="strNombreSp" type="System.string">
		///     <para>
		/// 		 Nombre del Procedimiento a ejecutar sobre el SP
		///     </para>
		/// </param>
		/// <param name="obj" type="Entidades.EntSucUrsal">
		///     <para>
		/// 		 Clase desde la que se va a ejecutar el SP de la tabla sucursal
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor de registros afectados en el Procedimiento de la tabla sucursal
		/// </returns>
		public int EjecutarSpDesdeObjeto(string strNombreSp, EntSucUrsal obj, ref CTrans localTrans)
		{
			try
			{
				
				ArrayList arrNombreParam = new ArrayList();
				arrNombreParam.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrNombreParam.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrNombreParam.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrNombreParam.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrNombreParam.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrNombreParam.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrNombreParam.Add(EntSucUrsal.Fields.usucre.ToString());
				arrNombreParam.Add(EntSucUrsal.Fields.feccre.ToString());
				arrNombreParam.Add(EntSucUrsal.Fields.usumod.ToString());
				arrNombreParam.Add(EntSucUrsal.Fields.fecmod.ToString());
				
				ArrayList arrValoresParam = new ArrayList();
				arrValoresParam.Add(obj.id_sucursal);
				arrValoresParam.Add(obj.direccion_sucursal == null ? null : "'" + obj.direccion_sucursal + "'");
				arrValoresParam.Add(obj.telefono_sucursal);
				arrValoresParam.Add(obj.id_usuario);
				arrValoresParam.Add(obj.id_autoescuelas);
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
		/// <param name="refField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesCount(EntSucUrsal.Fields refField)
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
		/// <param name="refField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="whereField" type="EntSucUrsal.Fileds">
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
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesCount(EntSucUrsal.Fields refField, EntSucUrsal.Fields whereField, object valueField)
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
		/// <param name="refField" type="EntSucUrsal.Fileds">
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
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesCount(EntSucUrsal.Fields refField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
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
		/// <param name="refField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesMin(EntSucUrsal.Fields refField)
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
		/// <param name="refField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="whereField" type="EntSucUrsal.Fileds">
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
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesMin(EntSucUrsal.Fields refField, EntSucUrsal.Fields whereField, object valueField)
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
		/// <param name="refField" type="EntSucUrsal.Fileds">
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
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesMin(EntSucUrsal.Fields refField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
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
		/// <param name="refField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesMax(EntSucUrsal.Fields refField)
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
		/// <param name="refField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="whereField" type="EntSucUrsal.Fileds">
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
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesMax(EntSucUrsal.Fields refField, EntSucUrsal.Fields whereField, object valueField)
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
		/// <param name="refField" type="EntSucUrsal.Fileds">
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
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesMax(EntSucUrsal.Fields refField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
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
		/// <param name="refField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesSum(EntSucUrsal.Fields refField)
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
		/// <param name="refField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="whereField" type="EntSucUrsal.Fileds">
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
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesSum(EntSucUrsal.Fields refField, EntSucUrsal.Fields whereField, object valueField)
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
		/// <param name="refField" type="EntSucUrsal.Fileds">
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
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesSum(EntSucUrsal.Fields refField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
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
		/// <param name="refField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <returns>
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesAvg(EntSucUrsal.Fields refField)
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
		/// <param name="refField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo al que se va a aplicar la funcion
		///     </para>
		/// </param>
		/// <param name="whereField" type="EntSucUrsal.Fileds">
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
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesAvg(EntSucUrsal.Fields refField, EntSucUrsal.Fields whereField, object valueField)
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
		/// <param name="refField" type="EntSucUrsal.Fileds">
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
		/// 	Valor del Tipo EntSucUrsal que cumple con los filtros de los parametros
		/// </returns>
		public int FuncionesAvg(EntSucUrsal.Fields refField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
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
		/// 	Funcion que inserta un nuevo registro en la tabla sucursal a partir de una clase del tipo Esucursal
		/// </summary>
		/// <param name="obj" type="Entidades.EntSucUrsal">
		///     <para>
		/// 		 Clase desde la que se van a insertar los valores a la tabla sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionsucursal
		/// </returns>
		public bool Insert(EntSucUrsal obj, bool bValidar = true)
		{
			try
			{
				ArrayList arrNombreParam = new ArrayList();
				ArrayList arrValoresParam = new ArrayList();
				arrNombreParam.Add("id_sucursal");
				arrValoresParam.Add(null);
				arrNombreParam.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrValoresParam.Add(obj.direccion_sucursal);
				
				arrNombreParam.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrValoresParam.Add(obj.telefono_sucursal);
				
				arrNombreParam.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrValoresParam.Add(obj.id_usuario);
				
				arrNombreParam.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrValoresParam.Add(obj.id_autoescuelas);
				
				arrNombreParam.Add(EntSucUrsal.Fields.usucre.ToString());
				arrValoresParam.Add(obj.usucre);
				
				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				string nombreSp = CListadoSP.SucUrsal.sp_sucursal_Ins.ToString();
				return (local.execStoreProcedure(nombreSp, arrNombreParam, arrValoresParam) > 0);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla sucursal a partir de una clase del tipo Esucursal
		/// </summary>
		/// <param name="obj" type="Entidades.EntSucUrsal">
		///     <para>
		/// 		 Clase desde la que se van a insertar los valores a la tabla sucursal
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionsucursal
		/// </returns>
		public bool Insert(EntSucUrsal obj, ref CTrans localTrans, bool bValidar = true)
		{
			try
			{
				ArrayList arrNombreParam = new ArrayList();
				ArrayList arrValoresParam = new ArrayList();
				arrNombreParam.Add("id_sucursal");
				arrValoresParam.Add("");
				arrNombreParam.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrValoresParam.Add(obj.direccion_sucursal);
				
				arrNombreParam.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrValoresParam.Add(obj.telefono_sucursal);
				
				arrNombreParam.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrValoresParam.Add(obj.id_usuario);
				
				arrNombreParam.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrValoresParam.Add(obj.id_autoescuelas);
				
				arrNombreParam.Add(EntSucUrsal.Fields.usucre.ToString());
				arrValoresParam.Add(obj.usucre);
				
				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				string nombreSp = CListadoSP.SucUrsal.sp_sucursal_Ins.ToString();
				return (local.execStoreProcedure(nombreSp, arrNombreParam, arrValoresParam, ref localTrans) > 0);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla sucursal a partir de una clase del tipo Esucursal
		/// </summary>
		/// <param name="obj" type="Entidades.EntSucUrsal">
		///     <para>
		/// 		 Clase desde la que se van a insertar los valores a la tabla sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor que indica la cantidad de registros actualizados en sucursal
		/// </returns>
		public int Update(EntSucUrsal obj, bool bValidar = true)
		{
			try
			{
				ArrayList arrNombreParam = new ArrayList();
				ArrayList arrValoresParam = new ArrayList();
				arrNombreParam.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrValoresParam.Add(obj.id_sucursal);
				
				arrNombreParam.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrValoresParam.Add(obj.direccion_sucursal);
				
				arrNombreParam.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrValoresParam.Add(obj.telefono_sucursal);
				
				arrNombreParam.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrValoresParam.Add(obj.id_usuario);
				
				arrNombreParam.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrValoresParam.Add(obj.id_autoescuelas);
				
				arrNombreParam.Add(EntSucUrsal.Fields.usumod.ToString());
				arrValoresParam.Add(obj.usumod);
				
				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				string nombreSp = CListadoSP.SucUrsal.sp_sucursal_Upd.ToString();
				return local.execStoreProcedure(nombreSp, arrNombreParam, arrValoresParam);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla sucursal a partir de una clase del tipo Esucursal
		/// </summary>
		/// <param name="obj" type="Entidades.EntSucUrsal">
		///     <para>
		/// 		 Clase desde la que se van a insertar los valores a la tabla sucursal
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionsucursal
		/// </returns>
		public int Update(EntSucUrsal obj, ref CTrans localTrans, bool bValidar = true)
		{
			try
			{
				ArrayList arrNombreParam = new ArrayList();
				ArrayList arrValoresParam = new ArrayList();
				arrNombreParam.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrValoresParam.Add(obj.id_sucursal);
				
				arrNombreParam.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrValoresParam.Add(obj.direccion_sucursal);
				
				arrNombreParam.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrValoresParam.Add(obj.telefono_sucursal);
				
				arrNombreParam.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrValoresParam.Add(obj.id_usuario);
				
				arrNombreParam.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrValoresParam.Add(obj.id_autoescuelas);
				
				arrNombreParam.Add(EntSucUrsal.Fields.usumod.ToString());
				arrValoresParam.Add(obj.usumod);
				
				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				string nombreSp = CListadoSP.SucUrsal.sp_sucursal_Upd.ToString();
				return local.execStoreProcedure(nombreSp, arrNombreParam, arrValoresParam, ref localTrans);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla sucursal a partir de una clase del tipo Esucursal
		/// </summary>
		/// <param name="obj" type="Entidades.EntSucUrsal">
		///     <para>
		/// 		 Clase desde la que se van a insertar los valores a la tabla sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionsucursal
		/// </returns>
		public int Delete(EntSucUrsal obj, bool bValidar = true)
		{
			try
			{
				ArrayList arrNombreParam = new ArrayList();
				ArrayList arrValoresParam = new ArrayList();
				arrNombreParam.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrValoresParam.Add(obj.id_sucursal);
				
				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				string nombreSp = CListadoSP.SucUrsal.sp_sucursal_Del.ToString();
				return local.execStoreProcedure(nombreSp, arrNombreParam, arrValoresParam);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta un nuevo registro en la tabla sucursal a partir de una clase del tipo Esucursal
		/// </summary>
		/// <param name="obj" type="Entidades.EntSucUrsal">
		///     <para>
		/// 		 Clase desde la que se van a insertar los valores a la tabla sucursal
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionsucursal
		/// </returns>
		public int Delete(EntSucUrsal obj, ref CTrans localTrans, bool bValidar = true)
		{
			try
			{
				ArrayList arrNombreParam = new ArrayList();
				ArrayList arrValoresParam = new ArrayList();
				arrNombreParam.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrValoresParam.Add(obj.id_sucursal);
				
				
				//Llamamos al Procedmiento Almacenado
				CConn local = new CConn();
				string nombreSp = CListadoSP.SucUrsal.sp_sucursal_Del.ToString();
				return local.execStoreProcedure(nombreSp, arrNombreParam, arrValoresParam, ref localTrans);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}

		/// <summary>
		/// 	Funcion que inserta o actualiza un registro un nuevo registro en la tabla sucursal a partir de una clase del tipo Esucursal
		/// </summary>
		/// <param name="obj" type="Entidades.EntSucUrsal">
		///     <para>
		/// 		 Clase desde la que se van a insertar o actualizar los valores a la tabla sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionsucursal
		/// </returns>
		public int InsertUpdate(EntSucUrsal obj)
		{
			try
			{
				bool esInsertar = true;
				
					esInsertar = (esInsertar && (obj.id_sucursal == null));
				
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
		/// 	Funcion que inserta o actualiza un registro un nuevo registro en la tabla sucursal a partir de una clase del tipo Esucursal
		/// </summary>
		/// <param name="obj" type="Entidades.EntSucUrsal">
		///     <para>
		/// 		 Clase desde la que se van a insertar o actualizar los valores a la tabla sucursal
		///     </para>
		/// </param>
		/// <param name="localTrans" type="App_Class.Conexion.CTrans">
		///     <para>
		/// 		 Clase desde la que se va a utilizar una transaccion sucursal
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Valor TRUE or FALSE que indica el exito de la operacionsucursal
		/// </returns>
		public int InsertUpdate(EntSucUrsal obj, ref CTrans localTrans)
		{
			try
			{
				bool esInsertar = false;
				
					esInsertar = (esInsertar && (obj.id_sucursal == null));
				
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
		/// 	 Funcion que carga un DropDownList con los valores de la tabla sucursal
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo DropDownList en el que se van a cargar los datos de la tabla sucursal
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
		/// 	 Funcion que carga un DropDownList con los valores de la tabla sucursal
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo DropDownList en el que se van a cargar los datos de la tabla sucursal
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
		/// 	 Funcion que carga un DropDownList con los valores de la tabla sucursal
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo DropDownList en el que se van a cargar los datos de la tabla sucursal
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
		/// 	 Funcion que carga un DropDownList con los valores de la tabla sucursal
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo DropDownList en el que se van a cargar los datos de la tabla sucursal
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
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				
				CargarDropDownList(ref cmb, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		
		/// <summary>
		/// 	 Funcion que carga un DropDownList con los valores de la tabla sucursal
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla sucursal
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntSucUrsal.Fields valueField, EntSucUrsal.Fields textField)
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
		/// 	 Funcion que carga un DropDownList con los valores de la tabla sucursal
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla sucursal
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="strParamAdicionales" type="String">
		///     <para>
		/// 		 Condiciones que van en la clausula WHERE. Deben ir sin WHERE
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntSucUrsal.Fields valueField, EntSucUrsal.Fields textField, string strParamAdicionales)
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
		/// 	 Funcion que carga un DropDownList con los valores de la tabla sucursal
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla sucursal
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="String">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntSucUrsal.Fields valueField, String textField)
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
		/// 	 Funcion que carga un DropDownList con los valores de la tabla sucursal
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla sucursal
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntSucUrsal.Fileds">
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
		public void CargarDropDownList(ref DropDownList cmb, EntSucUrsal.Fields valueField, String textField, String strParamAdicionales)
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
		/// 	 Funcion que carga un DropDownList con los valores de la tabla sucursal
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla sucursal
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntSucUrsal.Fields valueField, EntSucUrsal.Fields textField, EntSucUrsal.Fields searchField, object searchValue)
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
		/// 	 Funcion que carga un DropDownList con los valores de la tabla sucursal
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla sucursal
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="String">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		public void CargarDropDownList(ref DropDownList cmb, EntSucUrsal.Fields valueField, String textField, EntSucUrsal.Fields searchField, object searchValue)
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
		/// 	 Funcion que carga un DropDownList con los valores de la tabla sucursal
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla sucursal
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		public void CargarDropDownList(ref DropDownList cmb, EntSucUrsal.Fields valueField, EntSucUrsal.Fields textField, EntSucUrsal.Fields searchField, object searchValue, string strParamAdicionales)
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
		/// 	 Funcion que carga un DropDownList con los valores de la tabla sucursal
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla sucursal
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntSucUrsal.Fileds">
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
		public void CargarDropDownList(ref DropDownList cmb, EntSucUrsal.Fields valueField, String textField, EntSucUrsal.Fields searchField, object searchValue, string strParamAdicionales)
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
		/// 	 Funcion que carga un DropDownList con los valores de la tabla sucursal
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla sucursal
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntSucUrsal.Fileds">
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
		public void CargarDropDownList(ref DropDownList cmb, EntSucUrsal.Fields valueField, EntSucUrsal.Fields textField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
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
		/// 	 Funcion que carga un DropDownList con los valores de la tabla sucursal
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla sucursal
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntSucUrsal.Fileds">
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
		public void CargarDropDownList(ref DropDownList cmb, EntSucUrsal.Fields valueField, String textField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
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
		/// 	 Funcion que carga un DropDownList con los valores de la tabla sucursal
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla sucursal
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="textField" type="EntSucUrsal.Fileds">
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
		public void CargarDropDownList(ref DropDownList cmb, EntSucUrsal.Fields valueField, EntSucUrsal.Fields textField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
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
		/// 	 Funcion que carga un DropDownList con los valores de la tabla sucursal
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla sucursal
		///     </para>
		/// </param>
		/// <param name="valueField" type="EntSucUrsal.Fileds">
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
		public void CargarDropDownList(ref DropDownList cmb, EntSucUrsal.Fields valueField, String textField, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
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
		/// 	 Funcion que carga un DropDownList con los valores de la tabla sucursal
		/// </summary>
		/// <param name="cmb" type="System.Web.UI.WebControls.DropDownList">
		///     <para>
		/// 		 Control del tipo System.Windows.Forms.DropDownList en el que se van a cargar los datos de la tabla sucursal
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
				DataTable table = local.cargarDataTableAnd(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
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
		/// 	 Funcion que llena un GridView con los registro de una tabla sucursal
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
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
			
				
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
		/// 	 Funcion que llena un GridView con los registro de una tabla sucursal
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
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
			
				
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
		/// 	 Funcion que llena un GridView con los registro de una tabla sucursal
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
		/// 	 Funcion que llena un GridView con los registro de una tabla sucursal
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
		/// 	 Funcion que llena un GridView con los registro de una tabla sucursal
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
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
			
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla sucursal
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
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
			
				CargarGridView(ref dtg, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla sucursal
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
		/// 	 Funcion que llena un GridView con los registro de una tabla sucursal
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
				DbDataReader dsReader = local.cargarDataReaderAnd(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);
				
				dtg.DataSource = dsReader;
				dtg.DataBind();

			}
			catch (Exception exp)
			{
				throw exp;
			}
		}
		/// <summary>
		/// 	 Funcion que llena un GridView con los registro de una tabla sucursal
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, EntSucUrsal.Fields searchField, object searchValue)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
				
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
		/// 	 Funcion que llena un GridView con los registro de una tabla sucursal
		/// </summary>
		/// <param name="dtg" type="System.Web.UI.WebControls.GridView">
		///     <para>
		/// 		 Control del tipo System.Web.UI.WebControls.GridView
		///     </para>
		/// </param>
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		public void CargarGridView(ref GridView dtg, EntSucUrsal.Fields searchField, object searchValue, String strParamAdicionales)
		{
			try
			{
				ArrayList arrColumnas = new ArrayList();
				arrColumnas.Add(EntSucUrsal.Fields.id_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.direccion_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.telefono_sucursal.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_usuario.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.id_autoescuelas.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.apiestado.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usucre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.feccre.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.usumod.ToString());
				arrColumnas.Add(EntSucUrsal.Fields.fecmod.ToString());
				
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
		/// 	 Funcion que llena un GridView con los registro de una tabla sucursal
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
		/// <param name="searchField" type="EntSucUrsal.Fileds">
		///     <para>
		/// 		 Campo por el que se va a filtrar la busqueda
		///     </para>
		/// </param>
		/// <param name="searchValue" type="object">
		///     <para>
		/// 		 Valor para la busqueda
		///     </para>
		/// </param>
		public void CargarGridView(ref GridView dtg, ArrayList arrColumnas, EntSucUrsal.Fields searchField, object searchValue)
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
		/// 	 Funcion que llena un GridView con los registro de una tabla sucursal
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
		/// <param name="searchField" type="EntSucUrsal.Fileds">
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
		public void CargarGridView(ref GridView dtg, ArrayList arrColumnas, EntSucUrsal.Fields searchField, object searchValue, String strParamAdicionales)
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
		/// 	 Funcion que llena un GridView con los registro de una tabla sucursal
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
		/// 	 Funcion que llena un GridView con los registro de una tabla sucursal
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
				DbDataReader dsReader = local.cargarDataReaderOr(CParametros.schema + EntSucUrsal.StrNombreTabla, arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);
				
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
		/// 	 Objeto sucursal
		/// </returns>
		internal EntSucUrsal crearObjeto(DataRow row)
		{
			EntSucUrsal obj = new EntSucUrsal();
			obj.id_sucursal = (int) GetColumnType(row[EntSucUrsal.Fields.id_sucursal.ToString()], EntSucUrsal.Fields.id_sucursal);
			obj.direccion_sucursal = (string) GetColumnType(row[EntSucUrsal.Fields.direccion_sucursal.ToString()], EntSucUrsal.Fields.direccion_sucursal);
			obj.telefono_sucursal = (int?) GetColumnType(row[EntSucUrsal.Fields.telefono_sucursal.ToString()], EntSucUrsal.Fields.telefono_sucursal);
			obj.id_usuario = (int?) GetColumnType(row[EntSucUrsal.Fields.id_usuario.ToString()], EntSucUrsal.Fields.id_usuario);
			obj.id_autoescuelas = (int?) GetColumnType(row[EntSucUrsal.Fields.id_autoescuelas.ToString()], EntSucUrsal.Fields.id_autoescuelas);
			obj.apiestado = (string) GetColumnType(row[EntSucUrsal.Fields.apiestado.ToString()], EntSucUrsal.Fields.apiestado);
			obj.usucre = (string) GetColumnType(row[EntSucUrsal.Fields.usucre.ToString()], EntSucUrsal.Fields.usucre);
			obj.feccre = (DateTime) GetColumnType(row[EntSucUrsal.Fields.feccre.ToString()], EntSucUrsal.Fields.feccre);
			obj.usumod = (string) GetColumnType(row[EntSucUrsal.Fields.usumod.ToString()], EntSucUrsal.Fields.usumod);
			obj.fecmod = (DateTime?) GetColumnType(row[EntSucUrsal.Fields.fecmod.ToString()], EntSucUrsal.Fields.fecmod);
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
		/// 	 Objeto sucursal
		/// </returns>
		internal EntSucUrsal crearObjetoRevisado(DataRow row)
		{
			EntSucUrsal obj = new EntSucUrsal();
			if (row.Table.Columns.Contains(EntSucUrsal.Fields.id_sucursal.ToString()))
				obj.id_sucursal = (int) GetColumnType(row[EntSucUrsal.Fields.id_sucursal.ToString()], EntSucUrsal.Fields.id_sucursal);
			if (row.Table.Columns.Contains(EntSucUrsal.Fields.direccion_sucursal.ToString()))
				obj.direccion_sucursal = (string) GetColumnType(row[EntSucUrsal.Fields.direccion_sucursal.ToString()], EntSucUrsal.Fields.direccion_sucursal);
			if (row.Table.Columns.Contains(EntSucUrsal.Fields.telefono_sucursal.ToString()))
				obj.telefono_sucursal = (int?) GetColumnType(row[EntSucUrsal.Fields.telefono_sucursal.ToString()], EntSucUrsal.Fields.telefono_sucursal);
			if (row.Table.Columns.Contains(EntSucUrsal.Fields.id_usuario.ToString()))
				obj.id_usuario = (int?) GetColumnType(row[EntSucUrsal.Fields.id_usuario.ToString()], EntSucUrsal.Fields.id_usuario);
			if (row.Table.Columns.Contains(EntSucUrsal.Fields.id_autoescuelas.ToString()))
				obj.id_autoescuelas = (int?) GetColumnType(row[EntSucUrsal.Fields.id_autoescuelas.ToString()], EntSucUrsal.Fields.id_autoescuelas);
			if (row.Table.Columns.Contains(EntSucUrsal.Fields.apiestado.ToString()))
				obj.apiestado = (string) GetColumnType(row[EntSucUrsal.Fields.apiestado.ToString()], EntSucUrsal.Fields.apiestado);
			if (row.Table.Columns.Contains(EntSucUrsal.Fields.usucre.ToString()))
				obj.usucre = (string) GetColumnType(row[EntSucUrsal.Fields.usucre.ToString()], EntSucUrsal.Fields.usucre);
			if (row.Table.Columns.Contains(EntSucUrsal.Fields.feccre.ToString()))
				obj.feccre = (DateTime) GetColumnType(row[EntSucUrsal.Fields.feccre.ToString()], EntSucUrsal.Fields.feccre);
			if (row.Table.Columns.Contains(EntSucUrsal.Fields.usumod.ToString()))
				obj.usumod = (string) GetColumnType(row[EntSucUrsal.Fields.usumod.ToString()], EntSucUrsal.Fields.usumod);
			if (row.Table.Columns.Contains(EntSucUrsal.Fields.fecmod.ToString()))
				obj.fecmod = (DateTime?) GetColumnType(row[EntSucUrsal.Fields.fecmod.ToString()], EntSucUrsal.Fields.fecmod);
			return obj;
		}

		/// <summary>
		/// 	 Funcion que crea una Lista de objetos a partir de un DataTable
		/// </summary>
		/// <param name="dtsucursal" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Lista de Objetos sucursal
		/// </returns>
		internal List<EntSucUrsal> crearLista(DataTable dtsucursal)
		{
			List<EntSucUrsal> list = new List<EntSucUrsal>();
			
			EntSucUrsal obj = new EntSucUrsal();
			foreach (DataRow row in dtsucursal.Rows)
			{
				obj = crearObjeto(row);
				list.Add(obj);
			}
			return list;
		}
		
		/// <summary>
		/// 	 Funcion que crea una Lista de objetos a partir de un DataTable y con solo algunas columnas
		/// </summary>
		/// <param name="dtsucursal" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Lista de Objetos sucursal
		/// </returns>
		internal List<EntSucUrsal> crearListaRevisada(DataTable dtsucursal)
		{
			List<EntSucUrsal> list = new List<EntSucUrsal>();
			
			EntSucUrsal obj = new EntSucUrsal();
			foreach (DataRow row in dtsucursal.Rows)
			{
				obj = crearObjetoRevisado(row);
				list.Add(obj);
			}
			return list;
		}
		
		/// <summary>
		/// 	 Funcion que crea una Lista de objetos a partir de un DataTable
		/// </summary>
		/// <param name="dtsucursal" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Cola de Objetos sucursal
		/// </returns>
		internal Queue<EntSucUrsal> crearCola(DataTable dtsucursal)
		{
			Queue<EntSucUrsal> cola = new Queue<EntSucUrsal>();
			
			EntSucUrsal obj = new EntSucUrsal();
			foreach (DataRow row in dtsucursal.Rows)
			{
				obj = crearObjeto(row);
				cola.Enqueue(obj);
			}
			return cola;
		}
		
		/// <summary>
		/// 	 Funcion que crea una Lista de objetos a partir de un DataTable
		/// </summary>
		/// <param name="dtsucursal" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Pila de Objetos sucursal
		/// </returns>
		internal Stack<EntSucUrsal> crearPila(DataTable dtsucursal)
		{
			Stack<EntSucUrsal> pila = new Stack<EntSucUrsal>();
			
			EntSucUrsal obj = new EntSucUrsal();
			foreach (DataRow row in dtsucursal.Rows)
			{
				obj = crearObjeto(row);
				pila.Push(obj);
			}
			return pila;
		}
		
		/// <summary>
		/// 	 Funcion que crea un Dicionario a partir de un DataTable
		/// </summary>
		/// <param name="dtsucursal" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Diccionario de Objetos sucursal
		/// </returns>
		internal Dictionary<String, EntSucUrsal> crearDiccionario(DataTable dtsucursal)
		{
			Dictionary<String, EntSucUrsal>  miDic = new Dictionary<String, EntSucUrsal>();
			
			EntSucUrsal obj = new EntSucUrsal();
			foreach (DataRow row in dtsucursal.Rows)
			{
				obj = crearObjeto(row);
				miDic.Add(obj.id_sucursal.ToString(), obj);
			}
			return miDic;
		}
		
		/// <summary>
		/// 	 Funcion que crea un Dicionario a partir de un DataTable
		/// </summary>
		/// <param name="dtsucursal" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 HashTable de Objetos sucursal
		/// </returns>
		internal Hashtable crearHashTable(DataTable dtsucursal)
		{
			Hashtable miTabla = new Hashtable();
			
			EntSucUrsal obj = new EntSucUrsal();
			foreach (DataRow row in dtsucursal.Rows)
			{
				obj = crearObjeto(row);
				miTabla.Add(obj.id_sucursal.ToString(), obj);
			}
			return miTabla;
		}
		
		/// <summary>
		/// 	 Funcion que crea un Dicionario a partir de un DataTable y solo con columnas existentes
		/// </summary>
		/// <param name="dtsucursal" type="System.Data.DateTable">
		///     <para>
		/// 		 DataTable con el conjunto de Datos recuperados 
		///     </para>
		/// </param>
		/// <returns>
		/// 	 Diccionario de Objetos sucursal
		/// </returns>
		internal Dictionary<String, EntSucUrsal> crearDiccionarioRevisado(DataTable dtsucursal)
		{
			Dictionary<String, EntSucUrsal>  miDic = new Dictionary<String, EntSucUrsal>();
			
			EntSucUrsal obj = new EntSucUrsal();
			foreach (DataRow row in dtsucursal.Rows)
			{
				obj = crearObjetoRevisado(row);
				miDic.Add(obj.id_sucursal.ToString(), obj);
			}
			return miDic;
		}
		
		internal Dictionary<String, EntSucUrsal> crearDiccionario(DataTable dtsucursal, EntSucUrsal.Fields dicKey)
		{
			Dictionary<String, EntSucUrsal>  miDic = new Dictionary<String, EntSucUrsal>();
			
			EntSucUrsal obj = new EntSucUrsal();
			foreach (DataRow row in dtsucursal.Rows)
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

